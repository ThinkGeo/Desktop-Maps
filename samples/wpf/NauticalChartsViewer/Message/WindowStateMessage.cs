
namespace NauticalChartsViewer
{
    public class WindowStateMessage
    {
        public WindowStateMessage() { }

        public WindowStateMessage(S57WindowState state)
        {
            WindowState = state;
        }

        public S57WindowState WindowState { get; set; }


    }
}
