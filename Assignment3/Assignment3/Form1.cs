using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Configuration;
using ProductLibary;

namespace Assignment3
{
    public partial class Form1 : Form
    {

        private ProductDB productDb;

        public Form1()
        {
            InitializeComponent();
            productDb = new ProductDB(ConfigurationManager.ConnectionStrings["SaleDB"].ConnectionString);
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            dgvBookList.DataSource = productDb.GetProducts();

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(txtBookID.Text);
            if (ID <= 0)
            {
                MessageBox.Show("ID >= 0");
                return;
            }
            string Title = txtBookTitle.Text;
            float Price = float.Parse(txtBookPrice.Text);
            int Quantity = int.Parse(txtBookQuantity.Text);
            if (productDb.AddProduct(new Product{ProductID = ID,Quantity = Quantity,ProductName = Title,UnitPrice = Price}))
            {
                MessageBox.Show("Save successful");
            }
            else
            {
                MessageBox.Show("Save fail.");
            }
            GetData();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(txtBookID.Text);
            //Goi ham xoa Sach
            bool r = productDb.RemoveBook(ID);
            string s = (r == true ? "successful" : "fail");
            MessageBox.Show("Delete " + s);
            GetData();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(txtBookID.Text);
            string Title = txtBookTitle.Text;
            float Price = float.Parse(txtBookPrice.Text);
            int Quantity = int.Parse(txtBookQuantity.Text);
            Product p = new Product
            {
                ProductID = ID,
                ProductName = Title,
                UnitPrice = Price,
                Quantity = Quantity
            };
            //goi phuong thuc cap nhat
            bool r = productDb.UpdateProduct(p);
            string s = (r == true ? "successful" : "fail");
            MessageBox.Show("Update " + s);
            GetData();
        }
    }
}
