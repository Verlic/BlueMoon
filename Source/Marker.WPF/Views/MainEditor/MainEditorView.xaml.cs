namespace BlueMoon.UI.Views.MainEditor
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using BlueMoon.DocumentManager;
    using BlueMoon.UI.Components;
    using BlueMoon.UI.EditorCommands;

    using ScintillaNET;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainEditorView
    {
        private CommandBinder commandBinder;

        public MainEditorView()
        {
            this.InitializeComponent();
        }

        public EditorControlViewModel DocumentViewModel
        {
            get
            {
                return this.DataContext as EditorControlViewModel;
            }

            set
            {
                this.DataContext = value;
            }
        }

        private Scintilla MarkdownEditor
        {
            get
            {
                return (Scintilla)this.HostControl.Child;
            }
        }

        private void WindowLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var documents = new ObservableCollection<MarkdownDocument>
                                 {
                                     DocumentManager.StartNewDocument(),
                                     DocumentManager.StartNewDocument()
                                 };

            ScintillaConfig.ConfigureScintilla(this.HostControl);
            ScintillaConfig.AddHandlers(this.MarkdownEditor, this.ScintillaTextInserted, this.ScintillaTextChanged);
            this.commandBinder = new CommandBinder(this.MarkdownEditor);
            this.DocumentViewModel = new EditorControlViewModel(this.MarkdownEditor, documents[0])
                                         {
                                             Documents = documents
                                         };
            
            this.DocumentTabs.ItemsSource = this.DocumentViewModel.Documents;
            this.DocumentTabs.SelectedItem = this.DocumentViewModel.Documents[0];
            this.AddInputBinding(this.DocumentViewModel.PasteCommand, Key.V, ModifierKeys.Control);
            this.AddInputBinding(this.DocumentViewModel.NewCommand, Key.N, ModifierKeys.Control);
            this.AddInputBinding(this.DocumentViewModel.CloseTabCommand, Key.W, ModifierKeys.Control);
            this.AddInputBinding(this.DocumentViewModel.SwitchTabCommand, Key.Tab, ModifierKeys.Control);
            this.DocumentViewModel.UpdateCommandStatus();
        }

        private void DocumentTabsSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                this.LoadDocumentViewModel((MarkdownDocument)e.AddedItems[0]);
            }
        }

        private void ScintillaTextInserted(object sender, TextModifiedEventArgs e)
        {
            MarkdownSyntaxHighlighter.Highlight(this.MarkdownEditor, e.Position, e.LinesAddedCount);
        }

        private void ScintillaTextChanged(object sender, System.EventArgs e)
        {
        }

        private void LoadDocumentViewModel(MarkdownDocument document)
        {
            this.DocumentViewModel.Document = document;
        }

        private void AddInputBinding(ICommand command, Key key, ModifierKeys modifier)
        {
            var inputBinding = new InputBinding(command, new KeyGesture(key, modifier))
                                   {
                                       CommandParameter = this.DocumentViewModel
                                   };

            this.InputBindings.Add(inputBinding);
        }
    }
}
