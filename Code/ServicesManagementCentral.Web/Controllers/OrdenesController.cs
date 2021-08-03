using Newtonsoft.Json;
using ServicesManagement.Web.Models;
using Soriana.OMS.Ordenes.Common.DTO;
using System;
using System.Collections.Generic;
using System.Data;
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
    [Authorize]
    public class CodigoModels
    {

        public string ItemId { get; set; } //": "7501055313594",
        public string Description { get; set; } //": "REFRESCO COCA COLA 473 ML LAT(NO_ACT)",
        public string NormalPrice { get; set; } //": 8.5,
        public string OfferPrice { get; set; } //": 8.5,
        public string ForeignExchangeRate { get; set; } //": 19.8,
        public string ForeignNormalPrice { get; set; } //": 168.3,
        public string ForeignOfferPrice { get; set; } //": 0.0,
        public string Id_Num_SKU { get; set; } //": "3059640",
        public List<TemplatesModels> Templates { get; set; }
    }

    public class TemplatesModels
    {
        public string Id { get; set; }
        public string Template { get; set; }
    }

    public class InCodigoModels
    {

        public string storeId { get; set; }
        public string itemId { get; set; }

    }

    [Authorize]
    public class OrdenesController : Controller
    {
        string UrlApi = "";

        // GET: Ordenes
        public ActionResult Index()

        {
            try
            {
                Session["listaUN"] = DALServicesM.GetUN();

                return View();
            }
            catch (Exception x)
            {
                Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.ERROR, "", false, x);
                throw;
            }
        }



        public ActionResult SeleccionTda(string un = "", string desc_un = "")

        {

            Session["Id_Num_UN"] = un;

            Session["Desc_Num_UN"] = desc_un;



            return RedirectToAction("OrdenSeleccionada");

        }



        public ActionResult OrdenSeleccionada()



        {

            if (Session["Id_Num_UN"] != null)

            {

                string un = Session["Id_Num_UN"].ToString();





                Session["listaOrdersSurtir"] = DALServicesM.GetListaSurtir(un);

                Session["listaOrdersEmbarcar"] = DALServicesM.GetListaEmbarcar(un);

                Session["listTrans"] = DALServicesM.GetCarriers();



            }

            else

            {

                return RedirectToAction("Index", "Ordenes");

            }



            return View();



        }

        public ActionResult OrdenDetalle(string order)
        {

            if (!string.IsNullOrEmpty(order) & Session["Id_Num_UN"] != null)
            {
                Session["OrderSelected"] = DALServicesM.GetOrdersByOrderNo(order);
                Session["listS"] = DALServicesM.GetSurtidores(Session["Id_Num_UN"].ToString());
            }
            else
            {
                return RedirectToAction("Index", "Ordenes");
            }

            return View();
        }

        public ActionResult DespOrden()
        {
            return View();
        }

        public ActionResult GetOrderDetalle(string order)
        {
            if (!string.IsNullOrEmpty(order))
            {
                System.Data.DataSet ds = DALServicesM.GetOrdersByOrderNo(order);

                Session["OrderSelected"] = ds;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    var result = new { Success = true, message = Url.Action("ConsultaDetalle", "Ordenes") };

                    return Json(result, JsonRequestBehavior.AllowGet);

                    //RedirectToAction("ConsultaDetalle", "Ordenes", new { order = order });
                }
                else
                {
                    var result = new { Success = false, Message = "Alta exitosa" };

                    return Json(result, JsonRequestBehavior.AllowGet);
                }


            }
            return View();

        }

        [HttpPost]
        public async Task<JsonResult> FinalizarSurtido(string OrderNo, string tId, string trans, string ue, string store, string status, string manual)
        {

            try
            {

                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["api_FinalizarSurtido"];
                //using (HttpClient client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri(apiUrl);
                //    client.DefaultRequestHeaders.Accept.Clear();
                //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                //    var in_data = "{\"order-no\": {0},\"surtidor-orden\":{\"surtidor-id\":{1},\"surtidor-name\":\"{2}\"}}".Replace("{0}", OrderNo).Replace("{1}", tId).Replace("{2}", trans);

                //                    var in_data2 = "{	\"order\":    {\"order-no\":\"{0}\",\"order\":\"\",\"store-num\":0,\"ue-no\":\"\",\"status-ue\":\"\",\"order-date\":\"\",\"order-delivery-date\":\"\",\"created-by\":\"\",\"delivery-type\":\"\",\"ue-type\":\"\",\"additionalPoints\":\"\",\"redeemedPoints\":\"\" "
                //+"     ,\"ismanualpicking\":  "
                //+ "	}  "
                //+ "    ,\"payment\":{ \"method - payment\":\"\",\"card - number\":}             "
                //+ "    ,\"customer\":{ \"customer - no\":\"\",\"customer - name\":\"\",\"phone\":\"\"}             "
                //+ "    ,\"shipments\":{ \"address1\":\"\",\"address2\":\"\",\"city\":\"\",\"state - code\":\"\",\"postal - code\":\"\",\"reference\":\"\",\"name - receives\":\"\"}             "
                //+ "	,\"product - lineitem - to - supply\":[]             "
                //+ "	,\"product-lineitem-assorted\":[]             "
                //+ "	,\"detail-payment\":{\"total\":0.0,\"num-points\":0,\"num-cashier\":0,\"num-pos\":0,\"transaction-id\":0}             "
                //+ "	,\"shipper\":{\"shipper-name\":null,\"shipping-date\":null,\"transaction-no\":null,\"traking-no\":null,\"num-bags\":0,\"num-coolers\":0,\"num-containers\":0,\"terminal\":null}             "
                //+ "	,\"delivery\":{\"delivery-date\":null,\"id-receive\":null,\"name-receive\":null,\"comments\":null}             "
                //+ "	,\"orden-pos\":{             "
                //+ "									\"order-status\":{\"order-no\":,\"store-num\":,\"order-status\":,\"order-status-desc\":\"\",\"order-date-mov\":\"\",\"order-rounding\":0}             "
                //+ "									,\"products\":[]             "
                //+ "									,\"method-payment\":{\"order-no\":,\"payment-method-id\":,\"payment-method-desc\":\"\",\"payment-method-account\":\"\",\"payment-method-amount\":0.00,\"payment-method-exp-date\":\"\",\"payment-method-bit-encrypted\":false,\"payment-method-encrypt-account\":\"\",\"payment-method-encrypt-exp-date\":\"\",\"payment-method-id-key\":\"\"}             "
                //+ "									,\"client\":{             "
                //+ "												\"order-no\":,\"customer-no\":,\"customer-first-name\":\"\",\"customer-lastname1\":\"\",\"customer-lastname2\":\"\",\"customer-personal-id\":\"\",\"customer-total-points\":0}             "
                //+ "									,			\"promotions\":[{\"pos-barcode\":0,\"order-no\":{0},\"pos-additional-points\":0,\"pos-accum-points-exch\":0.0,\"pos-redemption-points\":0,\"pos-amount-exch\":0.0,\"pos-percent-applied\":0.0}]             "
                //+ "								  }   "
                //+ "	,\"surtidor-orden\":{\"surtidor-id\":{1},\"surtidor-name\":\"{2}\"}  "
                //+ "}".Replace("{0}", OrderNo).Replace("{1}", tId).Replace("{2}", trans); 



                //InformacionOrden o = new InformacionOrden();

                //o.Orden = new InformacionDetalleOrden();
                //o.Orden.NumeroOrden = OrderNo;
                //o.Surtidor = new InformacionSurtidor();
                //o.Surtidor.SurtidorID = Convert.ToInt32(tId);
                //o.Surtidor.NombreSurtidor = trans;




                //    HttpContent c = new StringContent(JsonConvert.SerializeObject(o).ToString(), System.Text.Encoding.UTF8, "application/json");


                //    Soriana.FWK.Log.clsLogManagerFWK.WriteMessage_Loger(JsonConvert.SerializeObject(o).ToString());

                //    //Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.INFO, "in_data: " + in_data2, false, null);
                //    //Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.INFO, "in_data: " + JsonConvert.SerializeObject(o).ToString(), false, null);


                //    Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.INFO, "Url: " + apiUrl, false, null);


                //    //HttpResponseMessage response = await client.GetAsync(apiUrl);
                //    Uri u = new Uri(apiUrl);

                //    HttpRequestMessage request = new HttpRequestMessage
                //    {
                //        Method = HttpMethod.Post,
                //        RequestUri = u,
                //        Content = c
                //    };

                //    HttpResponseMessage resultApi = await client.SendAsync(request);


                //    if (resultApi.IsSuccessStatusCode)
                //    {
                //        var data = await resultApi.Content.ReadAsStringAsync();

                //        Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.INFO, "IsSuccessStatusCode :" + data, false, null);

                //        // var jsonResult = JsonConvert.DeserializeObject(data).ToString();

                //        //var resp = JsonConvert.DeserializeObject<List<SupplierModel>>(data);



                //        var result2 = new { Success = true };
                //        return Json(result2, JsonRequestBehavior.AllowGet);
                //    }
                //    else //web api sent error response 
                //    {
                //        //log response status here..
                //        Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.INFO, "IsSuccessStatusCode False: " + resultApi.StatusCode, false, null);

                //        var result1 = new { Success = false, Message = resultApi.StatusCode };
                //        return Json(result1, JsonRequestBehavior.AllowGet);

                //    }

                //}




                //metodo mio
                InformacionOrden o = new InformacionOrden();

                o.Orden = new InformacionDetalleOrden();
                o.Orden.NumeroOrden = OrderNo;
                o.Orden.EsPickingManual = manual.Equals("1") ? true : false;
                o.Orden.EstatusUnidadEjecucion = status;
                o.Orden.NumeroUnidadEjecucion = ue;
                o.Orden.NumeroTienda = Convert.ToInt32(store);
                o.Surtidor = new InformacionSurtidor();
                o.Surtidor.SurtidorID = Convert.ToInt32(tId);
                o.Surtidor.NombreSurtidor = trans;


                string json2 = string.Empty;
                JavaScriptSerializer js = new JavaScriptSerializer();
                //json2 = js.Serialize(o);


                json2 = JsonConvert.SerializeObject(o);

                js = null;

                Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.INFO, "in_data: " + json2, false, null);

                Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.INFO, "Request: " + apiUrl, false, null);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Soriana.FWK.FmkTools.RestResponse r = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, System.Configuration.ConfigurationSettings.AppSettings["api_FinalizarSurtido"], "", json2);

                //Soriana.FWK.FmkTools.RestResponse r = Soriana.FWK.FmkTools.RestClient.RequestAzureMS(Soriana.FWK.FmkTools.HttpVerb.POST, System.Configuration.ConfigurationSettings.AppSettings["api_FinalizarSurtido"], "", json2);

                //var r =RestClient_2.RestClient_2.RequestAzure(System.Configuration.ConfigurationSettings.AppSettings["api_FinalizarSurtido"].ToString(), json2);



                //RestClient_2.Rest_3 r3 = new RestClient_2.Rest_3(f);



                //var client = new  WebMvc.Content.RestClient();

                //client.EndPoint = System.Configuration.ConfigurationSettings.AppSettings["api_FinalizarSurtido"];
                //client.Method = HttpVerb.POST;
                //client.PostData = json2;

                //var json = client.MakeRequest();

                //string respon = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(json);


                var result = new { Success = true, Message = "Alta exitosa" };

                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception x)
            {
                Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.ERROR, "", false, x);

                var result = new { Success = false, Message = x.Message };

                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }

        //[HttpPost]
        //public async Task<JsonResult> FinalizarTransportista(string OrderNo, string tId, string trans, string ue, string store, string status, string manual)
        //{

        //    try
        //    {

        //        string apiUrl = System.Configuration.ConfigurationManager.AppSettings["api_FinalizarSurtido"];

        //        //metodo mio
        //        InformacionOrden o = new InformacionOrden();

        //        o.Orden = new InformacionDetalleOrden();
        //        o.Orden.NumeroOrden = OrderNo;
        //        o.Orden.EsPickingManual = manual.Equals("1") ? true : false;
        //        o.Orden.EstatusUnidadEjecucion = status;
        //        o.Orden.NumeroUnidadEjecucion = ue;
        //        o.Orden.NumeroTienda = Convert.ToInt32(store);
        //        o.Surtidor = new InformacionSurtidor();
        //        o.Surtidor.SurtidorID = Convert.ToInt32(tId);
        //        o.Surtidor.NombreSurtidor = trans;





        //        string json2 = string.Empty;
        //        JavaScriptSerializer js = new JavaScriptSerializer();
        //        json2 = js.Serialize(o);
        //        js = null;


        //        Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.INFO, "Request: " + apiUrl, false, null);

        //        Soriana.FWK.FmkTools.RestResponse r = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, System.Configuration.ConfigurationSettings.AppSettings["api_FinalizarSurtido"], "", json2);

        //        Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.INFO, "Response : " + r.code + "-Message : " + r.message, false, null);



        //        //var client = new  WebMvc.Content.RestClient();

        //        //client.EndPoint = System.Configuration.ConfigurationSettings.AppSettings["api_FinalizarSurtido"];
        //        //client.Method = HttpVerb.POST;
        //        //client.PostData = json2;

        //        //var json = client.MakeRequest();

        //        //string respon = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(json);


        //        var result = new { Success = true, Message = "Alta exitosa" };

        //        return Json(result, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (Exception x)
        //    {
        //        Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.ERROR, "", false, x);

        //        var result = new { Success = false, Message = x.Message };

        //        return Json(result, JsonRequestBehavior.AllowGet);

        //    }
        //}




        //public bool RestClient(string urlApi, string postJSONdata) {


        //    using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, urlApi))
        //    {
        //        if (!string.IsNullOrEmpty("application/xml, application/json"))
        //            requestMessage.Headers.Add(HttpClientConstants.HTTP_CLIENT_HEADER_ACCEPT, "application/xml, application/json");
        //        if (!string.IsNullOrEmpty(""))
        //            requestMessage.Headers.Add(HttpClientConstants.HTTP_CLIENT_HEADER_CONTENT_TYPE, "");
        //        if (!string.IsNullOrEmpty("FinalizarSurtido"))
        //            requestMessage.Headers.Add(HttpClientConstants.HTTP_CLIENT_HEADER_USER_AGENT, "FinalizarSurtido");
        //        System.Net.Http.HttpClient client = new HttpClient();// _HttpClientFactory.CreateClient(_HttpClientOptions.EndPointName);
        //        requestMessage.Content = new StringContent(postJSONdata);
        //        HttpResponseMessage reponseMessage = client.SendAsync(requestMessage).Result;
        //        if (reponseMessage.IsSuccessStatusCode)
        //            return true;
        //        else
        //            return false;
        //    }

        //}

        [HttpPost]
        public async Task<JsonResult> FinalizarEmbarque(string OrderNo, string tId, string trans, string ue, string store, string status, string bolsas, string contenedores, string hieleras)
        {

            try
            {

                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["api_FinalizarEmbarque"];

                //metodo mio
                InformacionOrden o = new InformacionOrden();

                o.Orden = new InformacionDetalleOrden();
                o.Orden.NumeroOrden = OrderNo;


                o.Orden.EstatusUnidadEjecucion = status;
                o.Orden.NumeroUnidadEjecucion = ue;
                o.Orden.NumeroTienda = Convert.ToInt32(store);
                o.Expedidor.NombreExpedidor = "";


                o.Expedidor.NumeroBolsas = Convert.ToInt32(bolsas);
                o.Expedidor.NumeroContenedores = Convert.ToInt32(contenedores);
                o.Expedidor.NumeroEnfriadores = Convert.ToInt32(hieleras);


                string json2 = string.Empty;
                JavaScriptSerializer js = new JavaScriptSerializer();
                //json2 = js.Serialize(o);
                js = null;
                json2 = JsonConvert.SerializeObject(o);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.INFO, "in_data: " + json2, false, null);

                Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.INFO, "Request: " + apiUrl, false, null);

                Soriana.FWK.FmkTools.RestResponse r = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, System.Configuration.ConfigurationSettings.AppSettings["api_FinalizarEmbarque"], "", json2);

                Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.INFO, "Response : " + r.code + "-Message : " + r.message, false, null);

                var result = new { Success = true, Message = "Alta exitosa" };

                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception x)
            {
                Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.ERROR, "", false, x);

                var result = new { Success = false, Message = x.Message };

                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public async Task<JsonResult> FinalizarTransportista(string OrderNo, string tId, string trans, string ue, string store, string status)
        {

            try
            {
                System.Data.DataSet d = DALServicesM.GetOrdersByOrderNo(OrderNo);

                foreach (System.Data.DataRow r1 in d.Tables[0].Rows)
                {

                    status = r1["StatusUe"].ToString();
                    ue = r1["UeNo"].ToString();
                    store = r1["StoreNum"].ToString();

                }


                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["api_FinalizarTrans"];

                //metodo mio
                InformacionOrden o = new InformacionOrden();

                o.Orden = new InformacionDetalleOrden();
                o.Orden.NumeroOrden = OrderNo;


                o.Orden.EstatusUnidadEjecucion = status;
                o.Orden.NumeroUnidadEjecucion = ue;
                o.Orden.NumeroTienda = Convert.ToInt32(store);
                o.Expedidor.NombreExpedidor = trans;


                //o.Expedidor.NumeroBolsas = 1;
                //o.Expedidor.NumeroContenedores = 1;
                //o.Expedidor.NumeroEnfriadores = 1;


                string json2 = string.Empty;
                JavaScriptSerializer js = new JavaScriptSerializer();
                //json2 = js.Serialize(o);
                js = null;
                json2 = JsonConvert.SerializeObject(o);
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.INFO, "in_data: " + json2, false, null);


                Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.INFO, "Request: " + apiUrl, false, null);

                Soriana.FWK.FmkTools.RestResponse r = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, System.Configuration.ConfigurationSettings.AppSettings["api_FinalizarTrans"], "", json2);

                Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.INFO, "Response : " + r.code + "-Message : " + r.message, false, null);

                var result = new { Success = true, Message = "Alta exitosa" };

                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception x)
            {
                Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.ERROR, "", false, x);

                var result = new { Success = false, Message = x.Message };

                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public async Task<JsonResult> FinalizarEntrega(string OrderNo, string tId, string trans, string ue, string store, string status, string ide, string fechaD, string timeD, string comments, string flagE)
        {

            try
            {
                System.Data.DataSet d = DALServicesM.GetOrdersByOrderNo(OrderNo);

                foreach (System.Data.DataRow r1 in d.Tables[0].Rows)
                {

                    status = r1["StatusUe"].ToString();
                    ue = r1["UeNo"].ToString();
                    store = r1["StoreNum"].ToString();

                }


                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["api_Finaliza_Entrega"];

                //metodo mio
                InformacionOrden o = new InformacionOrden();

                o.Orden = new InformacionDetalleOrden();
                o.Orden.NumeroOrden = OrderNo;


                o.Orden.EstatusUnidadEjecucion = status;
                o.Orden.NumeroUnidadEjecucion = ue;
                o.Orden.NumeroTienda = Convert.ToInt32(store);
                o.Expedidor.NombreExpedidor = trans;

                o.Entrega.IdentificadorQuienRecibe = ide;
                o.Entrega.FechaEntrega = fechaD + " " + timeD;

                o.Entrega.Comentarios = comments;
                o.Entrega.NombreQuienRecibe = ide;


                //o.Expedidor.NumeroBolsas = 1;
                //o.Expedidor.NumeroContenedores = 1;
                //o.Expedidor.NumeroEnfriadores = 1;


                string json2 = string.Empty;
                JavaScriptSerializer js = new JavaScriptSerializer();
                //json2 = js.Serialize(o);
                js = null;
                json2 = JsonConvert.SerializeObject(o);
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.INFO, "in_data: " + json2, false, null);


                Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.INFO, "Request: " + apiUrl, false, null);

                Soriana.FWK.FmkTools.RestResponse r = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, System.Configuration.ConfigurationSettings.AppSettings["api_Finaliza_Entrega"], "", json2);

                Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.INFO, "Response : " + r.code + "-Message : " + r.message, false, null);

                var result = new { Success = true, Message = "Alta exitosa" };

                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception x)
            {
                Soriana.FWK.FmkTools.LoggerToFile.WriteToLogFile(Soriana.FWK.FmkTools.LogModes.LogError, Soriana.FWK.FmkTools.LogLevel.ERROR, "", false, x);

                var result = new { Success = false, Message = x.Message };

                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult ConsultaDetalle(string order)
        {

            if (!string.IsNullOrEmpty(order))
            {
                Session["OrderSelected"] = DALServicesM.GetOrdersByOrderNo(order);




            }
            return View();

        }

        public ActionResult Embarque(string order)
        {


            if (!string.IsNullOrEmpty(order))
            {
                Session["OrderSelected"] = DALServicesM.GetOrdersByOrderNo(order);




            }
            return View();

        }

        public ActionResult RecepcionGuiaEmbarque(string order)
        {


            if (!string.IsNullOrEmpty(order))
            {
                Session["OrderSelected"] = DALServicesM.GetOrdersByOrderNo(order);




            }
            return View();

        }


        [HttpGet]
        public List<UNModel> ListadoTdas()
        {

            try
            {
                //string Id_Num_UN = "24";
                //var rest = new Soriana.FWK.Datos.WS.RestClient();
                //// string apiUrl = string.Format("{0}api/Monitor/ListadoTdas", urlCat);

                //string apiUrl = string.Format("{0}/api/Oredenes/Getun", UrlApi);
                //rest.Url = apiUrl;

                //rest.Method = "GET";
                //rest.ContentType = "application/json";
                //rest.Accept = "application/json";

                //var jsonResponse = rest.GetResponse();

                var jsonResponse = "";

                var res = JsonConvert.DeserializeObject<List<UNModel>>(jsonResponse);

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonResult> GetCodigoApi(string cod,string cant,string clave,string medida,string action,string order)
        {
            try
            {
                InCodigoModels c = new InCodigoModels();
                c.storeId = Session["Id_Num_UN"].ToString();
                c.itemId = cod;

                string json2 = string.Empty;
                JavaScriptSerializer js = new JavaScriptSerializer();
                //json2 = js.Serialize(o);
                js = null;
                json2 = JsonConvert.SerializeObject(c);
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Soriana.FWK.FmkTools.RestResponse r = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, System.Configuration.ConfigurationSettings.AppSettings["api_GetCodigo"], "", json2);

                CodigoModels codigo = JsonConvert.DeserializeObject<CodigoModels>(r.message);

                InformacionOrden o = new InformacionOrden();

                InformacionProductoSuministrar p = new InformacionProductoSuministrar();

                p.Cantidad = Convert.ToDouble(cant);
                p.CodigoBarra = cod;
                p.DescripcionArticulo = codigo.Description;
                p.FueSuministrado = false;
                p.NumeroOrden = order;
                p.Precio = Convert.ToDouble(codigo.NormalPrice);
                p.UnidadMedida = medida;
                p.IdentificadorProducto = codigo.Id_Num_SKU;

                o.ProductosSuministrar.Add(p);
                 

                

                var result = new { Success = true, json = codigo.Description };
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };

                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }


        #region Traspaso

        [HttpGet]
        public async Task<JsonResult> GetOrdenesTraspaso(string Id_Num_Un)
        {

            try
            {

                string apiUrl = string.Format("{0}api/Oredenes/GetOder?Id_Num_Un={1}", UrlApi, Id_Num_Un);


                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        // var jsonResult = JsonConvert.DeserializeObject(data).ToString();

                        var resp = JsonConvert.DeserializeObject<List<OrderSelected>>(data);
                        var result = new { Success = true, json = resp };
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else //web api sent error response 
                    {
                        //log response status here..

                        var result = new { Success = false, Message = response.StatusCode };
                        return Json(result, JsonRequestBehavior.AllowGet);

                    }

                }

            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpGet]
        public async Task<JsonResult> GetOrdenesByOrderNoTras(string OrderNo)
        {

            try
            {

                string apiUrl = string.Format("{0}api/Oredenes/GetOderByOrderNo?OrderNo={1}", UrlApi, OrderNo);


                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        // var jsonResult = JsonConvert.DeserializeObject(data).ToString();

                        OrderSelected resp = JsonConvert.DeserializeObject<OrderSelected>(data);

                        //orden.CustomerName = "Pedro Castillo Gonzalez";
                        //orden.Phone = "8119652099";
                        //orden.Address1 = "Villas de Belgica 277 Colonia Roble Nuevo";
                        //orden.Address2 = "";
                        //orden.StateCode = "";
                        //orden.PostalCode = "66055";

                        var result = new
                        {
                            Success = true
                                            ,
                            CustomerName = resp.CustomerName
                                            ,
                            Phone = resp.Phone
                                            ,
                            Address1 = resp.Address1
                                            ,
                            PostalCode = resp.PostalCode

                        };
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else //web api sent error response 
                    {
                        //log response status here..

                        var result = new { Success = false, Message = response.StatusCode };
                        return Json(result, JsonRequestBehavior.AllowGet);

                    }

                }

            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }




        public JsonResult GetTdasTaspaso()
        {
            try
            {
                var tdas = ListadoTdas();
                //var data = JsonConvert.SerializeObject<List<UNModel>>(tdas);
                var result = new { Success = true, json = tdas };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }

        #endregion

        #region Etiquetas
        [HttpPost]
        public async Task<JsonResult> AddEtiqueta(string OrderNo, string Posicion)
        {

            try
            {



                var content = new EtiquetaModel
                {
                    OrderNo = int.Parse(OrderNo)
                    ,
                    Posicion = int.Parse(Posicion)
                };
                var json = JsonConvert.SerializeObject(content);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                string apiUrl = string.Format("{0}api/Oredenes/AddEtiqueta", UrlApi);


                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));



                    HttpResponseMessage response = await client.PostAsync(apiUrl, stringContent);
                    if (response.IsSuccessStatusCode)
                    {
                        //var data = await response.Content.ReadAsStringAsync();
                        // var jsonResult = JsonConvert.DeserializeObject(data).ToString();

                        //var resp = JsonConvert.DeserializeObject<List<SupplierModel>>(data);
                        var result = new { Success = true };
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else //web api sent error response 
                    {
                        //log response status here..

                        var result = new { Success = false, Message = response.StatusCode };
                        return Json(result, JsonRequestBehavior.AllowGet);

                    }

                }

            }
            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }
        #endregion

        #region Actualiza Forma de Pago

        [HttpGet]
        public JsonResult GetMotivoCambioFP()

        {
            try
            {
                DataSet ds = DALServicesM.GetMotivoCambioFP();

                var listC = ConvertTo<MotivoCambioFP>(ds.Tables[0]);

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
        public JsonResult UpdateFormaPago(string Id_Num_Orden, string Id_Num_MotCmbFP, string Obs_CambioFP = "")

        {
            try
            {
                DataSet ds = DALServicesM.UpdFormaPago(int.Parse(Id_Num_Orden), int.Parse(Id_Num_MotCmbFP), Obs_CambioFP);

                var result = new { Success = true, json = "Cambio con éxito" };
                return Json(result, JsonRequestBehavior.AllowGet);

            }

            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #region Observaciones
        [HttpPost]
        public JsonResult DelObservaciones(string OrderNo, string Id_Cnsc_CarObs, string CnscOrder)
        {
            try
            {
                DataSet ds = DALServicesM.DelObservaciones(int.Parse(OrderNo), int.Parse(Id_Cnsc_CarObs), int.Parse(CnscOrder));

                var result = new { Success = true, json = "Cambio con éxito" };
                return Json(result, JsonRequestBehavior.AllowGet);

            }

            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult AddObservaciones(string OrderNo, string Desc_CarObs)
        {
            try
            {
                DataSet ds = DALServicesM.AddObservaciones(int.Parse(OrderNo), Desc_CarObs);

                var result = new { Success = true, json = "Cambio con éxito" };
                return Json(result, JsonRequestBehavior.AllowGet);

            }

            catch (Exception ex)
            {
                var result = new { Success = false, Message = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #region ListaPasillosEspeciales
        public ActionResult ListaPasillosEsp(string order)
        {

            if (!string.IsNullOrEmpty(order) & Session["Id_Num_UN"] != null)
            {
                Session["OrderPasillos"] = DALServicesM.GetPasillosEspeciales(int.Parse(order));
            }
            else
            {
                return RedirectToAction("Index", "Ordenes");
            }

            return View();
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

    }
}