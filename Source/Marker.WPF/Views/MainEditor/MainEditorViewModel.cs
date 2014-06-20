namespace BlueMoon.UI.Views.MainEditor
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using BlueMoon.DocumentManager;
    using BlueMoon.UI.Annotations;

    public class MainEditorViewModel : INotifyPropertyChanged
    {
        private MarkdownDocument selectedDocument;

        public MainEditorViewModel()
        {
            this.Documents = new ObservableCollection<MarkdownDocument>
                                 {
                                     DocumentManager.StartNewDocument(),
                                     DocumentManager.StartNewDocument()
                                 };
            this.SelectedDocument = this.Documents.First();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<MarkdownDocument> Documents { get; set; }

        public MarkdownDocument SelectedDocument
        {
            get
            {
                return this.selectedDocument;
            }

            set
            {
                this.selectedDocument = value;
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
