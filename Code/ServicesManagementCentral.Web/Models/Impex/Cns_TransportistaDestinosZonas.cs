using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Impex
{
    public class Cns_TransportistaDestinosZonas
    {
        public int IdTransportista { get; set; }
        public int IdPlazaOrigen { get; set; }
        public int IdPlazaDestino { get; set; }
        public int IdZona { get; set; }
        public string CreatedId { get; set; }
        public string CreatedDate { get; set; }
    }
}