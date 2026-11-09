using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;
using ThinkGeo.MapSuite;

namespace NauticalChartsViewer
{

    public class SymbolsEditionViewModel : ViewModelBase
    {
        private string s52SymbolsSourcePath;
        private ColorTableViewModel colorTableViewModel;
        private SymbolTableViewModel symbolTableViewModel;
        private LookupTableViewModel lookupTableViewModel;
        private S52ResourceFilesAnalyst s52ResourceFilesAnalyst;
        private ICommand cancelCommand; 
        private ICommand s52ResourceBrowerCommand;

        public SymbolsEditionViewModel()
        {
            colorTableViewModel = new ColorTableViewModel();
            symbolTableViewModel = new SymbolTableViewModel();
            lookupTableViewModel = new LookupTableViewModel();

            // Open the built-in default S-52 library so the editor isn't empty. Edits are
            // saved back to this same file and applied to the map when the charts reload.
            // Users can still Browse to a different S-52 style file if they want.
            Globals.EnsureStyleFile();
            S52SymbolsSourcePath = Globals.StyleFilePath;
        }

        public string S52SymbolsSourcePath
        {
            get { return s52SymbolsSourcePath; }
            set
            {
                if (s52SymbolsSourcePath != value)
                {
                    try
                    {
                        s52ResourceFilesAnalyst = new S52ResourceFilesAnalyst(value);
                        s52SymbolsSourcePath = value;
                        ColorTable = new ColorTableViewModel(s52ResourceFilesAnalyst);
                        ColorTable.PropertyChanged += (sender, e) =>
                        {
                            if (e.PropertyName == "SelectedColorSchema")
                            {
                                symbolTableViewModel.SelectColorSchem = colorTableViewModel.SelectedColorSchema;
                            }
                        };
                        SymbolTable = new SymbolTableViewModel(s52ResourceFilesAnalyst, ColorTable.SelectedColorSchema);
                        LookupTable = new LookupTableViewModel(s52ResourceFilesAnalyst);
                        OnPropertyChanged("S52SymbolsSourcePath");
                    }
                    catch (NullReferenceException)
                    {
                        MessageBox.Show("S52 file format error.", string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        public ColorTableViewModel ColorTable
        {
            get { return colorTableViewModel; }
            set
            {
                if (colorTableViewModel != value)
                {
                    colorTableViewModel = value;
                    OnPropertyChanged("ColorTable");
                }
            }
        }

        public SymbolTableViewModel SymbolTable
        {
            get { return symbolTableViewModel; }
            set
            {
                if (symbolTableViewModel != value)
                {
                    symbolTableViewModel = value;
                    OnPropertyChanged("SymbolTable");
                }
            }
        }


        public LookupTableViewModel LookupTable
        {
            get { return lookupTableViewModel; }
            set
            {
                if (lookupTableViewModel != value)
                {
                    lookupTableViewModel = value;
                    OnPropertyChanged("LookupTable");
                }
            }
        }

        public ICommand S52ResourceBrowerCommand
        {
            get
            {
                return s52ResourceBrowerCommand ?? (s52ResourceBrowerCommand = new RelayCommand(HandleS52ResourceBrowerCommand));
            }
        }

        private void HandleS52ResourceBrowerCommand()
        {
            OpenFileDialog dialog = new OpenFileDialog() 
            {
                Filter = "(*.xml)|*.xml"
            };
            if (dialog.ShowDialog() ?? false)
            {
                S52SymbolsSourcePath = dialog.FileName;
            }
        }

        public ICommand CancelCommand
        {
            get { return cancelCommand ?? (cancelCommand = new RelayCommand(HandleCancelCommand)); }
        }

        private void HandleCancelCommand()
        {
            Messenger.Default.Send(new WindowStateMessage(S57WindowState.Close));
        }
    }
}
