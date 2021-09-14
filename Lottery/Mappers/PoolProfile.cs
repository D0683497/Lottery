﻿using AutoMapper;
using Lottery.Entities.Activity;
using Lottery.Models;
using Lottery.Models.Pool;

namespace Lottery.Mappers
{
    public class PoolProfile : Profile
    {
        public PoolProfile()
        {
            #region Pool 轉換成 PoolViewModel

            CreateMap<Pool, PoolViewModel>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name));

            #endregion

            #region PoolAddViewModel 轉換成 Pool

            CreateMap<PoolAddViewModel, Pool>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name));

            #endregion

            #region Pool 轉換成 PoolEditViewModel

            CreateMap<Pool, PoolEditViewModel>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name));

            #endregion

            #region PoolEditViewModel 轉換成 Pool

            CreateMap<PoolEditViewModel, Pool>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name));

            #endregion

            #region Pool 轉換成 EventStartViewModel

            CreateMap<Pool, EventStartViewModel>()
                .ForPath(dest => dest.Event,
                    opt => opt.MapFrom(src => src.Event))
                .ForPath(dest => dest.Pool,
                    opt => opt.MapFrom(src => src))
                .ForPath(dest => dest.Prizes,
                    opt => opt.MapFrom(src => src.Prizes));

            #endregion
        }
    }
}