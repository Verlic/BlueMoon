namespace BlueMoon.UI.Views.MainEditor
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using BlueMoon.DocumentManager;
    using BlueMoon.UI.Annotations;
    using BlueMoon.UI.Commands.DocumentCommands;
    using BlueMoon.UI.Commands.EditorCommands;

    using Converters.Markdown;

    public class EditorViewModel : INotifyPropertyChanged
    {
        private string htmlPreview;

        private readonly HtmlPreviewUpdater htmlPreviewer;

        private CommandRegister commandRegister;

        public EditorViewModel()
        {
            this.commandRegister = new CommandRegister(this);
            this.htmlPreviewer = new HtmlPreviewUpdater(this.UpdateHtml);
            MarkdownApp.Current.CurrentDocumentMarkdownChanged += this.CurrentCurrentDocumentMarkdownChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public BoldCommand BoldCommand { get; set; }

        public ItalicCommand ItalicCommand { get; set; }

        public CutCommand CutCommand { get; set; }

        public CopyCommand CopyCommand { get; set; }

        public PasteCommand PasteCommand { get; set; }

        public NewCommand NewCommand { get; set; }

        public CloseTabCommand CloseTabCommand { get; set; }

        public SwitchTabCommand SwitchTabCommand { get; set; }

        public SwitchTabBackCommand SwitchTabBackCommand { get; set; }

        public SaveDocumentCommand SaveDocumentCommand { get; set; }

        public OpenDocumentCommand OpenDocumentCommand { get; set; }

        public SaveAsDocumentCommand SaveAsDocumentCommand { get; set; }

        public ExitCommand ExitCommand { get; set; }

        public string HtmlPreview
        {
            get
            {
                return this.htmlPreview;
            }

            set
            {
                this.htmlPreview = value;
                this.OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void UpdateHtml(string html)
        {
            this.HtmlPreview = html;
        }

        private void CurrentCurrentDocumentMarkdownChanged(object sender, EventArgs eventArgs)
        {
            var currentDocument = MarkdownApp.Current.CurrentDocument;
            if (currentDocument == null)
            {
                return;
            }

            this.htmlPreviewer.Start(currentDocument.Markdown, currentDocument.WorkingFolder);
        }
    }
}
