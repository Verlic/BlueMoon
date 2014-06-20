namespace Converters.Markdown
{
    using System.Threading.Tasks;
    using System.Web;

    public class HtmlToMarkdown
    {
        private readonly string templatePath;

        public HtmlToMarkdown(string templatePath)
        {
            this.templatePath = templatePath;
        }

        public Task<string> ToHtml(string markdown)
        {
            var convertedHtml = MarkdownConverter.ToHtml(HttpUtility.HtmlEncode(markdown), this.templatePath);
            return convertedHtml;
        }
    }
}
