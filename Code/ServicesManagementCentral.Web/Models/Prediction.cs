using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models
{
    public class Prediction
    {
        public List<Place> results { set; get; }
        public string next_page_token { set; get; }
        public string status { set; get; }
    }
}