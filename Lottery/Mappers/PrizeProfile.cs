using AutoMapper;
using Lottery.Entities.Activity;
using Lottery.Models.Prize;

namespace Lottery.Mappers
{
    public class PrizeProfile : Profile
    {
        public PrizeProfile()
        {
            #region Prize 轉換成 PrizeViewModel

            CreateMap<Prize, PrizeViewModel>();

            #endregion

            #region PrizeAddViewModel 轉換成 Prize

            CreateMap<PrizeAddViewModel, Prize>();

            #endregion

            #region Prize 轉換成 PrizeEditViewModel

            CreateMap<Prize, PrizeEditViewModel>();

            #endregion

            #region PrizeEditViewModel 轉換成 Prize

            CreateMap<PrizeEditViewModel, Prize>();

            #endregion
        }
    }
}