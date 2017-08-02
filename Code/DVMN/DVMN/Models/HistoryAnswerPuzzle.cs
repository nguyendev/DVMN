using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Models
{
    public class HistoryAnswerPuzzle : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        // ID cau hoi da tra loi
        public int PuzzleID { get; set; }
        public bool IsMultiPuzzle { get; set; }
    }
}
