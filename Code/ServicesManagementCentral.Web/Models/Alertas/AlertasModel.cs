using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Alertas
{
    public class AlertasModel
    {
        public List<PendienteRecoleccionModel> lstPendientesRec { get; set; }
        public List<PendienteEntregaModel> lstEntregaModel { get; set; }
        public List<RevisionGeneralModel> lstRevisionGral { get; set; }
    }

    public class PendienteRecoleccionModel
    {
        public string DiasRetrasoRecoleccion { get; set; }
        public string ColorRetrasoRecoleccion { get; set; }
        public string Proveedor { get; set; }
        public string Consignacion { get; set; }
        public string GuiaTransporte { get; set; }
        public string Transportista { get; set; }
        public string Carrier { get; set; }
        public string TipoDeEnvio { get; set; }
        public string TipoDeServicio { get; set; }
        public string FechaComprimisoInicio { get; set; }
        public string FechaComprimisoFin { get; set; }
        public string FechaSolicitudReal { get; set; }
        public string DiasRetrasoSurtido { get; set; }
        public string ColorRetrasoSurtido { get; set; }
        public string FechaCompromisoRecoleccion { get; set; }
        public string fechaCreacion { get; set; }
    }

    public class PendienteEntregaModel
    {
        public string DiasRetrasoTransportista { get; set; }
        public string ColorRetrasoTransportista { get; set; }
        public string Proveedor { get; set; }
        public string Consignacion { get; set; }
        public string GuiaTransporte { get; set; }
        public string Transportista { get; set; }
        public string Carrier { get; set; }
        public string TipoDeEnvio { get; set; }
        public string TipoDeServicio { get; set; }
        public string CodigoPostalDestino { get; set; }
        public string EstadoDestino { get; set; }
        public string EstatusTransporte { get; set; }
        public string FechaComprimisoInicio { get; set; }
        public string FechaComprimisoFin { get; set; }
        public string DiasRetrasoRecoleccion { get; set; }
        public string ColorRetrasoRecoleccion { get; set; }
        public string FechaRecoleccionReal { get; set; }
        public string FechaEntregaTransporte { get; set; }
        public string fechaCreacion { get; set; }
    }

    public class RevisionGeneralModel
    {
        public string DiasRetrasoTotal { get; set; }
        public string ColorRetrasoTotal { get; set; }
        public string Proveedor { get; set; }
        public string Consignacion { get; set; }
        public string GuiaTransporte { get; set; }
        public string Transportista { get; set; }
        public string Carrier { get; set; }
        public string TipoDeEnvio { get; set; }
        public string TipoDeServicio { get; set; }
        public string EstatusTransporte { get; set; }
        public string FechaComprimisoInicio { get; set; }
        public string FechaComprimisoFin { get; set; }
        public string fechaCreacion { get; set; }
        public string FechaPago { get; set; }
        public string FechaCompromisoSurtido { get; set; }
        public string FechaSolicitudReal { get; set; }
        public string DiasRetrasoSurtido { get; set; }
        public string ColorRetrasoSurtido { get; set; }
        public string FechaCompromisoRecoleccion { get; set; }
        public string FechaRecoleccionReal { get; set; }
        public string DiasRetrasoRecoleccion { get; set; }
        public string ColorRetrasoRecoleccion { get; set; }
        public string FechaEntregaTransporte { get; set; }
        public string fechaEntregaReal { get; set; }
        public string DiasRetrasoTransportista { get; set; }
        public string ColorRetrasoTransportista { get; set; }
    }

    public class DatosCancelacion
    {
        public CabeceraCancelacion Cabecera { get; set; }
        public List<MotivosCancelacion> lstMotivos { get; set; }
    }

    public class MotivosCancelacion
    {
        public string IdMotivoCancelacion { get; set; }
        public string Descripcion { get; set; }
    }

    public class CabeceraCancelacion
    {
        public string Consignacion { get; set; }
        public string Transportista { get; set; }
        public string Carrier { get; set; }
        public string TipoDeEnvio { get; set; }
        public string TipoDeServicio { get; set; }
    }

    public class DatosHistorial
    {
        public HistorialEncabezado historialEncabezado { get; set; }
        public List<HistorialDetalle> lstHistorialDetalle { get; set; }
    }

    public class HistorialEncabezado
    {
         public string Proveedor { get; set; }
        public string Consignacion { get; set; }
        public string GuiaTransporte { get; set; }
        public string Transportista { get; set; }
        public string Carrier { get; set; }
        public string CiudadOrigen { get; set; }
        public string CodigoPostalOrigen { get; set; }
        public string CiudadDestino { get; set; }
        public string CodigoPostalDestino { get; set; }
        public string FechaCompromisoEntregaTransportista { get; set; }
        public string FechaComprimisoEntregaCliente { get; set; }
        public string NombreQuienRecibe { get; set; }
    }

    public class HistorialDetalle
    {
        public string UeNo { get; set; }
         public string IdTrackingService { get; set; }
        public string eventDateTime { get; set; }
        public string eventDescriptionSPA { get; set; }
        public string eventPlaceName { get; set; }
    }
}