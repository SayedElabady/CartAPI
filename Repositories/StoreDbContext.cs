using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApplication.Models;

namespace WebApplication.Context
{
    public class StoreDbContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        
        public readonly IMongoCollection<Product> Products;

        public readonly IMongoCollection<Cart> Cart;
        public StoreDbContext(IOptions<ProductStoreDatabaseSettings> mongoOptions)
        {
            var client = new MongoClient(mongoOptions.Value.ConnectionString);
            _mongoDatabase = client.GetDatabase(mongoOptions.Value.DatabaseName);
            Products = _mongoDatabase.GetCollection<Product>("Products");
            Cart = _mongoDatabase.GetCollection<Cart>("Carts");
        }
    }
}