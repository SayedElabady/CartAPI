﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly IMapper _mapper;

        public ProductsController(IProductsService productsService, IMapper mapper)
        {
            _mapper = mapper;
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            var products = await _productsService.GetProductsList();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [HttpPost]
        public Task Post(Product product)
        {
            return _productsService.CreateProduct(product);
        }

        [HttpDelete]
        public Task DeleteById(string productId)
        {
            return _productsService.RemoveProductById(productId);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult> GetProduct(string productId)
        {
            var product = await _productsService.GetProductById(productId);
            return Ok(_mapper.Map<ProductDto>(product));
        }
    }
}