using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Reportes
{
    public class NivelDeServicioModel
    {
        public string UeNo { get; set; }
        public long OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string UeType { get; set; }
        public string TrackingServiceStatus { get; set; }
        public DateTime TrackingDate { get; set; }
        public string Transportista { get; set; }
        public int TiempoSurtido { get; set; }
        public DateTime RecoleccionEstimada { get; set; }
        public bool RecoleccionAtiempo { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime fechaLimite { get; set; }
        public decimal Costo { get; set; }
        public decimal totalIngreso { get; set; }
        public bool PagoAutorizado { get; set; }
        public bool EntregadaATiempo { get; set; }

    }
}