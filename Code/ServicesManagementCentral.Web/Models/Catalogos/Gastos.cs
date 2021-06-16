using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Catalogos
{
    public class Gastos
    {

        public int IdGasto { get; set; }     
        public string Desc_Gasto { get; set; }
        public bool Activo { get; set; }
        public string FecCreacion { get; set; }
        public string FecMovto { get; set; }
    }
}