using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateEvent
{

    class Product
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public float UnitPrice { get; set; }
        public float SubTotal
        {
            get
            {
                return UnitPrice * Quantity;
            }
        }


    }


    class ProductManager
    {
        public delegate void DeleteWarning(string msg);
        public event DeleteWarning EventDeleteWarning;
        public ArrayList ProductList = new ArrayList();

        public ArrayList GetProductList
        {
            get
            {
                return ProductList;
            }
        }

        public Product Find(int id)
        {
            foreach(Product prod in ProductList)
            {
                if (prod.ProductID == id)
                    return prod;
            }
            return null;
        }

        public void Add(Product p)
        {
            ProductList.Add(p);
        }

        public void Remove(int id)
        {
            Product p = Find(id);
            if(p != null)
            {
                ProductList.Remove(p);
                //Raise evnet
                if(EventDeleteWarning != null)
                EventDeleteWarning("Product ID = " + p.ProductID + " is being removed");
            }
        }

    }

    class Program
    {

        static void PrintAllProduct(ArrayList al)
        {
            foreach(Product p in al)
            {
                Console.WriteLine("Product ID : " + p.ProductID);
                Console.WriteLine("Product Name : " + p.ProductName);
                Console.WriteLine("Product Quantity : " + p.Quantity);
                Console.WriteLine("Product Subtotal : " + p.SubTotal);
            }
        }

        static void HandleRemoveProductEvent(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Main(string[] args)
        {

            Product coffee = new Product
            {
                ProductID = 1, ProductName = "Coffee",
                Quantity = 10, UnitPrice = 40.1f
            };

            Product milk = new Product
            {
                ProductID = 2,
                ProductName = "Milk",
                Quantity = 12,
                UnitPrice = 31.1f
            };

            ProductManager pm = new ProductManager();
            pm.EventDeleteWarning += HandleRemoveProductEvent;

            pm.Add(coffee);
            pm.Add(milk);

            Console.WriteLine("======Danh sach mat hang =====");
            PrintAllProduct(pm.GetProductList);

            pm.Remove(1);

        }
    }
}
