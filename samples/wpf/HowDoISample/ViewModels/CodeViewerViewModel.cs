using CommunityToolkit.Mvvm.ComponentModel;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public class CodeViewerViewModel : ObservableObject
    {
        private bool _isCsharpCodeVisible;
        private bool _isXamlCodeVisible;
        private string _syntaxHighlighting;


        public bool IsCsharpCodeVisible
        {
            get => _isCsharpCodeVisible;
            set
            {
                if (_isCsharpCodeVisible == value) return;
                _isCsharpCodeVisible = value;
                if (_isCsharpCodeVisible)
                {
                    _isXamlCodeVisible = false;
                    SyntaxHighlighting = "C#";
                    OnPropertyChanged(nameof(IsXamlCodeVisible));
                }
                OnPropertyChanged();
            }
        }

        public bool IsXamlCodeVisible
        {
            get => _isXamlCodeVisible;
            set
            {
                if (_isXamlCodeVisible == value) return;
                _isXamlCodeVisible = value;
                if (_isXamlCodeVisible)
                {
                    _isCsharpCodeVisible = false;
                    SyntaxHighlighting = "XML";
                    OnPropertyChanged(nameof(IsCsharpCodeVisible));
                }
                OnPropertyChanged();
            }
        }

        public string CsharpCode { get; set; }

        public string XamlCode { get; set; }

        public string SyntaxHighlighting
        {
            get => _syntaxHighlighting;
            set
            {
                if (_syntaxHighlighting == value) return;
                _syntaxHighlighting = value;
                OnPropertyChanged();
            }
        }
    }
}
