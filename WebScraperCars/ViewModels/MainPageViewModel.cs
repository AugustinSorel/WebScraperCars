using System.Collections.ObjectModel;
using WebScraperCars.Models;

namespace WebScraperCars.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<CarModel> carModelsItemSource;
        
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
            CarModelsItemSource = CarManager.GetCars();
        }
    }
}
