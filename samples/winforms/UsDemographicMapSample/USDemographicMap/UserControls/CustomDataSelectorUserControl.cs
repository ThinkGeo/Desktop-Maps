using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Layers;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    // This is the bottom "Custom Data" item on the left.
    // It contains the initial SelectCustomDataUserControl, and would contain StyleSelectorUserControls like other DataSelectorUserControls.
    public class CustomDataSelectorUserControl : DataSelectorUserControl
    {
        public event EventHandler<DataSelectedCustomDataSelectorUserControlEventArgs> DataSelected;

        private SelectCustomDataUserControl customDataSelector;

        public CustomDataSelectorUserControl()
            : base()
        {
            customDataSelector = new SelectCustomDataUserControl();
            customDataSelector.FileSelected = CustomDataSelector_FileSelected;

            Panel.Controls.Add(customDataSelector);
            CompleteHeight = customDataSelector.Height;
        }

        protected void OnCustomDataSelected(DataSelectedCustomDataSelectorUserControlEventArgs e)
        {
            EventHandler<DataSelectedCustomDataSelectorUserControlEventArgs> handler = DataSelected;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void StyleSelectorUserControl_StyleUpdated(object sender, StyleUpdatedStyleSelectorUserControlEventArgs e)
        {
            if (e.DemographicStyleBuilderType != DemographicStyleBuilderType.PieChart)
            {
                StyleUpdatedDataSelectorUserControlEventArgs eventArgs = new StyleUpdatedDataSelectorUserControlEventArgs(e.DemographicStyleBuilderType);
                eventArgs.ActivatedStyleSelectors.Add((StyleSelectorUserControl)sender);
                OnStyleUpdated(eventArgs);
            }
            else
            {
                RaisePieChartUpdatedEvent();
            }
        }

        private void CustomDataSelector_FileSelected(string pathFileName)
        {
            ShapeFileFeatureLayer featureLayer = new ShapeFileFeatureLayer(pathFileName);
            featureLayer.Open();
            if (featureLayer.GetShapeFileType() == ShapeFileType.Polygon)
            {
                AddStyleSelectors(featureLayer);
                OnCustomDataSelected(new DataSelectedCustomDataSelectorUserControlEventArgs(featureLayer));
            }
            else
            {
                MessageBox.Show("The shape file must be polygon type, please try another one.", "Warning");
            }
        }

        private void AddStyleSelectors(ShapeFileFeatureLayer featureLayer)
        {
            for (int i = Panel.Controls.Count - 1; i >= 0; i--)
            {
                StyleSelectorUserControl styleSelectorUserControl = Panel.Controls[i] as StyleSelectorUserControl;
                if (styleSelectorUserControl != null)
                {
                    Panel.Controls.Remove(styleSelectorUserControl);
                }
            }

            int panelHeight = customDataSelector.Height + customDataSelector.Top;
            List<DbfColumn> numericDbfColumns = GetNumericDbfColumns(featureLayer.QueryTools.GetColumns().OfType<DbfColumn>());
            StyleSelectors.Clear();
            foreach (DbfColumn column in numericDbfColumns)
            {
                StyleSelectorUserControl styleSelectorUserControl = new StyleSelectorUserControl();
                styleSelectorUserControl.StyleUpdated += StyleSelectorUserControl_StyleUpdated;
                panelHeight += styleSelectorUserControl.Height + styleSelectorUserControl.Top;
                styleSelectorUserControl.Alias = column.ColumnName;
                styleSelectorUserControl.ColumnName = column.ColumnName;
                styleSelectorUserControl.LegendTitle = column.ColumnName;
                styleSelectorUserControl.WithinPieChart = true;
                StyleSelectors.Add(styleSelectorUserControl);
                CompleteHeight += styleSelectorUserControl.Height;
                Panel.Controls.Add(styleSelectorUserControl);
            }

            int height = 0;
            foreach (Control control in Panel.Controls.OfType<Control>())
            {
                control.Location = new Point(control.Margin.Left, control.Margin.Top + height);
                height = control.Location.Y + control.Height;
            }

            if (Panel.Controls.Count >= 2)
            {
                PieChartEnabled = true;
            }

            Panel.Height = panelHeight;
            Height = HeaderHeight + panelHeight;
        }

        private static List<DbfColumn> GetNumericDbfColumns(IEnumerable<DbfColumn> dbfColumns)
        {
            return dbfColumns.Where(IsNumericColumn).ToList();
        }

        private static bool IsNumericColumn(DbfColumn column)
        {
            return column.ColumnType == DbfColumnType.Float ||
                   column.ColumnType == DbfColumnType.Numeric;
        }
    }
}