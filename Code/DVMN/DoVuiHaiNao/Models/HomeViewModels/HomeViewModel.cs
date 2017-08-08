using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Models.HomeViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<SingleViewModel> RecentPuzzle { get; set; }
        public IEnumerable<SingleViewModel> MostFavorite { get; set; }
        public IEnumerable<SingleViewModel> Mostpopular { get; set; }

    }
}
