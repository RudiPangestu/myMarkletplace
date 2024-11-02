using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myMarkletplace.Data_Models;

namespace myMarkletplace.Data_Accesses
{
    public class DAProducts
    {
        private readonly string connstring = "Data Source = DESKTOP-OC4QH26\\SQLEXPRESS; Initial Catalog  = myMarketplace; Integrated Security = true";

        public DMProducts DMProducts { get; private set; }

        public List<DMProducts> GetProducts()
        {
            var products = new List<DMProducts>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();

                    string sql = "SELECT * FROM products ORDER BY product_id DESC";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DMProducts = new DMProducts
                                {
                                    product_id = reader.GetInt32(0),
                                    product_name = reader.GetString(1),
                                    product_price = reader.GetInt32(2),
                                    product_stock = reader.GetInt32(3),
                                    product_description = reader.GetString(4)
                                };

                                products.Add(DMProducts);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }


            return products;
        }

        public DMProducts GetProducts(int product_id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();

                    string sql = "SELECT * FROM products WHERE product_id = @product_id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@product_id", product_id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DMProducts = new DMProducts
                                {
                                    product_id = reader.GetInt32(0),
                                    product_name = reader.GetString(1),
                                    product_price = reader.GetInt32(2),
                                    product_stock = reader.GetInt32(3),
                                    product_description = reader.GetString(4)
                                };

                                return DMProducts;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

            return null;
        }

        //Create Products
        public void CreateProduct(DMProducts product)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();

                    string sql = "INSERT INTO products" +
                                 "(product_name, product_price, product_stock, product_description) VALUES" +
                                 "(@product_name, @product_price, @product_stock, @product_description);";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@product_id", product.product_id);
                        cmd.Parameters.AddWithValue("@product_name", product.product_name);
                        cmd.Parameters.AddWithValue("@product_price", product.product_price);
                        cmd.Parameters.AddWithValue("@product_stock", product.product_stock);
                        cmd.Parameters.AddWithValue("@product_description", product.product_description);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        //Update Products
        public void UpdateProduct(DMProducts product)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();

                    string sql = "UPDATE products SET product_name=@product_name, product_price=@product_price, product_stock=@product_stock WHERE product_id=@product_id";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@product_id", product.product_id);
                        cmd.Parameters.AddWithValue("@product_name", product.product_name);
                        cmd.Parameters.AddWithValue("@product_price", product.product_price);
                        cmd.Parameters.AddWithValue("@product_stock", product.product_stock);
                        cmd.Parameters.AddWithValue("@product_description", product.product_description);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        //Delete Products
        public void DeleteProduct(int product_id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();

                    string sql = "DELETE FROM products WHERE product_id = @product_id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@product_id", product_id);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
}
