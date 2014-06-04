namespace Marker.WPF
{
    using System.Drawing;
    using System.Windows.Media;

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

        public Scintilla MarkdownSource
        {
            get
            {
                return (Scintilla)this.HostControl.Child;
            }
        }

        private void WindowLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ConfigureScintilla();

            this.UpdateHtmlPreview();
        }

        private void ConfigureScintilla()
        {
            var scintilla = new Scintilla();
            this.HostControl.Child = scintilla;
            scintilla.Font = new Font("Consolas", 12);
            scintilla.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            scintilla.ForeColor = System.Drawing.Color.White;
            scintilla.Caret.Color = System.Drawing.Color.White;
            scintilla.LineWrapping.Mode = LineWrappingMode.Word;
            scintilla.IsBraceMatching = true;
            scintilla.Indentation.ShowGuides = true;
            scintilla.Indentation.SmartIndentType = SmartIndent.CPP2;
            scintilla.Indentation.TabIndents = true;
            scintilla.EndOfLine.Mode = EndOfLineMode.Crlf;
            scintilla.Encoding = System.Text.Encoding.UTF8;
            scintilla.TextChanged += this.ScintillaTextChanged;
            scintilla.TextInserted += this.ScintillaTextInserted;
            MarkdownLexer.Init(scintilla);
        }

        private void ScintillaTextInserted(object sender, TextModifiedEventArgs e)
        {
            if (e.LinesAddedCount > 0)
            {
                for (var i = 0; i <= e.LinesAddedCount; i++)
                {
                    var lineNumber = this.SourceControl.Lines.FromPosition(this.SourceControl.CurrentPos).Number + i;
                    var linePosition = this.SourceControl.Lines[lineNumber].StartPosition;
                    MarkdownLexer.StyleLine(this.SourceControl, linePosition);
                }
            }
            else
            {
                MarkdownLexer.StyleLine(this.SourceControl, e.Position);
            }
        }

        private void ScintillaTextChanged(object sender, System.EventArgs e)
        {
            this.UpdateHtmlPreview();
        }

        private void UpdateHtmlPreview()
        {
            var html = MarkdownConverter.ToHtml(this.MarkdownSource.Text);
            this.HtmlPreview.NavigateToString(html);
        }

        public Scintilla SourceControl
        {
            get
            {
                return (Scintilla)this.HostControl.Child;
            }
        }
    }
}
