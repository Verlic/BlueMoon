namespace BlueMoon.DocumentManager
{
    using System.Windows.Input;

    using BlueMoon.DocumentManager.Commands;

    public class ApplicationCommands
    {
        public ApplicationCommands()
        {
            this.NewDocument = new NewDocumentCommand();
            this.OpenDocument = new OpenDocumentCommand();
            this.CloseDocument = new CloseDocumentCommand();
            this.SaveDocument = new SaveDocumentCommand();
            this.SaveAsDocument = new SaveAsDocumentCommand();
        }

        public ICommand NewDocument { get; set; }

        public ICommand OpenDocument { get; set; }

        public ICommand CloseDocument { get; set; }

        public ICommand SaveDocument { get; set; }

        public ICommand SaveAsDocument { get; set; }
    }
}
