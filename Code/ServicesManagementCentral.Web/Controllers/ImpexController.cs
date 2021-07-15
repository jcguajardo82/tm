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
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;

namespace ServicesManagement.Web.Controllers
{
    public class ImpexController : Controller
    {

        public string draw = "";
        public string start = "";
        public string length = "";
        public string sortColumn = "";
        public string sortColumnDir = "";
        public string searchValue = "";
        public int pageSize, skip, recordsTotal;
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

                var list = DataTableToModel.ConvertTo<TransportistaRangosPesosShow>(DALImpex.upCorpTms_Cns_TransportistaRangosPesos().Tables[0]);

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
                                PorcentajeInicialCliente = Convert.ToDecimal(objDataRow[3].ToString()),
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

        [HttpPost]
        public ActionResult ImportFileCobPlaza(HttpPostedFileBase importFile)
        {
            if (importFile == null) return Json(new { Status = 0, Message = "No File Selected" });

            try
            {
                List<TransportistaPlazasDestinos> fileData2 = new List<TransportistaPlazasDestinos>();
                var fileData = GetDataFromFileCobPlaza(importFile.InputStream, ref fileData2);


                DALImpex.upCorpTms_Ins_TransportistaDestinosZonas(fileData.ToDataTable(), fileData2.ToDataTable());
                return Json(new { Status = 1, Message = "File Imported Successfully " });
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, Message = ex.Message });
            }
        }

        private List<TransportistaZonaPlazas> GetDataFromFileCobPlaza(Stream stream, ref List<TransportistaPlazasDestinos> TransportistaPlazasDestinos)
        {
            var dataTable = new DataTable();
            var TransportistaZonaPlazas = new List<TransportistaZonaPlazas>();
            try
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {


                            UseHeaderRow = false // To set First Row As Column Names    
                        }
                    });

                    if (dataSet.Tables.Count > 0)
                    {
                        dataTable = dataSet.Tables[0];




                        for (int i = 4; i < dataTable.Rows.Count; i++)
                        {
                            for (int a = 2; a < dataTable.Rows[i].ItemArray.Length; a++)
                            {
                                TransportistaZonaPlazas.Add(new TransportistaZonaPlazas
                                {
                                    IdTransportista = int.Parse(dataTable.Rows[0][1].ToString()),
                                    Cve_PlazaOrigen = dataTable.Rows[i][0].ToString().Trim(),
                                    Cve_PlazaDestino = dataTable.Rows[1].ItemArray[a].ToString().Trim(),
                                    IdZona = int.Parse(dataTable.Rows[i].ItemArray[a].ToString()),
                                    CreatedId = User.Identity.Name
                                });
                            }

                            var esDestino = false;
                            for (int a = 2; a < dataTable.Rows[1].ItemArray.Length; a++)
                            {
                                if (dataTable.Rows[i][0].ToString() == dataTable.Rows[1].ItemArray[a].ToString())
                                {

                                    esDestino = true;

                                }
                            }
                            TransportistaPlazasDestinos.Add(new TransportistaPlazasDestinos
                            {
                                IdTransportista = int.Parse(dataTable.Rows[0][1].ToString()),
                                Cve_Plaza = dataTable.Rows[i][0].ToString().Trim(),
                                EsOrigen = true,
                                EsDestino = esDestino,
                                CreatedId = User.Identity.Name
                            });


                        }


                        for (int a = 2; a < dataTable.Rows[1].ItemArray.Length; a++)
                        {
                            var esOrigen = false;

                            for (int i = 4; i < dataTable.Rows.Count; i++)
                            {
                                if (dataTable.Rows[i][0].ToString() == dataTable.Rows[1].ItemArray[a].ToString())
                                {
                                    esOrigen = true;
                                }
                            }
                            var item = new TransportistaPlazasDestinos();
                            item.IdTransportista = int.Parse(dataTable.Rows[0][1].ToString());
                            item.Cve_Plaza = dataTable.Rows[1][a].ToString();
                            item.EsOrigen = esOrigen;
                            item.EsDestino = true;
                            item.CreatedId = User.Identity.Name;

                            if (item.EsDestino != item.EsOrigen)
                            {
                                TransportistaPlazasDestinos.Add(item);
                            }

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return TransportistaZonaPlazas;
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
                    ,
                    resp = list.Count
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }




        public FileResult CostoEnvioProveedorExcel(int op, int IdTransportista, int IdTipoenvio, int IdTipoServicio)

        {

            var d = new DataSet();

            string nombreArchivo = "TransportistaZonaCostos";

            //Excel to create an object file

            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            //Add a sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");


            //Here you can set a variety of styles seemingly font color backgrounds, but not very convenient, there is not set
            //Sheet1 head to add the title of the first row
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);


            if (op == 1)
            {
                d = DALImpex.upCorpTms_Cns_TransportistaZonaCostos(IdTransportista, IdTipoenvio, IdTipoServicio);
            }
            else
            {
                d = DALImpex.upCorpTms_Cns_TransportistaZonaCostosTodos();
                nombreArchivo = "TransportistaZonaCostosTodos";
            }






            row1.CreateCell(0).SetCellValue("IdZona");
            row1.CreateCell(1).SetCellValue("NombreZona");
            row1.CreateCell(2).SetCellValue("CargoGasolina");
            row1.CreateCell(3).SetCellValue("PrecioExtraPeso");
            row1.CreateCell(4).SetCellValue("PrecioInicial");
            row1.CreateCell(5).SetCellValue("Otros");
            row1.CreateCell(6).SetCellValue("IdTransportista");
            row1.CreateCell(7).SetCellValue("NombreTransportista");
            row1.CreateCell(8).SetCellValue("IdTipoEnvio");
            row1.CreateCell(9).SetCellValue("NombreTipoEnvio");
            row1.CreateCell(10).SetCellValue("IdTipoServicio");
            row1.CreateCell(11).SetCellValue("NombreTipoServicio");
            row1.CreateCell(12).SetCellValue("diasEntrega");
            row1.CreateCell(13).SetCellValue("CreatedId");
            row1.CreateCell(14).SetCellValue("CreatedDate");



            //                                                
            //The data is written progressively sheet1 each row

            for (int i = 0; i < d.Tables[0].Rows.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(d.Tables[0].Rows[i][0].ToString());
                rowtemp.CreateCell(1).SetCellValue(d.Tables[0].Rows[i][1].ToString());
                rowtemp.CreateCell(2).SetCellValue(d.Tables[0].Rows[i][2].ToString());
                rowtemp.CreateCell(3).SetCellValue(d.Tables[0].Rows[i][3].ToString());
                rowtemp.CreateCell(4).SetCellValue(d.Tables[0].Rows[i][4].ToString());
                rowtemp.CreateCell(5).SetCellValue(d.Tables[0].Rows[i][5].ToString());
                rowtemp.CreateCell(6).SetCellValue(d.Tables[0].Rows[i][6].ToString());
                rowtemp.CreateCell(7).SetCellValue(d.Tables[0].Rows[i][7].ToString());
                rowtemp.CreateCell(8).SetCellValue(d.Tables[0].Rows[i][8].ToString());
                rowtemp.CreateCell(9).SetCellValue(d.Tables[0].Rows[i][9].ToString());
                rowtemp.CreateCell(10).SetCellValue(d.Tables[0].Rows[i][10].ToString());
                rowtemp.CreateCell(11).SetCellValue(d.Tables[0].Rows[i][11].ToString());
                rowtemp.CreateCell(12).SetCellValue(d.Tables[0].Rows[i][12].ToString());
                rowtemp.CreateCell(13).SetCellValue(d.Tables[0].Rows[i][13].ToString());
                rowtemp.CreateCell(14).SetCellValue(d.Tables[0].Rows[i][14].ToString());
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


        #region TransportistaRangosFijos
        public ActionResult CostosFijos()
        {


            return View();

        }


        public ActionResult GetCostosFijos()
        {
            try
            {

                var list = DataTableToModel.ConvertTo<TransportistaRangosFijosShow>(DALImpex.upCorpTms_Cns_TransportistaRangosFijos().Tables[0]);

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
        public ActionResult ImportCostosFijos(HttpPostedFileBase importFile)
        {
            if (importFile == null) return Json(new { Status = 0, Message = "No File Selected" });

            try
            {

                DALImpex.upCorpTms_Ins_TransportistaRangosFijos(GetDataFromFileCostosFijos(importFile.InputStream).ToDataTable());
                return Json(new { Status = 1, Message = "File Imported Successfully " });
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, Message = ex.Message });
            }
        }

        private List<TransportistaRangosFijos> GetDataFromFileCostosFijos(Stream stream)
        {
            var List = new List<TransportistaRangosFijos>();
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
                            List.Add(new TransportistaRangosFijos()
                            {
                                IdTransportista = Convert.ToInt32(objDataRow[0].ToString()),
                                IdZona = int.Parse(objDataRow[1].ToString()),
                                PesoInicio = decimal.Parse(objDataRow[2].ToString()),
                                PesoFin = Convert.ToInt32(objDataRow[3].ToString()),
                                CostoFijo = decimal.Parse(objDataRow[4].ToString()),
                                bitDeleted = objDataRow[5].ToString().Equals("0") ? false : true, //Convert.ToBoolean(objDataRow[4].ToString()),
                                CreatedId = User.Identity.Name,
                                TiempoEnvio = int.Parse(objDataRow[6].ToString())
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


        #region Prueba 
        public ActionResult Example()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Grid()
        {
            List<TransportistaPlazasShow> lst = new List<TransportistaPlazasShow>();

            //logistica datatable
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault().ToLower();



            #region Se Obtienen Filtros Por Columna
            var IdTransportista = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault().ToLower();
            var NomTransportista = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault().ToLower();
            var IdPlaza = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault().ToLower();
            var CvePlaza = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault().ToLower();
            var PostalCode = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault().ToLower();
            var DescTipoEnvio = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault().ToLower();

            var CreatedId = Request.Form.GetValues("columns[6][search][value]").FirstOrDefault().ToLower();
            var CreatedDate = Request.Form.GetValues("columns[7][search][value]").FirstOrDefault().ToLower();
            var CreatedTime = Request.Form.GetValues("columns[8][search][value]").FirstOrDefault().ToLower();
            var BitActivo = Request.Form.GetValues("columns[9][search][value]").FirstOrDefault().ToLower();



            #endregion

            pageSize = length != null ? Convert.ToInt32(length) : 0;
            skip = start != null ? Convert.ToInt32(start) : 0;
            recordsTotal = 0;

            IQueryable<TransportistaPlazasShow> query = from row in DALImpex.upCorpTms_Cns_TransportistaPlazas().Tables[0].AsEnumerable().AsQueryable()
                                                        select new TransportistaPlazasShow()
                                                        {
                                                            IdTransportista = row["IdTransportista"].ToString(),
                                                            NomTransportista = row["NomTransportista"].ToString(),
                                                            IdPlaza = int.Parse(row["IdPlaza"].ToString()),
                                                            CvePlaza = row["CvePlaza"].ToString(),
                                                            NomPlaza = row["NomPlaza"].ToString(),
                                                            DescTipoEnvio = row["DescTipoEnvio"].ToString(),
                                                            PostalCode = row["PostalCode"].ToString(),
                                                            CreatedId = row["CreatedId"].ToString(),
                                                            CreatedDate = row["CreatedDate"].ToString(),
                                                            CreatedTime = row["CreatedTime"].ToString(),
                                                            BitActivo = row["BitActivo"].ToString()


                                                        };




            if (searchValue != "")
                query = query.Where(d => d.IdTransportista.ToLower().Contains(searchValue)
                || d.NomTransportista.ToLower().Contains(searchValue)
                || d.IdPlaza.ToString().ToLower().Contains(searchValue)
                || d.CvePlaza.ToLower().Contains(searchValue)
                || d.NomPlaza.ToLower().Contains(searchValue)
                || d.DescTipoEnvio.ToLower().Contains(searchValue)
                || d.PostalCode.ToLower().Contains(searchValue)
                || d.CreatedId.ToLower().Contains(searchValue)
                || d.CreatedDate.ToLower().Contains(searchValue)
                || d.CreatedTime.ToLower().Contains(searchValue)
                || d.BitActivo.ToLower().Contains(searchValue)
                );





            //Filter By Columns
            #region Filter By Columns
            if (!string.IsNullOrEmpty(IdTransportista))
            {
                query = query.Where(a => a.IdTransportista.ToLower().Contains(IdTransportista));
            }
            if (!string.IsNullOrEmpty(NomTransportista))
            {
                query = query.Where(a => a.NomTransportista.ToLower().Contains(NomTransportista));
            }

            if (!string.IsNullOrEmpty(IdPlaza))
            {
                query = query.Where(a => a.IdPlaza.ToString().ToLower().Contains(IdPlaza));
            }
            if (!string.IsNullOrEmpty(CvePlaza))
            {
                query = query.Where(a => a.CvePlaza.ToLower().Contains(CvePlaza));
            }

            if (!string.IsNullOrEmpty(DescTipoEnvio))
            {
                query = query.Where(a => a.DescTipoEnvio.ToLower().Contains(DescTipoEnvio));
            }

            if (!string.IsNullOrEmpty(PostalCode))
            {
                query = query.Where(a => a.PostalCode.ToLower().Contains(PostalCode));
            }



            if (!string.IsNullOrEmpty(CreatedId))
            {
                query = query.Where(a => a.CreatedId.ToLower().Contains(CreatedId));
            }

            if (!string.IsNullOrEmpty(CreatedDate))
            {
                query = query.Where(a => a.CreatedDate.ToLower().Contains(CreatedDate));
            }
            if (!string.IsNullOrEmpty(CreatedTime))
            {
                query = query.Where(a => a.CreatedTime.ToLower().Contains(CreatedTime));
            }

            if (!string.IsNullOrEmpty(BitActivo))
            {
                query = query.Where(a => a.BitActivo.ToLower().Contains(BitActivo));
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

  
        public FileResult ExcelPrueba()

        {

            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault().ToLower();



            #region Se Obtienen Filtros Por Columna
            var IdTransportista = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault().ToLower();
            var NomTransportista = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault().ToLower();
            var IdPlaza = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault().ToLower();
            var CvePlaza = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault().ToLower();
            var PostalCode = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault().ToLower();
            var DescTipoEnvio = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault().ToLower();

            var CreatedId = Request.Form.GetValues("columns[6][search][value]").FirstOrDefault().ToLower();
            var CreatedDate = Request.Form.GetValues("columns[7][search][value]").FirstOrDefault().ToLower();
            var CreatedTime = Request.Form.GetValues("columns[8][search][value]").FirstOrDefault().ToLower();
            var BitActivo = Request.Form.GetValues("columns[9][search][value]").FirstOrDefault().ToLower();



            #endregion



            var d = new DataSet();

            string nombreArchivo = "TransportistaZonaCostos";

            //Excel to create an object file

            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            //Add a sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");


            //Here you can set a variety of styles seemingly font color backgrounds, but not very convenient, there is not set
            //Sheet1 head to add the title of the first row
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);









            row1.CreateCell(0).SetCellValue("IdZona");
            row1.CreateCell(1).SetCellValue("NombreZona");
            row1.CreateCell(2).SetCellValue("CargoGasolina");
            row1.CreateCell(3).SetCellValue("PrecioExtraPeso");
            row1.CreateCell(4).SetCellValue("PrecioInicial");
            row1.CreateCell(5).SetCellValue("Otros");
            row1.CreateCell(6).SetCellValue("IdTransportista");
            row1.CreateCell(7).SetCellValue("NombreTransportista");
            row1.CreateCell(8).SetCellValue("IdTipoEnvio");
            row1.CreateCell(9).SetCellValue("NombreTipoEnvio");
            row1.CreateCell(10).SetCellValue("IdTipoServicio");
            row1.CreateCell(11).SetCellValue("NombreTipoServicio");
            row1.CreateCell(12).SetCellValue("diasEntrega");
            row1.CreateCell(13).SetCellValue("CreatedId");
            row1.CreateCell(14).SetCellValue("CreatedDate");



            //                                                
            //The data is written progressively sheet1 each row

            for (int i = 0; i < d.Tables[0].Rows.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(d.Tables[0].Rows[i][0].ToString());
                rowtemp.CreateCell(1).SetCellValue(d.Tables[0].Rows[i][1].ToString());
                rowtemp.CreateCell(2).SetCellValue(d.Tables[0].Rows[i][2].ToString());
                rowtemp.CreateCell(3).SetCellValue(d.Tables[0].Rows[i][3].ToString());
                rowtemp.CreateCell(4).SetCellValue(d.Tables[0].Rows[i][4].ToString());
                rowtemp.CreateCell(5).SetCellValue(d.Tables[0].Rows[i][5].ToString());
                rowtemp.CreateCell(6).SetCellValue(d.Tables[0].Rows[i][6].ToString());
                rowtemp.CreateCell(7).SetCellValue(d.Tables[0].Rows[i][7].ToString());
                rowtemp.CreateCell(8).SetCellValue(d.Tables[0].Rows[i][8].ToString());
                rowtemp.CreateCell(9).SetCellValue(d.Tables[0].Rows[i][9].ToString());
                rowtemp.CreateCell(10).SetCellValue(d.Tables[0].Rows[i][10].ToString());
                rowtemp.CreateCell(11).SetCellValue(d.Tables[0].Rows[i][11].ToString());
                rowtemp.CreateCell(12).SetCellValue(d.Tables[0].Rows[i][12].ToString());
                rowtemp.CreateCell(13).SetCellValue(d.Tables[0].Rows[i][13].ToString());
                rowtemp.CreateCell(14).SetCellValue(d.Tables[0].Rows[i][14].ToString());
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