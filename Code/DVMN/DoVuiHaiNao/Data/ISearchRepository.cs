using DoVuiHaiNao.Models.HomeViewModels;
using DoVuiHaiNao.Models.SearchViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Data
{
    public interface ISearchRepository
    {
        Task<SearchViewModel> GetSearch(string search, int? page, int? pageSize);
    }
}
