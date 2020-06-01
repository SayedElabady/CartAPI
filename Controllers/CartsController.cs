using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartsController : ControllerBase
    {
        private readonly ICartsService _cartsService;
        private readonly IProductsService _productsService;
        private readonly IMapper _mapper;

        public CartsController(ICartsService cartsService, IProductsService productsService, IMapper mapper)
        {
            _mapper = mapper;
            _cartsService = cartsService;
            _productsService = productsService;
        }

        //needs refactoring 
        [HttpGet()]
        public async Task<ActionResult> Get()
        {
            return Ok(await _cartsService.GetCart());
        }

        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCart(string cartId)
        {
            var cart = await _cartsService.GetCartById(cartId);
            if (cart == null)
                return NotFound();
            return Ok(_mapper.Map<CartDto>(cart));
        }

        [HttpPost]
        public async Task Post(Cart cart)
        {
            await _cartsService.CreateCart(cart);
        }

        [HttpPut("product/{productId}")]
        public async Task<IActionResult> AddToCart(string productId)
        {
      
            await _cartsService.UpdateCart(productId);
            return Ok();
        }


        [HttpDelete]
        public void DeleteAll()
        {
            _cartsService.DeleteAll();
        }
    }
}