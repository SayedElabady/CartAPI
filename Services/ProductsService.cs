using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public class ProductsService
    {
        private readonly ProductsRepo _repo;

        public ProductsService(ProductsRepo repo)
        {
            _repo = repo;
        }

        public async Task<List<Product>> GetProductsList() => await _repo.Get();

        public void DeleteAll() => _repo.DeleteAll();

        public Task<Product> GetProductById(string id) => _repo.GetById(id);

        public async Task<Product> CreateProduct(Product product) => await _repo.Create(product);

        public async Task UpdateProduct(string id, Product product) => await _repo.Update(id, product);

        public async Task RemoveProduct(Product product) => await _repo.Remove(product);

        public Task RemoveProductById(string id) => _repo.Remove(id);
    }
}