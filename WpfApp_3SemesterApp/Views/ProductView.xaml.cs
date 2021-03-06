﻿using System;
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
using WpfApp_3SemesterApp.Data;
using WpfApp_3SemesterApp.Models;
using WpfApp_3SemesterApp.Services;
using WpfApp_3SemesterApp.ViewModels;

namespace WpfApp_3SemesterApp.Views
{
    /// <summary>
    /// Logika interakcji dla klasy ProductView.xaml
    /// </summary>
    public partial class ProductView : Page
    {
        public ProductView()
        {
            InitializeComponent();

            DataContext = new ProductViewModel();
        }

        private void addNewProductBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateProductView());
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dataGrid = sender as DataGrid;
                if (dataGrid != null && dataGrid.SelectedItem != null && dataGrid.SelectedItems.Count > 0)
                {
                    DataGridRow dgRow = dataGrid.ItemContainerGenerator.ContainerFromItem(dataGrid.SelectedItem) as DataGridRow;
                    var product = dgRow.Item as Product;

                    if (product != null)
                    {
                        NavigationService.Navigate(new ProductEditView(product.Id));
                    }
                }
            }
        }
    }
}
