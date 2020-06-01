using System.Threading.Tasks;
using MongoDB.Driver;
using WebApplication.Context;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public class CartsRepo : ICartsRepo
    {
        private readonly StoreDbContext _dbContext;

        public CartsRepo(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteAll() =>
            _dbContext.Carts.DeleteMany(cart => true);

        public Task<Cart> Get() =>
            _dbContext.Carts.Find(cart => true).FirstOrDefaultAsync();

        public Task<Cart> GetById(string id) =>
            _dbContext.Carts.Find<Cart>(cart => cart.Id == id).FirstOrDefaultAsync();

        public async Task<Cart> Create(Cart cart)
        {
            await _dbContext.Carts.InsertOneAsync(cart);
            return cart;
        }

        public async Task Update(string id, Cart cartIn) =>
            await _dbContext.Carts.ReplaceOneAsync(cart => cart.Id == id, cartIn);

        public void Remove(Cart cartIn) =>
            _dbContext.Carts.DeleteOne(cart => cart.Id == cartIn.Id);

        public void Remove(string id) =>
            _dbContext.Carts.DeleteOne(cart => cart.Id == id);
    }
}