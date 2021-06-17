using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Impex
{
    public class TransportistaRangosPesos
    {
        public int IdTransportista { get; set; }
        public decimal PesoInicio { get; set; }
        public decimal PesoFin { get; set; }
        public decimal PorcentajeInicialCliente { get; set; }
        public bool bitDeleted { get; set; }
        public string CreatedId { get; set; }
    }

    public class TransportistaRangosPesosShow
    {
        public string IdTransportista { get; set; }
        public decimal PesoInicio { get; set; }
        public decimal PesoFin { get; set; }
        public decimal PorcentajeInicialCliente { get; set; }
        public bool bitDeleted { get; set; }
        public string CreatedId { get; set; }
        public string CreatedDate { get; set; }
    }
}