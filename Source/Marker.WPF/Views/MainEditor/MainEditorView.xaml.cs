namespace BlueMoon.UI.Views.MainEditor
{
    using BlueMoon.DocumentManager;

    using ICSharpCode.AvalonEdit;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainEditorView
    {
        public MainEditorView()
        {
            this.InitializeComponent();
            MarkdownApp.Current.NewDocument();
            this.DocumentTabs.DataContext = MarkdownApp.Current;
            this.DataContext = new EditorViewModel();
        }

        private void EditorWindowLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var editCommandBindings = this.MarkdownEditor.TextArea.DefaultInputHandler.Editing.CommandBindings;
            foreach (var binding in editCommandBindings)
            {
                if (binding.Command == AvalonEditCommands.IndentSelection)
                {
                    editCommandBindings.Remove(binding);
                    break;
                }
            }
        }
    }
}
