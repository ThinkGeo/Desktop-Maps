using System;
using System.Windows.Forms;

namespace TopologyValidation
{
    public partial class ToleranceForm : Form
    {
        private double tolerance;

        public double Tolerance
        {
            get { return tolerance; }
            set { tolerance = value; }
        }

        public ToleranceForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tolerance = double.Parse(textBox1.Text);
            this.DialogResult = DialogResult.OK;
        }
    }
}
