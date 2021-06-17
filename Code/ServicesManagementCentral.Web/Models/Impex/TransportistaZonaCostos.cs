using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Impex
{
    public class TransportistaZonaCostos
    {
        public int IdZona { get; set; }

        public string NombreZona { get; set; }
        public decimal CargoGasolina { get; set; }
        public decimal PrecioExtraPeso { get; set; }
        public decimal PrecioInicial { get; set; }
        public decimal  Otros { get; set; }
        public int IdTransportista { get; set; }
        public int IdTipoEnvio { get; set; }
        public int IdTipoServicio { get; set; }
        public int diasEntrega { get; set; }


                               
    }
}