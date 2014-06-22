namespace BlueMoon.UI.Views.MainEditor
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    using BlueMoon.DocumentManager;
    using BlueMoon.UI.Annotations;
    using BlueMoon.UI.Commands.DocumentCommands;
    using BlueMoon.UI.Commands.EditorCommands;

    using Converters.Markdown;

    using ScintillaNET;

    using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

    public class EditorControlViewModel : INotifyPropertyChanged
    {
        private readonly HtmlPreviewUpdater htmlPreviewer;

        private string htmlPreview;

        private MarkdownDocument document;

        private bool updatingDocument;

        private CommandRegister commandRegister;

        public EditorControlViewModel()
        {
        }

        public EditorControlViewModel(Scintilla markdownEditor, MarkdownDocument document)
        {
            this.htmlPreviewer = new HtmlPreviewUpdater(this.UpdateHtml);
            this.MarkdownEditor = markdownEditor;
            this.commandRegister = new CommandRegister(this);
            this.HandleEditorEvents();
            this.Document = document;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<MarkdownDocument> Documents { get; set; }

        public MarkdownDocument Document
        {
            get
            {
                return this.document;
            }

            set
            {
                this.document = value;
                this.updatingDocument = true;
                this.MarkdownEditor.Text = this.document.Markdown;
                this.updatingDocument = false;
                this.OnPropertyChanged();
            }
        }

        public Scintilla MarkdownEditor { get; set; }

        public CutCommand CutCommand { get; set; }

        public CopyCommand CopyCommand { get; set; }

        public PasteCommand PasteCommand { get; set; }

        public NewCommand NewCommand { get; set; }

        public CloseTabCommand CloseTabCommand { get; set; }

        public SwitchTabCommand SwitchTabCommand { get; set; }

        public SwitchTabBackCommand SwitchTabBackCommand { get; set; }

        public SaveDocumentCommand SaveDocumentCommand { get; set; }

        public OpenDocumentCommand OpenDocumentCommand { get; set; }

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

        public void UpdateCommandStatus()
        {
            if (this.MarkdownEditor.Clipboard.CanCut)
            {
                this.CutCommand.RaiseCanExecuteChanged();
            }

            if (this.MarkdownEditor.Clipboard.CanCopy)
            {
                this.CopyCommand.RaiseCanExecuteChanged();
            }
        }

        public void InsertImage(string markdownImage)
        {
            this.MarkdownEditor.InsertText(this.MarkdownEditor.CurrentPos, markdownImage);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void HandleEditorEvents()
        {
            this.MarkdownEditor.SelectionChanged += this.MarkdownEditorSelectionChanged;
            this.MarkdownEditor.TextChanged += this.MarkdownEditorTextChanged;
            this.MarkdownEditor.KeyDown += this.MarkdownEditorKeyDown;
        }

        private void MarkdownEditorSelectionChanged(object sender, EventArgs e)
        {
            this.UpdateCommandStatus();
        }

        private void MarkdownEditorKeyDown(object sender, KeyEventArgs e)
        {
            var key = e.KeyCode;

            if (e.Control)
            {
                key = key | Keys.Control;
            }

            if (e.Shift)
            {
                key = key | Keys.Shift;
            }

            if (e.Alt)
            {
                key = key | Keys.Alt;
            }

            var commandToExecute = this.commandRegister.GetCommand(key);

            if (commandToExecute != null && commandToExecute.CanExecute(this))
            {
                commandToExecute.Execute(this);
            }
        }

        private void MarkdownEditorTextChanged(object sender, EventArgs e)
        {
            this.htmlPreviewer.Start(this.MarkdownEditor.Text, this.Document.WorkingFolder);
            if (this.updatingDocument)
            {
                return;
            }
            
            this.Document.Markdown = this.MarkdownEditor.Text;
        }

        private void UpdateHtml(string html)
        {
            this.HtmlPreview = html;
        }
    }
}
