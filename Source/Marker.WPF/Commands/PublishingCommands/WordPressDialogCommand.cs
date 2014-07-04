namespace BlueMoon.UI.Commands.PublishingCommands
{
    using BlueMoon.DocumentManager;
    using BlueMoon.UI.Views.Publishing;

    using Publishers;

    public class WordPressDialogCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var dialog = new PublishToWordPressView();
            var result = dialog.ShowDialog();
            if (result != true)
            {
                return;
            }

            var publisher = new WordpressPublisher(dialog.Site, dialog.Username, dialog.Password);
            publisher.Publish(dialog.PostTitle, MarkdownApp.Current.CurrentDocument.Markdown, !dialog.IsDraft);
        }
    }
}
