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
using WpfApp_3SemesterApp.ViewModels;

namespace WpfApp_3SemesterApp.Views
{
    /// <summary>
    /// Logika interakcji dla klasy DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Page
    {
        public DashboardView()
        {
            InitializeComponent();

            DashboardFrame.Content = new ProductView();
        }

        private void dashboard_ProductsBtn_Click(object sender, RoutedEventArgs e)
        {
            DashboardFrame.Content = new ProductView();
        }

        private void dashboard_CategoriesBtn_Click(object sender, RoutedEventArgs e)
        {
            DashboardFrame.Content = new CategoryView();
        }
    }
}
