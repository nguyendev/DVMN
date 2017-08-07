using DVMN.Models.HomeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Models.SearchViewModel
{
    public class SearchViewModel
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public int PageSize { get; set; }
        public int Count { get; set; }
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public IEnumerable<SingleViewModel> List { get; set; }
    }
}
