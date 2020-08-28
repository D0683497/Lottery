using AutoMapper;
using Lottery.Entities;
using Lottery.Models;

namespace Lottery.Profiles
{
    public class PrizeProfile : Profile
    {
        public PrizeProfile()
        {
            CreateMap<Prize, PrizeViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.PrizeId))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.PrizeName))
                .ForMember(dest => dest.Image,
                    opt => opt.MapFrom(src => src.PrizeImage))
                .ForMember(dest => dest.Number,
                    opt => opt.MapFrom(src => src.PrizeNumber))
                .ForMember(dest => dest.Complete,
                    opt => opt.MapFrom(src => src.PrizeComplete))
                .ForMember(dest => dest.Order,
                    opt => opt.MapFrom(src => src.PrizeOrder));

            CreateMap<PrizeAddViewModel, Prize>()
                .ForMember(dest => dest.PrizeName,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PrizeImage,
                    opt => opt.MapFrom(src => (string.IsNullOrEmpty(src.Image) ? null : src.Image)))
                .ForMember(dest => dest.PrizeNumber,
                    opt => opt.MapFrom(src => (src.Number == 0 || src.Number == null) ? 1 : src.Number))
                .ForMember(dest => dest.PrizeOrder,
                    opt => opt.MapFrom(src => (src.Order == 0 || src.Order == null) ? 1 : src.Order));
        }
    }
}