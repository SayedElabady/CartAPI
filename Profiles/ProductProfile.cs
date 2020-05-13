using AutoMapper;
using WebApplication.Dtos;
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