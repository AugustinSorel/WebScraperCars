using System.Collections.ObjectModel;
using WebScraperCars.Models;

namespace WebScraperCars.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<CarModel> carModels;

        public ObservableCollection<CarModel> CarModels
        {
            get { return carModels; }
            set 
            { 
                carModels = value;
                NotifyPropertyChanged("CarModels");
            }
        }


        public MainPageViewModel()
        {
            CarModels = CarManager.GetCars();
        }
    }
}
