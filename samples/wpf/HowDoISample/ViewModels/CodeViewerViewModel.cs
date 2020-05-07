using GalaSoft.MvvmLight;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public class CodeViewerViewModel : ViewModelBase
    {
        private bool _isCsharpCodeVisible;
        private bool _isXamlCodeVisible;
        private string _syntaxHighlighting;


        public bool IsCsharpCodeVisible
        {
            get { return _isCsharpCodeVisible; }
            set
            {
                if (_isCsharpCodeVisible != value)
                {
                    _isCsharpCodeVisible = value;
                    if (_isCsharpCodeVisible)
                    {
                        _isXamlCodeVisible = false;
                        SyntaxHighlighting = "C#";
                        RaisePropertyChanged(() => IsXamlCodeVisible);
                    }
                    RaisePropertyChanged(() => IsCsharpCodeVisible);
                }
            }
        }

        public bool IsXamlCodeVisible
        {
            get { return _isXamlCodeVisible; }
            set
            {
                if (_isXamlCodeVisible != value)
                {
                    _isXamlCodeVisible = value;
                    if (_isXamlCodeVisible)
                    {
                        _isCsharpCodeVisible = false;
                        SyntaxHighlighting = "XML";
                        RaisePropertyChanged(() => IsCsharpCodeVisible);
                    }
                    RaisePropertyChanged(() => IsXamlCodeVisible);
                }
            }
        }

        public string CsharpCode { get; set; }

        public string XamlCode { get; set; }

        public string SyntaxHighlighting
        {
            get { return _syntaxHighlighting; }
            set
            {
                if (_syntaxHighlighting != value)
                {
                    _syntaxHighlighting = value;
                    RaisePropertyChanged(() => SyntaxHighlighting);
                }
            }
        }
    }
}
