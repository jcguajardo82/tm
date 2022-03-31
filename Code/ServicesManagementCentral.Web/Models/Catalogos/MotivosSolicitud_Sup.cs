using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Catalogos
{
    public class MotivosSolicitud_Sup
    {
        public int idMotivo { get; set; }

        public string Desc_Motivo { get; set; }
        public string Fec_Movto { get; set; }
        public string Modified_User { get; set; }
        public string BitActivo { get; set; }

    }
}