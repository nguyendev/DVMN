using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Models.PuzzleViewModels
{
    public class SingleSinglePuzzleViewModel
    {
        public Member Author { get; set; }
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public int? ImageID { get; set; }
        public Images Image { get; set; }
        public bool IsYesNo { get; set; }
        public IEnumerable<SinglePuzzleTag> Tags { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public int Correct { get; set; }
        public string Reason { get; set; }
        public int Views { get; set; }
        public int Like { get; set; }
        public string DateTime { get; set; }
        public float Level { get; set; }

        public bool IsAnswered { get; set; }
    }
}
