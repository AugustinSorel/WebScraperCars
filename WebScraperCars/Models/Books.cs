using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraperCars.Models
{
    public class Books
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string CoverImage { get; set; }
    }

    public class BookManager
    {
        public static List<Books> GetBooks()
        {
            var books = new List<Books>()
            {
                new Books() { BookID = 1, Title = "Vulpate", Author = "Futur", CoverImage = "Assets/3DCameraOrbit_16x.png"},
                new Books() { BookID = 2, Title = "Mazil", Author = "Mak", CoverImage = "Assets/Accessibility_16x.png"},
                new Books() { BookID = 3, Title = "HelloTitle", Author = "Aemrj", CoverImage = "Assets/audi-r8-.jpg"},
                new Books() { BookID = 4, Title = "Per Mode", Author = "Mark", CoverImage = "Assets/AddDimension_16x.png"},
                new Books() { BookID = 5, Title = "Per Modo", Author = "Johgn", CoverImage = "Assets/3DCameraOrbit_16x.png"},
                new Books() { BookID = 6, Title = "Erat", Author = "Lenf", CoverImage = "Assets/audi-r8-.jpg"},
                new Books() { BookID = 7, Title = "Aliquio", Author = "Maidh", CoverImage = "Assets/AddBuildToQueue_16x.png"},
            };

            return books;
        }
    }
}
