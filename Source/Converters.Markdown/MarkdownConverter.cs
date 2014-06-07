namespace Converters.Markdown
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using HtmlAgilityPack;

    using Sundown;

    public class MarkdownConverter
    {
        const MarkdownExtensions Extensions = MarkdownExtensions.AutoLink |
                                                  MarkdownExtensions.FencedCode |
                                                  MarkdownExtensions.LaxHtmlBlocks |
                                                  MarkdownExtensions.NoIntraEmphasis |
                                                  MarkdownExtensions.StrikeThrough |
                                                  MarkdownExtensions.Tables;

        private const string CodeHeaderTemplate = "<span class=\"codelanguage\">{0}</span>";

        public static Task<string> ToHtml(string markdown, string templatePath)
        {
            
            var outputContent = MoonShine.Markdownify(
                markdown,
                Extensions,
                false);

            if (string.IsNullOrWhiteSpace(outputContent))
            {
                outputContent = "<body></body>";
            }

            outputContent = RemoveCodeSnippetExtraBreakline(outputContent);
            outputContent = PrcessCodeSnippetHeaders(outputContent);
            outputContent = new CodeSnippetHighlighter(outputContent).Highlight();

            var props = new Dictionary<string, object> { { "Content", outputContent } };
            return Task.FromResult(TextTemplatingHelper.Process(templatePath, props));
        }

        private static string PrcessCodeSnippetHeaders(string content)
        {
            var parser = new HtmlParser(content);
            var codeSnippetBlocks = parser.GetElements("code");

            if (codeSnippetBlocks == null)
            {
                return content;
            }
            
            foreach (var codeBlock in codeSnippetBlocks)
            {
                if (!codeBlock.Attributes.Contains("class"))
                {
                    continue;
                }

                var language = codeBlock.Attributes["class"].Value;
                if (!string.IsNullOrWhiteSpace(language))
                {
                    codeBlock.ParentNode.ParentNode.InsertBefore(
                        HtmlNode.CreateNode(string.Format(CodeHeaderTemplate, language)),
                        codeBlock.ParentNode);
                }
            }

            return parser.Html;
        }

        private static string RemoveCodeSnippetExtraBreakline(string content)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(content);

            var highlightedLines = doc.DocumentNode.SelectNodes("//p/code");
            if (highlightedLines != null)
            {
                foreach (var line in highlightedLines)
                {
                    if (line.InnerHtml.Substring(0, 1) == "\n")
                    {
                        line.InnerHtml = line.InnerHtml.Substring(1);
                    }
                }

                using (var writer = new StringWriter())
                {
                    doc.Save(writer);
                    return writer.ToString();
                }
            }
            
            return content;
        }
    }
}
