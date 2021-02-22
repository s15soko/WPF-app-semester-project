using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using WpfApp_3SemesterApp.Services;
using WpfApp_3SemesterApp.ViewModels;

namespace WpfApp_3SemesterApp.Views
{
    /// <summary>
    /// Logika interakcji dla klasy ProductEditView.xaml
    /// </summary>
    public partial class ProductEditView : Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId">Id of product to edit.</param>
        public ProductEditView(int productId)
        {
            InitializeComponent();

            var productService = new ProductService();
            var product = productService.Read(productId);

            var categoryService = new CategoryService();
            var category = categoryService.Read(product.CategoryId);
           
            var viewModel = new ProductViewModel(this);
            viewModel.Product = product;
            viewModel.SelectedCategory = category;

            DataContext = viewModel;
        }

        private void txtPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
