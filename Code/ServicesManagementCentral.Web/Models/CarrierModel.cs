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


    public class CarrierModel2
    {

        public Int32 IdTransportista { get; set; }//int
        public string Nombre { get; set; }
        public bool TarifaFija { get; set; }
        public string CostoTarifaFija { get; set; }
        public string Prioridad { get; set; }
        public string NivelServicio { get; set; }
        public string FactorPaqueteria { get; set; }
        public string LimitePaqueteria { get; set; }
        public string PorcAdicPaquete { get; set; }
        public string DiasVigenciaGuias { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaUltModif { get; set; }
        public string UsuarioUltModif { get; set; }
        public string BitActivo { get; set; }
        public string IdTipoLogistica { get; set; }
        public string FechaCreacion { get; set; }
        public string TipoLogistica { get; set; }

    }
}