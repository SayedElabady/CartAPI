using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public class CartsService : ICartsService
    {
        private readonly ICartsRepo _repo;
        private readonly IProductsRepo _productsRepo;
        private readonly IMapper _mapper;

        public CartsService(ICartsRepo repo, IProductsRepo productsRepo, IMapper mapper)
        {
            _repo = repo;
            _productsRepo = productsRepo;
            _mapper = mapper;
        }

        public async Task<CartDto> GetCart()
        {
            var products = new List<ProductDto>();
            var cart = await _repo.Get();
            foreach (var productId in cart.ProductIds)
            {
                var product = await _productsRepo.GetById(productId);

                if (product != null)
                {
                    products.Add(_mapper.Map<ProductDto>(product));
                }
            }

            var mappedCart = _mapper.Map<CartDto>(cart);
            mappedCart.Products = products;
            return mappedCart;
        }

        public void DeleteAll() => _repo.DeleteAll();

        public Task<Cart> GetCartById(string id) => _repo.GetById(id);

        public async Task<Cart> CreateCart(Cart cart) => await _repo.Create(cart);

        public async Task UpdateCart(string id)
        {
            var searchedCart = _mapper.Map<Cart>(await GetCart()) ?? await CreateCart(new Cart());

            var product = await _productsRepo.GetById(id);
            searchedCart.ProductIds.Add(id);
            searchedCart.TotalPrice += product.Price;
            await _repo.Update(searchedCart.Id, searchedCart);
        }

        public void RemoveMovie(Cart cart) => _repo.Remove(cart);

        public void RemoveMovieById(string id) => _repo.Remove(id);
    }
}