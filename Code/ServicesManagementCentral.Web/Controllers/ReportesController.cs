using Microsoft.Ajax.Utilities;
using ServicesManagement.Web.DAL;
using ServicesManagement.Web.Helpers;
using ServicesManagement.Web.Models.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
namespace ServicesManagement.Web.Controllers
{
    public class ReportesController : Controller
    {
        public string draw = "";
        public string start = "";
        public string length = "";
        public string sortColumn = "";
        public string sortColumnDir = "";
        public string searchValue = "";
        public int pageSize, skip, recordsTotal;
        // GET: Reportes
        public ActionResult MercanciasGrles()
        {
            ViewBag.FecIni = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            ViewBag.FecFin = DateTime.Now.ToString("yyyy/MM/dd");
            ViewBag.IdOwner = 0;
            ViewBag.IdTienda = "";

            int IdOwner = 0, count=0;
            string IdTienda = string.Empty;

            if (Request.QueryString["IdOwner"] != null && Request.QueryString["IdOwner"] != "0")
            {
                IdOwner = int.Parse(Request.QueryString["IdOwner"].ToString());
                ViewBag.IdOwner = IdOwner;
                Session["IdOwnerNiveles"] = IdOwner;
                count++;
            }

            if (Request.QueryString["IdTienda"] != null && Request.QueryString["IdTienda"] != "")
            {
                IdTienda = Request.QueryString["IdTienda"].ToString();
                ViewBag.IdTienda = IdTienda;
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
                Session["MercanciasGrles"] = DALReportes.MercanciasGrles_sUp(IdTienda, IdOwner, Convert.ToDateTime(ViewBag.FecIni), Convert.ToDateTime(ViewBag.FecFin));
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
                                                            Costo = row["Costo"].ToString() == "" ? 0 : decimal.Parse(row["Costo"].ToString()),
                                                            totalIngreso = row["totalIngreso"].ToString() == "" ? 0 : decimal.Parse(row["totalIngreso"].ToString()),
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
                            ConsignacionesRecolectadasEnTiempo = list.Where(x => x.TrackingServiceStatus == "EN_TRANSITO" && x.RecoleccionAtiempo == true).Count(),
                            ConsignacionesRecolectadasFueraTiempo = list.Where(x => x.TrackingServiceStatus == "EN_TRANSITO" && x.RecoleccionAtiempo == false).Count(),
                            ConsignacionesTotales = list.Count(),
                            NivelDeServicio = (list.Where(x => x.RecoleccionAtiempo == false).Count() / list.Count()) * 100,
                        }
                        );
                    lstIncidencias.Add(
                           new NivelDeServicioIncidencias()
                           {
                               Transportista = "General",
                               ConsignacionesTotales = list.Count(),
                               ConsignacionesIncidencias = list.Where(x => x.TrackingServiceStatus == "INCIDENCIA").Count(),
                               PorcentajeIncidencias = decimal.Round(list.Where(x => x.TrackingServiceStatus == "INCIDENCIA").Count() / list.Count(), 2),
                           }
                           );
                    lstEntregadas.Add(
                        new NivelDeServicioEntregadas()
                        {
                            Transportista = "General",
                            ConsignacionesEntregadasEnTiempo = list.Where(x => x.EntregadaATiempo == true).Count(),
                            ConsignacionesEntregadasFueraTiempo = list.Where(x => x.EntregadaATiempo == false).Count(),
                            ConsignacionesTotales = list.Count(),
                            NivelDeServicio = decimal.Round(list.Where(x => x.EntregadaATiempo == false).Count() / list.Count(), 2),
                            CostoPromedio = decimal.Round(list.Sum(x => x.Costo) / list.Count(), 2),
                            IngresoPromedio = decimal.Round(list.Sum(x => x.totalIngreso) / list.Count(), 2),
                        }
                        );
                    lstAlmacen.Add(
                        new NivelDeSevicioAlmacenModel()
                        {
                            Almacen = "General",
                            ConsignacionesEntregadasEnTiempo = list.Where(x => x.EntregadaATiempo == true).Count(),
                            ConsignacionesEntregadasFueraTiempo = list.Where(x => x.EntregadaATiempo == false).Count(),
                            ConsignacionesTotales = list.Count(),
                            NivelDeServicio = decimal.Round(list.Where(x => x.EntregadaATiempo == false).Count() / list.Count(), 2),
                            CostoPromedio = decimal.Round(list.Sum(x => x.Costo) / list.Count(), 2),
                            IngresoPromedio = decimal.Round(list.Sum(x => x.totalIngreso) / list.Count(), 2),
                            TotalConsignacionesPagadas = list.Where(x => x.PagoAutorizado == true).Count()
                        }
                        );
                    foreach (var carrier in list.Where(x => x.UeType != "SETC").DistinctBy(x => x.Transportista).Select(x => x.Transportista))
                    {
                        NivelDeServicioTransitoModel newTransito = new NivelDeServicioTransitoModel();
                        newTransito.Transportista = carrier;
                        newTransito.ConsignacionesTotales = list.Where(x => x.Transportista == carrier).Count();
                        newTransito.ConsignacionesRecolectadasEnTiempo = list.Where(x => x.Transportista == carrier && x.TrackingServiceStatus == "EN_TRANSITO" && x.RecoleccionAtiempo == true).Count();
                        newTransito.ConsignacionesRecolectadasFueraTiempo = list.Where(x => x.Transportista == carrier && x.TrackingServiceStatus == "EN_TRANSITO" && x.RecoleccionAtiempo == false).Count();
                        newTransito.NivelDeServicio = decimal.Round(newTransito.ConsignacionesRecolectadasFueraTiempo > 0 ? decimal.Round((newTransito.ConsignacionesRecolectadasFueraTiempo / newTransito.ConsignacionesTotales) * 100, 3) : 0, 2);
                        newTransito.Tipo = "MG";

                        lstTransitos.Add(newTransito);

                        NivelDeServicioIncidencias newIncidencias = new NivelDeServicioIncidencias();
                        newIncidencias.Transportista = carrier;
                        newIncidencias.ConsignacionesTotales = list.Where(x => x.Transportista == carrier).Count();
                        newIncidencias.ConsignacionesIncidencias = list.Where(x => x.Transportista == carrier && x.TrackingServiceStatus == "INCIDENCIA").Count();
                        newIncidencias.PorcentajeIncidencias = decimal.Round(newIncidencias.ConsignacionesIncidencias > 0 ? newIncidencias.ConsignacionesIncidencias / newIncidencias.ConsignacionesTotales : 0, 2);
                        newIncidencias.Tipo = "MG";

                        lstIncidencias.Add(newIncidencias);

                        NivelDeServicioEntregadas newEntregadas = new NivelDeServicioEntregadas();
                        newEntregadas.Transportista = carrier;
                        newEntregadas.ConsignacionesTotales = list.Where(x => x.Transportista == carrier).Count();
                        newEntregadas.ConsignacionesEntregadasEnTiempo = list.Where(x => x.Transportista == carrier && x.TrackingServiceStatus == "Entregada" && x.EntregadaATiempo == true).Count();
                        newEntregadas.ConsignacionesEntregadasFueraTiempo = list.Where(x => x.Transportista == carrier && x.TrackingServiceStatus == "Entregada" && x.EntregadaATiempo == false).Count();
                        newEntregadas.NivelDeServicio = decimal.Round(newEntregadas.ConsignacionesEntregadasFueraTiempo > 0 ? newEntregadas.ConsignacionesEntregadasFueraTiempo / newEntregadas.ConsignacionesTotales : 0, 2);
                        newEntregadas.CostoPromedio = decimal.Round(list.Where(x => x.Transportista == carrier).Sum(x => x.Costo) > 0 ? list.Where(x => x.Transportista == carrier).Sum(x => x.Costo) / newEntregadas.ConsignacionesTotales : 0, 2);
                        newEntregadas.IngresoPromedio = decimal.Round(list.Where(x => x.Transportista == carrier).Sum(x => x.totalIngreso) > 0 ? list.Where(x => x.Transportista == carrier).Sum(x => x.totalIngreso) / newEntregadas.ConsignacionesTotales : 0, 2);
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
                        newAlmacen.NivelDeServicio = decimal.Round(newAlmacen.ConsignacionesEntregadasFueraTiempo > 0 ? newAlmacen.ConsignacionesEntregadasFueraTiempo / newAlmacen.ConsignacionesTotales : 0, 2);
                        newAlmacen.CostoPromedio = decimal.Round(list.Where(x => x.UeType == almacen).Sum(x => x.Costo) > 0 ? list.Where(x => x.UeType == almacen).Sum(x => x.Costo) / newAlmacen.ConsignacionesTotales : 0, 2);
                        newAlmacen.IngresoPromedio = decimal.Round(list.Where(x => x.UeType == almacen).Sum(x => x.totalIngreso) > 0 ? list.Where(x => x.UeType == almacen).Sum(x => x.totalIngreso) / newAlmacen.ConsignacionesTotales : 0 ,2);
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
                            NivelDeServicio = decimal.Round(lstTransitos.Where(x => x.Tipo == "MG").Sum(x => x.NivelDeServicio), 2),
                        }
                        );
                    lstIncidencias.Add(
                           new NivelDeServicioIncidencias()
                           {
                               Transportista = "Subtotal MG",
                               ConsignacionesTotales = lstIncidencias.Where(x => x.Tipo == "MG").Sum(x => x.ConsignacionesTotales),
                               ConsignacionesIncidencias = lstIncidencias.Where(x => x.Tipo == "MG").Sum(x => x.ConsignacionesIncidencias),
                               PorcentajeIncidencias = decimal.Round(lstIncidencias.Where(x => x.Tipo == "MG").Sum(x => x.PorcentajeIncidencias), 2)
                           }
                           );
                    lstEntregadas.Add(
                        new NivelDeServicioEntregadas()
                        {
                            Transportista = "Subtotal MG",
                            ConsignacionesEntregadasEnTiempo = lstEntregadas.Where(x => x.Tipo == "MG").Sum(x => x.ConsignacionesEntregadasEnTiempo),
                            ConsignacionesEntregadasFueraTiempo = lstEntregadas.Where(x => x.Tipo == "MG").Sum(x => x.ConsignacionesEntregadasFueraTiempo),
                            ConsignacionesTotales = lstEntregadas.Where(x => x.Tipo == "MG").Sum(x => x.ConsignacionesTotales),
                            NivelDeServicio = decimal.Round(lstEntregadas.Where(x => x.Tipo == "MG").Sum(x => x.NivelDeServicio), 2),
                            CostoPromedio = decimal.Round(lstEntregadas.Where(x => x.Tipo == "MG").Sum(x => x.CostoPromedio), 2),
                            IngresoPromedio = decimal.Round(lstEntregadas.Where(x => x.Tipo == "MG").Sum(x => x.IngresoPromedio), 2)
                        }
                        );
                    lstAlmacen.Add(
                        new NivelDeSevicioAlmacenModel()
                        {
                            Almacen = "Subtotal MG",
                            ConsignacionesEntregadasEnTiempo = lstAlmacen.Where(x => x.Tipo == "MG").Sum(x => x.ConsignacionesEntregadasEnTiempo),
                            ConsignacionesEntregadasFueraTiempo = lstAlmacen.Where(x => x.Tipo == "MG").Sum(x => x.ConsignacionesEntregadasFueraTiempo),
                            ConsignacionesTotales = lstAlmacen.Where(x => x.Tipo == "MG").Sum(x => x.ConsignacionesTotales),
                            CostoPromedio = decimal.Round(lstAlmacen.Where(x => x.Tipo == "MG").Sum(x => x.CostoPromedio), 2),
                            TotalConsignacionesPagadas = lstAlmacen.Where(x => x.Tipo == "MG").Sum(x => x.TotalConsignacionesPagadas),
                            NivelDeServicio = decimal.Round(lstAlmacen.Where(x => x.Tipo == "MG").Sum(x => x.NivelDeServicio), 2),
                            IngresoPromedio = decimal.Round(lstAlmacen.Where(x => x.Tipo == "MG").Sum(x => x.IngresoPromedio), 2),
                        }
                        );

                    //foreach (var carrierSETC in list.Where(x => x.UeType == "SETC").DistinctBy(x => x.Transportista).Select(x => x.Transportista))
                    //{
                    //    NivelDeServicioTransitoModel newTransito = new NivelDeServicioTransitoModel();
                    //    newTransito.Transportista = carrierSETC;
                    //    newTransito.ConsignacionesTotales = list.Where(x => x.Transportista == carrierSETC).Count();
                    //    newTransito.ConsignacionesRecolectadasEnTiempo = list.Where(x => x.Transportista == carrierSETC && x.TrackingServiceStatus == "EN_TRANSITO" && x.RecoleccionAtiempo == true).Count();
                    //    newTransito.ConsignacionesRecolectadasFueraTiempo = list.Where(x => x.Transportista == carrierSETC && x.TrackingServiceStatus == "EN_TRANSITO" && x.RecoleccionAtiempo == false).Count();
                    //    newTransito.NivelDeServicio = newTransito.ConsignacionesRecolectadasFueraTiempo > 0 ? decimal.Round(newTransito.ConsignacionesRecolectadasFueraTiempo / newTransito.ConsignacionesTotales, 3) : 0;
                    //    newTransito.Tipo = "SETC";

                    //    lstTransitos.Add(newTransito);

                    //    NivelDeServicioIncidencias newIncidencias = new NivelDeServicioIncidencias();
                    //    newIncidencias.Transportista = carrierSETC;
                    //    newIncidencias.ConsignacionesTotales = list.Where(x => x.Transportista == carrierSETC).Count();
                    //    newIncidencias.ConsignacionesIncidencias = list.Where(x => x.Transportista == carrierSETC && x.TrackingServiceStatus == "INCIDENCIA").Count();
                    //    newIncidencias.PorcentajeIncidencias = newIncidencias.ConsignacionesIncidencias > 0 ? newIncidencias.ConsignacionesIncidencias / newIncidencias.ConsignacionesTotales : 0;
                    //    newIncidencias.Tipo = "SETC";

                    //    lstIncidencias.Add(newIncidencias);

                    //    NivelDeServicioEntregadas newEntregadas = new NivelDeServicioEntregadas();
                    //    newEntregadas.Transportista = carrierSETC;
                    //    newEntregadas.ConsignacionesTotales = list.Where(x => x.Transportista == carrierSETC).Count();
                    //    newEntregadas.ConsignacionesEntregadasEnTiempo = list.Where(x => x.Transportista == carrierSETC && x.TrackingServiceStatus == "Entregada" && x.EntregadaATiempo == true).Count();
                    //    newEntregadas.ConsignacionesEntregadasFueraTiempo = list.Where(x => x.Transportista == carrierSETC && x.TrackingServiceStatus == "Entregada" && x.EntregadaATiempo == false).Count();
                    //    newEntregadas.NivelDeServicio = newEntregadas.ConsignacionesEntregadasFueraTiempo > 0 ? newEntregadas.ConsignacionesEntregadasFueraTiempo / newEntregadas.ConsignacionesTotales : 0;
                    //    newEntregadas.CostoPromedio = list.Where(x => x.Transportista == carrierSETC).Sum(x => x.Costo) > 0 ? list.Where(x => x.Transportista == carrierSETC).Sum(x => x.Costo) / newEntregadas.ConsignacionesTotales : 0;
                    //    newEntregadas.IngresoPromedio = list.Where(x => x.Transportista == carrierSETC).Sum(x => x.totalIngreso) > 0 ? list.Where(x => x.Transportista == carrierSETC).Sum(x => x.totalIngreso) / newEntregadas.ConsignacionesTotales : 0;
                    //    newEntregadas.Tipo = "SETC";

                    //    lstEntregadas.Add(newEntregadas);


                    //}

                    lstTransitos.Add(
                        new NivelDeServicioTransitoModel()
                        {
                            Transportista = "Subtotal SETC",
                            ConsignacionesRecolectadasEnTiempo = list.Where(x => x.UeType == "SETC" && x.TrackingServiceStatus == "EN_TRANSITO" && x.RecoleccionAtiempo == true).Count(),
                            ConsignacionesRecolectadasFueraTiempo = list.Where(x => x.UeType == "SETC" && x.TrackingServiceStatus == "EN_TRANSITO" && x.RecoleccionAtiempo == false).Count(),
                            ConsignacionesTotales = list.Where(x => x.UeType == "SETC").Count(),
                            NivelDeServicio = decimal.Round(list.Where(x => x.UeType == "SETC" && x.TrackingServiceStatus == "EN_TRANSITO" && x.RecoleccionAtiempo == false).Count() > 0 ? list.Where(x => x.UeType == "SETC" && x.TrackingServiceStatus == "EN_TRANSITO" && x.RecoleccionAtiempo == false).Count() / list.Where(x => x.UeType == "SETC" && x.TrackingServiceStatus == "EN_TRANSITO" && x.RecoleccionAtiempo == true).Count() :0, 2),
                        }
                        );
                    lstIncidencias.Add(
                           new NivelDeServicioIncidencias()
                           {
                               Transportista = "Subtotal SETC",
                               ConsignacionesTotales = list.Where(x => x.UeType == "SETC").Count(),
                               ConsignacionesIncidencias = list.Where(x => x.UeType == "SETC" && x.TrackingServiceStatus == "INCIDENCIA").Count(),
                               PorcentajeIncidencias = decimal.Round(list.Where(x => x.UeType == "SETC" && x.TrackingServiceStatus == "INCIDENCIA").Count() > 0 ? list.Where(x => x.UeType == "SETC" && x.TrackingServiceStatus == "INCIDENCIA").Count() / list.Where(x => x.UeType == "SETC").Count() : 0, 2)
                           }
                           );
                    lstEntregadas.Add(
                        new NivelDeServicioEntregadas()
                        {
                            Transportista = "Subtotal SETC",
                            ConsignacionesEntregadasEnTiempo = list.Where(x => x.UeType == "SETC" && x.TrackingServiceStatus == "Entregada" && x.EntregadaATiempo == true).Count(),
                            ConsignacionesEntregadasFueraTiempo = list.Where(x => x.UeType == "SETC" && x.TrackingServiceStatus == "Entregada" && x.EntregadaATiempo == false).Count(),
                            ConsignacionesTotales = list.Where(x => x.UeType == "SETC").Count(),
                            NivelDeServicio = decimal.Round(list.Where(x => x.UeType == "SETC" && x.TrackingServiceStatus == "Entregada" && x.EntregadaATiempo == false).Count() > 0 ? list.Where(x => x.UeType == "SETC" && x.TrackingServiceStatus == "Entregada" && x.EntregadaATiempo == false).Count() / list.Where(x => x.UeType == "SETC" && x.TrackingServiceStatus == "Entregada" && x.EntregadaATiempo == true).Count() :0, 2),
                            CostoPromedio = decimal.Round(list.Where(x => x.UeType == "SETC").Sum(x => x.Costo) / list.Where(x => x.UeType == "SETC").Count(), 2),
                            IngresoPromedio = decimal.Round(list.Where(x => x.UeType == "SETC").Sum(x => x.totalIngreso) / list.Where(x => x.UeType == "SETC").Count(), 2)
                        }
                        );
                    lstAlmacen.Add(
                        new NivelDeSevicioAlmacenModel()
                        {
                            Almacen = "Subtotal SETC",
                            ConsignacionesEntregadasEnTiempo = list.Where(x => x.UeType == "SETC" && x.EntregadaATiempo == true).Count(),
                            ConsignacionesEntregadasFueraTiempo = list.Where(x => x.UeType == "SETC" && x.EntregadaATiempo == false).Count(),
                            ConsignacionesTotales = list.Where(x => x.UeType == "SETC").Count(),
                            CostoPromedio = decimal.Round(list.Where(x => x.UeType == "SETC").Sum(x => x.Costo) / list.Where(x => x.UeType == "SETC").Count(), 2),
                            TotalConsignacionesPagadas = list.Where(x => x.UeType == "SETC").Count(),
                            NivelDeServicio = decimal.Round(list.Where(x => x.UeType == "SETC" && x.EntregadaATiempo == false).Count() / list.Where(x => x.UeType == "SETC").Count(), 2),
                            IngresoPromedio = decimal.Round(list.Where(x => x.UeType == "SETC").Sum(x => x.totalIngreso) / list.Where(x => x.UeType == "SETC").Count(),2),
                        }
                        );
                }
                var result = new { Success = true, enTransito = lstTransitos.Where(x => x.Tipo != "SETC"), enIncidencias = lstIncidencias.Where(x => x.Tipo != "SETC"), entregadas = lstEntregadas.Where(x => x.Tipo != "SETC"), porAlmacen = lstAlmacen.Where(x => x.Tipo != "SETC") };
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


        #region REPORTE SETCE
        public ActionResult Setce(string feIni, string feFin)
        {
            if (string.IsNullOrEmpty(feIni) || string.IsNullOrEmpty(feFin))
            {

                ViewBag.FecIni = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
                ViewBag.FecFin = DateTime.Now.ToString("yyyy/MM/dd");
                
            }
            else
            {
                ViewBag.FecIni = feIni;
                ViewBag.FecFin = feFin;
     
            }
            return View();
        }

        [HttpPost]
        public ActionResult GetSetce(string FechaIni,string FechaFin)
        {
            List<SETCE> lst = new List<SETCE>();

            //logistica datatable
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault().ToLower();



            #region Se Obtienen Filtros Por Columna
            var AnioServ = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault().ToLower();
            var NoMesServ = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault().ToLower();
            var NomMesSer = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault().ToLower();
            var AnioConsig = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault().ToLower();
            var NoMesConsig = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault().ToLower();
            var NomMesConsig = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault().ToLower();
            var FecCreacionConsig = Request.Form.GetValues("columns[6][search][value]").FirstOrDefault().ToLower();
            var HoraCreacionConsig = Request.Form.GetValues("columns[7][search][value]").FirstOrDefault().ToLower();
            var Consignacion = Request.Form.GetValues("columns[8][search][value]").FirstOrDefault().ToLower();
            var NoTda = Request.Form.GetValues("columns[9][search][value]").FirstOrDefault().ToLower();
            var NomTda = Request.Form.GetValues("columns[10][search][value]").FirstOrDefault().ToLower();
            var Formato = Request.Form.GetValues("columns[11][search][value]").FirstOrDefault().ToLower();
            var Direccion = Request.Form.GetValues("columns[12][search][value]").FirstOrDefault().ToLower();
            var Director = Request.Form.GetValues("columns[13][search][value]").FirstOrDefault().ToLower();
            var Region = Request.Form.GetValues("columns[14][search][value]").FirstOrDefault().ToLower();
            var EdoOrigen = Request.Form.GetValues("columns[15][search][value]").FirstOrDefault().ToLower();
            var EdoDestino = Request.Form.GetValues("columns[16][search][value]").FirstOrDefault().ToLower();
            var CpOrigen = Request.Form.GetValues("columns[17][search][value]").FirstOrDefault().ToLower();
            var CpDestino = Request.Form.GetValues("columns[18][search][value]").FirstOrDefault().ToLower();
            var TipoEntrega = Request.Form.GetValues("columns[19][search][value]").FirstOrDefault().ToLower();
            var FecPagoConsig = Request.Form.GetValues("columns[20][search][value]").FirstOrDefault().ToLower();
            var HoraPagoConsig = Request.Form.GetValues("columns[21][search][value]").FirstOrDefault().ToLower();
            var EstatusPAgo = Request.Form.GetValues("columns[22][search][value]").FirstOrDefault().ToLower();
            var FecConsigSurtido = Request.Form.GetValues("columns[23][search][value]").FirstOrDefault().ToLower();
            var HoraConsigSurtido = Request.Form.GetValues("columns[24][search][value]").FirstOrDefault().ToLower();
            var TransAsignado = Request.Form.GetValues("columns[25][search][value]").FirstOrDefault().ToLower();
            var GuiaTrans = Request.Form.GetValues("columns[26][search][value]").FirstOrDefault().ToLower();
            var GuiaSoriana = Request.Form.GetValues("columns[27][search][value]").FirstOrDefault().ToLower();
            var FecRecoleccion = Request.Form.GetValues("columns[28][search][value]").FirstOrDefault().ToLower();
            var HoraRecoleccion = Request.Form.GetValues("columns[29][search][value]").FirstOrDefault().ToLower();
            var EstatusHD = Request.Form.GetValues("columns[30][search][value]").FirstOrDefault().ToLower();
            var FecEntregaClient = Request.Form.GetValues("columns[31][search][value]").FirstOrDefault().ToLower();
            var HoraMaxComEntrClie = Request.Form.GetValues("columns[32][search][value]").FirstOrDefault().ToLower();
            var FecComEnt = Request.Form.GetValues("columns[33][search][value]").FirstOrDefault().ToLower();
            var HoraComEnt = Request.Form.GetValues("columns[34][search][value]").FirstOrDefault().ToLower();
            var DiasTransRecEntrega = Request.Form.GetValues("columns[35][search][value]").FirstOrDefault().ToLower();
            var DiasCreacionEntrega = Request.Form.GetValues("columns[36][search][value]").FirstOrDefault().ToLower();
            var HorasComTrans = Request.Form.GetValues("columns[37][search][value]").FirstOrDefault().ToLower();
            var VariacionRecoleccion = Request.Form.GetValues("columns[38][search][value]").FirstOrDefault().ToLower();
            var VariacionCreacion = Request.Form.GetValues("columns[39][search][value]").FirstOrDefault().ToLower();
            var CumplimientEmbarque = Request.Form.GetValues("columns[40][search][value]").FirstOrDefault().ToLower();
            var CumplimientoCreacion = Request.Form.GetValues("columns[41][search][value]").FirstOrDefault().ToLower();
            var EnviosConsignacion = Request.Form.GetValues("columns[42][search][value]").FirstOrDefault().ToLower();
            var CostoTrans = Request.Form.GetValues("columns[43][search][value]").FirstOrDefault().ToLower();
            var CostoEnvClienteCot = Request.Form.GetValues("columns[44][search][value]").FirstOrDefault().ToLower();
            var CostoEnvClienteReal = Request.Form.GetValues("columns[45][search][value]").FirstOrDefault().ToLower();
            var TicketVta = Request.Form.GetValues("columns[46][search][value]").FirstOrDefault().ToLower();
            var ProdConsignacion = Request.Form.GetValues("columns[47][search][value]").FirstOrDefault().ToLower();
            var PzasConsignacion = Request.Form.GetValues("columns[48][search][value]").FirstOrDefault().ToLower();



            #endregion

            pageSize = length != null ? Convert.ToInt32(length) : 0;
            skip = start != null ? Convert.ToInt32(start) : 0;
            recordsTotal = 0;

            //IQueryable<SETCE> query = from row in lst.AsQueryable() select new SETCE { };

            IQueryable<SETCE> query = from row in DALReportes.ReporteSETC(FechaIni,FechaFin).Tables[0].AsEnumerable().AsQueryable()
                                      select new SETCE()
                                      {
                                          AnioServ = row[0].ToString(),
                                          NoMesServ = row[1].ToString(),
                                          NomMesSer = row[2].ToString(),
                                          AnioConsig = row[3].ToString(),
                                          NoMesConsig = row[4].ToString(),
                                          NomMesConsig = row[5].ToString(),
                                          FecCreacionConsig = row[6].ToString(),
                                          HoraCreacionConsig = row[7].ToString(),
                                          Consignacion = row[8].ToString(),
                                          NoTda = row[0].ToString(),
                                          NomTda = row[10].ToString(),
                                          Formato = row[11].ToString(),
                                          Direccion = row[12].ToString(),
                                          Director = row[13].ToString(),
                                          Region = row[14].ToString(),
                                          EdoOrigen = row[15].ToString(),
                                          EdoDestino = row[16].ToString(),
                                          CpOrigen = row[17].ToString(),
                                          CpDestino = row[18].ToString(),
                                          TipoEntrega = row[19].ToString(),
                                          FecPagoConsig = row[20].ToString(),
                                          HoraPagoConsig = row[21].ToString(),
                                          EstatusPAgo = row[22].ToString(),
                                          FecConsigSurtido = row[23].ToString(),
                                          HoraConsigSurtido = row[24].ToString(),
                                          TransAsignado = row[25].ToString(),
                                          GuiaTrans = row[26].ToString(),
                                          GuiaSoriana = row[27].ToString(),
                                          FecRecoleccion = row[28].ToString(),
                                          HoraRecoleccion = row[29].ToString(),
                                          EstatusHD = row[30].ToString(),
                                          FecEntregaClient = row[31].ToString(),
                                          HoraMaxComEntrClie = row[32].ToString(),
                                          FecComEnt = row[33].ToString(),
                                          HoraComEnt = row[34].ToString(),
                                          DiasTransRecEntrega = row[35].ToString(),
                                          DiasCreacionEntrega = row[36].ToString(),
                                          HorasComTrans = row[37].ToString(),
                                          VariacionRecoleccion = row[38].ToString(),
                                          VariacionCreacion = row[39].ToString(),
                                          CumplimientEmbarque = row[40].ToString(),
                                          CumplimientoCreacion = row[41].ToString(),
                                          EnviosConsignacion = row[42].ToString(),
                                          CostoTrans = row[43].ToString(),
                                          CostoEnvClienteCot = row[44].ToString(),
                                          CostoEnvClienteReal = row[45].ToString(),
                                          TicketVta = row[46].ToString(),
                                          ProdConsignacion = row[47].ToString(),
                                          PzasConsignacion = row[48].ToString(),
                                         
                                         


                                      };




            if (searchValue != "")
                query = query.Where(d => d.AnioServ.ToLower().Contains(searchValue)
                || d.NoMesServ.ToLower().Contains(searchValue)
                || d.NomMesSer.ToString().ToLower().Contains(searchValue)
                || d.AnioConsig.ToString().ToLower().Contains(searchValue)
                || d.NoMesConsig.ToString().ToLower().Contains(searchValue)
                || d.NomMesConsig.ToString().ToLower().Contains(searchValue)
                || d.FecCreacionConsig.ToString().ToLower().Contains(searchValue)
                || d.HoraCreacionConsig.ToString().ToLower().Contains(searchValue)
                || d.Consignacion.ToString().ToLower().Contains(searchValue)
                || d.NoTda.ToString().ToLower().Contains(searchValue)
                || d.NomTda.ToString().ToLower().Contains(searchValue)
                || d.Formato.ToString().ToLower().Contains(searchValue)
                || d.Direccion.ToString().ToLower().Contains(searchValue)
                || d.Director.ToString().ToLower().Contains(searchValue)
                || d.Region.ToString().ToLower().Contains(searchValue)
                || d.EdoOrigen.ToString().ToLower().Contains(searchValue)
                || d.EdoDestino.ToString().ToLower().Contains(searchValue)
                || d.CpOrigen.ToString().ToLower().Contains(searchValue)
                || d.CpDestino.ToString().ToLower().Contains(searchValue)
                || d.TipoEntrega.ToString().ToLower().Contains(searchValue)
                || d.FecPagoConsig.ToString().ToLower().Contains(searchValue)
                || d.HoraPagoConsig.ToString().ToLower().Contains(searchValue)
                || d.EstatusPAgo.ToString().ToLower().Contains(searchValue)
                || d.FecConsigSurtido.ToString().ToLower().Contains(searchValue)
                || d.HoraConsigSurtido.ToString().ToLower().Contains(searchValue)
                || d.TransAsignado.ToString().ToLower().Contains(searchValue)
                || d.GuiaTrans.ToString().ToLower().Contains(searchValue)
                || d.GuiaSoriana.ToString().ToLower().Contains(searchValue)
                || d.FecRecoleccion.ToString().ToLower().Contains(searchValue)
                || d.HoraRecoleccion.ToString().ToLower().Contains(searchValue)
                || d.EstatusHD.ToString().ToLower().Contains(searchValue)
                || d.FecEntregaClient.ToString().ToLower().Contains(searchValue)
                || d.HoraMaxComEntrClie.ToString().ToLower().Contains(searchValue)
                || d.FecComEnt.ToString().ToLower().Contains(searchValue)
                || d.HoraComEnt.ToString().ToLower().Contains(searchValue)
                || d.DiasTransRecEntrega.ToString().ToLower().Contains(searchValue)
                || d.DiasCreacionEntrega.ToString().ToLower().Contains(searchValue)
                || d.HorasComTrans.ToString().ToLower().Contains(searchValue)
                || d.VariacionRecoleccion.ToString().ToLower().Contains(searchValue)
                || d.VariacionCreacion.ToString().ToLower().Contains(searchValue)
                || d.CumplimientEmbarque.ToString().ToLower().Contains(searchValue)
                || d.CumplimientoCreacion.ToString().ToLower().Contains(searchValue)
                || d.EnviosConsignacion.ToString().ToLower().Contains(searchValue)
                || d.CostoTrans.ToString().ToLower().Contains(searchValue)
                || d.CostoEnvClienteCot.ToString().ToLower().Contains(searchValue)
                || d.CostoEnvClienteReal.ToString().ToLower().Contains(searchValue)
                || d.TicketVta.ToString().ToLower().Contains(searchValue)
                || d.ProdConsignacion.ToString().ToLower().Contains(searchValue)
                || d.PzasConsignacion.ToString().ToLower().Contains(searchValue)
                
                );





            //Filter By Columns
            #region Filter By Columns
            if (!string.IsNullOrEmpty(AnioServ))
            {
                query = query.Where(a => a.AnioServ.ToLower().Contains(AnioServ));
            }

            if (!string.IsNullOrEmpty(NoMesServ))
            {
                query = query.Where(a => a.NoMesServ.ToLower().Contains(NoMesServ));
            }
            if (!string.IsNullOrEmpty(NomMesSer))
            {
                query = query.Where(a => a.NomMesSer.ToLower().Contains(NomMesSer));
            }
            if (!string.IsNullOrEmpty(AnioConsig))
            {
                query = query.Where(a => a.AnioConsig.ToLower().Contains(AnioConsig));
            }
            if (!string.IsNullOrEmpty(NoMesConsig))
            {
                query = query.Where(a => a.NoMesConsig.ToLower().Contains(NoMesConsig));
            }
            if (!string.IsNullOrEmpty(NomMesConsig))
            {
                query = query.Where(a => a.NomMesConsig.ToLower().Contains(NomMesConsig));
            }
            if (!string.IsNullOrEmpty(FecCreacionConsig))
            {
                query = query.Where(a => a.FecCreacionConsig.ToLower().Contains(FecCreacionConsig));
            }
            if (!string.IsNullOrEmpty(HoraCreacionConsig))
            {
                query = query.Where(a => a.HoraCreacionConsig.ToLower().Contains(HoraCreacionConsig));
            }
            if (!string.IsNullOrEmpty(Consignacion))
            {
                query = query.Where(a => a.Consignacion.ToLower().Contains(Consignacion));
            }
            if (!string.IsNullOrEmpty(NoTda))
            {
                query = query.Where(a => a.NoTda.ToLower().Contains(NoTda));
            }
            if (!string.IsNullOrEmpty(NomTda))
            {
                query = query.Where(a => a.NomTda.ToLower().Contains(NomTda));
            }
            if (!string.IsNullOrEmpty(Formato))
            {
                query = query.Where(a => a.Formato.ToLower().Contains(Formato));
            }
            if (!string.IsNullOrEmpty(Direccion))
            {
                query = query.Where(a => a.Direccion.ToLower().Contains(Direccion));
            }
            if (!string.IsNullOrEmpty(Director))
            {
                query = query.Where(a => a.Director.ToLower().Contains(Director));
            }
            if (!string.IsNullOrEmpty(Region))
            {
                query = query.Where(a => a.Region.ToLower().Contains(Region));
            }
            if (!string.IsNullOrEmpty(EdoOrigen))
            {
                query = query.Where(a => a.EdoOrigen.ToLower().Contains(EdoOrigen));
            }
            if (!string.IsNullOrEmpty(EdoDestino))
            {
                query = query.Where(a => a.EdoDestino.ToLower().Contains(EdoDestino));
            }
            if (!string.IsNullOrEmpty(CpOrigen))
            {
                query = query.Where(a => a.CpOrigen.ToLower().Contains(CpOrigen));
            }
            if (!string.IsNullOrEmpty(CpDestino))
            {
                query = query.Where(a => a.CpDestino.ToLower().Contains(CpDestino));
            }
            if (!string.IsNullOrEmpty(TipoEntrega))
            {
                query = query.Where(a => a.TipoEntrega.ToLower().Contains(TipoEntrega));
            }
            if (!string.IsNullOrEmpty(FecPagoConsig))
            {
                query = query.Where(a => a.FecPagoConsig.ToLower().Contains(FecPagoConsig));
            }
            if (!string.IsNullOrEmpty(HoraPagoConsig))
            {
                query = query.Where(a => a.HoraPagoConsig.ToLower().Contains(HoraPagoConsig));
            }
            if (!string.IsNullOrEmpty(EstatusPAgo))
            {
                query = query.Where(a => a.EstatusPAgo.ToLower().Contains(EstatusPAgo));
            }
            if (!string.IsNullOrEmpty(FecConsigSurtido))
            {
                query = query.Where(a => a.FecConsigSurtido.ToLower().Contains(FecConsigSurtido));
            }
            if (!string.IsNullOrEmpty(HoraConsigSurtido))
            {
                query = query.Where(a => a.HoraConsigSurtido.ToLower().Contains(HoraConsigSurtido));
            }
            if (!string.IsNullOrEmpty(TransAsignado))
            {
                query = query.Where(a => a.TransAsignado.ToLower().Contains(TransAsignado));
            }
            if (!string.IsNullOrEmpty(GuiaTrans))
            {
                query = query.Where(a => a.GuiaTrans.ToLower().Contains(GuiaTrans));
            }
            if (!string.IsNullOrEmpty(GuiaSoriana))
            {
                query = query.Where(a => a.GuiaSoriana.ToLower().Contains(GuiaSoriana));
            }
            if (!string.IsNullOrEmpty(FecRecoleccion))
            {
                query = query.Where(a => a.FecRecoleccion.ToLower().Contains(FecRecoleccion));
            }
            if (!string.IsNullOrEmpty(HoraRecoleccion))
            {
                query = query.Where(a => a.HoraRecoleccion.ToLower().Contains(HoraRecoleccion));
            }
            if (!string.IsNullOrEmpty(EstatusHD))
            {
                query = query.Where(a => a.EstatusHD.ToLower().Contains(EstatusHD));
            }
            if (!string.IsNullOrEmpty(FecEntregaClient))
            {
                query = query.Where(a => a.FecEntregaClient.ToLower().Contains(FecEntregaClient));
            }
            if (!string.IsNullOrEmpty(HoraMaxComEntrClie))
            {
                query = query.Where(a => a.HoraMaxComEntrClie.ToLower().Contains(HoraMaxComEntrClie));
            }
            if (!string.IsNullOrEmpty(FecComEnt))
            {
                query = query.Where(a => a.FecComEnt.ToLower().Contains(FecComEnt));
            }
            if (!string.IsNullOrEmpty(HoraComEnt))
            {
                query = query.Where(a => a.HoraComEnt.ToLower().Contains(HoraComEnt));
            }
            if (!string.IsNullOrEmpty(DiasTransRecEntrega))
            {
                query = query.Where(a => a.DiasTransRecEntrega.ToLower().Contains(DiasTransRecEntrega));
            }
            if (!string.IsNullOrEmpty(DiasCreacionEntrega))
            {
                query = query.Where(a => a.DiasCreacionEntrega.ToLower().Contains(DiasCreacionEntrega));
            }
            if (!string.IsNullOrEmpty(HorasComTrans))
            {
                query = query.Where(a => a.HorasComTrans.ToLower().Contains(HorasComTrans));
            }
            if (!string.IsNullOrEmpty(VariacionRecoleccion))
            {
                query = query.Where(a => a.VariacionRecoleccion.ToLower().Contains(VariacionRecoleccion));
            }
            if (!string.IsNullOrEmpty(VariacionCreacion))
            {
                query = query.Where(a => a.VariacionCreacion.ToLower().Contains(VariacionCreacion));
            }
            if (!string.IsNullOrEmpty(CumplimientEmbarque))
            {
                query = query.Where(a => a.CumplimientEmbarque.ToLower().Contains(CumplimientEmbarque));
            }
            if (!string.IsNullOrEmpty(CumplimientoCreacion))
            {
                query = query.Where(a => a.CumplimientoCreacion.ToLower().Contains(CumplimientoCreacion));
            }
            if (!string.IsNullOrEmpty(EnviosConsignacion))
            {
                query = query.Where(a => a.EnviosConsignacion.ToLower().Contains(EnviosConsignacion));
            }
            if (!string.IsNullOrEmpty(CostoTrans))
            {
                query = query.Where(a => a.CostoTrans.ToLower().Contains(CostoTrans));
            }
            if (!string.IsNullOrEmpty(CostoEnvClienteCot))
            {
                query = query.Where(a => a.CostoEnvClienteCot.ToLower().Contains(CostoEnvClienteCot));
            }
            if (!string.IsNullOrEmpty(CostoEnvClienteReal))
            {
                query = query.Where(a => a.CostoEnvClienteReal.ToLower().Contains(CostoEnvClienteReal));
            }
            if (!string.IsNullOrEmpty(TicketVta))
            {
                query = query.Where(a => a.TicketVta.ToLower().Contains(TicketVta));
            }
            if (!string.IsNullOrEmpty(ProdConsignacion))
            {
                query = query.Where(a => a.ProdConsignacion.ToLower().Contains(ProdConsignacion));
            }
            if (!string.IsNullOrEmpty(PzasConsignacion))
            {
                query = query.Where(a => a.PzasConsignacion.ToLower().Contains(PzasConsignacion));
            }

            #endregion

            //Sorting    
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                query = query.OrderBy(sortColumn + " " + sortColumnDir);

            }

            recordsTotal = query.Count();

            lst = query.Skip(skip).Take(pageSize).ToList();


            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = lst });
        }

        public FileResult ExcelSetce(string AnioServ
    , string NoMesServ, string NomMesSer, string AnioConsig, string NoMesConsig
    , string NomMesConsig, string FecCreacionConsig
    , string HoraCreacionConsig, string Consignacion, string NoTda
    , string NomTda, string Formato, string Direccion
    , string Director, string Region, string EdoOrigen
    , string EdoDestino, string CpOrigen, string CpDestino
    , string CreatedDate, string CreatedTime, string BitActivo
    , string TipoEntrega, string FecPagoConsig, string HoraPagoConsig
    , string EstatusPAgo, string FecConsigSurtido, string HoraConsigSurtido
    , string TransAsignado, string GuiaTrans, string GuiaSoriana
    , string FecRecoleccion, string HoraRecoleccion, string EstatusHD
    , string FecEntregaClient, string HoraMaxComEntrClie, string FecComEnt
    , string HoraComEnt, string DiasTransRecEntrega, string DiasCreacionEntrega
    , string HorasComTrans, string VariacionRecoleccion, string VariacionCreacion
    , string CumplimientEmbarque, string CumplimientoCreacion, string EnviosConsignacion
    , string CostoTrans, string CostoEnvClienteCot, string CostoEnvClienteReal
    , string TicketVta, string ProdConsignacion, string PzasConsignacion     
            ,string FecIni,string FecFin
            , string searchValue)

        {
            List<SETCE> lst = new List<SETCE>();

            //IQueryable<SETCE> query = lst.AsQueryable();


            IQueryable<SETCE> query = from row in DALReportes.ReporteSETC(FecIni, FecFin).Tables[0].AsEnumerable().AsQueryable()
                                      select new SETCE()
                                      {
                                          AnioServ = row[0].ToString(),
                                          NoMesServ = row[1].ToString(),
                                          NomMesSer = row[2].ToString(),
                                          AnioConsig = row[3].ToString(),
                                          NoMesConsig = row[4].ToString(),
                                          NomMesConsig = row[5].ToString(),
                                          FecCreacionConsig = row[6].ToString(),
                                          HoraCreacionConsig = row[7].ToString(),
                                          Consignacion = row[8].ToString(),
                                          NoTda = row[0].ToString(),
                                          NomTda = row[10].ToString(),
                                          Formato = row[11].ToString(),
                                          Direccion = row[12].ToString(),
                                          Director = row[13].ToString(),
                                          Region = row[14].ToString(),
                                          EdoOrigen = row[15].ToString(),
                                          EdoDestino = row[16].ToString(),
                                          CpOrigen = row[17].ToString(),
                                          CpDestino = row[18].ToString(),
                                          TipoEntrega = row[19].ToString(),
                                          FecPagoConsig = row[20].ToString(),
                                          HoraPagoConsig = row[21].ToString(),
                                          EstatusPAgo = row[22].ToString(),
                                          FecConsigSurtido = row[23].ToString(),
                                          HoraConsigSurtido = row[24].ToString(),
                                          TransAsignado = row[25].ToString(),
                                          GuiaTrans = row[26].ToString(),
                                          GuiaSoriana = row[27].ToString(),
                                          FecRecoleccion = row[28].ToString(),
                                          HoraRecoleccion = row[29].ToString(),
                                          EstatusHD = row[30].ToString(),
                                          FecEntregaClient = row[31].ToString(),
                                          HoraMaxComEntrClie = row[32].ToString(),
                                          FecComEnt = row[33].ToString(),
                                          HoraComEnt = row[34].ToString(),
                                          DiasTransRecEntrega = row[35].ToString(),
                                          DiasCreacionEntrega = row[36].ToString(),
                                          HorasComTrans = row[37].ToString(),
                                          VariacionRecoleccion = row[38].ToString(),
                                          VariacionCreacion = row[39].ToString(),
                                          CumplimientEmbarque = row[40].ToString(),
                                          CumplimientoCreacion = row[41].ToString(),
                                          EnviosConsignacion = row[42].ToString(),
                                          CostoTrans = row[43].ToString(),
                                          CostoEnvClienteCot = row[44].ToString(),
                                          CostoEnvClienteReal = row[45].ToString(),
                                          TicketVta = row[46].ToString(),
                                          ProdConsignacion = row[47].ToString(),
                                          PzasConsignacion = row[48].ToString(),




                                      };




            #region MyRegion
            if (searchValue != "")
                query = query.Where(x => x.AnioServ.ToLower().Contains(searchValue)
                || x.NoMesServ.ToLower().Contains(searchValue)
                || x.NomMesSer.ToString().ToLower().Contains(searchValue)
                || x.AnioConsig.ToString().ToLower().Contains(searchValue)
                || x.NoMesConsig.ToString().ToLower().Contains(searchValue)
                || x.NomMesConsig.ToString().ToLower().Contains(searchValue)
                || x.FecCreacionConsig.ToString().ToLower().Contains(searchValue)
                || x.HoraCreacionConsig.ToString().ToLower().Contains(searchValue)
                || x.Consignacion.ToString().ToLower().Contains(searchValue)
                || x.NoTda.ToString().ToLower().Contains(searchValue)
                || x.NomTda.ToString().ToLower().Contains(searchValue)
                || x.Formato.ToString().ToLower().Contains(searchValue)
                || x.Direccion.ToString().ToLower().Contains(searchValue)
                || x.Director.ToString().ToLower().Contains(searchValue)
                || x.Region.ToString().ToLower().Contains(searchValue)
                || x.EdoOrigen.ToString().ToLower().Contains(searchValue)
                || x.EdoDestino.ToString().ToLower().Contains(searchValue)
                || x.CpOrigen.ToString().ToLower().Contains(searchValue)
                || x.CpDestino.ToString().ToLower().Contains(searchValue)
                || x.TipoEntrega.ToString().ToLower().Contains(searchValue)
                || x.FecPagoConsig.ToString().ToLower().Contains(searchValue)
                || x.HoraPagoConsig.ToString().ToLower().Contains(searchValue)
                || x.EstatusPAgo.ToString().ToLower().Contains(searchValue)
                || x.FecConsigSurtido.ToString().ToLower().Contains(searchValue)
                || x.HoraConsigSurtido.ToString().ToLower().Contains(searchValue)
                || x.TransAsignado.ToString().ToLower().Contains(searchValue)
                || x.GuiaTrans.ToString().ToLower().Contains(searchValue)
                || x.GuiaSoriana.ToString().ToLower().Contains(searchValue)
                || x.FecRecoleccion.ToString().ToLower().Contains(searchValue)
                || x.HoraRecoleccion.ToString().ToLower().Contains(searchValue)
                || x.EstatusHD.ToString().ToLower().Contains(searchValue)
                || x.FecEntregaClient.ToString().ToLower().Contains(searchValue)
                || x.HoraMaxComEntrClie.ToString().ToLower().Contains(searchValue)
                || x.FecComEnt.ToString().ToLower().Contains(searchValue)
                || x.HoraComEnt.ToString().ToLower().Contains(searchValue)
                || x.DiasTransRecEntrega.ToString().ToLower().Contains(searchValue)
                || x.DiasCreacionEntrega.ToString().ToLower().Contains(searchValue)
                || x.HorasComTrans.ToString().ToLower().Contains(searchValue)
                || x.VariacionRecoleccion.ToString().ToLower().Contains(searchValue)
                || x.VariacionCreacion.ToString().ToLower().Contains(searchValue)
                || x.CumplimientEmbarque.ToString().ToLower().Contains(searchValue)
                || x.CumplimientoCreacion.ToString().ToLower().Contains(searchValue)
                || x.EnviosConsignacion.ToString().ToLower().Contains(searchValue)
                || x.CostoTrans.ToString().ToLower().Contains(searchValue)
                || x.CostoEnvClienteCot.ToString().ToLower().Contains(searchValue)
                || x.CostoEnvClienteReal.ToString().ToLower().Contains(searchValue)
                || x.TicketVta.ToString().ToLower().Contains(searchValue)
                || x.ProdConsignacion.ToString().ToLower().Contains(searchValue)
                || x.PzasConsignacion.ToString().ToLower().Contains(searchValue)

                );
            #endregion





            #region Filter By Columns
            if (!string.IsNullOrEmpty(AnioServ))
            {
                query = query.Where(a => a.AnioServ.ToLower().Contains(AnioServ));
            }

            if (!string.IsNullOrEmpty(NoMesServ))
            {
                query = query.Where(a => a.NoMesServ.ToLower().Contains(NoMesServ));
            }
            if (!string.IsNullOrEmpty(NomMesSer))
            {
                query = query.Where(a => a.NomMesSer.ToLower().Contains(NomMesSer));
            }
            if (!string.IsNullOrEmpty(AnioConsig))
            {
                query = query.Where(a => a.AnioConsig.ToLower().Contains(AnioConsig));
            }
            if (!string.IsNullOrEmpty(NoMesConsig))
            {
                query = query.Where(a => a.NoMesConsig.ToLower().Contains(NoMesConsig));
            }
            if (!string.IsNullOrEmpty(NomMesConsig))
            {
                query = query.Where(a => a.NomMesConsig.ToLower().Contains(NomMesConsig));
            }
            if (!string.IsNullOrEmpty(FecCreacionConsig))
            {
                query = query.Where(a => a.FecCreacionConsig.ToLower().Contains(FecCreacionConsig));
            }
            if (!string.IsNullOrEmpty(HoraCreacionConsig))
            {
                query = query.Where(a => a.HoraCreacionConsig.ToLower().Contains(HoraCreacionConsig));
            }
            if (!string.IsNullOrEmpty(Consignacion))
            {
                query = query.Where(a => a.Consignacion.ToLower().Contains(Consignacion));
            }
            if (!string.IsNullOrEmpty(NoTda))
            {
                query = query.Where(a => a.NoTda.ToLower().Contains(NoTda));
            }
            if (!string.IsNullOrEmpty(NomTda))
            {
                query = query.Where(a => a.NomTda.ToLower().Contains(NomTda));
            }
            if (!string.IsNullOrEmpty(Formato))
            {
                query = query.Where(a => a.Formato.ToLower().Contains(Formato));
            }
            if (!string.IsNullOrEmpty(Direccion))
            {
                query = query.Where(a => a.Direccion.ToLower().Contains(Direccion));
            }
            if (!string.IsNullOrEmpty(Director))
            {
                query = query.Where(a => a.Director.ToLower().Contains(Director));
            }
            if (!string.IsNullOrEmpty(Region))
            {
                query = query.Where(a => a.Region.ToLower().Contains(Region));
            }
            if (!string.IsNullOrEmpty(EdoOrigen))
            {
                query = query.Where(a => a.EdoOrigen.ToLower().Contains(EdoOrigen));
            }
            if (!string.IsNullOrEmpty(EdoDestino))
            {
                query = query.Where(a => a.EdoDestino.ToLower().Contains(EdoDestino));
            }
            if (!string.IsNullOrEmpty(CpOrigen))
            {
                query = query.Where(a => a.CpOrigen.ToLower().Contains(CpOrigen));
            }
            if (!string.IsNullOrEmpty(CpDestino))
            {
                query = query.Where(a => a.CpDestino.ToLower().Contains(CpDestino));
            }
            if (!string.IsNullOrEmpty(TipoEntrega))
            {
                query = query.Where(a => a.TipoEntrega.ToLower().Contains(TipoEntrega));
            }
            if (!string.IsNullOrEmpty(FecPagoConsig))
            {
                query = query.Where(a => a.FecPagoConsig.ToLower().Contains(FecPagoConsig));
            }
            if (!string.IsNullOrEmpty(HoraPagoConsig))
            {
                query = query.Where(a => a.HoraPagoConsig.ToLower().Contains(HoraPagoConsig));
            }
            if (!string.IsNullOrEmpty(EstatusPAgo))
            {
                query = query.Where(a => a.EstatusPAgo.ToLower().Contains(EstatusPAgo));
            }
            if (!string.IsNullOrEmpty(FecConsigSurtido))
            {
                query = query.Where(a => a.FecConsigSurtido.ToLower().Contains(FecConsigSurtido));
            }
            if (!string.IsNullOrEmpty(HoraConsigSurtido))
            {
                query = query.Where(a => a.HoraConsigSurtido.ToLower().Contains(HoraConsigSurtido));
            }
            if (!string.IsNullOrEmpty(TransAsignado))
            {
                query = query.Where(a => a.TransAsignado.ToLower().Contains(TransAsignado));
            }
            if (!string.IsNullOrEmpty(GuiaTrans))
            {
                query = query.Where(a => a.GuiaTrans.ToLower().Contains(GuiaTrans));
            }
            if (!string.IsNullOrEmpty(GuiaSoriana))
            {
                query = query.Where(a => a.GuiaSoriana.ToLower().Contains(GuiaSoriana));
            }
            if (!string.IsNullOrEmpty(FecRecoleccion))
            {
                query = query.Where(a => a.FecRecoleccion.ToLower().Contains(FecRecoleccion));
            }
            if (!string.IsNullOrEmpty(HoraRecoleccion))
            {
                query = query.Where(a => a.HoraRecoleccion.ToLower().Contains(HoraRecoleccion));
            }
            if (!string.IsNullOrEmpty(EstatusHD))
            {
                query = query.Where(a => a.EstatusHD.ToLower().Contains(EstatusHD));
            }
            if (!string.IsNullOrEmpty(FecEntregaClient))
            {
                query = query.Where(a => a.FecEntregaClient.ToLower().Contains(FecEntregaClient));
            }
            if (!string.IsNullOrEmpty(HoraMaxComEntrClie))
            {
                query = query.Where(a => a.HoraMaxComEntrClie.ToLower().Contains(HoraMaxComEntrClie));
            }
            if (!string.IsNullOrEmpty(FecComEnt))
            {
                query = query.Where(a => a.FecComEnt.ToLower().Contains(FecComEnt));
            }
            if (!string.IsNullOrEmpty(HoraComEnt))
            {
                query = query.Where(a => a.HoraComEnt.ToLower().Contains(HoraComEnt));
            }
            if (!string.IsNullOrEmpty(DiasTransRecEntrega))
            {
                query = query.Where(a => a.DiasTransRecEntrega.ToLower().Contains(DiasTransRecEntrega));
            }
            if (!string.IsNullOrEmpty(DiasCreacionEntrega))
            {
                query = query.Where(a => a.DiasCreacionEntrega.ToLower().Contains(DiasCreacionEntrega));
            }
            if (!string.IsNullOrEmpty(HorasComTrans))
            {
                query = query.Where(a => a.HorasComTrans.ToLower().Contains(HorasComTrans));
            }
            if (!string.IsNullOrEmpty(VariacionRecoleccion))
            {
                query = query.Where(a => a.VariacionRecoleccion.ToLower().Contains(VariacionRecoleccion));
            }
            if (!string.IsNullOrEmpty(VariacionCreacion))
            {
                query = query.Where(a => a.VariacionCreacion.ToLower().Contains(VariacionCreacion));
            }
            if (!string.IsNullOrEmpty(CumplimientEmbarque))
            {
                query = query.Where(a => a.CumplimientEmbarque.ToLower().Contains(CumplimientEmbarque));
            }
            if (!string.IsNullOrEmpty(CumplimientoCreacion))
            {
                query = query.Where(a => a.CumplimientoCreacion.ToLower().Contains(CumplimientoCreacion));
            }
            if (!string.IsNullOrEmpty(EnviosConsignacion))
            {
                query = query.Where(a => a.EnviosConsignacion.ToLower().Contains(EnviosConsignacion));
            }
            if (!string.IsNullOrEmpty(CostoTrans))
            {
                query = query.Where(a => a.CostoTrans.ToLower().Contains(CostoTrans));
            }
            if (!string.IsNullOrEmpty(CostoEnvClienteCot))
            {
                query = query.Where(a => a.CostoEnvClienteCot.ToLower().Contains(CostoEnvClienteCot));
            }
            if (!string.IsNullOrEmpty(CostoEnvClienteReal))
            {
                query = query.Where(a => a.CostoEnvClienteReal.ToLower().Contains(CostoEnvClienteReal));
            }
            if (!string.IsNullOrEmpty(TicketVta))
            {
                query = query.Where(a => a.TicketVta.ToLower().Contains(TicketVta));
            }
            if (!string.IsNullOrEmpty(ProdConsignacion))
            {
                query = query.Where(a => a.ProdConsignacion.ToLower().Contains(ProdConsignacion));
            }
            if (!string.IsNullOrEmpty(PzasConsignacion))
            {
                query = query.Where(a => a.PzasConsignacion.ToLower().Contains(PzasConsignacion));
            }

            #endregion



            recordsTotal = query.Count();

            lst = query.Take(recordsTotal).ToList();







            var d = new DataSet();

            string nombreArchivo = "ReporteSetce";

            //Excel to create an object file

            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            //Add a sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");


            //Here you can set a variety of styles seemingly font color backgrounds, but not very convenient, there is not set
            //Sheet1 head to add the title of the first row
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);


            row1.CreateCell(0).SetCellValue("Año del servicio");
            row1.CreateCell(1).SetCellValue("No. Mes del servicio");
            row1.CreateCell(2).SetCellValue("Mes del servicio");
            row1.CreateCell(3).SetCellValue("Año de la consignación");
            row1.CreateCell(4).SetCellValue("No. Mes de la consignacion");
            row1.CreateCell(5).SetCellValue("Mes de la consignacion");
            row1.CreateCell(6).SetCellValue("Fecha creación consignación");
            row1.CreateCell(7).SetCellValue("Hora creación consignación");
            row1.CreateCell(8).SetCellValue("Consignación");
            row1.CreateCell(9).SetCellValue("Numero de tienda");

            row1.CreateCell(10).SetCellValue("Nombre de tienda ");
            row1.CreateCell(11).SetCellValue("Formato");
            row1.CreateCell(12).SetCellValue("Dirección");
            row1.CreateCell(13).SetCellValue("Director");
            row1.CreateCell(14).SetCellValue("Region");
            row1.CreateCell(15).SetCellValue("Estado Origen");
            row1.CreateCell(16).SetCellValue("Estado Destino");
            row1.CreateCell(17).SetCellValue("Codigo postal Origen");
            row1.CreateCell(18).SetCellValue("Codigo postal Destino");
            row1.CreateCell(19).SetCellValue("Tipo de entrega (Recoge en tienda / Entrega a domicilio)");
            row1.CreateCell(20).SetCellValue("Fecha pago consignación");

            row1.CreateCell(21).SetCellValue("Hora pago consignación");
            row1.CreateCell(22).SetCellValue("Estatus de pago");
            row1.CreateCell(23).SetCellValue("Fecha  consignación de surtido");
            row1.CreateCell(24).SetCellValue("Hora de surtido consignación");
            row1.CreateCell(25).SetCellValue("Transportista asignado");
            row1.CreateCell(26).SetCellValue("Guia Transportista ");
            row1.CreateCell(27).SetCellValue("Guia interna Soriana");
            row1.CreateCell(28).SetCellValue("Fecha de recolección");
            row1.CreateCell(29).SetCellValue("Hora de recolección");
            row1.CreateCell(30).SetCellValue("Estatus Home Delivery");

            row1.CreateCell(31).SetCellValue("Fecha de entrega al cliente");
            row1.CreateCell(32).SetCellValue("Hora máxima de entrega al cliente");
            row1.CreateCell(33).SetCellValue("Fecha compromiso de entrega");
            row1.CreateCell(34).SetCellValue("Hora compromiso de entrega");
            row1.CreateCell(35).SetCellValue("Días transcurridos desde la recolección a la entrega");
            row1.CreateCell(36).SetCellValue("Días transcurridos desde la creación a la entrega");
            row1.CreateCell(37).SetCellValue("Horas compromiso Transportista");
            row1.CreateCell(38).SetCellValue("Variación Vs Recolección");
            row1.CreateCell(39).SetCellValue("Variación Vs Creación");
            row1.CreateCell(40).SetCellValue("Cumplimiento Vs Embarque (Dias transportista)");
            
            row1.CreateCell(41).SetCellValue("Cumplimiento Vs creación (Dias compromiso clientes)");
            row1.CreateCell(42).SetCellValue("Envios (Guías) por consignacion");
            row1.CreateCell(43).SetCellValue("Costo transportista");
            row1.CreateCell(44).SetCellValue("Costo de envío al cliente Cotizado");
            row1.CreateCell(45).SetCellValue("Costo de envío al cliente Real");
            row1.CreateCell(46).SetCellValue("Ticket $ Venta");
            row1.CreateCell(47).SetCellValue("Productos por consignación");
            row1.CreateCell(48).SetCellValue("Piezas por consignación ");






            //                                                
            //The data is written progressively sheet1 each row
           
            for (int i = 0; i < lst.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(lst[i].AnioServ.ToString());
                rowtemp.CreateCell(1).SetCellValue(lst[i].NoMesServ.ToString());
                rowtemp.CreateCell(2).SetCellValue(lst[i].NomMesSer.ToString());
                rowtemp.CreateCell(3).SetCellValue(lst[i].AnioConsig.ToString());
                rowtemp.CreateCell(4).SetCellValue(lst[i].NoMesConsig.ToString());
                rowtemp.CreateCell(5).SetCellValue(lst[i].NomMesConsig.ToString());
                rowtemp.CreateCell(6).SetCellValue(lst[i].FecCreacionConsig.ToString());
                rowtemp.CreateCell(7).SetCellValue(lst[i].HoraCreacionConsig.ToString());
                rowtemp.CreateCell(8).SetCellValue(lst[i].Consignacion.ToString());
                rowtemp.CreateCell(9).SetCellValue(lst[i].NoTda.ToString());
                rowtemp.CreateCell(10).SetCellValue(lst[i].NomTda.ToString());
                rowtemp.CreateCell(11).SetCellValue(lst[i].Formato.ToString());
                rowtemp.CreateCell(12).SetCellValue(lst[i].Direccion.ToString());
                rowtemp.CreateCell(13).SetCellValue(lst[i].Director.ToString());
                rowtemp.CreateCell(14).SetCellValue(lst[i].Region.ToString());
                rowtemp.CreateCell(15).SetCellValue(lst[i].EdoOrigen.ToString());
                rowtemp.CreateCell(16).SetCellValue(lst[i].EdoDestino.ToString());
                rowtemp.CreateCell(17).SetCellValue(lst[i].CpOrigen.ToString());
                rowtemp.CreateCell(18).SetCellValue(lst[i].CpDestino.ToString());
                rowtemp.CreateCell(19).SetCellValue(lst[i].TipoEntrega.ToString());
                rowtemp.CreateCell(20).SetCellValue(lst[i].FecPagoConsig.ToString());
                rowtemp.CreateCell(21).SetCellValue(lst[i].HoraPagoConsig.ToString());
                rowtemp.CreateCell(22).SetCellValue(lst[i].EstatusPAgo.ToString());
                rowtemp.CreateCell(23).SetCellValue(lst[i].FecConsigSurtido.ToString());
                rowtemp.CreateCell(24).SetCellValue(lst[i].HoraConsigSurtido.ToString());
                rowtemp.CreateCell(25).SetCellValue(lst[i].TransAsignado.ToString());
                rowtemp.CreateCell(26).SetCellValue(lst[i].GuiaTrans.ToString());
                rowtemp.CreateCell(27).SetCellValue(lst[i].GuiaSoriana.ToString());
                rowtemp.CreateCell(28).SetCellValue(lst[i].FecRecoleccion.ToString());
                rowtemp.CreateCell(29).SetCellValue(lst[i].HoraRecoleccion.ToString());
                rowtemp.CreateCell(30).SetCellValue(lst[i].EstatusHD.ToString());
                rowtemp.CreateCell(31).SetCellValue(lst[i].FecEntregaClient.ToString());
                rowtemp.CreateCell(32).SetCellValue(lst[i].HoraMaxComEntrClie.ToString());
                rowtemp.CreateCell(33).SetCellValue(lst[i].FecComEnt.ToString());
                rowtemp.CreateCell(34).SetCellValue(lst[i].HoraComEnt.ToString());
                rowtemp.CreateCell(35).SetCellValue(lst[i].DiasTransRecEntrega.ToString());
                rowtemp.CreateCell(36).SetCellValue(lst[i].DiasCreacionEntrega.ToString());
                rowtemp.CreateCell(37).SetCellValue(lst[i].HorasComTrans.ToString());
                rowtemp.CreateCell(38).SetCellValue(lst[i].VariacionRecoleccion.ToString());
                rowtemp.CreateCell(39).SetCellValue(lst[i].VariacionCreacion.ToString());
                rowtemp.CreateCell(40).SetCellValue(lst[i].CumplimientEmbarque.ToString());
                rowtemp.CreateCell(41).SetCellValue(lst[i].CumplimientoCreacion.ToString());
                rowtemp.CreateCell(42).SetCellValue(lst[i].EnviosConsignacion.ToString());
                rowtemp.CreateCell(43).SetCellValue(lst[i].CostoTrans.ToString());
                rowtemp.CreateCell(44).SetCellValue(lst[i].CostoEnvClienteCot.ToString());
                rowtemp.CreateCell(45).SetCellValue(lst[i].CostoEnvClienteReal.ToString());
                rowtemp.CreateCell(46).SetCellValue(lst[i].TicketVta.ToString());
                rowtemp.CreateCell(47).SetCellValue(lst[i].ProdConsignacion.ToString());
                rowtemp.CreateCell(48).SetCellValue(lst[i].PzasConsignacion.ToString());


            }



            //  Write to the client 

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            book.Write(ms);

            ms.Seek(0, SeekOrigin.Begin);

            DateTime dt = DateTime.Now;

            string dateTime = dt.ToString("yyyyMMddHHmmssfff");

            string fileName = nombreArchivo + "_" + dateTime + ".xls";

            return File(ms, "application/vnd.ms-excel", fileName);

        }
        #endregion

        #region TarifasTransportista
        public ActionResult TarifasTransportistas(string feIni,string feFin,string reporte)
        {

            if (string.IsNullOrEmpty(feIni) || string.IsNullOrEmpty(feFin))
            {

                ViewBag.FecIni = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
                ViewBag.FecFin = DateTime.Now.ToString("yyyy/MM/dd");
                ViewBag.reporte = "1";
            }
            else {
                ViewBag.FecIni = feIni;
                ViewBag.FecFin = feFin;
                ViewBag.reporte = reporte;
            }
            return View();
        }

        [HttpPost]
        public ActionResult GetTarifaTransportista(string fechaInicio, string fechaFin,string reporte)
        {
            List<TarifasTransportista> lst = new List<TarifasTransportista>();

            //logistica datatable
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault().ToLower();



            #region Se Obtienen Filtros Por Columna
            var Consignacion = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault().ToLower();
            var CodigoAlmacen = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault().ToLower();
            var NomAlmacen = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault().ToLower();
            var TipoAlmacen = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault().ToLower();
            var Zona = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault().ToLower();
            var TransAsignado = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault().ToLower();
            var TipoEnvio = Request.Form.GetValues("columns[6][search][value]").FirstOrDefault().ToLower();
            var GuiaTrans = Request.Form.GetValues("columns[7][search][value]").FirstOrDefault().ToLower();
            var GuiaSoriana = Request.Form.GetValues("columns[8][search][value]").FirstOrDefault().ToLower();
            var EstatusEntrega = Request.Form.GetValues("columns[9][search][value]").FirstOrDefault().ToLower();
            var FecRecoleccion = Request.Form.GetValues("columns[10][search][value]").FirstOrDefault().ToLower();
            var FecEntClient = Request.Form.GetValues("columns[11][search][value]").FirstOrDefault().ToLower();
            var PesoKGSis = Request.Form.GetValues("columns[12][search][value]").FirstOrDefault().ToLower();
            var PesoVolSis = Request.Form.GetValues("columns[13][search][value]").FirstOrDefault().ToLower();
            var PesoMayorSis = Request.Form.GetValues("columns[14][search][value]").FirstOrDefault().ToLower();
            var CostoTransCotizado = Request.Form.GetValues("columns[15][search][value]").FirstOrDefault().ToLower();
            var PesoKGGuias = Request.Form.GetValues("columns[16][search][value]").FirstOrDefault().ToLower();
            var PesoVolGuias = Request.Form.GetValues("columns[17][search][value]").FirstOrDefault().ToLower();
            var PesoMayorGuias = Request.Form.GetValues("columns[18][search][value]").FirstOrDefault().ToLower();
            var CostoTransGuias = Request.Form.GetValues("columns[19][search][value]").FirstOrDefault().ToLower();
            var PesoKGTrans = Request.Form.GetValues("columns[20][search][value]").FirstOrDefault().ToLower();
            var PesoVolTrans = Request.Form.GetValues("columns[21][search][value]").FirstOrDefault().ToLower();
            var PesoMayorTrans = Request.Form.GetValues("columns[22][search][value]").FirstOrDefault().ToLower();
            var CostoTransReal = Request.Form.GetValues("columns[23][search][value]").FirstOrDefault().ToLower();
            var Variacion1 = Request.Form.GetValues("columns[24][search][value]").FirstOrDefault().ToLower();
            var Variacion2 = Request.Form.GetValues("columns[25][search][value]").FirstOrDefault().ToLower();
          

            #endregion

            pageSize = length != null ? Convert.ToInt32(length) : 0;
            skip = start != null ? Convert.ToInt32(start) : 0;
            recordsTotal = 0;

           

            IQueryable<TarifasTransportista> query = from row in DALReportes.ReporteTarifas(reporte,fechaInicio,fechaFin).Tables[0].AsEnumerable().AsQueryable()
                                      select new TarifasTransportista()
                                      {
                                          Consignacion= row[0].ToString(),
                                          CodigoAlmacen = row[1].ToString(),
                                          NomAlmacen = row[2].ToString(),
                                          TipoAlmacen = row[3].ToString(),
                                          Zona = row[4].ToString(),
                                          TransAsignado = row[5].ToString(),
                                          TipoEnvio = row[6].ToString(),
                                          GuiaTrans = row[7].ToString(),
                                          GuiaSoriana = row[8].ToString(),
                                          EstatusEntrega = row[9].ToString(),
                                          FecRecoleccion = row[10].ToString(),
                                          FecEntClient = row[11].ToString(),
                                          PesoKGSis = row[12].ToString(),
                                          PesoVolSis = row[13].ToString(),
                                          PesoMayorSis = row[14].ToString(),
                                          CostoTransCotizado = row[15].ToString(),
                                          PesoKGGuias = row[16].ToString(),
                                          PesoVolGuias = row[17].ToString(),
                                          PesoMayorGuias = row[18].ToString(),
                                          CostoTransGuias = row[19].ToString(),
                                          PesoKGTrans = row[20].ToString(),
                                          PesoVolTrans = row[21].ToString(),
                                          PesoMayorTrans = row[21].ToString(),
                                          CostoTransReal = row[23].ToString(),
                                          Variacion1 = row[24].ToString(),
                                          Variacion2 = row[25].ToString(),


                                      };




            if (searchValue != "")
                query = query.Where(d => d.Consignacion.ToLower().Contains(searchValue)
                || d.CodigoAlmacen.ToLower().Contains(searchValue)
                || d.NomAlmacen.ToString().ToLower().Contains(searchValue)
                || d.TipoAlmacen.ToString().ToLower().Contains(searchValue)
                || d.Zona.ToString().ToLower().Contains(searchValue)
                || d.TransAsignado.ToString().ToLower().Contains(searchValue)
                || d.TipoEnvio.ToString().ToLower().Contains(searchValue)
                || d.GuiaTrans.ToString().ToLower().Contains(searchValue)
                || d.GuiaSoriana.ToString().ToLower().Contains(searchValue)
                || d.EstatusEntrega.ToString().ToLower().Contains(searchValue)
                || d.FecRecoleccion.ToString().ToLower().Contains(searchValue)
                || d.FecEntClient.ToString().ToLower().Contains(searchValue)
                || d.PesoKGSis.ToString().ToLower().Contains(searchValue)
                || d.PesoVolSis.ToString().ToLower().Contains(searchValue)
                || d.PesoMayorSis.ToString().ToLower().Contains(searchValue)
                || d.CostoTransCotizado.ToString().ToLower().Contains(searchValue)
                || d.PesoKGGuias.ToString().ToLower().Contains(searchValue)
                || d.PesoVolGuias.ToString().ToLower().Contains(searchValue)
                || d.PesoMayorGuias.ToString().ToLower().Contains(searchValue)
                || d.CostoTransGuias.ToString().ToLower().Contains(searchValue)
                || d.PesoKGTrans.ToString().ToLower().Contains(searchValue)
                || d.PesoVolTrans.ToString().ToLower().Contains(searchValue)
                || d.PesoMayorTrans.ToString().ToLower().Contains(searchValue)
                || d.CostoTransReal.ToString().ToLower().Contains(searchValue)
                || d.Variacion1.ToString().ToLower().Contains(searchValue)
                || d.Variacion2.ToString().ToLower().Contains(searchValue)
                

                );





            //Filter By Columns
            #region Filter By Columns
            if (!string.IsNullOrEmpty(Consignacion))
            {
                query = query.Where(a => a.Consignacion.ToLower().Contains(Consignacion));
            }

            if (!string.IsNullOrEmpty(CodigoAlmacen))
            {
                query = query.Where(a => a.CodigoAlmacen.ToLower().Contains(CodigoAlmacen));
            }
            if (!string.IsNullOrEmpty(NomAlmacen))
            {
                query = query.Where(a => a.NomAlmacen.ToLower().Contains(NomAlmacen));
            }
            if (!string.IsNullOrEmpty(TipoAlmacen))
            {
                query = query.Where(a => a.TipoAlmacen.ToLower().Contains(TipoAlmacen));
            }
            if (!string.IsNullOrEmpty(Zona))
            {
                query = query.Where(a => a.Zona.ToLower().Contains(Zona));
            }
            if (!string.IsNullOrEmpty(TransAsignado))
            {
                query = query.Where(a => a.TransAsignado.ToLower().Contains(TransAsignado));
            }
            if (!string.IsNullOrEmpty(TipoEnvio))
            {
                query = query.Where(a => a.TipoEnvio.ToLower().Contains(TipoEnvio));
            }
            if (!string.IsNullOrEmpty(GuiaTrans))
            {
                query = query.Where(a => a.GuiaTrans.ToLower().Contains(GuiaTrans));
            }
            if (!string.IsNullOrEmpty(GuiaSoriana))
            {
                query = query.Where(a => a.GuiaSoriana.ToLower().Contains(GuiaSoriana));
            }
            if (!string.IsNullOrEmpty(EstatusEntrega))
            {
                query = query.Where(a => a.EstatusEntrega.ToLower().Contains(EstatusEntrega));
            }
            if (!string.IsNullOrEmpty(FecRecoleccion))
            {
                query = query.Where(a => a.FecRecoleccion.ToLower().Contains(FecRecoleccion));
            }
            if (!string.IsNullOrEmpty(FecEntClient))
            {
                query = query.Where(a => a.FecEntClient.ToLower().Contains(FecEntClient));
            }
            if (!string.IsNullOrEmpty(PesoKGSis))
            {
                query = query.Where(a => a.PesoKGSis.ToLower().Contains(PesoKGSis));
            }
            if (!string.IsNullOrEmpty(PesoVolSis))
            {
                query = query.Where(a => a.PesoVolSis.ToLower().Contains(PesoVolSis));
            }
            if (!string.IsNullOrEmpty(PesoMayorSis))
            {
                query = query.Where(a => a.PesoMayorSis.ToLower().Contains(PesoMayorSis));
            }
            if (!string.IsNullOrEmpty(CostoTransCotizado))
            {
                query = query.Where(a => a.CostoTransCotizado.ToLower().Contains(CostoTransCotizado));
            }
            if (!string.IsNullOrEmpty(PesoKGGuias))
            {
                query = query.Where(a => a.PesoKGGuias.ToLower().Contains(PesoKGGuias));
            }
            if (!string.IsNullOrEmpty(PesoVolGuias))
            {
                query = query.Where(a => a.PesoVolGuias.ToLower().Contains(PesoVolGuias));
            }
            if (!string.IsNullOrEmpty(PesoMayorGuias))
            {
                query = query.Where(a => a.PesoMayorGuias.ToLower().Contains(PesoMayorGuias));
            }
            if (!string.IsNullOrEmpty(CostoTransGuias))
            {
                query = query.Where(a => a.CostoTransGuias.ToLower().Contains(CostoTransGuias));
            }
            if (!string.IsNullOrEmpty(PesoKGTrans))
            {
                query = query.Where(a => a.PesoKGTrans.ToLower().Contains(PesoKGTrans));
            }
            if (!string.IsNullOrEmpty(PesoVolTrans))
            {
                query = query.Where(a => a.PesoVolTrans.ToLower().Contains(PesoVolTrans));
            }
            if (!string.IsNullOrEmpty(PesoMayorTrans))
            {
                query = query.Where(a => a.PesoMayorTrans.ToLower().Contains(PesoMayorTrans));
            }
            if (!string.IsNullOrEmpty(CostoTransReal))
            {
                query = query.Where(a => a.CostoTransReal.ToLower().Contains(CostoTransReal));
            }
            if (!string.IsNullOrEmpty(Variacion1))
            {
                query = query.Where(a => a.Variacion1.ToLower().Contains(Variacion1));
            }
            if (!string.IsNullOrEmpty(Variacion2))
            {
                query = query.Where(a => a.Variacion2.ToLower().Contains(Variacion2));
            }
          
            #endregion

            //Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    query = query.OrderBy(sortColumn + " " + sortColumnDir);

            //}

            recordsTotal = query.Count();

            lst = query.Skip(skip).Take(pageSize).ToList();


            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = lst });
        }


        public FileResult ExcelTarifaTransportista(string Consignacion
, string CodigoAlmacen, string NomAlmacen, string TipoAlmacen, string Zona
, string TransAsignado, string TipoEnvio
, string GuiaTrans, string GuiaSoriana, string EstatusEntrega
, string FecRecoleccion, string FecEntClient, string PesoKGSis
, string PesoVolSis, string PesoMayorSis, string CostoTransCotizado
, string PesoKGGuias, string PesoVolGuias, string PesoMayorGuias
, string CostoTransGuias, string PesoKGTrans, string PesoVolTrans
, string PesoMayorTrans, string CostoTransReal, string Variacion1
, string Variacion2
, string fechaInicio, string fechaFin
            ,string reporte
    , string searchValue)

        {
            List<TarifasTransportista> lst = new List<TarifasTransportista>();

            // IQueryable<TarifasTransportista> query = lst.AsQueryable();


            IQueryable<TarifasTransportista> query = from row in DALReportes.ReporteTarifas(reporte,fechaInicio, fechaFin).Tables[0].AsEnumerable().AsQueryable()
                                                     select new TarifasTransportista()
                                                     {
                                                         Consignacion = row[0].ToString(),
                                                         CodigoAlmacen = row[1].ToString(),
                                                         NomAlmacen = row[2].ToString(),
                                                         TipoAlmacen = row[3].ToString(),
                                                         Zona = row[4].ToString(),
                                                         TransAsignado = row[5].ToString(),
                                                         TipoEnvio = row[6].ToString(),
                                                         GuiaTrans = row[7].ToString(),
                                                         GuiaSoriana = row[8].ToString(),
                                                         EstatusEntrega = row[9].ToString(),
                                                         FecRecoleccion = row[10].ToString(),
                                                         FecEntClient = row[11].ToString(),
                                                         PesoKGSis = row[12].ToString(),
                                                         PesoVolSis = row[13].ToString(),
                                                         PesoMayorSis = row[14].ToString(),
                                                         CostoTransCotizado = row[15].ToString(),
                                                         PesoKGGuias = row[16].ToString(),
                                                         PesoVolGuias = row[17].ToString(),
                                                         PesoMayorGuias = row[18].ToString(),
                                                         CostoTransGuias = row[19].ToString(),
                                                         PesoKGTrans = row[20].ToString(),
                                                         PesoVolTrans = row[21].ToString(),
                                                         PesoMayorTrans = row[21].ToString(),
                                                         CostoTransReal = row[23].ToString(),
                                                         Variacion1 = row[24].ToString(),
                                                         Variacion2 = row[25].ToString(),


                                                     };




            #region MyRegion

            if (searchValue != "")
                query = query.Where(z => z.Consignacion.ToLower().Contains(searchValue)
                || z.CodigoAlmacen.ToLower().Contains(searchValue)
                || z.NomAlmacen.ToString().ToLower().Contains(searchValue)
                || z.TipoAlmacen.ToString().ToLower().Contains(searchValue)
                || z.Zona.ToString().ToLower().Contains(searchValue)
                || z.TransAsignado.ToString().ToLower().Contains(searchValue)
                || z.TipoEnvio.ToString().ToLower().Contains(searchValue)
                || z.GuiaTrans.ToString().ToLower().Contains(searchValue)
                || z.GuiaSoriana.ToString().ToLower().Contains(searchValue)
                || z.EstatusEntrega.ToString().ToLower().Contains(searchValue)
                || z.FecRecoleccion.ToString().ToLower().Contains(searchValue)
                || z.FecEntClient.ToString().ToLower().Contains(searchValue)
                || z.PesoKGSis.ToString().ToLower().Contains(searchValue)
                || z.PesoVolSis.ToString().ToLower().Contains(searchValue)
                || z.PesoMayorSis.ToString().ToLower().Contains(searchValue)
                || z.CostoTransCotizado.ToString().ToLower().Contains(searchValue)
                || z.PesoKGGuias.ToString().ToLower().Contains(searchValue)
                || z.PesoVolGuias.ToString().ToLower().Contains(searchValue)
                || z.PesoMayorGuias.ToString().ToLower().Contains(searchValue)
                || z.CostoTransGuias.ToString().ToLower().Contains(searchValue)
                || z.PesoKGTrans.ToString().ToLower().Contains(searchValue)
                || z.PesoVolTrans.ToString().ToLower().Contains(searchValue)
                || z.PesoMayorTrans.ToString().ToLower().Contains(searchValue)
                || z.CostoTransReal.ToString().ToLower().Contains(searchValue)
                || z.Variacion1.ToString().ToLower().Contains(searchValue)
                || z.Variacion2.ToString().ToLower().Contains(searchValue)


                );
            #endregion


            #region Filter By Columns
            if (!string.IsNullOrEmpty(Consignacion))
            {
                query = query.Where(a => a.Consignacion.ToLower().Contains(Consignacion));
            }

            if (!string.IsNullOrEmpty(CodigoAlmacen))
            {
                query = query.Where(a => a.CodigoAlmacen.ToLower().Contains(CodigoAlmacen));
            }
            if (!string.IsNullOrEmpty(NomAlmacen))
            {
                query = query.Where(a => a.NomAlmacen.ToLower().Contains(NomAlmacen));
            }
            if (!string.IsNullOrEmpty(TipoAlmacen))
            {
                query = query.Where(a => a.TipoAlmacen.ToLower().Contains(TipoAlmacen));
            }
            if (!string.IsNullOrEmpty(Zona))
            {
                query = query.Where(a => a.Zona.ToLower().Contains(Zona));
            }
            if (!string.IsNullOrEmpty(TransAsignado))
            {
                query = query.Where(a => a.TransAsignado.ToLower().Contains(TransAsignado));
            }
            if (!string.IsNullOrEmpty(TipoEnvio))
            {
                query = query.Where(a => a.TipoEnvio.ToLower().Contains(TipoEnvio));
            }
            if (!string.IsNullOrEmpty(GuiaTrans))
            {
                query = query.Where(a => a.GuiaTrans.ToLower().Contains(GuiaTrans));
            }
            if (!string.IsNullOrEmpty(GuiaSoriana))
            {
                query = query.Where(a => a.GuiaSoriana.ToLower().Contains(GuiaSoriana));
            }
            if (!string.IsNullOrEmpty(EstatusEntrega))
            {
                query = query.Where(a => a.EstatusEntrega.ToLower().Contains(EstatusEntrega));
            }
            if (!string.IsNullOrEmpty(FecRecoleccion))
            {
                query = query.Where(a => a.FecRecoleccion.ToLower().Contains(FecRecoleccion));
            }
            if (!string.IsNullOrEmpty(FecEntClient))
            {
                query = query.Where(a => a.FecEntClient.ToLower().Contains(FecEntClient));
            }
            if (!string.IsNullOrEmpty(PesoKGSis))
            {
                query = query.Where(a => a.PesoKGSis.ToLower().Contains(PesoKGSis));
            }
            if (!string.IsNullOrEmpty(PesoVolSis))
            {
                query = query.Where(a => a.PesoVolSis.ToLower().Contains(PesoVolSis));
            }
            if (!string.IsNullOrEmpty(PesoMayorSis))
            {
                query = query.Where(a => a.PesoMayorSis.ToLower().Contains(PesoMayorSis));
            }
            if (!string.IsNullOrEmpty(CostoTransCotizado))
            {
                query = query.Where(a => a.CostoTransCotizado.ToLower().Contains(CostoTransCotizado));
            }
            if (!string.IsNullOrEmpty(PesoKGGuias))
            {
                query = query.Where(a => a.PesoKGGuias.ToLower().Contains(PesoKGGuias));
            }
            if (!string.IsNullOrEmpty(PesoVolGuias))
            {
                query = query.Where(a => a.PesoVolGuias.ToLower().Contains(PesoVolGuias));
            }
            if (!string.IsNullOrEmpty(PesoMayorGuias))
            {
                query = query.Where(a => a.PesoMayorGuias.ToLower().Contains(PesoMayorGuias));
            }
            if (!string.IsNullOrEmpty(CostoTransGuias))
            {
                query = query.Where(a => a.CostoTransGuias.ToLower().Contains(CostoTransGuias));
            }
            if (!string.IsNullOrEmpty(PesoKGTrans))
            {
                query = query.Where(a => a.PesoKGTrans.ToLower().Contains(PesoKGTrans));
            }
            if (!string.IsNullOrEmpty(PesoVolTrans))
            {
                query = query.Where(a => a.PesoVolTrans.ToLower().Contains(PesoVolTrans));
            }
            if (!string.IsNullOrEmpty(PesoMayorTrans))
            {
                query = query.Where(a => a.PesoMayorTrans.ToLower().Contains(PesoMayorTrans));
            }
            if (!string.IsNullOrEmpty(CostoTransReal))
            {
                query = query.Where(a => a.CostoTransReal.ToLower().Contains(CostoTransReal));
            }
            if (!string.IsNullOrEmpty(Variacion1))
            {
                query = query.Where(a => a.Variacion1.ToLower().Contains(Variacion1));
            }
            if (!string.IsNullOrEmpty(Variacion2))
            {
                query = query.Where(a => a.Variacion2.ToLower().Contains(Variacion2));
            }

            #endregion



            recordsTotal = query.Count();

            lst = query.Take(recordsTotal).ToList();


            var d = new DataSet();

            string nombreArchivo = "ReporteTarifasTransportistasSETC";

            if (reporte.Equals("2")) { nombreArchivo = "ReporteTarifasTransportistasMG"; }

            //Excel to create an object file

            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            //Add a sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");


            //Here you can set a variety of styles seemingly font color backgrounds, but not very convenient, there is not set
            //Sheet1 head to add the title of the first row
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);


            row1.CreateCell(0).SetCellValue("Consignación");
            row1.CreateCell(1).SetCellValue("Codigo Almacen");
            row1.CreateCell(2).SetCellValue("Nombre Almacen");
            row1.CreateCell(3).SetCellValue("Tipo de Almacen");
            row1.CreateCell(4).SetCellValue("Zona");
            row1.CreateCell(5).SetCellValue("Transportista asignado");
            row1.CreateCell(6).SetCellValue("Tipo de envio (Guia paq / guia LTL)");
            row1.CreateCell(7).SetCellValue("Guia Transportista");
            row1.CreateCell(8).SetCellValue("Guia interna Soriana");
            row1.CreateCell(9).SetCellValue("Estatus Entregas");
            row1.CreateCell(10).SetCellValue("Fecha de recolección");
            row1.CreateCell(11).SetCellValue("Fecha de entrega al cliente");
            row1.CreateCell(12).SetCellValue("Peso KG (Sistema)");
            row1.CreateCell(13).SetCellValue("Peso Vol (Sistema)");
            row1.CreateCell(14).SetCellValue("Peso Mayor (Sistema)");
            row1.CreateCell(15).SetCellValue("Costo transportista cotizado");
            row1.CreateCell(16).SetCellValue("Peso KG (Solicitud guías)");
            row1.CreateCell(17).SetCellValue("Peso Vol (Solicitud guías)");
            row1.CreateCell(18).SetCellValue("Peso Mayor (Solicitud guías)");
            row1.CreateCell(19).SetCellValue("Costo transportista (Solicitud guías)");
            row1.CreateCell(20).SetCellValue("Peso KG (Transporte)");
            row1.CreateCell(21).SetCellValue("Peso Vol (Transporte)");
            row1.CreateCell(22).SetCellValue("Peso Mayor (Transporte)");
            row1.CreateCell(23).SetCellValue("Costo transportista real");
            row1.CreateCell(24).SetCellValue("Variación1");
            row1.CreateCell(25).SetCellValue("Variación2");

           





            //                                                
            //The data is written progressively sheet1 each row

            for (int i = 0; i < lst.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(lst[i].Consignacion.ToString());
                rowtemp.CreateCell(1).SetCellValue(lst[i].CodigoAlmacen.ToString());
                rowtemp.CreateCell(2).SetCellValue(lst[i].NomAlmacen.ToString());
                rowtemp.CreateCell(3).SetCellValue(lst[i].TipoAlmacen.ToString());
                rowtemp.CreateCell(4).SetCellValue(lst[i].Zona.ToString());
                rowtemp.CreateCell(5).SetCellValue(lst[i].TransAsignado.ToString());
                rowtemp.CreateCell(6).SetCellValue(lst[i].TipoEnvio.ToString());
                rowtemp.CreateCell(7).SetCellValue(lst[i].GuiaTrans.ToString());
                rowtemp.CreateCell(8).SetCellValue(lst[i].GuiaSoriana.ToString());
                rowtemp.CreateCell(9).SetCellValue(lst[i].EstatusEntrega.ToString());
                rowtemp.CreateCell(10).SetCellValue(lst[i].FecRecoleccion.ToString());
                rowtemp.CreateCell(11).SetCellValue(lst[i].FecEntClient.ToString());
                rowtemp.CreateCell(12).SetCellValue(lst[i].PesoKGSis.ToString());
                rowtemp.CreateCell(13).SetCellValue(lst[i].PesoVolSis.ToString());
                rowtemp.CreateCell(14).SetCellValue(lst[i].PesoMayorSis.ToString());
                rowtemp.CreateCell(15).SetCellValue(lst[i].CostoTransCotizado.ToString());
                rowtemp.CreateCell(16).SetCellValue(lst[i].PesoKGGuias.ToString());
                rowtemp.CreateCell(17).SetCellValue(lst[i].PesoVolGuias.ToString());
                rowtemp.CreateCell(18).SetCellValue(lst[i].PesoMayorGuias.ToString());
                rowtemp.CreateCell(19).SetCellValue(lst[i].CostoTransGuias.ToString());
                rowtemp.CreateCell(20).SetCellValue(lst[i].PesoKGTrans.ToString());
                rowtemp.CreateCell(21).SetCellValue(lst[i].PesoVolTrans.ToString());
                rowtemp.CreateCell(22).SetCellValue(lst[i].PesoMayorTrans.ToString());
                rowtemp.CreateCell(23).SetCellValue(lst[i].CostoTransReal.ToString());
                rowtemp.CreateCell(24).SetCellValue(lst[i].Variacion1.ToString());
                rowtemp.CreateCell(25).SetCellValue(lst[i].Variacion2.ToString());
             


            }



            //  Write to the client 

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            book.Write(ms);

            ms.Seek(0, SeekOrigin.Begin);

            DateTime dt = DateTime.Now;

            string dateTime = dt.ToString("yyyyMMddHHmmssfff");

            string fileName = nombreArchivo + "_" + dateTime + ".xls";

            return File(ms, "application/vnd.ms-excel", fileName);

        }
        #endregion
    }
}