using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assigment2
{
    static class GetInput
    {
        public static string GetString(string msg, string err)
        {
            string data = null;

            do
            {
                Console.Write(msg + ": ");
                data = Console.ReadLine();
                if (data.Trim().Length == 0)
                    Console.WriteLine(err);
            } while (data.Trim().Length == 0);

            return data;
        }

        public static DateTime GetDate(string msg, string err)
        {
            DateTime data = new DateTime();

            do
            {
                Console.Write(msg + ": ");
                try
                {
                    data = DateTime.Parse(Console.ReadLine());
                }
                catch (FormatException f)
                {
                    Console.WriteLine(err);
                    continue;
                }
                return data;
            } while (true);

        }

        public static string GetPhoneNumber(string msg, string err)
        {
            Regex rx = new Regex("^\\d{10,11}$");
            string data = null;

            do
            {
                Console.Write(msg + ": ");
                try
                {
                    data = Console.ReadLine();
                    //MatchCollection matches = rx.Matches(data);
                    if (!rx.IsMatch(data))
                    {
                        Console.WriteLine(err);
                        continue;
                    }
                }
                catch (FormatException f)
                {
                    Console.WriteLine(err);
                    continue;
                }
                return data;
            } while (true);

            return data;
        }


    }


    class Program
    {
        MyBookLibary.BookLibary BookLibary = new MyBookLibary.BookLibary();

        public void Menu()
        {
            int choice = -1;
            do
            {
                Console.WriteLine("=====Book Manager=====");
                Console.WriteLine("1. List all Book");
                Console.WriteLine("2. Add new book");
                Console.WriteLine("3. Update book");
                Console.WriteLine("4. Delete book");
                Console.WriteLine("5. Exit");
                Console.Write("Nhap: ");
                choice = Int32.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1: ShowAllBooks(); break;
                    case 2: AddBookMenu(); break;
                    case 3: UpdateBookMenu(); break;
                    case 4: RemoveBookMEnu(); break;
                    default: break;
                }

            } while (choice != 5);
        }
        #region MenuFunction
        private void AddBookMenu()
        {
            string id, name, publisher;
            float price;
            id = GetInput.GetString("Nhap ID", "Vui long nhap lai");
            //check student was in List
            if (BookLibary.FindBookByID(id) != null)
            {
                Console.WriteLine("That Book was exsits");
                return;
            }
            name = GetInput.GetString("Nhap ten sach", "Vui long nhap lai");
            publisher = GetInput.GetString("Nhap Publisher", "Vui long nhap lai");
            Console.Write("Nhap gia tien: ");
            price = float.Parse(Console.ReadLine());

            if (BookLibary.Add(id, name, publisher, price))
            {
                Console.WriteLine("Add successful");
            }
            else
            {
                Console.WriteLine("'Loi khi add, vui long thu lai");
            }

        }

        private void ShowAllBooks()
        {

            BookLibary.PrintAll();
        }


        private void UpdateBookMenu()
        {

            string id, name, publisher;
            float price;
            id = GetInput.GetString("Nhap id Can cap nhat: ", "Vui long nhap lai");

                if(BookLibary.FindBookByID(id) == null)
                    Console.WriteLine("No Book with that ID In DB");
                else
                {
                name = GetInput.GetString("Nhap Ten sach", "Vui long nhap lai");
                publisher = GetInput.GetString("Nhap Publisher", "Vui long nhap lai");
                Console.Write("Nhap gia tien: ");
                price = float.Parse(Console.ReadLine());
                    if(BookLibary.Update(id,name,publisher,price))
                    Console.WriteLine("Cap nhat thanh cong");
                    else
                        Console.WriteLine("Loi khi cap nhat");
                }
            
        }

        public void RemoveBookMEnu()
        {
            string id;
            id = GetInput.GetString("Nhap id Can cap nhat: ", "Vui long nhap lai");

            if (BookLibary.FindBookByID(id) == null)
                Console.WriteLine("No Book with that ID In DB");
            else
            {
                string confirm;
                Console.WriteLine("You want to delete yes/no");
                confirm = Console.ReadLine();
                if(confirm.ToLower().StartsWith("yes") 
                    || confirm.ToLower().StartsWith("y"))
                    if (BookLibary.Delete(id))
                    Console.WriteLine("Xoa thanh cong");
                else
                    Console.WriteLine("Loi khi xoa");
            }
        }

        #endregion


        static void Main(string[] args)
        {
            Program p = new Program();
            p.Menu();
        }
    }
}
