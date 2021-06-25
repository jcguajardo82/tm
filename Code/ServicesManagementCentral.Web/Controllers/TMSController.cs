using ExcelDataReader;
using Newtonsoft.Json;
using ServicesManagement.Web.DAL;
using ServicesManagement.Web.Helpers;
using ServicesManagement.Web.Models.Catalogos;
using ServicesManagement.Web.Models.Impex;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ServicesManagement.Web.Controllers
{
    public class ResponseModels
    {

        public string Cve_RespCode { get; set; } = "00";
        public string Guia { get; set; } = "";
        public string Desc_MensajeError { get; set; } = "";
        public byte[] pdf { get; set; }
    }

    public class EstafetaRequestModel
    {

        public string aditionalInfo { get; set; } = "Informacion adicional";
        //Contenido
        public string content { get; set; } = "Contenido";
        //Centro de costos
        public string costCenter { get; set; } = "CCtos";
        //Ocurre
        public bool deliveryToEstafetaOffice { get; set; } = false;
        //En caso de envio a otro pais, solo siglasWS de generación de guías Estafeta Versión 2 Fecha: 31/01/2011
        //Estafeta Mexicana S.A.de C.V. 36
        public string destinationCountryId { get; set; } = "MX";
        //Tipo de envio 1{get; set;} =SOBRE 4{get; set;} =PAQUETE
        public int parcelTypeId { get; set; } = 4;
        //Referencia
        public string reference { get; set; } = "Referencia";
        //Peso
        public int weight { get; set; } = 1;
        //Número de etiquetas solicitadas
        public int numberOfLabels { get; set; } = 1;
        //Código postal de Origen para enrutamiento
        public string originZipCodeForRouting { get; set; } = "62250";
        //Servicio que se usará
        public string serviceTypeId { get; set; } = "50";
        //Numero de oficina que corresponde al cliente
        public string officeNum { get; set; } = "421";
        //Documento de retorno
        public bool returnDocument { get; set; } = true;
        //Servicio del documento de retorno
        public string serviceTypeIdDocRet { get; set; } = "50";
        //Fecha de vigencia
        public string effectiveDate { get; set; } = "20110525";
        //Descripcion del contenido
        public string contentDescription { get; set; } = "Descripcion del contenido del paquete";

        public AddressModel DestinationInfo { get; set; }

        public AddressModel OriginInfo { get; set; }

    }


    public class AddressModel
    {


        public string address1 { get; set; } = "public string addr1";
        public string address2 { get; set; } = "public string Addr2";
        public string city { get; set; } = "Ciudad";
        public string contactName { get; set; } = "Cliente";
        public string corporateName { get; set; } = "Corporate";
        public string customerNumber { get; set; } = "1234568";
        public string neighborhood { get; set; } = "neighborhood";
        public string phoneNumber { get; set; } = "1111111";
        public string cellPhone { get; set; } = "0447777777777";
        public string state { get; set; } = "Mexico";
        //Código postal destino
        public string zipCode { get; set; } = "01000";


    }


    public class CotizacionModel
    {

        public string codigoPostal_origen { get; set; }

        public string codigoPostal_destino { get; set; }

        public bool esPaquete { get; set; }

        public double largo { get; set; }

        public double peso { get; set; }

        public double alto { get; set; }

        public double ancho { get; set; }

    }

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

        public int Id_TipoVehiculo { get; set; }
        public string TipoVehiculo { get; set; }

        // Modificar 

        public int IdGasto { get; set; }
        public string FecGasto { get; set; }
        public int Kilometraje { get; set; }
        public decimal CantidadGasto { get; set; }

        public string CreateDate { get; set; }
        public bool activo { get; set; }



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
    public class GastosVehiculoModel
    {
        public int IdGasto { get; set; }
        public int Id_Vehiculo { get; set; }
        public string FecGasto { get; set; }
        public string Kilometraje { get; set; }
        public string CantidadGasto { get; set; }
        public bool Estatus { get; set; }
        public string CreateDate { get; set; }
        public bool activo { get; set; }
        public string FecMovto { get; set; }
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

            Session["lista"] = GetTipoVehiculos();

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
            Session["lista"] = GetTipoVehiculos();

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
            Session["lista"] = GetTipoVehiculos();

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

        public ActionResult Cotizacion()
        {
            DataSet ds = GetOrdenes();
            Session["lisOrdenes"] = ds;

            return View();
        }


        public DataSet GetOrdenes()
        {

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {
                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

                System.Collections.Hashtable parametros = new System.Collections.Hashtable();

                return Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[up_Corp_Cns_OrdenesTMS]", false, parametros);


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
        }

        [HttpGet]
        public async Task<JsonResult> GetCotizacion(string cp_o, string cp_d, bool paquete, string alto, string ancho, string largo, string peso)
        {
            try
            {
                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["api_Cotizacion"].ToString(); //string.Format("{0}/DelOperador", UrlApi);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                CotizacionModel v = new CotizacionModel
                {
                    codigoPostal_destino = cp_d,
                    codigoPostal_origen = cp_o,
                    esPaquete = true,
                    largo = Convert.ToDouble(largo),
                    peso = Convert.ToDouble(peso)
                ,
                    alto = Convert.ToDouble(alto)
                ,
                    ancho = Convert.ToDouble(ancho)
                };

                Soriana.FWK.FmkTools.RestResponse responseApi1 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, apiUrl, null, Newtonsoft.Json.JsonConvert.SerializeObject(v));

                if (responseApi1.code.Equals("00"))
                {
                    ServicesManagement.Web.Models.ResponseModels listC = Newtonsoft.Json.JsonConvert.DeserializeObject<ServicesManagement.Web.Models.ResponseModels>(responseApi1.message);

                    listC.cotizacion.TipoServicio = new Models.TipoServicio[1];

                    listC.cotizacion.TipoServicio[0] = new Models.TipoServicio { AplicaCotizacion = "1", CargosExtra = 0, AplicaServicio = "1", CCSobrePeso = 1, CCTarifaBase = 1, CostoTotal = 1, DescripcionServicio = "1", Peso = 1, SobrePeso = 1, TarifaBase = 1, TipoEnvioRes = 1 };

                    if (!string.IsNullOrEmpty(listC.cotizacion.Error))
                    {
                        var result2 = new { Success = false, Message = listC.cotizacion.MensajeError };
                        return Json(result2, JsonRequestBehavior.AllowGet);
                    }

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
        public async Task<JsonResult> GeneraGuia(string UeNo, string OrderNo, string tipoServicio, string paqueteria)
        {
            try
            {

                DataSet ds = new DataSet();
                string msgErr = string.Empty;

                string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
                if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
                {
                    conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
                }


                try
                {
                    Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

                    System.Collections.Hashtable parametros = new System.Collections.Hashtable();
                    parametros.Add("@UeNo", UeNo);
                    parametros.Add("@OrderNo", OrderNo);

                    ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[dbo].[upCorpOms_Sel_EstafetaInfo]", false, parametros);

                }
                catch (SqlException ex)
                {
                    msgErr = "ERRSQL";
                    var result = new { Success = false, Message = "ERRSQL" + ex.Message };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                catch (System.Exception ex)
                {
                    msgErr = "ERR";
                    var result = new { Success = false, Message = "ERR" + ex.Message };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }




                foreach (DataRow r in ds.Tables[0].Rows)
                {


                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    EstafetaRequestModel m = new EstafetaRequestModel();

                    m.serviceTypeId = "1";
                    m.DestinationInfo = new AddressModel();

                    m.DestinationInfo.address1 = r["Address1"].ToString();
                    m.DestinationInfo.address2 = r["Address2"].ToString();
                    m.DestinationInfo.cellPhone = r["Phone"].ToString();
                    m.DestinationInfo.city = r["City"].ToString();
                    m.DestinationInfo.contactName = r["CustomerName"].ToString();
                    m.DestinationInfo.corporateName = r["CustomerName"].ToString();
                    m.DestinationInfo.customerNumber = r["CustomerNo"].ToString();
                    m.DestinationInfo.neighborhood = r["NameReceives"].ToString();
                    m.DestinationInfo.phoneNumber = r["Phone"].ToString();
                    m.DestinationInfo.state = r["StateCode"].ToString();
                    m.DestinationInfo.zipCode = r["PostalCode"].ToString();

                    string json2 = JsonConvert.SerializeObject(m);

                    Soriana.FWK.FmkTools.RestResponse r2 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, System.Configuration.ConfigurationSettings.AppSettings["api_Estafeta_Guia"], "", json2);

                    string msg = r2.message;

                    ResponseModels re = JsonConvert.DeserializeObject<ResponseModels>(r2.message);


                    Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

                    System.Collections.Hashtable parametros = new System.Collections.Hashtable();
                    parametros.Add("@Id_proveedor", 1);
                    parametros.Add("@OrderNo", OrderNo);
                    parametros.Add("@UeNo", UeNo);
                    parametros.Add("@Guia", re.Guia);
                    parametros.Add("@pdf", re.pdf);


                    Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[dbo].[up_Copr_Ins_GuiaPaqueteria]", false, parametros);

                    string pdfS = Convert.ToBase64String(re.pdf);

                    var result4 = new { Success = true , guia = re.Guia, pdf = pdfS};
                    return Json(result4, JsonRequestBehavior.AllowGet);

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
        public async Task<JsonResult> SaveCotizacion(string jsonData)
        {
            try
            {
                List<ServicesManagement.Web.Models.TipoServicio> lis = new List<Models.TipoServicio>();
                lis = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.TipoServicio>>(jsonData);

                string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
                if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
                {
                    conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
                }


                try
                {
                    Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);


                    foreach (Models.TipoServicio t in lis)
                    {

                        System.Collections.Hashtable parametros = new System.Collections.Hashtable();
                        parametros.Add("@Id_proveedor", 1);
                        parametros.Add("@descripcionServicioField", t.DescripcionServicio);
                        parametros.Add("@tipoEnvioResField", t.TipoEnvioRes);
                        parametros.Add("@aplicaCotizacionField", t.AplicaCotizacion);
                        parametros.Add("@tarifaBaseField", t.TarifaBase);
                        parametros.Add("@cCTarifaBaseField", t.CCTarifaBase);
                        parametros.Add("@cargosExtraField", t.CargosExtra);
                        parametros.Add("@sobrePesoField", t.SobrePeso);
                        parametros.Add("@cCSobrePesoField", t.CCSobrePeso);
                        parametros.Add("@costoTotalField", t.CostoTotal);
                        parametros.Add("@pesoField", t.Peso);
                        parametros.Add("@aplicaServicioField", t.AplicaServicio);


                        Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[up_Corp_ins_CotizacionPaqueteria]", false, parametros);

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

        #region Gastos de vehiculo

        //public ActionResult Gastosvehículo()
        //{
        //    return View();
        //}

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

        public ActionResult ListGastosVehiculo(int Id_Vehiculo)
        {
            try
            {
                var list = DataTableToModel.ConvertTo<GastosVehiculoModel>(DALGastosVehiculo.GastoVehiculo_sUp(Id_Vehiculo).Tables[0]);

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult AddGastoVehiculo(int IdGasto, int Id_Vehiculo, decimal CantidadGasto, int Kilometraje, string FecGasto )
        {
            try
            {
                string UserCreate = User.Identity.Name;

                DALGastosVehiculo.GastoVehiculo_iUp(Id_Vehiculo, IdGasto, FecGasto, Kilometraje, CantidadGasto, UserCreate);

                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }



        public ActionResult EditGastoVehiculo(int IdGasto, int Id_Vehiculo, decimal CantidadGasto, int Kilometraje, string FecGasto)
        {
            try
            {
                string UserCreate = User.Identity.Name;

                DALGastosVehiculo.GastoVehiculo_dUp(Id_Vehiculo, IdGasto, FecGasto, Kilometraje, CantidadGasto, UserCreate);

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





        [HttpGet]
        public async Task<JsonResult> SaveTipoVehiculo(string tipoVehiculo,string flag,string comentarios)
        {
            try
            {
                string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
                if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
                {
                    conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
                }


                try
                {
                    Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

                    System.Collections.Hashtable parametros = new System.Collections.Hashtable();
                    //parametros.Add("@Id_proveedor", 1);
                    //parametros.Add("@descripcionServicioField", t.DescripcionServicio);
                    //parametros.Add("@tipoEnvioResField", t.TipoEnvioRes);
                    //parametros.Add("@aplicaCotizacionField", t.AplicaCotizacion);
                    //parametros.Add("@tarifaBaseField", t.TarifaBase);
                    //parametros.Add("@cCTarifaBaseField", t.CCTarifaBase);
                    //parametros.Add("@cargosExtraField", t.CargosExtra);
                    //parametros.Add("@sobrePesoField", t.SobrePeso);
                    //parametros.Add("@cCSobrePesoField", t.CCSobrePeso);
                    //parametros.Add("@costoTotalField", t.CostoTotal);
                    //parametros.Add("@pesoField", t.Peso);
                    parametros.Add("@Descripcion", tipoVehiculo);


                    Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[up_CorpTMS_Ins_TipoVehiculo]", false, parametros);


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

        public DataSet GetTipoVehiculos() {

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }

            Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

            System.Collections.Hashtable parametros = new System.Collections.Hashtable();
            
            return Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[up_CorpTMS_Sel_TipoVehiculo]", false, parametros);



        }


    }
}