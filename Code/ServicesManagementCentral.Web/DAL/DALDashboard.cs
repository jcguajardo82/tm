using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.DAL
{
    public class DALDashboard
    {
        public static DataSet upCorpOms_Cns_Tableros(DateTime? fechaini,DateTime? fechafin,int? IdTransportista,int? IdTipoEnvio,int? IdTipoServicio,int? IdTipoLogistica)
        {




            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {
                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEv"].ConnectionString);

                System.Collections.Hashtable parametros = new System.Collections.Hashtable();
                parametros.Add("@fechaini", fechaini);
                parametros.Add("@fechafin", fechafin);

                if(IdTransportista!=null & IdTransportista != 0)
                    parametros.Add("@IdTransportista", IdTransportista);
                if (IdTipoEnvio != null & IdTipoEnvio !=0)
                    parametros.Add("@IdTipoEnvio", IdTipoEnvio);
                if (IdTipoServicio != null & IdTipoServicio != 0)
                    parametros.Add("@IdTipoServicio", IdTipoServicio);
                if (IdTipoLogistica != null & IdTipoLogistica != 0)
                    parametros.Add("@IdTipoLogistica", IdTipoLogistica);

                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[dbo].[upCorpOms_Cns_Tableros]", false,parametros);

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

        public static DataSet upCorpOms_Cns_GraphEnviosVsEstatus(DateTime? fechaini, DateTime? fechafin, int? IdTransportista, int? IdTipoEnvio, int? IdTipoServicio
            , int? IdTipoLogistica,int? TipoAlmacen,int? Almacen,int? TipoFecha,int? Estatus,int? Formato)
        {
            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {

                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEv"].ConnectionString);

                System.Collections.Hashtable parametros = new System.Collections.Hashtable();
                parametros.Add("@fechaini", fechaini);
                parametros.Add("@fechafin", fechafin);

                if (IdTransportista != null & IdTipoEnvio != 0)
                    parametros.Add("@IdTransportista", IdTransportista);
                if (IdTipoEnvio != null & IdTipoEnvio != 0)
                    parametros.Add("@IdTipoEnvio", IdTipoEnvio);
                if (IdTipoServicio != null & IdTipoEnvio != 0)
                    parametros.Add("@IdTipoServicio", IdTipoServicio);
                if (IdTipoLogistica != null & IdTipoEnvio != 0)
                    parametros.Add("@IdTipoLogistica", IdTipoLogistica);
                if (TipoAlmacen != null & TipoAlmacen != 0)
                    parametros.Add("@TipoAlmacen", TipoAlmacen);
                if (Almacen != null & Almacen != 0)
                    parametros.Add("@Almacen", Almacen);
                if (TipoFecha != null & TipoFecha != 0)
                    parametros.Add("@TipoFecha", TipoFecha);
                if (Estatus != null & Estatus != 0)
                    parametros.Add("@Estatus", Estatus);


                    parametros.Add("@Formato", Formato);
  

                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[dbo].[upCorpOms_Cns_GraphEnviosVsEstatus]", false, parametros);

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


        public static DataSet upCorpTms_Cns_DashboardEnviosLogistica(DateTime? fechaini, DateTime? fechafin
    , string IdTransportista, string IdTipoEnvio, string IdTipoServicio
    , string IdTipoLogistica, string json, int? Estatus)
        {
            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {

                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEv"].ConnectionString);

                System.Collections.Hashtable parametros = new System.Collections.Hashtable();
                parametros.Add("@fechaini", fechaini);
                parametros.Add("@fechafin", fechafin);

                if (!string.IsNullOrEmpty(IdTransportista))
                    parametros.Add("@IdTransportista", IdTransportista);

                if (!string.IsNullOrEmpty(IdTipoEnvio))
                    parametros.Add("@IdTipoEnvio", IdTipoEnvio);

                if (!string.IsNullOrEmpty(IdTipoServicio))
                    parametros.Add("@IdTipoServicio", IdTipoServicio);

                //if (!string.IsNullOrEmpty(IdTipoLogistica))
                //    parametros.Add("@IdTipoLogistica", IdTipoLogistica);


                if (!string.IsNullOrEmpty(json))
                    parametros.Add("@json", json);

                if (Estatus != null & Estatus != 0)
                    parametros.Add("@Estatus", Estatus);


//@fechaini datetime = null,      --fecha inicio del periodo
//@fechafin  datetime = null,      --fecha final del periodo
//@IdTransportista varchar(250) = null,  --Numero de transportista
//@IdTipoEnvio varchar(250) = null,  --'1,2,3'-- tipo de envio
//@IdTipoServicio varchar(250) = null,  --'1,4,5'-- tipo de servicio
//@json varchar(max),                   --json con lista de tipo almacen + nro.tienda / proveedor
//@Estatus   int = null-- 1 - EN_TRANSITO, 2 - ENTREGADO, 3 - AMBOS


                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[tms].[upCorpTms_Cns_DashboardEnviosLogistica]", false, parametros);

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


        public static DataSet spOwners_v3_sUP()
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

                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[common].[spOwners_v3_sUP]", false, parametros);

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


        public static DataSet upCorpTms_Cns_DashboardTrans()
        {




            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {
                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEv"].ConnectionString);



                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[tms].[upCorpTms_Cns_DashboardTrans]", false);

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

        public static DataSet upCorpTms_Cns_DashboardTipoEnvio()
        {




            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {
                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEv"].ConnectionString);



                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[tms].[upCorpTms_Cns_DashboardTipoEnvio]", false);

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

        public static DataSet upCorpTms_Cns_DashboardTipoServicio()
        {




            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {
                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEv"].ConnectionString);



                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[tms].[upCorpTms_Cns_DashboardTipoServicio]", false);

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

        public static DataSet upCorpTms_Cns_DashboardTipoLogistica()
        {




            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {
                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEv"].ConnectionString);



                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[tms].[upCorpTms_Cns_DashboardTipoLogistica]", false);

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
    }
}