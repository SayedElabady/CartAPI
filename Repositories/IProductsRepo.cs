using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IProductRepo
    {
        public void DeleteAll();
        public Task<List<Product>> Get();
        public Task<Product> GetById(string id);
        public Task<Product> Create(Product product);
        public Task Update(string id, Product ProductIn);
        public Task Remove(Product ProductIn);
        public Task Remove(string id);
    }
}