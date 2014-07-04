namespace BlueMoon
{
    using System.Linq;
    using System.Windows.Input;

    using BlueMoon.DocumentManager;

    using ICSharpCode.AvalonEdit;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
            MarkdownApp.Current.NewDocument();
            this.DataContext = MarkdownApp.Current;
        }

        private void WindowLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var editCommandBindings = this.MarkdownEditor.TextArea.DefaultInputHandler.Editing.CommandBindings;
            var pasteBinding = this.MarkdownEditor.TextArea.DefaultInputHandler.Editing.CommandBindings.SingleOrDefault(command => command.Command is RoutedUICommand && ((RoutedUICommand)command.Command).Text == "Paste");
            if (pasteBinding != null)
            {
                this.MarkdownEditor.TextArea.DefaultInputHandler.Editing.CommandBindings.Remove(pasteBinding);
            }

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
