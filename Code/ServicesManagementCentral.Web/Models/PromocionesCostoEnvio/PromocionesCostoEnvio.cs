using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.PromocionesCostoEnvio
{
    public class PromocionesCostoEnvio
    {
        public int cnscPromo { get; set; }
        public int IdTipoCatalogo { get; set; }
        public int IdOwner { get; set; }
        public int IdFormatoTienda { get; set; }
        public string PostalCodeOrig { get; set; }
        public string PostalCodeDestino { get; set; }
        public string CiudadOrig { get; set; }
        public string CiudadDest { get; set; }
        public string EdoOrig { get; set; }
        public string EdoDest { get; set; }

        public int IdSupplierWH { get; set; }
        public string SupplierName { get; set; }
        public int IdTransportista { get; set; }
        public string NombreTransportista { get; set; }
        public int IdTipoEnvio { get; set; }
        public int IdTipoServicio { get; set; }
        public string Cve_CategSAP { get; set; }
        public string Desc_CategSAP { get; set; }
        public string Cve_GciaCategSAP { get; set; }
        public string Desc_GciaCategSAP { get; set; }
        public int Material_MATNR { get; set; }
        public int Id_Num_CodBarra { get; set; }
        public int nombre_SKU { get; set; }
        public decimal PesoMinimo { get; set; }
        public decimal PesoMaximo { get; set; }
        public int IdTipoLogistica { get; set; }
        public int MesesSinIntereses { get; set; }
        public decimal ComprasMayor { get; set; }
        public decimal ComprasMenor { get; set; }
        public DateTime FechaInicioPromo { get; set; }
        public TimeSpan HoraInicioPromo { get; set; }
        public DateTime FechaFinPromo { get; set; }
        public TimeSpan HoraFinPromo { get; set; }
        public decimal CostoEspecial { get; set; }
        public decimal TarifaDesc { get; set; }
        public DateTime FechaUltModif { get; set; }
        public TimeSpan HoraUltModif { get; set; }
        public string UsuarioUltModif { get; set; }
        public bool BitActivo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public TimeSpan HoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }

    }
}