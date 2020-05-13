using System;
using System.IO;
using System.Windows.Forms;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    // This is the initial user control in CustomDataSelectorUserControl
    // This would let user browse and choose a custom shape file. 
    public partial class SelectCustomDataUserControl : UserControl
    {
        public SelectCustomDataUserControl()
        {
            InitializeComponent();
        }

        public Action<string> FileSelected { get; set; }

        public string SelectedPathFilename
        {
            get { return txtPathFilename.Text; }
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Shape Files|*.shp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtPathFilename.Text = openFileDialog.FileName;
                if (File.Exists(openFileDialog.FileName))
                {
                    OnFileSelected(openFileDialog.FileName);
                }
            }
        }

        protected void OnFileSelected(string pathFileName)
        {
            Action<string> handler = FileSelected;
            if (handler != null)
            {
                handler(pathFileName);
            }
        }
    }
}