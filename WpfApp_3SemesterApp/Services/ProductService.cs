using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfApp_3SemesterApp.Data;
using WpfApp_3SemesterApp.Models;

namespace WpfApp_3SemesterApp.Services
{
    public class ProductService : IDbContextService<Product>
    {
        public List<Product> GetAll()
        {
            var db = new ShopDbContext();
            List<Product> list = db.Set<Product>().ToList();
            return list;
        }

        public Product Create(Product entity)
        {
            var db = new ShopDbContext();
            var newEntity = db.Products.Add(entity);
            db.SaveChanges();
            return newEntity;
        }

        public Product Read(int id)
        {
            var db = new ShopDbContext();
            var result = db.Products.Find(id);
            return result;
        }

        public Product Update(Product entity)
        {
            var e = Read(entity.Id);
            if(e != null)
            {
                var db = new ShopDbContext();
                var updatedEntity = db.Products.Add(entity);
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
                db.Products.Remove(e);
                db.SaveChanges();
            }

            return true;
        }
    }
}
