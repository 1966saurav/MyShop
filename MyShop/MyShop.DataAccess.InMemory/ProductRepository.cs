using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository() {

            products = cache["products"] as List<Product>;

            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }
        public void update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);

            if (product != null) {
                productToUpdate = product;
            }

            else
            {
                throw new Exception("Product is not Found";
            }
        }

        public Product find(String Id)
        {
            Product product = products.Find(p => p.Id == Id);

            if (product != null)
            {
                return product;
            }

            else
            {
                throw new Exception("Product is not Found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null) {
                products.Remove(productToDelete);
            }

            else
            {
                throw new Exception("Product is not Found";
            }
        }
    }
}



