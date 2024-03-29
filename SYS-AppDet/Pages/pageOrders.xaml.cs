﻿using SYS_AppDet.Pages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace SYS_AppDet
{
    /// <summary>
    /// Interaction logic for pageOrders.xaml
    /// </summary>
    public partial class pageOrders : Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Source\Repos\Shhbainu\SYS-AppDet\SYS-AppDet\inventorySQL.mdf;Integrated Security=True");

        public pageOrders()
        {
            InitializeComponent();
            LoadOrder();
        }

        private void ManageOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            ManageOrders mngorder = new ManageOrders();
            mngorder.Show();
            ClearOrder();
        }

        public void LoadOrder()
        {
            SqlCommand cmd = new SqlCommand("SELECT order_id AS 'Order ID', order_date AS 'Order Date', prod_name AS 'Product Name', cust_name AS 'Customer Name', payment AS 'Payment', status AS 'Status', order_qty AS 'Order Quantity', prod_price AS 'Price', total_amount AS 'Total Amount' FROM OrderTable", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgOrder.ItemsSource = dt.DefaultView;
        }

        public void ClearOrder()
        {
            orderidtxtbox.Clear();
        }
        
        private void deleteOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to DELETE this Order?", "Deleting Order", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM OrderTable WHERE order_id=" + orderidtxtbox.Text + " ", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order has been successfully deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);

                    //prod_qty will increase base on order_qty if the order will be deleted
                    if (true)
                    {

                        SqlCommand cmd2 = new SqlCommand("UPDATE ProductTable SET prod_qty = (prod_qty + @order_qty ) WHERE prod_name = '"  + orderprodnametxtbox.Text + "'", con);
                        cmd2.Parameters.AddWithValue("@order_qty", Convert.ToInt16(orderquantitytxtbox.Text));
                        cmd2.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Not Deleted" + ex.Message);
            }
            finally
            {
                con.Close();
                LoadOrder();
            }
        }

        private void dgOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView selectedRow = dg.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                orderidtxtbox.Text = selectedRow["Order ID"].ToString();
                orderprodnametxtbox.Text = selectedRow["Product Name"].ToString();
                orderquantitytxtbox.Text = selectedRow["Order Quantity"].ToString();
            }
        }

        private void clearOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearOrder();
        }
    }
}
