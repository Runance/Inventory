using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    public partial class frmAddProduct : Form
    {
        private string _ProductName;
        private string _Category;
        private string MfgDate;
        private string _ExpDate;
        private string _Description;
        private int _Quantity;
        private double _SellPrice;
        private BindingSource showProductList;

        public frmAddProduct()
        {
            InitializeComponent();
            showProductList = new BindingSource();

        }
        public class NumberFormatException : Exception
        {
            public NumberFormatException(string message) : base(message)
            {
            }
        }

        public class StringFormatException : Exception
        {
            public StringFormatException(string message) : base(message)
            {
            }
        }

        public class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string message) : base(message)
            {
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = { "Beverages", "Bread/Bakery", "Canned/Jarred Goods", "Dairy",
                                       "Frozen Goods", "Meat", "Personal Care", "Other" };

            foreach (string category in ListOfProductCategory)
            {
                cbCategory.Items.Add(category);
            }
        }

        public string Product_Name(string Product)
        {
            if (!Regex.IsMatch(Product, @"^[a-zA-Z]+$"))
            {
                throw new StringFormatException("Product name must contain only letters.");
            }
            return Product;
        }

        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]+$"))
            {
                throw new NumberFormatException("Quantity must be a valid number.");
            }
            return Convert.ToInt32(qty);
        }

        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price, @"^(\d*\.)?\d+$"))
            {
                throw new CurrencyFormatException("Invalid price format. It must be a valid number or currency.");
            }
            return Convert.ToDouble(price);
        }



        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
             
                string _ProductName = Product_Name(txtProductName.Text);
                string _Category = cbCategory.Text;
                string _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                string _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                string _Description = richTxtDescription.Text;
                int _Quantity = Quantity(txtQuantity.Text);
                double _SellPrice = SellingPrice(txtSellPrice.Text);

                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellPrice, _Quantity, _Description));

                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridViewProductList.DataSource = showProductList;
            }
            catch (NumberFormatException ex)
            {
                MessageBox.Show(ex.Message, "Invalid Number Format", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
            catch (StringFormatException ex)
            {
                MessageBox.Show(ex.Message, "Invalid String Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CurrencyFormatException ex)
            {
                MessageBox.Show(ex.Message, "Invalid Currency Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
         
                MessageBox.Show("Process completed.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
