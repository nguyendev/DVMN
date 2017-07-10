using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Models
{
    public class Comment : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public int MemberID { get; set; }
        public Member Author { get; set; }
        public int Like { get; set; }
        public int MultiPuzzleID { get; set; }
        public MultiPuzzle MultiPuzzle { get; set; }
    }
}
