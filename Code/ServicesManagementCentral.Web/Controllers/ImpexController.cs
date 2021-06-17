using ExcelDataReader;
using ServicesManagement.Web.DAL;
using ServicesManagement.Web.Helpers;
using ServicesManagement.Web.Models.Catalogos;
using ServicesManagement.Web.Models.Impex;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicesManagement.Web.Controllers
{
    public class ImpexController : Controller
    {
        // GET: Impex
        public ActionResult Index()
        {
            return View();
        }

        #region TransPlazas
        public ActionResult TransPlazas()
        {
            return View();
        }

        public ActionResult GetTransPlazas()
        {
            try
            {
                List<TransportistaPlazasShow> list = new List<TransportistaPlazasShow>();

                var ds = DALImpex.upCorpTms_Cns_TransportistaPlazas();

                if (ds.Tables.Count > 0)
                {
                    list = DataTableToModel.ConvertTo<TransportistaPlazasShow>(ds.Tables[0]);
                }

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ImportFileTransPlazas(HttpPostedFileBase importFile)
        {
            if (importFile == null) return Json(new { Status = 0, Message = "No File Selected" });

            try
            {
                var fileData = GetDataFromFileTransPlazas(importFile.InputStream);



                var dt = fileData.ToDataTable();


                DALImpex.upCorpTms_Ins_TransportistaPlazas(dt);
                return Json(new { Status = 1, Message = "File Imported Successfully " });
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, Message = ex.Message });
            }
        }

        private List<TransportistaPlazas> GetDataFromFileTransPlazas(Stream stream)
        {
            var List = new List<TransportistaPlazas>();
            try
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true // To set First Row As Column Names    
                        }
                    });

                    if (dataSet.Tables.Count > 0)
                    {
                        var dataTable = dataSet.Tables[0];
                        foreach (DataRow objDataRow in dataTable.Rows)
                        {
                            if (objDataRow.ItemArray.All(x => string.IsNullOrEmpty(x?.ToString()))) continue;
                            List.Add(new TransportistaPlazas()
                            {
                                IdTransportista = Convert.ToInt32(objDataRow[0].ToString()),
                                Cve_Plaza = objDataRow[1].ToString(),
                                PostalCode = objDataRow[2].ToString(),
                                IdTipoEnvio = Convert.ToInt32(objDataRow[3].ToString()),
                                bitDeleted = objDataRow[4].ToString().Equals("0") ? false : true, //Convert.ToBoolean(objDataRow[4].ToString()),
                                CreatedId = User.Identity.Name
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return List;
        }
        #endregion

        #region TransportistaRangosPesos
        public ActionResult TransRangosPesos()
        {
            return View();
        }

        public ActionResult GetTransRangosPesos()
        {
            try
            {

                var list = DataTableToModel.ConvertTo<TransportistaRangosPesos>(DALImpex.upCorpTms_Cns_TransportistaRangosPesos().Tables[0]);

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ImportFileTransRangosPesos(HttpPostedFileBase importFile)
        {
            if (importFile == null) return Json(new { Status = 0, Message = "No File Selected" });

            try
            {
                var fileData = GetDataFromFileTransRangosPesos(importFile.InputStream);
                DALImpex.upCorpTms_Ins_TransportistaRangosPesos(fileData.ToDataTable());
                return Json(new { Status = 1, Message = "File Imported Successfully " });
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, Message = ex.Message });
            }
        }
        private List<TransportistaRangosPesos> GetDataFromFileTransRangosPesos(Stream stream)
        {
            var List = new List<TransportistaRangosPesos>();
            try
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true // To set First Row As Column Names    
                        }
                    });

                    if (dataSet.Tables.Count > 0)
                    {
                        var dataTable = dataSet.Tables[0];
                        foreach (DataRow objDataRow in dataTable.Rows)
                        {
                            if (objDataRow.ItemArray.All(x => string.IsNullOrEmpty(x?.ToString()))) continue;
                            List.Add(new TransportistaRangosPesos()
                            {
                                IdTransportista = Convert.ToInt32(objDataRow[0].ToString()),
                                PesoInicio = Convert.ToDecimal(objDataRow[1].ToString()),
                                PesoFin = Convert.ToDecimal(objDataRow[2].ToString()),
                                PorcentajeInicialCliente = Convert.ToDecimal(objDataRow[4].ToString()),
                                bitDeleted = objDataRow[4].ToString().Equals("0") ? false : true,//Convert.ToBoolean(int.Parse(objDataRow[4].ToString())),
                                CreatedId = User.Identity.Name
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return List;
        }
        #endregion

        #region Cobertura por plaza, de Origen al Destino.

        public ActionResult CoberturaOrigenDestino()
        {
            return View();
        }

        public ActionResult GetCoberturaOrigenDestino()
        {
            try
            {

                var list = DataTableToModel.ConvertTo<Cns_TransportistaDestinosZonas>(DALImpex.upCorpTms_Cns_TransportistaDestinosZonas().Tables[0]);

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion

        #region CostoEnvioProveedor
        public ActionResult CostoEnvioProveedor()
        {

            return View();
        }

        public ActionResult GetCostoEnvioProveedor(int IdTransportista, int IdTipoenvio, int IdTipoServicio)
        {
            try
            {
                var list = DataTableToModel.ConvertTo<TransportistaZonaCostos>(DALImpex.upCorpTms_Cns_TransportistaZonaCostos(IdTransportista, IdTipoenvio, IdTipoServicio).Tables[0]);

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetCombos()
        {
            try
            {
                //var list1 = DataTableToModel.ConvertTo<TransportistaZonaCostos>(DALCatalogo.TipoServicio_sUp().Tables[0]);
                //ViewBag.TipoEnvio = DataTableToModel.ConvertTo<TransportistaZonaCostos>(DALCatalogo.TipoEnvio_sUp().Tables[0]);

                var result = new
                {
                    Success = true
                    ,
                    servicio = DataTableToModel.ConvertTo<TipoServicio>(DALCatalogo.TipoServicio_sUp().Tables[0])
                    ,
                    envio = DataTableToModel.ConvertTo<TipoEnvio>(DALCatalogo.TipoEnvio_sUp().Tables[0])
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
       
        public ActionResult InsertCustomers(List<TransportistaZonaCostos> list)
        {


            try
            {
                //DALImpex.upCorpTms_Ins_TransportistaZonaCostos(int.Parse(IdZona)
                //    , decimal.Parse(CargoGasolina), decimal.Parse(PrecioExtraPeso), decimal.Parse(PrecioInicial), decimal.Parse(Otros)
                //    , int.Parse(IdTransportista), int.Parse(IdTipoenvio), int.Parse(IdTipoServicio), int.Parse(diasEntrega)
                //    , User.Identity.Name);

                foreach (TransportistaZonaCostos item in list)
                {
                    DALImpex.upCorpTms_Ins_TransportistaZonaCostos(item.IdZona, item.CargoGasolina, item.PrecioExtraPeso, item.PrecioInicial, item.Otros, item.IdTransportista
                        , item.IdTipoEnvio, item.IdTipoServicio, item.diasEntrega, User.Identity.Name);
                }

                var result = new
                {
                    Success = true
                    ,resp=list.Count
                };
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