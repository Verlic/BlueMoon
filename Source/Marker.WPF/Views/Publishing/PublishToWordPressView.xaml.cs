namespace BlueMoon.UI.Views.Publishing
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for PublishToWordPressView.xaml
    /// </summary>
    public partial class PublishToWordPressView
    {
        public PublishToWordPressView()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public string Username { get; set; }

        public string Password
        {
            get
            {
                return this.PasswordText.Password;
            }
        }

        public string PostTitle { get; set; }

        public string Site { get; set; }

        public bool IsDraft { get; set; }

        private void PublishButtonClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
