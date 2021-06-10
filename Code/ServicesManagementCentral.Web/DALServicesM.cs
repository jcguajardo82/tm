using Soriana.FWK.Datos.SQL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web
{
    public class DALServicesM
    {

        public static DataSet GetUN()
        {

            DataSet ds = new DataSet();

            //string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            //if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            //{
            //    conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            //}


            try
            {
                //System.Collections.Hashtable parametros = new System.Collections.Hashtable();
                //Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);
                //ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "upCorpOms_Cns_UN", false);
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.upCorpOms_Cns_UN", cnn))
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
        }

        public static DataSet GetTmsTiendas()
        {

            DataSet ds = new DataSet();

            //string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            //if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            //{
            //    conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            //}


            try
            {
                //System.Collections.Hashtable parametros = new System.Collections.Hashtable();
                //Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);
                //ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "upCorpOms_Cns_UN", false);
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV3"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("tms.upCorpTms_Cns_Tiendas", cnn))
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
        }

        public static DataSet GetTmsAlmacenes()
        {
            DataSet ds = new DataSet();
            //string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            //if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            //{
            //    conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            //}
            try
            {
                //System.Collections.Hashtable parametros = new System.Collections.Hashtable();
                //Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);
                //ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "upCorpOms_Cns_UN", false);
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV3"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("tms.upCorpTms_Cns_Almacenes", cnn))
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
        }

        public static DataSet GetCommonAlmacenes()
        {
            DataSet ds = new DataSet();
            //string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            //if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            //{
            //    conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            //}
            try
            {
                //System.Collections.Hashtable parametros = new System.Collections.Hashtable();
                //Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);
                //ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "upCorpOms_Cns_UN", false);
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV3"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("common.Warehouses_sUP", cnn))
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
        }

        public static DataSet GetCommonProveedores(string IdAlmacen)
        {
            DataSet ds = new DataSet();
            //string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            //if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            //{
            //    conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            //}
            try
            {
                //System.Collections.Hashtable parametros = new System.Collections.Hashtable();
                //Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);
                //ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "upCorpOms_Cns_UN", false);
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV3"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("common.Suppliers_sUP", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@idSupplierWHCode", IdAlmacen));
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
        }

        public static DataSet GetTmsAlmacenes(int IdAlmacen)
        {

            DataSet ds = new DataSet();
            try
            {               
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV2"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("tms.upCorpTms_Cns_AlmacenesById", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IdAlmacen", IdAlmacen));
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


        }

        public static int AddAlmacenes(long IdAlmacen,long IdProveedor, 
            int IdTipoAlmacen, bool Paqueteria, bool BigTicket, bool ServicioEstandar, bool ServicioExpress, string UsuarioCreacion)
        {
            int iResult;
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV3"].ConnectionString))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("tms.upCorpTms_Ins_Almacen", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IdAlmacen", IdAlmacen));
                        cmd.Parameters.Add(new SqlParameter("@IdProveedor", IdProveedor));
                        cmd.Parameters.Add(new SqlParameter("@IdTipoAlmacen", IdTipoAlmacen));
                        cmd.Parameters.Add(new SqlParameter("@Paqueteria", Paqueteria));
                        cmd.Parameters.Add(new SqlParameter("@BigTicket", BigTicket));
                        cmd.Parameters.Add(new SqlParameter("@ServicioEstandar", ServicioEstandar));
                        cmd.Parameters.Add(new SqlParameter("@ServicioExpress", ServicioExpress));
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", UsuarioCreacion));

                        iResult = cmd.ExecuteNonQuery();
                    }
                    cnn.Close();
                }
                return iResult;
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

        public static DataSet GetTmsUN()
        {

            DataSet ds = new DataSet();

            //string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            //if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            //{
            //    conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            //}


            try
            {
                //System.Collections.Hashtable parametros = new System.Collections.Hashtable();
                //Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);
                //ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "upCorpOms_Cns_UN", false);
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV3"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.upCorpTms_Cns_UN", cnn))
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
        }


        public static DataSet GetOrdersByUn(string Id_Num_UN)
        {


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
                parametros.Add("@Id_Num_UN", Id_Num_UN);



                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "upCorpOms_Cns_OrdersByUN", false, parametros);

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

        }

        public static DataSet GetSurtidores(string Id_Num_UN)
        {


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
                parametros.Add("@Id_Num_UN", Id_Num_UN);



                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[upCorpOms_Cns_SupplierByUn]", false, parametros);

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

        }

        public static DataSet GetOrdersByOrderNo(string OrderNo)
        {


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



                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "upCorpOms_Cns_OrdersByOrderNo", false, parametros);

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

        }


        public static DataSet GetCarriers()
        {

            DataSet ds = new DataSet();

            //string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            //if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            //{
            //    conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            //}


            try
            {
                //System.Collections.Hashtable parametros = new System.Collections.Hashtable();

                //Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV2"].ConnectionString);

                //ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[tms.upCorpTms_Cns_Trans]", false);
                                
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV2"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("tms.upCorpTms_Cns_Trans", cnn))
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


        }

        public static DataSet AddCarriers(string num, string name, string un)
        {

            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {
                System.Collections.Hashtable parametros = new System.Collections.Hashtable();

                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);



                parametros.Add("@Name", name);
                parametros.Add("@Id_Num_UN", un);
                parametros.Add("@Id_Num_Empleado", num);



                Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[upCorpOms_Ins_Carrier]", false, parametros);

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


        }

        public static int AddTiendas(int IdNumUM, int IdTipoAlmacen, bool Paqueteria, bool BigTicket, bool ServicioEstandar, bool ServicioExpress, string UsuarioCreacion)
        {
            int iResult;
            //DataSet ds = new DataSet();

            //string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            //if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            //{
            //    conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            //}


            try
            {
                //System.Collections.Hashtable parametros = new System.Collections.Hashtable();

                //Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV3"].ConnectionString))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("tms.upCorpTms_Ins_Tienda", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IdNumUM", IdNumUM));
                        cmd.Parameters.Add(new SqlParameter("@IdTipoAlmacen", IdTipoAlmacen));
                        cmd.Parameters.Add(new SqlParameter("@Paqueteria", Paqueteria));
                        cmd.Parameters.Add(new SqlParameter("@BigTicket", BigTicket));
                        cmd.Parameters.Add(new SqlParameter("@ServicioEstandar", ServicioEstandar));
                        cmd.Parameters.Add(new SqlParameter("@ServicioExpress", ServicioExpress));
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", UsuarioCreacion));

                        iResult = cmd.ExecuteNonQuery();
                    }
                    cnn.Close();
                }


                //Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[tms.upCorpTms_Ins_Trans]", false, parametros);

                return iResult;
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

        public static int AddCarriers(long IdTransportista, bool TarifaFija,
            decimal CostoTarifaFija, int Prioridad, decimal NivelServicio, decimal FactorPaqueteria, decimal PorcAdicPaquete,
            int IdTipoAlmacen, bool Paqueteria, bool BigTicket, bool ServicioEstandar, bool ServicioExpress, string UsuarioCreacion)
        {
            int iResult;
            //DataSet ds = new DataSet();

            //string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            //if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            //{
            //    conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            //}


            try
            {
                //System.Collections.Hashtable parametros = new System.Collections.Hashtable();

                //Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV3"].ConnectionString))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("tms.upCorpTms_Ins_Trans", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IdTransportista", IdTransportista));
                        cmd.Parameters.Add(new SqlParameter("@TarifaFija", TarifaFija));
                        cmd.Parameters.Add(new SqlParameter("@CostoTarifaFija", CostoTarifaFija));
                        cmd.Parameters.Add(new SqlParameter("@Prioridad", Prioridad));
                        cmd.Parameters.Add(new SqlParameter("@NivelServicio", NivelServicio));
                        cmd.Parameters.Add(new SqlParameter("@FactorPaqueteria", FactorPaqueteria));
                        cmd.Parameters.Add(new SqlParameter("@PorcAdicPaquete", PorcAdicPaquete));
                        cmd.Parameters.Add(new SqlParameter("@IdTipoAlmacen", IdTipoAlmacen));
                        cmd.Parameters.Add(new SqlParameter("@Paqueteria", Paqueteria));
                        cmd.Parameters.Add(new SqlParameter("@BigTicket", BigTicket));
                        cmd.Parameters.Add(new SqlParameter("@ServicioEstandar", ServicioEstandar));
                        cmd.Parameters.Add(new SqlParameter("@ServicioExpress", ServicioExpress));
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", UsuarioCreacion));

                        iResult = cmd.ExecuteNonQuery();
                    }
                    cnn.Close();
                }


                //Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[tms.upCorpTms_Ins_Trans]", false, parametros);

                return iResult;
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

        public static DataSet EditCarriers(string num, string name, string un, string status)
        {

            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {
                System.Collections.Hashtable parametros = new System.Collections.Hashtable();

                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);



                parametros.Add("@Id_Carrier", num);
                parametros.Add("@Name", name);
                parametros.Add("@Id_Num_UN", un);
                //parametros.Add("@Id_Num_Empleado", num);
                parametros.Add("@Activo", status);



                Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[upCorpOms_Upd_Carrier]", false, parametros);

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


        }

        public static DataSet DeleteCarriers(string num)
        {

            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {
                System.Collections.Hashtable parametros = new System.Collections.Hashtable();

                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

                parametros.Add("@Id_Carrier", num);

                Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[upCorpOms_Del_Carrier]", false, parametros);

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


        }

        public static DataSet GetCarrier(string num)
        {

            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {
                System.Collections.Hashtable parametros = new System.Collections.Hashtable();

                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);



                parametros.Add("@Id_Carrier", num);



                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[upCorpOms_Cns_CarrierById]", false, parametros);

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


        }

        public static DataSet GetCarrier(int IdTransportista)
        {

            DataSet ds = new DataSet();

            //string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            //if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            //{
            //    conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            //}


            try
            {
                //System.Collections.Hashtable parametros = new System.Collections.Hashtable();
                //Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);
                //parametros.Add("@Id_Carrier", num);
                //ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[upCorpOms_Cns_CarrierById]", false, parametros);
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV2"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("tms.upCorpTms_Cns_TransById", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IdTransportista", IdTransportista));
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


        }
        public static DataSet GetSuppliers()
        {

            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {
                System.Collections.Hashtable parametros = new System.Collections.Hashtable();

                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[upCorpOms_Sel_Supplier]", false);

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


        }

        public static DataSet AddSupplier(string num, string name, string un)
        {

            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {
                System.Collections.Hashtable parametros = new System.Collections.Hashtable();

                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);



                parametros.Add("@Name", name);
                parametros.Add("@Id_Num_UN", un);
                parametros.Add("@Id_Num_Empleado", num);



                Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[upCorpOms_Ins_Supplier]", false, parametros);

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


        }

        public static DataSet EditSupplier(string num, string name, string un, string status, string Id_Num_Empleado)
        {

            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {
                System.Collections.Hashtable parametros = new System.Collections.Hashtable();

                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);



                parametros.Add("@Id_Supplier", num);
                parametros.Add("@Name", name);
                parametros.Add("@Id_Num_UN", un);
                parametros.Add("@Id_Num_Empleado", Id_Num_Empleado);
                parametros.Add("@Activo", status);



                Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[upCorpOms_Upd_Supplier]", false, parametros);

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


        }

        public static DataSet DeleteSupplier(string num)
        {

            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {
                System.Collections.Hashtable parametros = new System.Collections.Hashtable();

                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

                parametros.Add("@Id_Supplier", num);

                Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[upCorpOms_Del_Supplier]", false, parametros);

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


        }

        public static DataSet GetSupplier(string num)
        {

            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {
                System.Collections.Hashtable parametros = new System.Collections.Hashtable();

                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);



                parametros.Add("@Id_Supplier", num);



                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[upCorpOms_Cns_SupplierById]", false, parametros);

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


        }

        public static DataSet GetListaEmbarcar(string Id_Num_UN, string Id_Num_Apl = "2")
        {
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
                parametros.Add("@Id_Num_UN", Id_Num_UN);
                parametros.Add("@Id_Num_Apl", Id_Num_Apl);



                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "spSurtido_ListaPorEmbarcarNvo_sup", false, parametros);

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
        }

        public static DataSet GetListaSurtir(string Id_Num_UN)
        {
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
                parametros.Add("@Id_Num_UN", Id_Num_UN);



                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "spSurtido_ListaPorSurtirNvo_sup", false, parametros);

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
        }

        #region ActualizaOrden

        public static DataSet GetMotivoCambioFP(int Id_Num_MotCmbFP=0, bool Bit_Eliminado = false)
        {
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
                parametros.Add("@Id_Num_MotCmbFP", Id_Num_MotCmbFP);
                parametros.Add("@Bit_Eliminado", Bit_Eliminado);

                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "CatMotivoCambioFP_Sup", false, parametros);

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
        }

        public static DataSet UpdFormaPago(int Id_Num_Orden,int Id_Num_MotCmbFP,string Obs_CambioFP)
        {
            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }

            try
            {
                System.Collections.Hashtable parametros = new System.Collections.Hashtable();

                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

                parametros.Add("@OrderNo", Id_Num_Orden);
                parametros.Add("@Id_Num_MotCmbFP", Id_Num_MotCmbFP);
                parametros.Add("@Obs_CambioFP", Obs_CambioFP);

                Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[upCorpOms_Upd_FormaPagoOrden]", false, parametros);

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

        }
        #endregion
        public static DataSet DelObservaciones(int OrderNo, int Id_Cnsc_CarObs, int CnscOrder)
        {
            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }

            try
            {
                System.Collections.Hashtable parametros = new System.Collections.Hashtable();

                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

                parametros.Add("@OrderNo", OrderNo);
                parametros.Add("@Id_Cnsc_CarObs", Id_Cnsc_CarObs);
                parametros.Add("@CnscOrder", CnscOrder);

                Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[upCorpOms_Del_OrderComments]", false, parametros);

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

        }

        public static DataSet AddObservaciones(int OrderNo, string Desc_CarObs)
        {
            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }

            try
            {
                System.Collections.Hashtable parametros = new System.Collections.Hashtable();

                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

                parametros.Add("@OrderNo", OrderNo);
                parametros.Add("@Desc_CarObs", Desc_CarObs);
                

                Soriana.FWK.FmkTools.SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[upCorpOms_Ins_OrderComments]", false, parametros);

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

        }
        public static DataSet GetPasillosEspeciales(int OrderNo)
        {
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



                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "upCorpOms_Cns_ListPasillosEspeciales", false, parametros);

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
        }

        #region Cancelacion Pass

        public static DataSet GetPassCan(int Id_Num_UN, int Id_Num_TipoClave = 1)

        {

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

                parametros.Add("@Id_Num_UN ", Id_Num_UN);

                parametros.Add("@Id_Num_TipoClave ", Id_Num_TipoClave);



                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "cPwdCan_Sup1", false, parametros);



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

        }



        public static DataSet UpdPassCan(int Id_Num_UN, string pwdnom, int Id_Num_TipoClave = 1)

        {

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

                parametros.Add("@Id_Num_UN ", Id_Num_UN);

                parametros.Add("@Id_Num_TipoClave ", Id_Num_TipoClave);

                parametros.Add("@pwdnom ", pwdnom);



                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "cPwdCan_Uup1", false, parametros);



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

        }

        #endregion



        #region Catalogo Pasillos



        public static DataSet GetPasillos(int Id_Num_UN)

        {

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

                parametros.Add("@Id_Num_UN", Id_Num_UN);





                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "PasilloUn_sUp", false, parametros);



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

        }



        public static DataSet AddPasilloUn(int Id_Num_UN)

        {

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

                parametros.Add("@Id_Num_UN", Id_Num_UN);





                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "PasilloUn_iUp", false, parametros);



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

        }



        public static DataSet DelPasilloUn(int Id_Num_UN, int Id_Cnsc_Pasillo)

        {

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

                parametros.Add("@Id_Num_UN", Id_Num_UN);

                parametros.Add("@Id_Cnsc_Pasillo", Id_Cnsc_Pasillo);





                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "PasilloUN_dup", false, parametros);



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

        }



        public static DataSet AddPasilloUnEspecial(int Id_Num_UN)

        {

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

                parametros.Add("@Id_Num_UN", Id_Num_UN);





                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "PasilloUN_CrearEspeciales_iUp", false, parametros);



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

        }





        public static DataSet ReporteMapeoPasillo(int Id_Num_UN, int Id_Cnsc_Pasillo = 0, int Id_Num_Div = 0, int Id_Num_Categ = 0)

        {

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

                parametros.Add("@Id_Num_UN", Id_Num_UN);



                if (Id_Cnsc_Pasillo != 0)

                    parametros.Add("@Id_Cnsc_Pasillo", Id_Cnsc_Pasillo);



                if (Id_Num_Div != 0)

                    parametros.Add("@Id_Num_Div", Id_Num_Div);



                if (Id_Num_Categ != 0)

                    parametros.Add("@Id_Num_Categ", Id_Num_Categ);





                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "ReporteMapeoPasillo_sUp", false, parametros);



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

        }



        public static DataSet GetPasilloCapturaAvance()

        {

            DataSet ds = new DataSet();



            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];

            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))

            {

                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);

            }





            try

            {

                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);



                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "PasilloCapturaAvance_sup", false);



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

        }



        public static DataSet GetPasilloCapturaAvanceUN(int Id_Num_UN)

        {

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

                parametros.Add("@Id_Num_UN", Id_Num_UN);





                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "PasilloCapturaAvanceUN_sup", false, parametros);



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

        }



        public static DataSet UpdPasilloUN(int Id_Num_UN, int Id_Cnsc_Pasillo, string Nom_Pasillo, int Num_Orden)

        {



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

                parametros.Add("@Id_Num_UN", Id_Num_UN);

                parametros.Add("@Id_Cnsc_Pasillo", Id_Cnsc_Pasillo);

                parametros.Add("@Nom_Pasillo", Nom_Pasillo);

                parametros.Add("@Num_Orden", Num_Orden);





                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "PasilloUN_uUp", false, parametros);



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

        }



        public static DataSet PasilloUnEditCateg(int Id_Num_UN, int Id_Cnsc_Pasillo, int Id_Num_Div, int Id_Num_Categ)

        {



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

                parametros.Add("@Id_Num_UN", Id_Num_UN);



                if (Id_Cnsc_Pasillo != 0)

                    parametros.Add("@Id_Cnsc_Pasillo", Id_Cnsc_Pasillo);



                if (Id_Num_Div != 0)

                    parametros.Add("@Id_Num_Div", Id_Num_Div);



                if (Id_Num_Categ != 0)

                    parametros.Add("@Id_Num_Categ", Id_Num_Categ);





                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[PasilloUN_EditCateg_V2_sup]", false, parametros);



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

        }



        public static DataSet AddPasilloUN_Linea(int Id_Num_UN, int Id_Cnsc_Pasillo, int Id_Num_Linea)

        {

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

                parametros.Add("@Id_Num_UN", Id_Num_UN);

                parametros.Add("@Id_Cnsc_Pasillo", Id_Cnsc_Pasillo);

                parametros.Add("@Id_Num_Linea", Id_Num_Linea);





                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "PasilloUN_Linea_iup", false, parametros);



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

        }



        public static DataSet DelPasilloUN_Linea(int Id_Num_UN, int Id_Cnsc_Pasillo, int Id_Num_Linea)

        {

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

                parametros.Add("@Id_Num_UN", Id_Num_UN);

                parametros.Add("@Id_Cnsc_Pasillo", Id_Cnsc_Pasillo);

                parametros.Add("@Id_Num_Linea", Id_Num_Linea);





                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "PasilloUN_Linea_dup", false, parametros);



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

        }





        #endregion
    }
}