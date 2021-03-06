﻿using System;
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
                db.Products.Attach(e);
                db.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }

            return true;
        }

        //

        /// <summary>
        /// Gets all products together with categories.
        /// </summary>
        /// <returns>List of objects.</returns>
        public List<Product> GetAllFull()
        {
            var db = new ShopDbContext();
            var query = from product in db.Products
                         from category in db.Categories.Where(category => category.Id == product.CategoryId).DefaultIfEmpty() 
                         select new
                         {
                             Product = product,
                             Category = category
                         };

            List<Product> list = new List<Product>();

            foreach(var r in query)
            {
                r.Product.Category = r.Category;
                list.Add(r.Product);
            }

            return list;
        }

        /// <summary>
        /// Count products.
        /// </summary>
        /// <returns>Number of products.</returns>
        public int Count()
        {
            var db = new ShopDbContext();
            return db.Products.Count();
        }
    }
}
