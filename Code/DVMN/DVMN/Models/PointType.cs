using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Models
{
    public class PointType : Base
    {
        public int ID { get; set; }
        public int Description { get; set; }

        public enum Type
        {
            MultipleChoice = 1,
            MultipleWithImage = 2
        }
    }
}
