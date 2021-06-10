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
    }
}