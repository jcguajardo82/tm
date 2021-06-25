using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicesManagement.Web.Controllers
{
    [Authorize]
    public class PromocionesCostoEnvioController : Controller
    {
        // GET: PromocionesCostoEnvio
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPromociones() {
            return View();
        }
    }
}