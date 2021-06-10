using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models
{
    public class TiendaModel
    {
        public Int32 IdUnidadNegocio { get; set; }//int
        public string UnidadNegocio { get; set; }//varchar
        public bool Paqueteria { get; set; }//int
        public bool BigTicket { get; set; }//int
        public bool ServicioEstandar { get; set; }//int
        public bool ServicioExpress { get; set; }//int
        public DateTime FechaCreacion { get; set; }//varchar
        public string UsuarioCreacion { get; set; }//varchar
        public DateTime FechaUltModif { get; set; }//varchar
        public string UsuarioUltModif { get; set; }//varchar
    }
}