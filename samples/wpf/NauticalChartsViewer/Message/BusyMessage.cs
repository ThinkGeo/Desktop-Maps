
namespace NauticalChartsViewer
{
    public class BusyMessage
    {
        public BusyMessage() { }

        public BusyMessage(bool isBusy)
        {
            IsBusy = isBusy;
        }

        public bool IsBusy { get; set; }
    }
}
