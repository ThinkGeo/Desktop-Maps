using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Geocoding
{
    public partial class FormAdvancedSampleTips : Form
    {
        public FormAdvancedSampleTips()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
               

        private void linkVideo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gis.thinkgeo.com/Products/GISComponentsforNETDevelopers/MapSuiteGeocoder/Videos/tabid/742/Default.aspx");
        }
    }
}
