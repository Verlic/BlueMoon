namespace BlueMoon.UI.Commands.DocumentCommands
{
    using BlueMoon.DocumentManager;

    public class ExitCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            MarkdownApp.Current.ExitApp();
        }
    }
}
