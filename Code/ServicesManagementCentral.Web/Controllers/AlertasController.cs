using Jitbit.Utils;
using ServicesManagement.Web.Models.Alertas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicesManagement.Web.Controllers
{
    public class AlertasController : Controller
    {
        #region Actions
        public ActionResult OperacionesGeneral(string FecIni, string FecFin, string Transportista, string EstatusTrans)
        {
            if (FecIni == null)
                FecIni = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");

            if (FecFin == null)
                FecFin = DateTime.Now.ToString("yyyy/MM/dd");

            if (EstatusTrans == null)
                EstatusTrans = "3";

            if (Transportista == null || Transportista == "0")
                Transportista = "111111,6569,222222";

            Session["RevisionGeneral"] = OperacionGeneralFiltros(FecIni, FecFin, Transportista, EstatusTrans, "1");
            Session["ddlTransportista"] = upCorpTms_Cns_DashboardTrans();

            ViewBag.Pendientes = false;
            ViewBag.Recoleccion = false;
            ViewBag.Entregadas = false;

            if (EstatusTrans == "3")
            {
                ViewBag.Pendientes = true;
                ViewBag.Recoleccion = true;
                ViewBag.Entregadas = true;
            }
            if (EstatusTrans == "2")
            {
                ViewBag.Pendientes = true;
                ViewBag.Recoleccion = true;
            }
            if (EstatusTrans == "1")
            {
                ViewBag.Entregadas = true;
            }

            ViewBag.FecIniGeneral = FecIni;
            ViewBag.FecFinGeneral = FecFin;
            return View();
        }

        public ActionResult OperacionCedis(string FecIni, string FecFin, string Transportista, string EstatusTrans)
        {
            if (FecIni == null)
                FecIni = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");

            if (FecFin == null)
                FecFin = DateTime.Now.ToString("yyyy/MM/dd");

            if (EstatusTrans == null)
                EstatusTrans = "3";

            if (Transportista == null || Transportista == "0")
                Transportista = "111111,6569,222222";

            Session["RevisionGeneral"] = OperacionGeneralFiltros(FecIni, FecFin, Transportista, EstatusTrans, "3");
            Session["ddlTransportista"] = upCorpTms_Cns_DashboardTrans();

            ViewBag.Pendientes = false;
            ViewBag.Recoleccion = false;
            ViewBag.Entregadas = false;

            if (EstatusTrans == "3")
            {
                ViewBag.Pendientes = true;
                ViewBag.Recoleccion = true;
                ViewBag.Entregadas = true;
            }
            if (EstatusTrans == "2")
            {
                ViewBag.Pendientes = true;
                ViewBag.Recoleccion = true;
            }
            if (EstatusTrans == "1")
            {
                ViewBag.Entregadas = true;
            }

            ViewBag.FecIniGeneral = FecIni;
            ViewBag.FecFinGeneral = FecFin;
            return View();
        }

        public ActionResult OperacionDSV(string FecIni, string FecFin, string Transportista, string EstatusTrans)
        {
            if (FecIni == null)
                FecIni = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");

            if (FecFin == null)
                FecFin = DateTime.Now.ToString("yyyy/MM/dd");

            if (EstatusTrans == null)
                EstatusTrans = "3";

            if (Transportista == null || Transportista == "0")
                Transportista = "111111,6569,222222";

            Session["RevisionGeneral"] = OperacionGeneralFiltros(FecIni, FecFin, Transportista, EstatusTrans, "4");
            Session["ddlTransportista"] = upCorpTms_Cns_DashboardTrans();

            ViewBag.Pendientes = false;
            ViewBag.Recoleccion = false;
            ViewBag.Entregadas = false;

            if (EstatusTrans == "3")
            {
                ViewBag.Pendientes = true;
                ViewBag.Recoleccion = true;
                ViewBag.Entregadas = true;
            }
            if (EstatusTrans == "2")
            {
                ViewBag.Pendientes = true;
                ViewBag.Recoleccion = true;
            }
            if (EstatusTrans == "1")
            {
                ViewBag.Entregadas = true;
            }

            ViewBag.FecIniGeneral = FecIni;
            ViewBag.FecFinGeneral = FecFin;
            return View();
        }

        public ActionResult OperacionDST(string FecIni, string FecFin, string Transportista, string EstatusTrans)
        {
            if (FecIni == null)
                FecIni = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");

            if (FecFin == null)
                FecFin = DateTime.Now.ToString("yyyy/MM/dd");

            if (EstatusTrans == null)
                EstatusTrans = "3";

            if (Transportista == null || Transportista == "0")
                Transportista = "111111,6569,222222";

            Session["RevisionGeneral"] = OperacionGeneralFiltros(FecIni, FecFin, Transportista, EstatusTrans, "2");
            Session["ddlTransportista"] = upCorpTms_Cns_DashboardTrans();

            ViewBag.Pendientes = false;
            ViewBag.Recoleccion = false;
            ViewBag.Entregadas = false;

            if (EstatusTrans == "3")
            {
                ViewBag.Pendientes = true;
                ViewBag.Recoleccion = true;
                ViewBag.Entregadas = true;
            }
            if (EstatusTrans == "2")
            {
                ViewBag.Pendientes = true;
                ViewBag.Recoleccion = true;
            }
            if (EstatusTrans == "1")
            {
                ViewBag.Entregadas = true;
            }

            ViewBag.FecIniGeneral = FecIni;
            ViewBag.FecFinGeneral = FecFin;
            return View();
        }

        #endregion

        #region ObtenerDatos 
        public FileResult Excel(int type)
        {

            DataSet dsGeneral = (DataSet)Session["RevisionGeneral"];

            var d = new DataSet();

            string nombreArchivo = "ReporteAlertas";


            //Excel to create an object file

            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            //Add a sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");


            //Here you can set a variety of styles seemingly font color backgrounds, but not very convenient, there is not set
            //Sheet1 head to add the title of the first row
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);

            DataTable dt = new DataTable();
            var i = 0;
            if (type == 1)
            {
                row1.CreateCell(0).SetCellValue("Dias de Retraso recoleccion");
                row1.CreateCell(1).SetCellValue("Almacen/Proveedor");
                row1.CreateCell(2).SetCellValue("Pedido");
                row1.CreateCell(3).SetCellValue("Guia Transportista");
                row1.CreateCell(4).SetCellValue("Transportista");
                row1.CreateCell(5).SetCellValue("Carrier");
                row1.CreateCell(6).SetCellValue("Tipo de envio");
                row1.CreateCell(7).SetCellValue("Tipo de Servicio");
                row1.CreateCell(8).SetCellValue("Fecha Comprimiso cliente Inicio");
                row1.CreateCell(9).SetCellValue("Fecha Comprimiso cliente Fin");
                row1.CreateCell(10).SetCellValue("Fecha fin surtido real");
                row1.CreateCell(11).SetCellValue("Dias de retraso surtido");
                row1.CreateCell(12).SetCellValue("Estatus de Retraso Surtido");
                row1.CreateCell(13).SetCellValue("Fecha compromiso recoleccion");
                dt = dsGeneral.Tables[0];

                foreach (DataRow item in dt.Rows)
                {

                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                    rowtemp.CreateCell(0).SetCellValue(item[0].ToString());
                    rowtemp.CreateCell(1).SetCellValue(item[2].ToString());
                    rowtemp.CreateCell(2).SetCellValue(item[3].ToString());
                    rowtemp.CreateCell(3).SetCellValue(item[4].ToString());
                    rowtemp.CreateCell(4).SetCellValue(item[5].ToString());
                    rowtemp.CreateCell(5).SetCellValue(item[6].ToString());
                    rowtemp.CreateCell(6).SetCellValue(item[7].ToString());
                    rowtemp.CreateCell(7).SetCellValue(item[8].ToString());
                    rowtemp.CreateCell(8).SetCellValue(item[9].ToString());
                    rowtemp.CreateCell(9).SetCellValue(item[10].ToString());
                    rowtemp.CreateCell(10).SetCellValue(item[11].ToString());
                    rowtemp.CreateCell(11).SetCellValue(item[12].ToString());
                    if (item[13].ToString() == "V")
                        rowtemp.CreateCell(12).SetCellValue("En Tiempo");
                    if (item[13].ToString() == "A")
                        rowtemp.CreateCell(12).SetCellValue("Por Vencer");
                    if (item[13].ToString() == "R")
                        rowtemp.CreateCell(12).SetCellValue("Vencida");
                    rowtemp.CreateCell(13).SetCellValue(item[14].ToString());
                    i++;
                }
            }
            if (type == 2)
            {
                row1.CreateCell(0).SetCellValue("Dias de Retraso Transportista");
                row1.CreateCell(1).SetCellValue("Almacen/Proveedor");
                row1.CreateCell(2).SetCellValue("Pedido");
                row1.CreateCell(3).SetCellValue("Guia Transportista");
                row1.CreateCell(4).SetCellValue("Transportista");
                row1.CreateCell(5).SetCellValue("Carrier");
                row1.CreateCell(6).SetCellValue("Tipo de envio");
                row1.CreateCell(7).SetCellValue("Tipo de Servicio");

                row1.CreateCell(8).SetCellValue("Codigo Postal");
                row1.CreateCell(9).SetCellValue("Estado Destino");
                row1.CreateCell(10).SetCellValue("Estatus Transporte");
                row1.CreateCell(11).SetCellValue("Fecha Comprimiso cliente Inicio");
                row1.CreateCell(12).SetCellValue("Fecha Comprimiso cliente Fin");
                row1.CreateCell(13).SetCellValue("Fecha Recoleccion real");
                row1.CreateCell(14).SetCellValue("Dias de retraso recoleccion");
                row1.CreateCell(15).SetCellValue("Estatus de Retraso Recoleccion");
                row1.CreateCell(16).SetCellValue("Estatus de Retraso Transportista");
                dt = dsGeneral.Tables[1];

                foreach (DataRow item in dt.Rows)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                    rowtemp.CreateCell(0).SetCellValue(item[0].ToString());
                    rowtemp.CreateCell(1).SetCellValue(item[2].ToString());
                    rowtemp.CreateCell(2).SetCellValue(item[3].ToString());
                    rowtemp.CreateCell(3).SetCellValue(item[4].ToString());
                    rowtemp.CreateCell(4).SetCellValue(item[5].ToString());
                    rowtemp.CreateCell(5).SetCellValue(item[6].ToString());
                    rowtemp.CreateCell(6).SetCellValue(item[7].ToString());
                    rowtemp.CreateCell(7).SetCellValue(item[8].ToString());
                    rowtemp.CreateCell(8).SetCellValue(item[9].ToString());
                    rowtemp.CreateCell(9).SetCellValue(item[10].ToString());
                    rowtemp.CreateCell(10).SetCellValue(item[11].ToString());
                    rowtemp.CreateCell(11).SetCellValue(item[12].ToString());
                    rowtemp.CreateCell(12).SetCellValue(item[13].ToString());
                    rowtemp.CreateCell(13).SetCellValue(item[16].ToString());
                    rowtemp.CreateCell(14).SetCellValue(item[14].ToString());

                    if (item[15].ToString() == "V")
                        rowtemp.CreateCell(15).SetCellValue("En Tiempo");
                    if (item[15].ToString() == "A")
                        rowtemp.CreateCell(15).SetCellValue("Por Vencer");
                    if (item[15].ToString() == "R")
                        rowtemp.CreateCell(15).SetCellValue("Vencida");

                    if (item[1].ToString() == "V")
                        rowtemp.CreateCell(16).SetCellValue("En Tiempo");
                    if (item[1].ToString() == "A")
                        rowtemp.CreateCell(16).SetCellValue("Por Vencer");
                    if (item[1].ToString() == "R")
                        rowtemp.CreateCell(16).SetCellValue("Vencida");
                    i++;
                }


            }
            if (type == 3)
            {
                row1.CreateCell(0).SetCellValue("Dias de Retraso Total");
                row1.CreateCell(1).SetCellValue("Almacen/Proveedor");
                row1.CreateCell(2).SetCellValue("Pedido");
                row1.CreateCell(3).SetCellValue("Guia Transportista");
                row1.CreateCell(4).SetCellValue("Transportista");
                row1.CreateCell(5).SetCellValue("Carrier");
                row1.CreateCell(6).SetCellValue("Tipo de envio");
                row1.CreateCell(7).SetCellValue("Tipo de Servicio");
                row1.CreateCell(8).SetCellValue("Estatus Transporte");
                row1.CreateCell(9).SetCellValue("Fecha Comprimiso cliente Inicio");
                row1.CreateCell(10).SetCellValue("Fecha Comprimiso cliente Fin");
                row1.CreateCell(11).SetCellValue("Fecha Creacion");
                row1.CreateCell(12).SetCellValue("Fecha de Pago");
                row1.CreateCell(13).SetCellValue("Fecha compromiso surtido");
                row1.CreateCell(14).SetCellValue("Fecha solicitud guía real");
                row1.CreateCell(15).SetCellValue("Dias de Retraso Surtido");
                row1.CreateCell(16).SetCellValue("Estatus de Retraso Surtido");

                row1.CreateCell(17).SetCellValue("Fecha compromiso Recoleccion");
                row1.CreateCell(18).SetCellValue("Fecha Recoleccion real");
                row1.CreateCell(19).SetCellValue("Dias de Retraso Recoleccion");
                row1.CreateCell(20).SetCellValue("Estatus de Retraso Recoleccion");

                row1.CreateCell(21).SetCellValue("Fecha compromiso Entrega");
                row1.CreateCell(22).SetCellValue("Fecha Entrega real");
                row1.CreateCell(23).SetCellValue("Dias de Retraso Transportista");
                row1.CreateCell(24).SetCellValue("Estatus de Retraso Transportista");
                row1.CreateCell(25).SetCellValue("Estatus de Retraso Total");

                if (dsGeneral.Tables.Count == 3)
                    dt = dsGeneral.Tables[2];
                else
                    dt = dsGeneral.Tables[0];

                foreach (DataRow item in dt.Rows)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                    rowtemp.CreateCell(0).SetCellValue(item[0].ToString());
                    rowtemp.CreateCell(1).SetCellValue(item[2].ToString());
                    rowtemp.CreateCell(2).SetCellValue(item[3].ToString());
                    rowtemp.CreateCell(3).SetCellValue(item[4].ToString());
                    rowtemp.CreateCell(4).SetCellValue(item[5].ToString());
                    rowtemp.CreateCell(5).SetCellValue(item[6].ToString());
                    rowtemp.CreateCell(6).SetCellValue(item[7].ToString());
                    rowtemp.CreateCell(7).SetCellValue(item[8].ToString());
                    rowtemp.CreateCell(8).SetCellValue(item[9].ToString());
                    rowtemp.CreateCell(9).SetCellValue(item[10].ToString());
                    rowtemp.CreateCell(10).SetCellValue(item[11].ToString());
                    rowtemp.CreateCell(11).SetCellValue(item[12].ToString());
                    rowtemp.CreateCell(12).SetCellValue(item[13].ToString());
                    rowtemp.CreateCell(13).SetCellValue(item[14].ToString());
                    rowtemp.CreateCell(14).SetCellValue(item[15].ToString());
                    rowtemp.CreateCell(15).SetCellValue(item[16].ToString());

                    if (item[17].ToString() == "V")
                        rowtemp.CreateCell(16).SetCellValue("En Tiempo");
                    if (item[17].ToString() == "A")
                        rowtemp.CreateCell(16).SetCellValue("Por Vencer");
                    if (item[17].ToString() == "R")
                        rowtemp.CreateCell(16).SetCellValue("Vencida");

                    rowtemp.CreateCell(17).SetCellValue(item[18].ToString());
                    rowtemp.CreateCell(18).SetCellValue(item[19].ToString());
                    rowtemp.CreateCell(19).SetCellValue(item[20].ToString());

                    if (item[21].ToString() == "V")
                        rowtemp.CreateCell(20).SetCellValue("En Tiempo");
                    if (item[21].ToString() == "A")
                        rowtemp.CreateCell(20).SetCellValue("Por Vencer");
                    if (item[21].ToString() == "R")
                        rowtemp.CreateCell(20).SetCellValue("Vencida");

                    rowtemp.CreateCell(21).SetCellValue(item[22].ToString());
                    rowtemp.CreateCell(22).SetCellValue(item[23].ToString());
                    rowtemp.CreateCell(23).SetCellValue(item[24].ToString());

                    if (item[25].ToString() == "V")
                        rowtemp.CreateCell(24).SetCellValue("En Tiempo");
                    if (item[25].ToString() == "A")
                        rowtemp.CreateCell(24).SetCellValue("Por Vencer");
                    if (item[25].ToString() == "R")
                        rowtemp.CreateCell(24).SetCellValue("Vencida");


                    if (item[1].ToString() == "V")
                        rowtemp.CreateCell(25).SetCellValue("En Tiempo");
                    if (item[1].ToString() == "A")
                        rowtemp.CreateCell(25).SetCellValue("Por Vencer");
                    if (item[1].ToString() == "R")
                        rowtemp.CreateCell(25).SetCellValue("Vencida");
                    i++;
                }


            }


            //                                                
            //The data is written progressively sheet1 each row

            //for (int i = 0; i < lst.Count; i++)
            //{
            //    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
            //    rowtemp.CreateCell(0).SetCellValue(lst[i].Consignacion.ToString());
            //    rowtemp.CreateCell(1).SetCellValue(lst[i].CodigoAlmacen.ToString());
            //    rowtemp.CreateCell(2).SetCellValue(lst[i].NomAlmacen.ToString());
            //    rowtemp.CreateCell(3).SetCellValue(lst[i].TipoAlmacen.ToString());
            //    rowtemp.CreateCell(4).SetCellValue(lst[i].Zona.ToString());
            //    rowtemp.CreateCell(5).SetCellValue(lst[i].TransAsignado.ToString());
            //    rowtemp.CreateCell(6).SetCellValue(lst[i].TipoEnvio.ToString());
            //    rowtemp.CreateCell(7).SetCellValue(lst[i].GuiaTrans.ToString());
            //    rowtemp.CreateCell(8).SetCellValue(lst[i].GuiaSoriana.ToString());
            //    rowtemp.CreateCell(9).SetCellValue(lst[i].EstatusEntrega.ToString());
            //    rowtemp.CreateCell(10).SetCellValue(lst[i].FecRecoleccion.ToString());
            //    rowtemp.CreateCell(11).SetCellValue(lst[i].FecEntClient.ToString());
            //    rowtemp.CreateCell(12).SetCellValue(lst[i].PesoKGSis.ToString());
            //    rowtemp.CreateCell(13).SetCellValue(lst[i].PesoVolSis.ToString());
            //    rowtemp.CreateCell(14).SetCellValue(lst[i].PesoMayorSis.ToString());
            //    rowtemp.CreateCell(15).SetCellValue(lst[i].CostoTransCotizado.ToString());
            //    rowtemp.CreateCell(16).SetCellValue(lst[i].PesoKGGuias.ToString());
            //    rowtemp.CreateCell(17).SetCellValue(lst[i].PesoVolGuias.ToString());
            //    rowtemp.CreateCell(18).SetCellValue(lst[i].PesoMayorGuias.ToString());
            //    rowtemp.CreateCell(19).SetCellValue(lst[i].CostoTransGuias.ToString());
            //    rowtemp.CreateCell(20).SetCellValue(lst[i].PesoKGTrans.ToString());
            //    rowtemp.CreateCell(21).SetCellValue(lst[i].PesoVolTrans.ToString());
            //    rowtemp.CreateCell(22).SetCellValue(lst[i].PesoMayorTrans.ToString());
            //    rowtemp.CreateCell(23).SetCellValue(lst[i].CostoTransReal.ToString());
            //    rowtemp.CreateCell(24).SetCellValue(lst[i].Variacion1.ToString());
            //    rowtemp.CreateCell(25).SetCellValue(lst[i].Variacion2.ToString());



            //}


            //  Write to the client 

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            book.Write(ms);

            ms.Seek(0, SeekOrigin.Begin);

            DateTime t = DateTime.Now;

            string dateTime = t.ToString("yyyyMMddHHmmssfff");

            string fileName = nombreArchivo + "_" + dateTime + ".xls";

            return File(ms, "application/vnd.ms-excel", fileName);

        }
        private DataSet Get_RevisionGeneral(string operacion)
        {
            List<RevisionGeneralModel> lstRevisionGral = new List<RevisionGeneralModel>();
            DataSet ds = new DataSet();

            try
            {
                var _ConnectionString = ConfigurationManager.ConnectionStrings["Connection_DEV"].ToString(); //Environment.GetEnvironmentVariable("ConnectionStrings:MercurioDB");
                string sp_Name = "tms.upCorpTms_Cns_Alertas";

                DateTime fecha = DateTime.Now;
                DateTime fechaINI = fecha.AddDays(-7);

                using (System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection(_ConnectionString))
                {
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sp_Name, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        #region Parametros
                        System.Data.SqlClient.SqlParameter param;

                        param = cmd.Parameters.Add("@fechaini", SqlDbType.VarChar);
                        param.Value = fechaINI.ToString("yyyy-MM-dd"); //"2022-01-01";

                        param = cmd.Parameters.Add("@fechafin", SqlDbType.VarChar);
                        param.Value = fecha.ToString("yyyy-MM-dd");

                        param = cmd.Parameters.Add("@transportistas", SqlDbType.VarChar);
                        param.Value = "111111,6569,222222";

                        param = cmd.Parameters.Add("@idOwner", SqlDbType.Int);
                        param.Value = operacion;

                        param = cmd.Parameters.Add("@estatusTransporte", SqlDbType.Int);
                        param.Value = 3;
                        #endregion

                        using (System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(cmd))
                            dataAdapter.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet OperacionGeneralFiltros(string FecIni, string FecFin, string Transportista, string EstatusTrans, string operacion)
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
                        param.Value = operacion;

                        param = cmd.Parameters.Add("@estatusTransporte", SqlDbType.Int);
                        param.Value = EstatusTrans; //3;
                        #endregion

                        using (System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(cmd))
                            dataAdapter.Fill(ds);
                    }
                }
                int EnTiempo1 = 0, PorVencer1 = 0, Vencidas1 = 0, EnTiempo2 = 0, PorVencer2 = 0, Vencidas2 = 0, EnTiempo3 = 0, PorVencer3 = 0, Vencidas3 = 0;
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (dt.TableName == "Table")
                        {
                            if (ds.Tables.Count == 3)
                            {
                                if (row["ColorRetrasoRecoleccion"].ToString() == "V")
                                    EnTiempo1++;
                                if (row["ColorRetrasoRecoleccion"].ToString() == "A")
                                    PorVencer1++;
                                if (row["ColorRetrasoRecoleccion"].ToString() == "R")
                                    Vencidas1++;
                            }
                            else
                            {
                                if (row["ColorRetrasoTotal"].ToString() == "V")
                                    EnTiempo3++;
                                if (row["ColorRetrasoTotal"].ToString() == "A")
                                    PorVencer3++;
                                if (row["ColorRetrasoTotal"].ToString() == "R")
                                    Vencidas3++;

                            }
                        }
                        if (dt.TableName == "Table1")
                        {
                            if (row["ColorRetrasoTransportista"].ToString() == "V")
                                EnTiempo2++;
                            if (row["ColorRetrasoTransportista"].ToString() == "A")
                                PorVencer2++;
                            if (row["ColorRetrasoTransportista"].ToString() == "R")
                                Vencidas2++;
                        }
                        if (dt.TableName == "Table2")
                        {
                            if (row["ColorRetrasoTotal"].ToString() == "V")
                                EnTiempo3++;
                            if (row["ColorRetrasoTotal"].ToString() == "A")
                                PorVencer3++;
                            if (row["ColorRetrasoTotal"].ToString() == "R")
                                Vencidas3++;

                        }
                    }
                }
                ViewBag.EnTiempo1 = EnTiempo1;
                ViewBag.PorVencer1 = PorVencer1;
                ViewBag.Vencidas1 = Vencidas1;

                ViewBag.EnTiempo2 = EnTiempo2;
                ViewBag.PorVencer2 = PorVencer2;
                ViewBag.Vencidas2 = Vencidas2;

                ViewBag.EnTiempo3 = EnTiempo3;
                ViewBag.PorVencer3 = PorVencer3;
                ViewBag.Vencidas3 = Vencidas3;

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult BuscarOperacionGeneralFiltros(string FecIni, string FecFin, string Transportista, string EstatusTrans, string operacion)
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
                        param.Value = operacion;

                        param = cmd.Parameters.Add("@estatusTransporte", SqlDbType.Int);
                        param.Value = EstatusTrans; //3;
                        #endregion

                        using (System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(cmd))
                            dataAdapter.Fill(ds);
                    }
                }

                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow row in dt.Rows)
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
                        if (dt.TableName == "Table1")
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
                        if (dt.TableName == "Table2")
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
            catch (Exception ex)
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

                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow row in dt.Rows)
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

                foreach (DataTable dt in ds2.Tables)
                {
                    foreach (DataRow row in dt.Rows)
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

                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (dt.TableName == "Table")
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
                        else if (dt.TableName == "Table1")
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
            catch (Exception ex)
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