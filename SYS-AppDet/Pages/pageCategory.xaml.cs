using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;

namespace SYS_AppDet
{
    /// <summary>
    /// Interaction logic for pageCategory.xaml
    /// </summary>
    public partial class pageCategory : Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\Shhbainu\SYS-AppDet\SYS-AppDet\inventorySQL.mdf;Integrated Security=True");

        public pageCategory()
        {
            InitializeComponent();
            LoadCateg();
        }

        public void ClearCateg()
        {
            categidtxtbox.Clear();
            categnametxtbox.Clear();
        }

        public void LoadCateg()
        {
            SqlCommand cmd = new SqlCommand("SELECT categ_id AS 'Category ID', categ_name AS 'Category' FROM CategoryTable", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgCateg.ItemsSource = dt.DefaultView;
        }

        public bool IsValid()
        {
            if(categnametxtbox.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void saveCategBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsValid())
                {
                    if (MessageBox.Show("Are you sure you want to SAVE this category?", "Saving Category", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO CategoryTable (categ_name)VALUES(@categ_name)", con);
                        cmd.Parameters.AddWithValue("@categ_name", categnametxtbox.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Category has been successfully saved", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearCateg();
                        LoadCateg();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearCategBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearCateg();
        }

        private void deleteCategBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsValid())
                {
                    if (MessageBox.Show("Are you sure you want to DELETE this Category?", "Deleting Category", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM CategoryTable WHERE categ_id=" + categidtxtbox.Text + " ", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Category has been successfully deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Not Deleted"+ex.Message);
            }
            finally
            {
                con.Close();
                ClearCateg();
                LoadCateg();
            }
        }

        private void updateCategBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsValid())
                {
                    if (MessageBox.Show("Are you sure you want to UPDATE this Category?", "Updating Category", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE CategoryTable SET categ_name='" + categnametxtbox.Text + "' WHERE categ_id='" + categidtxtbox.Text + "' ", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Category has been successfully updated", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
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
                ClearCateg();
                LoadCateg();
            }
        }

        private void dgCateg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView selectedRow = dg.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                categidtxtbox.Text = selectedRow["Category ID"].ToString();
                categnametxtbox.Text = selectedRow["Category"].ToString();
            }
        }
    }
}
