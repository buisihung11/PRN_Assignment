using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyBookLibary
{

    public class Book
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public float Price { get; set; }

        public Book(string iD, string name, string publisher, float price)
        {
            ID = iD;
            Name = name;
            Publisher = publisher;
            Price = price;
        }
    }





    public class BookLibary
    {



        const int MAX_NUMBER = 1;
        static List<Book> books = new List<Book>();


        public void PrintAll()
        {
            if(books.Count == 0)
                Console.WriteLine("No book in DB");
            else
                foreach(var book in books)
                    Console.WriteLine($"ID: {book.ID}, Name: {book.Name}, Price: {book.Price}");
        }

        public Book FindBookByID(string id)
        {
            foreach (var book in books)
                if (book.ID == id)
                    return book;
            return null;
        }

        public bool Add(string iD, string name, string publisher, float price)
        {
            if (FindBookByID(iD) != null)
                return false;
            books.Add(new Book( iD,  name,  publisher, price));
            return true;
        }

        public bool Update(string iD, string name, string publisher, float price)
        {
            Book book = FindBookByID(iD);

            if (book == null)
                return false;
            book.Name = name;
            book.Publisher = publisher;
            book.Price = price;
            return true;
        }

        public bool Delete(string iD)
        {
            Book book = FindBookByID(iD);
            if (book == null)
                return false;
            books.Remove(book);
            return true;
        }



    }
}
