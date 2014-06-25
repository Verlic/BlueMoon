namespace Converters.Markdown
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using HtmlAgilityPack;

    public class HtmlToMarkdown
    {
        private readonly string templatePath;

        public HtmlToMarkdown(string templatePath)
        {
            this.templatePath = templatePath;
            this.ErrorLogs = new List<string>();
        }

        public List<string> ErrorLogs { get; set; } 

        public Task<string> ToHtml(string markdown)
        {
            this.ErrorLogs.Clear();
            try
            {
                markdown = HtmlEntity.Entitize(markdown, false);
            }
            catch (Exception)
            {
                this.ErrorLogs.Add("Unable to process markdown text. Possible cause: Invalid characters");
            }

            try
            {
                var convertedHtml = MarkdownConverter.ToHtml(markdown, this.templatePath);
                return convertedHtml;
            }
            catch (Exception)
            {
                this.ErrorLogs.Add("Unable to convert markdown to HTML");
            }

            return Task.FromResult(markdown);
        }
    }
}
