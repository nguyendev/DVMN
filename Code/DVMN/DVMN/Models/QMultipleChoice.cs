using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Models
{
    public class QMultipleChoice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ID { get; set; }

        [Required]
        public int Number { get; set; }

        public string Image { get; set; }

        [Required]
        public int QuestionID { get; set; }

        [Required]
        public string Title { get; set; }

        public string FirstAnswer { get; set; }

        public string SecondAnswer { get; set; }
        public string ThirdAnswer { get; set; }
        public string FourthAnswer { get; set; }

        public int CorrectAnswer { get; set; }
        [Required]
        public bool YesNoQuestion { get; set; }

        public string Reason { get; set; }
        [Required]
        public int PointDetailsID { get; set; }

        public Question Question { get; set; }
        public PointDetails PointDetails { get; set; }
    }
}
