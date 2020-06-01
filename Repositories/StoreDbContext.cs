using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApplication.Models;

namespace WebApplication.Context
{
    public class StoreDbContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public readonly IMongoCollection<Product> Products;

        public readonly IMongoCollection<Cart> Carts;

        public readonly IMongoCollection<User> Users;

        public StoreDbContext(IOptions<ProductStoreDatabaseSettings> mongoOptions)
        {
            var client = new MongoClient(mongoOptions.Value.ConnectionString);
            _mongoDatabase = client.GetDatabase(mongoOptions.Value.DatabaseName);
            Products = _mongoDatabase.GetCollection<Product>("Products");
            Carts = _mongoDatabase.GetCollection<Cart>("Carts");
            Users = _mongoDatabase.GetCollection<User>("Users");

        }
    }
}