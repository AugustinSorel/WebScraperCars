using System.Collections.ObjectModel;
using System.Diagnostics;
using WebScraperCars.Models;

namespace WebScraperCars.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<CarModel> carModelsItemSource;
        public StartScrapingCommand ButtonScrapingCommand { get; set; }

        public ObservableCollection<CarModel> CarModelsItemSource
        {
            get { return carModelsItemSource; }
            set 
            { 
                carModelsItemSource = value;
                NotifyPropertyChanged("CarModelsItemSource");
            }
        }

        public MainPageViewModel()
        {
            ButtonScrapingCommand = new StartScrapingCommand(StartScraping);
        }

        public void StartScraping(object carName)
        {
            CarModelsItemSource = CarManager.GetCars();
        }
    }
}
