namespace WorldMapKitDataExtractor
{
    public class BoundingBoxRow
    {
        public System.Windows.Controls.TextBox txtUpperLeftPoint { get; set; }

        public System.Windows.Controls.TextBox txtLowerRightPoint { get; set; }

        public System.Windows.Controls.ComboBox cmbStartZoomLevel { get; set; }

        public System.Windows.Controls.ComboBox cmbEndZoomLevel { get; set; }

        public System.Windows.Controls.CheckBox chkRowEnabled { get; set; }

        public BoundingBoxRow() { }

        public BoundingBoxRow(System.Windows.Controls.TextBox txtUpperLeftPoint, System.Windows.Controls.TextBox txtLowerRightPoint, System.Windows.Controls.ComboBox cmbStartZoomLevel, System.Windows.Controls.ComboBox cmbEndZoomLevel, System.Windows.Controls.CheckBox chkRowEnabled)
        {
            this.txtUpperLeftPoint = txtUpperLeftPoint;
            this.txtLowerRightPoint = txtLowerRightPoint;
            this.cmbStartZoomLevel = cmbStartZoomLevel;
            this.cmbEndZoomLevel = cmbEndZoomLevel;
            this.chkRowEnabled = chkRowEnabled;
        }
    }
}
