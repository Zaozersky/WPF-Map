using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TaskMapApp
{
    /// <summary>
    /// Конвертер для вычисления радиуса элемента на карте
    /// </summary>
    public class RadiusConverter : IMultiValueConverter
    {
        /// <summary>
        /// Ground Resolution (meters / pixel)
        /// http://msdn.microsoft.com/en-us/library/bb259689.aspx
        /// </summary>
        static Dictionary<int, double> mapResolution = new Dictionary<int, double>();

        static RadiusConverter() 
        {
            mapResolution.Add(1, 78271.5170);
            mapResolution.Add(2, 39135.7585);
            mapResolution.Add(3, 19567.8792);
            mapResolution.Add(4, 9783.9396);

            mapResolution.Add(5, 4891.9698);
            mapResolution.Add(6, 2445.9849);
            mapResolution.Add(7, 1222.9925);
            mapResolution.Add(8, 611.4962);

            mapResolution.Add(9, 305.7481);
            mapResolution.Add(10, 152.8741);
            mapResolution.Add(11, 76.4370);
            mapResolution.Add(12, 38.2185);

            mapResolution.Add(13, 19.1093);
            mapResolution.Add(14, 9.5546);
            mapResolution.Add(15, 4.7773);
            mapResolution.Add(16, 2.3887);

            mapResolution.Add(17, 1.1943);
            mapResolution.Add(18, 0.5972);
            mapResolution.Add(19, 0.2986);

            mapResolution.Add(20, 0.1493);
            mapResolution.Add(21, 0.0746);
            mapResolution.Add(22, 0.0373);
            mapResolution.Add(23, 0.0187);
        }

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {            
            if (values != null && values.Length == 3)
            {
                int radius = (int)values[0];
                int count = (int)values[1];
                int zoom = (int)values[2];

                if (mapResolution.ContainsKey(zoom))
                    return (double)(2*radius*count/mapResolution[zoom]); // width = height = 2*R
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
