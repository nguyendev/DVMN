using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Models.PuzzleViewModels
{
    public class SingleMultiPuzzleViewModel
    {
        public IEnumerable<SingleSinglePuzzleViewModel> listSinglePuzzle { get; set; }
        public string Title { get; set; }

        public IEnumerable<SimplePostPuzzle> ListbestPuzzle { get; set; }
    }
}
