using System.Collections.ObjectModel;

namespace WebScraperCars.Models
{
    class CarModel
    {
        public int CarID { get; set; }
        public float CarPrice { get; set; }
        public string CarName { get; set; }
        public string CarCountry { get; set; }
        public string CarImage { get; set; }
        public string CarSite { get; set; }
        public string URL { get; set; }
    }

    class CarManager
    {
        public static ObservableCollection<CarModel> GetCars()
        {
            var cars = new ObservableCollection<CarModel>()
            {
                new CarModel(){CarID = 1212493493, CarCountry = "France", CarName = "Audi", CarPrice = 1000, CarImage = "Assets/audi-r8-.jpg", CarSite="Le Parking" },
                new CarModel(){CarID = 11323, CarCountry = "France", CarName = "BMW", CarPrice = 17200, CarImage = "Assets/BMW.jpg", CarSite="La Central" },
                new CarModel(){CarID = 12239289, CarCountry = "Angleterre", CarName = "Audi", CarPrice = 1023, CarImage = "Assets/audi-r8-.jpg", CarSite="La Central" },
                new CarModel(){CarID = 1829842, CarCountry = "Allemagne", CarName = "Renault", CarPrice = 7381, CarImage = "Assets/audi-r8-.jpg", CarSite="Le Bon Coin" },
                new CarModel(){CarID = 193613, CarCountry = "Portugale", CarName = "BMW", CarPrice = 190293, CarImage = "Assets/BMW.jpg", CarSite="Le Parking" },
                new CarModel(){CarID = 12111, CarCountry = "France", CarName = "Audi", CarPrice = 293, CarImage = "Assets/Audi2.jpg", CarSite="Le Bon Coin" },
            };

            return cars;
        }
    }
}
