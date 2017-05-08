using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPFLocalizeExtension.Engine;

namespace Beverages.Converters
{
    class LanguageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter != null)
            {
                value = parameter;
            }
            
            return ConvertResources(value);
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static string ConvertResources(object value)
        {
            var resourceValue = Properties.Resources.ResourceManager.GetString(value as string);
            return resourceValue;
        }

    }
}
