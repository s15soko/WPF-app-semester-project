using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfApp_3SemesterApp.Data;
using WpfApp_3SemesterApp.Models;

namespace WpfApp_3SemesterApp.Services
{
    public class CategoryService : IDbContextService<Category>
    {
        public List<Category> GetAll()
        {
            var db = new ShopDbContext();
            List<Category> list = db.Set<Category>().ToList();
            return list;
        }

        public Category Create(Category entity)
        {
            var db = new ShopDbContext();
            var newEntity = db.Categories.Add(entity);
            db.SaveChanges();
            return newEntity;
        }

        public Category Read(int id)
        {
            var db = new ShopDbContext();
            var result = db.Categories.Find(id);
            return result;
        }

        public Category Update(Category entity)
        {
            var e = Read(entity.Id);
            if(e != null)
            {
                var db = new ShopDbContext();
                var updatedEntity = db.Categories.Add(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return updatedEntity;
            }

            return null;
        }

        public bool Delete(int id)
        {
            var e = Read(id);
            if (e != null)
            {
                var db = new ShopDbContext();
                db.Categories.Remove(e);
                db.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Check if entity with this name exists.
        /// </summary>
        /// <param name="name">Entity name.</param>
        /// <returns>Entity.</returns>
        public Category NameExists(string name)
        {
            var db = new ShopDbContext();
            return db.Categories.Where(e => e.Name == name).FirstOrDefault();
        }
    }
}
