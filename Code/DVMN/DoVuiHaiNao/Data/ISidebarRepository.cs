using DoVuiHaiNao.Models.SidebarViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Data
{
    public interface ISidebarRepository
    {
        Task<AllSidebarViewModel> GetAllSibar();

        Task<IEnumerable<TopUserViewModel>> GetTopMemberPuzzle();
        Task<IEnumerable<TopTagViewModel>> GetTopTagPuzzle();

        Task<IEnumerable<TopPuzzleViewModel>> GetTopRecentPuzzle();
    }
}
