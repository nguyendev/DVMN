using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Models
{
    public class SinglePuzzleTag
    {
        public int SinglePuzzleID { get; set; }
        public SinglePuzzle SinglePuzzle { get; set; }
        public int TagID { get; set; }
        public Tag Tag { get; set; }
    }
}
