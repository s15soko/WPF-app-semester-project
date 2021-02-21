using System.Collections.ObjectModel;
using System.ComponentModel;
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

            LoadData();
        }

        /// <summary>
        /// Gets data and save it to list.
        /// </summary>
        public void LoadData()
        {
            ProductsList = new ObservableCollection<Product>(ProductService.GetAll());
        }
    }
}
