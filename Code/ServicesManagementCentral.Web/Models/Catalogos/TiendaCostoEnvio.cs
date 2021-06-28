using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Catalogos
{
    public class TiendaCostoEnvio
    {
        public int IdConsecutivo { get; set; }
        public string StoreNum { get; set; }
        public string NomTienda { get; set; }
        public string Direccion { get; set; }
        public int IdTipoFormato { get; set; }
        public string Desc_TipoFormato { get; set; }
        public string Director { get; set; }
        public string Region { get; set; }
        public string CostoEnvio { get; set; }
        public string BitActivo { get; set; }
        public string FechaCreacion { get; set; }
        public string HoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaUltModif { get; set; }
        public string HoraUltModif { get; set; }
        public string UsuarioUltModif { get; set; }



    }
}