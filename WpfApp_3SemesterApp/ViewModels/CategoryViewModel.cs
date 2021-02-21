using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using WpfApp_3SemesterApp.Commands;
using WpfApp_3SemesterApp.Models;
using WpfApp_3SemesterApp.Services;
using WpfApp_3SemesterApp.Views;

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

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(nameof(Message)); }
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

        private GeneralCommand _saveCommand;
        public GeneralCommand SaveCommand
        {
            get => _saveCommand;
        }

        public Page ViewPage;

        public event PropertyChangedEventHandler PropertyChanged;

        //

        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public CategoryViewModel(Page page = null)
        {
            CategoryService = new CategoryService();

            LoadData();

            _saveCommand = new GeneralCommand(Save);
            ViewPage = page;
        }

        //

        /// <summary>
        /// Gets data and save it to list.
        /// </summary>
        public void LoadData()
        {
            CategoriesList = new ObservableCollection<Category>(CategoryService.GetAll());
        }

        public void Save()
        {
            try
            {
                if (Name == null || Name.Length == 0)
                {
                    throw new Exception("Nazwa nie może być pusta");
                }

                var newEntity = new Category();
                newEntity.Name = Name;
                newEntity.CreatedAt = DateTime.Now;

                var IsSaved = CategoryService.Create(newEntity);
                if (IsSaved == null)
                {
                    Message = "Błąd w zapisie";
                } 
                else
                {
                    if(ViewPage != null)
                    {
                        ViewPage.NavigationService.Navigate(new CategoryView());
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
    }
}
