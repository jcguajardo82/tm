using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Catalogos
{
    public class CodigosPostales_Por_Almacen
    {
        public string Id_CP { get; set; }
        public string IdOwner { get; set; }
        public string IdSupplierWH { get; set; }
        public string IdSupplierWHCode { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string CP { get; set; }
        public string Usuario_Creation { get; set; }
        public string Fecha_Creacion { get; set; }
        public string BitActivo { get; set; }
        public string Fecha_Modificacion { get; set; }
        public string Usuario_Modificacion { get; set; }
        public string TipoAlmacen { get; set; }

        //Id_CP
        //idOwner
        //idSupplierWH
        //idSupplierWHCode
        //Latitud
        //Longitud
        //CP
        //Usuario_Creation
        //Fecha_Creacion
        //BitActivo
        //Fecha_Modificacion
        //Usuario_Modificacion
    }

    public class AlmacenesCPTableType {
        public int idOwner { get; set; }
        public int idSupplierWH { get; set; }
        public int idSupplierWHCode { get; set; }
        public int CP { get; set; }
        public int IdTipoLogistica { get; set; }


    }
}