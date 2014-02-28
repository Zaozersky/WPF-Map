using DevExpress.Xpf.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TaskMapApp
{
    /// <summary>
    /// Модель гео данных 
    /// </summary>
    public class MapPoint
    {      
        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="name">Идентификатор</param>
        /// <param name="location">Координаты (широта и долгота)</param>
        /// <param name="description">Описание места</param>
        public MapPoint(string name, GeoPoint location, string description = null)
        {
            Name = name;
            Location = location;
            Description = description;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Координаты (широта и долгота)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Описание места
        /// </summary>
        public GeoPoint Location { get; set; }

        public override string ToString()
        {
            return Name;
        }
    } 
   
}
