namespace BlueMoon.DocumentManager.Commands
{
    using BlueMoon.DocumentManager;

    public class SaveDocumentCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            var document = parameter as MarkdownDocument;
            return document != null && document.HasChanges;
        }

        public override async void Execute(object parameter)
        {
            var document = (MarkdownDocument)parameter;
            await MarkdownApp.Current.SaveDocumentAsync(document);
        }
    }
}
