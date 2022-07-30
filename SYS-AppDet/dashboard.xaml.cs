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
using System.Windows.Shapes;

namespace SYS_AppDet
{

    public partial class dashboard : Window
    {
        public dashboard()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            login log = new login();
            log.Show();
           
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            navFrame.Content = new pageDashboard();
        }

        private void Product_Click(object sender, RoutedEventArgs e)
        {
            navFrame.Content = new pageProduct();
        }

        private void Category_Click(object sender, RoutedEventArgs e)
        {
            navFrame.Content = new pageCategory();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            navFrame.Content = new pageOrders();
        }

        private void StatusUpdate_Click(object sender, RoutedEventArgs e)
        {
            navFrame.Content = new pageStatusUpdate();
        }

        private void User_Click(object sender, RoutedEventArgs e)
        {
            navFrame.Content = new pageUser();
        }

        private void Customers_Click(object sender, RoutedEventArgs e)
        {
            navFrame.Content = new pageCustomer();
        }
    }
}
