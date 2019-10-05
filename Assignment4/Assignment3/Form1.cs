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
        private DataTable dtProduct;
        public Form1()
        {
            InitializeComponent();
            productDb = new ProductDB(ConfigurationManager.ConnectionStrings["SaleDB"].ConnectionString);
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            //GetData();
        }

        private void GetData()
        {
            //Xoa rang buoc du lieu tren cac TextBoxes de Binding lai lan sau
            txtBookID.DataBindings.Clear();
            txtBookTitle.DataBindings.Clear();
            txtBookPrice.DataBindings.Clear();
            txtBookQuantity.DataBindings.Clear();

            dtProduct = productDb.GetProducts();

            dtProduct.PrimaryKey = new DataColumn[] { dtProduct.Columns["ProductID"]};
            dtProduct.Columns.Add("SubTotal", typeof(double), "Quantity * UnitPrice");

            bsProducts.DataSource = dtProduct;

            //Rang buoc du lieu tren cac Textboxes
            txtBookID.DataBindings.Add("Text", bsProducts, "ProductID");
            txtBookTitle.DataBindings.Add("Text", bsProducts, "ProductName");
            txtBookPrice.DataBindings.Add("Text", bsProducts, "UnitPrice");
            txtBookQuantity.DataBindings.Add("Text", bsProducts, "Quantity");
            
            //Binding for DGV
            dgvBookList.DataSource = bsProducts;
            bsProducts.Sort = "ProductID DESC";
            bnProductList.BindingSource = bsProducts;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            //O day la phuing cap Disconnect cua Microsoft
            //Tuc la da se thay doi du lieu tren DataSource cua client
            //Sau do goi server de add mot gia tri moi ma khong can phai load lai Data
            //1. Khoi tao gia tri mac dinh cho Product
            int ID = 1;
            string Name = string.Empty;
            float Price = 0;
            int productQuantity = 0;

            if(dtProduct.Rows.Count > 0)
            {
                ID = int.Parse(dtProduct.Compute("MAX(ProductID)", "").ToString()) + 1;
            }

            Product pro = new Product
                {ProductID = ID, Quantity = productQuantity, UnitPrice = Price, ProductName = Name};


            //2. Tao doi tuong moi va truyen du lieu
            frmProductDetails productDetails = new frmProductDetails(true,pro,productDb);

            //3. Lay ket qua tra ve
            DialogResult r = productDetails.ShowDialog();
            if (r == DialogResult.OK)
            {
                pro = productDetails.ProductAddOrEdit;
                //Cap nhat vao DataTable
                dtProduct.Rows.Add(pro.ProductID, pro.ProductName, pro.UnitPrice, pro.Quantity);

            }
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
            Product pro = new Product
            {
                ProductID = ID,
                ProductName = Title,
                UnitPrice = Price,
                Quantity = Quantity
            };
            //goi phuong thuc cap nhat

            frmProductDetails productDetails = new frmProductDetails(false, pro, productDb);

            //3. Lay ket qua tra ve
            DialogResult r = productDetails.ShowDialog();
            if (r == DialogResult.OK)
            {

                DataRow row = dtProduct.Rows.Find(pro.ProductID);
                row["ProductName"] = pro.ProductName;
                row["Quantity"] = pro.Quantity;
                row["UnitPrice"] = pro.UnitPrice;
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetData();
        }
    }
}
