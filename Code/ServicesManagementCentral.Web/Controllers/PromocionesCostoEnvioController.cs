﻿using ServicesManagement.Web.DAL;
using ServicesManagement.Web.Helpers;
using ServicesManagement.Web.Models.PromocionesCostoEnvio;
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

        public ActionResult GetPromociones()
        {
            return View();
        }

        public ActionResult AddPromociones(int IdTipoCatalogo, int IdOwner, int IdFormatoTienda, string PostalCodeOrig, string PostalCodeDestino
            , string CiudadOrig, string CiudadDest, string EdoOrig, string EdoDest, int IdSupplierWH, string SupplierName, int IdTransportista, string NombreTransportista
            , int IdTipoEnvio, int IdTipoServicio, string Cve_CategSAP, string Desc_CategSAP, string Cve_GciaCategSAP, string Desc_GciaCategSAP
            , string Material_MATNR, int Id_Num_CodBarra, string nombre_SKU, decimal PesoMinimo, decimal PesoMaximo, int IdTipoLogistica, int MesesSinIntereses
            , decimal ComprasMayor, decimal ComprasMenor, string FechaInicioPromo, string HoraInicioPromo, string FechaFinPromo, string HoraFinPromo
            , decimal CostoEspecial, decimal TarifaDesc)
        {
            try
            {

                var fecDemo = DateTime.Now.TimeOfDay;
                //TimeSpan time = TimeSpan.Parse(fecDemo);
                DALPromocionesCostoEnvio.PromocionesCostoEnvio_iUp(
                     IdTipoCatalogo, IdOwner, IdFormatoTienda, PostalCodeOrig, PostalCodeDestino
            , CiudadOrig, CiudadDest, EdoOrig, EdoDest, IdSupplierWH, SupplierName, IdTransportista, NombreTransportista
            , IdTipoEnvio, IdTipoServicio, Cve_CategSAP, Desc_CategSAP, Cve_GciaCategSAP, Desc_GciaCategSAP
            , Material_MATNR, Id_Num_CodBarra, nombre_SKU, PesoMinimo, PesoMaximo, IdTipoLogistica, MesesSinIntereses
            , ComprasMayor, ComprasMenor, Convert.ToDateTime(FechaInicioPromo), TimeSpan.Parse(HoraInicioPromo), Convert.ToDateTime(FechaFinPromo), TimeSpan.Parse(HoraFinPromo)
            , CostoEspecial, TarifaDesc, DateTime.Now, fecDemo, User.Identity.Name, true
                    );

                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

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

        [HttpGet]
        public ActionResult GetFormatoTenda()
        {
            try
            {

                var listC = DataTableToModel.ConvertTo<FormatoTienda>(DALPromocionesCostoEnvio.FormatoTiendaByBit_sUp().Tables[0]);

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