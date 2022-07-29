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
    /// Interaction logic for pageUser.xaml
    /// </summary>
    public partial class pageUser : Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Users\zhiel\source\repos\SYS-AppDet\SYS-AppDet\inventorySQL.mdf;Integrated Security=True");
        
        public pageUser()
        {
            InitializeComponent();
            LoadUser();
        }

        public void ClearUser()
        {
            uidtxtbox.Clear();
            nametxtbox.Clear();
            usernametxtbox.Clear();
            passtxtbox.Clear();
            emailtxtbox.Clear();
            phonetxtbox.Clear();
        }

        public void LoadUser()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM UserTable", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgUser.ItemsSource = dt.DefaultView;
        }

        public bool IsValid()
        {
            if (nametxtbox.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (usernametxtbox.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (passtxtbox.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (emailtxtbox.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (phonetxtbox.Text == String.Empty)
            {
                MessageBox.Show("A field is empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                if (IsValid())
                {
                    if (MessageBox.Show("Are you sure you want to SAVE this user?", "Saving Record", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO UserTable(user_name,user_username,user_password,user_email,user_phone)VALUES(@user_name,@user_username,@user_password,@user_email,@user_phone)", con);
                        cmd.Parameters.AddWithValue("@user_name", nametxtbox.Text);
                        cmd.Parameters.AddWithValue("@user_username", usernametxtbox.Text);
                        cmd.Parameters.AddWithValue("@user_password", passtxtbox.Text);
                        cmd.Parameters.AddWithValue("@user_email", emailtxtbox.Text);
                        cmd.Parameters.AddWithValue("@user_phone", phonetxtbox.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("User has been successfully saved", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearUser();
                        LoadUser();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearUser();
        }
                
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsValid())
                {
                    if (MessageBox.Show("Are you sure you want to DELETE this user?", "Deleting Record", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM UserTable WHERE user_id= " + uidtxtbox.Text + " ", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User has been successfullly deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Not Deleted" +ex.Message);
            }
            finally
            {
                con.Close();
                ClearUser();
                LoadUser();
            }
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (IsValid())
                {
                    if (MessageBox.Show("Are you sure you want to UPDATE this user?", "Updating Record", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE UserTable SET user_name='" + nametxtbox.Text + "', user_username='" + usernametxtbox.Text + "', user_password='" + passtxtbox.Text + "', user_email='" + emailtxtbox.Text + "', user_phone='" + phonetxtbox.Text + "' WHERE  user_id ='" + uidtxtbox.Text + "' ", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User has been successfully updated", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
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
                ClearUser();
                LoadUser();
            }
        }

        private void dgUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView selectedRow = dg.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                uidtxtbox.Text = selectedRow["user_id"].ToString();
                nametxtbox.Text = selectedRow["user_name"].ToString();
                usernametxtbox.Text = selectedRow["user_username"].ToString();
                passtxtbox.Text = selectedRow["user_password"].ToString();
                emailtxtbox.Text = selectedRow["user_email"].ToString();
                phonetxtbox.Text = selectedRow["user_phone"].ToString();
            }
        }

    }
}
