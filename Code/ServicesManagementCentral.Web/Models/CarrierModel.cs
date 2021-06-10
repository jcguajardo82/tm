using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models
{
    public class CarrierModel
    {

        public Int32 IdTransportista { get; set; }//int
        public string RazonSocial { get; set; }//varchar
        public bool TarifaFija { get; set; }//int
        public int Prioridad { get; set; }//smalldatetime
        public decimal NivelServicio { get; set; }//bit
        public decimal FactorPaqueteria { get; set; }//bit
        public bool Paqueteria { get; set; }//int
        public bool BigTicket { get; set; }//int
        public bool ServicioEstandar { get; set; }//int
        public bool ServicioExpress { get; set; }//int
        public decimal PorcAdicPaquete { get; set; }//bit
        public DateTime FechaCreacion { get; set; }//varchar
        public string UsuarioCreacion { get; set; }//varchar
        public DateTime FechaUltModif { get; set; }//varchar
        public string UsuarioUltModif { get; set; }//varchar
    }
}