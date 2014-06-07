namespace Wilco.SyntaxHighlighting
{
    using System;
    using System.Text;
    using System.Web;
    
    /// <summary>
    /// Wrapper Processor on top of the Syntax Highlighter implementations
    /// </summary>
    public class SyntaxHighlighterProcessor
    {
        private IParser htmlParser;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SyntaxHighlighterProcessor()
        {
        }

        /// <summary>
        /// Applies Syntax Highlighting to the specified source according to its language.
        /// </summary>
        /// <param name="source">The text to transform</param>
        /// <returns>The result.</returns>
        public string Highlight(string source)
        {
            return this.Highlight(source, string.Empty);
        }

        /// <summary>
        /// Applies Syntax Highlighting to the specified source according to its language.
        /// </summary>
        /// <param name="source">The text to transform</param>
        /// <param name="language">The language used to highlight the text.</param>
        /// <returns>The result.</returns>
        public string Highlight(string source, string language)
        {
            return this.Highlight(source, language, null);
        }

        /// <summary>
        /// Applies Syntax Highlighting to the specified source according to its language.
        /// </summary>
        /// <param name="source">The text to transform</param>
        /// <param name="language">The language used to highlight the text.</param>
        /// <param name="linesToMark">The line numbers to apply bold emphasizing.</param>
        /// <returns>The result.</returns>
        public string Highlight(string source, string language, int[] linesToMark)
        {
            return this.Highlight(source, language, linesToMark, null);
        }

        /// <summary>
        /// Applies Syntax Highlighting to the specified source according to its language.
        /// </summary>
        /// <param name="source">The text to transform</param>
        /// <param name="language">The language used to highlight the text.</param>
        /// <param name="linesToMark">The line numbers to apply bold emphasizing.</param>
        /// <param name="linesToStrikethrough">The line numbers to apply strikethrough emphasizing.</param>
        /// <returns>The result.</returns>
        public string Highlight(string source, string language, int[] linesToMark, int[] linesToStrikethrough)
        {
            if (string.IsNullOrEmpty(language))
            {
                language = "C#";
            }

            HighlighterBase highlighter = this.GetHighlighter(language);
            string parsedText = source;
            if (highlighter != null)
            {
                source = HttpUtility.HtmlDecode(source);
                parsedText = highlighter.Parse(source);
            }

            if (linesToMark != null)
            {
                parsedText = this.MarkLines(linesToMark, parsedText);
            }

            if (linesToStrikethrough != null)
            {
                parsedText = this.StrikethroughLines(linesToStrikethrough, parsedText);
            }

            return parsedText;
        }

        /// <summary>
        /// Parses source code.
        /// </summary>
        /// <param name="language">The language used to highlight the text.</param>
        /// <returns>The highlighter.</returns>
        protected HighlighterBase GetHighlighter(string language)
        {
            Register register = Register.Instance;
            HighlighterBase highlighter = register.Highlighters[language];

            if (highlighter == null)
            {
                return null;
            }

            this.EnsureParser();
            highlighter = highlighter.Create();
            highlighter.Parser = this.htmlParser;
            highlighter.ForceReset();
            return highlighter;
        }

        private string ConvertstringArrayToString(string[] array)
        {
            StringBuilder builder = new StringBuilder();
            Array.Resize(ref array, array.Length - 1);
            foreach (string value in array)
            {
                builder.AppendLine(value);
            }

            return builder.ToString();
        }

        private string MarkLines(int[] linesToMark, string parsedText)
        {
            var lines = parsedText.Replace("\r", string.Empty).Split(new char[] { '\n' });

            foreach (var lineNumber in linesToMark)
            {
                var arrayLineNumber = lineNumber - 1;
                if ((arrayLineNumber < lines.GetLowerBound(0)) || arrayLineNumber > lines.GetUpperBound(0))
                {
                    continue;
                }

                var startingStrongHtmlText = "<strong class=\"markLine\">";
                var endingStrongHtmlText = "</strong>";
                lines[arrayLineNumber] = lines[arrayLineNumber].Insert(0, startingStrongHtmlText);
                lines[arrayLineNumber] = lines[arrayLineNumber].Insert(lines[arrayLineNumber].Length, endingStrongHtmlText);
            }

            return this.ConvertstringArrayToString(lines);
        }

        private string StrikethroughLines(int[] linesToStrikethrough, string parsedText)
        {
            var lines = parsedText.Replace("\r", string.Empty).Split(new char[] { '\n' });
            foreach (var lineNumber in linesToStrikethrough)
            {
                var arrayLineNumber = lineNumber - 1;
                if ((arrayLineNumber < lines.GetLowerBound(0)) || arrayLineNumber > lines.GetUpperBound(0))
                {
                    continue;
                }

                var startingStrikeHtmlText = "<span class=\"strikeLine\" style=\"text-decoration:line-through;\">";
                var endingStrikeHtmlText = "</span>";
                lines[arrayLineNumber] = lines[arrayLineNumber].Insert(0, startingStrikeHtmlText);
                lines[arrayLineNumber] = lines[arrayLineNumber].Insert(lines[arrayLineNumber].Length, endingStrikeHtmlText);
            }

            return this.ConvertstringArrayToString(lines);
        }

        private void EnsureParser()
        {
            if (this.htmlParser == null)
            {
                this.htmlParser = new HtmlParser();
            }
        } 
    }
}
