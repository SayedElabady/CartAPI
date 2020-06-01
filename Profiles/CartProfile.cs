using System.Linq;
using AutoMapper;
using WebApplication.Models;

namespace WebApplication.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartDto>();
            CreateMap<CartDto, Cart>().ForMember(
                dest => dest.ProductIds,
                opt =>
                    opt.MapFrom(src => src.Products.Select(cart => cart.Id)));
        }
    }
}