namespace ServicesManagement.Web.DAL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    public class DALRangosTiemposExcepciones
    {
        public static DataSet RangosTiemposExcepciones_sUP()
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

                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[tms].[RangosTiemposExcepciones_sUP]", false, parametros);

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

        public static DataSet RangosTiemposExcepcionesById_sUP(int cnscDef)
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
                parametros.Add("@cnscDef", cnscDef);


                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[tms].[RangosTiemposExcepcionesById_sUP]", false, parametros);

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

        public static void RangosTiemposExcepciones_dUp(int cnscDef, DateTime FechaUltModif, TimeSpan HoraUltModif, string UsuarioUltModif)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString))
            {
                using (SqlCommand sqlComm = new SqlCommand("tms.PromocionesCostoEnvio_dUp", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;


                    sqlComm.Parameters.AddWithValue("@cnscDef", cnscDef);
                    sqlComm.Parameters.AddWithValue("@FechaUltModif", FechaUltModif);
                    sqlComm.Parameters.AddWithValue("@HoraUltModif", HoraUltModif);
                    sqlComm.Parameters.AddWithValue("@UsuarioUltModif", UsuarioUltModif);




                    con.Open();
                    sqlComm.ExecuteNonQuery();





                }
            }


        }

        public static void RangosTiemposExcepciones_iUp(int Prioridad, int IdTipoCatalogo, int IdOwner, string IdSupplierWH, string IdTipoLogistica
    , int IdTransportista, string NombreTransportista, int DiasExtra, DateTime FechaInicioPromo, TimeSpan HoraInicioPromo, DateTime FechaFinPromo, TimeSpan HoraFinPromo
    , DateTime FechaCreacion, TimeSpan HoraCreacion, string UsuarioCreacion, bool BitActivo)



        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString))
            {
                using (SqlCommand sqlComm = new SqlCommand("tms.RangosTiemposExcepciones_iUp", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;




                    sqlComm.Parameters.AddWithValue("@Prioridad", Prioridad);
                    sqlComm.Parameters.AddWithValue("@IdTipoCatalogo", IdTipoCatalogo);
                    sqlComm.Parameters.AddWithValue("@IdOwner", IdOwner);
                    sqlComm.Parameters.AddWithValue("@IdSupplierWH", IdSupplierWH);
                    sqlComm.Parameters.AddWithValue("@IdTipoLogistica", IdTipoLogistica);
                    sqlComm.Parameters.AddWithValue("@IdTransportista", IdTransportista);
                    sqlComm.Parameters.AddWithValue("@NombreTransportista", NombreTransportista);
                    sqlComm.Parameters.AddWithValue("@DiasExtra", DiasExtra);              
                    sqlComm.Parameters.AddWithValue("@FechaInicioPromo", FechaInicioPromo);
                    sqlComm.Parameters.AddWithValue("@HoraInicioPromo", HoraInicioPromo);
                    sqlComm.Parameters.AddWithValue("@FechaFinPromo", FechaFinPromo);
                    sqlComm.Parameters.AddWithValue("@HoraFinPromo", HoraFinPromo);
                    sqlComm.Parameters.AddWithValue("@FechaCreacion", FechaCreacion);
                    sqlComm.Parameters.AddWithValue("@HoraCreacion", HoraCreacion);
                    sqlComm.Parameters.AddWithValue("@UsuarioCreacion", UsuarioCreacion);
                    sqlComm.Parameters.AddWithValue("@BitActivo", BitActivo);




                    con.Open();
                    sqlComm.ExecuteNonQuery();

                }
            }

        }

        public static void RangosTiemposExcepciones_dUp(int cnscDef,int Prioridad, int IdTipoCatalogo, int IdOwner, string IdSupplierWH, string IdTipoLogistica
, int IdTransportista, string NombreTransportista, int DiasExtra, DateTime FechaInicioPromo, TimeSpan HoraInicioPromo, DateTime FechaFinPromo, TimeSpan HoraFinPromo
, DateTime FechaCreacion, TimeSpan HoraCreacion, string UsuarioCreacion, bool BitActivo)



        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString))
            {
                using (SqlCommand sqlComm = new SqlCommand("tms.RangosTiemposExcepciones_dUp", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;




                    sqlComm.Parameters.AddWithValue("@cnscDef", cnscDef);
                    sqlComm.Parameters.AddWithValue("@Prioridad", Prioridad);
                    sqlComm.Parameters.AddWithValue("@IdTipoCatalogo", IdTipoCatalogo);
                    sqlComm.Parameters.AddWithValue("@IdOwner", IdOwner);
                    sqlComm.Parameters.AddWithValue("@IdSupplierWH", IdSupplierWH);
                    sqlComm.Parameters.AddWithValue("@IdTipoLogistica", IdTipoLogistica);
                    sqlComm.Parameters.AddWithValue("@IdTransportista", IdTransportista);
                    sqlComm.Parameters.AddWithValue("@NombreTransportista", NombreTransportista);
                    sqlComm.Parameters.AddWithValue("@DiasExtra", DiasExtra);
                    sqlComm.Parameters.AddWithValue("@FechaInicioPromo", FechaInicioPromo);
                    sqlComm.Parameters.AddWithValue("@HoraInicioPromo", HoraInicioPromo);
                    sqlComm.Parameters.AddWithValue("@FechaFinPromo", FechaFinPromo);
                    sqlComm.Parameters.AddWithValue("@HoraFinPromo", HoraFinPromo);
                    sqlComm.Parameters.AddWithValue("@FechaCreacion", FechaCreacion);
                    sqlComm.Parameters.AddWithValue("@HoraCreacion", HoraCreacion);
                    sqlComm.Parameters.AddWithValue("@UsuarioCreacion", UsuarioCreacion);
                    sqlComm.Parameters.AddWithValue("@BitActivo", BitActivo);




                    con.Open();
                    sqlComm.ExecuteNonQuery();

                }
            }

        }
    }
}