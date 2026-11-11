using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// What a sample's map is drawn with, read off its code rather than its
    /// subject, so the drift check can hold every row to it.
    /// </summary>
    public enum SampleDraws
    {
        /// <summary>A page about the gallery, not a map.</summary>
        Page,

        /// <summary>Everything is in the map's style document.</summary>
        Style,

        /// <summary>A style document, with classic overlays above it - markers,
        /// popups, drawing and editing, adornments, map tools, layer overlays.</summary>
        StyleAndOverlay,

        /// <summary>No style document: classic layers and overlays only.</summary>
        Classic,

        /// <summary>Two maps in one window, one drawn each way.</summary>
        SideBySide,
    }

    /// <summary>
    /// One row of the sample catalog.
    /// <para>
    /// <see cref="Focus"/>, <see cref="KeyApi"/> and <see cref="Aliases"/> are what
    /// search matches on. A row carrying none of them is reachable only by someone
    /// who already knows its title - which is the state the whole gallery was in,
    /// and the reason searching "legend" returned nothing while six samples used
    /// <c>LegendAdornmentLayer</c>.
    /// </para>
    /// </summary>
    public sealed class SampleEntry
    {
        public SampleEntry(
            string category,
            string title,
            string sourceFile,
            string focus,
            string keyApi,
            SampleDraws draws,
            Type sampleType,
            Func<UserControl> factory,
            string aliases = null,
            string service = null)
        {
            Category = category;
            Title = title;
            SourceFile = sourceFile;
            Focus = focus;
            KeyApi = keyApi;
            Draws = draws;
            SampleType = sampleType;
            Factory = factory;
            Aliases = aliases ?? string.Empty;
            Service = service ?? string.Empty;
        }

        public string Category { get; }

        public string Title { get; }

        /// <summary>The embedded source shown in the code pane.</summary>
        public string SourceFile { get; }

        /// <summary>One line: what this sample demonstrates.</summary>
        public string Focus { get; }

        /// <summary>The API a developer would search for to land here.</summary>
        public string KeyApi { get; }

        /// <summary>
        /// Other words that should find this row, comma separated: the titles and
        /// class names of the samples merged into it, and the API names it uses
        /// that are not in <see cref="KeyApi"/>. Searched, never shown.
        /// </summary>
        public string Aliases { get; }

        /// <summary>The third-party service the sample needs, if any - when it is
        /// down the sample shows less, and nothing in the SDK is wrong.</summary>
        public string Service { get; }

        public SampleDraws Draws { get; }

        /// <summary>Home-page column text for <see cref="Draws"/>.</summary>
        public string DrawsLabel => Draws switch
        {
            SampleDraws.Style => "Style",
            SampleDraws.StyleAndOverlay => "Style + overlay",
            SampleDraws.Classic => "Classic",
            SampleDraws.SideBySide => "Side by side",
            _ => string.Empty,
        };

        /// <summary>
        /// The sample's type, kept alongside the factory so the drift check can
        /// compare the catalog against the assembly without constructing
        /// every sample to find out what it is.
        /// </summary>
        public Type SampleType { get; }

        public Func<UserControl> Factory { get; }
    }
}
