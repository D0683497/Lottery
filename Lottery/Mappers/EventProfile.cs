using AutoMapper;
using Lottery.Entities.Activity;
using Lottery.Models;
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
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.SubTitle,
                    opt => opt.MapFrom(src => src.SubTitle))
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom(src => src.Url));

            #endregion

            #region Event 轉換成 EventEditViewModel

            CreateMap<Event, EventEditViewModel>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.SubTitle,
                    opt => opt.MapFrom(src => src.SubTitle))
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom(src => src.Url));

            #endregion
            
            #region EventEditViewModel 轉換成 Event

            CreateMap<EventEditViewModel, Event>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.SubTitle,
                    opt => opt.MapFrom(src => src.SubTitle))
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom(src => src.Url));

            #endregion

            #region Event 轉換成 EventDisplayViewModel

            CreateMap<Event, EventDisplayViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.SubTitle,
                    opt => opt.MapFrom(src => src.SubTitle))
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.Pools,
                    opt => opt.MapFrom(src => src.Pools))
                .ForPath(dest => dest.Fields,
                    opt => opt.MapFrom(src => src.Fields));

            #endregion
        }
    }
}