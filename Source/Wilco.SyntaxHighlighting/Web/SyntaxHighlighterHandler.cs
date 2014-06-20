namespace Wilco.SyntaxHighlighting.Web
{
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    using Wilco.SyntaxHighlighting;
    using Wilco.Web.SyntaxHighlighting;

    /// <summary>
    /// Represents a syntax highlighter handler for ASP.NET.
    /// </summary>
    public class SyntaxHighlighterHandler : IHttpHandler
    {
        /// <summary>
        /// Gets whether the handler is reusable.
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            var absoluteFilePath = context.Server.MapPath(context.Request.FilePath);
            var fileExtension = Path.GetExtension(absoluteFilePath);
            if (fileExtension == null)
            {
                return;
            }

            var extension = fileExtension.Substring(1);

            var language = "C#"; // Default language.

            foreach (HighlighterBase h in Register.Instance.Highlighters)
            {
                if (h.FileExtensions.Contains(extension))
                {
                    language = h.Name;
                    break;
                }
            }
            
            var page = new Page();
            page.Controls.Add(new LiteralControl(@"<?xml version=""1.0""?>
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">
<html>"));

            var header = new HtmlHead();
            page.Controls.Add(header);
            header.Title = "Source for " + Path.GetFileName(absoluteFilePath);

            var body = new PlaceHolder();
            page.Controls.Add(body);

            page.Controls.Add(new LiteralControl(@"</body>
</html>"));

            var highlighter = new SyntaxHighlighter
                                  {
                                      Language = language,
                                      Mode = HighlightMode.Source,
                                      Text = File.ReadAllText(absoluteFilePath)
                                  };

            body.Controls.Add(highlighter);
            ((IHttpHandler)page).ProcessRequest(context);
        }
    }
}
