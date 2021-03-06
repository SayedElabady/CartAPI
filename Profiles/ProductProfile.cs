﻿using AutoMapper;
using WebApplication.Models;

namespace WebApplication.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}