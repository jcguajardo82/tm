﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models
{
    public class PasilloUnEditCat
    {
        public List<DivisionModel> Division { get; set; }
        public List<CategoriaModel> Categoria { get; set; }
        public List<LineaModel> Linea { get; set; }
        public List<PasilloUnCategModel> PasilloUnCateg { get; set; }
        //public List<PasilloUnLinea> PasilloUnLinea { get; set; }

    }
}