using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Dashboard
{
    public class EnviosVsEstatus
    {
        public List<DatosEnvio> Envios { get; set; }
        public List<DatosEstatus> Estatus { get; set; }
    }

    public class DatosEnvio
    {
        public DateTime FechaEnvio { get; set; }
        public int TotalEnvios { get; set; }

        public string Anio { get; set; }
        public string Mes { get; set; }
        public string Dia { get; set; }
        public string Valor { get; set; }
    }

    public class DatosEstatus
    {
        public DateTime FechaEstatus { get; set; }
        public int TotalEstatus { get; set; }

        public string Anio { get; set; }
        public string Mes { get; set; }
        public string Dia { get; set; }
        public string Valor { get; set; }
    }
}