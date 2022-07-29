using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;

namespace SYS_AppDet
{
    /// <summary>
    /// Interaction logic for pageProduct.xaml
    /// </summary>
    public partial class pageProduct : Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Source\Repos\Shhbainu\SYS-AppDet\SYS-AppDet\inventorySQL.mdf;Integrated Security=True");

        public pageProduct()
        {
            InitializeComponent();
            LoadProdCateg();
            LoadProd();
        }

        public void LoadProdCateg()
        {
            prodcombobox.Items.Clear();
            SqlCommand cmd = new SqlCommand("SELECT categ_name FROM CategoryTable", con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                prodcombobox.Items.Add(sdr[0].ToString());
            }
            sdr.Close();
            con.Close();
        }

        public void ClearProd()
        {
            prodidtxtbox.Clear();
            prodnametxtbox.Clear();
            prodcombobox.Items.Clear();
            prodpricetxtbox.Clear();
            prodqtytxtbox.Clear();
            proddesctxtbox.Clear();
        }

        public void LoadProd()
        {
            SqlCommand cmd = new SqlCommand("SELECT prod_id AS 'Product ID', prod_name AS 'Product', categ_name AS 'Category', prod_qty AS 'Quantity', prod_price AS 'Price', prod_desc AS 'Description' FROM ProductTable", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgProd.ItemsSource = dt.DefaultView;
        }

        public bool IsValid()
        {
            if (prodnametxtbox.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (prodcombobox.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (prodpricetxtbox.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (prodqtytxtbox.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (proddesctxtbox.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void saveprodBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsValid())
                {
                    if (MessageBox.Show("Are you sure you want to SAVE this Product?", "Saving Product", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO ProductTable(prod_name,categ_name,prod_price,prod_qty,prod_desc)VALUES(@prod_name,@categ_name,@prod_price,@prod_qty,@prod_desc)", con);
                        cmd.Parameters.AddWithValue("@prod_name", prodnametxtbox.Text);
                        //cmd.Parameters.AddWithValue("@categ_id", idunno ?);
                        cmd.Parameters.AddWithValue("@categ_name", prodcombobox.Text);
                        cmd.Parameters.AddWithValue("@prod_price", prodpricetxtbox.Text);
                        cmd.Parameters.AddWithValue("@prod_qty", prodqtytxtbox.Text);
                        cmd.Parameters.AddWithValue("@prod_desc", proddesctxtbox.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Product has been successfully saved", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearProd();
                        LoadProd();
                        LoadProdCateg();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void updateprodBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsValid())
                {
                    if (MessageBox.Show("Are you sure you want to UPDATE this Product?", "Updating Product", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE ProductTable SET prod_name='"+prodnametxtbox.Text+"', categ_name='"+prodcombobox.Text+"', prod_price='"+prodpricetxtbox.Text+"', prod_qty='"+prodqtytxtbox.Text+"', prod_desc='"+proddesctxtbox.Text+"' WHERE prod_id='"+prodidtxtbox.Text+"' ", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Product has been successfully updated", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                ClearProd();
                LoadProd();
                LoadProdCateg();
            }
        }

        private void clearprodBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearProd();
        }

        private void deleteprodBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsValid())
                {
                    if (MessageBox.Show("Are you sure you want to DELETE this Product?", "Deleting Product", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM ProductTable WHERE prod_id=" + prodidtxtbox.Text + " ", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Product has been successfully deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
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
                ClearProd();
                LoadProd();
                LoadProdCateg();
            }
        }

        private void dgProd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView selectedRow = dg.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                prodidtxtbox.Text = selectedRow["Product ID"].ToString();
                prodnametxtbox.Text = selectedRow["Product"].ToString();
                prodcombobox.Text = selectedRow["Category"].ToString();
                prodqtytxtbox.Text = selectedRow["Quantity"].ToString();
                prodpricetxtbox.Text = selectedRow["Price"].ToString();
                proddesctxtbox.Text = selectedRow["Description"].ToString();
            }
        }

    }
}
