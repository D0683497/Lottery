using AutoMapper;
using Lottery.Entities.Activity;
using Lottery.Models.Event;

namespace Lottery.Mappers
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            #region EventAddViewModel 轉換成 Event

            CreateMap<EventAddViewModel, Event>()
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.SubTitle,
                    opt => opt.MapFrom(src => src.SubTitle))
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom(src => src.Url));

            #endregion

            #region Event 轉換成 EventViewModel

            CreateMap<Event, EventViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.End,
                    opt => opt.MapFrom(src => src.End))
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.SubTitle,
                    opt => opt.MapFrom(src => src.SubTitle))
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom(src => src.Url));

            #endregion
        }
    }
}