namespace BlueMoon.DocumentManager
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;

    using BlueMoon.DocumentManager.Annotations;

    using Converters.Markdown;

    public class MarkdownApp : INotifyPropertyChanged
    {
        private readonly DocumentManager documentManager;

        private readonly HtmlPreviewUpdater htmlPreviewer;

        private MarkdownDocument currentDocument;

        private string htmlPreview;

        static MarkdownApp()
        {
            Current = new MarkdownApp();
        }

        public MarkdownApp()
        {
            this.Documents = new ObservableCollection<MarkdownDocument>();
            this.documentManager = new DocumentManager();
            this.Commands = new ApplicationCommands();
            this.htmlPreviewer = new HtmlPreviewUpdater(this.UpdateHtml);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler CurrentDocumentMarkdownChanged;

        public static MarkdownApp Current { get; private set; }

        public ObservableCollection<MarkdownDocument> Documents { get; set; }

        public ApplicationCommands Commands { get; private set; }

        public MarkdownDocument CurrentDocument
        {
            get
            {
                return this.currentDocument;
            }

            set
            {
                if (this.currentDocument != null)
                {
                    this.currentDocument.MarkdownChanged -= this.OnCurrentDocumentMarkdownChanged;
                }

                this.currentDocument = value;
                this.currentDocument.MarkdownChanged += this.OnCurrentDocumentMarkdownChanged;
                this.RaiseCurrentDocumentMarkdownChanged();
                this.OnPropertyChanged();
            }
        }

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

        public void NewDocument()
        {
            var newDocument = this.documentManager.StartNewDocument();
            this.Documents.Add(newDocument);
            this.CurrentDocument = newDocument;
        }

        public async void OpenDocument()
        {
            string filename;
            var result = Dialogs.ShowOpenFileDialog(out filename);

            if (result == false)
            {
                return;
            }

            var existingDocument =
                this.Documents.FirstOrDefault(
                    document =>
                    string.Equals(document.DocumentPath, filename, StringComparison.InvariantCultureIgnoreCase));

            if (existingDocument != null)
            {
                this.CurrentDocument = existingDocument;
            }
            else
            {
                var openedDocument = await this.documentManager.OpenDocumentAsync(filename);
                this.Documents.Add(openedDocument);
                this.CurrentDocument = openedDocument;
            }
        }

        public async Task<bool> SaveDocumentAsync(MarkdownDocument document, bool showDialog = false)
        {
            if (showDialog || document.IsTemporary)
            {
                string filename;
                if (Dialogs.ShowSaveFileDialog(out filename, document.Title) == false)
                {
                    return false;
                }

                if (document.IsTemporary)
                {
                    await this.documentManager.SaveDocumentFromTempAsync(document, filename);
                    return true;
                }

                document.DocumentPath = filename;
                document.Title = new FileInfo(document.DocumentPath).Name;
            }

            await this.documentManager.SaveDocumentAsync(document);
            return true;
        }

        public void MoveToPreviousDocument()
        {
            var index = this.Documents.IndexOf(this.CurrentDocument);

            if (index == 0)
            {
                index = this.Documents.Count - 1;
            }
            else
            {
                index -= 1;
            }

            this.CurrentDocument = this.Documents[index];
        }

        public void MoveToNextDocument()
        {
            var index = this.Documents.IndexOf(this.CurrentDocument);

            if (index == this.Documents.Count - 1)
            {
                index = 0;
            }
            else
            {
                index += 1;
            }

            this.CurrentDocument = this.Documents[index];
        }

        public async Task<bool> CloseDocumentAsync(MarkdownDocument document, bool removeFromCollection = true)
        {
            if (document.HasChanges)
            {
                var result = Dialogs.ShowSaveChangesDialog(document.Title);
                if (result == null)
                {
                    return false;
                }

                if (result == true)
                {
                    var isSaved = await this.SaveDocumentAsync(document);
                    if (!isSaved)
                    {
                        return false;
                    }
                }
            }

            var index = this.Documents.IndexOf(document);

            this.documentManager.CloseDocument(document);
            index = index == this.Documents.Count - 1 ? index - 1 : index + 1;

            // If the closing document is the only one in the list, we need to add a new instance
            if (this.Documents.Count == 1)
            {
                this.NewDocument();
                index = 0;
            }

            if (removeFromCollection)
            {
                this.CurrentDocument = this.Documents[index];
                this.Documents.Remove(document);
            }

            return true;
        }

        public void ExitApp()
        {
            while (this.Documents.Any())
            {
                var document = this.Documents.First();
                if (document.HasChanges)
                {
                    var result = Dialogs.ShowSaveChangesDialog(document.Title);
                    if (result == null)
                    {
                        return;
                    }

                    if (result == true)
                    {
                        this.SaveDocumentAsync(document);
                    }
                }

                var index = this.Documents.IndexOf(document);
                this.documentManager.CloseDocument(document);
                index = index == this.Documents.Count - 1 ? index - 1 : index + 1;

                if (index >= 0)
                {
                    this.CurrentDocument = this.Documents[index];
                }

                this.Documents.Remove(document);
            }

            Application.Current.Shutdown();
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

        private void OnCurrentDocumentMarkdownChanged(object sender, EventArgs e)
        {
            this.RaiseCurrentDocumentMarkdownChanged();
        }

        private void RaiseCurrentDocumentMarkdownChanged()
        {
            if (this.CurrentDocument == null)
            {
                return;
            }

            this.htmlPreviewer.Start(this.CurrentDocument.Markdown, this.CurrentDocument.WorkingFolder);
        }

        private void UpdateHtml(string html)
        {
            this.HtmlPreview = html;
        }
    }
}
