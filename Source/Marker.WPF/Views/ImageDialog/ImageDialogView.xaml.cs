namespace BlueMoon.UI.Views.ImageDialog
{
    using System.Drawing;
    using System.Windows.Forms;

    using BlueMoon.DocumentManager;
    using BlueMoon.UI.Components;

    /// <summary>
    /// Interaction logic for ImageDialogView.xaml
    /// </summary>
    public partial class ImageDialogView
    {
        private Image image;

        public ImageDialogView(MarkdownDocument document)
        {
            this.DataContext = new ImageDialogViewModel(document);
            this.InitializeComponent();
        }

        public Image Image
        {
            get
            {
                return this.image;
            }
        }

        public string FileName
        {
            get
            {
                return ((ImageDialogViewModel)this.DataContext).FileName;
            }
        }

        public string Destination
        {
            get
            {
                return ((ImageDialogViewModel)this.DataContext).Destination;
            }
        }

        public string AlternateText
        {
            get
            {
                return ((ImageDialogViewModel)this.DataContext).AltText;
            }
        }

        private void WindowLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.image = Clipboard.GetImage();
            this.ImagePreview.Source = this.image.ToWpfImage();
        }

        private void OkButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
