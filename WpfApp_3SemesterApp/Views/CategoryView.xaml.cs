using System;
using System.Collections.Generic;
using System.Data;
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
using WpfApp_3SemesterApp.Models;
using WpfApp_3SemesterApp.ViewModels;

namespace WpfApp_3SemesterApp.Views
{
    /// <summary>
    /// Logika interakcji dla klasy CategoryView.xaml
    /// </summary>
    public partial class CategoryView : Page
    {
        public CategoryView()
        {
            InitializeComponent();

            DataContext = new CategoryViewModel();
        }

        private void addNewCategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateCategoryView());
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(sender != null)
            {
                DataGrid dataGrid = sender as DataGrid;
                if(dataGrid != null && dataGrid.SelectedItem != null && dataGrid.SelectedItems.Count > 0)
                {
                    DataGridRow dgRow = dataGrid.ItemContainerGenerator.ContainerFromItem(dataGrid.SelectedItem) as DataGridRow;
                    var category = dgRow.Item as Category;

                    if(category != null)
                    {
                        NavigationService.Navigate(new CategoryEditView(category.Id));
                    }
                }
            }
        }
    }
}
