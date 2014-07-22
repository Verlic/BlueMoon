namespace Caladan.Addin.Views.WordPress
{
    using System.Windows.Forms;

    public partial class WordPressPublishDialog : Form
    {
        public WordPressPublishDialog()
        {
            this.InitializeComponent();
            this.Settings = new WordPressSettings();
        }

        public WordPressSettings Settings { get; set; }

        private void OkButtonClick(object sender, System.EventArgs e)
        {
            this.Settings.SiteUrl = this.siteUrlText.Text;
            this.Settings.PostTitle = this.postTitleText.Text;
            this.Settings.AsDraft = this.draftCheckbox.Checked;
            this.Settings.Tags = this.tagsText.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
