namespace BlueMoon.UI.Views.MainEditor
{
    using System.Windows;
    using System.Windows.Input;

    using BlueMoon.DocumentManager;
    using BlueMoon.UI.Components;
    using BlueMoon.UI.EditorCommands;

    using ScintillaNET;

    /// <summary>
    /// Interaction logic for EditorControl.xaml
    /// </summary>
    public partial class EditorControl
    {
        public static readonly DependencyProperty DocumentProperty = DependencyProperty.Register("Document", typeof(MarkdownDocument), typeof(EditorControl), null);

        private CommandBinder commandBinder;

        public EditorControl()
        {
            this.InitializeComponent();
        }

        // Using a DependencyProperty as the backing store for Document.  This enables animation, styling, binding, etc...

        public MainEditorViewModel ViewModel
        {
            get
            {
                return (MainEditorViewModel)this.DataContext;
            }
        }

        public Scintilla MarkdownEditor
        {
            get
            {
                return (Scintilla)this.HostControl.Child;
            }
        }

        public MarkdownDocument Document
        {
            get { return (MarkdownDocument)GetValue(DocumentProperty); }
            set { SetValue(DocumentProperty, value); }
        }

        private void UserControlLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ScintillaConfig.ConfigureScintilla(this.HostControl);
            ScintillaConfig.AddHandlers(this.MarkdownEditor, this.ScintillaTextInserted, this.ScintillaTextChanged);
            this.commandBinder = new CommandBinder(this.MarkdownEditor);
            var viewModel = new EditorControlViewModel(this.MarkdownEditor, this.Document);
            this.DataContext = viewModel;
            var inputBinding = new InputBinding(viewModel.PasteCommand, new KeyGesture(Key.V, ModifierKeys.Control));
            this.InputBindings.Add(inputBinding);
            viewModel.UpdateCommandStatus();
        }

        private void ScintillaTextInserted(object sender, TextModifiedEventArgs e)
        {
            MarkdownSyntaxHighlighter.Highlight(this.MarkdownEditor, e.Position, e.LinesAddedCount);
        }

        private void ScintillaTextChanged(object sender, System.EventArgs e)
        {

        }
    }
}
