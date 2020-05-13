using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public partial class BingMapsApplicationIdPromptForm : Form
    {
        public BingMapsApplicationIdPromptForm()
        {
            InitializeComponent();
        }

        public string ApplicationId
        {
            get { return applicationIdTextBox.Text; }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void BingPortalLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.bingmapsportal.com/");
        }
    }
}