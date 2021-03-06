﻿namespace BlueMoon.UI.Commands.DocumentCommands
{
    using BlueMoon.DocumentManager;

    public class SwitchTabCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return MarkdownApp.Current.Documents.Count > 1;
        }

        public override void Execute(object parameter)
        {
            MarkdownApp.Current.MoveToNextDocument();
        }
    }
}
