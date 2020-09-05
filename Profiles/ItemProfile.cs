using AutoMapper;
using Lottery.Entities;
using Lottery.Models.Item;

namespace Lottery.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.ItemName));

            CreateMap<ItemAddViewModel, Item>()
                .ForMember(dest => dest.ItemName,
                    opt => opt.MapFrom(src => src.Name));
        }
    }
}