using DVMN.Models.HomeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Data
{
    public interface IHomeRepository
    {
        Task<HomeViewModel> GetIndex();
    }
}
