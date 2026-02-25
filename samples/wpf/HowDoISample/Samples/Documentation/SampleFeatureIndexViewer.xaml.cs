using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class SampleFeatureIndexViewer : UserControl
    {
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly string[] _candidateMarkdownPaths;
        private readonly string[] _candidateSamplesJsonPaths;
        private bool _webMessageAttached;

        public SampleFeatureIndexViewer()
        {
            InitializeComponent();
            _candidateMarkdownPaths = BuildCandidatePaths(Path.Combine("Samples", "Documentation"), "README.md")
                .Concat(BuildCandidatePaths(Path.Combine("Samples", "Documentation"), "SampleFeatureIndex.md"))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();
            _candidateSamplesJsonPaths = BuildCandidatePaths("Samples", "samples.json");
            Loaded += SampleFeatureIndexViewer_OnLoaded;
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

        private async void SampleFeatureIndexViewer_OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= SampleFeatureIndexViewer_OnLoaded;
            await LoadMarkdownAsync();
        }

        private async Task LoadMarkdownAsync()
        {
            var markdownPath = _candidateMarkdownPaths.FirstOrDefault(File.Exists);
            if (string.IsNullOrEmpty(markdownPath))
            {
                ShowPlainText(
                    "# Missing file\n\nUnable to find `Samples/Documentation/README.md`.\n" +
                    "Please build or run from the project root so the file is copied to output.");
                return;
            }

            var markdownText = File.ReadAllText(markdownPath);
            var sampleCatalog = LoadSampleCatalog();

            var renderedInWebView = await TryRenderMarkdownWithWebViewAsync(markdownText, sampleCatalog);

            if (renderedInWebView)
            {
                return;
            }

            ShowPlainText(markdownText);
        }

        private IReadOnlyList<SampleCatalogEntry> LoadSampleCatalog()
        {
            var jsonPath = _candidateSamplesJsonPaths.FirstOrDefault(File.Exists);
            if (string.IsNullOrEmpty(jsonPath)) return Array.Empty<SampleCatalogEntry>();

            try
            {
                var rawJson = File.ReadAllText(jsonPath);
                var cleanedJson = StripLineComments(rawJson);
                var categories = JsonSerializer.Deserialize<List<SampleCatalogCategory>>(cleanedJson, JsonOptions) ??
                                 new List<SampleCatalogCategory>();

                var entries = new List<SampleCatalogEntry>();
                foreach (var category in categories)
                {
                    foreach (var sample in category?.Children ?? Enumerable.Empty<SampleCatalogItem>())
                    {
                        if (string.IsNullOrWhiteSpace(sample.Id)) continue;
                        entries.Add(new SampleCatalogEntry
                        {
                            Id = sample.Id,
                            Title = sample.Title ?? string.Empty,
                            Category = category?.Title ?? string.Empty,
                            Description = sample.Description ?? string.Empty
                        });
                    }
                }

                return entries;
            }
            catch
            {
                return Array.Empty<SampleCatalogEntry>();
            }
        }

        private static string StripLineComments(string json)
        {
            if (string.IsNullOrEmpty(json)) return string.Empty;
            return System.Text.RegularExpressions.Regex.Replace(json, @"(?m)^\s*//.*$", string.Empty);
        }

        private async Task<bool> TryRenderMarkdownWithWebViewAsync(string markdownText, IReadOnlyList<SampleCatalogEntry> sampleCatalog)
        {
            try
            {
                await MarkdownWebView.EnsureCoreWebView2Async();

                if (!_webMessageAttached)
                {
                    MarkdownWebView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
                    _webMessageAttached = true;
                }

                MarkdownWebView.NavigateToString(BuildHtmlDocument(markdownText, sampleCatalog));
                MarkdownWebView.Visibility = Visibility.Visible;
                MarkdownTextBox.Visibility = Visibility.Collapsed;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string sampleId;

            try
            {
                sampleId = e.TryGetWebMessageAsString();
            }
            catch
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(sampleId)) return;

            if (Window.GetWindow(this) is Samples samplesWindow)
            {
                samplesWindow.NavigateToSample(sampleId);
            }
        }

        private void ShowPlainText(string markdownText)
        {
            MarkdownTextBox.Text = markdownText;
            MarkdownTextBox.Visibility = Visibility.Visible;
            MarkdownWebView.Visibility = Visibility.Collapsed;
            MarkdownTextBox.ScrollToHome();
        }

        private static string BuildHtmlDocument(string markdownText, IReadOnlyList<SampleCatalogEntry> sampleCatalog)
        {
            var htmlBody = ConvertMarkdownToHtml(markdownText, sampleCatalog);

            return "<!DOCTYPE html><html><head><meta charset=\"utf-8\"/>" +
                   "<style>" +
                   "body{font-family:'Segoe UI',sans-serif;margin:16px;color:#dce3ec;background:#0d1117;line-height:1.5;}" +
                   "h1,h2,h3{margin:16px 0 8px 0;color:#f4f7fb;}" +
                   "p{margin:8px 0;}" +
                   "ul{margin:8px 0 8px 20px;padding:0;}" +
                   "code{font-family:Consolas,monospace;background:#1f2937;color:#dce3ec;padding:1px 4px;border-radius:4px;}" +
                   "table{width:100%;border-collapse:collapse;margin:12px 0;font-size:13px;}" +
                   "th,td{border:1px solid #2a3442;padding:6px 8px;text-align:left;vertical-align:top;}" +
                   "thead th{background:#172030;position:sticky;top:0;}" +
                   "tbody tr:nth-child(even){background:#121926;}" +
                   "a[data-sample-id]{color:#7eb6ff;text-decoration:none;font-weight:600;}" +
                   "a[data-sample-id]:hover{text-decoration:underline;}" +
                   "</style></head><body>" +
                   htmlBody +
                   "<script>" +
                   "document.addEventListener('click', function(event) {" +
                   "  var anchor = event.target.closest('a[data-sample-id]');" +
                   "  if (!anchor) return;" +
                   "  event.preventDefault();" +
                   "  if (window.chrome && window.chrome.webview) {" +
                   "    window.chrome.webview.postMessage(anchor.getAttribute('data-sample-id'));" +
                   "  }" +
                   "});" +
                   "</script>" +
                   "</body></html>";
        }

        private static string ConvertMarkdownToHtml(string markdownText, IReadOnlyList<SampleCatalogEntry> sampleCatalog)
        {
            var markdown = StripYamlFrontMatter(markdownText ?? string.Empty);
            var lines = markdown.Replace("\r\n", "\n").Split('\n');
            var html = new StringBuilder();
            bool inList = false;
            bool inParagraph = false;
            string currentCategory = string.Empty;

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Trim();

                if (line.Length == 0)
                {
                    if (inParagraph)
                    {
                        html.Append("</p>");
                        inParagraph = false;
                    }

                    if (inList)
                    {
                        html.Append("</ul>");
                        inList = false;
                    }

                    continue;
                }

                if (IsTableLine(line))
                {
                    if (inParagraph)
                    {
                        html.Append("</p>");
                        inParagraph = false;
                    }

                    if (inList)
                    {
                        html.Append("</ul>");
                        inList = false;
                    }

                    var tableLines = new List<string>();
                    while (i < lines.Length)
                    {
                        var candidate = lines[i].Trim();
                        if (!IsTableLine(candidate)) break;
                        tableLines.Add(candidate);
                        i++;
                    }

                    i--;
                    html.Append(BuildHtmlTable(tableLines, sampleCatalog, currentCategory));
                    continue;
                }

                if (line.StartsWith("# ", StringComparison.Ordinal) ||
                    line.StartsWith("## ", StringComparison.Ordinal) ||
                    line.StartsWith("### ", StringComparison.Ordinal))
                {
                    if (inParagraph)
                    {
                        html.Append("</p>");
                        inParagraph = false;
                    }

                    if (inList)
                    {
                        html.Append("</ul>");
                        inList = false;
                    }

                    if (line.StartsWith("### ", StringComparison.Ordinal))
                    {
                        var headingText = line.Substring(4);
                        currentCategory = ExtractCategoryName(headingText);
                        html.Append("<h3>").Append(ConvertInline(headingText)).Append("</h3>");
                    }
                    else if (line.StartsWith("## ", StringComparison.Ordinal))
                    {
                        html.Append("<h2>").Append(ConvertInline(line.Substring(3))).Append("</h2>");
                    }
                    else
                    {
                        html.Append("<h1>").Append(ConvertInline(line.Substring(2))).Append("</h1>");
                    }

                    continue;
                }

                if (line.StartsWith("- ", StringComparison.Ordinal))
                {
                    if (inParagraph)
                    {
                        html.Append("</p>");
                        inParagraph = false;
                    }

                    if (!inList)
                    {
                        html.Append("<ul>");
                        inList = true;
                    }

                    html.Append("<li>").Append(ConvertInline(line.Substring(2))).Append("</li>");
                    continue;
                }

                if (inList)
                {
                    html.Append("</ul>");
                    inList = false;
                }

                if (!inParagraph)
                {
                    html.Append("<p>");
                    inParagraph = true;
                }
                else
                {
                    html.Append(' ');
                }

                html.Append(ConvertInline(line));
            }

            if (inParagraph) html.Append("</p>");
            if (inList) html.Append("</ul>");
            return html.ToString();
        }

        private static string StripYamlFrontMatter(string markdown)
        {
            if (string.IsNullOrEmpty(markdown)) return string.Empty;

            var normalized = markdown.Replace("\r\n", "\n");
            if (!normalized.StartsWith("---\n", StringComparison.Ordinal))
                return normalized;

            var endIndex = normalized.IndexOf("\n---\n", 4, StringComparison.Ordinal);
            if (endIndex < 0) return normalized;

            return normalized.Substring(endIndex + 5);
        }

        private static string BuildHtmlTable(IReadOnlyList<string> tableLines, IReadOnlyList<SampleCatalogEntry> sampleCatalog, string currentCategory)
        {
            if (tableLines == null || tableLines.Count == 0) return string.Empty;

            var headerCells = SplitTableCells(tableLines[0]);
            var hasSeparator = tableLines.Count > 1 && IsTableSeparatorLine(tableLines[1]);
            var firstDataRowIndex = hasSeparator ? 2 : 1;
            var titleColumn = FindColumnIndex(headerCells, "Title");
            var focusColumn = FindColumnIndex(headerCells, "Focus");

            var sb = new StringBuilder();
            sb.Append("<table>");

            if (headerCells.Count > 0)
            {
                sb.Append("<thead><tr>");
                foreach (var headerCell in headerCells)
                {
                    sb.Append("<th>").Append(ConvertInline(headerCell)).Append("</th>");
                }
                sb.Append("</tr></thead>");
            }

            sb.Append("<tbody>");
            for (var i = firstDataRowIndex; i < tableLines.Count; i++)
            {
                var rowCells = SplitTableCells(tableLines[i]);
                if (rowCells.Count == 0) continue;

                var sampleId = ResolveSampleId(sampleCatalog, rowCells, currentCategory, titleColumn, focusColumn);
                sb.Append("<tr>");
                for (var cellIndex = 0; cellIndex < rowCells.Count; cellIndex++)
                {
                    var cellHtml = ConvertInline(rowCells[cellIndex]);

                    if (cellIndex == titleColumn && !string.IsNullOrEmpty(sampleId))
                    {
                        sb.Append("<td><a href=\"#\" data-sample-id=\"")
                          .Append(WebUtility.HtmlEncode(sampleId))
                          .Append("\">")
                          .Append(cellHtml)
                          .Append("</a></td>");
                    }
                    else
                    {
                        sb.Append("<td>").Append(cellHtml).Append("</td>");
                    }
                }
                sb.Append("</tr>");
            }
            sb.Append("</tbody></table>");

            return sb.ToString();
        }

        private static int FindColumnIndex(IReadOnlyList<string> headerCells, string columnNamePrefix)
        {
            if (headerCells == null) return -1;
            var target = NormalizeLookupPart(columnNamePrefix);

            for (var i = 0; i < headerCells.Count; i++)
            {
                var header = NormalizeLookupPart(headerCells[i]);
                if (header.StartsWith(target, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }

        private static string ResolveSampleId(IReadOnlyList<SampleCatalogEntry> sampleCatalog,
            IReadOnlyList<string> rowCells, string currentCategory, int titleColumn, int focusColumn)
        {
            if (sampleCatalog == null || sampleCatalog.Count == 0 || rowCells == null) return string.Empty;
            if (titleColumn < 0 || titleColumn >= rowCells.Count) return string.Empty;

            var title = rowCells[titleColumn];
            var focus = focusColumn >= 0 && focusColumn < rowCells.Count ? rowCells[focusColumn] : string.Empty;

            var titleKey = NormalizeLookupPart(title);
            var focusKey = NormalizeLookupPart(focus);

            var normalizedCategory = NormalizeLookupPart(currentCategory);

            var categoryTitleMatches = sampleCatalog.Where(item =>
                CategoryMatches(item, normalizedCategory) &&
                NormalizeLookupPart(item.Title) == titleKey).ToList();
            if (categoryTitleMatches.Count == 1) return categoryTitleMatches[0].Id;

            // Additional disambiguation using row focus text when duplicate titles exist.
            var categoryTitleFocusMatches = categoryTitleMatches.Where(item =>
                NormalizeLookupPart(item.Description) == focusKey).ToList();
            if (categoryTitleFocusMatches.Count == 1) return categoryTitleFocusMatches[0].Id;

            var titleMatches = sampleCatalog.Where(item => NormalizeLookupPart(item.Title) == titleKey).ToList();
            if (titleMatches.Count == 1) return titleMatches[0].Id;

            return string.Empty;
        }

        private static string NormalizeLookupPart(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;

            var normalized = text.Trim()
                .Replace("<br/>", "|")
                .Replace("<br />", "|")
                .Replace("<br>", "|")
                .Replace("<BR/>", "|")
                .Replace("<BR />", "|")
                .Replace("<BR>", "|");

            normalized = string.Join(" ", normalized
                .Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));

            return normalized;
        }

        private static string ExtractCategoryName(string headingText)
        {
            if (string.IsNullOrWhiteSpace(headingText)) return string.Empty;

            var trimmed = headingText.Trim();
            var match = System.Text.RegularExpressions.Regex.Match(trimmed, @"^(?<name>.+?)\s*\(\d+\)\s*$");
            return match.Success ? match.Groups["name"].Value.Trim() : trimmed;
        }

        private static bool CategoryMatches(SampleCatalogEntry entry, string normalizedCategory)
        {
            if (string.IsNullOrWhiteSpace(normalizedCategory)) return true;
            return NormalizeLookupPart(entry.Category) == normalizedCategory;
        }

        private static List<string> SplitTableCells(string tableLine)
        {
            if (string.IsNullOrWhiteSpace(tableLine)) return new List<string>();

            var trimmed = tableLine.Trim();
            if (trimmed.StartsWith("|", StringComparison.Ordinal))
                trimmed = trimmed.Substring(1);
            if (trimmed.EndsWith("|", StringComparison.Ordinal))
                trimmed = trimmed.Substring(0, trimmed.Length - 1);

            return trimmed.Split('|').Select(c => c.Trim()).ToList();
        }

        private static bool IsTableLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return false;
            var trimmed = line.Trim();
            return trimmed.Length > 2 &&
                   trimmed.StartsWith("|", StringComparison.Ordinal) &&
                   trimmed.EndsWith("|", StringComparison.Ordinal) &&
                   trimmed.IndexOf('|', 1) > 0;
        }

        private static bool IsTableSeparatorLine(string line)
        {
            var cells = SplitTableCells(line);
            if (cells.Count == 0) return false;

            foreach (var cell in cells)
            {
                var value = cell.Trim();
                if (value.Length == 0) return false;

                bool hasDash = false;
                foreach (var ch in value)
                {
                    if (ch == '-')
                    {
                        hasDash = true;
                        continue;
                    }

                    if (ch != ':') return false;
                }

                if (!hasDash) return false;
            }

            return true;
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
                    if (inCode)
                    {
                        result.Append("<code>").Append(WebUtility.HtmlEncode(token.ToString())).Append("</code>");
                        token.Clear();
                        inCode = false;
                    }
                    else
                    {
                        if (token.Length > 0)
                        {
                            result.Append(WebUtility.HtmlEncode(token.ToString()));
                            token.Clear();
                        }

                        inCode = true;
                    }

                    continue;
                }

                token.Append(ch);
            }

            if (token.Length > 0)
            {
                if (inCode)
                    result.Append("<code>").Append(WebUtility.HtmlEncode(token.ToString())).Append("</code>");
                else
                    result.Append(WebUtility.HtmlEncode(token.ToString()));
            }

            return result.ToString()
                .Replace("&lt;br&gt;", "<br/>")
                .Replace("&lt;br/&gt;", "<br/>")
                .Replace("&lt;br /&gt;", "<br/>");
        }

        private sealed class SampleCatalogEntry
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Category { get; set; }
            public string Description { get; set; }
        }

        private sealed class SampleCatalogCategory
        {
            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("children")]
            public List<SampleCatalogItem> Children { get; set; }
        }

        private sealed class SampleCatalogItem
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("description")]
            public string Description { get; set; }
        }
    }
}
