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
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Users\zhiel\source\repos\SYS-AppDet\SYS-AppDet\inventorySQL.mdf;Integrated Security=True");

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
                //orderprodtotaltxtbox.Text = selectedRow["Price"].ToString(); Computed Total Amount
            }
        }

        private void clearOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearOrderProdCust();
        }


        //Insert to OrderTable [to comment out a block 'ctrl + K & C' , to uncomment 'ctrl + K & Y'
        private void insertOrderBtn_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
