using DevExpress.Xpf.Map;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TaskMapApp
{
    /// <summary>
    /// ViewModel для элемента на карте
    /// </summary>
    public class MapPointViewModel : INotifyPropertyChanged
    {
        private MapPoint _mapPoint;
        private SolidColorBrush _mapPointColor;
        private int _count;
        private int? _zoom;
        private bool _isSelected;       

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="mapPoint">Модель данных</param>
        public MapPointViewModel(MapPoint mapPoint)
        {
            _mapPoint = mapPoint;
        }

        /// <summary>
        /// Название места
        /// </summary>
        public string Name 
        {
            get { return _mapPoint.Name; }
            set 
            {
                _mapPoint.Name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description 
        {
            get { return _mapPoint.Description; }
            set
            {
                _mapPoint.Description = value;
                OnPropertyChanged("Description");
            }
        }

        /// <summary>
        /// Текущая координата элемента на карте
        /// </summary>
        public GeoPoint Location 
        {
            get { return _mapPoint.Location; }
            set 
            {
                _mapPoint.Location = value;
                OnPropertyChanged("Location");
            }
        }
        
        /// <summary>
        /// Радиус окружности
        /// </summary>
        public int Radius
        {
            get { return CommonConstants.Radius; }            
        }

        /// <summary>
        /// Количество элементов с одинаковым названием
        /// </summary>
        public int Count
        {
            get { return _count; }
            set 
            {
                _count = value;
                OnPropertyChanged("Count");
            }
        }

        /// <summary>
        /// Цвет заливки элемента на карте
        /// </summary>
        public SolidColorBrush MapPointColor 
        {
            get { return _mapPointColor; }
            set
            {
                _mapPointColor = value;
                OnPropertyChanged("MapPointColor");
            }
        }
        
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        /// <summary>
        /// Текущий zoom
        /// </summary>
        public int? Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = value;
                OnPropertyChanged("Zoom");
            }
        }

        public override string ToString()
        {
            return _mapPoint.Name;
        }

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }     
    }
}
