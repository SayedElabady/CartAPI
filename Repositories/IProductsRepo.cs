﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IProductsRepo
    {
        public void DeleteAll();
        public Task<List<Product>> Get();
        public Task<Product> GetById(string id);
        public Task<Product> Create(Product product);
        public Task Update(string id, Product productIn);
        public Task Remove(Product productIn);
        public Task Remove(string id);
    }
}