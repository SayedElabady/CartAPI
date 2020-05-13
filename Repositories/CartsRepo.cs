using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public class CartsRepo
    {
        private readonly IMongoCollection<Cart> _cart;

        public CartsRepo(IMoviestoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _cart = database.GetCollection<Cart>(settings.MoviesCollectionName);
        }

        public void DeleteAll() =>
            _cart.DeleteMany(Movie => true);

        public Task<Cart> Get() =>
            _cart.Find(Movie => true).FirstOrDefaultAsync();
        
        public  Task<Cart> GetById(string id) =>
            _cart.Find<Cart>(Movie => Movie.Id == id).FirstOrDefaultAsync();

        public async Task<Cart> Create(Cart cart)
        {
            await _cart.InsertOneAsync(cart);
            return cart;
        }

        public async Task Update(string id, Cart cartIn) =>
            await _cart.ReplaceOneAsync(Movie => Movie.Id == id, cartIn);

        public void Remove(Cart cartIn) =>
            _cart.DeleteOne(Movie => Movie.Id == cartIn.Id);

        public void Remove(string id) =>
            _cart.DeleteOne(Movie => Movie.Id == id);
    }
}