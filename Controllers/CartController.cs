﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Dtos;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private CartService _cartService;
        private ProductsService _productsService;
        private IMapper _mapper;

        public CartController(CartService cartService, ProductsService productsService, IMapper mapper)
        {
            _mapper = mapper;
            _cartService = cartService;
            _productsService = productsService;
        }

        [HttpGet()]
        public async Task<ActionResult> Get()
        {
            var cart = await _cartService.GetCart();

            if (cart == null)
                return Ok();
            var products = new List<ProductDto>();
            foreach (var productId in cart.ProductIds)
            {
                var product = await _productsService.GetProductById(productId);
                if (product != null)
                {
                    products.Add(_mapper.Map<ProductDto>(product));
                }
            }

            var mappedCart = _mapper.Map<CartDto>(cart);
            mappedCart.Products = products;

            return Ok(mappedCart);
        }

        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCart(string cartId)
        {
            var cart = await _cartService.GetCartById(cartId);
            if (cart == null)
                return NotFound();
            return Ok(_mapper.Map<CartDto>(cart));
        }

        [HttpPost]
        public async Task Post(Cart cart)
        {
            await _cartService.CreateCart(cart);
        }

        [HttpPut("product/{productId}")]
        public async Task<IActionResult> AddToCart(string productId)
        {
            var cart = await _cartService.GetCart() ?? await _cartService.CreateCart(new Cart());

            var product = await _productsService.GetProductById(productId);
            cart.ProductIds.Add(productId);
            cart.TotalPrice += product.Price;
            await _cartService.UpdateCart(cart.Id, cart);
            return Ok();
        }

        [HttpPut]
        public async Task Update(Cart cart)
        {
            await _cartService.UpdateCart(cart.Id, cart);
        }

        [HttpDelete]
        public void DeleteAll()
        {
            _cartService.DeleteAll();
        }
    }
}