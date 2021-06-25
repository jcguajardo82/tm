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
        public string IdTransportista { get; set; }
        public string NomTransportista { get; set; }
        public int IdPlaza { get; set; }
        public string CvePlaza { get; set; }
        public string NomPlaza { get; set; }
        public string DescTipoEnvio { get; set; }
        public string PostalCode { get; set; }
        public string CreatedId { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedTime { get; set; }
        public string BitActivo { get; set; }
    }

}