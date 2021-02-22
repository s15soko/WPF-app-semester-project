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
        public Product Product
        {
            get => _product;
            set
            {
                _product = value;
                OnPropertyChanged(nameof(Product));
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
        }

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

        public void Update()
        {
            try
            {
                IsValid();

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
