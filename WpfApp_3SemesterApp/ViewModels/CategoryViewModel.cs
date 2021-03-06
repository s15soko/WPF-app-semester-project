﻿using System;
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
        private Category _category;

        /// <summary>
        /// Contains category data displayed in view.
        /// </summary>
        public Category Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
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

        private string message;

        /// <summary>
        /// Message to show after some action.
        /// </summary>
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
            if(PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <param name="page">Page object - if we want to redirect to another view, ex. after save action.</param>
        public CategoryViewModel(Page page = null)
        {
            CategoryService = new CategoryService();
            Category = new Category();

            LoadData();

            _saveCommand = new GeneralCommand(Save);
            _updateCommand = new GeneralCommand(Update);
            _deleteCommand = new GeneralCommand(Delete);
            ViewPage = page;
        }

        //

        /// <summary>
        /// Gets data and save it to list.
        /// </summary>
        public void LoadData()
        {
            CategoriesList = new ObservableCollection<Category>(CategoryService.GetAll());
            Count = CategoryService.Count();
        }

        /// <summary>
        /// Save new entity.
        /// </summary>
        public void Save()
        {
            try
            {
                IsValid();

                var newEntity = new Category();
                newEntity.Name = Category.Name;
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

        /// <summary>
        /// Update entity.
        /// </summary>
        public void Update()
        {
            try
            {
                IsValid();

                var entity = CategoryService.Update(Category);
                if(entity == null)
                {
                    Message = "Błąd podczas aktualizacji";
                }
                else
                {
                    if (ViewPage != null)
                    {
                        ViewPage.NavigationService.Navigate(new CategoryView());
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
                if (CategoryService.CategoryIsAttachedToProduct(Category.Id))
                    throw new Exception("Kategoria jest już przypisana do produktu");

                var deleted = CategoryService.Delete(Category.Id);
                if(deleted)
                {
                    if (ViewPage != null)
                    {
                        ViewPage.NavigationService.Navigate(new CategoryView());
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
        /// Check if category data is valid.
        /// </summary>
        /// <exception cref="Exception">Whether some data is not valid.</exception>
        /// <returns>Whether data is valid.</returns>
        public bool IsValid()
        {
            if (Category.Name == null || Category.Name.Length == 0)
            {
                throw new Exception("Nazwa nie może być pusta");
            }

            var entity = CategoryService.NameExists(Category.Name);
            if (entity != null && entity.Id != Category.Id)
            {
                throw new Exception("Ta nazwa jest już zajęta");
            }

            return true;
        }
    }
}
