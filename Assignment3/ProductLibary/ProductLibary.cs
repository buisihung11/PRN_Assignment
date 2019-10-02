using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace ProductLibary
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }

        public float SubTotal
        {
            get { return Quantity * UnitPrice; }
        }
    }

    public class ProductDB
    {
        private string strConnection;
        public ProductDB(string connection)
        {
            this.strConnection = connection;
        }

        #region ADO.NET
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            string SQL = "select * from Products";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);

            DataTable dtBook = new DataTable();
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Product p = new Product();
                    p.ProductID = (int) dr["ProductID"];
                    p.ProductName = (string) dr["ProductName"];
                    p.Quantity = (int) dr["Quantity"];
                    p.UnitPrice = float.Parse(dr["UnitPrice"].ToString());

                    products.Add(p);
                }

            }
            catch (SqlException se)
            {
                throw se;
            }
            finally
            {
                cnn.Close();
            }

            return products;
        }
        public bool AddProduct(Product product)
        {
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL =
                $"SET IDENTITY_INSERT Products ON " +
                $"Insert into Products(ProductID,ProductName,Quantity,UnitPrice) values(@ProductID,@ProductName,@Quantity,@UnitPrice)" +
                $" SET IDENTITY_INSERT Products OFF";
            //string SQL = "spAddBook";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
            cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
            cmd.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
            //cmd.CommandType = CommandType.StoredProcedure;
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            int count = cmd.ExecuteNonQuery();
            return (count > 0);
        }
        public bool UpdateProduct(Product product)
        {
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Update Products set ProductName=@ProductName,Quantity =@Quantity,UnitPrice=@UnitPrice where ProductID=@ProductID";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            //cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
            cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
            cmd.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            int count = cmd.ExecuteNonQuery();
            return (count > 0);
        }
        public bool RemoveBook(int id)
        {
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Delete Products where ProductID=@ProductID";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@ProductID", id);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            int count = cmd.ExecuteNonQuery();
            return (count > 0);
        }
        public Product FindProduct(int ProductID)
        {
            Product p = null;
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Select * From Products where ProductID=@ProductID";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }

            SqlDataReader dr = cmd.ExecuteReader();

            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }

                while (dr.Read())
                {
                    p = new Product();
                    p.ProductID = (int)dr["ProductID"];
                    p.ProductName = (string)dr["ProductID"];
                    p.Quantity = (int)dr["Quantity"];
                    p.UnitPrice = (float)dr["UnitPrice"];
                }

            }
            catch (SqlException se)
            {
                throw se;
            }
            finally
            {
                cnn.Close();
            }
            return p;
        }
        #endregion
    }
}
