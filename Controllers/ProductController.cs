using HelloWorldWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HelloWorldWebAPI.Controllers
{
    public class ProductController : ApiController
    {
        static List<Product> Products = new List<Product> { 
            new Product{ Id=1,Name="P1", Category="C1", Price=2.5M},
            new Product{Id = 2, Name = "P2", Category="C1", Price = 3.9M}
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return Products;
        }

        public IHttpActionResult GetProductById(int id)
        {
            Product product = Products.Where(p => p.Id == id).FirstOrDefault();

            if (product == null)
                return NotFound();
            else
                return Ok(product);
        }

        public HttpResponseMessage PostProduct(Product newProduct)
        {
            Products.Add(newProduct);

            var response = Request.CreateResponse<Product>(HttpStatusCode.Created, newProduct);

            response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = newProduct.Id }));
            return response;
        }

        public void PutProduct(int id, Product product)
        {
            int index = Products.FindIndex(p => p.Id == id);
            if (index != -1)
            {
                Products.RemoveAt(index);
                Products.Add(product);
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteProduct(int id)
        {
            Product product = Products.FirstOrDefault(p=>p.Id == id);

            if(product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Products.Remove(product);
        }
    }
}
