using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MongoConnector;
using WebConsoleAPI.Models;

namespace WebConsoleAPI.Controllers
{
    public class ProductsController : ApiController
    {
        private const string MongoConnectionString = @"mongodb://timonsk:password@ds033337.mongolab.com:33337/yandb";
        private const string MongoDbName = @"yandb";

        private readonly Product[] products =
        {
            new Product {Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1},
            new Product {Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M},
            new Product {Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M}
        };

        [HttpPost]
        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        [HttpPost]
        public IHttpActionResult AddProduct(Product product)
        {
            var connector = new Connector(MongoDbName, MongoConnectionString);
            connector.Create(product);
            return Ok(product);
        }

        [HttpPost]
        public IHttpActionResult UpdateProduct(Product product)
        {
            var connector = new Connector(MongoDbName, MongoConnectionString);
            connector.Update(product);
            return Ok(product);
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
