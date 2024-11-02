using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using myMarkletplace.Data_Accesses;
using myMarkletplace.Data_Models;

namespace myMarkletplace
{
    public partial class Form1 : Form
    {
        private readonly DAProducts _DAProducts;
        public Form1()
        {
            InitializeComponent();
            _DAProducts = new DAProducts();
            LoadProducts();
        }

        private void LoadProducts()
        {
            dgvProducts.DataSource = null; // Clear existing data source
            dgvProducts.DataSource = _DAProducts.GetProducts();
        }

        private void ClearFields()
        {
            txtProductName.Clear();
            txtProductPrice.Clear();
            txtProductStock.Clear();
            txtProductDescription.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DMProducts product = new DMProducts()
                {
                    product_name = txtProductName.Text,
                    product_price = int.Parse(txtProductPrice.Text),
                    product_stock = int.Parse(txtProductStock.Text),
                    product_description = txtProductDescription.Text
                };

                _DAProducts.CreateProduct(product);
                LoadProducts();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding product: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count != 0)
            {
                try
                {
                    // Get the selected product's ID
                    int selectedProductId = (int)dgvProducts.SelectedRows[0].Cells["product_id"].Value;
                    MessageBox.Show("Product updated succesfully!");

                    DMProducts product = new DMProducts()
                    {
                        product_id = selectedProductId,
                        product_name = this.txtProductName.Text,
                        product_price = int.Parse(this.txtProductPrice.Text),
                        product_stock = int.Parse(this.txtProductStock.Text),
                        product_description = this.txtProductDescription.Text
                    };

                    _DAProducts.UpdateProduct(product);
                    LoadProducts();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating product: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a product to update.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count != 0)
            {
                try
                {
                    int selectedProductId = (int)dgvProducts.SelectedRows[0].Cells["product_id"].Value;
                    DialogResult result =
                        MessageBox.Show("Are you sure you want to delete this Product?",
                        "Delete Product", MessageBoxButtons.YesNo);

                    if (result == DialogResult.No)
                    {
                        return;
                    }

                    _DAProducts.DeleteProduct(selectedProductId);
                    LoadProducts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating product: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete.");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                    pictureBox1.Tag = openFileDialog.FileName; // Simpan path foto di Tag
                }
            }
        }
    }
}

