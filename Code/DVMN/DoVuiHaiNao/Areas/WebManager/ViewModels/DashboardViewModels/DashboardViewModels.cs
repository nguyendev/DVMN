using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Areas.WebManager.ViewModels.DashboardViewModels
{
    public class DashboardViewModels
    {
        public int CountMemberInDay { get; set; }
        public int CountMemberInWeek { get; set; }
        public int CountMemberInMonth { get; set; }
        public int CountMemberTotal { get; set; }

        public int CountSingleQuestionInDay { get; set; }
        public int CountSingleQuestionInWeek { get; set; }
        public int CountSingleQuestionInMonth { get; set; }
        public int CountSingleQuestionTotal { get; set; }

        public int CountMultiQuestionInDay { get; set; }
        public int CountMultiQuestionInWeek { get; set; }
        public int CountMultiQuestionInMonth { get; set; }
        public int CountMultiQuestionTotal { get; set; }

        public int CountLikePuzzleInDay { get; set; }
        public int CountLikePuzzleInWeek { get; set; }
        public int CountLikePuzzleInMonth { get; set; }
        public int CountLikePuzzleTotal { get; set; }

        public int CountViewPuzzleInDay { get; set; }
        public int CountViewPuzzleInWeek { get; set; }
        public int CountViewPuzzleInMonth { get; set; }
        public int CountViewPuzzleTotal { get; set; }
    }
}
