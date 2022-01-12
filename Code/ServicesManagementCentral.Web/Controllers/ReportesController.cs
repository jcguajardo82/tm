using Microsoft.Ajax.Utilities;
using ServicesManagement.Web.DAL;
using ServicesManagement.Web.Helpers;
using ServicesManagement.Web.Models.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
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
        public ActionResult NivelDeServicio()
        {
            ViewBag.FecIni = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            ViewBag.FecFin = DateTime.Now.ToString("yyyy/MM/dd");
            
            return View("RptNivelDeServicio");
        }
        public ActionResult GetNivelDeServicio(string fechaInicio, string fechaFin)
        {
            List<NivelDeServicioTransitoModel> lstTransitos = new List<NivelDeServicioTransitoModel>();
            List<NivelDeServicioIncidencias> lstIncidencias = new List<NivelDeServicioIncidencias>();
            List<NivelDeServicioEntregadas> lstEntregadas = new List<NivelDeServicioEntregadas>();
            List<NivelDeSevicioAlmacenModel> lstAlmacen = new List<NivelDeSevicioAlmacenModel>();
            string[] carriers = { "Logyt", "Estafeta", "Envia.Com" };
            List<string> lstCarriers = new List<string>(carriers);
            string[] almacenesMG = { "DST", "CEDIS", "DSV" };
            List<string> lstAlmacenesMG = new List<string>(almacenesMG);

            try
            {
                //var list = DataTableToModel.ConvertTo<NivelDeServicioModel>(DALReportes.NivelDeServicio_sUp(fechaInicio, fechaFin).Tables[0]);
                IQueryable<NivelDeServicioModel> list = from row in DALReportes.NivelDeServicio_sUp(fechaInicio, fechaFin).Tables[0].AsEnumerable().AsQueryable()
                                                        select new NivelDeServicioModel()
                                                        {
                                                            Costo = decimal.Parse(row["Costo"].ToString()),
                                                            totalIngreso = decimal.Parse(row["totalIngreso"].ToString()),
                                                            Transportista = row["Transportista"].ToString(),
                                                            TrackingServiceStatus = row["TrackingServiceStatus"].ToString(),
                                                            UeType = row["UeType"].ToString(),
                                                            EntregadaATiempo = row["EntregadaATiempo"].ToString() == "1" ? true : false,
                                                            PagoAutorizado = row["PagoAutorizado"].ToString() == "1" ? true : false,
                                                            RecoleccionAtiempo = row["RecoleccionAtiempo"].ToString() == "1" ? true : false,

                                                        };
                if (list.Count() > 0)
                {
                    lstTransitos.Add(
                        new NivelDeServicioTransitoModel()
                        {
                            Transportista = "General",
                            ConsignacionesRecolectadasEnTiempo = list.Where(x => x.RecoleccionAtiempo == true).Count(),
                            ConsignacionesRecolectadasFueraTiempo = list.Where(x => x.RecoleccionAtiempo == false).Count(),
                            ConsignacionesTotales = list.Count(),
                            NivelDeServicio = list.Where(x => x.RecoleccionAtiempo == false).Count() / list.Count(),
                        }
                        );
                    lstIncidencias.Add(
                           new NivelDeServicioIncidencias()
                           {
                               Transportista = "General",
                               ConsignacionesTotales = list.Count(),
                               ConsignacionesIncidencias = list.Where(x => x.TrackingServiceStatus == "INCIDENCIA").Count(),
                               PorcentajeIncidencias = list.Where(x => x.TrackingServiceStatus == "INCIDENCIA").Count() / list.Count(),
                           }
                           );
                    lstEntregadas.Add(
                        new NivelDeServicioEntregadas()
                        {
                            Transportista = "General",
                            ConsignacionesEntregadasEnTiempo = list.Where(x => x.EntregadaATiempo == true).Count(),
                            ConsignacionesEntregadasFueraTiempo = list.Where(x => x.EntregadaATiempo == false).Count(),
                            ConsignacionesTotales = list.Count(),
                            NivelDeServicio = list.Where(x => x.EntregadaATiempo == false).Count() / list.Count(),
                            CostoPromedio = list.Sum(x => x.Costo) / list.Count(),
                            IngresoPromedio = list.Sum(x => x.totalIngreso) / list.Count(),
                        }
                        );
                    lstAlmacen.Add(
                        new NivelDeSevicioAlmacenModel()
                        {
                            Almacen = "General",
                            ConsignacionesEntregadasEnTiempo = list.Where(x => x.EntregadaATiempo == true).Count(),
                            ConsignacionesEntregadasFueraTiempo = list.Where(x => x.EntregadaATiempo == false).Count(),
                            ConsignacionesTotales = list.Count(),
                            NivelDeServicio = list.Where(x => x.EntregadaATiempo == false).Count() / list.Count(),
                            CostoPromedio = list.Sum(x => x.Costo) / list.Count(),
                            IngresoPromedio = list.Sum(x => x.totalIngreso) / list.Count(),
                            TotalConsignacionesPagadas = list.Where(x => x.PagoAutorizado == true).Count()
                        }
                        );
                    foreach (var carrier in lstCarriers)
                    {
                        NivelDeServicioTransitoModel newTransito = new NivelDeServicioTransitoModel();
                        newTransito.Transportista = carrier;
                        newTransito.ConsignacionesTotales = list.Where(x => x.Transportista == carrier).Count();
                        newTransito.ConsignacionesRecolectadasEnTiempo = list.Where(x => x.Transportista == carrier && x.TrackingServiceStatus == "EN_TRANSITO" && x.RecoleccionAtiempo == true).Count();
                        newTransito.ConsignacionesRecolectadasFueraTiempo = list.Where(x => x.Transportista == carrier && x.TrackingServiceStatus == "EN_TRANSITO" && x.RecoleccionAtiempo == false).Count();
                        newTransito.NivelDeServicio = newTransito.ConsignacionesRecolectadasFueraTiempo > 0 ? decimal.Round(newTransito.ConsignacionesRecolectadasFueraTiempo / newTransito.ConsignacionesTotales, 3) : 0;
                        newTransito.Tipo = "MG";

                        lstTransitos.Add(newTransito);

                        NivelDeServicioIncidencias newIncidencias = new NivelDeServicioIncidencias();
                        newIncidencias.Transportista = carrier;
                        newIncidencias.ConsignacionesTotales = list.Where(x => x.Transportista == carrier).Count();
                        newIncidencias.ConsignacionesIncidencias = list.Where(x => x.Transportista == carrier && x.TrackingServiceStatus == "INCIDENCIA").Count();
                        newIncidencias.PorcentajeIncidencias = newIncidencias.ConsignacionesIncidencias > 0 ? newIncidencias.ConsignacionesIncidencias / newIncidencias.ConsignacionesTotales : 0;
                        newIncidencias.Tipo = "MG";

                        lstIncidencias.Add(newIncidencias);

                        NivelDeServicioEntregadas newEntregadas = new NivelDeServicioEntregadas();
                        newEntregadas.Transportista = carrier;
                        newEntregadas.ConsignacionesTotales = list.Where(x => x.Transportista == carrier).Count();
                        newEntregadas.ConsignacionesEntregadasEnTiempo = list.Where(x => x.Transportista == carrier && x.TrackingServiceStatus == "Entregada" && x.EntregadaATiempo == true).Count();
                        newEntregadas.ConsignacionesEntregadasFueraTiempo = list.Where(x => x.Transportista == carrier && x.TrackingServiceStatus == "Entregada" && x.EntregadaATiempo == false).Count();
                        newEntregadas.NivelDeServicio = newEntregadas.ConsignacionesEntregadasFueraTiempo > 0 ? newEntregadas.ConsignacionesEntregadasFueraTiempo / newEntregadas.ConsignacionesTotales : 0;
                        newEntregadas.CostoPromedio = list.Where(x => x.Transportista == carrier).Sum(x => x.Costo) > 0 ? list.Where(x => x.Transportista == carrier).Sum(x => x.Costo) / newEntregadas.ConsignacionesTotales : 0;
                        newEntregadas.IngresoPromedio = list.Where(x => x.Transportista == carrier).Sum(x => x.totalIngreso) > 0 ? list.Where(x => x.Transportista == carrier).Sum(x => x.totalIngreso) / newEntregadas.ConsignacionesTotales : 0;
                        newEntregadas.Tipo = "MG";

                        lstEntregadas.Add(newEntregadas);

                    }
                    foreach (var almacen in lstAlmacenesMG)
                    {
                        NivelDeSevicioAlmacenModel newAlmacen = new NivelDeSevicioAlmacenModel();
                        newAlmacen.Almacen = almacen;
                        newAlmacen.TotalConsignacionesPagadas = list.Where(x => x.UeType == almacen && x.PagoAutorizado == true).Count();
                        newAlmacen.ConsignacionesTotales = list.Where(x => x.UeType == almacen).Count();
                        newAlmacen.ConsignacionesEntregadasEnTiempo = list.Where(x => x.UeType == almacen && x.TrackingServiceStatus == "Entregada" && x.EntregadaATiempo == true).Count();
                        newAlmacen.ConsignacionesEntregadasFueraTiempo = list.Where(x => x.UeType == almacen && x.TrackingServiceStatus == "Entregada" && x.EntregadaATiempo == false).Count();
                        newAlmacen.NivelDeServicio = newAlmacen.ConsignacionesEntregadasFueraTiempo > 0 ? newAlmacen.ConsignacionesEntregadasFueraTiempo / newAlmacen.ConsignacionesTotales : 0;
                        newAlmacen.CostoPromedio = list.Where(x => x.UeType == almacen).Sum(x => x.Costo) > 0 ? list.Where(x => x.UeType == almacen).Sum(x => x.Costo) / newAlmacen.ConsignacionesTotales : 0;
                        newAlmacen.IngresoPromedio = list.Where(x => x.UeType == almacen).Sum(x => x.totalIngreso) > 0 ? list.Where(x => x.UeType == almacen).Sum(x => x.totalIngreso) / newAlmacen.ConsignacionesTotales : 0;
                        newAlmacen.Tipo = "MG";

                        lstAlmacen.Add(newAlmacen);

                    }

                    lstTransitos.Add(
                        new NivelDeServicioTransitoModel()
                        {
                            Transportista = "Subtotal MG",
                            ConsignacionesRecolectadasEnTiempo = lstTransitos.Where(x => x.Tipo == "MG").Sum(x => x.ConsignacionesRecolectadasEnTiempo),
                            ConsignacionesRecolectadasFueraTiempo = lstTransitos.Where(x => x.Tipo == "MG").Sum(x => x.ConsignacionesRecolectadasFueraTiempo),
                            ConsignacionesTotales = lstTransitos.Where(x => x.Tipo == "MG").Sum(x => x.ConsignacionesTotales),
                            NivelDeServicio = lstTransitos.Where(x => x.Tipo == "MG").Sum(x => x.NivelDeServicio),
                        }
                        );
                    lstIncidencias.Add(
                           new NivelDeServicioIncidencias()
                           {
                               Transportista = "Subtotal MG",
                               ConsignacionesTotales = lstIncidencias.Where(x => x.Tipo == "MG").Sum(x => x.ConsignacionesTotales),
                               ConsignacionesIncidencias = lstIncidencias.Where(x => x.Tipo == "MG").Sum(x => x.ConsignacionesIncidencias),
                               PorcentajeIncidencias = lstIncidencias.Where(x => x.Tipo == "MG").Sum(x => x.PorcentajeIncidencias)
                           }
                           );
                    lstEntregadas.Add(
                        new NivelDeServicioEntregadas()
                        {
                            Transportista = "Subtotal MG",
                            ConsignacionesEntregadasEnTiempo = lstEntregadas.Where(x => x.Tipo == "MG").Sum(x => x.ConsignacionesEntregadasEnTiempo),
                            ConsignacionesEntregadasFueraTiempo = lstEntregadas.Where(x => x.Tipo == "MG").Sum(x => x.ConsignacionesEntregadasFueraTiempo),
                            ConsignacionesTotales = lstEntregadas.Where(x => x.Tipo == "MG").Sum(x => x.ConsignacionesTotales),
                            NivelDeServicio = lstEntregadas.Where(x => x.Tipo == "MG").Sum(x => x.NivelDeServicio),
                            CostoPromedio = lstEntregadas.Where(x => x.Tipo == "MG").Sum(x => x.CostoPromedio),
                            IngresoPromedio = lstEntregadas.Where(x => x.Tipo == "MG").Sum(x => x.IngresoPromedio)
                        }
                        );
                    lstAlmacen.Add(
                        new NivelDeSevicioAlmacenModel()
                        {
                            Almacen = "Subtotal MG",
                            ConsignacionesEntregadasEnTiempo = lstAlmacen.Where(x => x.Tipo == "MG").Sum(x => x.ConsignacionesEntregadasEnTiempo),
                            ConsignacionesEntregadasFueraTiempo = lstAlmacen.Where(x => x.Tipo == "MG").Sum(x => x.ConsignacionesEntregadasFueraTiempo),
                            ConsignacionesTotales = lstAlmacen.Where(x => x.Tipo == "MG").Sum(x => x.ConsignacionesTotales),
                            CostoPromedio = lstAlmacen.Where(x => x.Tipo == "MG").Sum(x => x.CostoPromedio),
                            TotalConsignacionesPagadas = lstAlmacen.Where(x => x.Tipo == "MG").Sum(x => x.TotalConsignacionesPagadas),
                            NivelDeServicio = lstAlmacen.Where(x => x.Tipo == "MG").Sum(x => x.NivelDeServicio),
                            IngresoPromedio = lstAlmacen.Where(x => x.Tipo == "MG").Sum(x => x.IngresoPromedio),
                        }
                        );

                    foreach (var carrierSETC in list.Where(x => x.UeType == "SETC").DistinctBy(x => x.Transportista).Select(x => x.Transportista))
                    {
                        NivelDeServicioTransitoModel newTransito = new NivelDeServicioTransitoModel();
                        newTransito.Transportista = carrierSETC;
                        newTransito.ConsignacionesTotales = list.Where(x => x.Transportista == carrierSETC).Count();
                        newTransito.ConsignacionesRecolectadasEnTiempo = list.Where(x => x.Transportista == carrierSETC && x.TrackingServiceStatus == "EN_TRANSITO" && x.RecoleccionAtiempo == true).Count();
                        newTransito.ConsignacionesRecolectadasFueraTiempo = list.Where(x => x.Transportista == carrierSETC && x.TrackingServiceStatus == "EN_TRANSITO" && x.RecoleccionAtiempo == false).Count();
                        newTransito.NivelDeServicio = newTransito.ConsignacionesRecolectadasFueraTiempo > 0 ? decimal.Round(newTransito.ConsignacionesRecolectadasFueraTiempo / newTransito.ConsignacionesTotales, 3) : 0;
                        newTransito.Tipo = "SETC";

                        lstTransitos.Add(newTransito);

                        NivelDeServicioIncidencias newIncidencias = new NivelDeServicioIncidencias();
                        newIncidencias.Transportista = carrierSETC;
                        newIncidencias.ConsignacionesTotales = list.Where(x => x.Transportista == carrierSETC).Count();
                        newIncidencias.ConsignacionesIncidencias = list.Where(x => x.Transportista == carrierSETC && x.TrackingServiceStatus == "INCIDENCIA").Count();
                        newIncidencias.PorcentajeIncidencias = newIncidencias.ConsignacionesIncidencias > 0 ? newIncidencias.ConsignacionesIncidencias / newIncidencias.ConsignacionesTotales : 0;
                        newIncidencias.Tipo = "SETC";

                        lstIncidencias.Add(newIncidencias);

                        NivelDeServicioEntregadas newEntregadas = new NivelDeServicioEntregadas();
                        newEntregadas.Transportista = carrierSETC;
                        newEntregadas.ConsignacionesTotales = list.Where(x => x.Transportista == carrierSETC).Count();
                        newEntregadas.ConsignacionesEntregadasEnTiempo = list.Where(x => x.Transportista == carrierSETC && x.TrackingServiceStatus == "Entregada" && x.EntregadaATiempo == true).Count();
                        newEntregadas.ConsignacionesEntregadasFueraTiempo = list.Where(x => x.Transportista == carrierSETC && x.TrackingServiceStatus == "Entregada" && x.EntregadaATiempo == false).Count();
                        newEntregadas.NivelDeServicio = newEntregadas.ConsignacionesEntregadasFueraTiempo > 0 ? newEntregadas.ConsignacionesEntregadasFueraTiempo / newEntregadas.ConsignacionesTotales : 0;
                        newEntregadas.CostoPromedio = list.Where(x => x.Transportista == carrierSETC).Sum(x => x.Costo) > 0 ? list.Where(x => x.Transportista == carrierSETC).Sum(x => x.Costo) / newEntregadas.ConsignacionesTotales : 0;
                        newEntregadas.IngresoPromedio = list.Where(x => x.Transportista == carrierSETC).Sum(x => x.totalIngreso) > 0 ? list.Where(x => x.Transportista == carrierSETC).Sum(x => x.totalIngreso) / newEntregadas.ConsignacionesTotales : 0;
                        newEntregadas.Tipo = "SETC";

                        lstEntregadas.Add(newEntregadas);


                    }

                    lstTransitos.Add(
                        new NivelDeServicioTransitoModel()
                        {
                            Transportista = "Subtotal SETC",
                            ConsignacionesRecolectadasEnTiempo = lstTransitos.Where(x => x.Tipo == "SETC").Sum(x => x.ConsignacionesRecolectadasEnTiempo),
                            ConsignacionesRecolectadasFueraTiempo = lstTransitos.Where(x => x.Tipo == "SETC").Sum(x => x.ConsignacionesRecolectadasFueraTiempo),
                            ConsignacionesTotales = lstTransitos.Where(x => x.Tipo == "SETC").Sum(x => x.ConsignacionesTotales),
                            NivelDeServicio = lstTransitos.Where(x => x.Tipo == "SETC").Sum(x => x.NivelDeServicio),
                        }
                        );
                    lstIncidencias.Add(
                           new NivelDeServicioIncidencias()
                           {
                               Transportista = "Subtotal SETC",
                               ConsignacionesTotales = lstIncidencias.Where(x => x.Tipo == "SETC").Sum(x => x.ConsignacionesTotales),
                               ConsignacionesIncidencias = lstIncidencias.Where(x => x.Tipo == "SETC").Sum(x => x.ConsignacionesIncidencias),
                               PorcentajeIncidencias = lstIncidencias.Where(x => x.Tipo == "SETC").Sum(x => x.PorcentajeIncidencias)
                           }
                           );
                    lstEntregadas.Add(
                        new NivelDeServicioEntregadas()
                        {
                            Transportista = "Subtotal SETC",
                            ConsignacionesEntregadasEnTiempo = lstEntregadas.Where(x => x.Tipo == "SETC").Sum(x => x.ConsignacionesEntregadasEnTiempo),
                            ConsignacionesEntregadasFueraTiempo = lstEntregadas.Where(x => x.Tipo == "SETC").Sum(x => x.ConsignacionesEntregadasFueraTiempo),
                            ConsignacionesTotales = lstEntregadas.Where(x => x.Tipo == "SETC").Sum(x => x.ConsignacionesTotales),
                            NivelDeServicio = lstEntregadas.Where(x => x.Tipo == "SETC").Sum(x => x.NivelDeServicio),
                            CostoPromedio = lstEntregadas.Where(x => x.Tipo == "SETC").Sum(x => x.CostoPromedio),
                            IngresoPromedio = lstEntregadas.Where(x => x.Tipo == "SETC").Sum(x => x.IngresoPromedio)
                        }
                        );
                    lstAlmacen.Add(
                        new NivelDeSevicioAlmacenModel()
                        {
                            Almacen = "Subtotal SETC",
                            ConsignacionesEntregadasEnTiempo = list.Where(x => x.UeType == "SETC" && x.EntregadaATiempo == true).Count(),
                            ConsignacionesEntregadasFueraTiempo = list.Where(x => x.UeType == "SETC" && x.EntregadaATiempo == false).Count(),
                            ConsignacionesTotales = list.Where(x => x.UeType == "SETC").Count(),
                            CostoPromedio = list.Where(x => x.UeType == "SETC").Sum(x => x.Costo) / list.Where(x => x.UeType == "SETC").Count(),
                            TotalConsignacionesPagadas = list.Where(x => x.UeType == "SETC").Count(),
                            NivelDeServicio = list.Where(x => x.UeType == "SETC" && x.EntregadaATiempo == false).Count() / list.Where(x => x.UeType == "SETC").Count(),
                            IngresoPromedio = list.Where(x => x.UeType == "SETC").Sum(x => x.totalIngreso) / list.Where(x => x.UeType == "SETC").Count(),
                        }
                        );
                }
                var result = new { Success = true, enTransito = lstTransitos, enIncidencias = lstIncidencias, entregadas = lstEntregadas, porAlmacen = lstAlmacen };
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