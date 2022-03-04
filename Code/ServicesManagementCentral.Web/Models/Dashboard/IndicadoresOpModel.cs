using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Dashboard
{
    public class IndicadoresOpModel
    {
        public EstatusOP general = new EstatusOP();
        public EstatusOP cedis = new EstatusOP();
        public EstatusOP dsv = new EstatusOP();
        public EstatusOP dst = new EstatusOP();
        public string Frecuencia { get; set; }

        public string FecIni { get; set; }
        public string FecFin { get; set; }
        public string op { get; set; }



        public List<Combo> DashboardTrans { get; set; }
        public List<Combo> DashboardTipoEnvio { get; set; }
        public List<Combo> DashboardTipoServicio { get; set; }
        public List<Combo> DashboardTipoLogistica { get; set; }


    }
}