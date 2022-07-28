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
        SqlCommand cmd = new SqlCommand();

        public pageUser()
        {
            InitializeComponent();
            LoadUser();
        }

        public void Clear()
        {
            fltxtbox.Clear();
            usertxtbox.Clear();
            passtxtbox.Clear();
            emailtxtbox.Clear();
            phonetxtbox.Clear();
        }

        public void LoadUser()
        {
            cmd = new SqlCommand("SELECT * FROM Usertbl", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgUser.ItemsSource = dt.DefaultView;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                if (MessageBox.Show("Are you sure you want to save this user?", "Saving Record", MessageBoxButton.YesNo, MessageBoxImage.Question)==MessageBoxResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO Usertbl(fullName,username,password,email,phone)VALUES(@fullName,@username,@password,@email,@phone)", con);
                    cmd.Parameters.AddWithValue("@fullName", fltxtbox.Text);
                    cmd.Parameters.AddWithValue("@username", usertxtbox.Text);
                    cmd.Parameters.AddWithValue("@password", passtxtbox.Text);
                    cmd.Parameters.AddWithValue("@email", emailtxtbox.Text);
                    cmd.Parameters.AddWithValue("@phone", phonetxtbox.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User has been successfully saved");
                    Clear();
                    LoadUser();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void dgUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView selectedRow = dg.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                uidtxtbox.Text = selectedRow["userID"].ToString();
                fltxtbox.Text = selectedRow["fullName"].ToString();
                usertxtbox.Text = selectedRow["username"].ToString();
                passtxtbox.Text = selectedRow["password"].ToString();
                emailtxtbox.Text = selectedRow["email"].ToString();
                phonetxtbox.Text = selectedRow["phone"].ToString();
            }
        }
    }
}
