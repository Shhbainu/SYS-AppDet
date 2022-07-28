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

namespace SYS_AppDet.Pages
{
    /// <summary>
    /// Interaction logic for pageCustomer.xaml
    /// </summary>
    public partial class pageCustomer : Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Users\zhiel\source\repos\SYS-AppDet\SYS-AppDet\inventorySQL.mdf;Integrated Security=True");

        public pageCustomer()
        {
            InitializeComponent();
            LoadCust();
        }

        public void ClearCust()
        {
            custidtxtbox.Clear();
            custnametxtbox.Clear();
            custphonetxtbox.Clear();
        }

        public void LoadCust()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM CustomerTable", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgCust.ItemsSource = dt.DefaultView;
        }

        public bool IsValid()
        {
            if (custnametxtbox.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (custphonetxtbox.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void saveCustBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsValid())
                {
                    if (MessageBox.Show("Are you sure you want to SAVE this customer?", "Saving Customer", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO CustomerTable(cust_name,cust_phone)VALUES(@cust_name,@cust_phone)", con);
                        cmd.Parameters.AddWithValue("@cust_name", custnametxtbox.Text);
                        cmd.Parameters.AddWithValue("@cust_phone", custphonetxtbox.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Customer has been successfully saved", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearCust();
                        LoadCust();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void updateCustBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(MessageBox.Show("Are you sure you want to UPDATE this Customer?", "Updating Customer", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE CustomerTable SET cust_name='"+custnametxtbox.Text+"', cust_phone='"+custphonetxtbox.Text+"' WHERE cust_id='"+custidtxtbox.Text+"' ", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer has been successfully updated", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                ClearCust();
                LoadCust();
            }
        }

        private void clearCustBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearCust();
        }

        private void deleteCustBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to DELETE this Customer?", "Deleting Customer", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM CustomerTable WHERE cust_id=" + custidtxtbox.Text + " ", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer has been successfully deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Not Deleted" + ex.Message);
            }
            finally
            {
                con.Close();
                ClearCust();
                LoadCust();
            }
        }

        private void dgCust_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView selectedRow = dg.SelectedItem as DataRowView;
            if(selectedRow != null)
            {
                custidtxtbox.Text = selectedRow["cust_id"].ToString();
                custnametxtbox.Text = selectedRow["cust_name"].ToString();
                custphonetxtbox.Text = selectedRow["cust_phone"].ToString();
            }
        }
    }
}
