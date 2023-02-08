using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Golotip
{
    public partial class DataGridForm : Form
    {
        public double[,] trainingMaterials;
        public double[,] examingMaterials;
        public DataGridForm()
        {
            InitializeComponent();
        }
         
        private void DataGridForm_Load(object sender, EventArgs e)
        {
            trainingDataGrid.RowCount = trainingMaterials.GetLength(0)+1;
            trainingDataGrid.ColumnCount = trainingMaterials.GetLength(1)+1;
            for (int i = 0; i < trainingDataGrid.ColumnCount; i++)
            {
                if (i == 0) trainingDataGrid.Columns[i].HeaderText = "Id";
                else trainingDataGrid.Columns[i].HeaderText = $"A{i+1}";
            }
            for (int i = 0; i < trainingDataGrid.RowCount-1; i++)
            {
                for(int j = 0;j< trainingDataGrid.ColumnCount-1; j++)
                {
                    if (j == 0)
                    {
                        trainingDataGrid[j, i].Value = i + 1;
                    }
                    trainingDataGrid[j+1, i].Value = trainingMaterials[i, j];
                    
                }
            }
            examingDataGrid.RowCount = examingMaterials.GetLength(0) + 1;
            examingDataGrid.ColumnCount = examingMaterials.GetLength(1) + 1;
            for (int i = 0; i < examingDataGrid.ColumnCount; i++)
            {
                if (i == 0) examingDataGrid.Columns[i].HeaderText = "Id";
                else examingDataGrid.Columns[i].HeaderText = $"A{i + 1}";
            }
            for (int i = 0; i < examingDataGrid.RowCount - 1; i++)
            {
                for (int j = 0; j < examingDataGrid.ColumnCount - 1; j++)
                {
                    if (j == 0)
                    {
                        examingDataGrid[j, i].Value = i + 1;
                    }
                    examingDataGrid[j + 1, i].Value = examingMaterials[i, j];

                }
            }
        }
    }
}
