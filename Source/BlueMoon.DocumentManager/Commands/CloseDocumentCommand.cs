namespace BlueMoon.DocumentManager.Commands
{
    using BlueMoon.DocumentManager;

    public class CloseDocumentCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return parameter as MarkdownDocument != null;
        }

        public override async void Execute(object parameter)
        {
            var document = (MarkdownDocument)parameter;
            await MarkdownApp.Current.CloseDocumentAsync(document);
        }
    }
}
