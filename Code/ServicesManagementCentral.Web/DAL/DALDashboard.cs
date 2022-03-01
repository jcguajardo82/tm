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
            , int? IdTipoLogistica,string TipoAlmacen,string Almacen,int? TipoFecha,int? Estatus)
        {
            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {

//@fechaini datetime = null,
//@fechafin datetime = null,
//@IdTransportista bigint = null,
//@IdTipoEnvio int = null,
//@IdTipoServicio int = null,
//@IdTipoLogistica int = null,
//@TipoAlmacen varchar(5) = null,
//@Almacen varchar(100) = null,
//@TipoFecha int = null, --1 - fecha creacion, 2 - fecha recoleccion, 3 - fecha entrega
//@Estatus int = null-- 1 - EN_TRANSITO, 2 - ENTREGADO, 3 - AMBOS

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
                if (!string.IsNullOrEmpty(TipoAlmacen))
                    parametros.Add("@TipoAlmacen", TipoAlmacen);
                if (!string.IsNullOrEmpty(Almacen))
                    parametros.Add("@Almacen", Almacen);
                if (TipoFecha != null & TipoFecha != 0)
                    parametros.Add("@TipoFecha", TipoFecha);
                if (Estatus != null & Estatus != 0)
                    parametros.Add("@Estatus", Estatus);

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