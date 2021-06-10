using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Catalogos
{
    public class Paqueteria
    {
        public int Id_paqueteria { get; set; }
        public int Id_Proveedor { get; set; }
        public decimal Peso { get; set; }
        public string Nombre { get; set; }
        //           
    }
}