using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Reportes
{
    public class NivelDeServicioIncidencias
    {
        public string Transportista { get; set; }
        public int ConsignacionesTotales { get; set; }
        public int ConsignacionesIncidencias { get; set; }
        public decimal PorcentajeIncidencias { get; set; }
        public string Tipo { get; set; }
    }
}