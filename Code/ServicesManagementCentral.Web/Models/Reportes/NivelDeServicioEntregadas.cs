using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Reportes
{
    public class NivelDeServicioEntregadas
    {
        public string Transportista { get; set; }
        public int ConsignacionesTotales { get; set; }
        public int ConsignacionesEntregadasEnTiempo { get; set; }
        public int ConsignacionesEntregadasFueraTiempo { get; set; }
        public decimal NivelDeServicio { get; set; }
        public decimal CostoPromedio { get; set; }
        public decimal IngresoPromedio { get; set; }
        public string Tipo { get; set; }
    }
}