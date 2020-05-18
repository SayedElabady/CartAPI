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
            _dbContext.Products.DeleteMany(Actress => true);

        public async Task<List<Product>> Get() =>
            await _dbContext.Products.Find(Actress => true).ToListAsync();

        public Task<Product> GetById(string id)
        {
            return string.IsNullOrEmpty(id)
                ? Task.FromResult<Product>(null)
                : _dbContext.Products.Find(Actress => Actress.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> Create(Product product)
        {
            await _dbContext.Products.InsertOneAsync(product);
            return product;
        }

        public async Task Update(string id, Product ProductIn) =>
            await _dbContext.Products.ReplaceOneAsync(Movie => Movie.Id == id, ProductIn);

        public async Task Remove(Product ProductIn) =>
            await _dbContext.Products.DeleteOneAsync(Actress => Actress.Id == ProductIn.Id);

        public Task Remove(string id) =>
            _dbContext.Products.DeleteOneAsync(Actress => Actress.Id == id);
    }
}