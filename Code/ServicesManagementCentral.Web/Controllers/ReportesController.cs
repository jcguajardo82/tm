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
            ViewBag.FecIni = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            ViewBag.FecFin = DateTime.Now.ToString("yyyy/MM/dd");
            ViewBag.IdOwner = 0;
            ViewBag.IdTienda = "";

            int IdOwner = 0, count=0;
            string IdTienda = string.Empty;

            if (Request.QueryString["IdOwner"] != null)
            {
                IdOwner = int.Parse(Request.QueryString["IdOwner"].ToString());
                ViewBag.IdOwner = IdOwner;
                Session["IdOwnerNiveles"] = IdOwner;
            }

            if (Request.QueryString["IdTienda"] != null)
            {
                IdTienda = Request.QueryString["IdTienda"].ToString();
                ViewBag.IdTienda = IdTienda;
                count++;
            }
            if (Request.QueryString["feIni"] != null)
            {
                ViewBag.FecIni = Convert.ToDateTime(Request.QueryString["feIni"]).ToString("yyyy/MM/dd");
                count++;

            }

            if (Request.QueryString["feFin"] != null)
            {
                ViewBag.FecFin = Convert.ToDateTime(Request.QueryString["feFin"]).ToString("yyyy/MM/dd");
                count++;
            }
            //Session["lstNiveles"] = null;

            if (count == 3)
                Session["MercanciasGrles"] = DALReportes.MercanciasGrles_sUp(IdTienda, Convert.ToDateTime(ViewBag.FecIni), Convert.ToDateTime(ViewBag.FecFin));
            else
                Session["MercanciasGrles"] = null;
            return View("RptMercanciasGrles");
        }
        public ActionResult GetMercanciasGrles()
        {
            try
            {
                //var list = DataTableToModel.ConvertTo<MercanciasGeneralesModel>(DALReportes.MercanciasGrles_sUp("","","").Tables[0]);

                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult ListSuppliersById(int idOwner)
        {
            try
            {
                var list = DataTableToModel.ConvertTo<SuppliersByIdModel>(DALReportes.upCorpTms_Cns_SuppliersById(idOwner).Tables[0]);

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