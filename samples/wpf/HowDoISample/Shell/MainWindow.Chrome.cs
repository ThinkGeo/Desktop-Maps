using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit.Highlighting;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// The chrome around a sample: the filter, the collapsible menu groups, the two
    /// panes that fold away, and the code viewer's C# / XAML tabs.
    /// <para>
    /// None of it knows what a sample is - it moves a selection, a width and a file
    /// name around. <c>MainWindow.xaml.cs</c> owns the catalog and the lifetime of
    /// the sample on screen; keeping the two apart is why the shell fits on a screen
    /// again.
    /// </para>
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>Every source file the build embedded, so the XAML tab can ask
        /// whether a sample has one without opening a stream per sample.</summary>
        private static readonly HashSet<string> EmbeddedSources = new HashSet<string>(
            typeof(MainWindow).Assembly.GetManifestResourceNames(), StringComparer.Ordinal);

        // What the panes go back to. Read off the column at the moment it is
        // collapsed, so a pane you resized reopens at the width you left it.
        private GridLength _sidebarWidth = new GridLength(260);
        private GridLength _codeWidth = new GridLength(470);

        // The groups that were open when a filter first took over the menu. A
        // search opens whatever it matched; clearing it has to give the menu back
        // the way it was, not the way the search left it.
        private List<SampleGroup> _expandedBeforeSearch;

        private string _csSourceFile;
        private string _xamlSourceFile;

        #region The menu

        /// <summary>
        /// Type-to-filter, not Enter-to-search. Every group that still has a hit
        /// opens itself: a filtered menu of shut groups looks like a menu that found
        /// nothing.
        /// </summary>
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = SearchBox.Text ?? string.Empty;
            SearchHint.Visibility = text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;

            var terms = SampleSearch.Terms(text);
            if (terms.Length == 0)
            {
                ApplyFilter(null);
                SearchStatus.Visibility = Visibility.Collapsed;
                RestoreExpansion();
                return;
            }

            RememberExpansion();
            ApplyFilter(item => item is SampleEntry entry && SampleSearch.Matches(entry, terms));

            foreach (var group in _groups.All)
            {
                group.IsExpanded = false;
            }

            foreach (SampleEntry entry in _sampleView)
            {
                _groups[entry.Category].IsExpanded = true;
            }

            // A search that finds nothing has to say so. An empty list with no
            // message reads as a broken app, which is what the classic shell did.
            var matches = _sampleView.Count;
            SearchStatus.Visibility = Visibility.Visible;
            SearchStatus.Text = matches == 0
                ? $"nothing matches “{text}”"
                : $"{matches} of {_samples.Count}";
        }

        /// <summary>Open the group a sample lives in and scroll to it.</summary>
        private void RevealInMenu(SampleEntry sample)
        {
            _groups[sample.Category].IsExpanded = true;

            // After the group has laid out, or there is nothing to scroll to yet.
            Dispatcher.BeginInvoke(
                new Action(() => SampleList.ScrollIntoView(sample)), DispatcherPriority.Background);
        }

        private void RememberExpansion()
        {
            _expandedBeforeSearch ??= _groups.All.Where(g => g.IsExpanded).ToList();
        }

        private void RestoreExpansion()
        {
            if (_expandedBeforeSearch == null)
            {
                return;
            }

            foreach (var group in _groups.All)
            {
                group.IsExpanded = _expandedBeforeSearch.Contains(group);
            }

            _expandedBeforeSearch = null;

            if (_openSample != null)
            {
                RevealInMenu(_openSample);
            }
        }

        /// <summary>
        /// Swap the menu's filter without letting it decide what is on screen.
        /// A selection means "open this sample", and WPF moves the selection when
        /// the selected row leaves the view - so filtering used to tear down the
        /// running sample and build whatever row the view landed on, once per
        /// keystroke.
        /// </summary>
        private void ApplyFilter(Predicate<object> filter)
        {
            _syncingSelection = true;
            try
            {
                _sampleView.Filter = filter;
            }
            finally
            {
                _syncingSelection = false;
            }

            RestoreSelection();
        }

        /// <summary>
        /// Put the highlight back on the sample that is actually running, once the
        /// filter lets its row exist again.
        /// </summary>
        private void RestoreSelection()
        {
            if (_openSample == null ||
                ReferenceEquals(SampleList.SelectedItem, _openSample) ||
                !_sampleView.Contains(_openSample))
            {
                return;
            }

            _syncingSelection = true;
            try
            {
                SampleList.SelectedItem = _openSample;
            }
            finally
            {
                _syncingSelection = false;
            }
        }

        #endregion

        #region The header

        private void ShowHeader(SampleEntry sample)
        {
            SampleTitle.Text = sample.Title;
            SampleFocus.Text = sample.Focus;
            KeyApiText.Text = sample.KeyApi;
            KeyApiChip.Visibility = string.IsNullOrEmpty(sample.KeyApi)
                ? Visibility.Collapsed
                : Visibility.Visible;
        }

        #endregion

        #region The panes

        private void ChromeToggle_Changed(object sender, RoutedEventArgs e)
        {
            // The toggles are checked in XAML, so this runs once while the window is
            // still being built and the columns do not exist yet.
            if (!IsInitialized)
            {
                return;
            }

            TogglePane(SidebarColumn, Sidebar, SidebarSplitter, MenuToggle.IsChecked == true, ref _sidebarWidth);
            TogglePane(CodeColumn, CodePane, CodeSplitter, CodeToggle.IsChecked == true, ref _codeWidth);
        }

        /// <summary>
        /// Collapse a pane to nothing, splitter included, and remember what it was
        /// so it comes back the same size. Hiding the content alone would leave its
        /// column - and its gap - on screen.
        /// </summary>
        private static void TogglePane(
            ColumnDefinition column, UIElement pane, UIElement splitter, bool show, ref GridLength remembered)
        {
            if (show)
            {
                column.Width = remembered;
                pane.Visibility = Visibility.Visible;
                splitter.Visibility = Visibility.Visible;
                return;
            }

            if (column.ActualWidth > 0)
            {
                remembered = new GridLength(column.ActualWidth);
            }

            pane.Visibility = Visibility.Collapsed;
            splitter.Visibility = Visibility.Collapsed;
            column.Width = new GridLength(0);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            // Ctrl+F is where a developer's hands already are, and it has to bring
            // the menu back with it - the box is not on screen when it is hidden.
            if (e.Key == Key.F && Keyboard.Modifiers == ModifierKeys.Control)
            {
                MenuToggle.IsChecked = true;
                SearchBox.Focus();
                SearchBox.SelectAll();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape && SearchBox.IsKeyboardFocusWithin && SearchBox.Text.Length > 0)
            {
                SearchBox.Clear();
                e.Handled = true;
            }

            base.OnPreviewKeyDown(e);
        }

        #endregion

        #region The code pane

        /// <summary>
        /// Show a sample's source. The XAML tab appears only when that sample has a
        /// XAML file - the GPU-only samples are single .cs files, and a tab that
        /// opens onto "source not embedded" is worse than no tab.
        /// </summary>
        private void ShowSource(string sourceFile)
        {
            _csSourceFile = sourceFile;
            _xamlSourceFile = XamlCounterpartOf(sourceFile);

            XamlTab.Visibility = _xamlSourceFile == null ? Visibility.Collapsed : Visibility.Visible;
            if (_xamlSourceFile == null)
            {
                CsTab.IsChecked = true;
            }

            RenderCode();
        }

        private void CodeTab_Checked(object sender, RoutedEventArgs e)
        {
            if (!IsInitialized)
            {
                return;
            }

            RenderCode();
        }

        private void RenderCode()
        {
            var showXaml = XamlTab.IsChecked == true && _xamlSourceFile != null;
            var file = showXaml ? _xamlSourceFile : _csSourceFile;

            CodeTitle.Text = file;
            CodeView.SyntaxHighlighting = Darkened(HighlightingManager.Instance.GetDefinition(showXaml ? "XML" : "C#"));
            CodeView.Text = LoadSampleSource(file);
            CodeView.ScrollToHome();
        }

        private static readonly HashSet<IHighlightingDefinition> DarkenedDefinitions = new();

        /// <summary>
        /// Recolors a built-in highlighting definition for the dark code pane (VS Code
        /// Dark+ palette). The built-ins ship light-theme colors - dark blue keywords
        /// vanish on a dark background. Definitions are process-wide singletons, so each
        /// is recolored once and reused.
        /// </summary>
        private static IHighlightingDefinition Darkened(IHighlightingDefinition definition)
        {
            if (definition == null || !DarkenedDefinitions.Add(definition))
            {
                return definition;
            }

            foreach (var color in definition.NamedHighlightingColors)
            {
                var hex = color.Name switch
                {
                    // C#
                    "Comment" or "DocComment" => "#6A9955",
                    "String" or "Char" => "#CE9178",
                    "NumberLiteral" => "#B5CEA8",
                    "MethodCall" => "#DCDCAA",
                    "Preprocessor" => "#9B9B9B",
                    "Punctuation" => "#D4D4D4",
                    "GotoKeywords" or "ExceptionKeywords" or "ContextKeywords" => "#C586C0",
                    "Keywords" or "Modifiers" or "Visibility" or "NamespaceKeywords" or
                    "ReferenceTypeKeywords" or "ValueTypeKeywords" or "TypeKeywords" or
                    "OperatorKeywords" or "ParameterModifiers" or "CheckedKeyword" or
                    "UnsafeKeywords" or "UnchekedKeyword" or "GetSetAddRemove" or
                    "TrueFalse" or "NullOrValueKeywords" or "ThisOrBaseReference" or
                    "SemanticKeywords" => "#569CD6",
                    // XML
                    "XmlTag" or "XmlDeclaration" or "DocType" => "#569CD6",
                    "AttributeName" => "#9CDCFE",
                    "AttributeValue" => "#CE9178",
                    "Entity" or "BrokenEntity" => "#569CD6",
                    "CData" => "#CE9178",
                    _ => null,
                };

                if (hex != null)
                {
                    color.Foreground = new SimpleHighlightingBrush(
                        (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(hex));
                }
            }

            return definition;
        }

        /// <summary>The .xaml beside a sample's .xaml.cs, or null when the build did
        /// not carry one.</summary>
        private static string XamlCounterpartOf(string sourceFile)
        {
            if (string.IsNullOrEmpty(sourceFile) ||
                !sourceFile.EndsWith(".xaml.cs", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var xaml = sourceFile.Substring(0, sourceFile.Length - ".cs".Length);
            return EmbeddedSources.Contains("SampleSource." + xaml) ? xaml : null;
        }

        /// <summary>
        /// The sample's complete source, from the embedded copy the build carried -
        /// what the code pane shows IS what runs, with no path dependence on where
        /// the app was launched from.
        /// </summary>
        private static string LoadSampleSource(string fileName)
        {
            try
            {
                using var stream = typeof(MainWindow).Assembly.GetManifestResourceStream("SampleSource." + fileName);
                if (stream == null)
                {
                    return "// source not embedded: " + fileName;
                }

                using var reader = new System.IO.StreamReader(stream);
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return "// failed to load source: " + ex.Message;
            }
        }

        #endregion
    }
}
