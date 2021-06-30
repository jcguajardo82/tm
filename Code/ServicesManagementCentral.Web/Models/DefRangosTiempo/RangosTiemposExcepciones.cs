using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.DefRangosTiempo
{
    public class RangosTiemposExcepciones
    {

        public int Prioridad { get; set; }
        public int cnscDef { get; set; }
        public int IdTipoCatalogo { get; set; }
        public int IdOwner { get; set; }
        public int IdSupplierWH { get; set; }
        public string SupplierName { get; set; }
        public int IdTipoLogistica { get; set; }
        public int IdTransportista { get; set; }
        public string NombreTransportista { get; set; }
        public int DiasExtra { get; set; }
        public string FechaInicioPromo { get; set; }
        public string HoraInicioPromo { get; set; }
        public string FechaFinPromo { get; set; }
        public string HoraFinPromo { get; set; }
        public string FechaCreacion { get; set; }
        public string HoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaUltModif { get; set; }
        public string HoraUltModif { get; set; }
        public string UsuarioUltModif { get; set; }

        public string BitActivo { get; set; }

    }
}