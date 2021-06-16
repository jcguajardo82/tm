﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Catalogos
{
    public class TipoServicio
    {
        public int IdTipoServicio { get; set; }
        public string Desc_TipoServicio { get; set; }
        public bool BitActivo { get; set; }
        public string CreateDate { get; set; }
        public string Created_User { get; set; }
        public string FecMovto { get; set; }
        public string Modified_User { get; set; }
    }
}