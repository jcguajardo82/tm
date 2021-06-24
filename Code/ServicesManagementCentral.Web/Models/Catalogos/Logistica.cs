using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Catalogos
{
    public class Logistica
    {
        public int IdTipoLogistica { get; set; }

        public int idSupplierWH { get; set; }
        public int idSupplierWHCode { get; set; }
        public bool IsChecked { get; set; }
    }
}