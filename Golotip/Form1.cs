using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using Golotip1 = BaseClassLibrary.Golotip;
using ZedGraph;
using BaseClassLibrary;

namespace Golotip
{
    public partial class Form1 : Form
    {
        string filePath;
        double[,] trainMaterials;
        double[,] examMaterials;
        Golotip1 golotip1;
        CreatingForm createForm;
        public Form1()
        {
            InitializeComponent();
            golotip1 = new Golotip1();
            fileDialog.Filter = "Excel files(*.xlsx)|*.xlsx|Excel Files 2009(*.xls*)|*.xls*";
            dataGraph.IsShowPointValues = true;

            // Будем обрабатывать событие PointValueEvent, чтобы изменить формат представления координат
            dataGraph.PointValueEvent +=
                new ZedGraphControl.PointValueHandler(dataGraph_PointValueEvent);
            dataGraph.GraphPane.Title.Text = "Голотип-1";
            dataGraph.GraphPane.XAxis.Title.Text = "Свойство 1";
            dataGraph.GraphPane.YAxis.Title.Text = "Свойство 2";


        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = GenericSingleton<HelpForm>.CreateInstrance();
            helpForm.Show();
        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridForm dataForm = GenericSingleton<DataGridForm>.CreateInstrance();
            if(trainMaterials != null && examMaterials != null)
            {
                dataForm.trainingMaterials = trainMaterials;
                dataForm.examingMaterials = examMaterials;
                dataForm.Show();

            }
            else
            {
                MessageBox.Show("Загрузите или создайте данные");
            }

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = fileDialog.FileName;
                OpenExcelFile(filePath);
                DrawTrainingElements(trainMaterials);
            }
            
        }
        
         
        private void OpenExcelFile(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets[0];
                if (workSheet.Cells["A1"] != null)
                {
                    int rowCount = workSheet.Dimension.Rows;
                    int colCount = workSheet.Dimension.Columns;
                    int startElemRow = workSheet.Dimension.Start.Row;
                    int startElemCol = workSheet.Dimension.Start.Column;
                    double[,] data_mas = new double[rowCount, colCount];
                    string text = "";
                    object objectVal;
                    string[] tags = new string[4] {
                                    "training start",
                                    "training end",
                                    "examing start",
                                    "examing end" };
                    Dictionary<string, int[]> tagPositions = new Dictionary<string, int[]>();
                    for (int i = 0; i < 4; i++)
                    {
                        tagPositions.Add(tags[i], new int[2]);
                    }
                    for (int i = 0; i < data_mas.GetLength(0); i++)
                    {
                        for (int j = 0; j < data_mas.GetLength(1); j++)
                        {
                            objectVal = workSheet.Cells[startElemRow + i, startElemCol + j].Value;
                            if (IsTag(objectVal))
                            {
                                if (tagPositions.ContainsKey(objectVal.ToString()))
                                {
                                    tagPositions[objectVal.ToString()] = new int[] { startElemRow + i, startElemCol + j };
                                    //text += $"{objectVal.ToString()} : [{(startElemCol + i).ToString()},{(startElemCol + j).ToString()}] ";
                                }
                            }
                        }

                    }
                    
                    //get training materials
                    int trainRowCount = tagPositions["training end"][0] - tagPositions["training start"][0] -1 ;
                    int trainColCount = 0;
                    int currentRow = tagPositions["training start"][0] + 2;
                    int currentCol = tagPositions["training start"][1]; 
                    var currentElement = workSheet.Cells[currentRow,currentCol];
                    while (currentElement.Value != null)
                    {
                        currentCol++;
                        trainColCount++;
                        currentElement = workSheet.Cells[currentRow, currentCol];
                    }
                    trainMaterials = new double[trainRowCount,trainColCount];
                    currentRow = tagPositions["training start"][0] + 1;
                    currentCol = tagPositions["training start"][1];
                    for (int i =0;i< trainMaterials.GetLength(0); i++)
                    {
                        for(int j = 0; j < trainMaterials.GetLength(1); j++)
                        {
                            objectVal = workSheet.Cells[currentRow + i, currentCol + j].Value;
                            string stringVal = objectVal.ToString();
                            trainMaterials[i, j] = Convert.ToDouble(stringVal);
                        }
                         
                    }
                   
                    // examing materials
                    int examRowCount = tagPositions["examing end"][0] - tagPositions["examing start"][0] -1 ;
                    int examColCount = 0;
                    currentRow = tagPositions["examing start"][0] + 1;
                    currentCol = tagPositions["examing start"][1];
                    currentElement = workSheet.Cells[currentRow, currentCol];
                    while (currentElement.Value != null)
                    {
                        currentCol++;
                        examColCount++;
                        currentElement = workSheet.Cells[currentRow, currentCol];
                    }
                    examMaterials = new double[examRowCount, examColCount];
                    currentRow = tagPositions["examing start"][0] + 1;
                    currentCol = tagPositions["examing start"][1];
                    for (int i = 0; i < examMaterials.GetLength(0); i++)
                    {
                        for (int j = 0; j < examMaterials.GetLength(1); j++)
                        {
                            objectVal = workSheet.Cells[currentRow + i, currentCol + j].Value;
                            string stringVal = objectVal.ToString();
                            examMaterials[i, j] = Convert.ToDouble(stringVal);
                             
                        }
                        
                    }

                    MessageBox.Show("Данные загружены");
                     
                } 
                else
                    MessageBox.Show("Excel данные не верны.\nПосмотрите правила в Help");

                excelPackage.Save();
            }

        }

        private void DrawTrainingElements(double[,] materials)
        {
            double[] maxs = GetMaxPropValues(trainMaterials);
            double[] mins = GetMinPropValues(trainMaterials);
            double xmin, xmax, ymin, ymax;
            double posX, posY;
            xmin = mins[0];
            xmax = maxs[0];
            ymin = mins[1];
            ymax = maxs[1];
            GraphPane pane = dataGraph.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Title.Text = "Свойство 1";
            pane.YAxis.Title.Text = "Свойство 2";

            // Изменим текст заголовка графика
            pane.Title.Text = "Голотип-1";
            PointPairList objectList = new PointPairList();
            int pointCount = materials.GetLength(0);
            
            for (int i = 0; i < pointCount; i++)
            {
                posX = materials[i, 0];
                posY = materials[i, 1];
                objectList.Add(posX, posY);
            }
             

            LineItem objectCurve = pane.AddCurve("МО",
                objectList,
                Color.Black,
                SymbolType.Circle);
            objectCurve.Line.IsVisible = false;
            objectCurve.Symbol.Fill.Color = Color.LightGreen;
            objectCurve.Symbol.Fill.Type = FillType.Solid;
            objectCurve.Symbol.Size = 10;
            pane.XAxis.MajorGrid.IsVisible = true;

            // Задаем вид пунктирной линии для крупных рисок по оси X:
            // Длина штрихов равна 10 пикселям, ...
            pane.XAxis.MajorGrid.DashOn = 10;

            // затем 5 пикселей - пропуск
            pane.XAxis.MajorGrid.DashOff = 5;


            // Включаем отображение сетки напротив крупных рисок по оси Y
            pane.YAxis.MajorGrid.IsVisible = true;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            pane.YAxis.MajorGrid.DashOn = 10;
            pane.YAxis.MajorGrid.DashOff = 5;


            // Включаем отображение сетки напротив мелких рисок по оси X
            pane.YAxis.MinorGrid.IsVisible = true;

            // Задаем вид пунктирной линии для крупных рисок по оси Y:
            // Длина штрихов равна одному пикселю, ...
            pane.YAxis.MinorGrid.DashOn = 1;

            // затем 2 пикселя - пропуск
            pane.YAxis.MinorGrid.DashOff = 2;

            // Включаем отображение сетки напротив мелких рисок по оси Y
            pane.XAxis.MinorGrid.IsVisible = true;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            pane.XAxis.MinorGrid.DashOn = 1;
            pane.XAxis.MinorGrid.DashOff = 2;
            pane.XAxis.Scale.Min = xmin - 10;
            pane.XAxis.Scale.Max = xmax + 10;
            pane.YAxis.Scale.Min = ymin - 10;
            pane.YAxis.Scale.Max = ymax + 10;
            dataGraph.AxisChange();
            dataGraph.Invalidate();
        }

        private double[] GetMaxPropValues(double[,] materials)
        {
            double[] maxValues = new double[materials.GetLength(1)];
            for (int i = 0; i < materials.GetLength(1); i++) 
            {
                double max = double.MinValue;
                for (int j = 0; j < materials.GetLength(0); j++)
                {
                    max = max < materials[j, i] ? materials[j, i] : max;
                }
                maxValues[i] = max;
            }
            return maxValues;
        }
        private double[] GetMinPropValues(double[,] materials)
        {
            double[] minValues = new double[materials.GetLength(1)];
            for (int i = 0; i < materials.GetLength(1); i++)
            {
                double min = double.MaxValue;
                for (int j = 0; j < materials.GetLength(0); j++)
                {
                    min = min > materials[j, i] ? materials[j, i] : min;
                }
                minValues[i] = min;
            }
            return minValues;
        }

        private bool IsTag(object value)
        {
            string[] tags = new string[4] { 
                "training start", 
                "training end",
                "examing start",
                "examing end" };
            string stringVal = value != null &&(value.ToString() is String) ? value.ToString() : "";
            foreach(var tag in tags)
            {
                if (stringVal == tag)
                    return true;
            }
            return false;
        }
        private void propStripMenuItem_Click(object sender, EventArgs e)
        {
            if(trainMaterials != null)
            {
                PropForm propForm = GenericSingleton<PropForm>.CreateInstrance();
                propForm.Show();
                propForm.FormClosing += (sender1, e1) => {
                    if (propForm.isTrain)
                    {
                        if (propForm.limit == 0)
                            golotip1.Training(trainMaterials, propForm.weightA, propForm.weightB);
                        else
                            golotip1.Training(trainMaterials, propForm.weightA, propForm.weightB, propForm.limit);
                        AddTrainingDataToExcel(golotip1.GetTrainingData);
                        AddTrainingDataToGraph(golotip1.GetTrainingData);
                    }
                    if (propForm.isExam)
                    {
                        golotip1.Exam(examMaterials);
                        AddExamingDataToExcel(golotip1.GetExamData);
                        AddExamingDataToGraph(golotip1.GetExamData, golotip1.GetTrainingData);
                    }
                    
                };
            }
            else
            {
                MessageBox.Show("Создайте или загрузите данные");
            }
            
        }

        private void AddTrainingDataToExcel(TrainingData trainingData)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelWorksheet workSheet;
                if (excelPackage.Workbook.Worksheets["Training"] == null)
                    workSheet = excelPackage.Workbook.Worksheets.Add("Training");
                else
                    workSheet = excelPackage.Workbook.Worksheets["Training"];
                workSheet.Cells.Clear();
                int startRow = 1;
                int startCol = 1;
                //Print Exts
                for(int i = 0; i < trainingData.ExtDiffs.Length; i++)
                {
                    for(int j = 0; j < trainingData.ExtDiffs.Length; j++)
                    {
                        if (startRow == 1 && startCol == 1)
                        {
                            workSheet.Cells[startRow, startCol + j].Value = $"Ext{startCol + j}";
                        }
                        else workSheet.Cells[startRow, startCol + j].Value = trainingData.ExtDiffs[j];
                    }
                    startRow++;
                }
                startRow++;
                //Print PropMatrixies
                List<double[,]> matrixList = trainingData.PropMatrixList;
                int matrixIndex = 1;
                foreach(var matrix in matrixList)
                {
                    for (int i = 0; i <= matrix.GetLength(1); i++)
                    {
                        if (startCol == 1) 
                            workSheet.Cells[startRow, startCol++].Value = $"PropMatrix{matrixIndex}";
                        else
                            workSheet.Cells[startRow, startCol++].Value = $"A{i}";
                    }
                    startCol = 1;
                    matrixIndex++;
                    startRow++;
                    startCol++;
                    for(int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for(int j =0; j < matrix.GetLength(1); j++)
                        {
                            workSheet.Cells[startRow, 1].Value = $"A{i+1}";
                            workSheet.Cells[startRow, startCol + j].Value = matrix[i,j];
                        }
                        startRow++;
                    }
                    startCol = 1;
                    startRow++;   
                }
                //joint matrix
                double[,] jointMatrix = trainingData.JointMatrix;
                for (int i = 0; i <= jointMatrix.GetLength(1); i++)
                {
                    if (startCol == 1)
                        workSheet.Cells[startRow, startCol++].Value = $"JointMatrix";
                    else
                        workSheet.Cells[startRow, startCol++].Value = $"A{i}";
                }
                startCol = 1;
                startRow++;
                startCol++;
                for (int i = 0; i < jointMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < jointMatrix.GetLength(1); j++)
                    {
                        workSheet.Cells[startRow, 1].Value = $"A{i + 1}";
                        workSheet.Cells[startRow, startCol + j].Value = jointMatrix[i, j];
                    }
                    startRow++;
                }
                startCol = 1;
                startRow++;
                //limit
                workSheet.Cells[startRow++, startCol].Value = $"Limit";
                workSheet.Cells[startRow++, startCol].Value = trainingData.Limit;
                //Clean Matrix
                double[,] cleanMatrix = trainingData.CleanMatrix;
                for (int i = 0; i <= cleanMatrix.GetLength(1); i++)
                {
                    if (startCol == 1)
                        workSheet.Cells[startRow, startCol++].Value = $"CleanMatrix";
                    else
                        workSheet.Cells[startRow, startCol++].Value = $"A{i}";
                }
                startCol = 1;
                startRow++;
                startCol++;
                for (int i = 0; i < cleanMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < cleanMatrix.GetLength(1); j++)
                    {
                        workSheet.Cells[startRow, 1].Value = $"A{i + 1}";
                        workSheet.Cells[startRow, startCol + j].Value = cleanMatrix[i, j];
                    }
                    startRow++;
                }
                startCol = 1;
                startRow++;
                //groups
                var groups = trainingData.Groups;
                var golotips = trainingData.Golotips;
                var radiuses = trainingData.Radiuses;
                int tmpRow1 = startRow;
                int tmpRow2 = startRow;
                int maxLength = int.MinValue;
                foreach (var group in groups) 
                    maxLength = group.Value.Length > maxLength ? group.Value.Length : maxLength;
                foreach (var group in groups)
                {
                    workSheet.Cells[startRow, 1].Value = $"group{group.Key}";
                    workSheet.Cells[startRow, startCol + maxLength].Value = "golotip";
                    workSheet.Cells[startRow++, startCol + maxLength + 1].Value = "radius";
                    for (int i =0; i < group.Value.Length; i++)
                    {
                        workSheet.Cells[startRow,startCol + i].Value = group.Value[i];
                    }
                    startRow++;
                }
                foreach(var golotip in golotips)
                {
                    workSheet.Cells[tmpRow1 + 1, startCol + maxLength].Value = golotip.Key;
                    tmpRow1++; tmpRow1++;
                }
                foreach (var radius in radiuses)
                {
                    workSheet.Cells[tmpRow2 + 1, startCol + maxLength+1].Value = radius;
                    tmpRow2++; tmpRow2++;
                }


                excelPackage.Save();
                MessageBox.Show("Обучение завершено");
            }
        }

        private void AddTrainingDataToGraph(TrainingData trainingData)
        {
            double[,] trainMaterials = trainingData.Materials;
            var golotips = trainingData.Golotips;
            double[] maxs = GetMaxPropValues(trainMaterials);
            double[] mins = GetMinPropValues(trainMaterials);
            double xmin, xmax, ymin, ymax;
            double posX, posY;
            xmin = mins[0];
            xmax = maxs[0];
            ymin = mins[1];
            ymax = maxs[1];
            GraphPane pane = dataGraph.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Title.Text = "Свойство 1";
            pane.YAxis.Title.Text = "Свойство 2";
            pane.Title.Text = "Голотип-1";
            PointPairList objectList = new PointPairList();
            PointPairList golotipList = new PointPairList();
            int pointCount = trainMaterials.GetLength(0);
            bool isGolotip;
            for (int i = 0; i < pointCount; i++)
            {
                posX = trainMaterials[i, 0];
                posY = trainMaterials[i, 1];
                isGolotip = false;
                foreach(var golotip in golotips)
                {
                    if (golotip.Key == i) isGolotip = true;
                }
                if (isGolotip) golotipList.Add(posX,posY);
                else objectList.Add(posX, posY);
            }


            LineItem objectCurve = pane.AddCurve("МО",
                objectList,
                Color.Black,
                SymbolType.Circle);
            objectCurve.Line.IsVisible = false;
            objectCurve.Symbol.Fill.Color = Color.LightGreen;
            objectCurve.Symbol.Fill.Type = FillType.Solid;
            objectCurve.Symbol.Size = 10;
            LineItem golotipCurve = pane.AddCurve("Golotip",
                golotipList,
                Color.Black,
                SymbolType.Diamond);
            golotipCurve.Line.IsVisible = false;
            golotipCurve.Symbol.Fill.Color = Color.Red;
            golotipCurve.Symbol.Fill.Type = FillType.Solid;
            golotipCurve.Symbol.Size = 10;
            pane.XAxis.MajorGrid.IsVisible = true;

            // Задаем вид пунктирной линии для крупных рисок по оси X:
            // Длина штрихов равна 10 пикселям, ...
            pane.XAxis.MajorGrid.DashOn = 10;

            // затем 5 пикселей - пропуск
            pane.XAxis.MajorGrid.DashOff = 5;


            // Включаем отображение сетки напротив крупных рисок по оси Y
            pane.YAxis.MajorGrid.IsVisible = true;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            pane.YAxis.MajorGrid.DashOn = 10;
            pane.YAxis.MajorGrid.DashOff = 5;


            // Включаем отображение сетки напротив мелких рисок по оси X
            pane.YAxis.MinorGrid.IsVisible = true;

            // Задаем вид пунктирной линии для крупных рисок по оси Y:
            // Длина штрихов равна одному пикселю, ...
            pane.YAxis.MinorGrid.DashOn = 1;

            // затем 2 пикселя - пропуск
            pane.YAxis.MinorGrid.DashOff = 2;

            // Включаем отображение сетки напротив мелких рисок по оси Y
            pane.XAxis.MinorGrid.IsVisible = true;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            pane.XAxis.MinorGrid.DashOn = 1;
            pane.XAxis.MinorGrid.DashOff = 2;
            pane.XAxis.Scale.Min = xmin - 10;
            pane.XAxis.Scale.Max = xmax + 10;
            pane.YAxis.Scale.Min = ymin - 10;
            pane.YAxis.Scale.Max = ymax + 10;
            dataGraph.AxisChange();
            dataGraph.Invalidate();

        }
        private void AddExamingDataToExcel(ExamData examData)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelWorksheet workSheet;
                if (excelPackage.Workbook.Worksheets["Exam"] == null)
                    workSheet = excelPackage.Workbook.Worksheets.Add("Exam");
                else
                    workSheet = excelPackage.Workbook.Worksheets["Exam"];
                workSheet.Cells.Clear();
                int startRow = 1;
                int startCol = 1;
                int nextRow = 1 ;
                //prop Vectors
                var propVectors = examData.PropVectors;
                var index = 1;
                foreach(var vector in propVectors)
                {
                    nextRow = vector.Value.GetLength(0);
                    workSheet.Cells[startRow++, startCol].Value = $"PropVector{vector.Key}";
                    for(int i = 0; i < vector.Value.GetLength(0); i++)
                    {
                        for(int j = 0; j < vector.Value.GetLength(1); j++)
                        {
                            workSheet.Cells[startRow, startCol + j].Value = vector.Value[i, j];
                        }
                        startRow++;
                    }
                    startRow = 1;
                    startCol = 1 + vector.Value.GetLength(1) * index;
                    index++;

                }
                //joint vectors
                startRow = 1 + nextRow;
                startCol = 1;
                startRow++;
                var jointVectors = examData.JointVectors;
                foreach (var vector in jointVectors)
                {
                    workSheet.Cells[startRow, startCol++].Value = $"JVector{vector.Key}";
                }
                startRow++;
                int tmpRow = startRow;
                int lastRow = startRow;
                startCol = 1;
                foreach (var vector in jointVectors)
                {
                    foreach(var elem in vector.Value)
                    {
                        workSheet.Cells[startRow++, startCol].Value = elem;
                    }
                    lastRow = startRow;
                    startRow = tmpRow;
                    startCol++;
                }
                var detectedObjs = examData.DetectedObjects;
                startRow = lastRow;
                startRow++;
                startRow++;
                startCol = 1;
                workSheet.Cells[startRow, startCol].Value = "DetectedObjs";
                startRow++;
                foreach (var obj in detectedObjs)
                {
                    workSheet.Cells[startRow, startCol++].Value = obj;
                }
                startRow++;
                excelPackage.Save();
                MessageBox.Show("Экзамен завершен");
            }
        }
        private void AddExamingDataToGraph(ExamData examData,TrainingData trainData)
        {
            double[,] trainMaterials = trainData.Materials;
            double[,] examMaterials = examData.Materials;
            var golotips = trainData.Golotips;
            double[] maxs = GetMaxPropValues(trainMaterials);
            double[] mins = GetMinPropValues(trainMaterials);
            double xmin, xmax, ymin, ymax;
            double posX, posY;
            xmin = mins[0];
            xmax = maxs[0];
            ymin = mins[1];
            ymax = maxs[1];
            GraphPane pane = dataGraph.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Title.Text = "Свойство 1";
            pane.YAxis.Title.Text = "Свойство 2";
            pane.Title.Text = "Голотип-1";
            PointPairList trainObjsList = new PointPairList();
            PointPairList examObjsList = new PointPairList();
            PointPairList golotipList = new PointPairList();
            PointPairList detectObjsList = new PointPairList();
            int pointCount = trainMaterials.GetLength(0);
            bool isGolotip;
            for (int i = 0; i < pointCount; i++)
            {
                posX = trainMaterials[i, 0];
                posY = trainMaterials[i, 1];
                isGolotip = false;
                foreach (var golotip in golotips)
                {
                    if (golotip.Key == i) isGolotip = true;
                }
                if (isGolotip) golotipList.Add(posX, posY);
                else trainObjsList.Add(posX, posY);
            }
            pointCount = examMaterials.GetLength(0);
            var detectedObjs = examData.DetectedObjects;
            bool detected = false;
            for (int i = 0; i < pointCount; i++)
            {
                posX = examMaterials[i, 0];
                posY = examMaterials[i, 1];
                
                detected = false;
                foreach (var elem in detectedObjs)
                {
                    if (elem == i) detected = true;
                }
                if (detected) detectObjsList.Add(posX, posY);
                else examObjsList.Add(posX, posY); 
            }

             
            LineItem trainObjCurve = pane.AddCurve("МО",
                trainObjsList,
                Color.Black,
                SymbolType.Circle);
            trainObjCurve.Line.IsVisible = false;
            trainObjCurve.Symbol.Fill.Color = Color.LightGreen;
            trainObjCurve.Symbol.Fill.Type = FillType.Solid;
            trainObjCurve.Symbol.Size = 6;
            LineItem golotipCurve = pane.AddCurve("Golotip",
                golotipList,
                Color.Black,
                SymbolType.Diamond);
            golotipCurve.Line.IsVisible = false;
            golotipCurve.Symbol.Fill.Color = Color.Red;
            golotipCurve.Symbol.Fill.Type = FillType.Solid;
            golotipCurve.Symbol.Size = 6;
            LineItem examObjCurve = pane.AddCurve("МE",
                examObjsList,
                Color.Black,
                SymbolType.Circle);
            examObjCurve.Line.IsVisible = false;
            examObjCurve.Symbol.Fill.Color = Color.Orange;
            examObjCurve.Symbol.Fill.Type = FillType.Solid;
            examObjCurve.Symbol.Size = 6;
            LineItem detectObjCurve = pane.AddCurve("Result",
                detectObjsList,
                Color.Black,
                SymbolType.Square);
            detectObjCurve.Line.IsVisible = false;
            detectObjCurve.Symbol.Fill.Color = Color.LightGreen;
            detectObjCurve.Symbol.Fill.Type = FillType.Solid;
            detectObjCurve.Symbol.Size = 6;
            pane.XAxis.Scale.Min = xmin - 10;
            pane.XAxis.Scale.Max = xmax + 10;
            pane.YAxis.Scale.Min = ymin - 10;
            pane.YAxis.Scale.Max = ymax + 10;
            pane.XAxis.MajorGrid.IsVisible = true;
            pane.XAxis.MajorGrid.DashOn = 10;
            pane.XAxis.MajorGrid.DashOff = 5;
            pane.YAxis.MajorGrid.IsVisible = true;
            pane.YAxis.MajorGrid.DashOn = 10;
            pane.YAxis.MajorGrid.DashOff = 5;
            pane.YAxis.MinorGrid.IsVisible = true;
            pane.YAxis.MinorGrid.DashOn = 1;
            pane.YAxis.MinorGrid.DashOff = 2;
            pane.XAxis.MinorGrid.IsVisible = true;
            pane.XAxis.MinorGrid.DashOn = 1;
            pane.XAxis.MinorGrid.DashOff = 2;
            dataGraph.AxisChange();
            dataGraph.Invalidate();
        }
        private string dataGraph_PointValueEvent(ZedGraphControl sender, GraphPane pane, CurveItem curve, int iPt)
        {
            PointPair point = curve[iPt];
            var trainingData = golotip1.GetTrainingData;
            var materials = trainMaterials;
            var golotips = trainingData.Golotips;
            var groups = trainingData.Groups;
            var radiuses = trainingData.Radiuses;
            var examMaterials = golotip1.GetExamData.Materials;
            // Сформируем строку
            string result;
            int golotipIndex = -1;
            bool isNull = false;
            if (golotips is null)
            {
                isNull = true;
            }
            if (!isNull)
            {
                foreach(var golotip in golotips)
                {
                    if (golotip.Value[0] == point.X && golotip.Value[1] == point.Y)
                    {
                        golotipIndex = golotip.Key;
                    }
                }
            }

            if (golotipIndex == -1)
            {
                if (examMaterials != null)
                {

                    result = string.Format("X: {0:F3}\nY: {1:F3}\nId: {2}",
                        point.X,
                        point.Y,
                        GetElemId(point.X, point.Y, materials) != -1 ? GetElemId(point.X, point.Y, materials) : GetElemId(point.X, point.Y, examMaterials) != -1 ? GetElemId(point.X, point.Y, examMaterials) : -1);
                }
                else
                {
                    result = string.Format("X: {0:F3}\nY: {1:F3}\nId: {2}",
                        point.X,
                        point.Y,
                        GetElemId(point.X, point.Y, materials));
                }
            }
 
            else
            {
                int i = 0;
                foreach (var group in groups)
                {
                    foreach(var elem in group.Value)
                    {
                        if (elem == golotipIndex)
                        {
                            i = group.Key;
                            break;
                        }
                    }
                   
                }
                    
                result = string.Format("X: {0:F3}\nY: {1:F3}\nS: {2:0.00}\nR: {3:0.00}\nId: {4}",
                    point.X,
                    point.Y,
                    groups[i].Length,
                    radiuses[i],
                    GetElemId(point.X, point.Y,materials));
            }
                

            return result;
        }

        
        private int GetElemId(double x,double y,double[,] materials)
        {
            for(int i = 0; i < materials.GetLength(0); i++)
            {
                if (x == materials[i, 0] && y == materials[i, 1])
                    return i + 1;
            }
            return -1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }



        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createForm = GenericSingleton<CreatingForm>.CreateInstrance();
            createForm.Show();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files(*.xlsx)|*.xlsx|Excel Files 2009(*.xls*)|*.xls*";
            createForm.FormClosing += (sender1,e1) => {
                trainMaterials = createForm.trainMaterials;
                examMaterials = createForm.examMaterials;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    //Set some properties of the Excel document

                    excelPackage.Workbook.Properties.Author = "User";
                    excelPackage.Workbook.Properties.Title = "Data";
                    excelPackage.Workbook.Properties.Subject = "export_data";
                    excelPackage.Workbook.Properties.Created = DateTime.Now;

                    //Create the WorkSheet
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("data");
                    int row = 1; int col = 1;
                    worksheet.Cells[row, col].Value = "training start";
                    for (int i = 0; i < trainMaterials.GetLength(0); i++)
                    {
                        for (int j = 0; j < trainMaterials.GetLength(1); j++)
                        {
                            worksheet.Cells[row + i + 1, col + j].Value = trainMaterials[i, j];
                        }
                    }
                    worksheet.Cells[trainMaterials.GetLength(0) + 2, col].Value = "training end";
                    col = trainMaterials.GetLength(1) + 2;
                    worksheet.Cells[row, col].Value = "examing start";
                    for (int i = 0; i < examMaterials.GetLength(0); i++)
                    {
                        for (int j = 0; j < examMaterials.GetLength(1); j++)
                        {
                            worksheet.Cells[row + i + 1, col + j].Value = examMaterials[i, j];
                        }
                    }
                    worksheet.Cells[examMaterials.GetLength(0) + 2, col].Value = "examing end";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        filePath = saveFileDialog.FileName;
                        FileInfo fi = new FileInfo(filePath);
                        excelPackage.SaveAs(fi);
                        DrawTrainingElements(trainMaterials);
                        MessageBox.Show("Данные сгенерированы");
                    }
                   
                    
                }


                
            };
            
        }
       
        
    }
}
