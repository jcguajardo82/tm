using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Reportes
{
    public class MercanciasGeneralesModel
    {
        public string UeNo { get; set; }
        public string AnioConsignacion { get; set; }
        public int NoMesConsignacion { get; set; }
        public string MesConsignacion { get; set; }
        public string OrderDate { get; set; }
        public string ConsignmentType { get; set; }
        public string UeNoRMA { get; set; }
        public string AnioServicio { get; set; }
        public int NoMesServicio { get; set; }
        public string MesServicio { get; set; }
        public string idSupplierWH { get; set; }
        public string supplierName { get; set; }
        public string idSupplierWHCode { get; set; }
        public string SupplierWHName { get; set; }
        public string UeType { get; set; }
        public string ordenDeCompraPDP { get; set; }
        public string addressState { get; set; }
        public string addressCity { get; set; }
        public string addressPostalCode { get; set; }
        public string IdPlaza { get; set; }
        public string addressReference1 { get; set; }
        public string addressReference2 { get; set; }
        public string Abrev_Estado { get; set; }
        public string Locality { get; set; }
        public string PostalCode { get; set; }
        public string idplazadestino { get; set; }
        public string zona { get; set; }
        public string tipodeenvio { get; set; }
        public string tipodeservicio { get; set; }
        public string tipodelogistica { get; set; }
        public string Ufechaestatusalmacen { get; set; }
        public string Uestatusalmacen { get; set; }
        public string fechaestatusentregas { get; set; }
        public string TrackingServiceStatus { get; set; }
        public string transportistaasignado { get; set; }
        public string TrackingServiceName { get; set; }
        public string IdTrackingService { get; set; }
        public string TrackingExpDate { get; set; }
        public string CreationDate { get; set; }
        public string fecharecoleccion { get; set; }
        public string visitas { get; set; }
        public string fechaprimerintento { get; set; }
        public string fechasegundointento { get; set; }
        public string fechaentregacliente { get; set; }
        public string diashabilescompromiso { get; set; }
        public string fechamaximacompromiso { get; set; }
        public string Fdiastranscurridosdesdelarecoleccion { get; set; }
        public string Fdiastranscurridosdesdelacreacion { get; set; }
        public string diascompromisotransportista { get; set; }
        public string Fvariacionvsrecoleccion { get; set; }
        public string Fvariacionvscreacion { get; set; }
        public string Fcumplimientovsembarque { get; set; }
        public string Fcumplimientovscreacion { get; set; }
        public string Ucausaincumplimiento { get; set; }
        public string enviosporconsignacion { get; set; }
        public string totalPrice { get; set; }
        public string costoenvioclientecotizado { get; set; }
        public string costoenvioclientereal { get; set; }
        public string OrderTotal { get; set; }
        public string total { get; set; }
        public string ProductosPorAsignacion { get; set; }
        public string PiezasPorAsignacion { get; set; }

    }
}