namespace BlueMoon.UI.Views.MainEditor
{
    using BlueMoon.DocumentManager;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainEditorView
    {
        public MainEditorView()
        {
            this.InitializeComponent();
        }

        private void WindowLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DataContext = new MainEditorViewModel();
        }

        private void DocumentTabs_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            
        }

        private void DocumentTabs_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void DocumentTabs_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }
    }
}
