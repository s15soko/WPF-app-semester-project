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
using WpfApp_3SemesterApp.Services;
using WpfApp_3SemesterApp.ViewModels;

namespace WpfApp_3SemesterApp.Views
{
    /// <summary>
    /// Logika interakcji dla klasy CategoryEditView.xaml
    /// </summary>
    public partial class CategoryEditView : Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId">Id of category to edit.</param>
        public CategoryEditView(int categoryId)
        {
            InitializeComponent();

            var categoryService = new CategoryService();
            var category = categoryService.Read(categoryId);            
            
            var viewModel = new CategoryViewModel(this);
            viewModel.Category = category;
            
            DataContext = viewModel;
        }
    }
}
