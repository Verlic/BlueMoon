namespace Converters.Markdown
{
    public class MarkdownConverter
    {
        public string ToHtml(string markdown)
        {
            var engine = new MarkdownDeep.Markdown();
            var result = engine.Transform(markdown);
            return result;
        }
    }
}
