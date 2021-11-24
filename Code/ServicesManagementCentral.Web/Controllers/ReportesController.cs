using ServicesManagement.Web.DAL;
using ServicesManagement.Web.Helpers;
using ServicesManagement.Web.Models.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicesManagement.Web.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult MercanciasGrles()
        {
            Session["MercanciasGrles"] = DALReportes.MercanciasGrles_sUp();
            return View("RptMercanciasGrles");
        }
        public ActionResult GetMercanciasGrles()
        {
            try
            {
                var list = DataTableToModel.ConvertTo<MercanciasGeneralesModel>(DALReportes.MercanciasGrles_sUp().Tables[0]);

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

    }
}