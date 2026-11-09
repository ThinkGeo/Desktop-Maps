
namespace NauticalChartsViewer
{
    internal class ChartSelectedItemMessage
    {
        private ChartSelectedItem chartSelectedItem;

        public ChartSelectedItem ChartSelectedItem
        {
            get
            {
                return chartSelectedItem;
            }
        }

        public ChartSelectedItemMessage(ChartSelectedItem selectedItem)
        {
            chartSelectedItem = selectedItem;
        }
    }
}
