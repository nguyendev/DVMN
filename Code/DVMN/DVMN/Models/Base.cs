﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Models
{
    public class Base
    {
        public DateTime? CreateDT { get; set; }
        public DateTime? UpdateDT { get; set; }
        public string MemberID { get; set; }
        public Member Member { get; set; }
        public string Approved { get; set; }
        public string Active { get; set; }
        public bool IsDeleted { get; set; }
        public string Note { get; set; }



    }
}
