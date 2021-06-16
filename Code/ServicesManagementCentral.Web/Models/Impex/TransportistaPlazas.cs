using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Impex
{
    public class TransportistaPlazas
    {
        public int IdTransportista { get; set; }
        public int IdPlaza { get; set; }
        public string Desc_Plaza { get; set; }
        public string PostalCode { get; set; }
        public int IdTipoEnvio { get; set; }
        public bool bitDeleted { get; set; }
        public string CreatedId { get; set; }
        public string DescTipoEnvio { get; set; }
    }
}