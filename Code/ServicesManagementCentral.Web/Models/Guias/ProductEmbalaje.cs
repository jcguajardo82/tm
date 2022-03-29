using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Guias
{
    public class ProductEmbalaje
    {
        public long Barcode { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Pieces { get; set; }
    }
}