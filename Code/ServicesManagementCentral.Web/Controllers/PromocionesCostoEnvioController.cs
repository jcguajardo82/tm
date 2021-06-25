using ServicesManagement.Web.DAL;
using ServicesManagement.Web.Helpers;
using ServicesManagement.Web.Models.TipoLogistica;
using System;
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

        [HttpGet]
        public ActionResult GetTipoLogistica()
        {
            try
            {

                var listC = DataTableToModel.ConvertTo<TipoLogisticaCns>(DALTipoLogistica.up_CorpTMS_sel_TipoLogisticaByBit().Tables[0]);

                var result = new { Success = true, resp = listC };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}