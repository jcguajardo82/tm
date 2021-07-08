using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Catalogos
{
    public class TipoVehiculo
    {
        public int Id_TipoVehiculo { get; set; }
        public string Descipcion { get; set; }
        public string Estatus { get; set; }
        public string Fec_Movto { get; set; }
        public string Modified_User { get; set; }
        public string Comentarios { get; set; }


    }
}