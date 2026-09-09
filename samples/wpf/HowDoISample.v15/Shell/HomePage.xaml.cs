using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// The landing page: every sample in one scannable table, with what it does and
    /// the API to search for.
    /// <para>
    /// It replaces a generated document that lived on the classic side and was
    /// missing 23 of this app's samples - every GPU-only one among them - because a
    /// second catalog always drifts from the first. This one is a projection of
    /// <see cref="SampleCatalog"/>, so it cannot.
    /// </para>
    /// </summary>
    public partial class HomePage : UserControl
    {
        private readonly ListCollectionView _view;

        public HomePage()
        {
            InitializeComponent();

            _view = new ListCollectionView(SampleCatalog.All.ToList());
            Rows.ItemsSource = _view;

            var gpu = SampleCatalog.All.Count(e => e.IsGpu);
            var groups = SampleCatalog.All.Select(e => e.Category).Distinct(StringComparer.Ordinal).Count();
            Subtitle.Text = FormattableString.Invariant(
                $"{SampleCatalog.All.Count} samples in {groups} groups, {gpu} of them on the GPU renderer. Double-click a row to open it.");
        }

        /// <summary>Raised when a row is opened; the shell selects that sample.</summary>
        public event EventHandler<SampleEntry> SampleRequested;

        private void FilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = FilterBox.Text ?? string.Empty;
            FilterHint.Visibility = text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;

            var terms = SampleSearch.Terms(text);
            if (terms.Length == 0)
            {
                _view.Filter = null;
                FilterStatus.Text = string.Empty;
                return;
            }

            _view.Filter = item => item is SampleEntry entry && SampleSearch.Matches(entry, terms);
            FilterStatus.Text = _view.Count == 0
                ? FormattableString.Invariant($"nothing matches “{text}”")
                : FormattableString.Invariant($"{_view.Count} of {SampleCatalog.All.Count}");
        }

        private void Rows_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Rows.SelectedItem is SampleEntry entry)
            {
                SampleRequested?.Invoke(this, entry);
            }
        }

        private void Rows_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Selection alone does not navigate: a keyboard user scrolling the table
            // would otherwise tear down and rebuild a map on every arrow key.
        }
    }
}
