using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.PromocionesCostoEnvio
{
    public class orderfacts_PostalCodes
    {
        public class Estados {      
            public string Region1Code { get; set; }
            public string Region1Name { get; set; }
        }

        public class Ciudades {
            public string Region2Code { get; set; }
            public string Region2Name { get; set; }
             
        }
    }
}