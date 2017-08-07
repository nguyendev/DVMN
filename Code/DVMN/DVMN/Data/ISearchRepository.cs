using DVMN.Models.HomeViewModels;
using DVMN.Models.SearchViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Data
{
    public interface ISearchRepository
    {
        Task<SearchViewModel> GetSearch(string search, int? page, int? pageSize);
    }
}
