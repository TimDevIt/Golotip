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
    public partial class CreatingForm : Form
    {
        public double[,] trainMaterials, examMaterials;
        public bool acceptData = false;
        public CreatingForm()
        {
            
            InitializeComponent();
        }

        private void CreatingForm_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            saveFileDialog.Filter = "Excel files(*.xlsx)|*.xlsx|Excel Files 2009(*.xls*)|*.xls*";
            this.button1.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int countA,countB;
            double minA, maxA, minB, maxB;
            bool succesA, succesB;
            bool succesMinA, succesMinB, succesMaxA, succesMaxB;
            succesA = int.TryParse(tbCountA.Text,out countA);
            succesB = int.TryParse(tbCountB.Text, out countB);
            succesMinA = double.TryParse(tbMinValueA.Text, out minA);
            succesMinB = double.TryParse(tbMinValueB.Text, out minB);
            succesMaxA = double.TryParse(tbMaxValueA.Text, out maxA);
            succesMaxB = double.TryParse(tbMaxValueB.Text, out maxB);
            if (succesA && succesB && succesMinA && succesMaxA && succesMinB && succesMaxB && minA < maxA && minB < maxB) { }
            {
                trainMaterials = new double[countA, 2];
                examMaterials = new double[countB, 2];
                Random rand = new Random();
                string[] minValues = new string[2];
                string[] temp = minA.ToString().Split(',');
                if (temp.Length < 2)
                {
                    minValues[0] = temp[0];
                    minValues[1] = "0";
                }
                else minValues = temp;
                string[] maxValues = new string[2];
                temp = maxA.ToString().Split(',');
                if (temp.Length < 2)
                {
                    maxValues[0] = temp[0];
                    maxValues[1] = "0";
                }
                else maxValues = temp;
                for (int i = 0; i < trainMaterials.GetLength(0); i++)
                {
                    for (int j = 0; j < trainMaterials.GetLength(1); j++)
                    {
                        int minLeft = int.Parse(minValues[0]);
                        int maxLeft = int.Parse(maxValues[0]);
                        int minRight = int.Parse(minValues[1]);
                        int maxRight = int.Parse(maxValues[1]);
                        
                        double rightNumber = Convert.ToDouble(rand.Next(0,Math.Max(minLeft, maxRight)));
                        double randNumber = Convert.ToDouble($"{rand.Next(minLeft, maxLeft)},{rightNumber}");
                        trainMaterials[i, j] = randNumber;
                    }
                }
                minValues = new string[2];
                temp = minB.ToString().Split(',');
                if (temp.Length < 2)
                {
                    minValues[0] = temp[0];
                    minValues[1] = "0";
                }
                else minValues = temp;
                maxValues = new string[2];
                temp = maxB.ToString().Split(',');
                if (temp.Length < 2)
                {
                    maxValues[0] = temp[0];
                    maxValues[1] = "0";
                }
                else maxValues = temp;
                for (int i = 0; i < examMaterials.GetLength(0); i++)
                {
                    for (int j = 0; j < examMaterials.GetLength(1); j++)
                    {
                        int minLeft = int.Parse(minValues[0]);
                        int maxLeft = int.Parse(maxValues[0]);
                        int minRight = int.Parse(minValues[1]);
                        int maxRight= int.Parse(maxValues[1]);
                        int num = rand.Next(0, Math.Max(minRight,maxRight));
                        double rightNumber = Convert.ToDouble(num);
                        double randNumber = Convert.ToDouble($"{rand.Next(minLeft, maxLeft)},{rightNumber}");
                        examMaterials[i, j] = randNumber;
                    }
                }
                acceptData = true;
            }
            this.Close();
        }
    }
}
