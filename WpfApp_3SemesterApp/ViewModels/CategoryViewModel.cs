using System.Collections.ObjectModel;
using System.ComponentModel;
using WpfApp_3SemesterApp.Models;
using WpfApp_3SemesterApp.Services;

namespace WpfApp_3SemesterApp.ViewModels
{
    public class CategoryViewModel : INotifyPropertyChanged
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

        private ObservableCollection<Category> _categories;
        public ObservableCollection<Category> CategoriesList 
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(CategoriesList));
            }
        }

        private CategoryService CategoryService { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        //

        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public CategoryViewModel()
        {
            CategoryService = new CategoryService();

            LoadData();
        }

        /// <summary>
        /// Gets data and save it to list.
        /// </summary>
        public void LoadData()
        {
            CategoriesList = new ObservableCollection<Category>(CategoryService.GetAll());
        }
    }
}
