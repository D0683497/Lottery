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
        }
    }
}