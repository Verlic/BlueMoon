namespace Converters.Markdown
{
    using System.Threading.Tasks;
    using System.Web;

    using HtmlAgilityPack;

    public class HtmlToMarkdown
    {
        private readonly string templatePath;

        public HtmlToMarkdown(string templatePath)
        {
            this.templatePath = templatePath;
        }

        public Task<string> ToHtml(string markdown)
        {
            markdown = HtmlEntity.Entitize(markdown);
            var convertedHtml = MarkdownConverter.ToHtml(markdown, this.templatePath);
            return convertedHtml;
        }
    }
}
