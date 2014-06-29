namespace BlueMoon.UI.Commands.DocumentCommands
{
    using BlueMoon.DocumentManager;

    public class SaveAsDocumentCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return parameter as MarkdownDocument != null;
        }

        public override void Execute(object parameter)
        {
            var document = (MarkdownDocument)parameter;
            MarkdownApp.Current.SaveDocument(document, true);
        }
    }
}
