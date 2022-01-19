using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Dashboard
{
    public class IndIngresosEgresos
    {
        public string TotalIngreso { get; set; }
        public string PorcIngresoPromedio { get; set; }
        public string TotalGasto { get; set; }
        public string PorcGastoPromedio { get; set; }
        public string PorcNetoPromedio { get; set; }    
    }
}