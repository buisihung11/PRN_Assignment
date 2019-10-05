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
            //Xoa rang buoc du lieu tren cac TextBoxes de Binding lai lan sau
            txtBookID.DataBindings.Clear();
            txtBookTitle.DataBindings.Clear();
            txtBookPrice.DataBindings.Clear();
            txtBookQuantity.DataBindings.Clear();

            dgvBookList.DataSource = productDb.GetProducts();
            //Rang buoc du lieu tren cac Textboxes
            txtBookID.DataBindings.Add("Text", dgvBookList.DataSource, "ProductID");
            txtBookTitle.DataBindings.Add("Text", dgvBookList.DataSource, "ProductName");
            txtBookPrice.DataBindings.Add("Text", dgvBookList.DataSource, "UnitPrice");
            txtBookQuantity.DataBindings.Add("Text", dgvBookList.DataSource, "Quantity");
            

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(txtBookID.Text);
            if (ID <= 0)
            {
                MessageBox.Show("ID >= 0");
                return;
            }
            string Title = txtBookTitle.Text.Trim();
            if(Title.Length <= 0)
            {
                MessageBox.Show("Please enter name");
                txtBookTitle.Focus();
                return;
            }

            try
            {
                float Price = float.Parse(txtBookPrice.Text);
                int Quantity = int.Parse(txtBookQuantity.Text);
                if (productDb.AddProduct(new Product { ProductID = ID, Quantity = Quantity, ProductName = Title, UnitPrice = Price }))
                {
                    MessageBox.Show("Save successful");
                }
                else
                {
                    MessageBox.Show("Save fail.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
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
