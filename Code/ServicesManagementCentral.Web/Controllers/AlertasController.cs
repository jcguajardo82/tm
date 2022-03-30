using Jitbit.Utils;
using ServicesManagement.Web.Models.Alertas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicesManagement.Web.Controllers
{
    public class AlertasController : Controller
    {
        #region Actions
        public ActionResult OperacionesGeneral()
        {
            Session["RevisionGeneral"] = Get_RevisionGeneral();
            Session["ddlTransportista"] = upCorpTms_Cns_DashboardTrans();

            return View();
        }

        public ActionResult OperacionCedis()
        {
            Session["RevisionGeneral"] = Get_RevisionGeneral();
            Session["ddlTransportista"] = upCorpTms_Cns_DashboardTrans();

            return View();
        }

        public ActionResult OperacionDSV()
        {
            return View();
        }

        public ActionResult OperacionDST()
        {
            return View();
        }

        #endregion

        #region ObtenerDatos 
        private DataSet Get_RevisionGeneral()
        {
            List<RevisionGeneralModel> lstRevisionGral = new List<RevisionGeneralModel>();
            DataSet ds = new DataSet();

            try
            {
                var _ConnectionString = ConfigurationManager.ConnectionStrings["Connection_DEV"].ToString(); //Environment.GetEnvironmentVariable("ConnectionStrings:MercurioDB");
                string sp_Name = "tms.upCorpTms_Cns_Alertas";

                using (System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection(_ConnectionString))
                {
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sp_Name, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        #region Parametros
                        System.Data.SqlClient.SqlParameter param;

                        param = cmd.Parameters.Add("@fechaini", SqlDbType.VarChar);
                        param.Value = "2022-01-01";

                        param = cmd.Parameters.Add("@fechafin", SqlDbType.VarChar);
                        param.Value = "2022-04-18";

                        param = cmd.Parameters.Add("@transportistas", SqlDbType.VarChar);
                        param.Value = "111111,6569,222222";

                        param = cmd.Parameters.Add("@idOwner", SqlDbType.Int);
                        param.Value = 1;

                        param = cmd.Parameters.Add("@estatusTransporte", SqlDbType.Int);
                        param.Value = 3;
                        #endregion

                        using (System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(cmd))
                            dataAdapter.Fill(ds);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public ActionResult BuscarOperacionGeneralFiltros(string FecIni, string FecFin, string Transportista, string EstatusTrans) 
        {
            DataSet ds = new DataSet();
            AlertasModel Alertas = new AlertasModel();
            List<PendienteRecoleccionModel> lstPendientesRecoleccion = new List<PendienteRecoleccionModel>();
            List<PendienteEntregaModel> lstPendientesEntrega = new List<PendienteEntregaModel>();
            List<RevisionGeneralModel> lstRevisionGeneral = new List<RevisionGeneralModel>();
            try
            {
                var _ConnectionString = ConfigurationManager.ConnectionStrings["Connection_DEV"].ToString(); //Environment.GetEnvironmentVariable("ConnectionStrings:MercurioDB");
                string sp_Name = "tms.upCorpTms_Cns_Alertas";

                using (System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection(_ConnectionString))
                {
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sp_Name, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        #region Parametros
                        System.Data.SqlClient.SqlParameter param;

                        param = cmd.Parameters.Add("@fechaini", SqlDbType.VarChar);
                        param.Value = FecIni;  //"2022-01-01";

                        param = cmd.Parameters.Add("@fechafin", SqlDbType.VarChar);
                        param.Value = FecFin;  //"2022-04-18";

                        param = cmd.Parameters.Add("@transportistas", SqlDbType.VarChar);
                        param.Value = Transportista; // "111111,6569,222222";

                        param = cmd.Parameters.Add("@idOwner", SqlDbType.Int);
                        param.Value = 1;

                        param = cmd.Parameters.Add("@estatusTransporte", SqlDbType.Int);
                        param.Value = EstatusTrans; //3;
                        #endregion

                        using (System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(cmd))
                            dataAdapter.Fill(ds);
                    }
                }

                foreach(DataTable dt in ds.Tables)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        #region Mapeo
                        if (dt.TableName == "Table")
                        {
                            PendienteRecoleccionModel pendienteRecol = new PendienteRecoleccionModel
                            {
                                DiasRetrasoRecoleccion = row["DiasRetrasoRecoleccion"].ToString(),
                                ColorRetrasoRecoleccion = row["ColorRetrasoRecoleccion"].ToString(),
                                Proveedor = row["Proveedor"].ToString(),
                                Consignacion = row["Consignacion"].ToString(),
                                GuiaTransporte = row["GuiaTransporte"].ToString(),
                                Transportista = row["Transportista"].ToString(),
                                Carrier = row["Carrier"].ToString(),
                                TipoDeEnvio = row["TipoDeEnvio"].ToString(),
                                TipoDeServicio = row["TipoDeServicio"].ToString(),
                                FechaComprimisoInicio = row["FechaComprimisoInicio"].ToString(),
                                FechaComprimisoFin = row["FechaComprimisoFin"].ToString(),
                                FechaSolicitudReal = row["FechaSolicitudReal"].ToString(),
                                DiasRetrasoSurtido = row["DiasRetrasoSurtido"].ToString(),
                                ColorRetrasoSurtido = row["ColorRetrasoSurtido"].ToString(),
                                FechaCompromisoRecoleccion = row["FechaCompromisoRecoleccion"].ToString(),
                                fechaCreacion = row["fechaCreacion"].ToString(),
                            };

                            lstPendientesRecoleccion.Add(pendienteRecol);
                        }
                        if(dt.TableName == "Table1")
                        {
                            PendienteEntregaModel PendienteEntrega = new PendienteEntregaModel
                            {
                                DiasRetrasoTransportista = row["DiasRetrasoTransportista"].ToString(),
                                ColorRetrasoTransportista = row["ColorRetrasoTransportista"].ToString(),
                                Proveedor = row["Proveedor"].ToString(),
                                Consignacion = row["Consignacion"].ToString(),
                                GuiaTransporte = row["GuiaTransporte"].ToString(),
                                Transportista = row["Transportista"].ToString(),
                                Carrier = row["Carrier"].ToString(),
                                TipoDeEnvio = row["TipoDeEnvio"].ToString(),
                                TipoDeServicio = row["TipoDeServicio"].ToString(),
                                CodigoPostalDestino = row["CodigoPostalDestino"].ToString(),
                                EstadoDestino = row["EstadoDestino"].ToString(),
                                EstatusTransporte = row["EstatusTransporte"].ToString(),
                                FechaComprimisoInicio = row["FechaComprimisoInicio"].ToString(),
                                FechaComprimisoFin = row["FechaComprimisoFin"].ToString(),
                                DiasRetrasoRecoleccion = row["DiasRetrasoRecoleccion"].ToString(),
                                ColorRetrasoRecoleccion = row["ColorRetrasoRecoleccion"].ToString(),
                                FechaRecoleccionReal = row["FechaRecoleccionReal"].ToString(),
                                FechaEntregaTransporte = row["FechaEntregaTransporte"].ToString(),
                                fechaCreacion = row["fechaCreacion"].ToString()
                            };

                            lstPendientesEntrega.Add(PendienteEntrega);
                        }
                        if(dt.TableName == "Table2")
                        {
                            RevisionGeneralModel RevisionGral = new RevisionGeneralModel
                            {
                                DiasRetrasoTotal = row["DiasRetrasoTotal"].ToString(),
                                ColorRetrasoTotal = row["ColorRetrasoTotal"].ToString(),
                                Proveedor = row["Proveedor"].ToString(),
                                Consignacion = row["Consignacion"].ToString(),
                                GuiaTransporte = row["GuiaTransporte"].ToString(),
                                Transportista = row["Transportista"].ToString(),
                                Carrier = row["Carrier"].ToString(),
                                TipoDeEnvio = row["TipoDeEnvio"].ToString(),
                                TipoDeServicio = row["TipoDeServicio"].ToString(),
                                EstatusTransporte = row["EstatusTransporte"].ToString(),
                                FechaComprimisoInicio = row["FechaComprimisoInicio"].ToString(),
                                FechaComprimisoFin = row["FechaComprimisoFin"].ToString(),
                                fechaCreacion = row["fechaCreacion"].ToString(),
                                FechaPago = row["FechaPago"].ToString(),
                                FechaCompromisoSurtido = row["FechaCompromisoSurtido"].ToString(),
                                FechaSolicitudReal = row["FechaSolicitudReal"].ToString(),
                                DiasRetrasoSurtido = row["DiasRetrasoSurtido"].ToString(),
                                ColorRetrasoSurtido = row["ColorRetrasoSurtido"].ToString(),
                                FechaCompromisoRecoleccion = row["FechaCompromisoRecoleccion"].ToString(),
                                FechaRecoleccionReal = row["FechaRecoleccionReal"].ToString(),
                                DiasRetrasoRecoleccion = row["DiasRetrasoRecoleccion"].ToString(),
                                ColorRetrasoRecoleccion = row["ColorRetrasoRecoleccion"].ToString(),
                                FechaEntregaTransporte = row["FechaEntregaTransporte"].ToString(),
                                fechaEntregaReal = row["fechaEntregaReal"].ToString(),
                                DiasRetrasoTransportista = row["DiasRetrasoTransportista"].ToString(),
                                ColorRetrasoTransportista = row["ColorRetrasoTransportista"].ToString()
                            };

                            lstRevisionGeneral.Add(RevisionGral);
                        }
                        #endregion
                    }
                }

                Alertas.lstEntregaModel = lstPendientesEntrega;
                Alertas.lstPendientesRec = lstPendientesRecoleccion;
                Alertas.lstRevisionGral = lstRevisionGeneral;

               

                var result = new { Success = true, resp = Alertas };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public DataSet upCorpTms_Cns_DashboardTrans()
        {
            DataSet ds = new DataSet();

            try
            {
                var _ConnectionString = ConfigurationManager.ConnectionStrings["Connection_DEV"].ToString(); //Environment.GetEnvironmentVariable("ConnectionStrings:MercurioDB");
                string sp_Name = "tms.upCorpTms_Cns_DashboardTrans";


                using (System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection(_ConnectionString))
                {
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sp_Name, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(cmd))
                            dataAdapter.Fill(ds);
                    }
                }

                return ds;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Functions
        public ActionResult CargarDatosReasignacion(string Consignacion, string Transportista)
        {
            try
            {
                /*DataSet ds = new DataSet();

                var _ConnectionString = ConfigurationManager.ConnectionStrings["Connection_DEV"].ToString(); //Environment.GetEnvironmentVariable("ConnectionStrings:MercurioDB");
                string sp_Name = "tms.";

                using (System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection(_ConnectionString))
                {
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sp_Name, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        #region Parametros
                        System.Data.SqlClient.SqlParameter param;

                        param = cmd.Parameters.Add("@", SqlDbType.VarChar);
                        param.Value = "";
                        #endregion

                        using (System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(cmd))
                            dataAdapter.Fill(ds);
                    }
                }*/

                var result = new { Success = true, resp = Consignacion };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult CargarDatosCancelacion(string Consignacion, string Transporte)
        {
            try
            {
                DatosCancelacion datosCancelacion = new DatosCancelacion();
                CabeceraCancelacion cabeceraCancelacion = new CabeceraCancelacion();
                List<MotivosCancelacion> lstMotivosCancelacion = new List<MotivosCancelacion>();

                #region Datos Cabecera Cancelacion
                DataSet ds = new DataSet();

                Transporte = Transporte.Replace("_", "").Trim();
                
                var _ConnectionString = ConfigurationManager.ConnectionStrings["Connection_DEV"].ToString(); //Environment.GetEnvironmentVariable("ConnectionStrings:MercurioDB");
                string sp_Name = "tms.upCorpTms_Cns_AlertasCabecera";

                using (System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection(_ConnectionString))
                {
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sp_Name, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        #region Parametros
                        System.Data.SqlClient.SqlParameter param;

                        param = cmd.Parameters.Add("@UeNo", SqlDbType.VarChar);
                        param.Value = Consignacion;

                        param = cmd.Parameters.Add("@IdTrackingService", SqlDbType.VarChar);
                        param.Value = Transporte;
                        #endregion

                        using (System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(cmd))
                            dataAdapter.Fill(ds);
                    }
                }

                foreach(DataTable dt in ds.Tables)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        cabeceraCancelacion.Consignacion = row["Consignacion"].ToString();
                        cabeceraCancelacion.Transportista = row["Transportista"].ToString();
                        cabeceraCancelacion.Carrier = row["Carrier"].ToString();
                        cabeceraCancelacion.TipoDeEnvio = row["TipoDeEnvio"].ToString();
                        cabeceraCancelacion.TipoDeServicio = row["TipoDeServicio"].ToString();
                    }
                }

                datosCancelacion.Cabecera = cabeceraCancelacion;
                #endregion

                #region Motivos Cancelacion
                //tms.upCorpTms_Cns_MotivosCancelacionGuias
                DataSet ds2 = new DataSet();

                _ConnectionString = ConfigurationManager.ConnectionStrings["Connection_DEV"].ToString(); //Environment.GetEnvironmentVariable("ConnectionStrings:MercurioDB");
                sp_Name = "tms.upCorpTms_Cns_MotivosCancelacionGuias";

                using (System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection(_ConnectionString))
                {
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sp_Name, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(cmd))
                            dataAdapter.Fill(ds2);
                    }
                }

                foreach(DataTable dt in ds2.Tables)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        MotivosCancelacion motivos = new MotivosCancelacion
                        {
                            IdMotivoCancelacion = row["IdMotivoCancelacion"].ToString(),
                            Descripcion = row["Descripcion"].ToString()
                        };

                        lstMotivosCancelacion.Add(motivos);
                    }
                }

                datosCancelacion.lstMotivos = lstMotivosCancelacion;
                #endregion

                var result = new { Success = true, resp = datosCancelacion };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult CargarDatosHistorial(string Consignacion, string Transporte)
        {
            try
            {
                #region Definiciones
                DatosHistorial datosHistorial = new DatosHistorial();
                DataSet ds = new DataSet();
                List<HistorialDetalle> lstHistorial = new List<HistorialDetalle>();
                HistorialEncabezado encabezado = new HistorialEncabezado();
                #endregion

                Transporte = Transporte.Replace("_", "").Trim();

                var _ConnectionString = ConfigurationManager.ConnectionStrings["Connection_DEV"].ToString(); //Environment.GetEnvironmentVariable("ConnectionStrings:MercurioDB");
                string sp_Name = "tms.upCorpTms_Cns_AlertasResumen";

                using (System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection(_ConnectionString))
                {
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sp_Name, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        #region Parametros
                        System.Data.SqlClient.SqlParameter param;

                        param = cmd.Parameters.Add("@UeNo", SqlDbType.VarChar);
                        param.Value = Consignacion;

                        param = cmd.Parameters.Add("@IdTrackingService", SqlDbType.VarChar);
                        param.Value = Transporte;
                        #endregion

                        using (System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(cmd))
                            dataAdapter.Fill(ds);
                    }
                }

                foreach(DataTable dt in ds.Tables)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        if(dt.TableName == "Table")
                        {                          
                            encabezado.Proveedor = row["Proveedor"].ToString();
                            encabezado.Consignacion = row["Consignacion"].ToString();
                            encabezado.GuiaTransporte = row["GuiaTransporte"].ToString();
                            encabezado.Transportista = row["Transportista"].ToString();
                            encabezado.Carrier = row["Carrier"].ToString();
                            encabezado.CiudadOrigen = row["CiudadOrigen"].ToString();
                            encabezado.CodigoPostalOrigen = row["CodigoPostalOrigen"].ToString();
                            encabezado.CiudadDestino = row["CiudadDestino"].ToString();
                            encabezado.CodigoPostalDestino = row["CodigoPostalDestino"].ToString();
                            encabezado.FechaCompromisoEntregaTransportista = row["FechaCompromisoEntregaTransportista"].ToString();
                            encabezado.FechaComprimisoEntregaCliente = row["FechaComprimisoEntregaCliente"].ToString();
                            encabezado.NombreQuienRecibe = row["NombreQuienRecibe"].ToString();
                        }
                        else if(dt.TableName == "Table1")
                        {
                            HistorialDetalle detalle = new HistorialDetalle
                            {
                                UeNo = row["UeNo"].ToString(),
                                IdTrackingService = row["IdTrackingService"].ToString(),
                                eventDateTime = row["eventDateTime"].ToString(),
                                eventDescriptionSPA = row["eventDescriptionSPA"].ToString(),
                                eventPlaceName = row["eventPlaceName"].ToString()
                            };

                            lstHistorial.Add(detalle);
                        }
                    }
                }

                datosHistorial.historialEncabezado = encabezado;
                datosHistorial.lstHistorialDetalle = lstHistorial;

                var result = new { Success = true, resp = datosHistorial };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Guardar_Reasignacion()
        {
            try
            {
                /*DataSet ds = new DataSet();

               var _ConnectionString = ConfigurationManager.ConnectionStrings["Connection_DEV"].ToString(); //Environment.GetEnvironmentVariable("ConnectionStrings:MercurioDB");
               string sp_Name = "tms.";

               using (System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection(_ConnectionString))
               {
                   using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sp_Name, cnn))
                   {
                       cmd.CommandType = CommandType.StoredProcedure;

                       #region Parametros
                       System.Data.SqlClient.SqlParameter param;

                       param = cmd.Parameters.Add("@", SqlDbType.VarChar);
                       param.Value = "";
                       #endregion

                       using (System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(cmd))
                           dataAdapter.Fill(ds);
                   }
               }*/

                var result = new { Success = true, resp = "" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Guardar_Cancelacion(string UeNo, string Transporte, string idMotivo, string User)
        {
            try
            {
                DataSet ds = new DataSet();

               var _ConnectionString = ConfigurationManager.ConnectionStrings["Connection_DEV"].ToString(); //Environment.GetEnvironmentVariable("ConnectionStrings:MercurioDB");
               string sp_Name = "tms.upCorpTms_Ins_GuiaCancelacion";

               using (System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection(_ConnectionString))
               {
                   using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sp_Name, cnn))
                   {
                       cmd.CommandType = CommandType.StoredProcedure;

                        #region Parametros
                        System.Data.SqlClient.SqlParameter param;

                        param = cmd.Parameters.Add("@UeNo", SqlDbType.VarChar);
                        param.Value = UeNo;

                        param = cmd.Parameters.Add("@IdTrackingService", SqlDbType.VarChar);
                        param.Value = Transporte;

                        param = cmd.Parameters.Add("@idMotivo", SqlDbType.Int);
                        param.Value = idMotivo;

                        param = cmd.Parameters.Add("@User", SqlDbType.VarChar);
                        param.Value = User;
                        #endregion

                        using (System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(cmd))
                           dataAdapter.Fill(ds);
                   }
               }

                var result = new { Success = true, resp = "" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}