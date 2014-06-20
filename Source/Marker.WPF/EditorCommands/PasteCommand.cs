﻿namespace BlueMoon.UI.EditorCommands
{
    using BlueMoon.UI.Views.MainEditor;

    public class PasteCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var viewModel = parameter as MainEditorViewModel;
            if (viewModel == null)
            {
                return;
            }

            var pasteCommand = PasteTypeFactory.GetPasteCommand();
            if (pasteCommand.CanExecute(viewModel))
            {
                pasteCommand.Execute(viewModel);    
            }
        }
    }
}
