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
    /// Interaction logic for pageLedger.xaml
    /// </summary>
    public partial class pageStatusUpdate : Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Source\Repos\Shhbainu\SYS-AppDet\SYS-AppDet\inventorySQL.mdf;Integrated Security=True");
        public pageStatusUpdate()
        {
            InitializeComponent();
            LoadOrder();
        }

        public void LoadOrder()
        {
            SqlCommand cmd = new SqlCommand("SELECT order_id AS 'Order ID', order_date AS 'Order Date', prod_name AS 'Product Name', cust_name AS 'Customer Name', payment AS 'Payment', status AS 'Status', total_amount AS 'Total Amount' FROM OrderTable", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgStatus.ItemsSource = dt.DefaultView;
        }

        private void dgStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            {
                DataGrid dg = (DataGrid)sender;
                DataRowView selectedRow = dg.SelectedItem as DataRowView;
                if (selectedRow != null)
                {
                    orderidtxtbox.Text = selectedRow["Order ID"].ToString();
                    statustxtbox.Text = selectedRow["Status"].ToString();
                }
            }
        }

        private void updateStatusBtn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE OrderTable SET status='" + statustxtbox.Text + "' WHERE order_id='" + orderidtxtbox.Text + "' ", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Status has been successfully updated", "Update", MessageBoxButton.OK, MessageBoxImage.Information);


        }

        private void orderidtxtbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
