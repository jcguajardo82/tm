using ExcelDataReader;
using ServicesManagement.Web.DAL;
using ServicesManagement.Web.Helpers;
using ServicesManagement.Web.Models.Impex;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ServicesManagement.Web.Controllers
{
    public class VehiculoModel
    {
        public int Id_Vehiculo { get; set; }
        public string Descripcion { get; set; }
        public string Placas { get; set; }
        public string Motor { get; set; }
        public bool Estatus { get; set; }
        public string Fec_Movto { get; set; }
        public string created_user { get; set; }
        public string modified_user { get; set; }

    }

    public class OperadorModel
    {
        public int Id_Transportista { get; set; }
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string Pass { get; set; }
        public bool Estatus { get; set; }
        public string Fec_Movto { get; set; }
        public string created_user { get; set; }
        public string modified_user { get; set; }

    }


    [Authorize]
    public class TMSController : Controller
    {
        string UrlApi = System.Configuration.ConfigurationManager.AppSettings["api_TMS"].ToString();


        // GET: TMS
        public ActionResult MainSystem()
        {
            return View();
        }


        public ActionResult AltaVehiculos()
        {
            return View();
        }


        public ActionResult AltaVehiculo()
        {
            return View();
        }


        public ActionResult ReglasEnvioLogistica()
        {
            return View();
        }


        public ActionResult CostoEnvioProv()
        {
            return View();
        }


        public ActionResult TiposEnvioPorAlmacen()
        {
            return View();
        }


        public ActionResult TiposEnvioDesdeAlmacen()
        {
            return View();
        }



        public ActionResult Transportistas()
        {
            return View();
        }


        public ActionResult CPxDisponibilidadTrans()
        {
            return View();
        }

        public ActionResult Gastosvehículo()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetVehiculos()
        {
            try
            {
                string apiUrl = string.Format("{0}/GetVehiculos", UrlApi);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Soriana.FWK.FmkTools.RestResponse responseApi1 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.GET, apiUrl, null, "");

                if (responseApi1.code.Equals("00"))
                {
                    List<VehiculoModel> listC = Newtonsoft.Json.JsonConvert.DeserializeObject<List<VehiculoModel>>(responseApi1.message);

                    var result = new { Success = true, json = listC };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                var result1 = new { Success = true };
                return Json(result1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Cat_Vehiculo()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetVehiculo(string Id)
        {
            try
            {
                //string apiUrl = string.Format("{0}/GetVehiculoById", UrlApi);
                string apiUrl = string.Format("{0}/GetVehiculos", UrlApi);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Soriana.FWK.FmkTools.RestResponse responseApi1 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.GET, apiUrl, null, "");

                if (responseApi1.code.Equals("00"))
                {
                    List<VehiculoModel> listC = Newtonsoft.Json.JsonConvert.DeserializeObject<List<VehiculoModel>>(responseApi1.message);

                    List<VehiculoModel> listR = new List<VehiculoModel>();

                    listR.Add(listC.First(c => c.Id_Vehiculo == Convert.ToInt32(Id)));



                    var result = new { Success = true, json = listR };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new { Success = false, Message = "Error al ejecutar la accion" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                var result1 = new { Success = true };
                return Json(result1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> InsVehiculo(string descripcion, string motor, string placas)
        {
            try
            {
                string apiUrl = string.Format("{0}/AddVehiculo", UrlApi);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                VehiculoModel v = new VehiculoModel { Descripcion = descripcion, Placas = placas, Motor = motor };

                Soriana.FWK.FmkTools.RestResponse responseApi1 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, apiUrl, null, Newtonsoft.Json.JsonConvert.SerializeObject(v));

                if (responseApi1.code.Equals("00"))
                {
                    List<VehiculoModel> listC = Newtonsoft.Json.JsonConvert.DeserializeObject<List<VehiculoModel>>(responseApi1.message);


                    var result = new { Success = true, json = listC };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new { Success = false, Message = "Error al ejecutar la accion" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                var result1 = new { Success = true };
                return Json(result1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetVehiculoById(string Id)
        {
            try
            {
                //string apiUrl = string.Format("{0}/GetVehiculos", UrlApi) + "?Id_Vehiculo=" + Id;
                string apiUrl = string.Format("{0}/GetVehiculos", UrlApi);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //VehiculoModel v = new VehiculoModel { Descripcion = descripcion, Placas = placas, Motor = motor };

                Soriana.FWK.FmkTools.RestResponse responseApi1 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.GET, apiUrl, null, "");

                if (responseApi1.code.Equals("00"))
                {
                    //VehiculoModel listC = Newtonsoft.Json.JsonConvert.DeserializeObject<VehiculoModel>(responseApi1.message);
                    List<VehiculoModel> listC = Newtonsoft.Json.JsonConvert.DeserializeObject<List<VehiculoModel>>(responseApi1.message);



                    var result = new { Success = true, json = listC.First(c => c.Id_Vehiculo == Convert.ToInt32(Id)) };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new { Success = false, Message = "Error al ejecutar la accion" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                var result1 = new { Success = true };
                return Json(result1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> EditVehiculo(string Id, string descripcion, string motor, string placas, string estatus)
        {
            try
            {
                string apiUrl = string.Format("{0}/UpdVehiculo", UrlApi);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                VehiculoModel v = new VehiculoModel { Id_Vehiculo = Convert.ToInt32(Id), Descripcion = descripcion, Placas = placas, Motor = motor, Estatus = estatus.Equals("1") ? true : false };

                Soriana.FWK.FmkTools.RestResponse responseApi1 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, apiUrl, null, Newtonsoft.Json.JsonConvert.SerializeObject(v));

                if (responseApi1.code.Equals("00"))
                {
                    List<VehiculoModel> listC = Newtonsoft.Json.JsonConvert.DeserializeObject<List<VehiculoModel>>(responseApi1.message);

                    var result = new { Success = true, json = listC };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new { Success = false, Message = "Error al ejecutar la accion" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                var result1 = new { Success = true };
                return Json(result1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> DeleteVehiculo(string Id)
        {
            try
            {
                string apiUrl = string.Format("{0}/DelVehiculo", UrlApi);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                VehiculoModel v = new VehiculoModel { Id_Vehiculo = Convert.ToInt32(Id) };

                Soriana.FWK.FmkTools.RestResponse responseApi1 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, apiUrl, null, Newtonsoft.Json.JsonConvert.SerializeObject(v));

                if (responseApi1.code.Equals("00"))
                {
                    List<VehiculoModel> listC = Newtonsoft.Json.JsonConvert.DeserializeObject<List<VehiculoModel>>(responseApi1.message);

                    var result = new { Success = true, json = listC };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new { Success = false, Message = "Error al ejecutar la accion" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                var result1 = new { Success = true };
                return Json(result1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetOperadores()
        {
            try
            {
                string apiUrl = string.Format("{0}/GetOperadores", UrlApi);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Soriana.FWK.FmkTools.RestResponse responseApi1 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.GET, apiUrl, null, "");

                if (responseApi1.code.Equals("00"))
                {
                    List<OperadorModel> listC = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OperadorModel>>(responseApi1.message);

                    var result = new { Success = true, json = listC };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                var result1 = new { Success = true };
                return Json(result1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetOperador(string Id)
        {
            try
            {
                string apiUrl = string.Format("{0}/GetOperadores", UrlApi);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Soriana.FWK.FmkTools.RestResponse responseApi1 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.GET, apiUrl, null, "");

                if (responseApi1.code.Equals("00"))
                {
                    List<OperadorModel> listC = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OperadorModel>>(responseApi1.message);

                    var result = new { Success = true, json = listC };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new { Success = false, Message = "Error al ejecutar la accion" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                var result1 = new { Success = true };
                return Json(result1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> InsOperador(string nombre, string user, string pass, string matricula)
        {
            try
            {
                string apiUrl = string.Format("{0}/AddOperador", UrlApi);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                OperadorModel v = new OperadorModel { Nombre = nombre, Usuario = user, Pass = pass, Matricula = matricula };

                Soriana.FWK.FmkTools.RestResponse responseApi1 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, apiUrl, null, Newtonsoft.Json.JsonConvert.SerializeObject(v));

                if (responseApi1.code.Equals("00"))
                {
                    List<OperadorModel> listC = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OperadorModel>>(responseApi1.message);

                    var result = new { Success = true, json = listC };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new { Success = false, Message = "Error al ejecutar la accion" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                var result1 = new { Success = true };
                return Json(result1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetOperadorById(string Id)
        {
            try
            {
                string apiUrl = string.Format("{0}/GetOperadorById", UrlApi) + "?Id_Transportista=" + Id;

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //VehiculoModel v = new VehiculoModel { Descripcion = descripcion, Placas = placas, Motor = motor };

                Soriana.FWK.FmkTools.RestResponse responseApi1 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.GET, apiUrl, null, "");

                if (responseApi1.code.Equals("00"))
                {
                    OperadorModel listC = Newtonsoft.Json.JsonConvert.DeserializeObject<OperadorModel>(responseApi1.message);

                    var result = new { Success = true, json = listC };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new { Success = false, Message = "Error al ejecutar la accion" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                var result1 = new { Success = true };
                return Json(result1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> EditOperador(string Id, string nombre, string user, string pass, string matricula, string estatus)
        {
            try
            {
                string apiUrl = string.Format("{0}/UpdOperador", UrlApi);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                OperadorModel v = new OperadorModel { Id_Transportista = Convert.ToInt32(Id), Matricula = matricula, Usuario = user, Pass = pass, Nombre = nombre, Estatus = estatus.Equals("1") ? true : false };

                Soriana.FWK.FmkTools.RestResponse responseApi1 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, apiUrl, null, Newtonsoft.Json.JsonConvert.SerializeObject(v));

                if (responseApi1.code.Equals("00"))
                {
                    List<OperadorModel> listC = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OperadorModel>>(responseApi1.message);

                    var result = new { Success = true, json = listC };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    var result = new { Success = false, Message = "Error al ejecutar la accion" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                var result1 = new { Success = true };
                return Json(result1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> DeleteOperador(string Id)
        {
            try
            {
                string apiUrl = string.Format("{0}/DelOperador", UrlApi);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                VehiculoModel v = new VehiculoModel { Id_Vehiculo = Convert.ToInt32(Id) };

                Soriana.FWK.FmkTools.RestResponse responseApi1 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, apiUrl, null, Newtonsoft.Json.JsonConvert.SerializeObject(v));

                if (responseApi1.code.Equals("00"))
                {
                    List<OperadorModel> listC = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OperadorModel>>(responseApi1.message);

                    var result = new { Success = true, json = listC };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new { Success = false, Message = "Error al ejecutar la accion" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                var result1 = new { Success = true };
                return Json(result1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        //public ActionResult Cat_Vehiculo()
        //{
        //    return View();
        //}


        public ActionResult Cat_Transportista()
        {
            return View();
        }

        public ActionResult Cat_Servicio()
        {

            string apiUrl = string.Format("{0}/GetVehiculos", UrlApi);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Soriana.FWK.FmkTools.RestResponse responseApi1 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.GET, apiUrl, null, "");

            if (responseApi1.code.Equals("00"))
            {
                List<VehiculoModel> listC = Newtonsoft.Json.JsonConvert.DeserializeObject<List<VehiculoModel>>(responseApi1.message);

                Session["listaV"] = listC;
            }

            apiUrl = string.Format("{0}/GetOperadores", UrlApi);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            responseApi1 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.GET, apiUrl, null, "");

            if (responseApi1.code.Equals("00"))
            {
                List<OperadorModel> listC = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OperadorModel>>(responseApi1.message);
                Session["listaO"] = listC;

            }

            return View();
        }

        #region ArchivosExcel
        public ActionResult CobZona()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ImportFileCobZona(HttpPostedFileBase importFile)
        {
            if (importFile == null) return Json(new { Status = 0, Message = "No File Selected" });

            try
            {
                var fileData = GetDataFromCSVFileCobZon(importFile.InputStream);

                ViewBag.List = fileData;

                //var dtEmployee = fileData.ToDataTable();
                //var tblEmployeeParameter = new SqlParameter("tblEmployeeTableType", SqlDbType.Structured)
                //{
                //    TypeName = "dbo.tblTypeEmployee",
                //    Value = dtEmployee
                //};
                //await _dbContext.Database.ExecuteSqlCommandAsync("EXEC spBulkImportEmployee @tblEmployeeTableType", tblEmployeeParameter);
                return Json(new { Status = 1, Message = "File Imported Successfully " });
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, Message = ex.Message });
            }
        }

        private List<CobZon> GetDataFromCSVFileCobZon(Stream stream)
        {
            var List = new List<CobZon>();
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
                            var item = new CobZon();


                            item.NoProveedor = objDataRow[0].ToString();
                            item.CpOrigen = objDataRow[1].ToString();
                            item.CoberturaBigT = objDataRow[2].ToString();
                            item.CoberturaPyM = objDataRow[3].ToString();
                            item.CodigoPostal = objDataRow[4].ToString();
                            item.Zonas = objDataRow[5].ToString();
                            item.TiemposEntregaBT = objDataRow[6].ToString();
                            item.TiemposEntregaPyM = objDataRow[7].ToString();
                            item.Municipio = objDataRow[8].ToString();
                            item.Estado = objDataRow[9].ToString();
                            item.SiglasPlaza = objDataRow[10].ToString();
                            item.Lunes = objDataRow[11].ToString();
                            item.Martes = objDataRow[12].ToString();
                            item.Miercoles = objDataRow[13].ToString();
                            item.Jueves = objDataRow[14].ToString();
                            item.Viernes = objDataRow[15].ToString();
                            item.Sabado = objDataRow[16].ToString();
                            item.Domingo = objDataRow[17].ToString();
                            item.Periodicidad = objDataRow[18].ToString();
                            item.EsOcurreForzoso = objDataRow[19].ToString();
                            item.Reexpedicion = objDataRow[20].ToString();
                            item.GarantiaMax = objDataRow[21].ToString();

                            List.Add(item);

                            DALImpex.CobZon_iUp(item);
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

        public ActionResult GetCobZona()
        {
            try
            {
                List<CobZon> list = new List<CobZon>();

                var ds = DALImpex.CobZon_sUp();

                if (ds.Tables.Count > 0)
                {
                    list = DataTableToModel.ConvertTo<CobZon>(ds.Tables[0]);
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


        public ActionResult KgDinero()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportFileKg(HttpPostedFileBase importFile)
        {
            if (importFile == null) return Json(new { Status = 0, Message = "No File Selected" });

            try
            {
                var fileData = GetDataFromCSVFileKg(importFile.InputStream);

                ViewBag.List = fileData;

                //var dtEmployee = fileData.ToDataTable();
                //var tblEmployeeParameter = new SqlParameter("tblEmployeeTableType", SqlDbType.Structured)
                //{
                //    TypeName = "dbo.tblTypeEmployee",
                //    Value = dtEmployee
                //};
                //await _dbContext.Database.ExecuteSqlCommandAsync("EXEC spBulkImportEmployee @tblEmployeeTableType", tblEmployeeParameter);
                return Json(new { Status = 1, Message = "File Imported Successfully " });
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, Message = ex.Message });
            }
        }

        private List<KgDinero> GetDataFromCSVFileKg(Stream stream)
        {
            var List = new List<KgDinero>();
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
                            var item = new KgDinero();


                            item.KG = objDataRow[0].ToString();
                            item.A1 = objDataRow[1].ToString();
                            item.A2 = objDataRow[2].ToString();
                            item.A3 = objDataRow[3].ToString();
                            item.A4 = objDataRow[4].ToString();
                            item.A5 = objDataRow[5].ToString();
                            item.A6 = objDataRow[6].ToString();
                            item.A7 = objDataRow[7].ToString();
                            item.PorcCliente = objDataRow[8].ToString();
                            item.B1 = objDataRow[9].ToString();
                            item.B2 = objDataRow[10].ToString();
                            item.B3 = objDataRow[11].ToString();
                            item.B4 = objDataRow[12].ToString();
                            item.B5 = objDataRow[13].ToString();
                            item.B6 = objDataRow[14].ToString();
                            item.B7 = objDataRow[15].ToString();
                          

                            List.Add(item);

                            DALImpex.KgDinero_iUp(item);
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

        public ActionResult GetKg()
        {
            try
            {
                List<KgDinero> list = new List<KgDinero>();

                var ds = DALImpex.KgDinero_sUp();

                if (ds.Tables.Count > 0)
                {
                    list = DataTableToModel.ConvertTo<KgDinero>(ds.Tables[0]);
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
        #endregion
    }
}