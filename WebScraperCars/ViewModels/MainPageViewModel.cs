using System.Collections.ObjectModel;
using WebScraperCars.Models;
using Windows.UI.Xaml;

namespace WebScraperCars.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<CarModel> carModelsItemSource;
        public StartScrapingCommand ButtonScrapingCommand { get; set; }

        private bool isLeParkingChecked;
        private Visibility isVisible;

        private bool isExpanded1;
        private bool isExpanded2;

        private int rangeMin;
        private int rangeMax;

        #region Properties
        public int RangeMin
        {
            get { return rangeMin; }
            set 
            {
                rangeMin = value; 
                NotifyPropertyChanged("RangeMin"); 
            }
        }

        public int RangeMax
        {
            get { return rangeMax; }
            set 
            { 
                rangeMax = value;
                NotifyPropertyChanged("RangeMax");
            }
        }

        public bool IsExpanded1
        {
            get { return isExpanded1; }
            set 
            { 
                isExpanded1 = value; 
                NotifyPropertyChanged("IsExpanded1"); 
            }
        }

        public bool IsExpanded2
        {
            get { return isExpanded2; }
            set 
            { 
                isExpanded2 = value; 
                NotifyPropertyChanged("IsExpanded2"); 
            }
        }

        public Visibility IsVisible
        {
            get { return isVisible; }
            set 
            { 
                isVisible = value; 
                NotifyPropertyChanged("IsVisible"); 
            }
        }

        public ObservableCollection<CarModel> CarModelsItemSource
        {
            get { return carModelsItemSource; }
            set 
            { 
                carModelsItemSource = value;
                NotifyPropertyChanged("CarModelsItemSource");
            }
        }

        public bool IsLeParkingChecked
        {
            get { return isLeParkingChecked; }
            set 
            { 
                isLeParkingChecked = value;
                NotifyPropertyChanged("IsLeParkingChecked");
            }
        }
        #endregion

        public MainPageViewModel()
        {
            IsVisible = Visibility.Collapsed;
            IsExpanded1 = false;
            IsExpanded2 = false;
            RangeMax = 100000;
            RangeMin = 0;
            ButtonScrapingCommand = new StartScrapingCommand(StartScraping);
        }

        public void StartScraping(string carName)
        {
            if (UserSelectedAtLeastOneCheckBox())
                StartPopulatingTheListView(carName);
        }

        public bool UserSelectedAtLeastOneCheckBox()
        {
            if (isLeParkingChecked)
                return true;

            return false;
        }

        private async void StartPopulatingTheListView(string carName)
        {
            IsVisible = Visibility.Visible;
            IsExpanded1 = false;
            IsExpanded2 = false;

            ObservableCollection<CarModel> cars = new ObservableCollection<CarModel>();

            if (isLeParkingChecked)
            {
                LeParkingScraper leParkingScraper = new LeParkingScraper(carName, rangeMin, rangeMax);
                foreach (var item in await leParkingScraper.GetCars())
                    cars.Add(item);
            }

            //if (isLeParkingChecked)
            //{
            //    LeParkingScraper leParkingScraper = new LeParkingScraper("Renault");
            //    foreach (var item in await leParkingScraper.GetCars())
            //        cars.Add(item);
            //}

            CarModelsItemSource = cars;

            IsVisible = Visibility.Collapsed;
        }
    }
}