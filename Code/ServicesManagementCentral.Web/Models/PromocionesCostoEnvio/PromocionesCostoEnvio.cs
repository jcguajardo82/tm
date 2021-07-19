using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.PromocionesCostoEnvio
{
    public class PromocionesCostoEnvio
    {
        public int cnscPromo { get; set; }
        public string IdTipoCatalogo { get; set; }
        public string IdOwner { get; set; }
        public string IdFormatoTienda { get; set; }
        public string PostalCodeOrig { get; set; }
        public string PostalCodeDestino { get; set; }
        public string CiudadOrig { get; set; }
        public string CiudadDest { get; set; }
        public string EdoOrig { get; set; }
        public string EdoDest { get; set; }

        public string IdSupplierWH { get; set; }
        public string SupplierName { get; set; }
        public string IdTransportista { get; set; }
        public string NombreTransportista { get; set; }
        public string IdTipoEnvio { get; set; }
        public string IdTipoServicio { get; set; }
        public string Cve_CategSAP { get; set; }
        public string Desc_CategSAP { get; set; }
        public string Cve_GciaCategSAP { get; set; }
        public string Desc_GciaCategSAP { get; set; }
        public string Material_MATNR { get; set; }
        public string Id_Num_CodBarra { get; set; }
        public string Nombre_SKU { get; set; }
        public string PesoMinimo { get; set; }
        public string PesoMaximo { get; set; }
        public string IdTipoLogistica { get; set; }
        public string MesesSinIntereses { get; set; }
        public string ComprasMayor { get; set; }
        public string ComprasMenor { get; set; }
        public string FechaInicioPromo { get; set; }
        public string HoraInicioPromo { get; set; }
        public string FechaFinPromo { get; set; }
        public string HoraFinPromo { get; set; }
        public decimal? CostoEspecial { get; set; }
        public decimal? TarifaDesc { get; set; }
        public string FechaUltModif { get; set; }
        public string HoraUltModif { get; set; }
        public string UsuarioUltModif { get; set; }
        public string BitActivo { get; set; }
        public string FechaCreacion { get; set; }
        public string HoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }

    }
}