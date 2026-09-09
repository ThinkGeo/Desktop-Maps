using System;
using System.Windows;
using System.Windows.Media;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Disposes whatever the shell swaps off the screen. A sample that implements
    /// <see cref="IDisposable"/> owns its whole teardown (timers, subscriptions,
    /// files); every other sample just holds maps, and the tree walk finds and
    /// disposes them - which is why samples carry no Dispose boilerplate.
    /// </summary>
    internal static class SampleTeardown
    {
        public static void Dispose(object content)
        {
            if (content is IDisposable disposable)
            {
                disposable.Dispose();
                return;
            }

            if (content is DependencyObject root)
            {
                DisposeMapViews(root);
            }
        }

        private static void DisposeMapViews(DependencyObject node)
        {
            if (node is MapView map)
            {
                map.Dispose();
                return;
            }

            // The logical walk below reaches inline text - a Run inside a TextBlock, a
            // Hyperlink - and those are content, not visuals: asking the visual tree
            // about their children throws. It threw here, which aborted the whole
            // teardown, so a sample whose panel contained a formatted paragraph left
            // every map after it in the tree undisposed.
            if (!(node is Visual) && !(node is System.Windows.Media.Media3D.Visual3D))
            {
                DisposeLogicalChildren(node);
                return;
            }

            var count = VisualTreeHelper.GetChildrenCount(node);
            if (count == 0)
            {
                // Not rendered yet (or a purely logical composite): the visual
                // tree is empty while the logical one already holds the map.
                DisposeLogicalChildren(node);
                return;
            }

            for (var i = 0; i < count; i++)
            {
                DisposeMapViews(VisualTreeHelper.GetChild(node, i));
            }
        }

        private static void DisposeLogicalChildren(DependencyObject node)
        {
            foreach (var child in LogicalTreeHelper.GetChildren(node))
            {
                if (child is DependencyObject logicalChild)
                {
                    DisposeMapViews(logicalChild);
                }
            }
        }
    }
}
