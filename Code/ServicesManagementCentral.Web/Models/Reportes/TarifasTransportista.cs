using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Reportes
{
    public class TarifasTransportista
    {
        public string Consignacion { get; set; }
        public string CodigoAlmacen { get; set; }
        public string NomAlmacen { get; set; }
        public string TipoAlmacen { get; set; }
        public string Zona { get; set; }
        public string TransAsignado { get; set; }
        public string TipoEnvio { get; set; }
        public string GuiaTrans { get; set; }
        public string GuiaSoriana { get; set; }
        public string EstatusEntrega { get; set; }
        public string FecRecoleccion { get; set; }
        public string FecEntClient { get; set; }
        public string PesoKGSis { get; set; }
        public string PesoVolSis { get; set; }
        public string PesoMayorSis { get; set; }
        public string CostoTransCotizado { get; set; }
        public string PesoKGGuias { get; set; }
        public string PesoVolGuias { get; set; }
        public string PesoMayorGuias { get; set; }
        public string CostoTransGuias { get; set; }
        public string PesoKGTrans { get; set; }
        public string PesoVolTrans { get; set; }
        public string PesoMayorTrans { get; set; }
        public string CostoTransReal { get; set; }
        public string Variacion1 { get; set; }
        public string Variacion2 { get; set; }
       
    }
}