using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// One collapsible menu group.
    /// <para>
    /// Expansion is state of the group, not of the Expander showing it: the
    /// grouped view throws its containers away and rebuilds them on every
    /// keystroke in the search box, so a flag left on the control would reset
    /// itself as you type. Keeping it here also lets the shell open a group
    /// because something was selected inside it, without going looking for a
    /// control in the visual tree.
    /// </para>
    /// </summary>
    public sealed class SampleGroup : INotifyPropertyChanged
    {
        private bool _isExpanded;

        public SampleGroup(string title, string chapter = null)
        {
            Title = title;
            Chapter = chapter;
        }

        public string Title { get; }

        /// <summary>The chapter label the sidebar prints above this group's
        /// header - set on the first group of each chapter, null on the rest.
        /// See <see cref="SampleCatalog.ChapterStarts"/>.</summary>
        public string Chapter { get; }

        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (_isExpanded == value)
                {
                    return;
                }

                _isExpanded = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsExpanded)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString() => Title;
    }

    /// <summary>
    /// The menu's groups, and the converter that hands them to the grouped view.
    /// <para>
    /// The catalog groups by a category <em>string</em>; a string cannot carry
    /// expansion state, so the view is told to group by whatever this converter
    /// returns for it - the one <see cref="SampleGroup"/> per category. Nothing in
    /// <see cref="SampleEntry"/> changes for it.
    /// </para>
    /// </summary>
    public sealed class SampleGroups : IValueConverter
    {
        private readonly Dictionary<string, SampleGroup> _byCategory =
            new Dictionary<string, SampleGroup>(StringComparer.Ordinal);

        private readonly List<SampleGroup> _inMenuOrder = new List<SampleGroup>();

        private readonly IReadOnlyDictionary<string, string> _chapterStarts;

        public SampleGroups(IEnumerable<string> categories, IReadOnlyDictionary<string, string> chapterStarts)
        {
            _chapterStarts = chapterStarts;
            foreach (var category in categories)
            {
                _ = this[category];
            }
        }

        /// <summary>The groups, in menu order.</summary>
        public IReadOnlyList<SampleGroup> All => _inMenuOrder;

        public SampleGroup this[string category]
        {
            get
            {
                var key = category ?? string.Empty;
                if (!_byCategory.TryGetValue(key, out var group))
                {
                    string chapter = null;
                    _chapterStarts?.TryGetValue(key, out chapter);
                    group = new SampleGroup(key, chapter);
                    _byCategory.Add(key, group);
                    _inMenuOrder.Add(group);
                }

                return group;
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            this[value as string];

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotSupportedException();
    }
}
