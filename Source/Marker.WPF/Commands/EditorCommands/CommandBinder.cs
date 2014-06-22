namespace BlueMoon.UI.Commands.EditorCommands
{
    using System.Globalization;
    using System.Windows.Forms;

    using ScintillaNET;

    public class CommandBinder
    {
        public CommandBinder(Scintilla markdownEditor)
        {
            markdownEditor.KeyDown += this.MarkdownEditorKeyDown;
        }

        private void MarkdownEditorKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.B:
                        {
                            new BoldCommand().Execute((Scintilla)sender);
                            break;
                        }

                    case Keys.I:
                        {
                            new ItalicCommand().Execute((Scintilla)sender);
                            break;
                        }

                    case Keys.D1:
                    case Keys.D2:
                    case Keys.D3:
                    case Keys.D4:
                    case Keys.D5:
                        {
                            new HeaderCommand(int.Parse(e.KeyCode.ToString()[e.KeyCode.ToString().Length - 1].ToString(CultureInfo.InvariantCulture))).Execute(
                                (Scintilla)sender);
                            break;
                        }
                }
            }
        }
    }
}
