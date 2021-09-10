using AutoMapper;
using Lottery.Entities.Activity;
using Lottery.Models.Prize;

namespace Lottery.Mappers
{
    public class PrizeProfile : Profile
    {
        public PrizeProfile()
        {
            #region Prize 轉換成 PrizeViewModel

            CreateMap<Prize, PrizeViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Quantity,
                    opt => opt.MapFrom(src => src.Quantity));

            #endregion

            #region PrizeAddViewModel 轉換成 Prize

            CreateMap<PrizeAddViewModel, Prize>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Quantity,
                    opt => opt.MapFrom(src => src.Quantity));

            #endregion

            #region Prize 轉換成 PrizeEditViewModel

            CreateMap<Prize, PrizeEditViewModel>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Quantity,
                    opt => opt.MapFrom(src => src.Quantity));

            #endregion

            #region PrizeEditViewModel 轉換成 Prize

            CreateMap<PrizeEditViewModel, Prize>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Quantity,
                    opt => opt.MapFrom(src => src.Quantity));

            #endregion
        }
    }
}