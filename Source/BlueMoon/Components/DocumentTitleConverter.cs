namespace BlueMoon.Components
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class DocumentTitleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue)
            {
                return string.Empty;
            }

            return string.Format("{0}{1}", values[0], (bool)values[1] ? "*" : string.Empty);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
