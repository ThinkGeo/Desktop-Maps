using System.Diagnostics;
using System.Windows.Forms;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public partial class BingMapsApplicationIdPromptForm : Form
    {
        public BingMapsApplicationIdPromptForm()
        {
            InitializeComponent();
        }

        public string ApplicationId
        {
            get { return MapKeyTextBox.Text; }
        }

        private void OkButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.bingmapsportal.com/");
        }
    }
}