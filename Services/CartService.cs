using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public class CartService
    {
        private readonly CartsRepo _repo;

        public CartService(CartsRepo repo)
        {
            _repo = repo;
        }

        public Task<Cart> GetCart() => _repo.Get();

        public List<Cart> GetMovies(string genre, string searchQuery)
        {
            return null;
        }

        public void DeleteAll() => _repo.DeleteAll();


        public Task<Cart> GetCartById(string id) => _repo.GetById(id);

        public async Task<Cart> CreateCart(Cart cart) => await _repo.Create(cart);

        public async Task UpdateCart(string id, Cart cart) => await _repo.Update(id, cart);


        public void RemoveMovie(Cart cart) => _repo.Remove(cart);

        public void RemoveMovieById(string id) => _repo.Remove(id);
    }
}