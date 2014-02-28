using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TaskMapApp
{
    /// <summary>
    /// Конвертер для координаты элемента на карте
    /// </summary>
    public class CoordConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var selectedPoint = value as MapPointViewModel;

            if (selectedPoint != null)
               return string.Format("{0},{1}", selectedPoint.Location.Latitude.ToString(culture), selectedPoint.Location.Longitude.ToString(culture));

            return "0,0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
