namespace BlueMoon.UI.Components
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class DocumentTitleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format("{0}{1}", values[0], (bool)values[1] ? "*" : string.Empty);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
