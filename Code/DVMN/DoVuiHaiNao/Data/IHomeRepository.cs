using DoVuiHaiNao.Models.HomeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Data
{
    public interface IHomeRepository
    {
        Task<HomeViewModel> GetIndex();
    }
}
