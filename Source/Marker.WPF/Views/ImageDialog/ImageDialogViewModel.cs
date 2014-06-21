namespace BlueMoon.UI.Views.ImageDialog
{
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Windows.Media;

    using BlueMoon.DocumentManager;
    using BlueMoon.UI.Annotations;
    using BlueMoon.UI.Components;

    public class ImageDialogViewModel : INotifyPropertyChanged
    {
        private readonly MarkdownDocument document;

        private string fileName;

        private string destination;

        private string altText;

        private ImageSource imageSouce;

        public ImageDialogViewModel(MarkdownDocument document)
        {
            this.document = document;
            this.FileName = string.Format("Image");
            this.AltText = this.FileName;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string FileName
        {
            get
            {
                return this.fileName;
            }

            set
            {
                this.fileName = value;
                this.Destination = Path.Combine(this.document.WorkingFolder, "Images", this.fileName + ".png");
                this.OnPropertyChanged();
            }
        }

        public string Destination
        {
            get
            {
                return this.destination;
            }

            set
            {
                this.destination = value;
                this.OnPropertyChanged();
            }
        }

        public string AltText
        {
            get
            {
                return this.altText;
            }

            set
            {
                this.altText = value;
                this.OnPropertyChanged();
            }
        }

        public ImageSource ImageSouce
        {
            get
            {
                return this.imageSouce;
            }

            set
            {
                this.imageSouce = value;
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
