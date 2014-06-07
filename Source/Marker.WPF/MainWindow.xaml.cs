namespace Marker.WPF
{
    using System.ComponentModel;
    using System.Timers;

    using Marker.WPF.Components;

    using ScintillaNET;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private string html;

        private BackgroundWorker worker;

        private Timer timer;

        private HtmlPreviewUpdater htmlPreviewUpdater;

        public MainWindow()
        {
            InitializeComponent();
        }

        public Scintilla MarkdownSource
        {
            get
            {
                return (Scintilla)this.HostControl.Child;
            }
        }

        private void WindowLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ScintillaConfig.ConfigureScintilla(this.HostControl);
            ScintillaConfig.AddHandlers(this.MarkdownEditor, this.ScintillaTextInserted, this.ScintillaTextChanged);
            
            this.htmlPreviewUpdater = new HtmlPreviewUpdater(this.MarkdownEditor, this.HtmlPreview);
            this.htmlPreviewUpdater.StartPreview();
        }

        private void ScintillaTextInserted(object sender, TextModifiedEventArgs e)
        {
            MarkdownSyntaxHighlighter.Highlight(this.MarkdownEditor, e.Position, e.LinesAddedCount);
        }

        private void ScintillaTextChanged(object sender, System.EventArgs e)
        {
            this.htmlPreviewUpdater.Update();
        }

        public Scintilla MarkdownEditor
        {
            get
            {
                return (Scintilla)this.HostControl.Child;
            }
        }
    }
}
