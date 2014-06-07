namespace Marker.WPF.Components
{
    using ScintillaNET;

    public class MarkdownSyntaxHighlighter
    {
        public static void Highlight(Scintilla markdownEditor,int position, int linesAdded)
        {
            if (linesAdded > 0)
            {
                for (var i = 0; i <= linesAdded; i++)
                {
                    var lineNumber = markdownEditor.Lines.FromPosition(markdownEditor.CurrentPos).Number + i;
                    var linePosition = markdownEditor.Lines[lineNumber].StartPosition;
                    MarkdownLexer.StyleLine(markdownEditor, linePosition);
                }
            }
            else
            {
                MarkdownLexer.StyleLine(markdownEditor, position);
            }
        }
    }
}
