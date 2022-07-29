using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace SYS_AppDet.Pages
{
    /// <summary>
    /// Interaction logic for ManageOrders.xaml
    /// </summary>
    public partial class ManageOrders : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Source\Repos\Shhbainu\SYS-AppDet\SYS-AppDet\inventorySQL.mdf;Integrated Security=True");


        public ManageOrders()
        {
            InitializeComponent();
            LoadProd();
            LoadCust();
        }

        //Displaying Values
        private void exitOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void LoadCust()
        {
            SqlCommand cmd = new SqlCommand("SELECT cust_id AS 'Customer ID', cust_name AS 'Customer Name', cust_phone AS 'Phone' FROM CustomerTable", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgOrderCust.ItemsSource = dt.DefaultView;
        }

        public void LoadProd()
        {
            SqlCommand cmd = new SqlCommand("SELECT prod_id AS 'Product ID', prod_name AS 'Product', categ_name AS 'Category', prod_qty AS 'Quantity', prod_price AS 'Price', prod_desc AS 'Description' FROM ProductTable", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgOrderProd.ItemsSource = dt.DefaultView;
        }

        public void ClearOrderProdCust()
        {
            ordercustidtxtbox.Clear();
            ordercustnametxtbox.Clear();

            orderprodidtxtbox.Clear();
            orderprodnametxtbox.Clear();

            orderprodpricetxtbox.Clear();
            orderquantity.Value = 1;
            orderprodtotaltxtbox.Clear();
            
        }

        private void dgOrderCust_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView selectedRow = dg.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                ordercustidtxtbox.Text = selectedRow["Customer ID"].ToString();
                ordercustnametxtbox.Text = selectedRow["Customer Name"].ToString();
            }
        }

        private void dgOrderProd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView selectedRow = dg.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                orderprodidtxtbox.Text = selectedRow["Product ID"].ToString();
                orderprodnametxtbox.Text = selectedRow["Product"].ToString();
                orderprodpricetxtbox.Text = selectedRow["Price"].ToString();
                prod_qty = Convert.ToInt16(selectedRow["Quantity"].ToString());
                //orderprodtotaltxtbox.Text = selectedRow["Price"].ToString(); Computed Total Amount
            }
        }

        private void clearOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearOrderProdCust();
        }

        //For dynamic multiplication of order_qty and prod_price
        int prod_qty = 0;
        private void orderquantity_ValueChanged(object sender, EventArgs e) {

            if (Convert.ToInt32(orderquantity.Text) > prod_qty) { 
                MessageBox.Show("Instock quantity is not enough!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                prod_qty = prod_qty - 1;
                return;
            }
            if (Convert.ToInt32(orderquantity.Text) > 0)
            {
                decimal total = (Convert.ToDecimal(orderprodpricetxtbox.Text)) * Convert.ToInt32(orderquantity.Text);
                orderprodtotaltxtbox.Text = total.ToString();
            }
        }

        public bool IsValid()
        {
            if (ordercustidtxtbox.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }            
            if (ordercustnametxtbox.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (orderproddate.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (orderprodidtxtbox.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (orderprodnametxtbox.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (orderprodpricetxtbox.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (orderquantity.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        //Insert to OrderTable [to comment out a block 'ctrl + K & C' , to uncomment 'ctrl + K & Y'
        private void insertOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsValid())
                {
                    if (MessageBox.Show("Are you sure you want to SAVE this order?", "Saving Orders", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO OrderTable (order_date, prod_id, prod_name, cust_id, cust_name, order_qty, prod_price, total_amount)VALUES(@order_date, @prod_id, @prod_name, @cust_id, @cust_name, @order_qty, @prod_price, @total_amount)", con);
                        cmd.Parameters.AddWithValue("@order_date", orderproddate.Text);
                        cmd.Parameters.AddWithValue("@prod_id", orderprodidtxtbox.Text);
                        cmd.Parameters.AddWithValue("@prod_name", orderprodnametxtbox.Text);
                        cmd.Parameters.AddWithValue("@cust_id", ordercustidtxtbox.Text);
                        cmd.Parameters.AddWithValue("@cust_name", ordercustnametxtbox.Text);
                        cmd.Parameters.AddWithValue("@order_qty", orderquantity.Text);
                        cmd.Parameters.AddWithValue("@prod_price", Convert.ToDecimal(orderprodpricetxtbox.Text));
                        cmd.Parameters.AddWithValue("@total_amount", Convert.ToDecimal(orderprodtotaltxtbox.Text));
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Order has been successfully saved", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Changing product quantity in ProductTable
                        cmd = new SqlCommand("UPDATE ProductTable SET prod_qty= (prod_qty - @product_qty) WHERE prod_id LIKE '" +orderprodidtxtbox.Text + "'", con);
                        cmd.Parameters.AddWithValue("@product_qty", Convert.ToInt16(orderquantity.Value));
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ClearOrderProdCust();
                        LoadProd();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


    }
}
