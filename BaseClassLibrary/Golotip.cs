using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClassLibrary
{
    public class Golotip
    {
        double[] extDiffs;
        double weightA, weightB;

        TrainingData trainingData;
        public TrainingData GetTrainingData { get { return trainingData; } }

        ExamData examData;
        public ExamData GetExamData { get { return examData; } }

        

        public Golotip() {   }
        public bool Training(double[,] trainingMaterials, double weightA, double weightB)
        {
            trainingData = new TrainingData();
            this.weightA = weightA;
            this.weightB = weightB;
            extDiffs = GetExtDiffs(trainingMaterials);
            List<double[,]> matrixList = new List<double[,]>();
            double[,] trainingMatrixA = GetPropMatrix(trainingMaterials, 0);
            double[,] trainingMatrixB = GetPropMatrix(trainingMaterials, 1);
            matrixList.Add(trainingMatrixA);
            matrixList.Add(trainingMatrixB);
            double[,] trainingMatrix = GetJointMatrix(trainingMatrixA, trainingMatrixB, weightA, weightB);
            trainingData.Limit = GetLimit(trainingMatrix);
            double[,] cleanMatrix = GetCleanMatrix(trainingMatrix, trainingData.Limit);
            Dictionary<int, int[]> groups = GetGroups(cleanMatrix);
            int[] golotips = GetGolotips(trainingMatrix, groups);
            Dictionary<int, double[]> golotipsData = GetGolotipsData(trainingMaterials, golotips);
            double[] radiuses = GetRadiuses(trainingMatrix, golotips, trainingData.Limit);
            trainingData.Materials = trainingMaterials;
            trainingData.ExtDiffs = extDiffs;
            trainingData.PropMatrixList = matrixList;
            trainingData.JointMatrix = trainingMatrix;
            trainingData.CleanMatrix = cleanMatrix;
            trainingData.Groups = groups;
            trainingData.Golotips = golotipsData;
            trainingData.Radiuses = radiuses;
            return false;
        }
        public bool Training(double[,] trainingMaterials,double weightA,double weightB, double limit)
        {
            trainingData = new TrainingData();
            this.weightA = weightA;
            this.weightB = weightB;
            extDiffs = GetExtDiffs(trainingMaterials); 
            List<double[,]> matrixList = new List<double[,]>();
            double[,] trainingMatrixA = GetPropMatrix(trainingMaterials, 0);
            double[,] trainingMatrixB = GetPropMatrix(trainingMaterials, 1);
            matrixList.Add(trainingMatrixA);
            matrixList.Add(trainingMatrixB);
            double[,] trainingMatrix = GetJointMatrix(trainingMatrixA, trainingMatrixB, weightA, weightB);
            double[,] cleanMatrix = GetCleanMatrix(trainingMatrix,limit);
            Dictionary<int, int[]> groups = GetGroups(cleanMatrix);
            int[] golotips = GetGolotips(trainingMatrix, groups);
            Dictionary<int, double[]> golotipsData = GetGolotipsData(trainingMaterials, golotips);
            double[] radiuses = GetRadiuses(trainingMatrix, golotips,limit);
            trainingData.Materials = trainingMaterials;
            trainingData.ExtDiffs = extDiffs;
            trainingData.PropMatrixList = matrixList;
            trainingData.JointMatrix = trainingMatrix;
            trainingData.CleanMatrix = cleanMatrix;
            trainingData.Groups = groups;
            trainingData.Golotips = golotipsData;
            trainingData.Radiuses = radiuses;
            return false;
        }

        double[] GetExtDiffs(double[,] data)
        {
            double[] extDiffs = new double[data.GetLength(1)];
            double max, min;
            for (int i = 0; i < data.GetLength(1); i++)
            {
                max = Double.MinValue;
                min = Double.MaxValue;
                for (int j = 0; j < data.GetLength(0); j++)
                {
                    max = Math.Max(max, data[j, i]);
                    min = Math.Min(min, data[j, i]);
                }
                extDiffs[i] = max - min;
            }
            return extDiffs;
        }

        public bool Exam(double[,] examMaterials)
        {
            examData = new ExamData();
            Dictionary<int, double[,]> propVectors = GetPropVectors(examMaterials, GetTrainingData.Golotips);
            Dictionary<int, double[]> jointVectors = GetJointVectors(propVectors);
            List<int> detectedObjects = GetObjects(jointVectors, GetTrainingData.Radiuses);
            examData.PropVectors = propVectors;
            examData.JointVectors = jointVectors;
            examData.DetectedObjects = detectedObjects;
            examData.Materials = examMaterials;
            return false;
        }

        Dictionary<int,double[,]> GetPropVectors(double[,] data, Dictionary<int,double[]> golotips) 
        {
            Dictionary<int, double[,]> propVectors = new Dictionary<int, double[,]>();
            foreach (var golotip in golotips)
            {
                double[,] propVector = new double[data.GetLength(0), data.GetLength(1)];
                for(int i = 0; i < data.GetLength(0); i++)
                {
                    for(int j = 0; j < data.GetLength(1); j++)
                    {
                        propVector[i, j] = 1 - (Math.Abs(golotip.Value[j] - data[i, j])/extDiffs[j]);
                    }
                }
                propVectors.Add(golotip.Key, propVector);
            }
            return propVectors;
        }
        Dictionary<int, double[]> GetJointVectors(Dictionary<int, double[,]> propVectors)
        {
            Dictionary<int, double[]> jointVectors = new Dictionary<int, double[]>();
            foreach (var vector in propVectors)
            {
                double[] data = new double[vector.Value.GetLength(0)];
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = vector.Value[i, 0]*weightA +vector.Value[i, 1]*weightB;
                }
                jointVectors.Add(vector.Key, data);
            }
            return jointVectors;
        }

        List<int> GetObjects(Dictionary<int, double[]> jointVectors,double[] radiuses)
        {
            int i = 0;
            List<int> objectList = new List<int>();
            foreach (var vector in jointVectors)
            {
                for(int j = 0; j < vector.Value.Length; j++)
                    if(vector.Value[j] >= radiuses[i]) objectList.Add(j);
                i++;
            }
            return objectList.Distinct().ToList();
        }   
        double[,] GetPropMatrix(double[,] data, int index)
        {
            double[,] matrix = new double[data.GetLength(0), data.GetLength(0)];
            if(CheckBoolData(data,index))
                for (int i = 0; i < matrix.GetLength(0); i++)
                    for (int j = 0; j < matrix.GetLength(1); j++)
                        matrix[i, j] = data[i, index] == data[j, index] ? 1 : 0;
            else
                for (int i = 0; i < matrix.GetLength(0); i++)
                    for (int j = 0; j < matrix.GetLength(1); j++)
                        matrix[i, j] = 1 - (Math.Abs(data[i, index] - data[j, index]) / extDiffs[index]);
                    
            return matrix;
        }

        double[,] GetJointMatrix(double[,] matrixA, double[,] matrixB, double weightA,double weightB)
        {
            double[,] matrix = new double[matrixA.GetLength(0), matrixA.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    matrix[i,j] = matrixA[i,j] * weightA + matrixB[i,j] * weightB;
            return matrix;
        }

        double[,] GetCleanMatrix(double[,] matrix,double limit)
        {
            double[,] cleanMatrix = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < cleanMatrix.GetLength(0); i++)
                for (int j = 0; j < cleanMatrix.GetLength(1); j++)
                    cleanMatrix[i, j] = matrix[i, j] >= limit ? matrix[i,j] : 0; 

            return cleanMatrix;
        }

        public double GetLimit(double[,] matrix)
        {
            double avg = 0;
            int n = matrix.GetLength(0);
            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
                for (int j = i + 1; j < matrix.GetLength(1); j++)
                    avg += matrix[i, j];
            avg /= (n * (n - 1) / 2);
            avg = Math.Round(avg, 2);
            return avg;
        }

        Dictionary<int,int[]> GetGroups(double[,] matrix)
        {
            Stack<int> stack = new Stack<int>();
            Dictionary<int, int[]> groups = new Dictionary<int, int[]>();
            int groupIndex = 0;
            int[] nodes = new int[matrix.GetLength(0)];
            for(int i = 0; i < nodes.Length; i++)
                nodes[i] = 0;
            while (!CheckAllNodes(GetUncheckedNode(nodes)))
            {
                int i = GetUncheckedNode(nodes);
                stack.Push(i);
                string group = "";
                while (stack.Count != 0)
                {
                    int node = stack.Peek();
                    stack.Pop();
                    if (nodes[node] == 2) continue;
                    nodes[node] = 2;
                    for (int j = nodes.Length - 1; j >= 0; j--)
                    { 
                        if (matrix[node, j] != 0 && nodes[j] != 2)
                        { 
                            stack.Push(j);  
                            nodes[j] = 1;  
                        }
                    }
                    group += $"{node} ";
                }
                int[] groupArray = group.Split(' ')
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => int.Parse(x))
                    .ToArray();
                Array.Sort(groupArray);
                groups.Add(groupIndex, groupArray);
                groupIndex++;
                
            }

            return groups;
        }

        int[] GetGolotips(double[,] matrix,Dictionary<int,int[]> groups)
        {
            int[] golotips = new int[groups.Keys.Count];
            int i = 0; 
            foreach(var group in groups)
            {
                golotips[i] = GetGolotip(matrix,group.Value);
                i++;
            }
            return golotips;
        }
        int GetGolotip(double[,] matrix, int[] groupElements)
        {
            double[] sums = new double[groupElements.Length];
            for (int i = 0; i < groupElements.Length; i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    sums[i] += matrix[groupElements[i], j];
                }
            }
            int golotip = groupElements[GetMaxSum(sums)];
            return golotip;
        }

        private Dictionary<int, double[]> GetGolotipsData(double[,] materials, int[] golotips)
        {
            Dictionary<int, double[]> golotipsData = new Dictionary<int, double[]>();
            double[] golotipProp;
            for (int i = 0; i < golotips.Length; i++)
            {
                golotipProp = new double[materials.GetLength(1)];
                for (int j = 0; j < golotipProp.Length; j++)
                    golotipProp[j] = materials[golotips[i], j];
                golotipsData[golotips[i]] = golotipProp;
            }
            return golotipsData;
        }

        int GetMaxSum(double[] sums)
        {
            double max = sums.Max();
            for(int i = 0; i < sums.Length; i++)
                if (sums[i] == max) return i;
            return -1;
        }

        double[] GetRadiuses(double[,] matrix, int[] golotips,double limit) 
        {
            double[] radiuses = new double[golotips.Length];
            double[] row;
            for(int i = 0; i < golotips.Length; i++)
            {
                row = new double[matrix.GetLength(1)];
                for(int j = 0; j < row.Length; j++)
                {
                    row[j] = matrix[golotips[i], j];
                }
                radiuses[i] = GetRadiuse(row);
                radiuses[i] = radiuses[i] == 1 ? limit : radiuses[i];
            }
            
            return radiuses;
        }

        double GetRadiuse(double[] row)
        {
            double radiuse = double.MaxValue;
            for (int i = 0; i < row.Length; i++)
                radiuse = (row[i] < radiuse) && row[i] != 0? row[i] : radiuse;
            return radiuse;
        }

        int GetMinElement(double[] row)
        {
            double min = row.Min();
            for (int i = 0; i < row.Length; i++)
                if (row[i] == min) return i;
            return -1;
        }

        bool CheckAllNodes(int code) { return code == -1 ? true : false; }
       
        int GetUncheckedNode(int[] nodes) 
        {
            for(int i = 0; i < nodes.Length; i++)
                if (nodes[i] == 0) return i;
            return -1;
        }

        bool CheckBoolData(double[,] data,int index)
        {
            for(int i = 0; i < data.GetLength(0); i++)
                if (data[i, index] != 1 && data[i, index] != 0) return false;
            
            return true;
        }
    }

    public struct TrainingData 
    {
         
        public double[,] Materials { get; set; }
        public double[] ExtDiffs { get; set; }
        public List<double[,]> PropMatrixList { get; set; }
        public double Limit { get; set; }
        public double[,] JointMatrix { get; set; }
        public double[,] CleanMatrix { get; set; }
        public Dictionary<int, int[]> Groups { get; set; }
        public Dictionary<int, double[]> Golotips { get; set; }
        public double[] Radiuses { get; set; }
        
    }
    public struct ExamData
    {
        public double[,] Materials { get; set; }
        public Dictionary<int,double[,]> PropVectors { get; set; }
        public Dictionary<int, double[]> JointVectors { get; set; }
        public List<int> DetectedObjects { get; set; }

    }
    

}
