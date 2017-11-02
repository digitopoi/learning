using SampleWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleWebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        //  Define the products list
        List<Product> products = new List<Product>();

        /// <summary>
        /// Web API method to return a list of products
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetAllProducts()
        {
            GetProducts();
            return products;
        }

        private void GetProducts()
        {
            products.Add(new Product { Id = 1, Name = "Television", Category = "Electronic", Price = 82000 });
            products.Add(new Product { Id = 2, Name = "Refrigerator", Category = "Electronic", Price = 23000 });
            products.Add(new Product { Id = 3, Name = "Mobiles", Category = "Electronic", Price = 20000 });
            products.Add(new Product { Id = 4, Name = "Laptops", Category = "Electronic", Price = 45000 });
            products.Add(new Product { Id = 5, Name = "iPads", Category = "Electronic", Price = 67000 });
            products.Add(new Product { Id = 6, Name = "Toys", Category = "Gift Items", Price = 15000 });
        }

        /// <summary>
        /// Web API method to return selected product based on the passed id
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts(int selectedId)
        {
            if(products.Count() > 0)
            {
                return products.Where(p => p.Id == selectedId);
            }
            else
            {
                GetProducts();
                return products.Where(p => p.Id == selectedId);
            }
        }

    }
}
