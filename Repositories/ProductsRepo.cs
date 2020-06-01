using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using WebApplication.Context;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public class ProductsRepo : IProductsRepo
    {
        private readonly StoreDbContext _dbContext;

        public ProductsRepo(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteAll() =>
            _dbContext.Products.DeleteMany(product => true);

        public async Task<List<Product>> Get() =>
            await _dbContext.Products.Find(product => true).ToListAsync();

        public Task<Product> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Task.FromResult<Product>(null);
            }

            return  _dbContext.Products.Find(product => product.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> Create(Product product)
        {
            if (product == null)
            {
                return await Task.FromResult<Product>(null);
            }

            await _dbContext.Products.InsertOneAsync(product);
            return product;
        }

        public async Task Update(string id, Product productIn) =>
            await _dbContext.Products.ReplaceOneAsync(product => product.Id == id, productIn);

        public async Task Remove(Product productIn) =>
            await _dbContext.Products.DeleteOneAsync(product => product.Id == productIn.Id);

        public Task Remove(string id) =>
            _dbContext.Products.DeleteOneAsync(product => product.Id == id);
    }
}