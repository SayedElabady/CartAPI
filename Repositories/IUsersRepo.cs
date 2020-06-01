using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IUsersRepo
    {
        public Task<List<User>> Get();
        public Task<User> GetById(string id);
        public Task<User> FindUserByEmail(string email);
        public Task<User> Create(User user);
        public Task Update(string id, User userIn);
        public Task Remove(User userIn);
        public Task Remove(string id);
    }
}