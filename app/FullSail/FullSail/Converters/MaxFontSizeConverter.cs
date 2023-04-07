using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSail.Converters;
public class MaxFontSizeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double parentHeight)
        {
            const double fontSizeRatio = 0.5;
            const double minFontSize = 10;

            double fontSize = parentHeight * fontSizeRatio;
            fontSize = Math.Max(minFontSize, fontSize);

            return fontSize;
        }

        return Binding.DoNothing;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
