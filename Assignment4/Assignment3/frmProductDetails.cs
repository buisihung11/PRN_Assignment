using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductLibary;

namespace Assignment3
{
    public partial class frmProductDetails : Form
    {

        private bool addOrEdit;
        private ProductDB productDb;
        public Product ProductAddOrEdit { get; set; }

        public frmProductDetails()
        {
            InitializeComponent();
        }

        public frmProductDetails(bool flag,Product p, ProductDB db) : this()
        {
            addOrEdit = flag;
            ProductAddOrEdit = p;
            productDb = db;
            InitData();
        }

        private void InitData()
        {
            txtBookID.Text = ProductAddOrEdit.ProductID.ToString();
            txtBookTitle.Text = ProductAddOrEdit.ProductName;
            txtBookQuantity.Text = ProductAddOrEdit.Quantity.ToString();
            txtBookPrice.Text = ProductAddOrEdit.UnitPrice.ToString();
            if (addOrEdit) txtBookID.ReadOnly = false;
            else txtBookID.ReadOnly = true;

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            bool result = false;

            ProductAddOrEdit.ProductID = int.Parse(txtBookID.Text);
            ProductAddOrEdit.ProductName = txtBookTitle.Text;
            ProductAddOrEdit.UnitPrice = float.Parse(txtBookPrice.Text);
            ProductAddOrEdit.Quantity = int.Parse(txtBookQuantity.Text);


            if (addOrEdit)
                result = productDb.AddProduct(ProductAddOrEdit);
            else
                result = productDb.UpdateProduct(ProductAddOrEdit);

            if (result) MessageBox.Show("Save successful");
            else MessageBox.Show("Save fail.");

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
