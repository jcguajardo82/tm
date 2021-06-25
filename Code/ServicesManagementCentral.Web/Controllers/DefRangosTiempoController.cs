using ServicesManagement.Web.DAL;
using ServicesManagement.Web.Helpers;
using ServicesManagement.Web.Models.Catalogos;
using ServicesManagement.Web.Models.DefRangosTiempo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicesManagement.Web.Controllers
{
    [Authorize]
    public class DefRangosTiempoController : Controller
    {
        // GET: DefRangosTiempo
        public ActionResult Index()
        {
            return View();
        }

        #region ParametrosDias
        public ActionResult ParametrosDias()
        {
            return View();
        }

        public ActionResult GetParametrosDiasId(int IdParametro)
        {
            try
            {
                var list = DataTableToModel.ConvertTo<ParametrosDias>(DALDefRangosTiempo.ParametrosDiasById_sUp(IdParametro).Tables[0]).FirstOrDefault();

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetParametrosDias()
        {
            try
            {
                var list = DataTableToModel.ConvertTo<ParametrosDias>(DALDefRangosTiempo.ParametrosDias_sUp().Tables[0]);

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult AddarametrosDias(int IdParametro, int DiasParametro1, int DiasParametro2, int DiasParametro3)
        {
            try
            {
                if (IdParametro == 0)
                {
                    DALDefRangosTiempo.ParametrosDias_iUp(IdParametro, DiasParametro1, DiasParametro2, DiasParametro3);
                }
                else { DALDefRangosTiempo.ParametrosDias_uUp(IdParametro, DiasParametro1, DiasParametro2, DiasParametro3); }
                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}