using System;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Shapes;

namespace TopologyValidation
{
    public partial class SaveTestCaseDialog : Form
    {
        public string Description { get; set; }
        public string TestCaseName { get; set; }

        public SaveTestCaseDialog(string currentTopologyCaseName, string description, string folder, RectangleShape currentExtent)
        {
            InitializeComponent();
            this.TestCaseName = GetSplittedName(currentTopologyCaseName.Replace("TopologyTestCase", ""));
            this.Description = description;
            string path = folder + "\\" + TestCaseName;

            int count = 1;
            string tempPath = path + ".xml";
            while (File.Exists(tempPath))
            {
                tempPath = path + "_" + count + ".xml";
                count++;
            }

            txtName.Text = this.TestCaseName + (count - 1 > 0 ? ("_" + (count - 1)) : "");
            txtDescription.Text = this.Description;

            if (txtName.Text.Trim() == string.Empty)
            {
                txtName.Text = "New Test Case";
            }

            if (txtDescription.Text.Trim() == string.Empty)
            {
                txtDescription.Text = String.Format("{2}{3}. Created at {0} {1}.", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), GetSplittedName(currentTopologyCaseName), currentExtent == null ? "" : " Within Extent(" + currentExtent.ToString() + ")");
            }
        }

        private static string GetSplittedName(string name)
        {
            string result = "";
            int lastIndex = 0;
            for (int i = 0; i < name.Length; i++)
            {
                if (Char.IsUpper(name[i]))
                {
                    result = result + " " + name.Substring(lastIndex, i - lastIndex);
                    lastIndex = i;
                }
            }

            result = result + " " + name.Substring(lastIndex);

            return result.Trim();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.TestCaseName = txtName.Text;
            this.Description = txtDescription.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void CreateTestCaseMessage_Load(object sender, EventArgs e)
        {
            txtName.SelectAll();
        }
    }
}
