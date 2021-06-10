using Newtonsoft.Json;

namespace Soriana.OMS.Ordenes.Common.DTO
{
    public class InformacionExpedidor
    {
        #region Public Properties
        [JsonProperty("shipper-name", Order = 1)]
        public string NombreExpedidor { get; set; }

        [JsonProperty("shipping-date", Order = 2)]
        public string FechaEnvio { get; set; }

        [JsonProperty("transaction-no", Order = 3)]
        public string NumeroTransaccion { get; set; }

        [JsonProperty("traking-no", Order = 4)]
        public string NumeroTracking { get; set; }

        [JsonProperty("num-bags", Order = 5)]
        public int NumeroBolsas { get; set; }

        [JsonProperty("num-coolers", Order = 6)]
        public int NumeroEnfriadores { get; set; }

        [JsonProperty("num-containers", Order = 7)]
        public int NumeroContenedores { get; set; }

        [JsonProperty("terminal", Order = 8)]
        public string Terminal { get; set; }
        #endregion
    }
}
