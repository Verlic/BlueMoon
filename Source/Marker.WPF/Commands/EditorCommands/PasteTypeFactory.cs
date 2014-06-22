namespace BlueMoon.UI.Commands.EditorCommands
{
    using System.Windows;

    public class PasteTypeFactory
    {
        public static CommandBase GetPasteCommand()
        {
            if (Clipboard.ContainsImage())
            {
                return new PasteImageCommand();
            }

            if (Clipboard.ContainsText(TextDataFormat.Rtf))
            {
                return new PasteRichTextCommand();
            }

            return new PastePlainTextCommand();
        }
    }
}
