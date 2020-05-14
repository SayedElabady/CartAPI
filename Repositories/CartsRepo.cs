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
            _dbContext.Cart.DeleteMany(Movie => true);

        public Task<Cart> Get() =>
            _dbContext.Cart.Find(Cart => true).FirstOrDefaultAsync();

        public Task<Cart> GetById(string id) =>
            _dbContext.Cart.Find<Cart>(Cart => Cart.Id == id).FirstOrDefaultAsync();

        public async Task<Cart> Create(Cart cart)
        {
            await _dbContext.Cart.InsertOneAsync(cart);
            return cart;
        }

        public async Task Update(string id, Cart cartIn) =>
            await _dbContext.Cart.ReplaceOneAsync(Movie => Movie.Id == id, cartIn);

        public void Remove(Cart cartIn) =>
            _dbContext.Cart.DeleteOne(Movie => Movie.Id == cartIn.Id);

        public void Remove(string id) =>
            _dbContext.Cart.DeleteOne(Movie => Movie.Id == id);
    }
}