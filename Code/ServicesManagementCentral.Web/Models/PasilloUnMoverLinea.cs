using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models
{
    public class PasilloUnMoverLinea
    {
        public int Id_Num_UN { get; set; }
        public int Id_Cnsc_Pasillo { get; set; }
        public int Id_Num_Linea { get; set; }
        public int Num_Orden { get; set; }
      
    }
}