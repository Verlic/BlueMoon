namespace BlueMoon.UI.Views.MainEditor
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    using BlueMoon.UI.Commands;
    using BlueMoon.UI.Commands.DocumentCommands;
    using BlueMoon.UI.Commands.EditorCommands;

    public class CommandRegister
    {
        public CommandRegister(EditorViewModel viewModel)
        {
            this.CommandCatalog = new Dictionary<Keys, CommandBase>();
            this.InstantiateCommands(viewModel);
            this.RegisterCommands(viewModel);
        }

        private Dictionary<Keys, CommandBase> CommandCatalog { get; set; }
        
        public CommandBase GetCommand(Keys key)
        {
            if (this.CommandCatalog.ContainsKey(key))
            {
                return this.CommandCatalog[key];    
            }

            return null;
        }

        private void InstantiateCommands(EditorViewModel viewModel)
        {
            viewModel.ItalicCommand = new ItalicCommand();
            viewModel.BoldCommand = new BoldCommand();
            viewModel.CutCommand = new CutCommand();
            viewModel.CopyCommand = new CopyCommand();
            viewModel.PasteCommand = new PasteCommand();
            viewModel.NewCommand = new NewCommand();
            viewModel.CloseTabCommand = new CloseTabCommand();
            viewModel.SwitchTabCommand = new SwitchTabCommand();
            viewModel.SwitchTabBackCommand = new SwitchTabBackCommand();
            viewModel.SaveDocumentCommand = new SaveDocumentCommand();
            viewModel.SaveAsDocumentCommand = new SaveAsDocumentCommand();
            viewModel.OpenDocumentCommand = new OpenDocumentCommand();
            viewModel.ExitCommand = new ExitCommand();
        }

        private void RegisterCommands(EditorViewModel viewModel)
        {
            this.CommandCatalog.Add(Keys.Control | Keys.X, viewModel.CutCommand);
            this.CommandCatalog.Add(Keys.Control | Keys.C, viewModel.CopyCommand);
            this.CommandCatalog.Add(Keys.Control | Keys.V, viewModel.PasteCommand);
            this.CommandCatalog.Add(Keys.Control | Keys.N, viewModel.NewCommand);
            this.CommandCatalog.Add(Keys.Control | Keys.O, viewModel.OpenDocumentCommand);
            this.CommandCatalog.Add(Keys.Control | Keys.S, viewModel.SaveDocumentCommand);
            this.CommandCatalog.Add(Keys.Control | Keys.Shift | Keys.S, viewModel.SaveAsDocumentCommand);
            this.CommandCatalog.Add(Keys.Control | Keys.Tab, viewModel.SwitchTabCommand);
            this.CommandCatalog.Add(Keys.Control | Keys.Shift | Keys.Tab, viewModel.SwitchTabBackCommand);
            this.CommandCatalog.Add(Keys.Control | Keys.W, viewModel.CloseTabCommand);
        }
    }
}
