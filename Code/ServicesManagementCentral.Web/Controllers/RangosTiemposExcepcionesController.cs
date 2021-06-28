

namespace ServicesManagement.Web.Controllers
{
    using ServicesManagement.Web.DAL;
    using ServicesManagement.Web.Helpers;
    using ServicesManagement.Web.Models.DefRangosTiempo;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    [Authorize]
    public class RangosTiemposExcepcionesController : Controller
    {
        // GET: RangosTiemposExcepciones
        public ActionResult RangosTiemposExcepciones()
        {
            return View();
        }

        public ActionResult GetRangosTiemposExcepciones()
        {
            try
            {

                var listC = DataTableToModel.ConvertTo<RangosTiemposExcepciones>(DALRangosTiemposExcepciones.RangosTiemposExcepciones_sUP().Tables[0]);

                var result = new { Success = true, resp = listC };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetRangosTiemposExcepcionesId(int Id)
        {
            try
            {

                var listC = DataTableToModel.ConvertTo<RangosTiemposExcepciones>(DALRangosTiemposExcepciones.RangosTiemposExcepcionesById_sUP(Id).Tables[0]).FirstOrDefault();

                var result = new { Success = true, resp = listC };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult DelRangosTiemposExcepciones(int cnscDef)
        {
            try
            {

                DALRangosTiemposExcepciones.RangosTiemposExcepciones_dUp(cnscDef
                       , DateTime.Now, DateTime.Now.TimeOfDay, User.Identity.Name);

                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddRangosTiemposExcepciones(int cnscDef, int Prioridad, int IdTipoCatalogo, int IdOwner, int IdSupplierWH, string SupplierName, int IdTipoLogistica
    , int IdTransportista, string NombreTransportista, int DiasExtra, string FechaInicioPromo, string HoraInicioPromo, string FechaFinPromo, string HoraFinPromo
    , bool BitActivo)
        {
            try
            {
                if (cnscDef == 0)
                {

                    DALRangosTiemposExcepciones.RangosTiemposExcepciones_iUp( Prioridad,  IdTipoCatalogo,  IdOwner,  IdSupplierWH,  SupplierName,  IdTipoLogistica
                    , IdTransportista,  NombreTransportista,  DiasExtra, Convert.ToDateTime(FechaInicioPromo), TimeSpan.Parse(HoraInicioPromo), Convert.ToDateTime(FechaFinPromo), TimeSpan.Parse(HoraFinPromo)
                    , DateTime.Now, DateTime.Now.TimeOfDay, User.Identity.Name, BitActivo);
                }
                else
                {
                    DALRangosTiemposExcepciones.RangosTiemposExcepciones_uUp(cnscDef,Prioridad, IdTipoCatalogo, IdOwner, IdSupplierWH, SupplierName, IdTipoLogistica
                                       , IdTransportista, NombreTransportista, DiasExtra, Convert.ToDateTime(FechaInicioPromo), TimeSpan.Parse(HoraInicioPromo), Convert.ToDateTime(FechaFinPromo), TimeSpan.Parse(HoraFinPromo)
                                       , DateTime.Now, DateTime.Now.TimeOfDay, User.Identity.Name, BitActivo);
                }
                var result = new { Success = true };
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