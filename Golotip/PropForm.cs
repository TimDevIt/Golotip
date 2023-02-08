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
    public partial class PropForm : Form
    {
        public double weightA,weightB;
        public double limit = 0;
        public bool isTrain = false;
        public bool isExam = false;
        public PropForm()
        {
            InitializeComponent();
            trackBarWeightA.Minimum = trackBarWeightB.Maximum = 0;
            trackBarWeightA.Maximum = trackBarWeightB.Maximum = 100;
            trackBarWeightA.Value = trackBarWeightB.Value = 50;
            tbDataWeightA.Text = ((double)trackBarWeightA.Value/trackBarWeightA.Maximum)
                .ToString();
            tbDataWeightB.Text = ((double)trackBarWeightB.Value / trackBarWeightB.Maximum)
                .ToString();
            tbLimit.Enabled = false;
            weightA = (double)trackBarWeightA.Value / trackBarWeightA.Maximum;
            weightB = (double)trackBarWeightB.Value / trackBarWeightB.Maximum;
        }

        private void trackBarWeightA_Scroll(object sender, EventArgs e)
        {
            trackBarWeightB.Value = trackBarWeightB.Maximum - trackBarWeightA.Value;
            weightA = (double)trackBarWeightA.Value / trackBarWeightA.Maximum;
            weightB = (double)trackBarWeightB.Value / trackBarWeightB.Maximum;
            tbDataWeightA.Text = weightA.ToString();
            tbDataWeightB.Text = weightB.ToString();
        }

        private void trackBarWeightB_Scroll(object sender, EventArgs e)
        {
            trackBarWeightA.Value = trackBarWeightA.Maximum - trackBarWeightB.Value;
            weightA = (double)trackBarWeightA.Value / trackBarWeightA.Maximum;
            weightB = (double)trackBarWeightB.Value / trackBarWeightB.Maximum;
            tbDataWeightA.Text = weightA.ToString();
            tbDataWeightB.Text = weightB.ToString(); 
        }

        private void PropForm_Load(object sender, EventArgs e)
        {
             
        }

         

        private void tbDataWeightA_KeyUp(object sender, KeyEventArgs e)
        {
            if (!trackBarWeightB.Capture && !trackBarWeightA.Capture)
            {
                double value;
                bool succes = double.TryParse(tbDataWeightA.Text, out value);
                if (succes)
                {
                    if (value >= 0 && value <= 1)
                    {
                        weightA = value;
                        weightB = 1 - value;
                        trackBarWeightA.Value = Convert.ToInt32(weightA * 100);
                        trackBarWeightB.Value = Convert.ToInt32(weightB * 100);
                        tbDataWeightB.Text = weightB.ToString();
                    }
                    else
                    {
                        weightA = 0.5;
                        weightB = 1 - 0.5;
                        trackBarWeightA.Value = Convert.ToInt32(weightA * 100);
                        trackBarWeightB.Value = Convert.ToInt32(weightB * 100);
                        tbDataWeightA.Text = weightA.ToString();
                        tbDataWeightB.Text = weightB.ToString();
                    }
                }
            }
        }

        private void checkBoxLimit_CheckedChanged(object sender, EventArgs e)
        {
            tbLimit.Enabled = (sender as CheckBox).Checked ? true : false;
        }

        private void btnTraining_Click(object sender, EventArgs e)
        {
            if (checkBoxLimit.Checked)
            {
                bool succes = double.TryParse(tbLimit.Text, out limit);
                if (succes && limit > 0 && limit <= 1) { MessageBox.Show("Нажмите на кнопку экзамена"); }
                else MessageBox.Show("Данные введены неправильно");
            }
            else
                MessageBox.Show("Нажмите на кнопку экзамена");

            isTrain = true;
        }

        private void btnExaming_Click(object sender, EventArgs e)
        {
            if (!isTrain) MessageBox.Show("Нажмите на кнопку обучения");
            else
            {
                isExam = true;
                this.Close();
            }
        }

        private void tbDataWeightB_KeyUp(object sender, KeyEventArgs e)
        {
            if (!trackBarWeightB.Capture && !trackBarWeightA.Capture)
            {
                double value;
                bool succes = double.TryParse(((TextBox)sender).Text, out value);
                if (succes)
                {

                    if (value >= 0 && value <= 1)
                    {
                        weightB = value;
                        weightA = 1 - value;
                        trackBarWeightA.Value = Convert.ToInt32(weightA * 100);
                        trackBarWeightB.Value = Convert.ToInt32(weightB * 100);
                        tbDataWeightA.Text = weightA.ToString();
                    }
                    else
                    {
                        weightB = 0.5;
                        weightA = 1 - 0.5;
                        trackBarWeightA.Value = Convert.ToInt32(weightA * 100);
                        trackBarWeightB.Value = Convert.ToInt32(weightB * 100);
                        tbDataWeightA.Text = weightA.ToString();
                        tbDataWeightB.Text = weightB.ToString();
                    }
                }
            }
        }

         
    }
}
