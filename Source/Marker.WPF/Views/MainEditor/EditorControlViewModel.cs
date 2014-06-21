namespace BlueMoon.UI.Views.MainEditor
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Windows.Input;

    using BlueMoon.DocumentManager;
    using BlueMoon.UI.Annotations;
    using BlueMoon.UI.EditorCommands;

    using Converters.Markdown;

    using ScintillaNET;

    using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

    public class EditorControlViewModel : INotifyPropertyChanged
    {
        private readonly HtmlPreviewUpdater htmlPreviewer;

        private string htmlPreview;

        private MarkdownDocument document;

        private bool updatingDocument;

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
            this.NewCommand = new NewCommand();
            this.CloseTabCommand = new CloseTabCommand();
            this.SwitchTabCommand = new SwitchTabCommand();
            this.SwitchTabBackCommand = new SwitchTabBackCommand();
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
            ICommand commandToExecute = null;

            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.V:
                        {
                            commandToExecute = this.PasteCommand;
                            break;
                        }

                    case Keys.N:
                        {
                            commandToExecute = this.NewCommand;
                            break;
                        }

                    case Keys.W:
                        {
                            commandToExecute = this.CloseTabCommand;
                            break;
                        }
                    case Keys.Tab:
                        {
                            if (e.Shift)
                            {
                                commandToExecute = this.SwitchTabBackCommand;
                            }
                            else
                            {
                                commandToExecute = this.SwitchTabCommand;    
                            }
                            
                            break;
                        }
                }
            }

            if (commandToExecute != null && commandToExecute.CanExecute(this))
            {
                commandToExecute.Execute(this);
            }
        }

        private async void MarkdownEditorTextChanged(object sender, EventArgs e)
        {
            this.htmlPreviewer.Start(this.MarkdownEditor.Text);
            if (this.updatingDocument)
            {
                return;
            }
            
            this.Document.Markdown = this.MarkdownEditor.Text;
            if (this.Document.HasChanges && DocumentManager.CanSave())
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
