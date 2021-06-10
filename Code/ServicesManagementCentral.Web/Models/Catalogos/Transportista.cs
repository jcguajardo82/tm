using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Catalogos
{
    public class Transportista
    {
        public int IdTransportista { get; set; }
        public string Nombre { get; set; }
        public string TarifaFija { get; set; }
        public decimal CostoTarifaFija { get; set; }
        public string Prioridad { get; set; }
        public decimal NivelServicio { get; set; }
        public decimal FactorPaqueteria { get; set; }
        public decimal LimitePaqueteria { get; set; }
        public decimal PorcAdicPaquete { get; set; }
        public int DiasVigenciaGuias { get; set; }

    }
}