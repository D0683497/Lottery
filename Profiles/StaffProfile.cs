using AutoMapper;
using Lottery.Entities;
using Lottery.Models;

namespace Lottery.Profiles
{
    public class StaffProfile : Profile
    {
        public StaffProfile()
        {
            CreateMap<Staff, StaffViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.StaffId))
                .ForMember(dest => dest.NID,
                    opt => opt.MapFrom(src => src.StaffNID))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.StaffName))
                .ForMember(dest => dest.Department,
                    opt => opt.MapFrom(src => src.StaffDepartment));

            CreateMap<StaffAddViewModel, Staff>()
                .ForMember(dest => dest.StaffNID,
                    opt => opt.MapFrom(src => src.NID))
                .ForMember(dest => dest.StaffName,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.StaffDepartment,
                    opt => opt.MapFrom(src => src.Department));
        }
    }
}