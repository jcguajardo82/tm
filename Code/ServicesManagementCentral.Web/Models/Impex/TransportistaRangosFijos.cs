using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Impex
{
    public class TransportistaRangosFijos
    {
        public int IdTransportista { get; set; }
        public int IdZona { get; set; }
        public decimal PesoInicio { get; set; }
        public decimal PesoFin { get; set; }
        public decimal CostoFijo { get; set; }
        public bool bitDeleted { get; set; }
        public string CreatedId { get; set; }
        public int TiempoEnvio { get; set; }

    }

    public class TransportistaRangosFijosShow: TransportistaRangosFijos
    {
        public string NombreTransportista { get; set; }
        public string Desc_Zona { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedTime { get; set; }
        public string bitActivo { get; set; }

    }
}