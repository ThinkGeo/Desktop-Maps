using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public partial class BingMapsApplicationIdPromptForm : Form
    {
        public BingMapsApplicationIdPromptForm()
        {
            InitializeComponent();
        }

        public string ApplicationId
        {
            get { return ApplicationIdTextBox.Text; }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.bingmapsportal.com/");
        }
    }
}