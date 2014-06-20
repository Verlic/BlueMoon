namespace BlueMoon.UI.Views.MainEditor
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    using BlueMoon.DocumentManager;
    using BlueMoon.UI.Annotations;
    using BlueMoon.UI.EditorCommands;

    using Converters.Markdown;

    using ScintillaNET;

    public class EditorControlViewModel : INotifyPropertyChanged
    {
        private readonly HtmlPreviewUpdater htmlPreviewer;

        private string htmlPreview;

        private MarkdownDocument document;

        public EditorControlViewModel()
        {
        }

        public EditorControlViewModel(Scintilla markdownEditor, MarkdownDocument document)
        {
            this.htmlPreviewer = new HtmlPreviewUpdater(this.UpdateHtml);
            this.MarkdownEditor = markdownEditor;
            this.CreateCommands();
            this.HandleEditorEvents();
            this.Document = document;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MarkdownDocument Document
        {
            get
            {
                return this.document;
            }

            set
            {
                this.document = value;
                this.MarkdownEditor.Text = this.document.Markdown;
            }
        }

        public Scintilla MarkdownEditor { get; set; }

        public CutCommand CutCommand { get; set; }

        public CopyCommand CopyCommand { get; set; }

        public PasteCommand PasteCommand { get; set; }

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

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void CreateCommands()
        {
            this.CutCommand = new CutCommand();
            this.CopyCommand = new CopyCommand();
            this.PasteCommand = new PasteCommand();
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
            if (!e.Control || e.KeyCode != Keys.V)
            {
                return;
            }

            if (this.PasteCommand.CanExecute(this))
            {
                this.PasteCommand.Execute(this);
            }
        }

        private async void MarkdownEditorTextChanged(object sender, EventArgs e)
        {
            this.htmlPreviewer.Start(this.MarkdownEditor.Text);
            this.Document.Markdown = this.MarkdownEditor.Text;
            if (DocumentManager.CanSave())
            {
                await DocumentManager.SaveDocument(this.Document);
            }
        }

        private void UpdateHtml(string html)
        {
            this.HtmlPreview = html;
        }
    }
}
