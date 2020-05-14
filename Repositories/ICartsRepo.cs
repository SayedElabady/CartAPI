using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface ICartsRepo
    {
        public void DeleteAll();

        public Task<Cart> Get();

        public Task<Cart> GetById(string id);

        public Task<Cart> Create(Cart cart);

        public Task Update(string id, Cart cartIn);

        public void Remove(Cart cartIn);

        public void Remove(string id);
    }
}