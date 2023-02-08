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
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            label4.Text = "Государственный университет Дубна\n2022";
        }
    }
}
