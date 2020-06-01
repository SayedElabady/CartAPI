using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using WebApplication.Context;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public class UsersRepo : IUsersRepo
    {
        private readonly StoreDbContext _dbContext;

        public UsersRepo(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> Get() =>
            await _dbContext.Users.Find(user => true).ToListAsync();

        public Task<User> GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> FindUserByEmail(string email)
            => _dbContext.Users.Find(user => user.Email == email).FirstOrDefaultAsync();


        public Task<User> Create(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(string id, User userIn)
        {
            throw new System.NotImplementedException();
        }

        public Task Remove(User userIn)
        {
            throw new System.NotImplementedException();
        }

        public Task Remove(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}