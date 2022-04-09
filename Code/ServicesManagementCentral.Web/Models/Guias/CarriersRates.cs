using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Guias
{
    public class CarriersRates
    {
        public string carrier { get; set; }
        public string Service { get; set; }
        public int diasEntrega { get; set; }
        public bool BitAsegurado { get; set; }
        public double Montoasegurado { get; set; }
        public decimal totalPrice { get; set; }
        public decimal NivelServicio { get; set; }
        public int Prioridad { get; set; }
        public long cotizeId { get; set; }
    }
}