using System.Windows.Controls;
using System.Windows.Media;

namespace LabelingFlightLines
{
    /// <summary>
    /// Interaction logic for FlightsInfoControl.xaml
    /// </summary>
    public partial class FlightsContent : UserControl
    {
        public FlightsContent(Flight flight, bool isAirline)
        {
            InitializeComponent();

            header.Text = flight.Key;

            if (isAirline)
            {
                header.Background = new SolidColorBrush(Colors.LightGreen);
                flightDetail.Text = "Date: " + flight.Date + "\r\n" + "Flight Time: " + flight.FlightTime + " hours";
            }
            else
            {
                header.Background = new SolidColorBrush(Colors.LightSkyBlue);
                flightDetail.Text = "Origin Address: " + flight.OriginAddress + "\r\n" + "Destination Address: " + flight.DestinationAddress;
            }
        }
    }
}
