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
        private readonly string url;
        private HtmlNode productListItem;

        public EbayScraper(string carName, int rangeMin, int rangeMax)
        {
            //url = "https://www.ebay.co.uk/sch/i.html?_from=R40&_trksid=p2334524.m570.l1313&_nkw=mgtf&_sacat=0&LH_TitleDesc=0&_udlo=1257&_udhi=2741&_osacat=0&_odkw=" + carName;
            url = "https://www.ebay.co.uk/sch/i.html?_from=R40&_trksid=p2334524.m570.l1313&_nkw=" + carName + "&_sacat=0&LH_TitleDesc=0&_udlo=" + rangeMin.ToString() + "&_udhi=" + rangeMax.ToString() + "&_osacat=0&_odkw=" + carName;

            //url = "https://www.ebay.co.uk/sch/i.html?_from=R40&_fosrp=1&_nkw=" + carName + "&_in_kw=1&_ex_kw=&_sacat=0&_mPrRngCbx=1&_udlo=" + rangeMin.ToString() + "&_udhi=" + rangeMax.ToString() + "&_ftrt=901&_ftrv=1&_sabdlo=&_sabdhi=&_samilow=&_samihi=&_sadis=15&_stpos=&_sargn=-1%26saslc%3D1&_salic=3&_sop=12&_dmd=1&_ipg=200";

        }

        internal async Task<ObservableCollection<CarModel>> GetCars()
        {
            ObservableCollection<CarModel> carModels = new ObservableCollection<CarModel>();
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(await new HttpClient().GetStringAsync(url));

            List<HtmlNode> productHTML = htmlDocument.DocumentNode.Descendants("ul").Where(node => node.GetAttributeValue("class", "").Equals("srp-results srp-list clearfix")).ToList();

            foreach (var productListItem in GetProductLists(productHTML))
            {
                this.productListItem = productListItem;
                CarModel carModel = new CarModel()
                {
                    CarID = GetItemID(),
                    //CarCountry = GetCountry(),
                    CarName = GetTitle(),
                    CarPrice = GetPrice(),
                    CarSite = "Ebay",
                    URL = GetURL(),
                    CarImage = GetItemImage(),
                };

                carModels.Add(carModel);
            }

            return carModels;
        }

        private string GetItemID()
        {
            return productListItem.GetAttributeValue("data-view", "").Trim();
        }

        private string GetItemImage()
        {
            return productListItem.Descendants("img").Where(node => node.GetAttributeValue("class", "").Equals("s-item__image-img")).FirstOrDefault().GetAttributeValue("src", "");
        }

        private string GetURL()
        {
            return productListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "").Trim();
        }

        private string GetTitle()
        {
            return productListItem.Descendants("h3").Where(node => node.GetAttributeValue("class", "").Equals("s-item__title")).FirstOrDefault().InnerText.Trim();
        }

        private string GetCountry()
        {
            return productListItem.Descendants("span").Where(node => node.GetAttributeValue("Class", "").Equals("s-item__location s-item__itemLocation")).FirstOrDefault().InnerText.Trim().Replace("from ", string.Empty);
        }

        private string GetPrice()
        {
            return productListItem.Descendants("span").Where(node => node.GetAttributeValue("Class", "").Equals("s-item__price")).FirstOrDefault().InnerText.Trim();
        }

        private IEnumerable<HtmlNode> GetProductLists(List<HtmlNode> productHTML)
        {
            return productHTML[0].Descendants("li").Where(node => node.GetAttributeValue("class", "").Contains("s-item        s-item--watch-at-corner  ")).ToList();
        }
    }
}