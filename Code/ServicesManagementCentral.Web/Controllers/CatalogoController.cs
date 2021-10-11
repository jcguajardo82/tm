﻿

using ExcelDataReader;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Newtonsoft.Json;
using ServicesManagement.Web.DAL;
using ServicesManagement.Web.Helpers;
using ServicesManagement.Web.Models;
using ServicesManagement.Web.Models.Catalogos;
using ServicesManagement.Web.Models.TipoLogistica;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TipoEntregaSETC = ServicesManagement.Web.Models.Catalogos.TipoEntregaSETC;
using TipoEnvio = ServicesManagement.Web.Models.Catalogos.TipoEnvio;
using TipoServicio = ServicesManagement.Web.Models.Catalogos.TipoServicio;

namespace ServicesManagement.Web.Controllers

{
    public class Poblacion_dModel
    {
        public string Id_Num_Pais { get; set; }
        public Int16 Id_Num_Estado { get; set; }
        public Int16 Id_Num_Poblacion { get; set; }
        public String Nom_Poblacion { get; set; }
        public String Abrev_Poblacion { get; set; }
        public String Cod_AreaTelPoblacion { get; set; }
    }


    public class AtributosModels
    {

        public Int32 Id_AtributosSYE { get; set; }
        public string Id_CategoriaArt { get; set; }
        public string CategoriaArt { get; set; }
        public decimal Precio_Fact { get; set; }
        public string Seguro_Trans { get; set; }
        public decimal Porc_VS_Costo_Fact { get; set; }
        public decimal Porc_adic_por_empaque { get; set; }
        public string Usuario { get; set; }
        public DateTime Fec_Creacion { get; set; }
        public string Fec_Movto { get; set; }
        public string Time_Movto { get; set; }
        public string BitActivo { get; set; }
        public string CategoriaArt2 { get; set; }
    }



    public class codigoPostalModels
    {

        public string d_codigo { get; set; }
        public string d_asenta { get; set; }
        public string d_tipo_asenta { get; set; }
        public string D_mnpio { get; set; }
        public string d_estado { get; set; }
        public string d_ciudad { get; set; }
        public string d_CP { get; set; }
        public string c_estado { get; set; }
        public string c_oficina { get; set; }
        public string c_CP { get; set; }
        public string c_tipo_asenta { get; set; }
        public string c_mnpio { get; set; }
        public string id_asenta_cpcons { get; set; }
        public string d_zona { get; set; }
        public string c_cve_ciudad { get; set; }



    }

    public class AlmacenCmb
    {

        public int IdAlmacen { get; set; }
        public string NombreAlmacen { get; set; }

    }

    public class EmpleadoModels
    {

        public string Num_Empl { get; set; }
        public string Ids_Num_UN { get; set; }
        public string Id_Num_DptoNomina { get; set; }
        public string Id_Num_PuestoNomina { get; set; }
        public string Desc_PuestoNomina { get; set; }
        public string Apellido_Paterno { get; set; }
        public string Apellido_Materno { get; set; }
        public string Nombre { get; set; }
        public string FecX_Ingreso { get; set; }
        public string FecX_Nacimiento { get; set; }
        public string Bit_Activo { get; set; }

    }


    /// <summary>

    /// Catalog Controller

    /// </summary>
    [Authorize]
    public class CatalogoController : Controller

    {
        public string draw = "";
        public string start = "";
        public string length = "";
        public string sortColumn = "";
        public string sortColumnDir = "";
        public string searchValue = "";
        public int pageSize, skip, recordsTotal;

        string UrlApi = System.Configuration.ConfigurationManager.AppSettings["urlApi"].ToString();




        /// <summary>
        /// View Servicios
        /// </summary>

        public ActionResult Servicios()

        {

            return View("Servicios/Index");

        }



        /// <summary>
        /// View Servidores
        /// </summary>
        public ActionResult Servidores()

        {

            return View("Servidores/Index");

        }


        /// <summary>
        /// View Logs
        /// </summary>
        public ActionResult Logs()

        {

            return View("Logs/Index");

        }

        /// <summary>
        /// View CPanel
        /// </summary>
        public ActionResult CPanel()

        {

            return View("CPanel/Index");

        }



        #region Surtido



        #region Surtidores

        public ActionResult Surtidores()

        {

            return View("Surtidores/Index");



        }













        [HttpGet]

        public async Task<JsonResult> GetSurtidores()

        {



            try

            {



                string apiUrl = string.Format("{0}api/Oredenes/GetSupplier", UrlApi);





                DataSet ds = DALServicesM.GetSuppliers();



                List<SupplierModel> listC = ConvertTo<SupplierModel>(ds.Tables[0]);



                var result = new { Success = true, json = listC };

                return Json(result, JsonRequestBehavior.AllowGet);



                //using (HttpClient client = new HttpClient())

                //{

                //    client.BaseAddress = new Uri(apiUrl);

                //    client.DefaultRequestHeaders.Accept.Clear();

                //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));



                //    HttpResponseMessage response = await client.GetAsync(apiUrl);

                //    if (response.IsSuccessStatusCode)

                //    {

                //        var data = await response.Content.ReadAsStringAsync();

                //        // var jsonResult = JsonConvert.DeserializeObject(data).ToString();



                //        var resp = JsonConvert.DeserializeObject<List<SupplierModel>>(data);





                //        DataSet ds = DALServicesM.GetCarriers();



                //        List<CarrierModel> listC = ConvertTo<CarrierModel>(ds.Tables[0]);



                //        var result = new { Success = true, json = listC };

                //        return Json(result, JsonRequestBehavior.AllowGet);

                //    }

                //    else //web api sent error response 

                //    {

                //        //log response status here..



                //        var result = new { Success = false, Message = response.StatusCode };

                //        return Json(result, JsonRequestBehavior.AllowGet);



                //    }



                //}



            }

            catch (Exception ex)

            {

                var result = new { Success = false, Message = ex.Message };

                return Json(result, JsonRequestBehavior.AllowGet);



            }

        }





        public async Task<ActionResult> AddSurtidores(string num, string name, string un)

        {



            try

            {

                DALServicesM.AddSupplier(num, name, Session["Id_Num_UN"].ToString());



                var result = new { Success = true, Message = "Alta exitosa" };



                return Json(result, JsonRequestBehavior.AllowGet);



            }

            catch (Exception x)

            {

                var result = new { Success = false, Message = x.Message };



                return Json(result, JsonRequestBehavior.AllowGet);



            }

        }



        public ActionResult EditSurtidores(string num, string name, string un, string status, string Id_Num_Empleado)

        {
            try
            {
                DALServicesM.EditSupplier(num, name, Session["Id_Num_UN"].ToString(), status, Id_Num_Empleado);

                var result = new { Success = true, Message = "Alta exitosa" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            catch (Exception x)

            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }



        public async Task<ActionResult> DeleteSurtidores(string num)

        {



            try

            {

                DALServicesM.DeleteSupplier(num);



                var result = new { Success = true, Message = "Eliminacion exitosa" };



                return Json(result, JsonRequestBehavior.AllowGet);



            }

            catch (Exception x)

            {

                var result = new { Success = false, Message = x.Message };



                return Json(result, JsonRequestBehavior.AllowGet);



            }

        }



        public async Task<JsonResult> GetSurtidor(string num)

        {



            try

            {

                DataSet d = DALServicesM.GetSupplier(num);



                var c = new ServicesManagement.Web.Models.OMSModels.SupplierModel();



                if (!Soriana.FWK.FmkTools.DatosDB.IsNullOrEmptyDataSet(d))

                {

                    if (!Soriana.FWK.FmkTools.DatosDB.IsNullOrEmptyDatatable(d.Tables[0]))

                    {

                        foreach (DataRow r in d.Tables[0].Rows)

                        {



                            c.Name = r["Name"].ToString();

                            c.Id_Num_UN = Convert.ToInt32(r["Id_Num_UN"]);

                            c.Id_Num_Empleado = r["Id_Num_Empleado"].ToString();

                        }

                    }



                }





                var result = new { Success = true, json = c };



                return Json(result, JsonRequestBehavior.AllowGet);



            }

            catch (Exception x)

            {

                var result = new { Success = false, Message = x.Message };



                return Json(result, JsonRequestBehavior.AllowGet);



            }

        }

        //api_GetEmpleado
        public async Task<JsonResult> GetSurtidorApi(string num)
        {
            try
            {


                string json2 = string.Empty;
                JavaScriptSerializer js = new JavaScriptSerializer();
                //json2 = js.Serialize(o);
                js = null;
                json2 = "";// JsonConvert.SerializeObject(o);
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Soriana.FWK.FmkTools.RestResponse r = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.GET, System.Configuration.ConfigurationSettings.AppSettings["api_GetEmpleado"] + num, "", "");



                List<EmpleadoModels> list = JsonConvert.DeserializeObject<List<EmpleadoModels>>(r.message);


                var result = new { Success = true, json = list[0].Nombre + " " + list[0].Apellido_Paterno + " " + list[0].Apellido_Materno };
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };

                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }


        // [HttpGet]

        //public async Task<JsonResult> AddSurtidores(string pId_Supplier,string pName,string pId_Num_UN,string pId_Num_Empleado)

        //{



        //    try

        //    {



        //        var data = new SupplierModel

        //        {

        //            Id_Supplier = 1

        //            ,

        //            Name = "JUAN PEREZ"

        //            ,

        //            Id_Num_UN = 24

        //            ,

        //            Fec_Movto = DateTime.Now

        //            ,

        //            Activo = true

        //            ,

        //            Id_Num_Empleado = "908909809"

        //        };



        //        string apiUrl = string.Format("{0}api/Oredenes/AddSupplier", UrlApi);





        //        using (HttpClient client = new HttpClient())

        //        {

        //            client.BaseAddress = new Uri(apiUrl);

        //            client.DefaultRequestHeaders.Accept.Clear();

        //            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));







        //            HttpResponseMessage response = await client.PostAsync(apiUrl);

        //            if (response.IsSuccessStatusCode)

        //            {

        //                var data = await response.Content.ReadAsStringAsync();

        //                // var jsonResult = JsonConvert.DeserializeObject(data).ToString();



        //                var resp = JsonConvert.DeserializeObject<List<SupplierModel>>(data);

        //                var result = new { Success = true, json = resp };

        //                return Json(result, JsonRequestBehavior.AllowGet);

        //            }

        //            else //web api sent error response 

        //            {

        //                //log response status here..



        //                var result = new { Success = false, Message = response.StatusCode };

        //                return Json(result, JsonRequestBehavior.AllowGet);



        //            }



        //        }



        //    }

        //    catch (Exception ex)

        //    {

        //        var result = new { Success = false, Message = ex.Message };

        //        return Json(result, JsonRequestBehavior.AllowGet);



        //    }

        //}

        #endregion

        #region Tiendas

        public ActionResult Tiendas()
        {
            return View("Tiendas/Index");
        }

        [HttpGet]
        public async Task<JsonResult> GetTiendas()
        {
            try
            {
                string apiUrl = string.Format("{0}api/Oredenes/GetCarrier", UrlApi);
                DataSet ds = DALServicesM.GetTmsTiendas();
                List<TiendaModel> listC = ConvertTo<TiendaModel>(ds.Tables[0]);
                var result = new { Success = true, json = listC };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region Transportistas

        public ActionResult Transportistas()

        {

            return View("Transportistas/Index");

        }

        [HttpGet]

        public async Task<JsonResult> GetTransportistas()

        {



            try

            {



                string apiUrl = string.Format("{0}api/Oredenes/GetCarrier", UrlApi);



                DataSet ds = DALServicesM.GetCarriers();



                // List<CarrierModel> listC = ConvertTo<CarrierModel>(ds.Tables[0]);
                List<CarrierModelTMS> listC = ConvertTo<CarrierModelTMS>(ds.Tables[0]);

                var result = new { Success = true, json = listC };

                return Json(result, JsonRequestBehavior.AllowGet);



                //using (HttpClient client = new HttpClient())

                //{

                //    client.BaseAddress = new Uri(apiUrl);

                //    client.DefaultRequestHeaders.Accept.Clear();

                //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));



                //    HttpResponseMessage response = await client.GetAsync(apiUrl);

                //    if (response.IsSuccessStatusCode)

                //    {

                //        var data = await response.Content.ReadAsStringAsync();

                //        // var jsonResult = JsonConvert.DeserializeObject(data).ToString();

                //        var resp = JsonConvert.DeserializeObject<List<CarrierModel>>(data);



                //        var result = new { Success = true, json = resp };

                //        return Json(result, JsonRequestBehavior.AllowGet);

                //    }

                //    else //web api sent error response 

                //    {

                //        //log response status here..



                //        var result = new { Success = false, Message = response.StatusCode };

                //        return Json(result, JsonRequestBehavior.AllowGet);



                //    }



                //}



            }

            catch (Exception ex)

            {

                var result = new { Success = false, Message = ex.Message };

                return Json(result, JsonRequestBehavior.AllowGet);



            }

        }



        [HttpGet]
        public async Task<JsonResult> GetUnidadesNegocios()
        {
            try
            {
                string apiUrl = string.Format("{0}api/Oredenes/GetCarrier", UrlApi);
                DataSet ds = DALServicesM.GetTmsUN();
                List<UnidadNegocioModel> listC = ConvertTo<UnidadNegocioModel>(ds.Tables[0]);
                var result = new { Success = true, json = listC };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        public async Task<ActionResult> AddTransportistas(string num, string name, string un)

        {



            try

            {

                DALServicesM.AddCarriers(num, name, Session["Id_Num_UN"].ToString());



                var result = new { Success = true, Message = "Alta exitosa" };



                return Json(result, JsonRequestBehavior.AllowGet);



            }

            catch (Exception x)

            {

                var result = new { Success = false, Message = x.Message };



                return Json(result, JsonRequestBehavior.AllowGet);



            }

        }

        // @IdTransportista int
        // ,@RazonSocial varchar(60)
        // ,@TarifaFija bit
        // ,@CostoTarifaFija decimal(10,2)
        // ,@Prioridad int
        // ,@NivelServicio decimal(6,2)
        // ,@FactorPaqueteria decimal(6,2)
        // ,@PorcAdicPaquete decimal(6,2)
        // ,@IdTipoAlmacen int
        // ,@Paqueteria bit
        // ,@BigTicket bit
        // ,@ServicioEstandar bit
        // ,@ServicioExpress bit
        // ,@UsuarioCreacion varchar(20)
        [HttpPost]
        public async Task<ActionResult> AddTiendas(int IdNumUM, int IdTipoAlmacen, bool Paqueteria, bool BigTicket, bool ServicioEstandar, bool ServicioExpress, string UsuarioCreacion)

        {
            try
            {
                DALServicesM.AddTiendas(IdNumUM, IdTipoAlmacen, Paqueteria, BigTicket, ServicioEstandar, ServicioExpress, UsuarioCreacion);

                var result = new { Success = true, Message = "Alta exitosa" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddTransportistas(long IdTransportista, bool TarifaFija,
            decimal CostoTarifaFija, int Prioridad, decimal NivelServicio, decimal FactorPaqueteria, decimal PorcAdicPaquete,
            int IdTipoAlmacen, bool Paqueteria, bool BigTicket, bool ServicioEstandar, bool ServicioExpress, string UsuarioCreacion)

        {
            try

            {

                DALServicesM.AddCarriers(IdTransportista, TarifaFija, CostoTarifaFija, Prioridad, NivelServicio,
                    FactorPaqueteria, PorcAdicPaquete, IdTipoAlmacen, Paqueteria, BigTicket, ServicioEstandar, ServicioExpress, UsuarioCreacion);



                var result = new { Success = true, Message = "Alta exitosa" };



                return Json(result, JsonRequestBehavior.AllowGet);



            }

            catch (Exception x)

            {

                var result = new { Success = false, Message = x.Message };



                return Json(result, JsonRequestBehavior.AllowGet);



            }

        }



        public async Task<ActionResult> EditTransportistas(string num, string name, string un, string status)

        {



            try

            {

                DALServicesM.EditCarriers(num, name, Session["Id_Num_UN"].ToString(), status);



                var result = new { Success = true, Message = "Alta exitosa" };



                return Json(result, JsonRequestBehavior.AllowGet);



            }

            catch (Exception x)

            {

                var result = new { Success = false, Message = x.Message };



                return Json(result, JsonRequestBehavior.AllowGet);



            }

        }



        public async Task<ActionResult> DeleteTransportistas(string num)

        {



            try

            {

                DALServicesM.DeleteCarriers(num);



                var result = new { Success = true, Message = "Eliminacion exitosa" };



                return Json(result, JsonRequestBehavior.AllowGet);



            }

            catch (Exception x)

            {

                var result = new { Success = false, Message = x.Message };



                return Json(result, JsonRequestBehavior.AllowGet);



            }

        }



        public async Task<JsonResult> GetTransportista(string num)

        {



            try

            {

                DataSet d = DALServicesM.GetCarrier(num);



                ServicesManagement.Web.Models.OMSModels.CarrierModel c = new ServicesManagement.Web.Models.OMSModels.CarrierModel();



                if (!Soriana.FWK.FmkTools.DatosDB.IsNullOrEmptyDataSet(d))

                {

                    if (!Soriana.FWK.FmkTools.DatosDB.IsNullOrEmptyDatatable(d.Tables[0]))

                    {

                        foreach (DataRow r in d.Tables[0].Rows)

                        {



                            c.Name = r["Name"].ToString();

                            c.Id_Num_UN = Convert.ToInt32(r["Id_Num_UN"]);

                            c.Id_Num_Empleado = r["Id_Num_Empleado"].ToString();

                        }

                    }



                }





                var result = new { Success = true, json = c };



                return Json(result, JsonRequestBehavior.AllowGet);



            }

            catch (Exception x)

            {

                var result = new { Success = false, Message = x.Message };



                return Json(result, JsonRequestBehavior.AllowGet);



            }

        }

        public async Task<JsonResult> GetTransportistas(int IdTransportista)
        {
            try
            {
                DataSet ds = DALServicesM.GetCarrier(IdTransportista);

                //ServicesManagement.Web.Models.OMSModels.CarrierModel c = new ServicesManagement.Web.Models.OMSModels.CarrierModel();
                //if (!Soriana.FWK.FmkTools.DatosDB.IsNullOrEmptyDataSet(d))
                //{
                //    if (!Soriana.FWK.FmkTools.DatosDB.IsNullOrEmptyDatatable(d.Tables[0]))
                //    {
                //        foreach (DataRow r in d.Tables[0].Rows)
                //        {
                //            c.Name = r["Name"].ToString();
                //            c.Id_Num_UN = Convert.ToInt32(r["Id_Num_UN"]);
                //            c.Id_Num_Empleado = r["Id_Num_Empleado"].ToString();
                //        }
                //    }
                //}
                //var result = new { Success = true, json = c };
                //return Json(result, JsonRequestBehavior.AllowGet);

                List<CarrierModel> listC = ConvertTo<CarrierModel>(ds.Tables[0]);
                var result = new { Success = true, json = listC };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }





        #endregion



        #endregion

        #region CodigosPostales

        public DataSet GetAlmacenCP()
        {

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }

            Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

            System.Collections.Hashtable parametros = new System.Collections.Hashtable();
            //parametros.Add("@idSupplierWH", IdProv);

            return Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "tms.upCorpTms_Cns_SuppliersWarehouses", false, parametros);



        }

        [HttpPost]
        public ActionResult GetCodigos()
        {
            try
            {
                List<CodigosPostales_Por_Almacen> lst = new List<CodigosPostales_Por_Almacen>();

                //logistica datatable
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault().ToLower();

                var ids = "0";
                if (Session["IdsBusqueda"] != null)
                    ids = Session["IdsBusqueda"].ToString();


                #region Se Obtienen Filtros Por Columna
                var Id_CP = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault().ToLower();
                var idSupplierWH = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault().ToLower();
                var Latitud = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault().ToLower();
                var Longitud = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault().ToLower();
                var CP = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault().ToLower();
                var Usuario_Creation = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault().ToLower();
                var Fecha_Creacion = Request.Form.GetValues("columns[6][search][value]").FirstOrDefault().ToLower();
                var Fecha_Modificacion = Request.Form.GetValues("columns[7][search][value]").FirstOrDefault().ToLower();
                var BitActivo = Request.Form.GetValues("columns[8][search][value]").FirstOrDefault().ToLower();




                #endregion

                pageSize = length != null ? Convert.ToInt32(length) : 0;
                skip = start != null ? Convert.ToInt32(start) : 0;
                recordsTotal = 0;

                IQueryable<CodigosPostales_Por_Almacen> query = from row in DALCatalogo.up_CorpTMS_sel_CodigosPostales_Por_Almacen(ids).Tables[0].AsEnumerable().AsQueryable()
                                                                select new CodigosPostales_Por_Almacen()
                                                                {
                                                                    Id_CP = (row["Id_CP"].ToString()),
                                                                    IdOwner = (row["idOwner"].ToString()),
                                                                    IdSupplierWH = row["idSupplierWH"].ToString(),
                                                                    IdSupplierWHCode = (row["idSupplierWHCode"].ToString()),
                                                                    Latitud = row["Latitud"].ToString(),
                                                                    Longitud = row["Longitud"].ToString(),
                                                                    CP = row["CP"].ToString(),
                                                                    Usuario_Creation = row["Usuario_Creation"].ToString(),
                                                                    Fecha_Creacion = row["Fecha_Creacion"].ToString(),
                                                                    BitActivo = row["BitActivo"].ToString(),
                                                                    Fecha_Modificacion = row["Fecha_Modificacion"].ToString(),
                                                                    Usuario_Modificacion = row["Usuario_Modificacion"].ToString()
                                                                };




                if (searchValue != "")
                    query = query.Where(d => d.Id_CP.ToString().ToLower().Contains(searchValue)
                    || d.IdSupplierWH.ToLower().Contains(searchValue)
                    || d.Latitud.ToString().ToLower().Contains(searchValue)
                    || d.Longitud.ToLower().Contains(searchValue)
                    || d.CP.ToLower().Contains(searchValue)
                    || d.Usuario_Creation.ToLower().Contains(searchValue)
                    || d.Fecha_Creacion.ToLower().Contains(searchValue)
                    || d.Fecha_Modificacion.ToLower().Contains(searchValue)
                    || d.BitActivo.ToLower().Contains(searchValue)
                    );


                //Filter By Columns
                #region Filter By Columns
                if (!string.IsNullOrEmpty(Id_CP))
                {
                    query = query.Where(a => a.Id_CP.ToString().ToLower().Contains(Id_CP));
                }
                if (!string.IsNullOrEmpty(idSupplierWH))
                {
                    query = query.Where(a => a.IdSupplierWH.ToLower().Contains(idSupplierWH));
                }

                if (!string.IsNullOrEmpty(Latitud))
                {
                    query = query.Where(a => a.Latitud.ToString().ToLower().Contains(Latitud));
                }
                if (!string.IsNullOrEmpty(Longitud))
                {
                    query = query.Where(a => a.Longitud.ToLower().Contains(Longitud));
                }

                if (!string.IsNullOrEmpty(CP))
                {
                    query = query.Where(a => a.CP.ToLower().Contains(CP));
                }

                if (!string.IsNullOrEmpty(Usuario_Creation))
                {
                    query = query.Where(a => a.Usuario_Creation.ToLower().Contains(Usuario_Creation));
                }

                if (!string.IsNullOrEmpty(Fecha_Creacion))
                {
                    query = query.Where(a => a.Fecha_Creacion.ToLower().Contains(Fecha_Creacion));
                }

                if (!string.IsNullOrEmpty(Fecha_Modificacion))
                {
                    query = query.Where(a => a.Fecha_Modificacion.ToLower().Contains(Fecha_Modificacion));
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
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult Buscar(string ids)
        {
            try
            {
                Session["IdsBusqueda"] = ids;
                return Json(new { Success = true });
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public FileResult ExcelCodigos(string Id_CP
    , string idSupplierWH, string Latitud, string Longitud, string CP
    , string Usuario_Creation, string Fecha_Creacion
    , string Fecha_Modificacion, string BitActivo, string searchValue)

        {




            List<CodigosPostales_Por_Almacen> lst = new List<CodigosPostales_Por_Almacen>();

            IQueryable<CodigosPostales_Por_Almacen> query = from row in DALCatalogo.up_CorpTMS_sel_CodigosPostales_Por_Almacen(null).Tables[0].AsEnumerable().AsQueryable()
                                                            select new CodigosPostales_Por_Almacen()
                                                            {
                                                                Id_CP = (row["Id_CP"].ToString()),
                                                                IdOwner = (row["idOwner"].ToString()),
                                                                IdSupplierWH = row["idSupplierWH"].ToString(),
                                                                IdSupplierWHCode = (row["idSupplierWHCode"].ToString()),
                                                                Latitud = row["Latitud"].ToString(),
                                                                Longitud = row["Longitud"].ToString(),
                                                                CP = row["CP"].ToString(),
                                                                Usuario_Creation = row["Usuario_Creation"].ToString(),
                                                                Fecha_Creacion = row["Fecha_Creacion"].ToString(),
                                                                BitActivo = row["BitActivo"].ToString(),
                                                                Fecha_Modificacion = row["Fecha_Modificacion"].ToString(),
                                                                Usuario_Modificacion = row["Usuario_Modificacion"].ToString()
                                                            };




            if (searchValue != "")
                query = query.Where(dd => dd.Id_CP.ToString().ToLower().Contains(searchValue)
                || dd.IdSupplierWH.ToLower().Contains(searchValue)
                || dd.Latitud.ToString().ToLower().Contains(searchValue)
                || dd.Longitud.ToLower().Contains(searchValue)
                || dd.CP.ToLower().Contains(searchValue)
                || dd.Usuario_Creation.ToLower().Contains(searchValue)
                || dd.Fecha_Creacion.ToLower().Contains(searchValue)
                || dd.Fecha_Modificacion.ToLower().Contains(searchValue)
                || dd.BitActivo.ToLower().Contains(searchValue)
                );


            //Filter By Columns
            #region Filter By Columns
            if (!string.IsNullOrEmpty(Id_CP))
            {
                query = query.Where(a => a.Id_CP.ToString().ToLower().Contains(Id_CP));
            }
            if (!string.IsNullOrEmpty(idSupplierWH))
            {
                query = query.Where(a => a.IdSupplierWH.ToLower().Contains(idSupplierWH));
            }

            if (!string.IsNullOrEmpty(Latitud))
            {
                query = query.Where(a => a.Latitud.ToString().ToLower().Contains(Latitud));
            }
            if (!string.IsNullOrEmpty(Longitud))
            {
                query = query.Where(a => a.Longitud.ToLower().Contains(Longitud));
            }

            if (!string.IsNullOrEmpty(CP))
            {
                query = query.Where(a => a.CP.ToLower().Contains(CP));
            }

            if (!string.IsNullOrEmpty(Usuario_Creation))
            {
                query = query.Where(a => a.Usuario_Creation.ToLower().Contains(Usuario_Creation));
            }

            if (!string.IsNullOrEmpty(Fecha_Creacion))
            {
                query = query.Where(a => a.Fecha_Creacion.ToLower().Contains(Fecha_Creacion));
            }

            if (!string.IsNullOrEmpty(Fecha_Modificacion))
            {
                query = query.Where(a => a.Fecha_Modificacion.ToLower().Contains(Fecha_Modificacion));
            }

            if (!string.IsNullOrEmpty(BitActivo))
            {
                query = query.Where(a => a.BitActivo.ToLower().Contains(BitActivo));
            }

            #endregion

            recordsTotal = query.Count();

            lst = query.Take(recordsTotal).ToList();

            var d = new DataSet();

            string nombreArchivo = "CodigosPostales";

            //Excel to create an object file

            //NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            IWorkbook book = new XSSFWorkbook();

            //Add a sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");


            //Here you can set a variety of styles seemingly font color backgrounds, but not very convenient, there is not set
            //Sheet1 head to add the title of the first row
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);


            row1.CreateCell(0).SetCellValue("Id");
            row1.CreateCell(1).SetCellValue("Almacen");
            row1.CreateCell(2).SetCellValue("Latitud");
            row1.CreateCell(3).SetCellValue("Longitud");
            row1.CreateCell(4).SetCellValue("CP");
            row1.CreateCell(5).SetCellValue("Usuario Creacion");
            row1.CreateCell(6).SetCellValue("Fecha Creacion");
            row1.CreateCell(7).SetCellValue("Fecha Modificación");
            row1.CreateCell(8).SetCellValue("BitActivo");


            //                                                
            //The data is written progressively sheet1 each row

            for (int i = 0; i < lst.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(lst[i].Id_CP.ToString());
                rowtemp.CreateCell(1).SetCellValue(lst[i].IdSupplierWH.ToString());
                rowtemp.CreateCell(2).SetCellValue(lst[i].Latitud.ToString());
                rowtemp.CreateCell(3).SetCellValue(lst[i].Longitud.ToString());
                rowtemp.CreateCell(4).SetCellValue(lst[i].CP.ToString());
                rowtemp.CreateCell(5).SetCellValue(lst[i].Usuario_Creation.ToString());
                rowtemp.CreateCell(6).SetCellValue(lst[i].Fecha_Creacion.ToString());
                rowtemp.CreateCell(7).SetCellValue(lst[i].Fecha_Modificacion.ToString());
                rowtemp.CreateCell(8).SetCellValue(lst[i].BitActivo.ToString());


            }



            //  Write to the client 

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            book.Write(ms);

            //ms.Seek(0, SeekOrigin.Begin);

            DateTime dt = DateTime.Now;

            string dateTime = dt.ToString("yyyyMMddHHmmssfff");

            string fileName = nombreArchivo + "_" + dateTime + ".xlsx";

            //return File(ms, "application/vnd.ms-excel", fileName);
            return File(ms.ToArray(), "application/application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

        }

        [HttpPost]
        public ActionResult ImportFileCodigos(HttpPostedFileBase importFile)
        {
            if (importFile == null) return Json(new { Status = 0, Message = "No File Selected" });

            try
            {

                DALCatalogo.upCorpTms_Ins_AlmacenesCodigos(GetDataFromFileCodigos(importFile.InputStream).ToDataTable(), User.Identity.Name);
                return Json(new { Status = 1, Message = "File Imported Successfully " });

            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, Message = ex.Message });
            }
        }

        private List<AlmacenesCPTableType> GetDataFromFileCodigos(Stream stream)
        {
            var List = new List<AlmacenesCPTableType>();
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
                            List.Add(new AlmacenesCPTableType()
                            {
                                idOwner = int.Parse(objDataRow[0].ToString()),
                                idSupplierWH = int.Parse(objDataRow[1].ToString()),
                                idSupplierWHCode = int.Parse(objDataRow[2].ToString()),
                                CP = int.Parse(objDataRow[3].ToString()),
                                IdTipoLogistica = int.Parse(objDataRow[4].ToString()),

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

        public ActionResult CodigosPostales()

        {
            Session["listaAL"] = GetAlmacenCP();
            //Session["listaCPS"] = GeListaCP();

            Session["listaEstados"] = GetEstados();

            Session["lstAlmacenesConCP"] = GetAlmacenesConCP();
            Session["IdsBusqueda"] = null;

            return View("CodigosPostales/Index");
        }

        public DataSet GetAlmacenesConCP()
        {
            DataSet ds = new DataSet();

            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("tms.up_CorpTMS_sel_Almacenes_Con_CodigosPostales", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                            dataAdapter.Fill(ds);
                    }
                }
                return ds;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            return ds;

        }
        public DataSet GetEstados()
        {

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }

            Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

            System.Collections.Hashtable parametros = new System.Collections.Hashtable();
            //parametros.Add("@idSupplierWH", IdProv);

            return Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "up_Corp_sel_Estados", false, parametros);



        }

        public ActionResult GetPoblados(string estado)
        {

            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }

            Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

            System.Collections.Hashtable parametros = new System.Collections.Hashtable();
            //parametros.Add("@Id_Num_Estado", estado);
            parametros.Add("@Nom_Estado", estado);

            ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "up_Corp_sel_Municipio", false, parametros);

            List<Poblacion_dModel> listC = ConvertTo<Poblacion_dModel>(ds.Tables[0]);

            var result = new { Success = true, json = listC };
            return Json(result, JsonRequestBehavior.AllowGet);

        }



        public DataSet GeListaCP()
        {

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }

            Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

            System.Collections.Hashtable parametros = new System.Collections.Hashtable();
            //parametros.Add("@idSupplierWH", IdProv);

            return Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "tms.up_CorpTMS_sel_CodigosPostales_Por_Almacen", false, parametros);



        }

        [HttpPost]
        public JsonResult InsCPS(string codigos, string idOwner, string idSupplierWH, string idSupplierWHCode, string lat = "", string lon = "")
        {
            try
            {


                try
                {
                    Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

                    System.Collections.Hashtable parametros = new System.Collections.Hashtable();

                    if (codigos.Trim().Length > 0)
                        foreach (string c in codigos.Split('\n'))
                        {
                            parametros = new System.Collections.Hashtable();


                            parametros.Add("@idOwner", idOwner);
                            parametros.Add("@idSupplierWH", idSupplierWH);
                            parametros.Add("@idSupplierWHCode", idSupplierWHCode);
                            parametros.Add("@Latitud", lat);
                            parametros.Add("@Longitud", lon);
                            parametros.Add("@CP", c);
                            parametros.Add("@Usuario_Creation", User.Identity.Name);

                            Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "tms.up_CorpTMS_ins_CodigosPostales_Por_Almacen", false, parametros);


                        }


                    //if (string.IsNullOrEmpty(IdTipoLogistica) || IdTipoLogistica.Equals("0"))
                    //{
                    //    parametros.Add("@TipoLogistica", TipoLogistica);
                    //    parametros.Add("@MinPesoVolumetrico", MinPesoVolumetrico);
                    //    parametros.Add("@MaxPesoVolumetrico", MaxPesoVolumetrico);
                    //    parametros.Add("@MaxCosto", MaxCosto);
                    //    parametros.Add("@TipoArticulo", TipoArticulo);
                    //    parametros.Add("@UsuarioCreacion", "sysAdmin");

                    //    Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "up_CorpTMS_ins_TipoLogistica", false, parametros);
                    //}
                    //else
                    //{

                    //    parametros.Add("@IdTipoLogistica", IdTipoLogistica);
                    //    parametros.Add("@TipoLogistica", TipoLogistica);
                    //    parametros.Add("@MinPesoVolumetrico", MinPesoVolumetrico);
                    //    parametros.Add("@MaxPesoVolumetrico", MaxPesoVolumetrico);
                    //    parametros.Add("@MaxCosto", MaxCosto);
                    //    parametros.Add("@TipoArticulo", TipoArticulo);
                    //    parametros.Add("@UsuarioUltModif", "sysAdmin2");
                    //    parametros.Add("@BitActivo", estatus.Equals("0") ? 1 : 0);

                    //    Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "up_CorpTMS_upd_TipoLogistica", false, parametros);

                    //}




                    //return ds;
                }
                catch (SqlException ex)
                {

                    throw ex;
                }
                catch (System.Exception ex)
                {

                    throw ex;
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



        public ActionResult GetCps(string estado, string[] municipios)
        {

            try
            {

                List<EstadosMuniCpsTableType> edosMun = new List<EstadosMuniCpsTableType>();

                if (municipios == null)
                {
                    edosMun.Add(new EstadosMuniCpsTableType { Region1Name = estado, Region2Name = string.Empty });
                }
                else
                {
                    foreach (var item in municipios)
                    {
                        edosMun.Add(new EstadosMuniCpsTableType { Region1Name = estado, Region2Name = item });
                    }
                }

                var dt = edosMun.ToDataTable();

                var list = DataTableToModel.ConvertTo<upCorpTms_Cns_EstadosMuniCodigos>(DALCatalogo.upCorpTms_Cns_EstadosMuniCodigos(dt).Tables[0]);

                var result = new { Success = true ,list = list};
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }


        }


        #endregion


        public List<T> ConvertTo<T>(DataTable datatable) where T : new()

        {

            List<T> Temp = new List<T>();

            try

            {

                List<string> columnsNames = new List<string>();

                foreach (DataColumn DataColumn in datatable.Columns)

                    columnsNames.Add(DataColumn.ColumnName);

                Temp = datatable.AsEnumerable().ToList().ConvertAll<T>(row => getObject<T>(row, columnsNames));

                return Temp;

            }

            catch

            {

                return Temp;

            }



        }

        public T getObject<T>(DataRow row, List<string> columnsName) where T : new()

        {

            T obj = new T();

            try

            {

                string columnname = "";

                string value = "";

                PropertyInfo[] Properties;

                Properties = typeof(T).GetProperties();

                foreach (PropertyInfo objProperty in Properties)

                {

                    columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());

                    if (!string.IsNullOrEmpty(columnname))

                    {

                        value = row[columnname].ToString();

                        if (!string.IsNullOrEmpty(value))

                        {

                            if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)

                            {

                                value = row[columnname].ToString().Replace("$", "").Replace(",", "");

                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);

                            }

                            else

                            {

                                value = row[columnname].ToString().Replace("%", "");

                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);

                            }

                        }

                    }

                }

                return obj;

            }

            catch

            {

                return obj;

            }

        }


        #region Pasillos



        public ActionResult Pasillos()

        {

            if (Session["Id_Num_UN"] == null)

            {

                return RedirectToAction("Index", "Ordenes");

            }



            return View("Pasillos/Index");

        }





        public JsonResult GetPasilloUn()

        {

            try

            {

                if (Session["Id_Num_UN"] != null)

                {

                    DataSet d = DALServicesM.GetPasillos(int.Parse(Session["Id_Num_UN"].ToString()));

                    List<PasilloUnModel> listC = new List<PasilloUnModel>();

                    if (!Soriana.FWK.FmkTools.DatosDB.IsNullOrEmptyDataSet(d))

                    {

                        if (!Soriana.FWK.FmkTools.DatosDB.IsNullOrEmptyDatatable(d.Tables[0]))

                        {

                            listC = ConvertTo<PasilloUnModel>(d.Tables[0]);

                        }

                    }

                    var result = new { Success = true, json = listC };

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

                else

                {

                    var result = new { Success = false, json = "OKS" };

                    RedirectToAction("Index", "Ordenes");

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

            }

            catch (Exception x)

            {

                var result = new { Success = false, Message = x.Message };

                return Json(result, JsonRequestBehavior.AllowGet);

            }

        }

        [HttpPost]
        public JsonResult AddPasilloUn()

        {

            try

            {

                if (Session["Id_Num_UN"] != null)

                {

                    DataSet d = DALServicesM.AddPasilloUn(int.Parse(Session["Id_Num_UN"].ToString()));



                    var result = new { Success = true, json = "Ok" };

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

                else

                {

                    var result = new { Success = false, json = "OKS" };

                    RedirectToAction("Index", "Ordenes");

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

            }

            catch (Exception x)

            {

                var result = new { Success = false, Message = x.Message };

                return Json(result, JsonRequestBehavior.AllowGet);

            }

        }

        [HttpPost]
        public JsonResult DellPasilloUn(string Id_Num_UN, string Id_Cnsc_Pasillo)

        {

            try

            {

                if (Session["Id_Num_UN"] != null)

                {

                    DataSet d = DALServicesM.DelPasilloUn(int.Parse(Id_Num_UN), int.Parse(Id_Cnsc_Pasillo));



                    var result = new { Success = true, json = "Ok" };

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

                else

                {

                    var result = new { Success = false, json = "OKS" };

                    RedirectToAction("Index", "Ordenes");

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

            }

            catch (Exception x)

            {

                var result = new { Success = false, Message = x.Message };

                return Json(result, JsonRequestBehavior.AllowGet);

            }

        }

        [HttpPost]
        public JsonResult AddPasilloUnEsp()

        {

            try

            {

                if (Session["Id_Num_UN"] != null)

                {

                    DataSet d = DALServicesM.AddPasilloUnEspecial(int.Parse(Session["Id_Num_UN"].ToString()));



                    var result = new { Success = true, json = "Ok" };

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

                else

                {

                    var result = new { Success = false, json = "OKS" };

                    RedirectToAction("Index", "Ordenes");

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

            }

            catch (Exception x)

            {

                var result = new { Success = false, Message = x.Message };

                return Json(result, JsonRequestBehavior.AllowGet);

            }

        }

        [HttpGet]
        public JsonResult GetPasilloUnReporteMap(string Id_Num_Un, string Id_Cnsc_Pasillo = "0"

            , string Id_Num_Div = "0", string Id_Num_Categ = "0")

        {

            try

            {

                if (Session["Id_Num_UN"] != null)

                {

                    DataSet d = DALServicesM.ReporteMapeoPasillo(int.Parse(Id_Num_Un), int.Parse(Id_Cnsc_Pasillo)

                        , int.Parse(Id_Num_Div), int.Parse(Id_Num_Categ));

                    PasilloUnReporteMapModel ListRpt = new PasilloUnReporteMapModel();

                    List<CategoriaModel> Categoria = new List<CategoriaModel>();

                    List<DivisionModel> Division = new List<DivisionModel>();

                    List<PasilloUnDetalleRep> PasilloDetalle = new List<PasilloUnDetalleRep>();

                    List<PasilloUnRepPasillo> Pasillo = new List<PasilloUnRepPasillo>();



                    if (!Soriana.FWK.FmkTools.DatosDB.IsNullOrEmptyDataSet(d))

                    {

                        if (!Soriana.FWK.FmkTools.DatosDB.IsNullOrEmptyDatatable(d.Tables[0]))

                        {

                            Pasillo = ConvertTo<PasilloUnRepPasillo>(d.Tables[0]);

                            Division = ConvertTo<DivisionModel>(d.Tables[1]);

                            Categoria = ConvertTo<CategoriaModel>(d.Tables[2]);

                            PasilloDetalle = ConvertTo<PasilloUnDetalleRep>(d.Tables[3]);



                        }

                    }

                    ListRpt.Categoria = Categoria;

                    ListRpt.Division = Division;

                    ListRpt.Pasillo = Pasillo;

                    ListRpt.PasilloDetalle = PasilloDetalle;



                    var result = new { Success = true, json = ListRpt };

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

                else

                {

                    var result = new { Success = false, json = "OKS" };

                    RedirectToAction("Index", "Ordenes");

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

            }

            catch (Exception x)

            {

                var result = new { Success = false, Message = x.Message };

                return Json(result, JsonRequestBehavior.AllowGet);

            }

        }

        [HttpPost]
        public JsonResult UpdatePasilloUn(string Id_Num_UN, string Id_Cnsc_Pasillo, string Nom_PasilloTipo, string Num_Orden)

        {

            try

            {

                if (Session["Id_Num_UN"] != null)

                {



                    DataSet d = DALServicesM.UpdPasilloUN(int.Parse(Id_Num_UN), int.Parse(Id_Cnsc_Pasillo), Nom_PasilloTipo, int.Parse(Num_Orden));



                    var result = new { Success = true, json = "Ok" };

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

                else

                {

                    var result = new { Success = false, json = "OKS" };

                    RedirectToAction("Index", "Ordenes");

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

            }

            catch (Exception x)

            {

                var result = new { Success = false, Message = x.Message };

                return Json(result, JsonRequestBehavior.AllowGet);

            }

        }

        [HttpGet]
        public JsonResult PasilloUnEditCateg(string Id_Num_Un, string Id_Cnsc_Pasillo = "0"

    , string Id_Num_Div = "0", string Id_Num_Categ = "0")

        {

            try

            {

                if (Session["Id_Num_UN"] != null)

                {

                    DataSet d = DALServicesM.PasilloUnEditCateg(int.Parse(Id_Num_Un), int.Parse(Id_Cnsc_Pasillo)

                        , int.Parse(Id_Num_Div), int.Parse(Id_Num_Categ));

                    PasilloUnEditCat List = new PasilloUnEditCat();

                    List<CategoriaModel> Categoria = new List<CategoriaModel>();

                    List<DivisionModel> Division = new List<DivisionModel>();

                    List<LineaModel> Linea = new List<LineaModel>();

                    List<PasilloUnCategModel> PasilloUnCateg = new List<PasilloUnCategModel>();

                    List<PasilloUnLinea> PasilloUnLinea = new List<PasilloUnLinea>();



                    if (!Soriana.FWK.FmkTools.DatosDB.IsNullOrEmptyDataSet(d))

                    {

                        if (!Soriana.FWK.FmkTools.DatosDB.IsNullOrEmptyDatatable(d.Tables[0]))

                        {



                            Division = ConvertTo<DivisionModel>(d.Tables[1]);

                            Categoria = ConvertTo<CategoriaModel>(d.Tables[2]);

                            Linea = ConvertTo<LineaModel>(d.Tables[4]);

                            PasilloUnCateg = ConvertTo<PasilloUnCategModel>(d.Tables[3]);







                        }

                    }

                    List.Categoria = Categoria;

                    List.Division = Division;

                    List.Linea = Linea;

                    List.PasilloUnCateg = PasilloUnCateg;



                    var result = new { Success = true, json = List };

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

                else

                {

                    var result = new { Success = false, json = "OKS" };

                    RedirectToAction("Index", "Ordenes");

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

            }

            catch (Exception x)

            {

                var result = new { Success = false, Message = x.Message };

                return Json(result, JsonRequestBehavior.AllowGet);

            }

        }

        [HttpGet]
        public JsonResult AddPasilloUnLinea(string Id_Num_UN, string Id_Cnsc_Pasillo, string Id_Num_Lineas)

        {

            try

            {

                if (Session["Id_Num_UN"] != null)

                {





                    string[] lineas = Id_Num_Lineas.Split(',');



                    foreach (string value in lineas)

                    {

                        string Id_Num_Linea = value.Replace("chbEleg-", "");



                        DataSet d = DALServicesM.AddPasilloUN_Linea(int.Parse(Id_Num_UN), int.Parse(Id_Cnsc_Pasillo), int.Parse(Id_Num_Linea));

                    }





                    var result = new { Success = true, json = "Ok" };

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

                else

                {

                    var result = new { Success = false, json = "OKS" };

                    RedirectToAction("Index", "Ordenes");

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

            }

            catch (Exception x)

            {

                var result = new { Success = false, Message = x.Message };

                return Json(result, JsonRequestBehavior.AllowGet);

            }

        }

        [HttpGet]
        public JsonResult DelPasilloUnLinea(string Id_Num_UN, string Id_Cnsc_Pasillo, string Id_Num_Lineas)

        {

            try

            {

                if (Session["Id_Num_UN"] != null)

                {





                    string[] lineas = Id_Num_Lineas.Split(',');



                    foreach (string value in lineas)

                    {

                        string Id_Num_Linea = value.Replace("chbEleg-", "");



                        DataSet d = DALServicesM.DelPasilloUN_Linea(int.Parse(Id_Num_UN), int.Parse(Id_Cnsc_Pasillo), int.Parse(Id_Num_Linea));

                    }





                    var result = new { Success = true, json = "Ok" };

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

                else

                {

                    var result = new { Success = false, json = "OKS" };

                    RedirectToAction("Index", "Ordenes");

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

            }

            catch (Exception x)

            {

                var result = new { Success = false, Message = x.Message };

                return Json(result, JsonRequestBehavior.AllowGet);

            }

        }

        //================================ exporta a excel ============================================

        public FileResult PasilloRptExcel(string Id_Num_Un = "")

        {

            var d = new DataSet();

            string nombreArchivo = string.Empty;



            //Excel to create an object file

            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            //Add a sheet

            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");



            //Here you can set a variety of styles seemingly font color backgrounds, but not very convenient, there is not set



            //Sheet1 head to add the title of the first row

            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);





            if (string.IsNullOrEmpty(Id_Num_Un))

            {

                d = DALServicesM.GetPasilloCapturaAvance();

                nombreArchivo = "PasilloCapturaAvance";



                row1.CreateCell(0).SetCellValue("TIENDA");

                row1.CreateCell(1).SetCellValue("DIVISION");

                row1.CreateCell(2).SetCellValue("LINEAS CATALOGADAS");

                row1.CreateCell(3).SetCellValue("LINEAS DADAS DE ALTA EN EL ORDENAMIENTO DE PASILLOS");

                row1.CreateCell(4).SetCellValue("% AVANCE DE ALTA DE LINEAS");



                //The data is written progressively sheet1 each row

                for (int i = 0; i < d.Tables[0].Rows.Count; i++)

                {

                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);

                    rowtemp.CreateCell(0).SetCellValue(d.Tables[0].Rows[i][0].ToString());

                    rowtemp.CreateCell(0).SetCellValue(d.Tables[0].Rows[i][1].ToString());

                    rowtemp.CreateCell(0).SetCellValue(d.Tables[0].Rows[i][2].ToString());

                    rowtemp.CreateCell(0).SetCellValue(d.Tables[0].Rows[i][3].ToString());

                    rowtemp.CreateCell(0).SetCellValue(d.Tables[0].Rows[i][4].ToString());



                }



            }

            else

            {

                d = DALServicesM.GetPasilloCapturaAvanceUN(int.Parse(Id_Num_Un));

                nombreArchivo = "PasilloCapturaAvanceUN";



                row1.CreateCell(0).SetCellValue("TIENDA");

                row1.CreateCell(1).SetCellValue("DIVISION");

                row1.CreateCell(2).SetCellValue("CATEGORIA");

                row1.CreateCell(3).SetCellValue("NUM DE LINEA");

                row1.CreateCell(4).SetCellValue("NOMBRE DE LA LINEA");

                row1.CreateCell(5).SetCellValue("STATUS DE LA LINEA");

                row1.CreateCell(6).SetCellValue("NOMBRE DEL PASILLO");



                //The data is written progressively sheet1 each row

                for (int i = 0; i < d.Tables[0].Rows.Count; i++)

                {

                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);

                    rowtemp.CreateCell(0).SetCellValue(d.Tables[0].Rows[i][0].ToString());

                    rowtemp.CreateCell(0).SetCellValue(d.Tables[0].Rows[i][1].ToString());

                    rowtemp.CreateCell(0).SetCellValue(d.Tables[0].Rows[i][2].ToString());

                    rowtemp.CreateCell(0).SetCellValue(d.Tables[0].Rows[i][3].ToString());

                    rowtemp.CreateCell(0).SetCellValue(d.Tables[0].Rows[i][4].ToString());

                    rowtemp.CreateCell(0).SetCellValue(d.Tables[0].Rows[i][5].ToString());

                    rowtemp.CreateCell(0).SetCellValue(d.Tables[0].Rows[i][6].ToString());



                }

            }









            //.... N line





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

        #region Pass



        public ActionResult PassCan()



        {

            try

            {

                string pass = string.Empty;

                if (Session["Id_Num_UN"] != null)

                {

                    DataSet d = DALServicesM.GetPassCan(int.Parse(Session["Id_Num_UN"].ToString()));

                    if (!Soriana.FWK.FmkTools.DatosDB.IsNullOrEmptyDatatable(d.Tables[0]))

                    {

                        foreach (DataRow r in d.Tables[0].Rows)

                        {

                            ViewBag.pass = r["nom"].ToString();

                        }

                    }

                }

                else

                {

                    return RedirectToAction("Index", "Ordenes");

                }



                return View("PassCan/Index");

            }

            catch (Exception)

            {

                throw;

            }

        }



        [HttpPost]

        public JsonResult UpdatePassCancel(string PassCan)



        {

            try

            {

                string pass = string.Empty;

                if (Session["Id_Num_UN"] != null)

                {

                    DataSet d = DALServicesM.UpdPassCan(int.Parse(Session["Id_Num_UN"].ToString()), PassCan);

                    var result = new { Success = true, json = "OK" };



                    return Json(result, JsonRequestBehavior.AllowGet);

                }

                else

                {

                    var result = new { Success = false, json = "OKS" };

                    RedirectToAction("Index", "Ordenes");

                    return Json(result, JsonRequestBehavior.AllowGet);

                }

            }



            catch (Exception ex)

            {

                var result = new { Success = false, Message = ex.Message };

                return Json(result, JsonRequestBehavior.AllowGet);

            }



        }






        #endregion

        #region PesoTransportista
        public ActionResult PesoTransportista()
        {
            return View();
        }

        public ActionResult ListTransportista()
        {
            try
            {
                var list = DataTableToModel.ConvertTo<Transportista>(DALCatalogo.Transportista_sUp().Tables[0]);

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetPaqueterias()
        {
            try
            {
                var list = DataTableToModel.ConvertTo<Paqueteria>(DALCatalogo.Paqueteria_sUp().Tables[0]);

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetPaqueteriasId(string Id_paqueteria)
        {
            try
            {
                var list = DataTableToModel.ConvertTo<Paqueteria>(DALCatalogo.PaqueteriaById_sUp(int.Parse(Id_paqueteria)).Tables[0]);

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult DelPaqueteria(string Id_paqueteria)
        {
            try
            {
                DALCatalogo.Paqueteria_dUp(int.Parse(Id_paqueteria));

                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult AddPeso(string Id_Proveedor, string peso, string Id_paqueteria)
        {
            try
            {
                if (!Id_paqueteria.Equals("0"))
                {
                    DALCatalogo.Paqueteria_uUp(int.Parse(Id_Proveedor), decimal.Parse(peso), int.Parse(Id_paqueteria));
                }
                else { DALCatalogo.Paqueteria_iUp(int.Parse(Id_Proveedor), decimal.Parse(peso)); }


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

        #region Gastos

        public ActionResult Gastos()
        {
            return View();
        }

        public ActionResult ListGastos()
        {
            try
            {
                var list = DataTableToModel.ConvertTo<Gastos>(DALCatalogo.Gastos_sUp().Tables[0]);

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult AddGasto(string gasto, int Id_gasto, string activo)
        {
            try
            {
                if (Id_gasto.Equals(0))
                {
                    DALCatalogo.Gastos_iUp(Id_gasto, gasto, activo, User.Identity.Name);
                }
                else { DALCatalogo.Gastos_uUp(gasto, Id_gasto, activo, User.Identity.Name); }


                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetGastosId(string Id_gasto)
        {
            try
            {
                var list = DataTableToModel.ConvertTo<Gastos>(DALCatalogo.GastosById_sUp(int.Parse(Id_gasto)).Tables[0]);

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult DelGastos(string Id_gastos)
        {
            try
            {
                DALCatalogo.Gastos_dUp(int.Parse(Id_gastos));

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


        #region tipoEnvio
        public ActionResult TipoEnvio()
        {
            return View();
        }

        public ActionResult GetTipoEnvio()
        {
            try
            {
                var list = DataTableToModel.ConvertTo<TipoEnvio>(DALCatalogo.TipoEnvio_sUp().Tables[0]);

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult CmbClaseEnvio()
        {
            try
            {
                var list = DataTableToModel.ConvertTo<ClaseEnvio>(DALCatalogo.ClaseEnvioByBit_sUp().Tables[0]);

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }



        public ActionResult GetTipoEnvioId(int IdTipoEnvio)
        {
            try
            {
                var list = DataTableToModel.ConvertTo<TipoEnvio>(DALCatalogo.TipoEnvioById_sUp(IdTipoEnvio).Tables[0]).FirstOrDefault();

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult AddTipoEnvio(string Desc_TipoEnvio, int BitActivo, decimal PesoMinimo, decimal PesoMaximo, int Clase)
        {
            try
            {
                DALCatalogo.TipoEnvio_iUp(Desc_TipoEnvio, Convert.ToBoolean(BitActivo), User.Identity.Name, PesoMinimo, PesoMaximo, Clase);

                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult UpdTipoEnvio(string IdTipoEnvio, string Desc_TipoEnvio, int BitActivo, decimal PesoMinimo, decimal PesoMaximo, int Clase)
        {
            try
            {
                DALCatalogo.TipoEnvio_uUp(int.Parse(IdTipoEnvio), Desc_TipoEnvio, Convert.ToBoolean(BitActivo), User.Identity.Name, PesoMinimo, PesoMaximo, Clase);

                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult DelTipoEnvio(string IdTipoEnvio)
        {
            try
            {
                DALCatalogo.TipoEnvio_dUp(int.Parse(IdTipoEnvio), User.Identity.Name);

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

        #region tipoServicio
        public ActionResult TipoServicio()
        {
            return View();
        }

        public ActionResult GetTipoServicio()
        {
            try
            {
                var list = DataTableToModel.ConvertTo<TipoServicio>(DALCatalogo.TipoServicio_sUp().Tables[0]);

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetTipoServicioId(int IdTipoServicio)
        {
            try
            {
                var list = DataTableToModel.ConvertTo<TipoServicio>(DALCatalogo.TipoServicioById_sUp(IdTipoServicio).Tables[0]).FirstOrDefault();

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult AddTipoServicio(string Desc_TipoServicio, int BitActivo)
        {
            try
            {
                DALCatalogo.TipoServicio_iUp(Desc_TipoServicio, Convert.ToBoolean(BitActivo), User.Identity.Name);

                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult UpdTipoServicio(string IdTipoServicio, string Desc_TipoServicio, int BitActivo)
        {
            try
            {
                DALCatalogo.TipoServicio_uUp(int.Parse(IdTipoServicio), Desc_TipoServicio, Convert.ToBoolean(BitActivo), User.Identity.Name);

                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult DelTipoServicio(string IdTipoServicio)
        {
            try
            {
                DALCatalogo.TipoServicio_dUp(int.Parse(IdTipoServicio), User.Identity.Name);

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

        #region Zonas
        public ActionResult Zonas()
        {
            return View();
        }

        public ActionResult GetZonas()
        {
            try
            {
                var list = DataTableToModel.ConvertTo<Zonas>(DALCatalogo.Zonas_sUp().Tables[0]);

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetZonasId(int IdZona)
        {
            try
            {
                var list = DataTableToModel.ConvertTo<Zonas>(DALCatalogo.ZonasById_sUp(IdZona).Tables[0]).FirstOrDefault();

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult AddZonas(string Desc_Zona, int BitActivo)
        {
            try
            {
                DALCatalogo.Zonas_iUp(Desc_Zona, Convert.ToBoolean(BitActivo), User.Identity.Name);

                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult UpdZonas(string IdZona, string Desc_Zona, int BitActivo)
        {
            try
            {
                DALCatalogo.Zonas_uUp(int.Parse(IdZona), Desc_Zona, Convert.ToBoolean(BitActivo), User.Identity.Name);

                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult DelZonas(string IdZona)
        {
            try
            {
                DALCatalogo.Zonas_dUp(int.Parse(IdZona), User.Identity.Name);

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

        #region Plazas

        public ActionResult Plazas()
        {
            return View();
        }

        public ActionResult ListPlazas()
        {
            try
            {
                var list = DataTableToModel.ConvertTo<Plazas>(DALCatalogo.Plazas_sUp().Tables[0]);

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult ListPlazasbyID(int Id)
        {
            try
            {
                var list = DataTableToModel.ConvertTo<Plazas>(DALCatalogo.Plazas_sUpbyId(Id).Tables[0]);

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult AddPlazas(string Desc_Plaza, string cve_Plaza, string BitActivo)
        {
            try
            {
                string UserCreate = User.Identity.Name;

                DALCatalogo.Plazas_iUp(Desc_Plaza, cve_Plaza, BitActivo, UserCreate);


                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);


            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }



        //public ActionResult DelPlazas(string Id_gastos)
        //{
        //    try
        //    {
        //        DALCatalogo.Gastos_dUp(int.Parse(Id_gastos));

        //        var result = new { Success = true };
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception x)
        //    {
        //        var result = new { Success = false, Message = x.Message };
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }

        //}

        public ActionResult EditPlazas(string idPlaza, string Desc_Plaza, string cve_Plaza, string BitActivo)
        {
            try
            {
                string UserCreate = User.Identity.Name;

                DALCatalogo.Plazas_uUp(idPlaza, Desc_Plaza, cve_Plaza, BitActivo, UserCreate);


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

        #region TipoLogistica
        public ActionResult TipoLogistica()
        {

            Session["listaTL"] = GetTipoLogistica();


            return View();
        }


        public DataSet GetTipoLogistica()
        {

            DataSet ds = new DataSet();

            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("up_CorpTMS_sel_TipoLogistica", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                            dataAdapter.Fill(ds);
                    }
                }
                return ds;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            return ds;

        }


        [HttpPost]
        public async Task<JsonResult> InsTipoLogistica(string TipoLogistica, string MinPesoVolumetrico, string MaxPesoVolumetrico, string MaxCosto, string estatus, string IdTipoLogistica, string tipoCatalago)
        {
            try
            {


                try
                {
                    Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

                    System.Collections.Hashtable parametros = new System.Collections.Hashtable();



                    if (string.IsNullOrEmpty(IdTipoLogistica) || IdTipoLogistica.Equals("0"))
                    {
                        parametros.Add("@TipoLogistica", TipoLogistica);
                        parametros.Add("@MinPesoVolumetrico", MinPesoVolumetrico);
                        parametros.Add("@MaxPesoVolumetrico", MaxPesoVolumetrico);
                        parametros.Add("@MaxCosto", MaxCosto);
                        parametros.Add("@UsuarioCreacion", "sysAdmin");
                        parametros.Add("@TipoCatalogo", tipoCatalago);

                        Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "up_CorpTMS_ins_TipoLogistica", false, parametros);
                    }
                    else
                    {

                        parametros.Add("@IdTipoLogistica", IdTipoLogistica);
                        parametros.Add("@TipoLogistica", TipoLogistica);
                        parametros.Add("@MinPesoVolumetrico", MinPesoVolumetrico);
                        parametros.Add("@MaxPesoVolumetrico", MaxPesoVolumetrico);
                        parametros.Add("@MaxCosto", MaxCosto);
                        parametros.Add("@UsuarioUltModif", "sysAdmin2");
                        parametros.Add("@BitActivo", estatus.Equals("0") ? 0 : 1);
                        parametros.Add("@TipoCatalogo", tipoCatalago);

                        Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "up_CorpTMS_upd_TipoLogistica", false, parametros);

                    }




                    //return ds;
                }
                catch (SqlException ex)
                {

                    throw ex;
                }
                catch (System.Exception ex)
                {

                    throw ex;
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
        public async Task<JsonResult> GetTipoLogistica(string IdTipoLogistica)
        {
            try
            {


                try
                {
                    DataSet ds = (DataSet)Session["listaTL"];


                    string out_IdTipoLogistica = "";
                    string TipoLogistica = "";
                    string MinPesoVolumetrico = "";
                    string MaxPesoVolumetrico = "";
                    string MaxCosto = "";
                    string TipoArticulo = "";
                    string TipoCatalogo = string.Empty;
                    string estatus = "";

                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        if (IdTipoLogistica.Equals(r["IdTipoLogistica"].ToString().Trim()))
                        {
                            out_IdTipoLogistica = r["IdTipoLogistica"].ToString();
                            TipoLogistica = r["TipoLogistica"].ToString();
                            MinPesoVolumetrico = r["MinPesoVolumetrico"].ToString();
                            MaxPesoVolumetrico = r["MaxPesoVolumetrico"].ToString();
                            TipoArticulo = r["TipoArticulo"].ToString();
                            MaxCosto = r["MaxCosto"].ToString();
                            estatus = r["BitActivo"].ToString();
                            TipoCatalogo = r["TipoCatalagos"].ToString();
                            break;
                        }

                    }


                    var result = new
                    {
                        Success = true,
                        Id = out_IdTipoLogistica,
                        tl = TipoLogistica,
                        mp = MinPesoVolumetrico,
                        mxp = MaxPesoVolumetrico,
                        ta = TipoArticulo,
                        mx = MaxCosto,
                        e = estatus,
                        //e = estatus.ToLower().Equals("1") ? 1 : 0, 
                        tipoCat = TipoCatalogo
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);


                    //return ds;
                }
                catch (SqlException ex)
                {

                    throw ex;
                }
                catch (System.Exception ex)
                {

                    throw ex;
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
        #endregion

        #region DatosSAP
        public DataSet GetCategSAP()
        {

            DataSet ds = new DataSet();

            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("up_CorpTMS_cmd_Categ", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                            dataAdapter.Fill(ds);
                    }
                }
                return ds;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            return ds;

        }


        public DataSet GetCategSAP2()
        {

            DataSet ds = new DataSet();

            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV3"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("up_CorpTMS_cmd_Categ", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                            dataAdapter.Fill(ds);
                    }
                }
                return ds;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            return ds;

        }
        #endregion

        #region AtributosSYE
        public ActionResult AtributosSYE()
        {
            Session["listaCatSap"] = GetCategSAP2();
            Session["listaASYE"] = GetAtributosSYE();


            DataSet ds = GetCategSAP2();
            DataSet ds2 = GetAtributosSYE();

            List<AtributosModels> results = (from t1 in ds.Tables[0].AsEnumerable()
                                             join t2 in ds2.Tables[0].AsEnumerable() on (string)t1["Cve_CategSAP"] equals (string)t2["Id_CategoriaArt"]
                                             select new AtributosModels()
                                             {
                                                 Id_AtributosSYE = t2.Field<Int32>("Id_AtributosSYE"),
                                                 Id_CategoriaArt = t2.Field<string>("Id_CategoriaArt"),
                                                 CategoriaArt = t1.Field<string>("Desc_CategSAP"),
                                                 Precio_Fact = t2.Field<decimal>("Precio_Fact"),
                                                 Seguro_Trans = (string)t2["Seguro_Trans"],
                                                 Porc_VS_Costo_Fact = (decimal)t2["Porc_VS_Costo_Fact"],
                                                 Porc_adic_por_empaque = (decimal)t2["Porc_adic_por_empaque"],
                                                 Usuario = (string)t2["Usuario"],
                                                 Fec_Creacion = (DateTime)t2["Fec_Creacion"],
                                                 Fec_Movto = t2["Fec_Movto"] == DBNull.Value ? "" : (string)t2["Fec_Movto"],
                                                 Time_Movto = t2["Time_Movto"] == DBNull.Value ? "00:00:00" : (string)t2["Time_Movto"],
                                                 BitActivo = (string)t2["BitActivo"],
                                                 CategoriaArt2 = t1.Field<string>("Desc_CategSAP2")
                                             }).ToList();

            Session["grid"] = results;

            return View();
        }

        public DataSet GetAtributosSYE()
        {

            DataSet ds = new DataSet();

            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("up_CorpTMS_sel_AtributosSYE", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                            dataAdapter.Fill(ds);
                    }
                }
                return ds;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            return ds;

        }

        [HttpPost]
        public async Task<JsonResult> InsAtributosSYE(string Id_AtributosSYE, string Id_CategoriaArt
                                                    , string Precio_Fact
                                                    , string Seguro_Trans
                                                    , string Porc_VS_Costo_Fact
                                                    , string Porc_adic_por_empaque
                                                    , string Estatus
                                                    )
        {
            try
            {


                try
                {
                    Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

                    System.Collections.Hashtable parametros = new System.Collections.Hashtable();

                    if (string.IsNullOrEmpty(Id_AtributosSYE) || Id_AtributosSYE.Equals("0"))
                    {
                        parametros = new System.Collections.Hashtable();

                        parametros.Add("@Id_CategoriaArt", Id_CategoriaArt);
                        parametros.Add("@Precio_Fact", Precio_Fact);
                        parametros.Add("@Seguro_Trans", Seguro_Trans);
                        parametros.Add("@Porc_VS_Costo_Fact", Porc_adic_por_empaque);
                        parametros.Add("@Porc_adic_por_empaque", Porc_adic_por_empaque);
                        parametros.Add("@Usuario", "sysAdmin");


                        Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[tms].[up_CorpTMS_ins_AtributosSYE]", false, parametros);
                    }
                    else
                    {
                        parametros = new System.Collections.Hashtable();


                        parametros.Add("@Id_AtributosSYE", Id_AtributosSYE);
                        parametros.Add("@Id_CategoriaArt", Id_CategoriaArt);
                        parametros.Add("@Precio_Fact", Precio_Fact);
                        parametros.Add("@Seguro_Trans", Seguro_Trans);
                        parametros.Add("@Porc_VS_Costo_Fact", Porc_VS_Costo_Fact);
                        parametros.Add("@Porc_adic_por_empaque", Porc_adic_por_empaque);
                        parametros.Add("@Usuario", "sysAdmin");
                        parametros.Add("@BitActivo", Estatus.Equals("0") ? 1 : 0);

                        Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[tms].[up_CorpTMS_upd_AtributosSYE]", false, parametros);
                    }


                    //return ds;
                }
                catch (SqlException ex)
                {

                    throw ex;
                }
                catch (System.Exception ex)
                {

                    throw ex;
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
        public async Task<JsonResult> GetAtributosSYE(string Id_AtributosSYE)
        {
            try
            {


                try
                {
                    DataSet ds = GetAtributosSYE();

                    string o_id = "";
                    string o_cat = "";
                    string o_pre = "";
                    string o_seg = "";
                    string o_porc = "";
                    string o_adic = "";
                    string o_est = "";

                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        if (Id_AtributosSYE.Equals(r["Id_AtributosSYE"].ToString().Trim()))
                        {
                            o_id = r["Id_AtributosSYE"].ToString();
                            o_cat = r["Id_CategoriaArt"].ToString();
                            o_pre = r["Precio_Fact"].ToString();
                            o_seg = r["Seguro_Trans"].ToString();
                            o_porc = r["Porc_VS_Costo_Fact"].ToString();
                            o_adic = r["Porc_adic_por_empaque"].ToString();
                            o_est = r["BitActivo"].ToString();


                            break;
                        }

                    }


                    var result = new
                    {
                        Success = true,
                        Id = o_id,
                        cat = o_cat,
                        pre = o_pre,
                        seg = o_seg,
                        porc = o_porc,
                        adic = o_adic,
                        e = o_est.ToLower().Equals("0") ? 0 : 1
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);


                    //return ds;
                }
                catch (SqlException ex)
                {

                    throw ex;
                }
                catch (System.Exception ex)
                {

                    throw ex;
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
        #endregion

        #region TipoEntrega
        public ActionResult TipoEntregas()
        {
            Session["listaTE"] = GetTipoEntrega();

            Session["listaC"] = GetCategSAP2();

            return View();
        }

        public DataSet GetTipoEntrega()
        {

            DataSet ds = new DataSet();

            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("up_CorpTMS_sel_TipoEntregas", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                            dataAdapter.Fill(ds);
                    }
                }
                return ds;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            return ds;

        }


        [HttpPost]
        public async Task<JsonResult> InsTipoEntregas(string Id_TipoEntrega
                                                    , string Descripcion
                                                    , string TipoCatalogo
                                                    , string Id_TipoAlmacen
                                                    , string Id_CategoriaArt
                                                    , string Peso_min
                                                    , string Peso_max
                                                    , string Estatus
                                                    )
        {
            try
            {


                try
                {
                    Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

                    System.Collections.Hashtable parametros = new System.Collections.Hashtable();

                    if (string.IsNullOrEmpty(Id_TipoEntrega) || Id_TipoEntrega.Equals("0"))
                    {

                        string pmin;
                        string pmax;

                        if (Peso_min == "")
                        {
                            pmin = "0";
                        }
                        else
                        {
                            pmin = Peso_min;
                        }

                        if (Peso_max == "")
                        {
                            pmax = "0";
                        }
                        else
                        {
                            pmax = Peso_max;
                        }


                        parametros = new System.Collections.Hashtable();
                        parametros.Add("@Descripcion", Descripcion);
                        parametros.Add("@TipoCatalogo", TipoCatalogo);
                        parametros.Add("@Id_TipoAlmacen", Id_TipoAlmacen);
                        parametros.Add("@Id_CategoriaArt", Id_CategoriaArt);
                        parametros.Add("@Peso_min", pmin);
                        parametros.Add("@Peso_max", pmax);
                        parametros.Add("@Usuario", "sysAdmin");


                        Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "up_CorpTMS_ins_TipoEntregas", false, parametros);
                    }
                    else
                    {
                        parametros = new System.Collections.Hashtable();

                        parametros = new System.Collections.Hashtable();

                        parametros.Add("@Id_TipoEntrega", Id_TipoEntrega);
                        parametros.Add("@Descripcion", Descripcion);
                        parametros.Add("@TipoCatalogo", TipoCatalogo);
                        parametros.Add("@Id_TipoAlmacen", Id_TipoAlmacen);
                        parametros.Add("@Id_CategoriaArt", Id_CategoriaArt);
                        parametros.Add("@Peso_min", Peso_min);
                        parametros.Add("@Peso_max", Peso_max);
                        parametros.Add("@Usuario", "sysAdmin");
                        parametros.Add("@BitActivo", Estatus);

                        Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "up_CorpTMS_upd_TipoEntregas", false, parametros);
                    }


                    //return ds;
                }
                catch (SqlException ex)
                {

                    throw ex;
                }
                catch (System.Exception ex)
                {

                    throw ex;
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
        public async Task<JsonResult> GetTipoEntrega(string IdTipoEntrega)
        {
            try
            {


                try
                {
                    DataSet ds = (DataSet)Session["listaTE"];

                    string out_Id_TipoEntrega = "";
                    string out_Descripcion = "";
                    string out_TipoCatalogo = "";
                    string out_Id_TipoAlmacen = "";
                    string out_Id_CategoriaArt = "";
                    string out_Peso_min = "";
                    string out_Peso_max = "";
                    string out_estatus = "";

                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        if (IdTipoEntrega.Equals(r["Id_TipoEntrega"].ToString().Trim()))
                        {
                            out_Id_TipoEntrega = r["Id_TipoEntrega"].ToString();
                            out_Descripcion = r["Descripcion"].ToString();
                            out_TipoCatalogo = r["TipoCatalogo"].ToString();
                            out_Id_TipoAlmacen = r["Id_TipoAlmacen"].ToString();
                            out_Id_CategoriaArt = r["Id_CategoriaArt"].ToString();
                            out_Peso_min = r["Peso_min"].ToString();
                            out_Peso_max = r["Peso_max"].ToString();
                            out_estatus = r["BitActivo"].ToString();


                            break;
                        }

                    }


                    var result = new
                    {
                        Success = true,
                        Id = out_Id_TipoEntrega
                        ,
                        des = out_Descripcion
                        ,
                        tc = out_TipoCatalogo
                        ,
                        ta = out_Id_TipoAlmacen
                        ,
                        ca = out_Id_CategoriaArt
                        ,
                        pm = out_Peso_min
                        ,
                        pmx = out_Peso_max
                        ,
                        e = out_estatus
                    };
                    return Json(result, JsonRequestBehavior.AllowGet);


                    //return ds;
                }
                catch (SqlException ex)
                {

                    throw ex;
                }
                catch (System.Exception ex)
                {

                    throw ex;
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
        #endregion

        #region Almacenes 


        public DataSet GetProv()
        {

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }

            Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

            System.Collections.Hashtable parametros = new System.Collections.Hashtable();

            return Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "tms.upCorpTms_Cns_Suppliers", false, parametros);



        }





        public ActionResult Almacenes()
        {
            Session["listaProv"] = GetProv();
            //ViewBag.Consulta = DataTableToModel.ConvertTo<almacenTMS>(DALCatalogo.almacenTMS_sUp().Tables[0]); 
            return View("Almacenes/Index");
        }



        [HttpPost]
        public async Task<JsonResult> GetPostalCodeFromPosition(string latitude, string longitude)
        {
            PostalCodeRepository pcRepository = new PostalCodeRepository();
            try
            {
                List<int> PostalCodeList = pcRepository.GetPostalCode(Convert.ToDecimal(latitude), Convert.ToDecimal(longitude)).ToList();
                var result = new { Success = true, json = PostalCodeList };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public async Task<JsonResult> GetCommonAlmacenes()
        {
            try
            {
                string apiUrl = string.Format("{0}api/Ordenes/GetCarrier", UrlApi);
                DataSet ds = DALServicesM.GetCommonAlmacenes();
                List<CommonAlmacenModel> listC = ConvertTo<CommonAlmacenModel>(ds.Tables[0]);
                var result = new { Success = true, json = listC };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetCommonProveedores(string IdAlmacen)
        {
            try
            {
                string apiUrl = string.Format("{0}api/Ordenes/GetCarrier", UrlApi);
                DataSet ds = DALServicesM.GetCommonProveedores(IdAlmacen);
                List<CommonProveedorModel> listC = ConvertTo<CommonProveedorModel>(ds.Tables[0]);
                var result = new { Success = true, json = listC };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        //public async Task<JsonResult> GetAlmacenes(int IdTransportista) 
        //{ 
        //    try 
        //    { 
        //        DataSet ds = DALServicesM.GetTmsAlmacenes(IdTransportista); 
        //        List<AlmacenModel> listC = ConvertTo<AlmacenModel>(ds.Tables[0]); 
        //        var result = new { Success = true, json = listC }; 
        //        return Json(result, JsonRequestBehavior.AllowGet); 
        //    } 

        //    catch (Exception x) 
        //    { 
        //        var result = new { Success = false, Message = x.Message }; 
        //        return Json(result, JsonRequestBehavior.AllowGet); 
        //    } 

        //} 
        //************************************************************************************************ 
        //************************************************************************************************ 
        //************************************************************************************************ 
        [HttpPost]
        public ActionResult AddAlmacenes(int idOwner, int idSupplierWH//, string RazonSocial, 
            , int idSupplierWHCode, bool ServicioEstandar, bool ServicioExpress, int TiempoSurtido, bool BitActivo)
        {

            try
            {
                DALCatalogo.almacenTMS_iUp(idOwner, idSupplierWH, idSupplierWHCode, ServicioEstandar, ServicioExpress, TiempoSurtido, BitActivo, User.Identity.Name);
                var result = new { Success = true, Message = "Alta exitosa" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UpdAlmacenes(int idOwner, int idSupplierWH//, string RazonSocial, 
    , int idSupplierWHCode, bool ServicioEstandar, bool ServicioExpress, int TiempoSurtido, bool BitActivo)
        {

            try
            {
                DALCatalogo.almacenTMS_uUp(idOwner, idSupplierWH, idSupplierWHCode, ServicioEstandar, ServicioExpress, TiempoSurtido, BitActivo, User.Identity.Name);
                var result = new { Success = true, Message = "Actualización exitosa" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DelAlmacenes(int idOwner, int idSupplierWH, int idSupplierWHCode)
        {

            try
            {
                DALCatalogo.almacenTMS_dUp(idOwner, idSupplierWH, idSupplierWHCode, User.Identity.Name);
                var result = new { Success = true, Message = "Actualización exitosa" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetAlmacenes()
        {
            try
            {

                var listC = ConvertTo<almacenTMS>(DALCatalogo.almacenTMS_sUp().Tables[0]);
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
        public ActionResult GetAlmacenesById(int idOwner, int idSupplierWH, int idSupplierWHCode)
        {
            try
            {



                var listC = ConvertTo<almacenTMS>(DALCatalogo.almacenTMSById_sUp(idOwner, idSupplierWH, idSupplierWHCode).Tables[0]).FirstOrDefault();
                var result = new { Success = true, resp = listC };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult IniciarComboTipoAlmacen()
        {
            try
            {
                var dropdownVD = DataTableToModel.ConvertTo<Owners>(DALCatalogo.spOwners_sUP().Tables[0]);
                //DataTableToModel.ConvertTo<ServicesManagement.Web.Models.Almacenes.SPOwners_sUP>(DALAltaProveedor.spOwners_sUP().Tables[0]) 
                var result = new { Success = true, resp = dropdownVD };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetSuppliersWarehousest(string IdProv, int idOwner)
        {
            try
            {

                List<AlmacenCmb> listC = ConvertTo<AlmacenCmb>(DALCatalogo.upCorpTms_Cns_SuppliersWarehouses(IdProv, idOwner).Tables[0]);
                var result = new { Success = true, json = listC };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTipoLogisticaAlm(int op, int idSupplierWH, int idSupplierWHCode, int idOwner)
        {
            try
            {

                var listC = ConvertTo<TipoLogisticaCns>(DALTipoLogistica.up_CorpTMS_sel_TipoLogistica().Tables[0]);


                foreach (var item in listC)
                {
                    if (DALCatalogo.AlmacenTipoLogisticaById_sUp(item.IdTipoLogistica, idSupplierWH, idSupplierWHCode, idOwner).Tables[0].Rows.Count != 0)
                    {
                        item.IsChecked = true;
                    }
                    else { item.IsChecked = false; }
                }

                var result = new { Success = true, resp = listC };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddLogisticaAlm(List<Logistica> logistica)
        {
            try
            {
                foreach (var item in logistica)
                {
                    if (item.IsChecked)
                    {
                        DALCatalogo.AlmacenTipoLogistica_iUp(item.IdTipoLogistica, item.idSupplierWH, item.idSupplierWHCode, User.Identity.Name, item.idOwner);
                    }
                    else
                    {
                        DALCatalogo.AlmacenTipoLogistica_dUp(item.IdTipoLogistica, item.idSupplierWH, item.idSupplierWHCode, item.idOwner);
                    }
                }

                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetProveedoresCmb(int idOwner)
        {
            try
            {
                var dropdownVD = DataTableToModel.ConvertTo<SuppliersCmb>(DALCatalogo.upCorpTms_Cns_SuppliersById(idOwner).Tables[0]);
                //DataTableToModel.ConvertTo<ServicesManagement.Web.Models.Almacenes.SPOwners_sUP>(DALAltaProveedor.spOwners_sUP().Tables[0]) 
                var result = new { Success = true, resp = dropdownVD };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region TiendasCostoEnvio
        public ActionResult TiendasCostoEnvio()
        {

            return View();
        }



        public ActionResult GetTiendasCostoEnvio()
        {
            try
            {
                var list = DataTableToModel.ConvertTo<TiendaCostoEnvio>(DALCatalogo.upCorpTms_Cns_TiendasCostoEnvio().Tables[0]);

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetTiendasCostoEnvioId(int IdConsecutivo)
        {
            try
            {
                var list = DataTableToModel.ConvertTo<TiendaCostoEnvio>(DALCatalogo.upCorpTms_Cns_TiendasCostoEnvioById(IdConsecutivo).Tables[0]).FirstOrDefault();

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult AddTiendasCostoEnvio(int IdConsecutivo, int StoreNum, string Direccion, int IdTipoFormato, string Director, string Region
            , decimal CostoEnvio, bool BitActivo)
        {
            try
            {
                if (IdConsecutivo == 0)
                {
                    DALCatalogo.upCorpTms_Ins_TiendasCostoEnvio(StoreNum, Direccion, IdTipoFormato, Director, Region
            , CostoEnvio, Convert.ToBoolean(BitActivo), User.Identity.Name, User.Identity.Name);
                }
                else
                {
                    DALCatalogo.upCorpTms_Upd_TiendasCostoEnvio(IdConsecutivo, StoreNum, Direccion, IdTipoFormato, Director, Region
                , CostoEnvio, Convert.ToBoolean(BitActivo), User.Identity.Name);
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


        public ActionResult DelTiendasCostoEnvio(int IdConsecutivo)
        {
            try
            {
                DALCatalogo.upCorpTms_Del_TiendasCostoEnvio(IdConsecutivo, User.Identity.Name);

                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult CmbUN()
        {
            try
            {
                var list = DataTableToModel.ConvertTo<upCorpOms_Cns_UN>(DALServicesM.GetTmsUN().Tables[0]);

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

        #region tipoEntregasSETC
        public ActionResult TipoEntregaSETC()
        {
            return View();

        }

        [HttpPost]
        public ActionResult GetEntregaSETC()
        {
            try
            {
                List<ServicesManagement.Web.Models.Catalogos.TipoEntregaSETC> lst = new List<ServicesManagement.Web.Models.Catalogos.TipoEntregaSETC>();

                //logistica datatable
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault().ToLower();



                #region Se Obtienen Filtros Por Columna
                var StoreNum = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault().ToLower();
                var Desc_UN = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault().ToLower();
                var IdTipoEnvio = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault().ToLower();
                var Desc_TipoEnvio = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault().ToLower();
                var UsuarioCreacion = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault().ToLower();
                var FechaCreacion = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault().ToLower();

                var HoraCreacion = Request.Form.GetValues("columns[6][search][value]").FirstOrDefault().ToLower();
                var UsuarioUltModif = Request.Form.GetValues("columns[7][search][value]").FirstOrDefault().ToLower();
                var FechaUltModif = Request.Form.GetValues("columns[8][search][value]").FirstOrDefault().ToLower();
                var HoraUltModif = Request.Form.GetValues("columns[9][search][value]").FirstOrDefault().ToLower();
                var BitActivo = Request.Form.GetValues("columns[10][search][value]").FirstOrDefault().ToLower();



                #endregion

                pageSize = length != null ? Convert.ToInt32(length) : 0;
                skip = start != null ? Convert.ToInt32(start) : 0;
                recordsTotal = 0;

                IQueryable<TipoEntregaSETC> query = from row in DALCatalogo.TipoEntregaSETC_sUp().Tables[0].AsEnumerable().AsQueryable()
                                                    select new TipoEntregaSETC()
                                                    {
                                                        IdTipoEntrega = int.Parse(row["IdTipoEntrega"].ToString()),
                                                        StoreNum = int.Parse(row["StoreNum"].ToString()),
                                                        Desc_UN = row["Desc_UN"].ToString(),
                                                        IdTipoEnvio = int.Parse(row["IdTipoEnvio"].ToString()),
                                                        Desc_TipoEnvio = row["Desc_TipoEnvio"].ToString(),
                                                        UsuarioCreacion = row["UsuarioCreacion"].ToString(),
                                                        FechaCreacion = row["FechaCreacion"].ToString(),
                                                        HoraCreacion = row["HoraCreacion"].ToString(),
                                                        UsuarioUltModif = row["UsuarioUltModif"].ToString(),
                                                        FechaUltModif = row["FechaUltModif"].ToString(),
                                                        HoraUltModif = row["HoraUltModif"].ToString(),
                                                        BitActivo = row["BitActivo"].ToString()
                                                    };




                if (searchValue != "")
                    query = query.Where(d => d.StoreNum.ToString().ToLower().Contains(searchValue)
                    || d.Desc_UN.ToLower().Contains(searchValue)
                    || d.IdTipoEnvio.ToString().ToLower().Contains(searchValue)
                    || d.Desc_TipoEnvio.ToLower().Contains(searchValue)
                    || d.UsuarioCreacion.ToLower().Contains(searchValue)
                    || d.FechaCreacion.ToLower().Contains(searchValue)
                    || d.HoraCreacion.ToLower().Contains(searchValue)
                    || d.UsuarioUltModif.ToLower().Contains(searchValue)
                    || d.FechaUltModif.ToLower().Contains(searchValue)
                    || d.HoraUltModif.ToLower().Contains(searchValue)
                    || d.BitActivo.ToLower().Contains(searchValue)
                    );


                //Filter By Columns
                #region Filter By Columns
                if (!string.IsNullOrEmpty(StoreNum))
                {
                    query = query.Where(a => a.StoreNum.ToString().ToLower().Contains(StoreNum));
                }
                if (!string.IsNullOrEmpty(Desc_UN))
                {
                    query = query.Where(a => a.Desc_UN.ToLower().Contains(Desc_UN));
                }

                if (!string.IsNullOrEmpty(IdTipoEnvio))
                {
                    query = query.Where(a => a.IdTipoEnvio.ToString().ToLower().Contains(IdTipoEnvio));
                }
                if (!string.IsNullOrEmpty(Desc_TipoEnvio))
                {
                    query = query.Where(a => a.Desc_TipoEnvio.ToLower().Contains(Desc_TipoEnvio));
                }

                if (!string.IsNullOrEmpty(UsuarioCreacion))
                {
                    query = query.Where(a => a.UsuarioCreacion.ToLower().Contains(UsuarioCreacion));
                }

                if (!string.IsNullOrEmpty(FechaCreacion))
                {
                    query = query.Where(a => a.FechaCreacion.ToLower().Contains(FechaCreacion));
                }

                if (!string.IsNullOrEmpty(HoraCreacion))
                {
                    query = query.Where(a => a.HoraCreacion.ToLower().Contains(HoraCreacion));
                }

                if (!string.IsNullOrEmpty(UsuarioUltModif))
                {
                    query = query.Where(a => a.UsuarioUltModif.ToLower().Contains(UsuarioUltModif));
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
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public FileResult ExcelTipoEntregaSETC(string StoreNum
            , string Desc_UN, string IdTipoEnvio, string Desc_TipoEnvio, string UsuarioCreacion
            , string FechaCreacion, string HoraCreacion
            , string UsuarioUltModif, string FechaUltModif, string HoraUltModif, string BitActivo, string searchValue)

        {
            List<TipoEntregaSETC> lst = new List<TipoEntregaSETC>();

            IQueryable<TipoEntregaSETC> query = from row in DALCatalogo.TipoEntregaSETC_sUp().Tables[0].AsEnumerable().AsQueryable()
                                                select new TipoEntregaSETC()
                                                {
                                                    StoreNum = int.Parse(row["StoreNum"].ToString()),
                                                    Desc_UN = row["Desc_UN"].ToString(),
                                                    IdTipoEnvio = int.Parse(row["IdTipoEnvio"].ToString()),
                                                    Desc_TipoEnvio = row["Desc_TipoEnvio"].ToString(),
                                                    UsuarioCreacion = row["UsuarioCreacion"].ToString(),
                                                    FechaCreacion = row["FechaCreacion"].ToString(),
                                                    HoraCreacion = row["HoraCreacion"].ToString(),
                                                    UsuarioUltModif = row["UsuarioUltModif"].ToString(),
                                                    FechaUltModif = row["FechaUltModif"].ToString(),
                                                    HoraUltModif = row["HoraUltModif"].ToString(),
                                                    BitActivo = row["BitActivo"].ToString()
                                                };


            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(x => x.StoreNum.ToString().ToLower().Contains(searchValue)
                    || x.Desc_UN.ToLower().Contains(searchValue)
                    || x.IdTipoEnvio.ToString().ToLower().Contains(searchValue)
                    || x.Desc_TipoEnvio.ToLower().Contains(searchValue)
                    || x.UsuarioCreacion.ToLower().Contains(searchValue)
                    || x.FechaCreacion.ToLower().Contains(searchValue)
                    || x.HoraCreacion.ToLower().Contains(searchValue)
                    || x.UsuarioUltModif.ToLower().Contains(searchValue)
                    || x.FechaUltModif.ToLower().Contains(searchValue)
                    || x.HoraUltModif.ToLower().Contains(searchValue)
                    || x.BitActivo.ToLower().Contains(searchValue)
                    );

            //Filter By Columns
            #region Filter By Columns
            if (!string.IsNullOrEmpty(StoreNum))
            {
                query = query.Where(a => a.StoreNum.ToString().ToLower().Contains(StoreNum));
            }
            if (!string.IsNullOrEmpty(Desc_UN))
            {
                query = query.Where(a => a.Desc_UN.ToLower().Contains(Desc_UN));
            }

            if (!string.IsNullOrEmpty(IdTipoEnvio))
            {
                query = query.Where(a => a.IdTipoEnvio.ToString().ToLower().Contains(IdTipoEnvio));
            }
            if (!string.IsNullOrEmpty(Desc_TipoEnvio))
            {
                query = query.Where(a => a.Desc_TipoEnvio.ToLower().Contains(Desc_TipoEnvio));
            }

            if (!string.IsNullOrEmpty(UsuarioCreacion))
            {
                query = query.Where(a => a.UsuarioCreacion.ToLower().Contains(UsuarioCreacion));
            }

            if (!string.IsNullOrEmpty(FechaCreacion))
            {
                query = query.Where(a => a.FechaCreacion.ToLower().Contains(FechaCreacion));
            }

            if (!string.IsNullOrEmpty(HoraCreacion))
            {
                query = query.Where(a => a.HoraCreacion.ToLower().Contains(HoraCreacion));
            }

            if (!string.IsNullOrEmpty(UsuarioUltModif))
            {
                query = query.Where(a => a.UsuarioUltModif.ToLower().Contains(UsuarioUltModif));
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
                query = query.Where(a => a.BitActivo.ToLower().Contains(BitActivo));
            }

            #endregion

            recordsTotal = query.Count();

            lst = query.Take(recordsTotal).ToList();

            var d = new DataSet();

            string nombreArchivo = "TipoEntregaSETC";

            //Excel to create an object file

            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            //Add a sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");


            //Here you can set a variety of styles seemingly font color backgrounds, but not very convenient, there is not set
            //Sheet1 head to add the title of the first row
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);


            row1.CreateCell(0).SetCellValue("StoreNum");
            row1.CreateCell(1).SetCellValue("Desc_UN");
            row1.CreateCell(2).SetCellValue("IdTipoEnvio");
            row1.CreateCell(3).SetCellValue("Desc_TipoEnvio");
            row1.CreateCell(4).SetCellValue("UsuarioCreacion");
            row1.CreateCell(5).SetCellValue("FechaCreacion");
            row1.CreateCell(6).SetCellValue("HoraCreacion");
            row1.CreateCell(7).SetCellValue("UsuarioUltModif");
            row1.CreateCell(8).SetCellValue("FechaUltModif");
            row1.CreateCell(9).SetCellValue("HoraUltModif");
            row1.CreateCell(10).SetCellValue("BitActivo");

            //                                                
            //The data is written progressively sheet1 each row
            foreach (TipoEntregaSETC item in lst)
            {

            }
            for (int i = 0; i < lst.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(lst[i].StoreNum.ToString());
                rowtemp.CreateCell(1).SetCellValue(lst[i].Desc_UN.ToString());
                rowtemp.CreateCell(2).SetCellValue(lst[i].IdTipoEnvio.ToString());
                rowtemp.CreateCell(3).SetCellValue(lst[i].Desc_TipoEnvio.ToString());
                rowtemp.CreateCell(4).SetCellValue(lst[i].UsuarioCreacion.ToString());
                rowtemp.CreateCell(5).SetCellValue(lst[i].FechaCreacion.ToString());
                rowtemp.CreateCell(6).SetCellValue(lst[i].HoraCreacion.ToString());
                rowtemp.CreateCell(7).SetCellValue(lst[i].UsuarioUltModif.ToString());
                rowtemp.CreateCell(8).SetCellValue(lst[i].FechaUltModif.ToString());
                rowtemp.CreateCell(9).SetCellValue(lst[i].HoraUltModif.ToString());
                rowtemp.CreateCell(10).SetCellValue(lst[i].BitActivo.ToString());

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

        public ActionResult GetTipoEntregaSETCId(int IdTipoEntrega)
        {
            try
            {
                var list = DataTableToModel.ConvertTo<TipoEntregaSETC>(DALCatalogo.TipoEntregaSETCById_sUp(IdTipoEntrega).Tables[0]).FirstOrDefault();

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult AddTipoEntregaSETC(string StoreNum, string IdTipoEnvio, int BitActivo)
        {
            try
            {
                DALCatalogo.TipoEntregaSETC_iUp(int.Parse(StoreNum), int.Parse(IdTipoEnvio), Convert.ToBoolean(BitActivo), User.Identity.Name);

                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult UpdTipoEntregaSETC(string IdTipoEntrega, string StoreNum, string IdTipoEnvio, int BitActivo)
        {
            try
            {
                DALCatalogo.TipoEntregaSETC_uUp(int.Parse(IdTipoEntrega), int.Parse(StoreNum), int.Parse(IdTipoEnvio), Convert.ToBoolean(BitActivo), User.Identity.Name);

                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult DelTipoEntregaSETC(string IdTipoEntrega)
        {
            try
            {
                DALCatalogo.TipoEntregaSETC_dUp(int.Parse(IdTipoEntrega), User.Identity.Name);

                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult ListEntrega()

        {

            try

            {

                var list = DataTableToModel.ConvertTo<TipoEntregaSETC>(DALCatalogo.upCorpTms_Cns_TipoEnvioSETC().Tables[0]);



                var result = new { Success = true, resp = list };

                return Json(result, JsonRequestBehavior.AllowGet);

            }

            catch (Exception x)

            {

                var result = new { Success = false, Message = x.Message };

                return Json(result, JsonRequestBehavior.AllowGet);

            }



        }

        public ActionResult ListTienda()

        {

            try

            {

                var list = DataTableToModel.ConvertTo<TipoEntregaSETC>(DALCatalogo.upCorpTms_Cns_UNList().Tables[0]);

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


        #region ClasesEnvio
        public ActionResult ClasesEnvios()
        {
            return View();
        }
        public ActionResult GetClasesEnvios()
        {
            try
            {

                var listC = ConvertTo<ClaseEnvio>(DALCatalogo.ClaseEnvio_sUp().Tables[0]);
                var result = new { Success = true, resp = listC };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetClasesEnviosId(int IdClase)
        {
            try
            {

                var listC = ConvertTo<ClaseEnvio>(DALCatalogo.ClaseEnvioById_sUp(IdClase).Tables[0]).FirstOrDefault();
                var result = new { Success = true, resp = listC };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddClasesEnvios(int IdClase, string Desc_ClaseEnvio, bool BitActivo)
        {
            try
            {
                if (IdClase == 0)
                {
                    DALCatalogo.ClaseEnvio_iUp(Desc_ClaseEnvio, BitActivo, User.Identity.Name);
                }
                else
                {
                    DALCatalogo.ClaseEnvio_uUp(IdClase, Desc_ClaseEnvio, BitActivo, User.Identity.Name);
                }

                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DelClasesEnvios(int IdClase)
        {
            try
            {

                DALCatalogo.ClaseEnvio_dUp(IdClase, User.Identity.Name);
                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

    }

}