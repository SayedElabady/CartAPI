using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IProductsService
    {
        public Task<List<Product>> GetProductsList();

        public void DeleteAll();

        public Task<Product> GetProductById(string id);

        public Task<Product> CreateProduct(Product product);

        public Task UpdateProduct(string id, Product product);

        public Task RemoveProduct(Product product);

        public Task RemoveProductById(string id);
    }
}