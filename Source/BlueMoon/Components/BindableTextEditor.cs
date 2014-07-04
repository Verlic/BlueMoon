namespace BlueMoon.Components
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;

    using ICSharpCode.AvalonEdit;

    public class BindableTextEditor : TextEditor, INotifyPropertyChanged
    {
        public static readonly DependencyProperty MarkdownProperty = DependencyProperty.Register(
            "Markdown",
            typeof(string),
            typeof(BindableTextEditor),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnMarkdownChanged));

        public event PropertyChangedEventHandler PropertyChanged;

        public string Markdown
        {
            get
            {
                var markdown = (string)this.GetValue(MarkdownProperty);
                return markdown;
            }

            set
            {
                this.SetValue(MarkdownProperty, value);
                this.Text = value;
                this.RaisePropertyChanged("Markdown");
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            var expression = BindingOperations.GetBindingExpression(this, MarkdownProperty);
            if (expression != null)
            {
                this.SetValue(MarkdownProperty, this.Text);
            }

            base.OnTextChanged(e);
        }

        private static void OnMarkdownChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = d as BindableTextEditor;
            if (editor == null)
            {
                return;
            }

            if (editor.Text != e.NewValue.ToString())
            {
                editor.Text = e.NewValue.ToString();
            }
        }

        /// <summary>
        /// Raises a property changed event
        /// </summary>
        /// <param name="property">The name of the property that updates</param>
        private void RaisePropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
