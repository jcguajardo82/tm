using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Dashboard
{
    public class CumplimientoTransporte
    {
        public List<string> Tendencias { get; set; }
    }
    public class TendenciasTransporte
    {
        public DateTime Fecha { get; set; }
        public int Total { get; set; }

        public string Anio { get; set; }
        public string Mes { get; set; }
        public string Dia { get; set; }
        public string Valor { get; set; }
        public string Tendencia { get; set; }
    }
}