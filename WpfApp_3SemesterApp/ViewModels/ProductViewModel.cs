using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp_3SemesterApp.Commands;
using WpfApp_3SemesterApp.Models;
using WpfApp_3SemesterApp.Services;
using WpfApp_3SemesterApp.Views;

namespace WpfApp_3SemesterApp.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private Product _product;

        /// <summary>
        /// Contains product data displayed in view.
        /// </summary>
        public Product Product
        {
            get => _product;
            set
            {
                _product = value;
                OnPropertyChanged(nameof(Product));
            }
        }

        private int _count;

        /// <summary>
        /// Contains number of all categories.
        /// </summary>
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged(nameof(Count));
            }
        }

        private ObservableCollection<Category> _categoriesList;
        public ObservableCollection<Category> CategoriesList
        {
            get => _categoriesList;
            set { _categoriesList = value; OnPropertyChanged(nameof(CategoriesList)); }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        private string message;

        /// <summary>
        /// Message to show after some action.
        /// </summary>
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(nameof(Message)); }
        }

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> ProductsList
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(ProductsList));
            }
        }

        private ProductService ProductService { get; }
        private CategoryService CategoryService { get; }

        private GeneralCommand _saveCommand;
        public GeneralCommand SaveCommand
        {
            get => _saveCommand;
        }

        private GeneralCommand _updateCommand;
        public GeneralCommand UpdateCommand
        {
            get => _updateCommand;
        }

        private GeneralCommand _deleteCommand;
        public GeneralCommand DeleteCommand
        {
            get => _deleteCommand;
        }

        /// <summary>
        /// Contains page object required to redirects.
        /// </summary>
        public Page ViewPage;

        public event PropertyChangedEventHandler PropertyChanged;

        //

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <param name="page">Page object - if we want to redirect to another view, ex. after save action.</param>
        public ProductViewModel(Page page = null)
        {
            ProductService = new ProductService();
            CategoryService = new CategoryService();
            Product = new Product();

            LoadData();

            _saveCommand = new GeneralCommand(Save);
            _updateCommand = new GeneralCommand(Update);
            _deleteCommand = new GeneralCommand(Delete);
            ViewPage = page;
        }

        /// <summary>
        /// Gets data and save it to list.
        /// </summary>
        public void LoadData()
        {
            ProductsList = new ObservableCollection<Product>(ProductService.GetAllFull());
            CategoriesList = new ObservableCollection<Category>(CategoryService.GetAll());
            Count = ProductService.Count();
        }

        /// <summary>
        /// Save new entity.
        /// </summary>
        public void Save()
        {
            try
            {
                IsValid();

                var newEntity = new Product();
                newEntity.Name = Product.Name;
                newEntity.Description = Product.Description;
                newEntity.Price = Product.Price;
                newEntity.CategoryId = SelectedCategory.Id;
                newEntity.CreatedAt = DateTime.Now;

                var IsSaved = ProductService.Create(newEntity);
                if (IsSaved == null)
                {
                    Message = "Błąd w zapisie";
                }
                else
                {
                    if (ViewPage != null)
                    {
                        ViewPage.NavigationService.Navigate(new ProductView());
                    }
                    else
                    {
                        Message = "Zapisano poprawnie";
                    }
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        /// <summary>
        /// Update entity.
        /// </summary>
        public void Update()
        {
            try
            {
                IsValid();

                Product.CategoryId = SelectedCategory.Id;

                var entity = ProductService.Update(Product);
                if (entity == null)
                {
                    Message = "Błąd podczas aktualizacji";
                }
                else
                {
                    if (ViewPage != null)
                    {
                        ViewPage.NavigationService.Navigate(new ProductView());
                    }
                    else
                    {
                        Message = "Zaktualizowano poprawnie";
                    }
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        /// <summary>
        /// Delete entity.
        /// </summary>
        public void Delete()
        {
            try
            {
                var deleted = ProductService.Delete(Product.Id);
                if (deleted)
                {
                    if (ViewPage != null)
                    {
                        ViewPage.NavigationService.Navigate(new ProductView());
                    }
                    else
                    {
                        Message = "Usunięto poprawnie";
                    }

                }
                else
                {
                    Message = "Błąd podczas usuwania";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        /// <summary>
        /// Check if product data is valid.
        /// </summary>
        /// <exception cref="Exception">Whether some data is not valid.</exception>
        /// <returns>Whether data is valid.</returns>
        public bool IsValid()
        {
            if (Product.Name == null || Product.Name.Length == 0)
            {
                throw new Exception("Nazwa nie może być pusta");
            }

            if (SelectedCategory == null)
            {
                throw new Exception("Kategoria nie może być pusta");
            }

            return true;
        }
    }
}
