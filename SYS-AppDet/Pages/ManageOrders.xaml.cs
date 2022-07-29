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

namespace SYS_AppDet.Pages
{
    /// <summary>
    /// Interaction logic for ManageOrders.xaml
    /// </summary>
    public partial class ManageOrders : Window
    {
        public ManageOrders()
        {
            InitializeComponent();
        }
        
        private void dgOrderProd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgOrderCust_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void exitOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
