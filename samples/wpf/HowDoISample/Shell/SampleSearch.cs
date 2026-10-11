using System;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// What "matching" means, in one place. The menu and the home page filter the
    /// same catalog and were doing it with two copies of the same three methods -
    /// which is how a search rule gets improved in one of them and not the other.
    /// </summary>
    internal static class SampleSearch
    {
        private static readonly char[] Separators = { ' ' };

        public static string[] Terms(string text) =>
            (text ?? string.Empty).Split(Separators, StringSplitOptions.RemoveEmptyEntries);

        /// <summary>
        /// Every term has to match something, so two words narrow instead of widen.
        /// Title, focus, key API and group all count: a developer looks for
        /// <c>LegendAdornmentLayer</c>, not for a category.
        /// </summary>
        public static bool Matches(SampleEntry entry, string[] terms)
        {
            foreach (var term in terms)
            {
                var hit = Contains(entry.Title, term)
                          || Contains(entry.Focus, term)
                          || Contains(entry.KeyApi, term)
                          || Contains(entry.Category, term);
                if (!hit)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool Contains(string haystack, string needle) =>
            !string.IsNullOrEmpty(haystack) &&
            haystack.IndexOf(needle, StringComparison.OrdinalIgnoreCase) >= 0;
    }
}
