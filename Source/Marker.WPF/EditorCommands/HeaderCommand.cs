﻿namespace Marker.WPF.EditorCommands
{
    using ScintillaNET;

    public class HeaderCommand
    {
        private readonly string wrapper;

        public HeaderCommand(int level) : this(new string('#', level))
        {
        }

        private HeaderCommand(string wrapper)
        {
            this.wrapper = wrapper;
        }

        public void Execute(Scintilla markdownEditor)
        {
            markdownEditor.Selection.Range.StartingLine.Text = string.Format(
                "{0} {1} {0}",
                this.wrapper,
                markdownEditor.Selection.Range.StartingLine.Text);
        }
    }
}