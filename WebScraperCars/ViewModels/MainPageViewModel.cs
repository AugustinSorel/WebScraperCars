using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebScraperCars.Models;

namespace WebScraperCars.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        public ObservableCollection<CarModel> carModels { get; set; }
        public List<Books> Books { get; set; }

        public MainPageViewModel()
        {
            Books = BookManager.GetBooks();
            carModels = CarManager.GetCars();
        }
    }
}
