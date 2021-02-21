using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp_3SemesterApp.Commands;
using WpfApp_3SemesterApp.Models;
using WpfApp_3SemesterApp.Services;

namespace WpfApp_3SemesterApp.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
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

        public event PropertyChangedEventHandler PropertyChanged;

        //

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ProductViewModel()
        {
            ProductService = new ProductService();
            CategoryService = new CategoryService();

            LoadData();

            _saveCommand = new GeneralCommand(Save);
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
                if(Name == null || Name.Length == 0)
                {
                    throw new Exception("Nazwa nie może być pusta");
                }

                var newEntity = new Product();
                newEntity.Name = Name;
                newEntity.Description = Description;
                newEntity.Price = Price;
                newEntity.CategoryId = SelectedCategory.Id;
                newEntity.CreatedAt = DateTime.Now;

                var IsSaved = ProductService.Create(newEntity);
                if (IsSaved == null)
                {
                    Message = "Błąd w zapisie";
                }
                else
                {
                    Message = "Zapisano poprawnie";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
    }
}
