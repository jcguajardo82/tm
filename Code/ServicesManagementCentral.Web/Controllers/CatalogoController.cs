

using Newtonsoft.Json;
using ServicesManagement.Web.DAL;
using ServicesManagement.Web.Helpers;
using ServicesManagement.Web.Models;
using ServicesManagement.Web.Models.Catalogos;
using System;

using System.Collections.Generic;

using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;

using System.Reflection;

using System.Text;

using System.Threading.Tasks;

using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ServicesManagement.Web.Controllers

{


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









        #region CodigosPostales

        public ActionResult CodigosPostales()

        {
            return View("CodigosPostales/Index");
        }

        #endregion



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

                
                var result = new { Success = true, json = list[0].Nombre + " "+ list[0].Apellido_Paterno + " " + list[0].Apellido_Materno };
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

        #region Almacenes

        public ActionResult Almacenes()
        {
            return View("Almacenes/Index");
        }

        [HttpGet]
        public async Task<JsonResult> GetAlmacenes()
        {
            try
            {
                string apiUrl = string.Format("{0}api/Ordenes/GetCarrier", UrlApi);
                DataSet ds = DALServicesM.GetTmsAlmacenes();
                List<AlmacenModel> listC = ConvertTo<AlmacenModel>(ds.Tables[0]);
                var result = new { Success = true, json = listC };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetPostalCodeFromPosition(string latitude, string longitude)
        {
            PostalCodeRepository pcRepository = new PostalCodeRepository();
            try
            {
                List<int> PostalCodeList = pcRepository.GetPostalCode(Convert.ToDecimal( latitude), Convert.ToDecimal(longitude)).ToList();
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

        public async Task<JsonResult> GetAlmacenes(int IdTransportista)
        {
            try
            {
                DataSet ds = DALServicesM.GetTmsAlmacenes(IdTransportista);
                List<AlmacenModel> listC = ConvertTo<AlmacenModel>(ds.Tables[0]);
                var result = new { Success = true, json = listC };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public async Task<ActionResult> AddAlmacenes(long IdAlmacen,long IdProveedor, string RazonSocial, 
            int IdTipoAlmacen, bool Paqueteria, bool BigTicket, bool ServicioEstandar, bool ServicioExpress, string UsuarioCreacion)
        {
            try
            {
                DALServicesM.AddAlmacenes(IdAlmacen, IdProveedor, IdTipoAlmacen, Paqueteria, BigTicket, ServicioEstandar, 
                    ServicioExpress, UsuarioCreacion);
                var result = new { Success = true, Message = "Alta exitosa" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
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



                List<CarrierModel> listC = ConvertTo<CarrierModel>(ds.Tables[0]);



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

                DALServicesM.AddCarriers(IdTransportista,TarifaFija, CostoTarifaFija, Prioridad, NivelServicio,
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
        public ActionResult AddPeso(string Id_Proveedor, string peso,string Id_paqueteria)
        {
            try
            {
                if (!Id_paqueteria.Equals("0"))
                {
                    DALCatalogo.Paqueteria_uUp(int.Parse(Id_Proveedor), decimal.Parse(peso),int.Parse(Id_paqueteria));
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

        public ActionResult AddGasto(string gasto, int Id_gasto)
        {
            try
            {
                if (Id_gasto.Equals(0))
                {
                    DALCatalogo.Gastos_iUp(Id_gasto, gasto);
                }
                else { DALCatalogo.Gastos_uUp(gasto, Id_gasto ); }


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

    }

}