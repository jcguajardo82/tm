using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Dashboard
{
    public class IndicadoresOpModel
    {
       public MercanciasGenerales mercancias = new MercanciasGenerales();
        public IndIngresosEgresos indicadores = new IndIngresosEgresos();
        public SuperCasa super = new SuperCasa();
        public string Frecuencia { get; set; }
    }
}