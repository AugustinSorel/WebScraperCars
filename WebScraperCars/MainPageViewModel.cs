using System.Collections.Generic;
using WebScraperCars.Models;

namespace WebScraperCars.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public List<Books> Books { get; set; }

        public MainPageViewModel()
        {
            Books = BookManager.GetBooks();
        }
    }
}
