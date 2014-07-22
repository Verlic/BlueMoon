namespace Caladan.Addin
{
    using System.Web;
    using System.Windows.Forms;

    using Caladan.Addin.Publishers;

    public partial class OAuthDialog : Form
    {
        private readonly WordPressPublisher publisher;

        public OAuthDialog(WordPressPublisher publisher)
        {
            this.publisher = publisher;
            this.InitializeComponent();
        }

        public string BearerCode { get; set; }

        private void OAuthDialogLoad(object sender, System.EventArgs e)
        {
            this.webBrowserControl.Navigate(this.publisher.TokenUri);
        }

        private void WebBrowserControlNavigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            var queryString = e.Url.Query;
            var queryVariables = HttpUtility.ParseQueryString(queryString);

            if (string.IsNullOrWhiteSpace(queryVariables.Get("returnToken")))
            {
                return;
            }

            this.BearerCode = queryVariables.Get("code");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void WebBrowserControlNavigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            
        }

        private void WebBrowserControlDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
