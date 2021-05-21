using HtmlAgilityPack;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebScraperCars.Models;

namespace WebScraperCars.ViewModels
{
    class EbayScraper
    {
        private int rangeMin;
        private int rangeMax;
        private readonly string url;
        private HtmlNode productListItem;

        public EbayScraper(string carName, int rangeMin, int rangeMax)
        {
            url = "https://www.leparking.fr/voiture-occasion/" + carName + ".html#!/voiture-occasion/" + carName.ToString() + ".html%3Fslider_prix%3D" + rangeMin.ToString() + "%7C" + rangeMax.ToString();
            this.rangeMin = rangeMin;
            this.rangeMax = rangeMax;
        }

        internal async Task<ObservableCollection<CarModel>> GetCars()
        {
            ObservableCollection<CarModel> carModels = new ObservableCollection<CarModel>();
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(await new HttpClient().GetStringAsync(url));

            List<HtmlNode> productHTML = htmlDocument.DocumentNode.Descendants("ul").Where(node => node.GetAttributeValue("class", "").Equals("resultat")).ToList();

            foreach (var productListItem in GetProductLists(productHTML))
            {
                this.productListItem = productListItem;

                CarModel carModel = new CarModel()
                {
                    //CarID = GetItemID(),
                    //CarCountry = GetCountry(),
                    //CarName = GetTitle(),
                    //CarPrice = GetPrice(),
                    //CarSite = "Le Parking",
                    //URL = GetURL(),
                    //CarImage = GetImage(),
                };

                //int priceInt = GetPriceStringToInt(carModel);
                //if (priceInt > RangeMax || priceInt < RangeMin)
                //    continue;

                carModels.Add(carModel);
            }

            return carModels;
        }
        
        private IEnumerable<HtmlNode> GetProductLists(List<HtmlNode> productHTML)
        {
            return productHTML[0].Descendants("li").Where(node => node.GetAttributeValue("class", "").Contains("clearfix")).ToList();
        }
    }
}