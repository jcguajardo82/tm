using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Impex
{
    public class TransportistaPlazasDestinos
    {
        public int IdTransportista { get; set; }
        public string Cve_Plaza { get; set; }
        public bool EsOrigen { get; set; }
        public bool EsDestino { get; set; }
        public string CreatedId { get; set; }

   
    }
}