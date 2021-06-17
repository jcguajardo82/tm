using ServicesManagement.Web.Models.Impex;
using Soriana.FWK.Datos.SQL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.DAL
{
    public class DALImpex
    {
        #region V1

        public static DataSet CobZon_iUp(CobZon item)
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
                parametros.Add("@NoProveedor", item.NoProveedor);
                parametros.Add("@CpOrigen", item.CpOrigen);
                parametros.Add("@CoberturaBigT", item.CoberturaBigT);
                parametros.Add("@CoberturaPyM", item.CoberturaPyM);
                parametros.Add("@CodigoPostal", item.CodigoPostal);
                parametros.Add("@Zonas", item.Zonas);
                parametros.Add("@TiemposEntregaBT", item.TiemposEntregaBT);
                parametros.Add("@TiemposEntregaPyM", item.TiemposEntregaPyM);
                parametros.Add("@Municipio", item.Municipio);
                parametros.Add("@Estado", item.Estado);
                //parametros.Add("@Municipio", item.Municipio);
                parametros.Add("@SiglasPlaza", item.SiglasPlaza);
                parametros.Add("@Lunes", item.Lunes);
                parametros.Add("@Martes", item.Martes);
                parametros.Add("@Miercoles", item.Miercoles);
                parametros.Add("@Jueves", item.Jueves);
                parametros.Add("@Viernes", item.Viernes);
                parametros.Add("@Sabado", item.Sabado);
                parametros.Add("@Domingo", item.Domingo);
                parametros.Add("@Periodicidad", item.Periodicidad);
                parametros.Add("@EsOcurreForzoso", item.EsOcurreForzoso);
                parametros.Add("@Reexpedicion", item.Reexpedicion);
                parametros.Add("@GarantiaMax", item.GarantiaMax);

                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "tms.CobZon_iUp", false, parametros);

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

        public static DataSet CobZon_sUp()
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


                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "tms.CobZon_sUp", false);

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

        public static DataSet KgDinero_iUp(KgDinero item)
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
                parametros.Add("@KG", item.KG);
                parametros.Add("@A1", item.A1);
                parametros.Add("@A2", item.A2);
                parametros.Add("@A3", item.A3);
                parametros.Add("@A4", item.A4);
                parametros.Add("@A5", item.A5);
                parametros.Add("@A6", item.A6);
                parametros.Add("@A7", item.A7);
                parametros.Add("@PorcCliente", item.PorcCliente);
                parametros.Add("@B1", item.B1);
                parametros.Add("@B2", item.B2);
                parametros.Add("@B3", item.B3);
                parametros.Add("@B4", item.B4);
                parametros.Add("@B5", item.B5);
                parametros.Add("@B6", item.B6);
                parametros.Add("@B7", item.B7);


                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "tms.KgDinero_iUp", false, parametros);

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

        public static DataSet KgDinero_sUp()
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


                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "tms.KgDinero_sUp", false);

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


        //public static DataSet upCorpTms_Ins_TransportistaRelacion(List<TransportistaTipoEnvio> TransportistaTipoEnvio
        //    , List<TransportistaPlazas> TransportistaPlazas, List<TransportistaZonaPlazas> TransportistaZonaPlazas
        //    , List<TransportistaTipoEnvioZona> TransportistaTipoEnvioZona, List<TransportistaTipoServicio> TransportistaTipoServicio
        //    , int TipoTran = 1)
        //{
        //    DataSet ds = new DataSet();

        //    string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
        //    if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
        //    {
        //        conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
        //    }


        //    try
        //    {
        //        Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);


        //        System.Collections.Hashtable parametros = new System.Collections.Hashtable();
        //        parametros.Add("@TransportistaTipoEnvioTableType", TransportistaTipoEnvio);
        //        parametros.Add("@TransportistaPlazasTableType", TransportistaPlazas);
        //        parametros.Add("@TransportistaZonaPlazasTableType", TransportistaZonaPlazas);
        //        parametros.Add("@TransportistaTipoEnvioZonaTableType", TransportistaTipoEnvioZona);
        //        parametros.Add("@TransportistaTipoServicioTableType", TransportistaTipoServicio);
        //        parametros.Add("@TipoTran", TipoTran);



        //        ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "tms.upCorpTms_Ins_TransportistaRelacion", false, parametros);

        //        return ds;
        //    }
        //    catch (SqlException ex)
        //    {

        //        throw ex;
        //    }
        //    catch (System.Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        //public static void insert(DataTable TransportistaTipoEnvio
        //    , DataTable TransportistaPlazas, DataTable TransportistaZonaPlazas
        //    , DataTable TransportistaTipoEnvioZona, DataTable TransportistaTipoServicio
        //    , int TipoTran = 1)
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString))
        //    {
        //        using (SqlCommand sqlComm = new SqlCommand("tms.upCorpTms_Ins_TransportistaRelacion", con))
        //        {
        //            sqlComm.CommandType = CommandType.StoredProcedure;



        //            SqlParameter param = new SqlParameter("@TransTipoEnvioType", SqlDbType.Structured)
        //            {
        //                TypeName = "tms.TransportistaTipoEnvioTableType",
        //                Value = TransportistaTipoEnvio
        //            };
        //            sqlComm.Parameters.Add(param);

        //            param = new SqlParameter("@TransPlazasType", SqlDbType.Structured)
        //            {
        //                TypeName = "tms.TransportistaPlazasTableType",
        //                Value = TransportistaPlazas
        //            };
        //            sqlComm.Parameters.Add(param);


        //            param = new SqlParameter("@TransZonaPlazasType", SqlDbType.Structured)
        //            {
        //                TypeName = "tms.TransportistaZonaPlazasTableType",
        //                Value = TransportistaZonaPlazas
        //            };
        //            sqlComm.Parameters.Add(param);


        //            param = new SqlParameter("@TransTipoEnvioZonaType", SqlDbType.Structured)
        //            {
        //                TypeName = "tms.TransportistaTipoEnvioZonaTableType",
        //                Value = TransportistaTipoEnvioZona
        //            };

        //            sqlComm.Parameters.Add(param);


        //            param = new SqlParameter("@TransTipoServicioType", SqlDbType.Structured)
        //            {
        //                TypeName = "tms.TransportistaTipoServicioTableType",
        //                Value = TransportistaTipoServicio
        //            };

        //            sqlComm.Parameters.Add(param);

        //            param = new SqlParameter("@TipoTran", TipoTran);
        //            sqlComm.Parameters.Add(param);


        //            con.Open();
        //            sqlComm.ExecuteReader();



        //            //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
        //            //adapter.Fill(ds);



        //        }
        //    }
        //}
        #endregion

        #region TransportistasZonas
        public static void upCorpTms_Ins_TransportistaPlazas(DataTable TransportistaPlazas)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString))
            {
                using (SqlCommand sqlComm = new SqlCommand("tms.upCorpTms_Ins_TransportistaPlazas", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;



                    SqlParameter param = new SqlParameter("@TransPlazasType", SqlDbType.Structured)
                    {
                        TypeName = "tms.TransportistaPlazasTableType",
                        Value = TransportistaPlazas
                    };
                    sqlComm.Parameters.Add(param);

                    con.Open();
                    sqlComm.ExecuteReader();



                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);



                }
            }
        }

        public static DataSet upCorpTms_Cns_TransportistaPlazas()
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


                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "tms.upCorpTms_Cns_TransportistaPlazas", false);

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

        #region TransportistaRangosPesos
        public static void upCorpTms_Ins_TransportistaRangosPesos(DataTable TransportistaRangosPesos)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString))
            {
                using (SqlCommand sqlComm = new SqlCommand("tms.upCorpTms_Ins_TransportistaRangosPesos", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;



                    SqlParameter param = new SqlParameter("@TransRangosPesosType", SqlDbType.Structured)
                    {
                        TypeName = "tms.TransportistaRangosPesosTableType",
                        Value = TransportistaRangosPesos
                    };
                    sqlComm.Parameters.Add(param);

                    con.Open();
                    sqlComm.ExecuteReader();



                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);



                }
            }
        }

        public static DataSet upCorpTms_Cns_TransportistaRangosPesos()
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


                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "tms.upCorpTms_Cns_TransportistaRangosPesos", false);

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

        #region Cobertura por plaza, de Origen al Destino.
        public static DataSet upCorpTms_Cns_TransportistaDestinosZonas()
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


                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "tms.upCorpTms_Cns_TransportistaDestinosZonas", false);

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

        #region Costo envio Proveedor
        public static DataSet upCorpTms_Cns_TransportistaZonaCostos(int IdTransportista, int IdTipoenvio, int IdTipoServicio)
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
    
                parametros.Add("@IdTransportista", IdTransportista);
                parametros.Add("@IdTipoenvio", IdTipoenvio);
                parametros.Add("@IdTipoServicio", IdTipoServicio);

                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "tms.upCorpTms_Cns_TransportistaZonaCostos", false,parametros);

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

        public static DataSet upCorpTms_Ins_TransportistaZonaCostos(int IdZona, decimal CargoGasolina
            ,decimal PrecioExtraPeso, decimal PrecioInicial, decimal Otros, int IdTransportista, int IdTipoenvio, int IdTipoServicio
            ,int diasEntrega,string CreatedId)
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
                parametros.Add("@IdZona", IdZona);
                parametros.Add("@CargoGasolina", CargoGasolina);
                parametros.Add("@PrecioExtraPeso", PrecioExtraPeso);
                parametros.Add("@PrecioInicial", PrecioInicial);
                parametros.Add("@Otros", Otros);
                parametros.Add("@IdTransportista", IdTransportista);
                parametros.Add("@IdTipoenvio", IdTipoenvio);
                parametros.Add("@IdTipoServicio", IdTipoServicio);
                parametros.Add("@diasEntrega", diasEntrega);
                parametros.Add("@CreatedId", CreatedId);


                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "tms.upCorpTms_Ins_TransportistaZonaCostos", false, parametros);

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

        public static DataSet upCorpTms_Del_TransportistaZonaCostos(int IdZona,  int IdTransportista, int IdTipoenvio, int IdTipoServicio)
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
                parametros.Add("@IdZona", IdZona);
                parametros.Add("@IdTransportista", IdTransportista);
                parametros.Add("@IdTipoenvio", IdTipoenvio);
                parametros.Add("@IdTipoServicio", IdTipoServicio);



                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "tms.upCorpTms_Del_TransportistaZonaCostos", false, parametros);

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