using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Catalogos
{
    public class almacenTMS
    {
        public int idOwner { get; set; }
        public int idSupplierWH { get; set; }
        public int idSupplierWHCode { get; set; }
        public bool ServicioEstandar { get; set; }
        public bool ServicioExpress { get; set; }
        public int TiempoSurtido { get; set; }
        public bool BitActivo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaUltModif { get; set; }
        public string UsuarioUltModif { get; set; }
        public string OwnerName { get; set; }
        public string SupplierName { get; set; }
        public string SupplierWHName { get; set; }


    }
}