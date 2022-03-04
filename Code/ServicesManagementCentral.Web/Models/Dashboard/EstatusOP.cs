using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Dashboard
{
    public class EstatusOP
    {//  
        public int TotalTransito { get; set; }
        public int TotalPend { get; set; }
        public int TotalEnt { get; set; }
        public decimal PorcPend { get; set; }
        public decimal PorcTransito { get; set; }
        public decimal PorcEntregadas { get; set; }
      
    }

    public class Combo
    {
        public string Value { get; set; }
        public string Text { get; set; }

        public bool Selected { get; set; } = false;
    }
}