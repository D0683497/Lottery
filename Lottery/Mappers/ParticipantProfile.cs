using System.Collections.Generic;
using AutoMapper;
using Lottery.Entities.Activity;
using Lottery.Models.Participant;

namespace Lottery.Mappers
{
    public class ParticipantProfile : Profile
    {
        public ParticipantProfile()
        {
            #region Participant 轉換成 ParticipantViewModel

            CreateMap<Participant, ParticipantViewModel>();

            #endregion

            #region ParticipantAddViewModel 轉換成 Participant

            CreateMap<ParticipantAddViewModel, Participant>();

            #endregion

            #region Participant 轉換成 ParticipantEditViewModel

            CreateMap<Participant, ParticipantEditViewModel>();

            #endregion

            #region ParticipantEditViewModel 轉換成 Participant

            CreateMap<ParticipantEditViewModel, Participant>();

            #endregion

            #region EventClaim 轉換成 ParticipantAddViewModel

            CreateMap<EventClaim, ParticipantAddViewModel>()
                .ForPath(dest => dest.Field,
                    opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Value,
                    opt => opt.Ignore());

            #endregion

            #region List<ParticipantAddViewModel> 轉換成 Participant

            CreateMap<List<ParticipantAddViewModel>, Participant>()
                .ForPath(dest => dest.Claims,
                    opt => opt.MapFrom(src => src));

            #endregion

            #region ParticipantAddViewModel 轉換成 ParticipantClaim

            CreateMap<ParticipantAddViewModel, ParticipantClaim>()
                .ForMember(dest => dest.Value,
                    opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.EventClaimId,
                    opt => opt.MapFrom(src => src.Field.Id));

            #endregion

            #region Participant 轉換成 ParticipantViewModel

            CreateMap<Participant, ParticipantViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.Claims,
                    opt => opt.MapFrom(src => src.Claims));

            #endregion

            #region ParticipantClaim 轉換成 ParticipantClaimViewMode

            CreateMap<ParticipantClaim, ParticipantClaimViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Value,
                    opt => opt.MapFrom(src => src.Value))
                .ForPath(dest => dest.Field,
                    opt => opt.MapFrom(src => src.EventClaim));

            #endregion
        }
    }
}