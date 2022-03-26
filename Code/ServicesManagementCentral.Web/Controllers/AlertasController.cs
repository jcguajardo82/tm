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

                //foreach(DataTable dt in ds.Tables)
                //{
                //    foreach(DataRow row in dt.Rows)
                //    {
                //        RevisionGeneralModel revision = new RevisionGeneralModel();

                //        #region Mapping
                //        revision.DiasRetrasoTotal = row[""].ToString();
                //        revision.Almacen_Proveedor = row[""].ToString();
                //        revision.Consignacion = row[""].ToString();
                //        revision.GuiaTransporte = row[""].ToString();
                //        revision.Transportista = row[""].ToString();
                //        revision.Carrier = row[""].ToString();
                //        revision.TipoEnvio = row[""].ToString();
                //        revision.TipoServicio = row[""].ToString();
                //        revision.Estatustransporte = row[""].ToString();
                //        revision.FechaCompromisoClienteIni = row[""].ToString();
                //        revision.FechaCompromisoClienteFin = row[""].ToString();
                //        revision.FechaCreacion = row[""].ToString();
                //        revision.FechaPago = row[""].ToString();
                //        revision.FechaCompromisoSurtido = row[""].ToString();
                //        revision.FechaSolicitudGuIaReal = row[""].ToString();
                //        revision.DiasRetrasoSurtido = row[""].ToString();
                //        revision.FechaCompromisoRecoleccion = row[""].ToString();
                //        revision.FechaRecoleccionReal = row[""].ToString();
                //        revision.DiasRetrasoRecoleccion = row[""].ToString();
                //        revision.FechaCompromisoEntregaTransporte = row[""].ToString();
                //        revision.FechaEntregaReal = row[""].ToString();
                //        revision.DiasRetrasoTransportista = row[""].ToString();
                //        #endregion

                //        lstRevisionGral.Add(revision);
                //    }
                //}
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        private DataSet Get_PendientesRecoleccion()
        {
            List<PendienteRecoleccionModel> lstPendienteRecoleccion = new List<PendienteRecoleccionModel>();
            DataSet ds = new DataSet();

            try
            {
                var _ConnectionString = ConfigurationManager.ConnectionStrings["Connection_PDP"].ToString(); //Environment.GetEnvironmentVariable("ConnectionStrings:MercurioDB");
                string sp_Name = "";

                using (System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection(_ConnectionString))
                {
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sp_Name, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(cmd))
                            dataAdapter.Fill(ds);
                    }
                }

                //foreach(DataTable dt in ds.Tables)
                //{
                //    foreach(DataRow row in dt.Rows)
                //    {
                //        PendienteRecoleccionModel recoleccion = new PendienteRecoleccionModel();

                //        #region Mapping
                //        recoleccion.DiasRetrasoRecoleccion = row[""].ToString();
                //        recoleccion.Almacen_Proveedor = row[""].ToString();
                //        recoleccion.Consignacion = row[""].ToString();
                //        recoleccion.GuiaTransporte = row[""].ToString();
                //        recoleccion.Transportista = row[""].ToString();
                //        recoleccion.Carrier = row[""].ToString();
                //        recoleccion.TipoEnvio = row[""].ToString();
                //        recoleccion.TipoServicio = row[""].ToString();
                //        recoleccion.FechaCompromisoClienteIni = row[""].ToString();
                //        recoleccion.FechaCompromisoClienteFin = row[""].ToString();
                //        recoleccion.FechaSolicitudGuiaReal = row[""].ToString();
                //        recoleccion.DiasRetrasoSurtido = row[""].ToString();
                //        recoleccion.FechaCompromisoRecoleccion = row[""].ToString();
                //        recoleccion.ReasignacionTransporte = row[""].ToString();
                //        recoleccion.CancelacionGuias = row[""].ToString();
                //        #endregion

                //        lstPendienteRecoleccion.Add(recoleccion);
                //    }
                //}
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        private DataSet Get_PendientesEntrega()
        {
            DataSet ds = new DataSet();
            List<PendienteEntregaModel> lstPendienteEntrega = new List<PendienteEntregaModel>();

            try
            {
                var _ConnectionString = ConfigurationManager.ConnectionStrings["Connection_PDP"].ToString(); //Environment.GetEnvironmentVariable("ConnectionStrings:MercurioDB");
                string sp_Name = "";

                using (System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection(_ConnectionString))
                {
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sp_Name, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(cmd))
                            dataAdapter.Fill(ds);
                    }
                }

                //foreach(DataTable dt in ds.Tables)
                //{
                //    foreach(DataRow row in dt.Rows)
                //    {
                //        PendienteEntregaModel entrega = new PendienteEntregaModel();

                //        #region Mapping
                //        entrega.DiasRetrasoTransportista = row[""].ToString();
                //        entrega.Almacen_Proveedor = row[""].ToString();
                //        entrega.Consignacion = row[""].ToString();
                //        entrega.GuiaTransporte = row[""].ToString();
                //        entrega.Transportista = row[""].ToString();
                //        entrega.Carrier = row[""].ToString();
                //        entrega.TipoEnvio = row[""].ToString();
                //        entrega.TipoServicio = row[""].ToString();
                //        entrega.CodigoPostalDestino = row[""].ToString();
                //        entrega.EstadoDestino = row[""].ToString();
                //        entrega.EstatusTransporte = row[""].ToString();
                //        entrega.FechaCompromisoClienteIni = row[""].ToString();
                //        entrega.FechaCompromisoClienteFin = row[""].ToString();
                //        entrega.FechaRecoleccionReal = row[""].ToString();
                //        entrega.DiasRetrasoRecoleccion = row[""].ToString();
                //        entrega.FechaCompromisoEntregaTransporte = row[""].ToString();
                //        #endregion

                //        lstPendienteEntrega.Add(entrega);
                //    }
                //}

                return ds;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void BuscarOperacionGeneralFiltros(string FecIni, string FecFin, string Transportista, string EstatusTrans) 
        {
            try
            {
                var a = Transportista;
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

    }
}