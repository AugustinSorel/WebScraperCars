using HtmlAgilityPack;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebScraperCars.Models;

namespace WebScraperCars.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<CarModel> carModelsItemSource;
        public StartScrapingCommand ButtonScrapingCommand { get; set; }

        private bool isLeParkingChecked;

        #region Properties
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
            ButtonScrapingCommand = new StartScrapingCommand(StartScraping);
        }

        public async void StartScraping(object carName)
        {
            if (UserSelectedAtLeastOneCheckBox())
                CarModelsItemSource = await GetCars(carName as string);
        }

        public bool UserSelectedAtLeastOneCheckBox()
        {
            if (isLeParkingChecked)
                return true;

            return false;
        }

        #region Le Parking Scraping
        private async Task<ObservableCollection<CarModel>> GetCars(string v)
        {
            var url = "https://www.leparking.fr/voiture-occasion/" + v + ".html";
            ObservableCollection<CarModel> carModels = new ObservableCollection<CarModel>();
            var htmlDocument = new HtmlAgilityPack.HtmlDocument();

            htmlDocument.LoadHtml(await new HttpClient().GetStringAsync(url));

            var productHTML = htmlDocument.DocumentNode.Descendants("ul").Where(node => node.GetAttributeValue("class", "").Equals("resultat")).ToList();

            foreach (HtmlNode productListItem in GetProductLists(productHTML))
            {
                carModels.Add(new CarModel()
                {
                    CarID = GetItemID(productListItem),
                    CarCountry = GetCountry(productListItem),
                    CarName = GetTitle(productListItem),
                    CarPrice = GetPrice(productListItem),
                    CarSite = "Le Parking",
                    URL = "NO",
                    CarImage = GetImage(productListItem),
                });
            }

            return carModels;
        }

        private string GetImage(HtmlNode productListItem)
        {
            string imageString = productListItem.Descendants("img").FirstOrDefault().GetAttributeValue("src", "").Trim();

            if (imageString.Length < 40)
                return "Assets/ImageNotAvailable.png";

            return imageString;
        }

        private string GetItemID(HtmlNode productListItem)
        {
            return productListItem.GetAttributeValue("tref", "").Trim();
        }

        private string GetTitle(HtmlNode productListItem)
        {
            return productListItem.Descendants("span").Where(node => node.GetAttributeValue("class", "").Equals("title-block brand")).FirstOrDefault().InnerText.Trim();
        }

        private string GetCountry(HtmlNode productListItem)
        {
            return productListItem.Descendants("span").Where(node => node.GetAttributeValue("Class", "").Equals("upper")).FirstOrDefault().InnerText.Trim();
        }

        private string GetPrice(HtmlNode productListItem)
        {
            return productListItem.Descendants("p").Where(node => node.GetAttributeValue("Class", "").Equals("prix")).FirstOrDefault().InnerText.Trim();
        }

        private IEnumerable<HtmlNode> GetProductLists(List<HtmlNode> productHTML)
        {
            return productHTML[0].Descendants("li").Where(node => node.GetAttributeValue("class", "").Contains("clearfix")).ToList();
        }
        #endregion
    }
}
