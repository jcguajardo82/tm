using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Impex
{
    public class TransportistaPlazas
    {
        public int IdTransportista { get; set; }
        public string Cve_Plaza { get; set; }
        public string PostalCode { get; set; }
        public int IdTipoEnvio { get; set; }
        public bool bitDeleted { get; set; }
        public string CreatedId { get; set; }
    }

    public class TransportistaPlazasShow
    {
        public string Transportista { get; set; }
        public string Plaza { get; set; }
        public string DescTipoEnvio { get; set; }
        public string PostalCode { get; set; }
        public string CreatedId { get; set; }
        public string CreatedDate { get; set; }

    }

}