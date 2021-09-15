using AutoMapper;
using Lottery.Entities.Activity;
using Lottery.Models.Field;

namespace Lottery.Mappers
{
    public class FieldProfile : Profile
    {
        public FieldProfile()
        {
            #region Field 轉換成 FieldViewModel

            CreateMap<Field, FieldViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Key,
                    opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.Show,
                    opt => opt.MapFrom(src => src.Show))
                .ForMember(dest => dest.Security,
                    opt => opt.MapFrom(src => src.Security))
                .ForMember(dest => dest.Value,
                    opt => opt.MapFrom(src => src.Value));

            #endregion

            #region FieldAddViewModel 轉換成 Field

            CreateMap<FieldAddViewModel, Field>()
                .ForMember(dest => dest.Key,
                    opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.Show,
                    opt => opt.MapFrom(src => src.Show))
                .ForMember(dest => dest.Security,
                    opt => opt.MapFrom(src => src.Security))
                .ForMember(dest => dest.Value,
                    opt => opt.MapFrom(src => src.Value));

            #endregion

            #region Field 轉換成 FieldEditViewModel

            CreateMap<Field, FieldEditViewModel>()
                .ForMember(dest => dest.Key,
                    opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.Show,
                    opt => opt.MapFrom(src => src.Show))
                .ForMember(dest => dest.Security,
                    opt => opt.MapFrom(src => src.Security))
                .ForMember(dest => dest.Value,
                    opt => opt.MapFrom(src => src.Value));

            #endregion

            #region FieldEditViewModel 轉換成 Field

            CreateMap<FieldEditViewModel, Field>()
                .ForMember(dest => dest.Key,
                    opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.Show,
                    opt => opt.MapFrom(src => src.Show))
                .ForMember(dest => dest.Security,
                    opt => opt.MapFrom(src => src.Security))
                .ForMember(dest => dest.Value,
                    opt => opt.MapFrom(src => src.Value));

            #endregion
        }
    }
}