using DoVuiHaiNao.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Models
{
    public class PointType : Base
    {
        public int ID { get; set; }
        public int Name { get; set; }
        public TypeQuestion? Type { get; set; }
    }
}