using SYS_AppDet.Pages;
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
        public pageOrders()
        {
            InitializeComponent();
            LoadOrder();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Source\Repos\Shhbainu\SYS-AppDet\SYS-AppDet\inventorySQL.mdf;Integrated Security=True");

        private void ManageOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            ManageOrders mngorder = new ManageOrders();
            mngorder.Show();
        }

        public void LoadOrder()
        {
            SqlCommand cmd = new SqlCommand("SELECT order_id AS 'Order ID', order_date AS 'Order Date', prod_name AS 'Product Name', cust_name AS 'Customer Name', order_qty AS 'Order Quantity', prod_price AS 'Price', total_amount AS 'Total Amount' FROM OrderTable", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgOrder.ItemsSource = dt.DefaultView;
        }

        
        private void dgOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
