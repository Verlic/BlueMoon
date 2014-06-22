namespace BlueMoon.DocumentManager
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

    using BlueMoon.DocumentManager.Annotations;

    public class MarkdownDocument : INotifyPropertyChanged
    {
        private string title;

        private bool isTemporary;

        private string workingFolder;

        private bool hasChanges;

        private string documentPath;

        private string markdown;

        internal MarkdownDocument(string title)
        {
            this.Markdown = string.Empty;
            this.Title = title;
            this.IsTemporary = true;
            this.hasChanges = false;
            this.WorkingFolder = Path.Combine(Path.GetTempPath(), "Marker", Guid.NewGuid().ToString());
            this.DocumentPath = string.Format("{0}\\{1}", this.WorkingFolder, title);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string DocumentPath
        {
            get
            {
                return this.documentPath;
            }

            set
            {
                this.documentPath = value;
                this.OnPropertyChanged();
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsTemporary
        {
            get
            {
                return this.isTemporary;
            }

            set
            {
                this.isTemporary = value;
                this.OnPropertyChanged();
            }
        }

        public string WorkingFolder
        {
            get
            {
                return this.workingFolder;
            }

            set
            {
                this.workingFolder = value;
                this.OnPropertyChanged();
            }
        }

        public bool HasChanges
        {
            get
            {
                return this.hasChanges;
            }

            set
            {
                this.hasChanges = value;
                this.OnPropertyChanged();
            }
        }

        public string Markdown
        {
            get
            {
                return this.markdown;
            }

            set
            {
                this.HasChanges = this.markdown != value;
                this.markdown = value;
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
    }
}
