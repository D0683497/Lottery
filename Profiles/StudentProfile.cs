using AutoMapper;
using Lottery.Entities;
using Lottery.Models;

namespace Lottery.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.NID,
                    opt => opt.MapFrom(src => src.StudentNID))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.StudentName))
                .ForMember(dest => dest.Department,
                    opt => opt.MapFrom(src => src.StudentDepartment));

            CreateMap<StudentAddViewModel, Student>()
                .ForMember(dest => dest.StudentNID,
                    opt => opt.MapFrom(src => src.NID))
                .ForMember(dest => dest.StudentName,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.StudentDepartment,
                    opt => opt.MapFrom(src => src.Department));
        }
    }
}