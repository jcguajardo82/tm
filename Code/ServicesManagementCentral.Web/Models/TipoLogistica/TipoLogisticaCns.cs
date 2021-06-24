using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.TipoLogistica
{
    public class TipoLogisticaCns
    {
        public int IdTipoLogistica { get; set; }
        public string TipoLogistica { get; set; }
        public decimal MinPesoVolumetrico { get; set; }
        public decimal MaxPesoVolumetrico { get; set; }
        public decimal MaxCosto { get; set; }
        public string TipoArticulo { get; set; }
        public string FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaUltModif { get; set; }
        public string UsuarioUltModif { get; set; }

    }
}