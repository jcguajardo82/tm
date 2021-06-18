﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Impex
{
    public class Cns_TransportistaDestinosZonas
    {
        public int IdTransportista { get; set; }
        public string Cve_PlazaOrigen { get; set; }
        public string Cve_PlazaDestino { get; set; }
        public string IdZona { get; set; }
        public string CreatedId { get; set; }
        public string CreatedDate { get; set; }
    }
}