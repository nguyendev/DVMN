using DoVuiHaiNao.Areas.WebManager.ViewModels.DashboardViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Areas.WebManager.Data
{
    public interface IDashboardRepository
    {
        Task<DashboardViewModels> GetIndex();
    }
}
