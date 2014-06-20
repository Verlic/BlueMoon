namespace BlueMoon.UI.Components
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
            scintilla.Commands.RemoveBinding(Keys.V, Keys.Control);
            scintilla.Font = new Font("Segoe UI", 12);
            scintilla.LineWrapping.Mode = LineWrappingMode.Word;
            scintilla.IsBraceMatching = true;
            scintilla.Indentation.ShowGuides = true;
            scintilla.Indentation.SmartIndentType = SmartIndent.CPP2;
            scintilla.Indentation.TabIndents = true;
            scintilla.EndOfLine.Mode = EndOfLineMode.Crlf;
            scintilla.Margins[0].Width = 40;
            MarkdownLexer.Init(scintilla);
        }

        public static void AddHandlers(Scintilla markdownEditor, EventHandler<TextModifiedEventArgs> scintillaTextInserted, EventHandler scintillaTextChanged)
        {
            markdownEditor.TextChanged += scintillaTextChanged;
            markdownEditor.TextInserted += scintillaTextInserted;
        }
    }
}
