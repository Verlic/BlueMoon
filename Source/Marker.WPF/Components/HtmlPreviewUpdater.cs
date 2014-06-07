namespace Marker.WPF.Components
{
    using System.IO;
    using System.Reflection;
    using System.Timers;
    using System.Windows.Controls;

    using ScintillaNET;

    public class HtmlPreviewUpdater
    {
        private readonly Scintilla markdownSource;

        private readonly WebBrowser htmlPreview;

        private HtmlToMarkdown htmlToMarkdown;

        private Timer timer;

        private bool update;

        public HtmlPreviewUpdater(Scintilla markdownSource, WebBrowser htmlPreview)
        {
            this.markdownSource = markdownSource;
            this.htmlPreview = htmlPreview;
            
            this.InitializeConverter();
        }

        public void Update()
        {
            if (!this.update)
            {
                this.update = true;
                this.timer.Enabled = true;
            }

        }

        public void StartPreview()
        {
            this.InitializeTimer();
        }

        private void InitializeConverter()
        {
            var templatePath = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName
                               + "\\Templates\\DefaultTheme\\content.tt";

            this.htmlToMarkdown = new HtmlToMarkdown(templatePath);
        }

        private void InitializeTimer()
        {
            this.timer = new Timer(500) { SynchronizingObject = this.markdownSource };
            this.timer.Elapsed += this.TimerElapsed;
            this.timer.Enabled = true;
        }

        private async void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (!this.update)
            {
                return;
            }

            this.timer.Enabled = false;
            this.htmlPreview.NavigateToString(await this.htmlToMarkdown.ToHtml(this.markdownSource.Text));
            this.update = false;
        }
    }
}
