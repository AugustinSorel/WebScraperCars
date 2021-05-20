using HtmlAgilityPack;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebScraperCars.Models;

namespace WebScraperCars
{
    class LeParkingScraper
    {
        private readonly string url;
        private HtmlNode productListItem;

        public int RangeMin { get; }
        public int RangeMax { get; }

        public LeParkingScraper(string carName, int rangeMin, int rangeMax)
        {
            url = "https://www.leparking.fr/voiture-occasion/" + carName + ".html#!/voiture-occasion/" + carName.ToString() + ".html%3Fslider_prix%3D" + rangeMin.ToString() + "%7C" + rangeMax.ToString();
            RangeMin = rangeMin;
            RangeMax = rangeMax;
            //+ rangeMin.ToString() + "%7C" + rangeMax.ToString();
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
                    CarID = GetItemID(),
                    CarCountry = GetCountry(),
                    CarName = GetTitle(),
                    CarPrice = GetPrice(),
                    CarSite = "Le Parking",
                    URL = GetURL(),
                    CarImage = GetImage(),
                };

                int priceInt = GetPriceStringToInt(carModel);
                if (priceInt > RangeMax || priceInt < RangeMin)
                    continue;

                carModels.Add(carModel);
            }

            return carModels;
        }

        private int GetPriceStringToInt(CarModel carModel)
        {
            string priceString = string.Empty;
            foreach (char item in carModel.CarPrice)
            {
                if (char.IsDigit(item))
                    priceString += item;
            }

            return int.Parse(priceString);
        }

        private string GetURL()
        {
            string x = productListItem.Descendants("a").Where(node => node.GetAttributeValue("class", "").Equals("external btn-plus no-partenaire-btn")).FirstOrDefault().GetAttributeValue("href", "");
            string url = "https://www.leparking.fr" + x;
            return url;
        }

        private string GetImage()
        {
            string imageString = productListItem.Descendants("img").FirstOrDefault().GetAttributeValue("src", "").Trim();

            if (imageString.Length < 40)
                return "Assets/ImageNotAvailable.png";

            return imageString;
        }

        private string GetItemID()
        {
            return productListItem.GetAttributeValue("tref", "").Trim();
        }

        private string GetTitle()
        {
            return productListItem.Descendants("span").Where(node => node.GetAttributeValue("class", "").Equals("title-block brand")).FirstOrDefault().InnerText.Trim();
        }

        private string GetCountry()
        {
            return productListItem.Descendants("span").Where(node => node.GetAttributeValue("Class", "").Equals("upper")).FirstOrDefault().InnerText.Trim();
        }

        private string GetPrice()
        {
            var s = productListItem.Descendants("p").Where(node => node.GetAttributeValue("Class", "").Equals("prix")).FirstOrDefault().InnerText.Trim();
            return s;
        }

        private IEnumerable<HtmlNode> GetProductLists(List<HtmlNode> productHTML)
        {
            return productHTML[0].Descendants("li").Where(node => node.GetAttributeValue("class", "").Contains("clearfix")).ToList();
        }
    }
}