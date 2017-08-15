using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Models
{
    public class HistoryLikePuzzle : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ID { get; set; }
        // ID cau hoi da tra loi
        public int PuzzleID { get; set; }
        public bool IsMultiPuzzle { get; set; }
    }
}
