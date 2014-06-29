namespace BlueMoon.UI.Commands.DocumentCommands
{
    using BlueMoon.DocumentManager;

    public class SaveDocumentCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            var document = parameter as MarkdownDocument;
            return document != null && document.HasChanges;
        }

        public override void Execute(object parameter)
        {
            var document = (MarkdownDocument)parameter;
            MarkdownApp.Current.SaveDocument(document);
        }
    }
}
