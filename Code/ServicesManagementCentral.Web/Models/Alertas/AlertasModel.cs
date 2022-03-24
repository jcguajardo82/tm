using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Alertas
{
    public class PendienteRecoleccionModel
    {
        public string DiasRetrasoRecoleccion { get; set; }
        public string Almacen_Proveedor { get; set; }
        public string Consignacion { get; set; }
        public string GuiaTransporte { get; set; }
        public string Transportista { get; set; }
        public string Carrier { get; set; }
        public string TipoEnvio { get; set; }
        public string TipoServicio { get; set; }
        public string FechaCompromisoClienteIni { get; set; }
        public string FechaCompromisoClienteFin { get; set; }
        public string FechaSolicitudGuiaReal { get; set; }
        public string DiasRetrasoSurtido { get; set; }
        public string FechaCompromisoRecoleccion { get; set; }
        public string ReasignacionTransporte { get; set; }
        public string CancelacionGuias { get; set; }
    }

    public class PendienteEntregaModel
    {
        public string DiasRetrasoTransportista { get; set; }
        public string Almacen_Proveedor { get; set; }
        public string Consignacion { get; set; }
        public string GuiaTransporte { get; set; }
        public string Transportista { get; set; }
        public string Carrier { get; set; }
        public string TipoEnvio { get; set; }
        public string TipoServicio { get; set; }
        public string CodigoPostalDestino { get; set; }
        public string EstadoDestino { get; set; }
        public string EstatusTransporte { get; set; }
        public string FechaCompromisoClienteIni { get; set; }
        public string FechaCompromisoClienteFin { get; set; }
        public string FechaRecoleccionReal { get; set; }
        public string DiasRetrasoRecoleccion { get; set; }
        public string FechaCompromisoEntregaTransporte { get; set; }
    }

    public class RevisionGeneralModel
    {
        public string DiasRetrasoTotal { get; set; }
        public string Almacen_Proveedor { get; set; }
        public string Consignacion { get; set; }
        public string GuiaTransporte { get; set; }
        public string Transportista { get; set; }
        public string Carrier { get; set; }
        public string TipoEnvio { get; set; }
        public string TipoServicio { get; set; }
        public string Estatustransporte { get; set; }
        public string FechaCompromisoClienteIni { get; set; }
        public string FechaCompromisoClienteFin { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaPago { get; set; }
        public string FechaCompromisoSurtido { get; set; }
        public string FechaSolicitudGuIaReal { get; set; }
        public string DiasRetrasoSurtido { get; set; }
        public string FechaCompromisoRecoleccion { get; set; }
        public string FechaRecoleccionReal { get; set; }
        public string DiasRetrasoRecoleccion { get; set; }
        public string FechaCompromisoEntregaTransporte { get; set; }
        public string FechaEntregaReal { get; set; }
        public string DiasRetrasoTransportista { get; set; }
    }
}