using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Dashboard
{
    public class MercanciasGenerales
    {
        public string TotalPend { get; set; }
        public string TotalTransito { get; set; }
        public string TotalEnt { get; set; }
        public string TotalTiempo { get; set; }
        public string TotalFueraTiempo { get; set; }
        public string PorcPend { get; set; }
        public string PorcTransito { get; set; }
        public string PorcEnTiempo { get; set; }
        public string PorcFueraTiempo { get; set; }
        public string PorcEntregadas { get; set; }
    }
}