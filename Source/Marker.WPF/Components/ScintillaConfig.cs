namespace Marker.WPF.Components
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Windows.Forms.Integration;

    using ScintillaNET;

    public class ScintillaConfig
    {
        public static void ConfigureScintilla(WindowsFormsHost hostControl)
        {
            var scintilla = new Scintilla();
            hostControl.Child = scintilla;
            scintilla.Commands.RemoveBinding(Keys.I, Keys.Control);
            scintilla.Font = new Font("Consolas", 12);
            scintilla.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            scintilla.ForeColor = System.Drawing.Color.White;
            scintilla.Caret.Color = System.Drawing.Color.White;
            scintilla.LineWrapping.Mode = LineWrappingMode.Word;
            scintilla.IsBraceMatching = true;
            scintilla.Indentation.ShowGuides = true;
            scintilla.Indentation.SmartIndentType = SmartIndent.CPP2;
            scintilla.Indentation.TabIndents = true;
            scintilla.EndOfLine.Mode = EndOfLineMode.Crlf;
            scintilla.Margins[0].Width = 40;
            scintilla.Encoding = System.Text.Encoding.UTF8;
            MarkdownLexer.Init(scintilla);
        }

        public static void AddHandlers(Scintilla markdownEditor, EventHandler<TextModifiedEventArgs> scintillaTextInserted, EventHandler scintillaTextChanged)
        {
            markdownEditor.TextChanged += scintillaTextChanged;
            markdownEditor.TextInserted += scintillaTextInserted;
        }
    }
}
