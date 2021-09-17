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
                .ForMember(dest => dest.Total,
                    opt => opt.MapFrom(src => src.Total))
                .ForMember(dest => dest.Last,
                    opt => opt.MapFrom(src => src.Last));

            #endregion

            #region PrizeAddViewModel 轉換成 Prize

            CreateMap<PrizeAddViewModel, Prize>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Total,
                    opt => opt.MapFrom(src => src.Total))
                .ForMember(dest => dest.Last,
                    opt => opt.MapFrom(src => src.Total));

            #endregion

            #region Prize 轉換成 PrizeEditViewModel

            CreateMap<Prize, PrizeEditViewModel>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Total,
                    opt => opt.MapFrom(src => src.Total));

            #endregion

            #region PrizeEditViewModel 轉換成 Prize

            CreateMap<PrizeEditViewModel, Prize>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Total,
                    opt => opt.MapFrom(src => src.Total))
                .ForMember(dest => dest.Last,
                    opt => opt.MapFrom(src => src.Total));

            #endregion

            #region ParticipantPrize 轉換成 PrizeSearchViewModel

            CreateMap<ParticipantPrize, PrizeSearchViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.ParticipantPrizeId))
                .ForMember(dest => dest.PrizeId,
                    opt => opt.MapFrom(src => src.PrizeId))
                .ForMember(dest => dest.ParticipantId,
                    opt => opt.MapFrom(src => src.ParticipantId));

            #endregion
        }
    }
}