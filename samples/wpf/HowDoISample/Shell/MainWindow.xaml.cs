using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// The shell: the catalog on the left, its source in the middle, the sample
    /// itself on the right. Everything about the chrome around a sample - filtering,
    /// collapsing a pane, the code tabs - lives in <c>MainWindow.Chrome.cs</c>; what
    /// is here is the catalog and the lifetime of the sample on screen.
    /// </summary>
    public partial class MainWindow : Window
    {
        // The catalog is SampleCatalog, not a list here: the menu, the search
        // index and the home page are all projections of one list, so a sample
        // cannot ship in the app and be missing from the index. Copied to a List
        // because the grouped ListCollectionView and FindIndex both want IList.
        // internal, not private: ThinkGeo.UI.Wpf.HowDoI.Checks (the scripted
        // verification app, a ChecksWindow subclass of this window) reads the
        // catalog list, the group model and the title lookup to drive samples.
        internal readonly List<SampleEntry> _samples = SampleCatalog.All.ToList();

        // The menu groups. They carry their own expansion state - see SampleGroup.
        internal readonly SampleGroups _groups =
            new SampleGroups(SampleCatalog.Categories, SampleCatalog.ChapterStarts);

        private readonly ListCollectionView _sampleView;

        /// <summary>What <c>SampleHost</c> is showing. The menu selection is a view
        /// of this, not the other way round - filtering can take the selection away
        /// without taking the sample off the screen.</summary>
        private SampleEntry _openSample;

        /// <summary>Set while the menu selection is being put back in line with
        /// <see cref="_openSample"/>, so that does not read as "open this sample".</summary>
        private bool _syncingSelection;

        public MainWindow()
        {
            InitializeComponent();

            _sampleView = new ListCollectionView(_samples);

            // Grouped by the category string, but keyed on the group model the
            // converter returns for it: the header binds to something that can be
            // opened and closed, and SampleEntry stays a plain catalog row.
            _sampleView.GroupDescriptions.Add(
                new PropertyGroupDescription(nameof(SampleEntry.Category), _groups));
            SampleList.ItemsSource = _sampleView;

            SampleList.SelectedIndex = 0;

#if DEBUG
            // A sample compiled into the binary but absent from the catalog is
            // unreachable, and nothing used to say so - which is how the FAQ viewer
            // stayed invisible. Debug-only: this is a development-time mistake.
            foreach (var problem in SampleCatalog.FindDrift())
            {
                System.Diagnostics.Trace.WriteLine("[SampleCatalog] drift: " + problem);
            }
#endif
        }

        internal void NavigateToSample(string sampleId)
        {
            if (string.IsNullOrWhiteSpace(sampleId))
            {
                return;
            }

            var name = sampleId.Substring(sampleId.LastIndexOf('.') + 1);
            var index = _samples.FindIndex(s => string.Equals(
                s.SourceFile.Substring(0, s.SourceFile.IndexOf('.')), name, StringComparison.OrdinalIgnoreCase));
            if (index >= 0)
            {
                SampleList.SelectedIndex = index;
            }
        }

        /// <summary>
        /// Menu position of a sample by title. The scripted checks used to open
        /// index 0 and assume Navigation Map; that held only while Map Navigation
        /// was the first group, so reordering the menu silently retargeted them.
        /// </summary>
        internal int IndexOfSample(string title)
        {
            var index = _samples.FindIndex(s => string.Equals(s.Title, title, StringComparison.Ordinal));
            if (index < 0)
            {
                System.Diagnostics.Trace.WriteLine("[SampleCatalog] no sample titled \"" + title + "\"");
            }

            return index;
        }

        private void SampleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_syncingSelection) return;

            // SelectedItem, not _samples[SelectedIndex]: once the filter is on, the
            // index is a position in the filtered view and would open a different
            // sample than the one clicked. Null means the filter has hidden the open
            // sample's row - the sample itself stays exactly where it is. Typing in
            // the search box used to build a map per keystroke, because WPF moves the
            // selection when the selected row leaves the view.
            if (SampleList.SelectedItem is not SampleEntry sample) return;

            _openSample = sample;

            // A sample opened from anywhere but the menu - the home page, a
            // documentation link, a scripted check - must not land inside a closed
            // group, where the menu would show no selection at all.
            RevealInMenu(sample);
            ShowHeader(sample);

            // Emptied BEFORE the old one is disposed and the new one is built: a
            // factory that throws - a sample whose data file is missing - would
            // otherwise leave a DISPOSED control sitting in Content, and the next
            // selection would try to dispose it again.
            var previous = SampleHost.Content;
            SampleHost.Content = null;
            SampleTeardown.Dispose(previous);

            var content = sample.Factory();
            SampleHost.Content = content;

            if (content is HomePage home)
            {
                home.SampleRequested += (_, entry) => SampleList.SelectedItem = entry;
            }

            ShowSource(sample.SourceFile);
        }

        /// <summary>Titles carry punctuation a filename cannot (a colon becomes an NTFS stream).</summary>
        internal static string SafeFileName(string title)
        {
            var chars = title.ToCharArray();
            for (var c = 0; c < chars.Length; c++)
            {
                if (chars[c] == ' ' || Array.IndexOf(System.IO.Path.GetInvalidFileNameChars(), chars[c]) >= 0)
                {
                    chars[c] = '_';
                }
            }

            return new string(chars);
        }
    }
}
