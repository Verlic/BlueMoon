namespace Marker.WPF
{
    using Converters.Markdown;

    using ScintillaNET;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var scintilla = new Scintilla();
            this.HostControl.Child = scintilla;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var converter = new MarkdownConverter();

            var result = converter.ToHtml(this.SourceContent.Text);
        }

        public Scintilla SourceContent
        {
            get
            {
                return (Scintilla)this.HostControl.Child;
            }
        }
    }
}
