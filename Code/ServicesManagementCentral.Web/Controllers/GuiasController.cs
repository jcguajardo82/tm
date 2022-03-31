using Newtonsoft.Json;
using ServicesManagement.Web.DAL;
using ServicesManagement.Web.Helpers;
using ServicesManagement.Web.Models;
using ServicesManagement.Web.Models.Cotizador;
using ServicesManagement.Web.Models.Estafeta;
using ServicesManagement.Web.Models.Guias;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ServicesManagement.Web.Controllers
{
    public class GuiasController : Controller
    {
        [HttpGet]
        public ActionResult Cotizar(string UeNo, string IdTrackingService)
        {
            int OrderNo=0, IdTracking = 0;
            try
            {
                var lstCabecera = DataTableToModel.ConvertTo<Cabecera>(DALGuias.ConsultarCabecera(UeNo, IdTrackingService).Tables[0]);
                OrderNo = lstCabecera.FirstOrDefault().OrderNo;
                IdTracking = lstCabecera.FirstOrDefault().IdTracking;

                var Products = DataTableToModel.ConvertTo<ProductEmbalaje>(DALGuias.ConsultarProductosByUeno(UeNo,IdTracking).Tables[0]);
                int enviaCom = int.Parse(DALGuias.ActiveEnviaCom().Tables[0].Rows[0][0].ToString());
                List<CarrierRequest> lstCarrierRequests = new List<CarrierRequest>();

                DALGuias.EliminarTarifasAnteriores(UeNo, OrderNo);
                CrearCotizacionesLogyt(Products, OrderNo);

                decimal decimalPeso = decimal.Round(decimal.Parse(Session["SumPeso"].ToString()));
                int peso = decimal.ToInt32(decimalPeso);

                if (enviaCom == 1 && peso <= 70)
                {
                    DataSet dsCarriers = DALGuias.CarriersPorTransportista("envia.com");
                    foreach (DataRow row in dsCarriers.Tables[0].Rows)
                    {
                        var tarifaCarrier = CreateGuiaCotizador(UeNo, OrderNo, 1, row["Carrier"].ToString(), peso);

                        if (tarifaCarrier.Carrier != null)
                        {
                            DALGuias.GuardarTarifas(UeNo, OrderNo, tarifaCarrier.msj);
                            lstCarrierRequests.Add(tarifaCarrier);
                        }
                    }
                    Session["lstCarrierRequest"] = lstCarrierRequests;
                }

                //DataSet dsCarrier = DALGuias.CarrierRates(OrderNo);
                //var lstCarriers = DataTableToModel.ConvertTo<CarriersRates>(DALGuias.CarrierRates(OrderNo).Tables[0]);
                DataSet ds = DALGuias.CarrierRates(OrderNo);
                List<CarriersRates> lstCarriers = new List<CarriersRates>();
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        lstCarriers.Add(
                        new CarriersRates
                        {
                            carrier = item[0].ToString(),
                            Service = item[1].ToString(),
                            BitAsegurado = item[2].ToString() == "1" ? true : false,
                            Montoasegurado = double.Parse(item[3].ToString()),
                            diasEntrega = int.Parse(item[4].ToString()),
                            totalPrice = decimal.Parse(item[5].ToString()),
                            NivelServicio = decimal.Parse(item[6].ToString()),
                            Prioridad= int.Parse(item[7].ToString()),
                            cotizeId = long.Parse(item[8].ToString())
                        }
                        ) ;
                    }
                }
                Session["lstCarrierRates"] = lstCarriers;
                Session["lstProducts"] = Products;
                Session["UeNoSelected"] = UeNo;
                Session["OrderNoSelected"] = OrderNo;
                Session["IdTrackingServiceSelected"] = IdTrackingService;
                var result = new { Success = true, Carriers = lstCarriers, Cabecera = lstCabecera };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult GenerarGuia(string _cotizeId)
        {
            string  guia = string.Empty, servicioPaq = string.Empty, trackUrl = string.Empty, requestRecoleccion = string.Empty;
            string UeNo = string.Empty, IdTrackingService = string.Empty;
            int OrderNo = 0;
            try
            {
                IdTrackingService = Session["IdTrackingServiceSelected"].ToString();
                UeNo = Session["UeNoSelected"].ToString();
                OrderNo = int.Parse(Session["OrderNoSelected"].ToString());
                List<CarriersRates> lstCarriers = (List<CarriersRates>)Session["lstCarrierRates"];
                CarriersRates Carrier = lstCarriers.Where(x => x.cotizeId ==long.Parse(_cotizeId)).FirstOrDefault();
                List<ProductEmbalaje> Products = (List<ProductEmbalaje>)Session["lstProducts"];
                List<string> folios = new List<string>();
                //foreach (PackageMeasure item in Paquetes)
                //{
                    var FolioDisp = DALGuias.upCorpOms_Cns_NextTracking().Tables[0].Rows[0]["NextTracking"].ToString();
                    folios.Add(FolioDisp);
                    
                    decimal decimalPeso = decimal.Round(decimal.Parse(Session["SumPeso"].ToString()));
                    int peso = decimal.ToInt32(decimalPeso);
                    List<CarrierRequest> lstCarrierRequests = (List<CarrierRequest>)Session["lstCarrierRequest"];
                    int cotizeId = 0;
                        //paqueteria = row["Carrier"].ToString();
                        //service = row["Service"].ToString();
                    cotizeId = int.Parse(Carrier.cotizeId.ToString());

                        if (Carrier.carrier.Contains("Logyt"))
                        {
                            guia = CreateGuiaLogyt(UeNo, OrderNo, peso, 1);

                            servicioPaq = "Logyt-Estafeta"; //esta variable sera dinamica
                        }
                        if (Carrier.carrier.Equals("Estafeta"))
                        {
                            DataSet dsEstafeta = DALGuias.EstafetaActive();
                            if (dsEstafeta != null)
                            {
                                if (dsEstafeta.Tables[0].Rows[0][1].ToString().ToLower().Equals("estafeta"))
                                    guia = CreateGuiaEstafeta(UeNo, OrderNo, peso, 1);
                                else
                                    guia = CreateGuiaEstafetaAPI(UeNo, OrderNo, decimalPeso, Products, Carrier, null);
                            }
                            else
                                throw new Exception("No hay Canal de Estafeta");
                            
                            servicioPaq = "Soriana-Estafeta"; //esta variable sera dinamica
                        }
                        if (!Carrier.carrier.Equals("Estafeta") && !Carrier.carrier.Contains("Logyt"))
                        {
                            var request = lstCarrierRequests.Where(x => x.Carrier == Carrier.carrier).FirstOrDefault().requests[0];
                            guia = CreateGuiaEnvia(request, Carrier.Service, peso);
                            if (guia != "ERROR")
                            {
                                servicioPaq = "Envia-" + Carrier.carrier;
                                trackUrl = guia.Split(',')[2];
                                requestRecoleccion = lstCarrierRequests.Where(x => x.Carrier == Carrier.carrier).FirstOrDefault().requests[1];
                            }
                        }
                    //    if (guia != "ERROR")
                    //        break;
                    //}

                    if (guia == "ERROR")
                        throw new Exception("No se pudo generar la guia con la paqueteria elegida. ");

                    string GuiaEstatus = "CREADA";

                    var cabeceraGuia = DALGuias.upCorpOms_Ins_UeNoTracking(UeNo, OrderNo, FolioDisp, "Normal",
                    Products[0].Tipo, Products[0].Largo, Products[0].Ancho, Products[0].Alto, peso,
                    User.Identity.Name, servicioPaq, guia.Split(',')[0], guia.Split(',')[1], GuiaEstatus, null, trackUrl).Tables[0].Rows[0][0];
                    DALGuias.CarrierSelected(OrderNo, cotizeId);

                    if (!string.IsNullOrEmpty(requestRecoleccion))
                    {
                        var responseRecoleccion = RecoleccionEnvia(requestRecoleccion);

                        if (responseRecoleccion.Contains("carrier"))
                            DALGuias.GuardarPickUp(UeNo, OrderNo, FolioDisp, responseRecoleccion, User.Identity.Name, null, null, null);
                        else
                        {
                            RecoleccionRequestModel request = JsonConvert.DeserializeObject<RecoleccionRequestModel>(requestRecoleccion);
                            DALGuias.GuardarPickUp(UeNo, OrderNo, FolioDisp, "--", User.Identity.Name, Carrier.carrier, request.shipment.pickup.date, request.origin.postalCode);
                        }
                    }
                //}

                //DESCOMENTAR
                foreach (var folio in folios)
                {
                    foreach (var p in Products)
                    {
                        DALGuias.upCorpOms_Ins_UeNoTrackingDetail(UeNo, OrderNo, folio, "Normal",
                        p.ProductId, p.Barcode, p.ProductName, User.Identity.Name);
                    }

                }
                DALGuias.CancelacionGuia(UeNo, IdTrackingService, User.Identity.Name);
                var result = new { Success = true };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        private void CrearCotizacionesLogyt(List<ProductEmbalaje> Products, int orderNo)
        {
            string productsAll = string.Empty;
            bool bigTicket = false;
            decimal sumPeso = 0, width = 0, length = 0, heigth = 0;
            DataTable dt = DALGuias.GetTableProducts();

            foreach (var p in Products)
            {
                productsAll += p.ProductId.ToString() + ",";
            }
            List<WeightByProducts> lstPesos = DataTableToModel.ConvertTo<WeightByProducts>(DALGuias.GetDimensionsByProducts(productsAll).Tables[0]);

            Session["ListWeightByProducts"] = lstPesos;

            foreach (var item in lstPesos)
            {
                if (item.PesoVol > item.Peso)
                {
                    if (item.PesoVol > 70)
                        bigTicket = true;

                    var piezas = Products.Where(x => x.ProductId == item.Product).FirstOrDefault().Pieces;
                    sumPeso += (item.PesoVol * piezas);
                    width += item.Width;
                    length += item.Lenght;
                    heigth += item.Height;

                    dt.Rows.Add(item.Product, item.PesoVol, item.Cve_CategSAP, item.Cve_GciaCategSAP, item.Cve_GpoCategSAP, item.Desc_CategSAP);
                }
                else
                {
                    if (item.Peso > 70)
                        bigTicket = true;

                    var piezas = Products.Where(x => x.ProductId == item.Product).FirstOrDefault().Pieces;
                    sumPeso += (item.Peso * piezas);
                    width += item.Width;
                    length += item.Lenght;
                    heigth += item.Height;

                    dt.Rows.Add(item.Product, item.Peso, item.Cve_CategSAP, item.Cve_GciaCategSAP, item.Cve_GpoCategSAP, item.Desc_CategSAP);
                }


            }

            if (sumPeso < 1)
                sumPeso = 1;

            var widthRound = decimal.Round(width);
            if (widthRound > 1)
                Session["SumWidth"] = decimal.ToInt32(widthRound);
            else
                Session["SumWidth"] = 1;

            var lengthRound = decimal.Round(length);
            if (lengthRound > 1)
                Session["SumLength"] = decimal.ToInt32(lengthRound);
            else
                Session["SumLength"] = 1;

            var heightRound = decimal.Round(heigth);
            if (heightRound > 1)
                Session["SumHeigth"] = decimal.ToInt32(heightRound);
            else
                Session["SumHeigth"] = 1;

            Session["SumPeso"] = sumPeso;

            DALGuias.GuardarTarifasLogyt(orderNo, sumPeso, bigTicket, dt);

        }
        public CarrierRequest CreateGuiaCotizador(string UeNo, int OrderNo, int type, string carrier, int weight)
        {
            string[] _json = new string[2];
            DataSet ds = new DataSet();
            CarrierRequest carrierRequest = new CarrierRequest();

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
                parametros.Add("@carrier", carrier);
                parametros.Add("@type", type);

                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[dbo].[upCorpOms_Cns_UeNoCotizeInfo]", false, parametros);

            }
            catch (SqlException ex)
            {
                return carrierRequest;
            }
            catch (System.Exception ex)
            {
                return carrierRequest;
            }

            //if (weight > 70)
            //    _json = RequestLTLCotizador(ds);
            //else
            _json = RequestCotizador(ds);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


            Soriana.FWK.FmkTools.RestResponse r2 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, System.Configuration.ConfigurationSettings.AppSettings["api_Cotizador_Guia"], "", _json[0]);

            string msg = r2.message;

            if (msg.Contains("costSummary"))
            {
                CotizadorResponseModel re = JsonConvert.DeserializeObject<CotizadorResponseModel>(r2.message);
                carrierRequest.Carrier = carrier;
                carrierRequest.requests = _json;
                carrierRequest.msj = msg;
            }

            return carrierRequest;
        }
        private string RequestLTLCotizador(DataSet ds)
        {
            string _json = string.Empty;

            CotizadorRequestLTLModel requestCotizador = new CotizadorRequestLTLModel();

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                AddressCotzModel origin = new AddressCotzModel();

                origin.city = r["city"].ToString();
                origin.company = r["company"].ToString();
                origin.country = r["country"].ToString();
                origin.district = r["district"].ToString();
                origin.email = r["email"].ToString();
                origin.name = r["name"].ToString();
                origin.number = r["number"].ToString();
                origin.phone = r["phone"].ToString();
                origin.postalCode = r["postalCode"].ToString();
                origin.reference = r["reference"].ToString();
                origin.state = r["state"].ToString();
                origin.street = r["street"].ToString();

                CoordinatesModel coordinates = new CoordinatesModel();
                coordinates.latitudde = r["latitude"].ToString();
                coordinates.longitude = r["longitude"].ToString();

                origin.coordinates = coordinates;

                requestCotizador.origin = origin;

            }
            foreach (DataRow r in ds.Tables[1].Rows)
            {
                AddressCotzModel destination = new AddressCotzModel();

                destination.city = r["city"].ToString();
                destination.company = r["company"].ToString();
                destination.country = r["country"].ToString();
                destination.district = r["district"].ToString();
                destination.email = r["email"].ToString();
                destination.name = r["name"].ToString();
                destination.number = r["number"].ToString();
                destination.phone = r["phone"].ToString();
                destination.postalCode = r["postalCode"].ToString();
                destination.reference = r["reference"].ToString();
                destination.state = r["state"].ToString();
                destination.street = r["street"].ToString();

                CoordinatesModel coordinates = new CoordinatesModel();
                coordinates.latitudde = r["latitude"].ToString();
                coordinates.longitude = r["longitude"].ToString();

                destination.coordinates = coordinates;

                requestCotizador.destination = destination;
            }


            PackageModel packages = new PackageModel();
            packages.amount = 1;
            packages.content = "Mercancias Generales";
            packages.declaredValue = 0;
            packages.insurance = 0;
            var pesoRound = decimal.Round(Convert.ToDecimal(Session["SumPeso"].ToString()));
            var peso = decimal.ToInt32(pesoRound);
            packages.weight = peso;
            packages.weightUnit = "KG";
            packages.lenghtUnit = "CM";
            packages.type = "pallet";
            DimensionsModel dimensions = new DimensionsModel();
            dimensions.height = int.Parse(Session["SumHeigth"].ToString());
            dimensions.length = int.Parse(Session["SumLength"].ToString());
            dimensions.width = int.Parse(Session["SumWidth"].ToString());
            packages.dimensions = dimensions;

            List<PackageModel> lstPackages = new List<PackageModel>();
            lstPackages.Add(packages);

            requestCotizador.packages = lstPackages;

            foreach (DataRow r in ds.Tables[2].Rows)
            {
                ShipmentLTLModel shipment = new ShipmentLTLModel();

                shipment.carrier = r["carrier"].ToString();
                shipment.type = 2;

                Pickup pickup = new Pickup();
                pickup.timeFrom = "09:00";
                pickup.timeTo = "14:00";
                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);
                pickup.date = tomorrow.ToString("yyyy") + "-" + tomorrow.ToString("MM") + "-" + tomorrow.ToString("dd");

                shipment.pickup = pickup;

                requestCotizador.shipment = shipment;
            }

            foreach (DataRow r in ds.Tables[3].Rows)
            {
                SettingsModel settings = new SettingsModel();

                settings.cashOnDelivery = r["cashOnDelivery"].ToString();
                settings.comments = r["comments"].ToString();
                settings.currency = r["currency"].ToString();
                settings.printFormat = r["printFormat"].ToString();
                settings.printSize = r["printSize"].ToString();

                requestCotizador.settings = settings;
            }
            List<string> additionals = new List<string>(3);
            additionals.Add("pickup_appointment");
            additionals.Add("delivery_schedule");
            additionals.Add("dry");

            requestCotizador.additionalServices = additionals;

            _json = JsonConvert.SerializeObject(requestCotizador);


            return _json;
        }
        private string[] RequestCotizador(DataSet ds)
        {
            string _json = string.Empty, _jsonRecoleccion = string.Empty;
            string[] result = new string[2];

            CotizadorRequestModel requestCotizador = new CotizadorRequestModel();
            RecoleccionRequestModel requestRecoleccion = new RecoleccionRequestModel();

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                AddressCotzModel origin = new AddressCotzModel();

                origin.city = r["city"].ToString();
                origin.company = r["company"].ToString();
                origin.country = r["country"].ToString();
                origin.district = r["district"].ToString();
                origin.email = r["email"].ToString();
                origin.name = r["name"].ToString();
                origin.number = r["number"].ToString();
                origin.phone = r["phone"].ToString();
                origin.postalCode = r["postalCode"].ToString();
                origin.reference = r["reference"].ToString();
                origin.state = r["state"].ToString();
                origin.street = r["street"].ToString();

                requestRecoleccion.origin = origin;

                CoordinatesModel coordinates = new CoordinatesModel();
                coordinates.latitudde = r["latitude"].ToString();
                coordinates.longitude = r["longitude"].ToString();

                origin.coordinates = coordinates;

                requestCotizador.origin = origin;

            }
            foreach (DataRow r in ds.Tables[1].Rows)
            {
                AddressCotzModel destination = new AddressCotzModel();

                destination.city = r["city"].ToString();
                destination.company = r["company"].ToString();
                destination.country = r["country"].ToString();
                destination.district = r["district"].ToString();
                destination.email = r["email"].ToString();
                destination.name = r["name"].ToString();
                destination.number = r["number"].ToString();
                destination.phone = r["phone"].ToString();
                destination.postalCode = r["postalCode"].ToString();
                destination.reference = r["reference"].ToString();
                destination.state = r["state"].ToString();
                destination.street = r["street"].ToString();

                CoordinatesModel coordinates = new CoordinatesModel();
                coordinates.latitudde = r["latitude"].ToString();
                coordinates.longitude = r["longitude"].ToString();

                destination.coordinates = coordinates;

                requestCotizador.destination = destination;
            }

            foreach (DataRow r in ds.Tables[2].Rows)
            {
                ShipmentLTLModel shipment = new ShipmentLTLModel();

                shipment.carrier = r["carrier"].ToString();
                shipment.type = 1;

                Pickup pickup = new Pickup();
                pickup.timeFrom = "9";
                pickup.timeTo = "14";
                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);
                pickup.date = tomorrow.ToString("yyyy") + "-" + tomorrow.ToString("MM") + "-" + tomorrow.ToString("dd");
                pickup.instructions = "Recolectar en direccion indicada";
                pickup.totalPackages = 1;
                pickup.totalWeight = decimal.Round(Convert.ToDecimal(Session["SumPeso"].ToString()));

                shipment.pickup = pickup;

                requestRecoleccion.shipment = shipment;
            }

            PackageModel packages = new PackageModel();
            packages.amount = 1;
            packages.content = "Mercancias Generales";
            packages.declaredValue = 0;
            packages.insurance = 0;
            var pesoRound = decimal.Round(Convert.ToDecimal(Session["SumPeso"].ToString()));
            var peso = decimal.ToInt32(pesoRound);
            packages.weight = peso;
            packages.weightUnit = "KG";
            packages.lenghtUnit = "CM";
            packages.type = "box";
            DimensionsModel dimensions = new DimensionsModel();
            dimensions.height = int.Parse(Session["SumHeigth"].ToString());
            dimensions.length = int.Parse(Session["SumLength"].ToString());
            dimensions.width = int.Parse(Session["SumWidth"].ToString());
            packages.dimensions = dimensions;

            List<PackageModel> lstPackages = new List<PackageModel>();
            lstPackages.Add(packages);

            requestCotizador.packages = lstPackages;

            foreach (DataRow r in ds.Tables[2].Rows)
            {
                ShipmentModel shipment = new ShipmentModel();

                shipment.carrier = r["carrier"].ToString();
                shipment.type = int.Parse(r["type"].ToString());

                requestCotizador.shipment = shipment;
            }

            foreach (DataRow r in ds.Tables[3].Rows)
            {
                SettingsModel settings = new SettingsModel();

                settings.cashOnDelivery = r["cashOnDelivery"].ToString();
                settings.comments = r["comments"].ToString();
                settings.currency = r["currency"].ToString();
                settings.printFormat = r["printFormat"].ToString();
                settings.printSize = r["printSize"].ToString();

                requestCotizador.settings = settings;
            }
            SettingsRecoleccionModel settingsRecoleccionModel = new SettingsRecoleccionModel();
            settingsRecoleccionModel.currency = "MXN";
            settingsRecoleccionModel.labelFormat = "pdf";

            requestRecoleccion.settings = settingsRecoleccionModel;

            _json = JsonConvert.SerializeObject(requestCotizador);
            result[0] = _json;
            _jsonRecoleccion = JsonConvert.SerializeObject(requestRecoleccion);
            result[1] = _jsonRecoleccion;

            return result;
        }
        private string RequestEstafeta(DataSet ds, decimal weight, List<ProductEmbalaje> Products, CarriersRates Carrier, ShipmentToTrackingModel packageCEDIS, string UeNo)
        {
            string json = string.Empty;

            RequestEstafetaModel model = new RequestEstafetaModel();
            LabelDefinition label = new LabelDefinition();

            WayBillDocument wayBill = new WayBillDocument();
            wayBill.aditionalInfo = "Informacion Adicional";
            wayBill.content = "Contenido";
            wayBill.costCenter = "SPMXA12345";
            wayBill.customerShipmentId = null;
            var reference = ds.Tables[0].Rows[0]["addressReference1"].ToString() + " " + ds.Tables[0].Rows[0]["addressReference2"].ToString();
            wayBill.referenceNumber = reference.Length > 30 ? reference.Substring(0, 29) : reference;
            wayBill.groupShipmentId = null;

            label.wayBillDocument = wayBill;

            ItemDescription itemDescription = new ItemDescription();
            itemDescription.parcelId = 1;
            itemDescription.weight = weight;
            itemDescription.height = int.Parse(Session["SumHeigth"].ToString()) > 190 ? 190 : int.Parse(Session["SumHeigth"].ToString());
            itemDescription.length = int.Parse(Session["SumLength"].ToString()) > 240 ? 240 : int.Parse(Session["SumLength"].ToString());
            itemDescription.width = int.Parse(Session["SumWidth"].ToString()) > 120 ? 120 : int.Parse(Session["SumWidth"].ToString());

            Merchandises merchandises = new Merchandises();
            merchandises.totalGrossWeight = weight;
            merchandises.weightUnitCode = "KGM";

            List<Merchandise> lstMerchandises = new List<Merchandise>();

            if (Products != null)
            {
                foreach (var product in Products)
                {
                    var total = 0.0;
                    var quantity = 0.0;

                    Merchandise merchandise = new Merchandise();

                    foreach (DataRow r in ds.Tables[2].Rows)
                    {
                        if (r["PosBarCode"].ToString().Equals(product.Barcode.ToString()))
                        {
                            total = double.Parse(r["Total"].ToString());
                            quantity = double.Parse(r["PosBarCode"].ToString());

                        }
                    }
                    merchandise.merchandiseValue = total;
                    merchandise.currency = "MXN";
                    merchandise.productServiceCode = "01010101";
                    merchandise.merchandiseQuantity = product.Pieces;
                    merchandise.measurementUnitCode = "H87";
                    merchandise.tariffFraction = "12345678";
                    merchandise.UUIDExteriorTrade = "ABCDed02-a12A-B34B-c56C-c5abcdef61F2";
                    merchandise.isInternational = false;
                    merchandise.isImport = false;
                    merchandise.isHazardousMaterial = false;
                    merchandise.hazardousMaterialCode = "M0035";
                    merchandise.packagingCode = "4A";

                    lstMerchandises.Add(merchandise);
                }
            }
            else
            {
                var total = 0.0;
                var quantity = 0.0;

                Merchandise merchandise = new Merchandise();

                foreach (DataRow r in ds.Tables[2].Rows)
                {
                    if (r["PosBarCode"].ToString().Equals(packageCEDIS.barcode.ToString()))
                    {
                        total = double.Parse(r["Total"].ToString());
                        quantity = double.Parse(r["PosBarCode"].ToString());

                    }
                }
                merchandise.merchandiseValue = total;
                merchandise.currency = "MXN";
                merchandise.productServiceCode = "01010101";
                merchandise.merchandiseQuantity = packageCEDIS.piezas;
                merchandise.measurementUnitCode = "H87";
                merchandise.tariffFraction = "12345678";
                merchandise.UUIDExteriorTrade = "ABCDed02-a12A-B34B-c56C-c5abcdef61F2";
                merchandise.isInternational = false;
                merchandise.isImport = false;
                merchandise.isHazardousMaterial = false;
                merchandise.hazardousMaterialCode = "M0035";
                merchandise.packagingCode = "4A";

                lstMerchandises.Add(merchandise);
            }

            merchandises.merchandise = lstMerchandises;
            itemDescription.merchandises = merchandises;

            label.itemDescription = itemDescription;

            ServiceConfiguration serviceConfiguration = new ServiceConfiguration();
            serviceConfiguration.quantityOfLabels = 1;
            serviceConfiguration.serviceTypeId = "70";
            serviceConfiguration.salesOrganization = System.Configuration.ConfigurationManager.AppSettings["salesOrganization"];
            serviceConfiguration.effectiveDate = ds.Tables[1].Rows[0]["effectiveDate"].ToString();
            serviceConfiguration.originZipCodeForRouting = ds.Tables[1].Rows[0]["PostalCode"].ToString();
            // validar si tiene seguro---------------
            if (Carrier.Montoasegurado > 0)
            {
                serviceConfiguration.isInsurance = true;

                Insurance insurance = new Insurance();
                insurance.contentDescription = "Mercancias Generales";
                insurance.declaredValue = Carrier.Montoasegurado;

                serviceConfiguration.insurance = insurance;

            }
            else
                serviceConfiguration.isInsurance = false;


            serviceConfiguration.isReturnDocument = false;
            ReturnDocument returnDocument = new ReturnDocument();
            returnDocument.serviceId = "60";
            returnDocument.type = "DRFZ";

            serviceConfiguration.returnDocument = returnDocument;

            label.serviceConfiguration = serviceConfiguration;

            Location location = new Location();
            location.isDRAAlternative = false;

            Origin origin = new Origin();
            Contact contact = new Contact();
            contact.corporateName = ds.Tables[0].Rows[0]["supplierName"].ToString();
            contact.contactName = ds.Tables[0].Rows[0]["commInfoName"].ToString();
            contact.cellPhone = ds.Tables[0].Rows[0]["commInfoPhone"].ToString();
            contact.telephone = ds.Tables[0].Rows[0]["commInfoPhone"].ToString();
            contact.phoneExt = ds.Tables[0].Rows[0]["commInfoExt"].ToString();
            contact.email = ds.Tables[0].Rows[0]["commInfoEmail"].ToString();

            origin.contact = contact;

            Address address = new Address();
            address.bUsedCode = false;
            address.roadTypeCode = "001";
            address.roadTypeAbbName = "RUTA1";
            address.roadName = ds.Tables[0].Rows[0]["addressStreet"].ToString();
            address.townshipCode = "08-019";
            address.townshipName = ds.Tables[0].Rows[0]["addressCity"].ToString();
            address.settlementTypeCode = "001";
            address.settlementTypeAbbName = ds.Tables[0].Rows[0]["addressCol"].ToString().Length > 5 ? ds.Tables[0].Rows[0]["addressCol"].ToString().Substring(0, 4) : ds.Tables[0].Rows[0]["addressCol"].ToString();
            address.settlementName = ds.Tables[0].Rows[0]["addressCol"].ToString();
            address.stateCode = "02";
            address.stateAbbName = ds.Tables[0].Rows[0]["Abrev_Estado"].ToString();
            address.zipCode = ds.Tables[0].Rows[0]["addressPostalCode"].ToString();
            address.countryCode = "484";
            address.countryName = "MEX";
            address.addressReference = ds.Tables[0].Rows[0]["addressReference1"].ToString();
            address.betweenRoadName1 = ds.Tables[0].Rows[0]["addressReference2"].ToString();
            address.betweenRoadName2 = " ";
            address.latitude = ds.Tables[0].Rows[0]["Latitude"].ToString();
            address.longitude = ds.Tables[0].Rows[0]["Longitude"].ToString();
            address.externalNum = ds.Tables[0].Rows[0]["addressNumberExt"].ToString();
            address.indoorInformation = ds.Tables[0].Rows[0]["addressNumberInt"].ToString();
            address.nave = " ";
            address.platform = " ";
            address.localityCode = "02";
            address.localityName = ds.Tables[0].Rows[0]["addressCity"].ToString();

            origin.address = address;

            location.origin = origin;


            Destination destination = new Destination();
            destination.isDeliveryToPUDO = false;
            destination.deliveryPUDOCode = "";

            HomeAddress homeAddress = new HomeAddress();
            Contact contactD = new Contact();
            contactD.corporateName = UeNo;
            contactD.contactName = ds.Tables[1].Rows[0]["CustomerName"].ToString();
            contactD.cellPhone = ds.Tables[1].Rows[0]["Phone"].ToString();
            contactD.telephone = ds.Tables[1].Rows[0]["Phone"].ToString();
            contactD.phoneExt = "";//ds.Tables[1].Rows[0]["commInfoExt"].ToString();
            contactD.email = ds.Tables[1].Rows[0]["Email"].ToString();

            homeAddress.contact = contactD;

            Address addressD = new Address();
            addressD.bUsedCode = false;
            addressD.roadTypeCode = "001";
            addressD.roadTypeAbbName = "RUTA1";
            addressD.roadName = ds.Tables[1].Rows[0]["Address1"].ToString();
            addressD.townshipCode = "08-019";
            addressD.townshipName = ds.Tables[1].Rows[0]["City"].ToString();
            addressD.settlementTypeCode = "001";
            addressD.settlementTypeAbbName = ds.Tables[1].Rows[0]["Address2"].ToString().Length > 5 ? ds.Tables[1].Rows[0]["Address2"].ToString().Substring(0, 4) : ds.Tables[1].Rows[0]["Address2"].ToString();
            addressD.settlementName = ds.Tables[1].Rows[0]["Address2"].ToString();
            addressD.stateCode = "02";
            addressD.stateAbbName = ds.Tables[1].Rows[0]["Abrev_Estado"].ToString();
            addressD.zipCode = ds.Tables[1].Rows[0]["PostalCode"].ToString();
            addressD.countryCode = "484";
            addressD.countryName = "MEX";
            addressD.addressReference = ds.Tables[1].Rows[0]["Reference"].ToString();
            addressD.betweenRoadName1 = " ";
            addressD.betweenRoadName2 = " ";
            addressD.latitude = ds.Tables[1].Rows[0]["Latitude"].ToString();
            addressD.longitude = ds.Tables[1].Rows[0]["Longitude"].ToString();
            addressD.externalNum = " ";
            addressD.indoorInformation = " ";
            addressD.nave = " ";
            addressD.platform = " ";
            addressD.localityCode = "02";
            addressD.localityName = ds.Tables[1].Rows[0]["City"].ToString();

            homeAddress.address = addressD;

            destination.homeAddress = homeAddress;
            location.destination = destination;

            Notified notified = new Notified();
            notified.notifiedTaxIdCode = "notifiedTaxCode";
            notified.notifiedTaxCountry = "MEX";
            Residence residence = new Residence();
            Contact contactN = new Contact();
            contactN.corporateName = ds.Tables[1].Rows[0]["CustomerName"].ToString();
            contactN.contactName = ds.Tables[1].Rows[0]["CustomerName"].ToString();
            contactN.cellPhone = ds.Tables[1].Rows[0]["Phone"].ToString();
            contactN.telephone = ds.Tables[1].Rows[0]["Phone"].ToString();
            contactN.phoneExt = "";//ds.Tables[1].Rows[0]["commInfoExt"].ToString();
            contactN.email = ds.Tables[1].Rows[0]["Email"].ToString();

            residence.contact = contactN;

            Address addressN = new Address();
            addressN.bUsedCode = false;
            addressN.roadTypeCode = "001";
            addressN.roadTypeAbbName = "RUTA1";
            addressN.roadName = ds.Tables[1].Rows[0]["Address1"].ToString();
            addressN.townshipCode = "08-019";
            addressN.townshipName = ds.Tables[1].Rows[0]["City"].ToString();
            addressN.settlementTypeCode = "001";
            addressN.settlementTypeAbbName = ds.Tables[1].Rows[0]["Address2"].ToString().Length > 5 ? ds.Tables[1].Rows[0]["Address2"].ToString().Substring(0, 4) : ds.Tables[1].Rows[0]["Address2"].ToString();
            addressN.settlementName = ds.Tables[1].Rows[0]["Address2"].ToString();
            addressN.stateCode = "02";
            addressN.stateAbbName = ds.Tables[1].Rows[0]["Abrev_Estado"].ToString();
            addressN.zipCode = ds.Tables[1].Rows[0]["PostalCode"].ToString();
            addressN.countryCode = "484";
            addressN.countryName = "MEX";
            addressN.addressReference = ds.Tables[1].Rows[0]["Reference"].ToString();
            addressN.betweenRoadName1 = " ";
            addressN.betweenRoadName2 = " ";
            addressN.latitude = ds.Tables[1].Rows[0]["Latitude"].ToString();
            addressN.longitude = ds.Tables[1].Rows[0]["Longitude"].ToString();
            addressN.externalNum = " ";
            addressN.indoorInformation = " ";
            addressN.nave = " ";
            addressN.platform = " ";
            addressN.localityCode = "02";
            addressN.localityName = ds.Tables[1].Rows[0]["City"].ToString();

            residence.address = addressN;

            notified.residence = residence;

            location.notified = notified;
            label.location = location;

            model.labelDefinition = label;

            json = JsonConvert.SerializeObject(model);

            return json;
        }
        private string RequestEstafetaLTL(DataSet ds, decimal weight, List<ProductEmbalaje> Products, CarriersRates Carrier, ShipmentToTrackingModel packageCEDIS, string UeNo)
        {
            string json = string.Empty;

            RequestEstafetaLtlModel model = new RequestEstafetaLtlModel();
            LabelDefinitionLtl label = new LabelDefinitionLtl();

            WayBillDocumentLtl wayBill = new WayBillDocumentLtl();
            wayBill.aditionalInfo = "Informacion Adicional";
            wayBill.content = "Contenido";
            wayBill.costCenter = "SPMXA12345";
            wayBill.customerShipmentId = null;
            var reference = ds.Tables[0].Rows[0]["addressReference1"].ToString() + " " + ds.Tables[0].Rows[0]["addressReference2"].ToString();
            wayBill.referenceNumber = reference.Length > 30 ? reference.Substring(0, 29) : reference;
            wayBill.groupShipmentId = null;

            ItemDescription itemDescription = new ItemDescription();
            itemDescription.parcelId = 5;
            itemDescription.weight = weight;
            itemDescription.height = int.Parse(Session["SumHeigth"].ToString());
            itemDescription.length = int.Parse(Session["SumLength"].ToString());
            itemDescription.width = int.Parse(Session["SumWidth"].ToString());

            Pallet pallet = new Pallet();
            pallet.merchandise = "NATIONAL";
            pallet.genericContent = "Mercancias Generales (" + UeNo + ")";
            if (itemDescription.length <= 120 && itemDescription.width <= 105)
                pallet.type = "SIMPLE";
            else
                pallet.type = "DOUBLE";
            wayBill.pallet = pallet;
            label.wayBillDocument = wayBill;

            Merchandises merchandises = new Merchandises();
            merchandises.totalGrossWeight = weight;
            merchandises.weightUnitCode = "KGM";

            List<Merchandise> lstMerchandises = new List<Merchandise>();

            if (Products != null)
            {
                foreach (var product in Products)
                {
                    var total = 0.0;
                    var quantity = 0.0;

                    Merchandise merchandise = new Merchandise();

                    foreach (DataRow r in ds.Tables[2].Rows)
                    {
                        if (r["PosBarCode"].ToString().Equals(product.Barcode.ToString()))
                        {
                            total = double.Parse(r["Total"].ToString());
                            quantity = double.Parse(r["PosBarCode"].ToString());

                        }
                    }
                    merchandise.merchandiseValue = total;
                    merchandise.currency = "MXN";
                    merchandise.productServiceCode = "01010101";
                    merchandise.merchandiseQuantity = product.Pieces;
                    merchandise.measurementUnitCode = "H87";
                    merchandise.tariffFraction = "12345678";
                    merchandise.UUIDExteriorTrade = "ABCDed02-a12A-B34B-c56C-c5abcdef61F2";
                    merchandise.isInternational = false;
                    merchandise.isImport = false;
                    merchandise.isHazardousMaterial = false;
                    merchandise.hazardousMaterialCode = "M0035";
                    merchandise.packagingCode = "4A";

                    lstMerchandises.Add(merchandise);
                }
            }
            else
            {
                var total = 0.0;
                var quantity = 0.0;

                Merchandise merchandise = new Merchandise();

                foreach (DataRow r in ds.Tables[2].Rows)
                {
                    if (r["PosBarCode"].ToString().Equals(packageCEDIS.barcode.ToString()))
                    {
                        total = double.Parse(r["Total"].ToString());
                        quantity = double.Parse(r["PosBarCode"].ToString());

                    }
                }
                merchandise.merchandiseValue = total;
                merchandise.currency = "MXN";
                merchandise.productServiceCode = "01010101";
                merchandise.merchandiseQuantity = packageCEDIS.piezas;
                merchandise.measurementUnitCode = "H87";
                merchandise.tariffFraction = "12345678";
                merchandise.UUIDExteriorTrade = "ABCDed02-a12A-B34B-c56C-c5abcdef61F2";
                merchandise.isInternational = false;
                merchandise.isImport = false;
                merchandise.isHazardousMaterial = false;
                merchandise.hazardousMaterialCode = "M0035";
                merchandise.packagingCode = "4A";

                lstMerchandises.Add(merchandise);
            }
            merchandises.merchandise = lstMerchandises;
            itemDescription.merchandises = merchandises;

            label.itemDescription = itemDescription;

            ServiceConfiguration serviceConfiguration = new ServiceConfiguration();
            serviceConfiguration.quantityOfLabels = 1;
            serviceConfiguration.serviceTypeId = "L0";
            serviceConfiguration.salesOrganization = System.Configuration.ConfigurationManager.AppSettings["salesOrganization"];
            serviceConfiguration.effectiveDate = ds.Tables[1].Rows[0]["effectiveDate"].ToString();
            serviceConfiguration.originZipCodeForRouting = ds.Tables[1].Rows[0]["PostalCode"].ToString();
            // validar si tiene seguro---------------
            if (Carrier.Montoasegurado > 0)
            {
                serviceConfiguration.isInsurance = true;

                Insurance insurance = new Insurance();
                insurance.contentDescription = "Mercancias Generales";
                insurance.declaredValue = Carrier.Montoasegurado;

                serviceConfiguration.insurance = insurance;

            }
            else
                serviceConfiguration.isInsurance = false;

            serviceConfiguration.isReturnDocument = false;
            ReturnDocument returnDocument = new ReturnDocument();
            returnDocument.serviceId = "60";
            returnDocument.type = "DRFZ";

            serviceConfiguration.returnDocument = returnDocument;

            label.serviceConfiguration = serviceConfiguration;

            Location location = new Location();
            location.isDRAAlternative = false;

            Origin origin = new Origin();
            Contact contact = new Contact();
            contact.corporateName = ds.Tables[0].Rows[0]["supplierName"].ToString();
            contact.contactName = ds.Tables[0].Rows[0]["commInfoName"].ToString();
            contact.cellPhone = ds.Tables[0].Rows[0]["commInfoPhone"].ToString();
            contact.telephone = ds.Tables[0].Rows[0]["commInfoPhone"].ToString();
            contact.phoneExt = ds.Tables[0].Rows[0]["commInfoExt"].ToString();
            contact.email = ds.Tables[0].Rows[0]["commInfoEmail"].ToString();

            origin.contact = contact;

            Address address = new Address();
            address.bUsedCode = false;
            address.roadTypeCode = "001";
            address.roadTypeAbbName = "RUTA1";
            address.roadName = ds.Tables[0].Rows[0]["addressStreet"].ToString();
            address.townshipCode = "08-019";
            address.townshipName = ds.Tables[0].Rows[0]["addressCity"].ToString();
            address.settlementTypeCode = "001";
            address.settlementTypeAbbName = ds.Tables[0].Rows[0]["addressCol"].ToString().Length > 5 ? ds.Tables[0].Rows[0]["addressCol"].ToString().Substring(0, 4) : ds.Tables[0].Rows[0]["addressCol"].ToString();
            address.settlementName = ds.Tables[0].Rows[0]["addressCol"].ToString();
            address.stateCode = "02";
            address.stateAbbName = ds.Tables[0].Rows[0]["Abrev_Estado"].ToString();
            address.zipCode = ds.Tables[0].Rows[0]["addressPostalCode"].ToString();
            address.countryCode = "484";
            address.countryName = "MEX";
            address.addressReference = ds.Tables[0].Rows[0]["addressReference1"].ToString();
            address.betweenRoadName1 = ds.Tables[0].Rows[0]["addressReference2"].ToString();
            address.betweenRoadName2 = " ";
            address.latitude = ds.Tables[0].Rows[0]["Latitude"].ToString();
            address.longitude = ds.Tables[0].Rows[0]["Longitude"].ToString();
            address.externalNum = ds.Tables[0].Rows[0]["addressNumberExt"].ToString();
            address.indoorInformation = ds.Tables[0].Rows[0]["addressNumberInt"].ToString();
            address.nave = " ";
            address.platform = " ";
            address.localityCode = "02";
            address.localityName = ds.Tables[0].Rows[0]["addressCity"].ToString();

            origin.address = address;

            location.origin = origin;


            Destination destination = new Destination();
            destination.isDeliveryToPUDO = false;
            destination.deliveryPUDOCode = "";

            HomeAddress homeAddress = new HomeAddress();
            Contact contactD = new Contact();
            contactD.corporateName = ds.Tables[1].Rows[0]["CustomerName"].ToString();
            contactD.contactName = ds.Tables[1].Rows[0]["CustomerName"].ToString();
            contactD.cellPhone = ds.Tables[1].Rows[0]["Phone"].ToString();
            contactD.telephone = ds.Tables[1].Rows[0]["Phone"].ToString();
            contactD.phoneExt = "";//ds.Tables[1].Rows[0]["commInfoExt"].ToString();
            contactD.email = ds.Tables[1].Rows[0]["Email"].ToString();

            homeAddress.contact = contactD;

            Address addressD = new Address();
            addressD.bUsedCode = false;
            addressD.roadTypeCode = "001";
            addressD.roadTypeAbbName = "RUTA1";
            addressD.roadName = ds.Tables[1].Rows[0]["Address1"].ToString();
            addressD.townshipCode = "08-019";
            addressD.townshipName = ds.Tables[1].Rows[0]["City"].ToString();
            addressD.settlementTypeCode = "001";
            addressD.settlementTypeAbbName = ds.Tables[1].Rows[0]["Address2"].ToString().Length > 5 ? ds.Tables[1].Rows[0]["Address2"].ToString().Substring(0, 4) : ds.Tables[1].Rows[0]["Address2"].ToString();
            addressD.settlementName = ds.Tables[1].Rows[0]["Address2"].ToString();
            addressD.stateCode = "02";
            addressD.stateAbbName = ds.Tables[1].Rows[0]["Abrev_Estado"].ToString();
            addressD.zipCode = ds.Tables[1].Rows[0]["PostalCode"].ToString();
            addressD.countryCode = "484";
            addressD.countryName = "MEX";
            addressD.addressReference = ds.Tables[1].Rows[0]["Reference"].ToString();
            addressD.betweenRoadName1 = " ";
            addressD.betweenRoadName2 = " ";
            addressD.latitude = ds.Tables[1].Rows[0]["Latitude"].ToString();
            addressD.longitude = ds.Tables[1].Rows[0]["Longitude"].ToString();
            addressD.externalNum = " ";
            addressD.indoorInformation = " ";
            addressD.nave = " ";
            addressD.platform = " ";
            addressD.localityCode = "02";
            addressD.localityName = ds.Tables[1].Rows[0]["City"].ToString();

            homeAddress.address = addressD;

            destination.homeAddress = homeAddress;
            location.destination = destination;

            Notified notified = new Notified();
            notified.notifiedTaxIdCode = "notifiedTaxCode";
            notified.notifiedTaxCountry = "MEX";
            Residence residence = new Residence();
            Contact contactN = new Contact();
            contactN.corporateName = ds.Tables[1].Rows[0]["CustomerName"].ToString();
            contactN.contactName = ds.Tables[1].Rows[0]["CustomerName"].ToString();
            contactN.cellPhone = ds.Tables[1].Rows[0]["Phone"].ToString();
            contactN.telephone = ds.Tables[1].Rows[0]["Phone"].ToString();
            contactN.phoneExt = "";//ds.Tables[1].Rows[0]["commInfoExt"].ToString();
            contactN.email = ds.Tables[1].Rows[0]["Email"].ToString();

            residence.contact = contactN;

            Address addressN = new Address();
            addressN.bUsedCode = false;
            addressN.roadTypeCode = "001";
            addressN.roadTypeAbbName = "RUTA1";
            addressN.roadName = ds.Tables[1].Rows[0]["Address1"].ToString();
            addressN.townshipCode = "08-019";
            addressN.townshipName = ds.Tables[1].Rows[0]["City"].ToString();
            addressN.settlementTypeCode = "001";
            addressN.settlementTypeAbbName = ds.Tables[1].Rows[0]["Address2"].ToString().Length > 5 ? ds.Tables[1].Rows[0]["Address2"].ToString().Substring(0, 4) : ds.Tables[1].Rows[0]["Address2"].ToString();
            addressN.settlementName = ds.Tables[1].Rows[0]["Address2"].ToString();
            addressN.stateCode = "02";
            addressN.stateAbbName = ds.Tables[1].Rows[0]["Abrev_Estado"].ToString();
            addressN.zipCode = ds.Tables[1].Rows[0]["PostalCode"].ToString();
            addressN.countryCode = "484";
            addressN.countryName = "MEX";
            addressN.addressReference = ds.Tables[1].Rows[0]["Reference"].ToString();
            addressN.betweenRoadName1 = " ";
            addressN.betweenRoadName2 = " ";
            addressN.latitude = ds.Tables[1].Rows[0]["Latitude"].ToString();
            addressN.longitude = ds.Tables[1].Rows[0]["Longitude"].ToString();
            addressN.externalNum = " ";
            addressN.indoorInformation = " ";
            addressN.nave = " ";
            addressN.platform = " ";
            addressN.localityCode = "02";
            addressN.localityName = ds.Tables[1].Rows[0]["City"].ToString();

            residence.address = addressN;

            notified.residence = residence;

            location.notified = notified;
            label.location = location;

            model.labelDefinition = label;

            json = JsonConvert.SerializeObject(model);

            return json;
        }
        public string CreateGuiaEstafetaAPI(string UeNo, int OrderNo, decimal weight, List<ProductEmbalaje> Products, CarriersRates Carrier, ShipmentToTrackingModel packageCEDIS)
        {
            string result = string.Empty;
            string json = string.Empty, url = string.Empty;
            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {
                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

                System.Collections.Hashtable parametros = new System.Collections.Hashtable();
                parametros.Add("@OrderNo", OrderNo);

                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[dbo].[upCorpOms_Cns_EstafetaInfo]", false, parametros);

                if (weight <= 70)
                {
                    json = RequestEstafeta(ds, weight, Products, Carrier, packageCEDIS, UeNo);
                    url = System.Configuration.ConfigurationSettings.AppSettings["Estafeta_API"];
                }
                else
                {
                    json = RequestEstafetaLTL(ds, weight, Products, Carrier, packageCEDIS, UeNo);
                    url = System.Configuration.ConfigurationSettings.AppSettings["EstafetaLTL_API"];
                }
                Soriana.FWK.FmkTools.RestResponse r2 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, url, "", json);

                string msg = r2.message;

                ResponseEstafetaModel re = JsonConvert.DeserializeObject<ResponseEstafetaModel>(r2.message);

                string pdfcadena2 = Convert.ToBase64String(re.data, Base64FormattingOptions.None);

                //return re.Guia + "," + re.pdf;
                result = re.labelPetitionResult.result.description + "," + pdfcadena2;

            }

            catch
            {
                //throw new Exception("La generacion de la Guia por Estafeta Fallo. " + ex.Message);
                result = "ERROR";
            }

            return result;
        }
        public string CreateGuiaEstafeta(string UeNo, int OrderNo, int weight, int typeId)
        {
            string result = string.Empty;
            DataSet ds = new DataSet();
            DataSet dsO = new DataSet();

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



                System.Collections.Hashtable parametros2 = new System.Collections.Hashtable();
                parametros2.Add("@UeNo", UeNo);


                dsO = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[dbo].[upCorpOms_Cns_UeNoOriginInfo]", false, parametros2);
                EstafetaRequestModel m = new EstafetaRequestModel();
                foreach (DataRow r in dsO.Tables[0].Rows)
                {


                    m.OriginInfo = new AddressModel();

                    m.OriginInfo.address1 = r["address1"].ToString();
                    m.OriginInfo.address2 = r["address2"].ToString();
                    m.OriginInfo.cellPhone = r["cellPhone"].ToString();
                    m.OriginInfo.city = r["city"].ToString();
                    m.OriginInfo.contactName = r["contactName"].ToString();
                    m.OriginInfo.corporateName = r["corporateName"].ToString();
                    m.OriginInfo.customerNumber = r["customerNumber"].ToString();
                    m.OriginInfo.neighborhood = r["neighborhood"].ToString();
                    m.OriginInfo.phoneNumber = r["phone"].ToString();
                    m.OriginInfo.state = r["state"].ToString();
                    m.OriginInfo.zipCode = r["zipCode"].ToString();

                }

                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    //m.serviceTypeId = "60";
                    //m.serviceTypeId = System.Configuration.ConfigurationManager.AppSettings["val_serviceTypeId"];
                    m.serviceTypeId = r["ServiceType"].ToString();
                    if (weight >= 70)
                    {
                        m.serviceTypeId = "L0";
                    }


                    m.DestinationInfo = new AddressModel();

                    m.DestinationInfo.address1 = r["Address1"].ToString();
                    m.DestinationInfo.address2 = r["Address2"].ToString();
                    m.DestinationInfo.cellPhone = r["Phone"].ToString();
                    m.DestinationInfo.city = r["City"].ToString();
                    m.DestinationInfo.contactName = r["CustomerName"].ToString();
                    //m.DestinationInfo.corporateName = r["CustomerName"].ToString();
                    m.DestinationInfo.corporateName = r["UeNo"].ToString();
                    m.DestinationInfo.customerNumber = r["CustomerNo"].ToString();
                    m.DestinationInfo.neighborhood = r["NameReceives"].ToString();
                    m.DestinationInfo.phoneNumber = r["Phone"].ToString();
                    m.DestinationInfo.state = r["StateCode"].ToString();
                    m.DestinationInfo.zipCode = r["PostalCode"].ToString();

                    m.reference = r["Reference"].ToString();
                    m.originZipCodeForRouting = r["PostalCode"].ToString();
                    m.weight = weight; // lo capturado en el modal
                    m.parcelTypeId = typeId; // 1 - sobre, 4 - paquete
                    m.effectiveDate = r["effectiveDate"].ToString();

                    //OrderNo
                    //    CnscOrder
                    //    StoreNum
                    //    UeNo
                    //    StatusUe
                    //    OrderDate
                    //    OrderTime
                    //    CustomerNo  
                    //    CustomerName 
                    //    Phone   
                    //    Address1 
                    //    Address2    
                    //    City 
                    //    StateCode   
                    //    PostalCode 
                    //    Reference   
                    //    NameReceives 
                    //    Total   


                    string json2 = JsonConvert.SerializeObject(m);

                    Soriana.FWK.FmkTools.RestResponse r2 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, System.Configuration.ConfigurationSettings.AppSettings["api_Estafeta_Guia"], "", json2);

                    string msg = r2.message;

                    ResponseEstModels re = JsonConvert.DeserializeObject<ResponseEstModels>(r2.message);

                    string pdfcadena2 = Convert.ToBase64String(re.pdf, Base64FormattingOptions.None);

                    //return re.Guia + "," + re.pdf;
                    return re.Guia + "," + pdfcadena2;


                }

            }

            catch
            {
                //throw new Exception("La generacion de la Guia por Estafeta Fallo. " + ex.Message);
                result = "ERROR";
            }
            return result;
        }
        public string CreateGuiaLogyt(string UeNo, int OrderNo, int weight, int typeId)
        {
            string result = string.Empty;
            var ServiceTypeId = 1;
            DataSet ds = new DataSet();
            DataSet dsO = new DataSet();

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



                System.Collections.Hashtable parametros2 = new System.Collections.Hashtable();
                parametros2.Add("@UeNo", UeNo);


                dsO = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[dbo].[upCorpOms_Cns_UeNoOriginInfo]", false, parametros2);

                LogytRequestModel m = new LogytRequestModel();
                foreach (DataRow r in dsO.Tables[0].Rows)
                {


                    m.Origin = new LogytAddressModel();

                    m.Origin.Address1 = r["address1"].ToString();
                    m.Origin.Address2 = r["address2"].ToString();
                    m.Origin.City = r["city"].ToString();
                    m.Origin.ContactName = r["contactName"].ToString();
                    m.Origin.CorporateName = r["corporateName"].ToString();
                    m.Origin.CustomerNumber = r["customerNumber"].ToString();
                    m.Origin.Neighborhood = r["neighborhood"].ToString();
                    m.Origin.PhoneNumber = r["phone"].ToString();
                    m.Origin.State = r["state"].ToString();
                    m.Origin.ZipCode = r["zipCode"].ToString();

                }

                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    //m.ServiceType = System.Configuration.ConfigurationManager.AppSettings["val_serviceTypeId"];
                    m.ServiceType = r["ServiceType"].ToString();
                    if (weight >= 70)
                    {
                        m.ServiceType = "L0";
                    }


                    m.Destination = new LogytAddressModel();

                    m.Destination.Address1 = r["Address1"].ToString();
                    m.Destination.Address2 = r["Address2"].ToString();
                    m.Destination.City = r["City"].ToString();
                    m.Destination.ContactName = r["CustomerName"].ToString();
                    //m.Destination.corporateName = r["CustomerName"].ToString();
                    m.Destination.CorporateName = r["UeNo"].ToString();
                    m.Destination.CustomerNumber = r["CustomerNo"].ToString();
                    m.Destination.Neighborhood = r["NameReceives"].ToString();
                    m.Destination.PhoneNumber = r["Phone"].ToString();
                    m.Destination.State = r["StateCode"].ToString();
                    m.Destination.ZipCode = r["PostalCode"].ToString();

                    m.Reference = r["Reference"].ToString();
                    //m.originZipCodeForRouting = r["PostalCode"].ToString();
                    m.Weight = weight; // lo capturado en el modal
                    m.Volume = weight;

                    string json2 = JsonConvert.SerializeObject(m);

                    Soriana.FWK.FmkTools.RestResponse r2 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, System.Configuration.ConfigurationSettings.AppSettings["api_Logyt_Guia"], "", json2);

                    string msg = r2.message;
                    string pdfcadena2 = string.Empty;

                    LogytResponseModels re = JsonConvert.DeserializeObject<LogytResponseModels>(r2.message);


                    if (!Convert.ToBoolean(re.Error))
                        pdfcadena2 = Convert.ToBase64String(re.Labels[0].PDF, Base64FormattingOptions.None);
                    else
                    {
                        string msgDetail = string.Empty;
                        foreach (var itemMsg in re.Messages)
                        {
                            msgDetail += itemMsg + ". ";
                        }
                        throw new Exception(msgDetail);
                    }
                    //return re.Guia + "," + re.pdf;
                    result = re.Labels[0].Folios[0] + "," + pdfcadena2;

                }

            }
            catch
            {
                //throw new Exception("La generacion de la Guia por Logyt Fallo. " + ex.Message);
                result = "ERROR";
            }
            return result;
        }
        private string RequestEnvia(string json, string service)
        {
            string _jsonFinal = string.Empty;

            CotizadorRequestModel request = JsonConvert.DeserializeObject<CotizadorRequestModel>(json);

            request.shipment.service = service;

            _jsonFinal = JsonConvert.SerializeObject(request);

            return _jsonFinal;
        }
        private string RequestLTLEnvia(string json, string service)
        {
            string _jsonFinal = string.Empty;

            CotizadorRequestLTLModel request = JsonConvert.DeserializeObject<CotizadorRequestLTLModel>(json);

            request.shipment.service = service;

            _jsonFinal = JsonConvert.SerializeObject(request);

            return _jsonFinal;
        }
        public string CreateGuiaEnvia(string request, string service, int weight)
        {
            string result = string.Empty, _json = string.Empty;
            try
            {
                if (weight > 70)
                    _json = RequestLTLEnvia(request, service);
                else
                    _json = RequestEnvia(request, service);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Soriana.FWK.FmkTools.RestResponse r2 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, System.Configuration.ConfigurationSettings.AppSettings["api_Envia_Guia"], "", _json);

                string msg = r2.message;

                //string msg = "{ 'meta': 'generate', 'data': [{'carrier': 'fedex','service': 'express','trackingNumber': '794693403268','trackUrl': 'https://test.envia.com/rastreo?label=794693403268&cntry_code=mx', 'label': 'https://s3.us-east-2.amazonaws.com/envia-staging/uploads/fedex/79469340326840461b0130d92379.pdf', 'additionalFiles': [],'totalPrice': 434,'currentBalance': 1580, 'currency': 'MXN'} ]}";


                EnviaResponseModel re = JsonConvert.DeserializeObject<EnviaResponseModel>(msg);


                result = re.data[0].trackingNumber + "," + re.data[0].label + "," + re.data[0].trackUrl;
            }
            catch
            {
                //throw new Exception("La generacion de la Guia por Envia.Com Fallo. " + ex.Message);
                result = "ERROR";
            }

            return result;
        }
        public string RecoleccionEnvia(string request)
        {
            string result = string.Empty;
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Soriana.FWK.FmkTools.RestResponse r2 = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, System.Configuration.ConfigurationSettings.AppSettings["api_Envia_Recoleccion"], "", request);

                result = r2.message;

            }
            catch
            {
                //throw new Exception("La generacion de la Guia por Envia.Com Fallo. " + ex.Message);
                result = "ERROR";
            }

            return result;
        }


    }
}