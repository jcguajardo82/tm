using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Reportes
{
    public class SETCE
    {
        public string AnioServ { get; set; }
        public string NoMesServ { get; set; }
        public string NomMesSer { get; set; }
        public string AnioConsig { get; set; }
        public string NoMesConsig { get; set; }
        public string NomMesConsig { get; set; }
        public string FecCreacionConsig { get; set; }
        public string HoraCreacionConsig { get; set; }
        public string Consignacion { get; set; }
        public string NoTda { get; set; }
        public string NomTda { get; set; }

        public string Formato { get; set; }
        public string Direccion { get; set; }
        public string Director { get; set; }
        public string Region { get; set; }
        public string EdoOrigen { get; set; }
        public string EdoDestino { get; set; }
        public string CpOrigen { get; set; }
        public string CpDestino { get; set; }
        public string TipoEntrega { get; set; }
        public string FecPagoConsig { get; set; }
        public string HoraPagoConsig { get; set; }
        public string EstatusPAgo { get; set; }
        public string FecConsigSurtido { get; set; }
        public string HoraConsigSurtido { get; set; }

        public string TransAsignado { get; set; }
        public string GuiaTrans { get; set; }
        public string GuiaSoriana { get; set; }
        public string FecRecoleccion { get; set; }
        public string HoraRecoleccion { get; set; }
        public string EstatusHD { get; set; }
        public string FecEntregaClient { get; set; }
        public string HoraMaxComEntrClie { get; set; }
        public string FecComEnt { get; set; }
        public string HoraComEnt { get; set; }
        public string DiasTransRecEntrega { get; set; }
        public string DiasCreacionEntrega { get; set; }
        public string HorasComTrans { get; set; }
        public string VariacionRecoleccion { get; set; }
        public string VariacionCreacion { get; set; }
        public string CumplimientEmbarque { get; set; }
        public string CumplimientoCreacion { get; set; }
        public string EnviosConsignacion { get; set; }
        public string CostoTrans { get; set; }
        public string CostoEnvClienteCot { get; set; }
        public string CostoEnvClienteReal { get; set; }

        public string TicketVta { get; set; }
        public string ProdConsignacion { get; set; }
        public string PzasConsignacion { get; set; }
    }
}