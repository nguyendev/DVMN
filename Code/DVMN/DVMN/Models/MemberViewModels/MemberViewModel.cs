using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Models.MemberViewModels
{
    public class ListMemberViewModel
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

        public IEnumerable<MemberViewModel> List { get; set; }
    }

    public class MemberViewModel
    {
        public string FullName { get; set;}

        public string Slug { get; set; }

        public int Score { get; set; }

        public string Image { get; set; }

        public string Facebook { get; set; }
    }
}
