using DevExpress.Xpf.Map;
using DevExpress.Xpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using TaskMapApp.Properties;

namespace TaskMapApp
{
    /// <summary>
    /// View-Model для Main Window
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MapPointViewModel> _placeList;
        private ObservableCollection<MapCustomElement> _mapElements;
        private ObservableCollection<MapPoint> _mapPointsInfo;

        private Dictionary<string, int> _mapNameToCount;
        private Dictionary<string, MapPointViewModel> _mapNameToMapPoint;
        private Dictionary<string, MapCustomElement> _mapNameToMapElement;

        private MapPointViewModel _selectedPlace;
        private MapCustomElement _selectedMapElement;

        private int _zoom = 3; // начальный zoom

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="template">Шаблон для элемента на карте</param>
        public MainViewModel(DataTemplate template)
        {
            _mapPointsInfo = new ObservableCollection<MapPoint>();
            _mapNameToCount = new Dictionary<string, int>();
            _mapNameToMapPoint = new Dictionary<string, MapPointViewModel>();
            _mapNameToMapElement = new Dictionary<string, MapCustomElement>();

            _mapElements = new ObservableCollection<MapCustomElement>();
            _placeList = new ObservableCollection<MapPointViewModel>();

            LoadData();

            Init(template);
        }

        public ObservableCollection<MapPointViewModel> PlaceList
        {
            get { return _placeList; }
            set
            {
                _placeList = value;
                OnPropertyChanged("PlaceList");
            }
        }

        public ObservableCollection<MapCustomElement> MapElements
        {
            get { return _mapElements; }
            set
            {
                _mapElements = value;
                OnPropertyChanged("MapElements");
            }
        }

        public MapCustomElement SelectedMapElement
        {
            get { return _selectedMapElement; }
            set
            {
                _selectedMapElement = value;
                OnPropertyChanged("SelectedMapPlace");
            }
        }

        public MapPointViewModel SelectedPlace
        {
             get { return _selectedPlace; }
            set
            {
                _selectedPlace = value;
                OnPropertyChanged("SelectedPlace");
                UpdateSelectedMapElement();                  
            }
        }

        public int? Zoom
        {
            get { return _zoom; }
            set 
            {
                _zoom = (int)value;
                OnPropertyChanged("Zoom");
                UpdateMapElementsZoom();                
            }
        }

        public string Key
        {
            get { return "Arj3FFIpFAcjhkPlE_8cUkNQkdpldhBtdmbFW_-oqmwnUVhAhqwUuTUIW-7o2efR"; }
        }

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void Init(DataTemplate template)
        {
            Random random = new Random(DateTime.Now.Millisecond);

            foreach (var info in _mapPointsInfo)
            {
                var mapPointViewModel = new MapPointViewModel(info) { Count = _mapNameToCount[info.Name]};
                mapPointViewModel.Zoom = Zoom;
                mapPointViewModel.MapPointColor = new SolidColorBrush(Color.FromRgb(
                                                                                    (byte)random.Next(1, 255),
                                                                                    (byte)random.Next(1, 255),
                                                                                    (byte)random.Next(1, 255)));
                PlaceList.Add(mapPointViewModel);
                _mapNameToMapPoint.Add(mapPointViewModel.Name, mapPointViewModel);

                var mapCustomElement = new MapCustomElement()
                {
                    Content = mapPointViewModel,
                    ContentTemplate = template,
                };                          

                _mapNameToMapElement.Add(info.Name, mapCustomElement);

                mapCustomElement.MouseLeftButtonDown += OnMapCustomElementClick;

                BindingOperations.SetBinding(mapCustomElement, MapCustomElement.LocationProperty,
                    new Binding("Location") { Source = mapPointViewModel });

                MapElements.Add(mapCustomElement);               
            }
        }

        private void OnMapCustomElementClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedElement = sender as MapCustomElement;

            if (selectedElement != null)
            {
                SetFocusOnSelectedElement(selectedElement);

                var info = selectedElement.Content as MapPointViewModel;
                if (info != null)
                    SelectedPlace = _mapNameToMapPoint[info.Name];
            }
        }

        private void UpdateMapElementsZoom()
        {
            foreach (var element in MapElements)
            {
                var point = (MapPointViewModel)element.Content;
                point.Zoom = Zoom;
            }
        }

        private void UpdateSelectedMapElement()
        {
            SelectedMapElement = _mapNameToMapElement[SelectedPlace.Name];
            SetFocusOnSelectedElement(SelectedMapElement);

            Zoom = 15; // сфокусировать на выбранном месте          
        }

        private void SetFocusOnSelectedElement(MapCustomElement element)
        {
            var pointViewModel = element.Content as MapPointViewModel;
            foreach (var place in PlaceList)
                place.IsSelected = false;

            pointViewModel.IsSelected = true;
        }

        private void LoadData()
        {
            using (var textReader = new StreamReader("Resources/in.csv"))
            {
                string[] sep = { "\r\n", ";" };
                textReader.ReadLine();

                string[] data = textReader.ReadToEnd().Split(sep, StringSplitOptions.None);

                double lat, lng;
                int k = 0;

                CultureInfo culture = CultureInfo.InvariantCulture;                

                // парсим данные, формат : МЕСТО;ШИРОТА;ДОЛГОТА;ОПИСАНИЕ
                while (k <= data.Length - 4)
                {
                    lat = Double.Parse(data[k + 1], culture);
                    lng = Double.Parse(data[k + 2], culture);

                    GeoPoint geoPoint = new GeoPoint(lat, lng);
                    MapPoint point = new MapPoint(data[k], geoPoint, data[k + 3]);   

                    if (!_mapNameToCount.ContainsKey(data[k]))
                    {
                        _mapNameToCount.Add(data[k], 1);
                        _mapPointsInfo.Add(point);
                    }
                    else _mapNameToCount[data[k]]++;

                    if ((k + 4) < data.Length && string.IsNullOrEmpty(data[k + 4]))
                        k++;

                    k += 4;
                }
            }
        }
    }
}
