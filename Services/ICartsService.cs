using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface ICartsService
    {
        public Task<CartDto> GetCart();

        public void DeleteAll();

        public Task<Cart> GetCartById(string id);

        public Task<Cart> CreateCart(Cart cart);

        public Task UpdateCart(string productId);

        public void RemoveMovie(Cart cart);

        public void RemoveMovieById(string id);
    }
}