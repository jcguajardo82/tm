using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Catalogos
{
    public class TipoEntregaSETC
    {
        public int IdTipoEntrega { get; set; }
        public int StoreNum { get; set; }
        public string Desc_UN { get; set; }
        public int IdTipoEnvio { get; set; }
        public string Desc_TipoEnvio { get; set; }
        public string BitActivo { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaCreacion { get; set; }
        public string HoraCreacion { get; set; }
        public string UsuarioUltModif { get; set; }
        public string FechaUltModif { get; set; }
        public string HoraUltModif { get; set; }

    }
}