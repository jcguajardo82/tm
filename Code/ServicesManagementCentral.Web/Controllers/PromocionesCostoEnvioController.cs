using System.Linq;
using System.Linq.Dynamic;
using ServicesManagement.Web.DAL;
using ServicesManagement.Web.Helpers;
using ServicesManagement.Web.Models.PromocionesCostoEnvio;
using ServicesManagement.Web.Models.TipoLogistica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using System.IO;

namespace ServicesManagement.Web.Controllers
{
    [Authorize]
    public class PromocionesCostoEnvioController : Controller
    {
        // GET: PromocionesCostoEnvio
        public string draw = "";
        public string start = "";
        public string length = "";
        public string sortColumn = "";
        public string sortColumnDir = "";
        public string searchValue = "";
        public int pageSize, skip, recordsTotal;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetPromociones()
        {
            try
            {

                //var listC = DataTableToModel.ConvertTo<PromocionesCostoEnvio>(DALPromocionesCostoEnvio.PromocionesCostoEnvio_sUp().Tables[0]);

                //var result = new { Success = true, resp = listC };
                //return Json(result, JsonRequestBehavior.AllowGet);

                List<PromocionesCostoEnvio> lst = new List<PromocionesCostoEnvio>();

                //logistica datatable
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault().ToLower();

                #region Se Obtienen Filtros Por Columna
                var Desc_TipoEnvio = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault().ToLower();
                var Desc_TipoServicio = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault().ToLower();
                var TipoCatalogo = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault().ToLower();
                var Desc_TipoFormato = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault().ToLower();
                var PostalCodeOrig = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault().ToLower();
                var PostalCodeDestino = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault().ToLower();
                var CiudadOrig = Request.Form.GetValues("columns[6][search][value]").FirstOrDefault().ToLower();
                var CiudadDest = Request.Form.GetValues("columns[7][search][value]").FirstOrDefault().ToLower();
                var EdoOrig = Request.Form.GetValues("columns[8][search][value]").FirstOrDefault().ToLower();
                var EdoDest = Request.Form.GetValues("columns[9][search][value]").FirstOrDefault().ToLower();
                var SupplierName = Request.Form.GetValues("columns[10][search][value]").FirstOrDefault().ToLower();
                var NombreTransportista = Request.Form.GetValues("columns[11][search][value]").FirstOrDefault().ToLower();
                var Cve_CategSAP = Request.Form.GetValues("columns[12][search][value]").FirstOrDefault().ToLower();
                var Desc_CategSAP = Request.Form.GetValues("columns[13][search][value]").FirstOrDefault().ToLower();
                var Cve_GciaCategSAP = Request.Form.GetValues("columns[14][search][value]").FirstOrDefault().ToLower();
                var Desc_GciaCategSAP = Request.Form.GetValues("columns[15][search][value]").FirstOrDefault().ToLower();
                var Material_MATNR = Request.Form.GetValues("columns[16][search][value]").FirstOrDefault().ToLower();
                var Id_Num_CodBarra = Request.Form.GetValues("columns[17][search][value]").FirstOrDefault().ToLower();
                var Nombre_SKU = Request.Form.GetValues("columns[18][search][value]").FirstOrDefault().ToLower();
                var PesoMinimo = Request.Form.GetValues("columns[19][search][value]").FirstOrDefault().ToLower();
                var PesoMaximo = Request.Form.GetValues("columns[20][search][value]").FirstOrDefault().ToLower();
                var MesesSinIntereses = Request.Form.GetValues("columns[21][search][value]").FirstOrDefault().ToLower();

                var ComprasMayor = Request.Form.GetValues("columns[22][search][value]").FirstOrDefault().ToLower();
                var ComprasMenor = Request.Form.GetValues("columns[23][search][value]").FirstOrDefault().ToLower();
                var FechaInicioPromo = Request.Form.GetValues("columns[24][search][value]").FirstOrDefault().ToLower();
                var HoraInicioPromo = Request.Form.GetValues("columns[25][search][value]").FirstOrDefault().ToLower();
                var FechaFinPromo = Request.Form.GetValues("columns[26][search][value]").FirstOrDefault().ToLower();
                var HoraFinPromo = Request.Form.GetValues("columns[27][search][value]").FirstOrDefault().ToLower();
                var CostoEspecial = Request.Form.GetValues("columns[28][search][value]").FirstOrDefault().ToLower();
                var TarifaDesc = Request.Form.GetValues("columns[29][search][value]").FirstOrDefault().ToLower();
                var UsuarioCreacion = Request.Form.GetValues("columns[30][search][value]").FirstOrDefault().ToLower();
                var FechaUltModif = Request.Form.GetValues("columns[31][search][value]").FirstOrDefault().ToLower();
                var HoraUltModif = Request.Form.GetValues("columns[32][search][value]").FirstOrDefault().ToLower();
                var BitActivo = Request.Form.GetValues("columns[33][search][value]").FirstOrDefault().ToLower();

                #endregion


                pageSize = length != null ? Convert.ToInt32(length) : 0;
                skip = start != null ? Convert.ToInt32(start) : 0;
                recordsTotal = 0;

                IQueryable<PromocionesCostoEnvio> query = from row in DALPromocionesCostoEnvio.PromocionesCostoEnvio_sUp().Tables[0].AsEnumerable().AsQueryable()
                                                          select new PromocionesCostoEnvio()
                                                          {
                                                              Desc_TipoEnvio = row["Desc_TipoEnvio"].ToString(),
                                                              Desc_TipoServicio = row["Desc_TipoServicio"].ToString(),
                                                              TipoCatalogo = row["TipoCatalogo"].ToString(),
                                                              Desc_TipoFormato = row["Desc_TipoFormato"].ToString(),
                                                              PostalCodeOrig = row["PostalCodeOrig"].ToString(),
                                                              PostalCodeDestino = row["PostalCodeDestino"].ToString(),
                                                              CiudadOrig = row["CiudadOrig"].ToString(),
                                                              CiudadDest = row["CiudadDest"].ToString(),
                                                              EdoOrig = row["EdoOrig"].ToString(),
                                                              EdoDest = row["EdoDest"].ToString(),
                                                              SupplierName = row["SupplierName"].ToString(),
                                                              NombreTransportista = row["NombreTransportista"].ToString(),

                                                              Cve_CategSAP = row["Cve_CategSAP"].ToString(),
                                                              Desc_CategSAP = row["Desc_CategSAP"].ToString(),
                                                              Cve_GciaCategSAP = row["Cve_GciaCategSAP"].ToString(),
                                                              Desc_GciaCategSAP = row["Desc_GciaCategSAP"].ToString(),
                                                              Material_MATNR = row["Material_MATNR"].ToString(),
                                                              Id_Num_CodBarra = row["Id_Num_CodBarra"].ToString(),
                                                              Nombre_SKU = row["Nombre_SKU"].ToString(),
                                                              PesoMinimo = row["PesoMinimo"].ToString(),
                                                              PesoMaximo = row["PesoMaximo"].ToString(),
                                                              MesesSinIntereses = row["MesesSinIntereses"].ToString(),
                                                              ComprasMayor = row["ComprasMayor"].ToString(),
                                                              ComprasMenor = row["ComprasMenor"].ToString(),
                                                              FechaInicioPromo = row["FechaInicioPromo"].ToString(),
                                                              HoraInicioPromo = row["HoraInicioPromo"].ToString(),
                                                              FechaFinPromo = row["FechaFinPromo"].ToString(),
                                                              HoraFinPromo = row["HoraFinPromo"].ToString(),
                                                              CostoEspecial = string.IsNullOrEmpty(row["CostoEspecial"].ToString()) ? 0 : decimal.Parse(row["CostoEspecial"].ToString()),
                                                              TarifaDesc = string.IsNullOrEmpty(row["TarifaDesc"].ToString()) ? 0 : decimal.Parse(row["TarifaDesc"].ToString()),
                                                              UsuarioCreacion = row["UsuarioCreacion"].ToString(),
                                                              FechaUltModif = row["FechaUltModif"].ToString(),
                                                              HoraUltModif = row["HoraUltModif"].ToString(),
                                                              BitActivo = row["BitActivo"].ToString(),
                                                              cnscPromo = int.Parse(row["cnscPromo"].ToString()),
                                                              Orden = int.Parse(row["Orden"].ToString())


                                                          };




                if (searchValue != "")
                    query = query.Where(d => d.Desc_TipoEnvio.ToLower().Contains(searchValue)
                    || d.Desc_TipoServicio.ToLower().Contains(searchValue)
                    || d.TipoCatalogo.ToString().ToLower().Contains(searchValue)
                    || d.Desc_TipoFormato.ToLower().Contains(searchValue)
                    || d.PostalCodeOrig.ToLower().Contains(searchValue)
                    || d.PostalCodeDestino.ToLower().Contains(searchValue)
                    || d.CiudadOrig.ToLower().Contains(searchValue)
                    || d.CiudadDest.ToLower().Contains(searchValue)
                    || d.EdoOrig.ToLower().Contains(searchValue)
                    || d.EdoDest.ToLower().Contains(searchValue)
                    || d.SupplierName.ToLower().Contains(searchValue)
                    || d.NombreTransportista.ToLower().Contains(searchValue)
                    || d.Cve_CategSAP.ToLower().Contains(searchValue)
                    || d.Desc_CategSAP.ToLower().Contains(searchValue)
                    || d.Cve_GciaCategSAP.ToLower().Contains(searchValue)
                    || d.Desc_GciaCategSAP.ToLower().Contains(searchValue)
                    || d.Material_MATNR.ToLower().Contains(searchValue)
                    || d.Id_Num_CodBarra.ToLower().Contains(searchValue)
                    || d.Nombre_SKU.ToLower().Contains(searchValue)
                    || d.PesoMinimo.ToLower().Contains(searchValue)
                    || d.PesoMaximo.ToLower().Contains(searchValue)
                    || d.MesesSinIntereses.ToLower().Contains(searchValue)
                    || d.ComprasMayor.ToLower().Contains(searchValue)
                    || d.ComprasMenor.ToLower().Contains(searchValue)
                    || d.FechaInicioPromo.ToLower().Contains(searchValue)
                    || d.HoraInicioPromo.ToLower().Contains(searchValue)

                    || d.FechaFinPromo.ToLower().Contains(searchValue)
                    || d.HoraFinPromo.ToLower().Contains(searchValue)
                    || d.CostoEspecial.ToString().ToLower().Contains(searchValue)
                    || d.TarifaDesc.ToString().ToLower().Contains(searchValue)
                    || d.UsuarioCreacion.ToLower().Contains(searchValue)
                    || d.FechaUltModif.ToLower().Contains(searchValue)
                    || d.HoraUltModif.ToLower().Contains(searchValue)
                    || d.BitActivo.ToLower().Contains(searchValue)
                    );


                #region Filter By Columns
                if (!string.IsNullOrEmpty(Desc_TipoEnvio))
                {
                    query = query.Where(a => a.Desc_TipoEnvio.ToLower().Contains(Desc_TipoEnvio));
                }
                if (!string.IsNullOrEmpty(Desc_TipoServicio))
                {
                    query = query.Where(a => a.Desc_TipoServicio.ToLower().Contains(Desc_TipoServicio));
                }

                if (!string.IsNullOrEmpty(TipoCatalogo))
                {
                    query = query.Where(a => a.TipoCatalogo.ToString().ToLower().Contains(TipoCatalogo));
                }
                if (!string.IsNullOrEmpty(Desc_TipoFormato))
                {
                    query = query.Where(a => a.Desc_TipoFormato.ToLower().Contains(Desc_TipoFormato));
                }

                if (!string.IsNullOrEmpty(PostalCodeOrig))
                {
                    query = query.Where(a => a.PostalCodeOrig.ToLower().Contains(PostalCodeOrig));
                }

                if (!string.IsNullOrEmpty(PostalCodeDestino))
                {
                    query = query.Where(a => a.PostalCodeDestino.ToLower().Contains(PostalCodeDestino));
                }



                if (!string.IsNullOrEmpty(CiudadOrig))
                {
                    query = query.Where(a => a.CiudadOrig.ToLower().Contains(CiudadOrig));
                }

                if (!string.IsNullOrEmpty(CiudadDest))
                {
                    query = query.Where(a => a.CiudadDest.ToLower().Contains(CiudadDest));
                }
                if (!string.IsNullOrEmpty(EdoOrig))
                {
                    query = query.Where(a => a.EdoOrig.ToLower().Contains(EdoOrig));
                }

                if (!string.IsNullOrEmpty(EdoDest))
                {
                    query = query.Where(a => a.EdoDest.ToLower().Contains(EdoDest));
                }


                //------------------
                if (!string.IsNullOrEmpty(SupplierName))
                {
                    query = query.Where(a => a.SupplierName.ToLower().Contains(SupplierName));
                }
                if (!string.IsNullOrEmpty(NombreTransportista))
                {
                    query = query.Where(a => a.NombreTransportista.ToLower().Contains(NombreTransportista));
                }
                if (!string.IsNullOrEmpty(Cve_CategSAP))
                {
                    query = query.Where(a => a.Cve_CategSAP.ToLower().Contains(Cve_CategSAP));
                }
                if (!string.IsNullOrEmpty(Desc_CategSAP))
                {
                    query = query.Where(a => a.Desc_CategSAP.ToLower().Contains(Desc_CategSAP));
                }
                if (!string.IsNullOrEmpty(Cve_GciaCategSAP))
                {
                    query = query.Where(a => a.Cve_GciaCategSAP.ToLower().Contains(Cve_GciaCategSAP));
                }
                if (!string.IsNullOrEmpty(Desc_GciaCategSAP))
                {
                    query = query.Where(a => a.Desc_GciaCategSAP.ToLower().Contains(Desc_GciaCategSAP));
                }
                if (!string.IsNullOrEmpty(Material_MATNR))
                {
                    query = query.Where(a => a.Material_MATNR.ToLower().Contains(Material_MATNR));
                }
                if (!string.IsNullOrEmpty(Id_Num_CodBarra))
                {
                    query = query.Where(a => a.Id_Num_CodBarra.ToLower().Contains(Id_Num_CodBarra));
                }
                if (!string.IsNullOrEmpty(Nombre_SKU))
                {
                    query = query.Where(a => a.Nombre_SKU.ToLower().Contains(Nombre_SKU));
                }
                if (!string.IsNullOrEmpty(PesoMinimo))
                {
                    query = query.Where(a => a.PesoMinimo.ToLower().Contains(PesoMinimo));
                }
                if (!string.IsNullOrEmpty(PesoMaximo))
                {
                    query = query.Where(a => a.PesoMaximo.ToLower().Contains(PesoMaximo));
                }
                if (!string.IsNullOrEmpty(MesesSinIntereses))
                {
                    query = query.Where(a => a.MesesSinIntereses.ToLower().Contains(MesesSinIntereses));
                }
                //--
                if (!string.IsNullOrEmpty(ComprasMayor))
                {
                    query = query.Where(a => a.ComprasMayor.ToLower().Contains(ComprasMayor));
                }
                if (!string.IsNullOrEmpty(ComprasMenor))
                {
                    query = query.Where(a => a.ComprasMenor.ToLower().Contains(ComprasMenor));
                }
                if (!string.IsNullOrEmpty(FechaInicioPromo))
                {
                    query = query.Where(a => a.FechaInicioPromo.ToLower().Contains(FechaInicioPromo));
                }
                if (!string.IsNullOrEmpty(HoraInicioPromo))
                {
                    query = query.Where(a => a.HoraInicioPromo.ToLower().Contains(HoraInicioPromo));
                }
                if (!string.IsNullOrEmpty(FechaFinPromo))
                {
                    query = query.Where(a => a.FechaFinPromo.ToLower().Contains(FechaFinPromo));
                }
                if (!string.IsNullOrEmpty(HoraFinPromo))
                {
                    query = query.Where(a => a.HoraFinPromo.ToLower().Contains(HoraFinPromo));
                }
                if (!string.IsNullOrEmpty(CostoEspecial))
                {
                    query = query.Where(a => a.CostoEspecial.ToString().ToLower().Contains(CostoEspecial));
                }
                if (!string.IsNullOrEmpty(TarifaDesc))
                {
                    query = query.Where(a => a.TarifaDesc.ToString().ToLower().Contains(TarifaDesc));
                }
                if (!string.IsNullOrEmpty(UsuarioCreacion))
                {
                    query = query.Where(a => a.UsuarioCreacion.ToLower().Contains(UsuarioCreacion));
                }
                if (!string.IsNullOrEmpty(FechaUltModif))
                {
                    query = query.Where(a => a.FechaUltModif.ToLower().Contains(FechaUltModif));
                }
                if (!string.IsNullOrEmpty(HoraUltModif))
                {
                    query = query.Where(a => a.HoraUltModif.ToLower().Contains(HoraUltModif));
                }
                if (!string.IsNullOrEmpty(BitActivo))
                {
                    string ac = "activo", ina = "inactivo";

                    if(ac.Contains(BitActivo))
                        query = query.Where(a => a.BitActivo.ToLower().Contains("1"));
                    if (ina.Contains(BitActivo))
                        query = query.Where(a => a.BitActivo.ToLower().Contains("0"));
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
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetPromocionesId(int Id)
        {
            try
            {

                var listC = DataTableToModel.ConvertTo<PromocionesCostoEnvio>(DALPromocionesCostoEnvio.PromocionesCostoEnvioById_sUp(Id).Tables[0]).FirstOrDefault();

                var result = new { Success = true, resp = listC };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult AddPromociones(int cnscPromo, int IdTipoCatalogo, int? IdOwner, int? IdFormatoTienda, string PostalCodeOrig, string PostalCodeDestino
            , string CiudadOrig, string CiudadDest, string EdoOrig, string EdoDest, int? IdSupplierWH, string SupplierName, int? IdTransportista, string NombreTransportista,
             int? IdTipoEnvio, int? IdTipoServicio, string Cve_CategSAP, string Desc_CategSAP, string Cve_GciaCategSAP, string Desc_GciaCategSAP
            , string Material_MATNR, long? Id_Num_CodBarra, string nombre_SKU, decimal PesoMinimo, decimal PesoMaximo, int? IdTipoLogistica, int MesesSinIntereses
            , decimal ComprasMayor, decimal ComprasMenor, string FechaInicioPromo, string HoraInicioPromo, string FechaFinPromo, string HoraFinPromo
            , decimal? CostoEspecial, decimal? TarifaDesc, int orden)
        {
            try
            {
                if (cnscPromo == 0)
                {

                    DALPromocionesCostoEnvio.PromocionesCostoEnvio_iUp(
                         IdTipoCatalogo, IdOwner, IdFormatoTienda, PostalCodeOrig, PostalCodeDestino
                , CiudadOrig, CiudadDest, EdoOrig, EdoDest, IdSupplierWH, SupplierName, IdTransportista, NombreTransportista
                , IdTipoEnvio, IdTipoServicio, Cve_CategSAP, Desc_CategSAP, Cve_GciaCategSAP, Desc_GciaCategSAP
                , Material_MATNR, Id_Num_CodBarra, nombre_SKU, PesoMinimo, PesoMaximo, IdTipoLogistica, MesesSinIntereses
                , ComprasMayor, ComprasMenor, Convert.ToDateTime(FechaInicioPromo), TimeSpan.Parse(HoraInicioPromo), Convert.ToDateTime(FechaFinPromo), TimeSpan.Parse(HoraFinPromo)
                , CostoEspecial, TarifaDesc, DateTime.Now, DateTime.Now.TimeOfDay, User.Identity.Name, true, orden
                        );
                }
                else
                {
                    DALPromocionesCostoEnvio.PromocionesCostoEnvio_uUp(cnscPromo,
                         IdTipoCatalogo, IdOwner, IdFormatoTienda, PostalCodeOrig, PostalCodeDestino
                , CiudadOrig, CiudadDest, EdoOrig, EdoDest, IdSupplierWH, SupplierName, IdTransportista, NombreTransportista
                , IdTipoEnvio, IdTipoServicio, Cve_CategSAP, Desc_CategSAP, Cve_GciaCategSAP, Desc_GciaCategSAP
                , Material_MATNR, Id_Num_CodBarra, nombre_SKU, PesoMinimo, PesoMaximo, IdTipoLogistica, MesesSinIntereses
                , ComprasMayor, ComprasMenor, Convert.ToDateTime(FechaInicioPromo), TimeSpan.Parse(HoraInicioPromo), Convert.ToDateTime(FechaFinPromo), TimeSpan.Parse(HoraFinPromo)
                , CostoEspecial, TarifaDesc, DateTime.Now, DateTime.Now.TimeOfDay, User.Identity.Name, true, orden
                        );
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

        [HttpGet]
        public ActionResult DelPromociones(int cnscPromo)
        {
            try
            {

                DALPromocionesCostoEnvio.PromocionesCostoEnvio_dUp(cnscPromo
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

        [HttpGet]
        public ActionResult GetArt(string Material_MATNR, decimal? Id_Num_CodBarra)
        {
            try
            {

                var dt = DALPromocionesCostoEnvio.up_CorpTMS_cmd_SKU(Material_MATNR, Id_Num_CodBarra);
                var descArt = string.Empty;

                if (dt.Tables[0].Rows.Count > 0)
                {
                    descArt = dt.Tables[0].Rows[0][0].ToString();
                }

                var result = new { Success = true, resp = descArt };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CboEstados()
        {
            try
            {

                var listC = DataTableToModel.ConvertTo<orderfacts_PostalCodes.Estados>(DALPromocionesCostoEnvio.upCorpTms_Cns_CpEstados().Tables[0]);

                var result = new { Success = true, resp = listC };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult CboCiudad(string Region1Code)
        {
            try
            {

                var listC = DataTableToModel.ConvertTo<orderfacts_PostalCodes.Ciudades>(DALPromocionesCostoEnvio.upCorpTms_Cns_CpCiudadesxEstado(Region1Code).Tables[0]);

                var result = new { Success = true, resp = listC };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public FileResult Excel(string Desc_TipoEnvio
            , string Desc_TipoServicio, string TipoCatalogo, string Desc_TipoFormato, string PostalCodeOrig
            , string PostalCodeDestino, string CiudadOrig,string CiudadDest,string EdoOrig
            , string EdoDest, string SupplierName, string NombreTransportista, string Cve_CategSAP
            , string Desc_CategSAP, string Cve_GciaCategSAP, string Desc_GciaCategSAP, string Material_MATNR
            , string Id_Num_CodBarra, string Nombre_SKU, string PesoMinimo, string PesoMaximo
            , string MesesSinIntereses, string ComprasMayor, string ComprasMenor, string FechaInicioPromo
            , string HoraInicioPromo, string FechaFinPromo, string HoraFinPromo, string CostoEspecial
            , string TarifaDesc, string UsuarioCreacion, string FechaUltModif, string HoraUltModif
            ,string BitActivo, string searchValue)

        {
            List<PromocionesCostoEnvio> lst = new List<PromocionesCostoEnvio>();
            IQueryable<PromocionesCostoEnvio> query = from row in DALPromocionesCostoEnvio.PromocionesCostoEnvio_sUp().Tables[0].AsEnumerable().AsQueryable()
                                                      select new PromocionesCostoEnvio()
                                                      {
                                                          Desc_TipoEnvio = row["Desc_TipoEnvio"].ToString(),
                                                          Desc_TipoServicio = row["Desc_TipoServicio"].ToString(),
                                                          TipoCatalogo = row["TipoCatalogo"].ToString(),
                                                          Desc_TipoFormato = row["Desc_TipoFormato"].ToString(),
                                                          PostalCodeOrig = row["PostalCodeOrig"].ToString(),
                                                          PostalCodeDestino = row["PostalCodeDestino"].ToString(),
                                                          CiudadOrig = row["CiudadOrig"].ToString(),
                                                          CiudadDest = row["CiudadDest"].ToString(),
                                                          EdoOrig = row["EdoOrig"].ToString(),
                                                          EdoDest = row["EdoDest"].ToString(),
                                                          SupplierName = row["SupplierName"].ToString(),
                                                          NombreTransportista = row["NombreTransportista"].ToString(),

                                                          Cve_CategSAP = row["Cve_CategSAP"].ToString(),
                                                          Desc_CategSAP = row["Desc_CategSAP"].ToString(),
                                                          Cve_GciaCategSAP = row["Cve_GciaCategSAP"].ToString(),
                                                          Desc_GciaCategSAP = row["Desc_GciaCategSAP"].ToString(),
                                                          Material_MATNR = row["Material_MATNR"].ToString(),
                                                          Id_Num_CodBarra = row["Id_Num_CodBarra"].ToString(),
                                                          Nombre_SKU = row["Nombre_SKU"].ToString(),
                                                          PesoMinimo = row["PesoMinimo"].ToString(),
                                                          PesoMaximo = row["PesoMaximo"].ToString(),
                                                          MesesSinIntereses = row["MesesSinIntereses"].ToString(),
                                                          ComprasMayor = row["ComprasMayor"].ToString(),
                                                          ComprasMenor = row["ComprasMenor"].ToString(),
                                                          FechaInicioPromo = row["FechaInicioPromo"].ToString(),
                                                          HoraInicioPromo = row["HoraInicioPromo"].ToString(),
                                                          FechaFinPromo = row["FechaFinPromo"].ToString(),
                                                          HoraFinPromo = row["HoraFinPromo"].ToString(),
                                                          CostoEspecial = string.IsNullOrEmpty(row["CostoEspecial"].ToString()) ? 0 : decimal.Parse(row["CostoEspecial"].ToString()),
                                                          TarifaDesc = string.IsNullOrEmpty(row["TarifaDesc"].ToString()) ? 0 : decimal.Parse(row["TarifaDesc"].ToString()),
                                                          UsuarioCreacion = row["UsuarioCreacion"].ToString(),
                                                          FechaUltModif = row["FechaUltModif"].ToString(),
                                                          HoraUltModif = row["HoraUltModif"].ToString(),
                                                          BitActivo = row["BitActivo"].ToString(),
                                                          cnscPromo = int.Parse(row["cnscPromo"].ToString())


                                                      };




            if (searchValue != "")
                query = query.Where(dd => dd.Desc_TipoEnvio.ToLower().Contains(searchValue)
                || dd.Desc_TipoServicio.ToLower().Contains(searchValue)
                || dd.TipoCatalogo.ToString().ToLower().Contains(searchValue)
                || dd.Desc_TipoFormato.ToLower().Contains(searchValue)
                || dd.PostalCodeOrig.ToLower().Contains(searchValue)
                || dd.PostalCodeDestino.ToLower().Contains(searchValue)
                || dd.CiudadOrig.ToLower().Contains(searchValue)
                || dd.CiudadDest.ToLower().Contains(searchValue)
                || dd.EdoOrig.ToLower().Contains(searchValue)
                || dd.EdoDest.ToLower().Contains(searchValue)
                || dd.SupplierName.ToLower().Contains(searchValue)
                || dd.NombreTransportista.ToLower().Contains(searchValue)
                || dd.Cve_CategSAP.ToLower().Contains(searchValue)
                || dd.Desc_CategSAP.ToLower().Contains(searchValue)
                || dd.Cve_GciaCategSAP.ToLower().Contains(searchValue)
                || dd.Desc_GciaCategSAP.ToLower().Contains(searchValue)
                || dd.Material_MATNR.ToLower().Contains(searchValue)
                || dd.Id_Num_CodBarra.ToLower().Contains(searchValue)
                || dd.Nombre_SKU.ToLower().Contains(searchValue)
                || dd.PesoMinimo.ToLower().Contains(searchValue)
                || dd.PesoMaximo.ToLower().Contains(searchValue)
                || dd.MesesSinIntereses.ToLower().Contains(searchValue)
                || dd.ComprasMayor.ToLower().Contains(searchValue)
                || dd.ComprasMenor.ToLower().Contains(searchValue)
                || dd.FechaInicioPromo.ToLower().Contains(searchValue)
                || dd.HoraInicioPromo.ToLower().Contains(searchValue)

                || dd.FechaFinPromo.ToLower().Contains(searchValue)
                || dd.HoraFinPromo.ToLower().Contains(searchValue)
                || dd.CostoEspecial.ToString().ToLower().Contains(searchValue)
                || dd.TarifaDesc.ToString().ToLower().Contains(searchValue)
                || dd.UsuarioCreacion.ToLower().Contains(searchValue)
                || dd.FechaUltModif.ToLower().Contains(searchValue)
                || dd.HoraUltModif.ToLower().Contains(searchValue)
                || dd.BitActivo.ToLower().Contains(searchValue)
                );


            #region Filter By Columns
            if (!string.IsNullOrEmpty(Desc_TipoEnvio))
            {
                query = query.Where(a => a.Desc_TipoEnvio.ToLower().Contains(Desc_TipoEnvio));
            }
            if (!string.IsNullOrEmpty(Desc_TipoServicio))
            {
                query = query.Where(a => a.Desc_TipoServicio.ToLower().Contains(Desc_TipoServicio));
            }

            if (!string.IsNullOrEmpty(TipoCatalogo))
            {
                query = query.Where(a => a.TipoCatalogo.ToString().ToLower().Contains(TipoCatalogo));
            }
            if (!string.IsNullOrEmpty(Desc_TipoFormato))
            {
                query = query.Where(a => a.Desc_TipoFormato.ToLower().Contains(Desc_TipoFormato));
            }

            if (!string.IsNullOrEmpty(PostalCodeOrig))
            {
                query = query.Where(a => a.PostalCodeOrig.ToLower().Contains(PostalCodeOrig));
            }

            if (!string.IsNullOrEmpty(PostalCodeDestino))
            {
                query = query.Where(a => a.PostalCodeDestino.ToLower().Contains(PostalCodeDestino));
            }



            if (!string.IsNullOrEmpty(CiudadOrig))
            {
                query = query.Where(a => a.CiudadOrig.ToLower().Contains(CiudadOrig));
            }

            if (!string.IsNullOrEmpty(CiudadDest))
            {
                query = query.Where(a => a.CiudadDest.ToLower().Contains(CiudadDest));
            }
            if (!string.IsNullOrEmpty(EdoOrig))
            {
                query = query.Where(a => a.EdoOrig.ToLower().Contains(EdoOrig));
            }

            if (!string.IsNullOrEmpty(EdoDest))
            {
                query = query.Where(a => a.EdoDest.ToLower().Contains(EdoDest));
            }


            //------------------
            if (!string.IsNullOrEmpty(SupplierName))
            {
                query = query.Where(a => a.SupplierName.ToLower().Contains(SupplierName));
            }
            if (!string.IsNullOrEmpty(NombreTransportista))
            {
                query = query.Where(a => a.NombreTransportista.ToLower().Contains(NombreTransportista));
            }
            if (!string.IsNullOrEmpty(Cve_CategSAP))
            {
                query = query.Where(a => a.Cve_CategSAP.ToLower().Contains(Cve_CategSAP));
            }
            if (!string.IsNullOrEmpty(Desc_CategSAP))
            {
                query = query.Where(a => a.Desc_CategSAP.ToLower().Contains(Desc_CategSAP));
            }
            if (!string.IsNullOrEmpty(Cve_GciaCategSAP))
            {
                query = query.Where(a => a.Cve_GciaCategSAP.ToLower().Contains(Cve_GciaCategSAP));
            }
            if (!string.IsNullOrEmpty(Desc_GciaCategSAP))
            {
                query = query.Where(a => a.Desc_GciaCategSAP.ToLower().Contains(Desc_GciaCategSAP));
            }
            if (!string.IsNullOrEmpty(Material_MATNR))
            {
                query = query.Where(a => a.Material_MATNR.ToLower().Contains(Material_MATNR));
            }
            if (!string.IsNullOrEmpty(Id_Num_CodBarra))
            {
                query = query.Where(a => a.Id_Num_CodBarra.ToLower().Contains(Id_Num_CodBarra));
            }
            if (!string.IsNullOrEmpty(Nombre_SKU))
            {
                query = query.Where(a => a.Nombre_SKU.ToLower().Contains(Nombre_SKU));
            }
            if (!string.IsNullOrEmpty(PesoMinimo))
            {
                query = query.Where(a => a.PesoMinimo.ToLower().Contains(PesoMinimo));
            }
            if (!string.IsNullOrEmpty(PesoMaximo))
            {
                query = query.Where(a => a.PesoMaximo.ToLower().Contains(PesoMaximo));
            }
            if (!string.IsNullOrEmpty(MesesSinIntereses))
            {
                query = query.Where(a => a.MesesSinIntereses.ToLower().Contains(MesesSinIntereses));
            }
            //--
            if (!string.IsNullOrEmpty(ComprasMayor))
            {
                query = query.Where(a => a.ComprasMayor.ToLower().Contains(ComprasMayor));
            }
            if (!string.IsNullOrEmpty(ComprasMenor))
            {
                query = query.Where(a => a.ComprasMenor.ToLower().Contains(ComprasMenor));
            }
            if (!string.IsNullOrEmpty(FechaInicioPromo))
            {
                query = query.Where(a => a.FechaInicioPromo.ToLower().Contains(FechaInicioPromo));
            }
            if (!string.IsNullOrEmpty(HoraInicioPromo))
            {
                query = query.Where(a => a.HoraInicioPromo.ToLower().Contains(HoraInicioPromo));
            }
            if (!string.IsNullOrEmpty(FechaFinPromo))
            {
                query = query.Where(a => a.FechaFinPromo.ToLower().Contains(FechaFinPromo));
            }
            if (!string.IsNullOrEmpty(HoraFinPromo))
            {
                query = query.Where(a => a.HoraFinPromo.ToLower().Contains(HoraFinPromo));
            }
            if (!string.IsNullOrEmpty(CostoEspecial))
            {
                query = query.Where(a => a.CostoEspecial.ToString().ToLower().Contains(CostoEspecial));
            }
            if (!string.IsNullOrEmpty(TarifaDesc))
            {
                query = query.Where(a => a.TarifaDesc.ToString().ToLower().Contains(TarifaDesc));
            }
            if (!string.IsNullOrEmpty(UsuarioCreacion))
            {
                query = query.Where(a => a.UsuarioCreacion.ToLower().Contains(UsuarioCreacion));
            }
            if (!string.IsNullOrEmpty(FechaUltModif))
            {
                query = query.Where(a => a.FechaUltModif.ToLower().Contains(FechaUltModif));
            }
            if (!string.IsNullOrEmpty(HoraUltModif))
            {
                query = query.Where(a => a.HoraUltModif.ToLower().Contains(HoraUltModif));
            }
            if (!string.IsNullOrEmpty(BitActivo))
            {
                string ac = "activo", ina = "inactivo";

                if (ac.Contains(BitActivo))
                    query = query.Where(a => a.BitActivo.ToLower().Contains("1"));
                if (ina.Contains(BitActivo))
                    query = query.Where(a => a.BitActivo.ToLower().Contains("0"));
            }
            #endregion



            recordsTotal = query.Count();

            lst = query.Take(recordsTotal).ToList();







            var d = new DataSet();

            string nombreArchivo = "Procomociones Costo de Envío";

            //Excel to create an object file

            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            //Add a sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");


            //Here you can set a variety of styles seemingly font color backgrounds, but not very convenient, there is not set
            //Sheet1 head to add the title of the first row
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);


            row1.CreateCell(0).SetCellValue("Tipo de  Envio");
            row1.CreateCell(1).SetCellValue("Tipo de  Servicio");
            row1.CreateCell(2).SetCellValue("Tipo de  Catalogo");
            row1.CreateCell(3).SetCellValue("Tipo de  Formato");
            row1.CreateCell(4).SetCellValue("Codigo Postal  Origen");
            row1.CreateCell(5).SetCellValue("Codigo Postal  Destino");
            row1.CreateCell(6).SetCellValue("Ciudad  Origen");
            row1.CreateCell(7).SetCellValue("Ciudad  Destino");
            row1.CreateCell(8).SetCellValue("Estado  Origen");
            row1.CreateCell(9).SetCellValue("Estado  Destino");
            row1.CreateCell(10).SetCellValue("Proveedor");
            row1.CreateCell(11).SetCellValue("Transportista");
            row1.CreateCell(12).SetCellValue("Id Categoria  (SAP)");
            row1.CreateCell(13).SetCellValue("Categoria  (SAP)");
            row1.CreateCell(14).SetCellValue("Id SubCategoria  (SAP)");
            row1.CreateCell(15).SetCellValue("SubCategoria  (SAP)");
            row1.CreateCell(16).SetCellValue("Numero de Material");
            row1.CreateCell(17).SetCellValue("EAN");
            row1.CreateCell(18).SetCellValue("Nombre SKU");
            row1.CreateCell(19).SetCellValue("Peso Mínimo");
            row1.CreateCell(20).SetCellValue("Peso Máximo");
            row1.CreateCell(21).SetCellValue("Meses sin  Intereses");
            row1.CreateCell(22).SetCellValue("Compras  Mayores a");
            row1.CreateCell(23).SetCellValue("Compras  Menores a ");
            row1.CreateCell(24).SetCellValue("Fecha inical de  tarifa especial");
            row1.CreateCell(25).SetCellValue("Hora inical de  tarifa especial");
            row1.CreateCell(26).SetCellValue("Fecha final de  tarifa especial");
            row1.CreateCell(27).SetCellValue("Hora final de  tarifa especial");
            row1.CreateCell(28).SetCellValue("Costo  especial($)");
            row1.CreateCell(29).SetCellValue("Aplica tarifa  descuento(%)");
            row1.CreateCell(30).SetCellValue("Usuario de  ultima actualización");
            row1.CreateCell(31).SetCellValue("Fecha de  ultima actualización");
            row1.CreateCell(32).SetCellValue("Hora de  ultima actualización");
            row1.CreateCell(33).SetCellValue("Activo");


             
                     


            //                                                
            //The data is written progressively sheet1 each row

            for (int i = 0; i < lst.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(lst[i].Desc_TipoEnvio.ToString());
                rowtemp.CreateCell(1).SetCellValue(lst[i].Desc_TipoServicio.ToString());
                rowtemp.CreateCell(2).SetCellValue(lst[i].TipoCatalogo.ToString());
                rowtemp.CreateCell(3).SetCellValue(lst[i].Desc_TipoFormato.ToString());
                rowtemp.CreateCell(4).SetCellValue(lst[i].PostalCodeOrig.ToString());
                rowtemp.CreateCell(5).SetCellValue(lst[i].PostalCodeDestino.ToString());
                rowtemp.CreateCell(6).SetCellValue(lst[i].CiudadOrig.ToString());
                rowtemp.CreateCell(7).SetCellValue(lst[i].CiudadDest.ToString());
                rowtemp.CreateCell(8).SetCellValue(lst[i].EdoOrig.ToString());
                rowtemp.CreateCell(9).SetCellValue(lst[i].EdoDest.ToString());
                rowtemp.CreateCell(10).SetCellValue(lst[i].SupplierName.ToString());
                rowtemp.CreateCell(11).SetCellValue(lst[i].NombreTransportista.ToString());
                rowtemp.CreateCell(12).SetCellValue(lst[i].Cve_CategSAP.ToString());
                rowtemp.CreateCell(13).SetCellValue(lst[i].Desc_CategSAP.ToString());
                rowtemp.CreateCell(14).SetCellValue(lst[i].Cve_GciaCategSAP.ToString());
                rowtemp.CreateCell(15).SetCellValue(lst[i].Desc_GciaCategSAP.ToString());
                rowtemp.CreateCell(16).SetCellValue(lst[i].Material_MATNR.ToString());
                rowtemp.CreateCell(17).SetCellValue(lst[i].Id_Num_CodBarra.ToString());
                rowtemp.CreateCell(18).SetCellValue(lst[i].Nombre_SKU.ToString());
                rowtemp.CreateCell(19).SetCellValue(lst[i].PesoMinimo.ToString());
                rowtemp.CreateCell(20).SetCellValue(lst[i].PesoMaximo.ToString());
                rowtemp.CreateCell(21).SetCellValue(lst[i].MesesSinIntereses.ToString());
                rowtemp.CreateCell(22).SetCellValue(lst[i].ComprasMayor.ToString());
                rowtemp.CreateCell(23).SetCellValue(lst[i].ComprasMenor.ToString());
                rowtemp.CreateCell(24).SetCellValue(lst[i].FechaInicioPromo.ToString());
                rowtemp.CreateCell(25).SetCellValue(lst[i].HoraInicioPromo.ToString());
                rowtemp.CreateCell(26).SetCellValue(lst[i].FechaFinPromo.ToString());
                rowtemp.CreateCell(27).SetCellValue(lst[i].HoraFinPromo.ToString());
                rowtemp.CreateCell(28).SetCellValue(lst[i].CostoEspecial.ToString());
                rowtemp.CreateCell(29).SetCellValue(lst[i].TarifaDesc.ToString());
                rowtemp.CreateCell(30).SetCellValue(lst[i].UsuarioCreacion.ToString());
                rowtemp.CreateCell(31).SetCellValue(lst[i].FechaUltModif.ToString());
                rowtemp.CreateCell(32).SetCellValue(lst[i].HoraUltModif.ToString());
                rowtemp.CreateCell(33).SetCellValue(lst[i].BitActivo.ToString());


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


    }
}