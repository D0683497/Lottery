using AutoMapper;
using Lottery.Entities.Activity;
using Lottery.Models.Winner;

namespace Lottery.Mappers
{
    public class WinnerProfile : Profile
    {
        public WinnerProfile()
        {
            #region ParticipantPrize 轉換成 WinnerViewModel

            CreateMap<ParticipantPrize, WinnerViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.ParticipantPrizeId))
                .ForPath(dest => dest.Participant,
                    opt => opt.MapFrom(src => src.Participant));

            #endregion
        }
    }
}