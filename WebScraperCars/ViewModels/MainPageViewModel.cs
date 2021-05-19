using System.Collections.ObjectModel;
using WebScraperCars.Models;

namespace WebScraperCars.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        public ObservableCollection<CarModel> CarModels { get; set; }
        
        public MainPageViewModel()
        {
            CarModels = CarManager.GetCars();
        }
    }
}
