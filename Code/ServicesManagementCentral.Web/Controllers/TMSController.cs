using System;
using System.Collections.Generic;
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

                OperadorModel v = new OperadorModel { Nombre = nombre,Usuario = user,Pass = pass,Matricula = matricula};

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
        public async Task<JsonResult> EditOperador(string Id, string nombre,string user,string pass ,string matricula, string estatus)
        {
            try
            {
                string apiUrl = string.Format("{0}/UpdOperador", UrlApi);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                OperadorModel v = new OperadorModel { Id_Transportista = Convert.ToInt32(Id), Matricula = matricula,Usuario = user,Pass = pass, Nombre = nombre, Estatus = estatus.Equals("1") ? true : false };

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
                else {
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

    }
}