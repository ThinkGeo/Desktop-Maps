using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class MarkdownDocumentViewer : UserControl
    {
        private const string FaqDocumentType = "thinkgeo-howdoi-faq";
        private readonly string[] _candidatePaths;
        private bool _webMessageAttached;

        public MarkdownDocumentViewer(string fileName)
        {
            InitializeComponent();
            _candidatePaths = BuildCandidatePaths(Path.Combine("Samples", "Documentation"), fileName);
            Loaded += OnLoaded;
        }

        private static string[] BuildCandidatePaths(string relativeFolder, string fileName)
        {
            var baseDirectory = AppContext.BaseDirectory;
            return new[]
            {
                Path.Combine(baseDirectory, relativeFolder, fileName),
                Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\..", relativeFolder, fileName)),
                Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..", relativeFolder, fileName)),
                Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, relativeFolder, fileName))
            };
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;
            await LoadMarkdownAsync();
        }

        private async Task LoadMarkdownAsync()
        {
            var path = _candidatePaths.FirstOrDefault(File.Exists);
            if (string.IsNullOrEmpty(path))
            {
                ShowPlainText("# Missing file\n\nUnable to find the markdown file in `Samples/Documentation/`.\n" +
                              "Please build or run from the project root so the file is copied to output.");
                return;
            }

            var markdownText = File.ReadAllText(path);
            if (!await TryRenderInWebViewAsync(markdownText))
                ShowPlainText(markdownText);
        }

        private async Task<bool> TryRenderInWebViewAsync(string markdownText)
        {
            try
            {
                await MarkdownWebView.EnsureCoreWebView2Async();

                if (!_webMessageAttached)
                {
                    MarkdownWebView.CoreWebView2.WebMessageReceived += OnWebMessageReceived;
                    _webMessageAttached = true;
                }

                MarkdownWebView.NavigateToString(BuildHtmlDocument(markdownText));
                MarkdownWebView.Visibility = Visibility.Visible;
                MarkdownTextBox.Visibility = Visibility.Collapsed;
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected virtual void OnWebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            // Subclasses can override to handle link clicks.
        }

        private void ShowPlainText(string text)
        {
            MarkdownTextBox.Text = text;
            MarkdownTextBox.Visibility = Visibility.Visible;
            MarkdownWebView.Visibility = Visibility.Collapsed;
            MarkdownTextBox.ScrollToHome();
        }

        private static string BuildHtmlDocument(string markdownText)
        {
            var document = ParseMarkdownDocument(markdownText);
            var headings = CollectHeadings(document.Body);
            var bodyHtml = ConvertMarkdownToHtml(document.Body, headings);

            if (ShouldRenderTableOfContents(document.DocumentType, headings))
                bodyHtml = InsertTableOfContents(bodyHtml, BuildTableOfContents(headings));

            return "<!DOCTYPE html><html><head><meta charset=\"utf-8\"/>" +
                   "<style>" +
                   "body{font-family:'Segoe UI',sans-serif;margin:16px;color:#dce3ec;background:#0d1117;line-height:1.6;}" +
                   "h1,h2,h3{margin:20px 0 8px 0;color:#f4f7fb;}" +
                   "h2{border-bottom:1px solid #2a3442;padding-bottom:4px;}" +
                   "p{margin:8px 0;}" +
                   "ul,ol{margin:8px 0 8px 24px;padding:0;}" +
                   "li{margin:4px 0;}" +
                   "code{font-family:Consolas,monospace;background:#1f2937;color:#dce3ec;padding:1px 4px;border-radius:4px;font-size:0.9em;}" +
                   "pre{background:#1f2937;color:#dce3ec;padding:12px 16px;border-radius:6px;overflow-x:auto;font-family:Consolas,monospace;font-size:0.88em;line-height:1.5;margin:10px 0;border:1px solid #2a3442;}" +
                   "pre code{background:none;padding:0;border-radius:0;font-size:inherit;}" +
                   "hr{border:none;border-top:1px solid #2a3442;margin:20px 0;}" +
                   "strong{color:#f4f7fb;font-weight:600;}" +
                   "a{color:#7eb6ff;text-decoration:none;font-weight:600;}" +
                   "a:hover{text-decoration:underline;}" +
                   ".toc{margin:20px 0;padding:16px 18px;background:#111827;border:1px solid #2a3442;border-radius:8px;}" +
                   ".toc h2{margin:0 0 12px 0;border:none;padding:0;}" +
                   ".toc h3{margin:14px 0 6px 0;color:#f4f7fb;font-size:1em;}" +
                   ".toc ul{margin:0 0 0 20px;padding:0;}" +
                   ".toc li{margin:4px 0;}" +
                   "</style></head><body>" +
                   bodyHtml +
                   "</body></html>";
        }

        private static string ConvertMarkdownToHtml(string markdownText, IReadOnlyList<HeadingInfo> headings)
        {
            var lines = (markdownText ?? string.Empty).Replace("\r\n", "\n").Split('\n');
            var html = new StringBuilder();
            bool inList = false, inParagraph = false, inCodeBlock = false;
            string codeFence = null;
            int headingIndex = 0;

            foreach (var rawLine in lines)
            {
                var line = rawLine.Trim();

                if (!inCodeBlock && line.Length >= 3 &&
                    (line.StartsWith("```", StringComparison.Ordinal) || line.StartsWith("~~~", StringComparison.Ordinal)))
                {
                    CloseParagraphAndList(html, ref inParagraph, ref inList);
                    codeFence = line.Substring(0, 3);
                    html.Append("<pre><code>");
                    inCodeBlock = true;
                    continue;
                }

                if (inCodeBlock)
                {
                    if (line == codeFence || (line.StartsWith(codeFence, StringComparison.Ordinal) && line.Trim(codeFence[0]).Length == 0))
                    {
                        html.Append("</code></pre>");
                        inCodeBlock = false;
                        codeFence = null;
                    }
                    else
                    {
                        html.Append(WebUtility.HtmlEncode(rawLine)).Append('\n');
                    }
                    continue;
                }

                if (line.Length == 0) { CloseParagraphAndList(html, ref inParagraph, ref inList); continue; }

                if (line == "---" || line == "***" || line == "___")
                {
                    CloseParagraphAndList(html, ref inParagraph, ref inList);
                    html.Append("<hr/>");
                    continue;
                }

                if (line.StartsWith("### ", StringComparison.Ordinal) ||
                    line.StartsWith("## ", StringComparison.Ordinal) ||
                    line.StartsWith("# ", StringComparison.Ordinal))
                {
                    CloseParagraphAndList(html, ref inParagraph, ref inList);

                    var heading = headings[headingIndex++];
                    html.Append("<h")
                        .Append(heading.Level)
                        .Append(" id=\"")
                        .Append(heading.Id)
                        .Append("\">")
                        .Append(ConvertInline(heading.RawText))
                        .Append("</h")
                        .Append(heading.Level)
                        .Append(">");
                    continue;
                }

                if (line.StartsWith("- ", StringComparison.Ordinal))
                {
                    if (inParagraph) { html.Append("</p>"); inParagraph = false; }
                    if (!inList) { html.Append("<ul>"); inList = true; }
                    html.Append("<li>").Append(ConvertInline(line.Substring(2))).Append("</li>");
                    continue;
                }

                if (inList) { html.Append("</ul>"); inList = false; }
                if (!inParagraph) { html.Append("<p>"); inParagraph = true; } else html.Append(' ');
                html.Append(ConvertInline(line));
            }

            if (inCodeBlock) html.Append("</code></pre>");
            if (inParagraph) html.Append("</p>");
            if (inList) html.Append("</ul>");
            return html.ToString();
        }

        private static MarkdownDocument ParseMarkdownDocument(string markdown)
        {
            var normalized = (markdown ?? string.Empty).Replace("\r\n", "\n");
            if (!normalized.StartsWith("---\n", StringComparison.Ordinal))
                return new MarkdownDocument(null, normalized);

            var end = normalized.IndexOf("\n---\n", 4, StringComparison.Ordinal);
            if (end < 0)
                return new MarkdownDocument(null, normalized);

            string documentType = null;
            var frontMatter = normalized.Substring(4, end - 4).Split('\n');
            foreach (var line in frontMatter)
            {
                var separatorIndex = line.IndexOf(':');
                if (separatorIndex <= 0) continue;

                var key = line.Substring(0, separatorIndex).Trim();
                var value = line.Substring(separatorIndex + 1).Trim();
                if (key.Equals("document_type", StringComparison.OrdinalIgnoreCase))
                {
                    documentType = value;
                    break;
                }
            }

            return new MarkdownDocument(documentType, normalized.Substring(end + 5));
        }

        private static List<HeadingInfo> CollectHeadings(string markdownText)
        {
            var headings = new List<HeadingInfo>();
            var usedIds = new Dictionary<string, int>(StringComparer.Ordinal);
            var lines = (markdownText ?? string.Empty).Replace("\r\n", "\n").Split('\n');
            bool inCodeBlock = false;
            string codeFence = null;

            foreach (var rawLine in lines)
            {
                var line = rawLine.Trim();

                if (!inCodeBlock && line.Length >= 3 &&
                    (line.StartsWith("```", StringComparison.Ordinal) || line.StartsWith("~~~", StringComparison.Ordinal)))
                {
                    codeFence = line.Substring(0, 3);
                    inCodeBlock = true;
                    continue;
                }

                if (inCodeBlock)
                {
                    if (line == codeFence || (line.StartsWith(codeFence, StringComparison.Ordinal) && line.Trim(codeFence[0]).Length == 0))
                    {
                        inCodeBlock = false;
                        codeFence = null;
                    }

                    continue;
                }

                if (line.StartsWith("### ", StringComparison.Ordinal))
                {
                    headings.Add(CreateHeadingInfo(3, line.Substring(4), usedIds));
                }
                else if (line.StartsWith("## ", StringComparison.Ordinal))
                {
                    headings.Add(CreateHeadingInfo(2, line.Substring(3), usedIds));
                }
                else if (line.StartsWith("# ", StringComparison.Ordinal))
                {
                    headings.Add(CreateHeadingInfo(1, line.Substring(2), usedIds));
                }
            }

            return headings;
        }

        private static HeadingInfo CreateHeadingInfo(int level, string rawText, Dictionary<string, int> usedIds)
        {
            var plainText = GetPlainText(rawText);
            var id = CreateHeadingId(plainText, usedIds);
            return new HeadingInfo(level, rawText.Trim(), plainText, id);
        }

        private static string GetPlainText(string text)
        {
            return (text ?? string.Empty).Replace("`", string.Empty).Trim();
        }

        private static string CreateHeadingId(string text, Dictionary<string, int> usedIds)
        {
            var builder = new StringBuilder();
            bool previousWasDash = false;

            foreach (var ch in text.ToLowerInvariant())
            {
                if (char.IsLetterOrDigit(ch))
                {
                    builder.Append(ch);
                    previousWasDash = false;
                }
                else if ((char.IsWhiteSpace(ch) || ch == '-' || ch == '_') && !previousWasDash && builder.Length > 0)
                {
                    builder.Append('-');
                    previousWasDash = true;
                }
            }

            var baseId = builder.ToString().Trim('-');
            if (string.IsNullOrEmpty(baseId))
                baseId = "section";

            if (!usedIds.TryGetValue(baseId, out var count))
            {
                usedIds[baseId] = 1;
                return baseId;
            }

            count++;
            usedIds[baseId] = count;
            return $"{baseId}-{count}";
        }

        private static bool ShouldRenderTableOfContents(string documentType, IReadOnlyList<HeadingInfo> headings)
        {
            return string.Equals(documentType, FaqDocumentType, StringComparison.OrdinalIgnoreCase) &&
                   headings.Any(heading => heading.Level == 3);
        }

        private static string BuildTableOfContents(IReadOnlyList<HeadingInfo> headings)
        {
            var sections = new List<TableOfContentsSection>();
            TableOfContentsSection currentSection = null;

            foreach (var heading in headings.Where(h => h.Level >= 2))
            {
                if (heading.Level == 2)
                {
                    currentSection = new TableOfContentsSection(heading.PlainText);
                    sections.Add(currentSection);
                    continue;
                }

                if (currentSection == null)
                {
                    currentSection = new TableOfContentsSection(string.Empty);
                    sections.Add(currentSection);
                }

                currentSection.Items.Add(heading);
            }

            if (!sections.Any(section => section.Items.Count > 0))
                return string.Empty;

            var html = new StringBuilder();
            html.Append("<nav class=\"toc\"><h2>Table of Contents</h2>");

            foreach (var section in sections.Where(section => section.Items.Count > 0))
            {
                if (!string.IsNullOrEmpty(section.Title))
                    html.Append("<h3>").Append(WebUtility.HtmlEncode(section.Title)).Append("</h3>");

                html.Append("<ul>");
                foreach (var item in section.Items)
                {
                    html.Append("<li><a href=\"#")
                        .Append(item.Id)
                        .Append("\">")
                        .Append(WebUtility.HtmlEncode(item.PlainText))
                        .Append("</a></li>");
                }

                html.Append("</ul>");
            }

            html.Append("</nav>");
            return html.ToString();
        }

        private static string InsertTableOfContents(string bodyHtml, string tableOfContentsHtml)
        {
            if (string.IsNullOrEmpty(tableOfContentsHtml))
                return bodyHtml;

            var firstSectionIndex = bodyHtml.IndexOf("<h2", StringComparison.Ordinal);
            return firstSectionIndex >= 0
                ? bodyHtml.Insert(firstSectionIndex, tableOfContentsHtml)
                : tableOfContentsHtml + bodyHtml;
        }

        private static void CloseParagraphAndList(StringBuilder html, ref bool inParagraph, ref bool inList)
        {
            if (inParagraph) { html.Append("</p>"); inParagraph = false; }
            if (inList) { html.Append("</ul>"); inList = false; }
        }

        private static string ConvertInline(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            var result = new StringBuilder();
            var token = new StringBuilder();
            bool inCode = false;

            foreach (var ch in text)
            {
                if (ch == '`')
                {
                    if (inCode) { result.Append("<code>").Append(WebUtility.HtmlEncode(token.ToString())).Append("</code>"); token.Clear(); inCode = false; }
                    else { if (token.Length > 0) { result.Append(WebUtility.HtmlEncode(token.ToString())); token.Clear(); } inCode = true; }
                    continue;
                }
                token.Append(ch);
            }

            if (token.Length > 0)
                result.Append(inCode ? "<code>" + WebUtility.HtmlEncode(token.ToString()) + "</code>" : WebUtility.HtmlEncode(token.ToString()));

            return result.ToString();
        }

        private sealed class MarkdownDocument
        {
            public MarkdownDocument(string documentType, string body)
            {
                DocumentType = documentType;
                Body = body ?? string.Empty;
            }

            public string DocumentType { get; }

            public string Body { get; }
        }

        private sealed class HeadingInfo
        {
            public HeadingInfo(int level, string rawText, string plainText, string id)
            {
                Level = level;
                RawText = rawText;
                PlainText = plainText;
                Id = id;
            }

            public int Level { get; }

            public string RawText { get; }

            public string PlainText { get; }

            public string Id { get; }
        }

        private sealed class TableOfContentsSection
        {
            public TableOfContentsSection(string title)
            {
                Title = title;
                Items = new List<HeadingInfo>();
            }

            public string Title { get; }

            public List<HeadingInfo> Items { get; }
        }
    }
}
