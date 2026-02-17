using AutoMapper;
using CoffeeLocator.Application.DTOs.CoffeeShops;
using CoffeeLocator.Application.DTOs.Reviews;
using CoffeeLocator.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        
        CreateMap<CoffeeShop, CoffeeShopNearbyDto>();

        CreateMap<CoffeeShop, CoffeeShopDetailDto>()
            .ForMember(dest => dest.RecentReviews, opt => opt.MapFrom(src => src.Reviews));

        CreateMap<Review, ReviewResponseDto>();

        CreateMap<CreateCoffeeShopDto, CoffeeShop>();
        CreateMap<UpdateCoffeeShopDto, CoffeeShop>();
    }
}