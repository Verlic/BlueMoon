namespace Marker.WPF.Components
{
    using System.Threading.Tasks;

    using Converters.Markdown;

    public class HtmlToMarkdown
    {
        private readonly string templatePath;

        public HtmlToMarkdown(string templatePath)
        {
            this.templatePath = templatePath;
        }

        public async Task<string> ToHtml(string markdown)
        {
            return await MarkdownConverter.ToHtml(markdown, this.templatePath);
        }
    }
}
