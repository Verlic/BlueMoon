namespace Converters.Markdown
{
    using Sundown;

    public class MarkdownConverter
    {
        const MarkdownExtensions Extensions = MarkdownExtensions.AutoLink |
                                                  MarkdownExtensions.FencedCode |
                                                  MarkdownExtensions.LaxHtmlBlocks |
                                                  MarkdownExtensions.NoIntraEmphasis |
                                                  MarkdownExtensions.StrikeThrough |
                                                  MarkdownExtensions.Tables;

        public static string ToHtml(string markdown)
        {
            
            var outputContent = MoonShine.Markdownify(
                markdown,
                Extensions,
                false);

            if (string.IsNullOrWhiteSpace(outputContent))
            {
                outputContent = "<body></body>";
            }

            return "<style>body{ background-color: rgb(40,40,40); color: white; }</style>" + outputContent;
        }
    }
}
