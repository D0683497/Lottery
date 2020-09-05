using AutoMapper;
using Lottery.Entities;
using Lottery.Models.Attendee;

namespace Lottery.Profiles
{
    public class AttendeeProfile : Profile
    {
        public AttendeeProfile()
        {
            CreateMap<Attendee, AttendeeViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.AttendeeId))
                .ForMember(dest => dest.NID,
                    opt => opt.MapFrom(src => src.AttendeeNID))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.AttendeeName))
                .ForMember(dest => dest.Department,
                    opt => opt.MapFrom(src => src.AttendeeDepartment))
                .ForMember(dest => dest.IsAwarded,
                    opt => opt.MapFrom(src => src.AttendeeIsAwarded));

            CreateMap<AttendeeAddViewModel, Attendee>()
                .ForMember(dest => dest.AttendeeNID,
                    opt => opt.MapFrom(src => src.NID))
                .ForMember(dest => dest.AttendeeName,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.AttendeeDepartment,
                    opt => opt.MapFrom(src => src.Department));

            CreateMap<Attendee, AttendeeFileViewModel>()
                .ForMember(dest => dest.NID,
                    opt => opt.MapFrom(src => src.AttendeeNID))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.AttendeeName))
                .ForMember(dest => dest.Department,
                    opt => opt.MapFrom(src => src.AttendeeDepartment))
                .ForMember(dest => dest.IsAwarded,
                    opt => opt.MapFrom(src => src.AttendeeIsAwarded ? "是" : "否"));
        }
    }
}