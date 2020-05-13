using AutoMapper;
using WebApplication.Dtos;
using WebApplication.Models;

namespace WebApplication.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartDto>();
        }
    }
}