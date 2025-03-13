using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public class LogViewModel : ObservableObject
    {
        private bool _showErrors;
        private bool _showWarnings;
        private bool _showInfo;

        public ObservableCollection<LogEntry> AllLogs { get; } = new ObservableCollection<LogEntry>();
        public ObservableCollection<LogEntry> FilteredLogs { get; } = new ObservableCollection<LogEntry>();

        public bool ShowErrors
        {
            get => _showErrors;
            set
            {
                SetProperty(ref _showErrors, value);
                UpdateFilteredLogs();
            }
        }

        public bool ShowWarnings
        {
            get => _showWarnings;
            set
            {
                SetProperty(ref _showWarnings, value);
                UpdateFilteredLogs();
            }
        }

        public bool ShowInfo
        {
            get => _showInfo;
            set
            {
                SetProperty(ref _showInfo, value);
                UpdateFilteredLogs();
            }
        }

        public LogViewModel()
        {
            // Sample data for testing
            AllLogs.Add(new LogEntry { Message = "Error loading map", Type = LogType.Error });
            AllLogs.Add(new LogEntry { Message = "Map loaded successfully", Type = LogType.Info });
            AllLogs.Add(new LogEntry { Message = "Low memory warning", Type = LogType.Warning });

            UpdateFilteredLogs();
        }

        private void UpdateFilteredLogs()
        {
            FilteredLogs.Clear();
            foreach (var log in AllLogs)
            {
                if ((ShowErrors && log.Type == LogType.Error) ||
                    (ShowWarnings && log.Type == LogType.Warning) ||
                    (ShowInfo && log.Type == LogType.Info))
                {
                    FilteredLogs.Add(log);
                }
            }
        }
    }

    public class LogEntry
    {
        public string Message { get; set; }
        public LogType Type { get; set; }
    }

    public enum LogType
    {
        Error,
        Warning,
        Info
    }
}
