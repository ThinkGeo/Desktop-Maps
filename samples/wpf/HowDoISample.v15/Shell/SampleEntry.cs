using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// One row of the sample catalog.
    /// <para>
    /// <see cref="Focus"/> and <see cref="KeyApi"/> are what search matches on and
    /// what the home page shows. A row carrying neither is reachable only by
    /// someone who already knows its title - which is the state the whole gallery
    /// was in, and the reason searching "legend" returned nothing while six
    /// samples used <c>LegendAdornmentLayer</c>.
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
            bool isGpu,
            Type sampleType,
            Func<UserControl> factory)
        {
            Category = category;
            Title = title;
            SourceFile = sourceFile;
            Focus = focus;
            KeyApi = keyApi;
            IsGpu = isGpu;
            SampleType = sampleType;
            Factory = factory;
        }

        public string Category { get; }

        public string Title { get; }

        /// <summary>The embedded source shown in the code pane.</summary>
        public string SourceFile { get; }

        /// <summary>One line: what this sample demonstrates.</summary>
        public string Focus { get; }

        /// <summary>The API a developer would search for to land here.</summary>
        public string KeyApi { get; }

        /// <summary>Draws through the GPU renderer rather than the classic overlays.</summary>
        public bool IsGpu { get; }

        /// <summary>Home-page column marker for <see cref="IsGpu"/>.</summary>
        public string GpuMark => IsGpu ? "\u25CF" : string.Empty;

        /// <summary>
        /// The sample's type, kept alongside the factory so the drift check can
        /// compare the catalog against the assembly without constructing
        /// every sample to find out what it is.
        /// </summary>
        public Type SampleType { get; }

        public Func<UserControl> Factory { get; }
    }
}
