using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Models.SidebarViewModels
{
    public class AllSidebarViewModel
    {
        public IEnumerable<TopUserViewModel> TopUser { get; set; }

        public IEnumerable<TopTagViewModel> TopTag { get; set; }

        public IEnumerable<TopPuzzleViewModel> RecentPuzzle { get; set; }

        public long CountSinglePuzzle { get; set; }
        public long CountMultiPuzzle { get; set; }
    }
}
