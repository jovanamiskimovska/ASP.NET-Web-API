using SEDC.BooksApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.BooksApp
{
    public static class StaticDb
    {
        public static List<Book> Books = new List<Book>
        {
            new Book(){ Author="Leo Tolstoy", Title="Anna Karenina"},
            new Book(){ Author="Harper Lee", Title="To kill a mockingbird"},
            new Book(){ Author="F. Scott Fitzgerald", Title="The Great Gatsby"},
            new Book(){ Author="J.R.R. Tolkien", Title="Lord of the Rings" },
            new Book(){ Author="Agatha Christie", Title="And Then There Were None" }
        };
    }
}
