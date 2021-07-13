namespace ServicesManagement.Web.DAL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    public class DALPromocionesCostoEnvio
    {

        public static DataSet PromocionesCostoEnvio_sUp()
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

                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[tms].[PromocionesCostoEnvio_sUp]", false, parametros);

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

        public static DataSet up_CorpTMS_cmd_SKU(string Material_MATNR, decimal? Id_Num_CodBarra)
        {
            DataSet ds = new DataSet();

            string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            {
                conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            }


            try
            {
                Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV3"].ConnectionString);

                System.Collections.Hashtable parametros = new System.Collections.Hashtable();
                if (!string.IsNullOrEmpty(Material_MATNR))
                    parametros.Add("@Material_MATNR", Material_MATNR);
                if (Id_Num_CodBarra != null)
                    parametros.Add("@Id_Num_CodBarra", Id_Num_CodBarra);


                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[dbo].[up_CorpTMS_cmd_SKU]", false, parametros);

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

        public static DataSet PromocionesCostoEnvioById_sUp(int cnscPromo)
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
                parametros.Add("@cnscPromo", cnscPromo);


                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[tms].[PromocionesCostoEnvioById_sUp]", false, parametros);

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

        public static void PromocionesCostoEnvio_dUp(int cnscPromo, DateTime FechaUltModif, TimeSpan HoraUltModif, string UsuarioUltModif)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString))
            {
                using (SqlCommand sqlComm = new SqlCommand("tms.PromocionesCostoEnvio_dUp", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;


                    sqlComm.Parameters.AddWithValue("@cnscPromo", cnscPromo);
                    sqlComm.Parameters.AddWithValue("@FechaUltModif", FechaUltModif);
                    sqlComm.Parameters.AddWithValue("@HoraUltModif", HoraUltModif);
                    sqlComm.Parameters.AddWithValue("@UsuarioUltModif", UsuarioUltModif);




                    con.Open();
                    sqlComm.ExecuteNonQuery();





                }
            }


        }

        public static void PromocionesCostoEnvio_iUp(int IdTipoCatalogo, int? IdOwner, int? IdFormatoTienda, string PostalCodeOrig, string PostalCodeDestino
            , string CiudadOrig, string CiudadDest, string EdoOrig, string EdoDest, int? IdSupplierWH, string SupplierName, int? IdTransportista, string NombreTransportista
            , int? IdTipoEnvio, int? IdTipoServicio, string Cve_CategSAP, string Desc_CategSAP, string Cve_GciaCategSAP, string Desc_GciaCategSAP
            , string Material_MATNR, int? Id_Num_CodBarra, string nombre_SKU, decimal PesoMinimo, decimal PesoMaximo, int? IdTipoLogistica, int MesesSinIntereses
            , decimal ComprasMayor, decimal ComprasMenor, DateTime FechaInicioPromo, TimeSpan HoraInicioPromo, DateTime FechaFinPromo, TimeSpan HoraFinPromo
            , decimal CostoEspecial, decimal TarifaDesc, DateTime FechaCreacion, TimeSpan HoraCreacion, string UsuarioCreacion, bool BitActivo)

        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString))
            {
                using (SqlCommand sqlComm = new SqlCommand("tms.PromocionesCostoEnvio_iUp", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;




                    sqlComm.Parameters.AddWithValue("@IdTipoCatalogo", IdTipoCatalogo);
                    sqlComm.Parameters.AddWithValue("@IdOwner", IdOwner);
                    sqlComm.Parameters.AddWithValue("@IdFormatoTienda", IdFormatoTienda);
                    sqlComm.Parameters.AddWithValue("@PostalCodeOrig", PostalCodeOrig);
                    sqlComm.Parameters.AddWithValue("@PostalCodeDestino", PostalCodeDestino);
                    sqlComm.Parameters.AddWithValue("@CiudadOrig", CiudadOrig);
                    sqlComm.Parameters.AddWithValue("@CiudadDest", CiudadDest);
                    sqlComm.Parameters.AddWithValue("@EdoOrig", EdoOrig);
                    sqlComm.Parameters.AddWithValue("@EdoDest", EdoDest);
                    sqlComm.Parameters.AddWithValue("@IdSupplierWH", IdSupplierWH);
                    sqlComm.Parameters.AddWithValue("@SupplierName", SupplierName);
                    sqlComm.Parameters.AddWithValue("@IdTransportista", IdTransportista);
                    sqlComm.Parameters.AddWithValue("@NombreTransportista", NombreTransportista);
                    sqlComm.Parameters.AddWithValue("@IdTipoEnvio", IdTipoEnvio);
                    sqlComm.Parameters.AddWithValue("@Cve_CategSAP", Cve_CategSAP);
                    sqlComm.Parameters.AddWithValue("@Desc_CategSAP", Desc_CategSAP);
                    sqlComm.Parameters.AddWithValue("@Cve_GciaCategSAP", Cve_GciaCategSAP);
                    sqlComm.Parameters.AddWithValue("@Desc_GciaCategSAP", Desc_GciaCategSAP);
                    sqlComm.Parameters.AddWithValue("@Material_MATNR", Material_MATNR);
                    sqlComm.Parameters.AddWithValue("@Id_Num_CodBarra", Id_Num_CodBarra);
                    sqlComm.Parameters.AddWithValue("@nombre_SKU", nombre_SKU);
                    sqlComm.Parameters.AddWithValue("@PesoMinimo", PesoMinimo);
                    sqlComm.Parameters.AddWithValue("@PesoMaximo", PesoMaximo);
                    sqlComm.Parameters.AddWithValue("@IdTipoLogistica", IdTipoLogistica);
                    sqlComm.Parameters.AddWithValue("@MesesSinIntereses", MesesSinIntereses);
                    sqlComm.Parameters.AddWithValue("@ComprasMayor", ComprasMayor);
                    sqlComm.Parameters.AddWithValue("@ComprasMenor", ComprasMenor);
                    sqlComm.Parameters.AddWithValue("@FechaInicioPromo", FechaInicioPromo);
                    sqlComm.Parameters.AddWithValue("@HoraInicioPromo", HoraInicioPromo);
                    sqlComm.Parameters.AddWithValue("@FechaFinPromo", FechaFinPromo);
                    sqlComm.Parameters.AddWithValue("@HoraFinPromo", HoraFinPromo);
                    sqlComm.Parameters.AddWithValue("@CostoEspecial", CostoEspecial);
                    sqlComm.Parameters.AddWithValue("@TarifaDesc", TarifaDesc);
                    sqlComm.Parameters.AddWithValue("@FechaCreacion", FechaCreacion);
                    sqlComm.Parameters.AddWithValue("@HoraCreacion", HoraCreacion);
                    sqlComm.Parameters.AddWithValue("@UsuarioCreacion", UsuarioCreacion);
                    sqlComm.Parameters.AddWithValue("@BitActivo", BitActivo);
                    sqlComm.Parameters.AddWithValue("@IdTipoServicio", @IdTipoServicio);



                    con.Open();
                    sqlComm.ExecuteNonQuery();



                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);



                }
            }


            //DataSet ds = new DataSet();

            //string conection = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]];
            //if (System.Configuration.ConfigurationManager.AppSettings["flagConectionDBEcriptado"].ToString().Trim().Equals("1"))
            //{
            //    conection = Soriana.FWK.FmkTools.Seguridad.Desencriptar(ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["AmbienteSC"]]);
            //}


            //try
            //{
            //    Soriana.FWK.FmkTools.SqlHelper.connection_Name(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString);

            //    System.Collections.Hashtable parametros = new System.Collections.Hashtable();
            //    parametros.Add("@IdTipoCatalogo", IdTipoCatalogo);
            //    parametros.Add("@IdOwner", IdOwner);
            //    parametros.Add("@IdFormatoTienda", IdFormatoTienda);
            //    parametros.Add("@PostalCodeOrig", PostalCodeOrig);
            //    parametros.Add("@PostalCodeDestino", PostalCodeDestino);
            //    parametros.Add("@CiudadOrig", CiudadOrig);
            //    parametros.Add("@CiudadDest", CiudadDest);
            //    parametros.Add("@EdoOrig", EdoOrig);
            //    parametros.Add("@EdoDest", EdoDest);
            //    parametros.Add("@IdSupplierWH", IdSupplierWH);
            //    parametros.Add("@SupplierName", SupplierName);
            //    parametros.Add("@IdTransportista", IdTransportista);
            //    parametros.Add("@NombreTransportista", NombreTransportista);
            //    parametros.Add("@IdTipoEnvio", IdTipoEnvio);
            //    parametros.Add("@IdTipoServicio", IdTipoServicio);
            //    parametros.Add("@Cve_CategSAP", Cve_CategSAP);
            //    parametros.Add("@Desc_CategSAP", Desc_CategSAP);
            //    parametros.Add("@Cve_GciaCategSAP", Cve_GciaCategSAP);
            //    parametros.Add("@Desc_GciaCategSAP", Desc_GciaCategSAP);
            //    parametros.Add("@Material_MATNR", Material_MATNR);

            //    parametros.Add("@Id_Num_CodBarra", Id_Num_CodBarra);
            //    parametros.Add("@nombre_SKU", nombre_SKU);
            //    parametros.Add("@PesoMinimo", PesoMinimo);
            //    parametros.Add("@PesoMaximo", PesoMaximo);
            //    parametros.Add("@IdTipoLogistica", IdTipoLogistica);
            //    parametros.Add("@MesesSinIntereses", MesesSinIntereses);
            //    parametros.Add("@ComprasMayor", ComprasMayor);
            //    parametros.Add("@ComprasMenor", ComprasMenor);
            //    parametros.Add("@FechaInicioPromo", FechaInicioPromo);
            //    parametros.Add("@HoraInicioPromo", HoraInicioPromo);
            //    parametros.Add("@FechaFinPromo", FechaFinPromo);
            //    parametros.Add("@HoraFinPromo", HoraFinPromo);
            //    parametros.Add("@CostoEspecial", CostoEspecial);
            //    parametros.Add("@TarifaDesc", TarifaDesc);
            //    parametros.Add("@FechaCreacion", FechaCreacion);
            //    parametros.Add("@HoraCreacion", HoraCreacion);
            //    parametros.Add("@UsuarioCreacion", UsuarioCreacion);
            //    parametros.Add("@BitActivo", BitActivo);


            //    ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[tms].[PromocionesCostoEnvioById_sUp]", false, parametros);

            //    return ds;
            //}
            //catch (SqlException ex)
            //{

            //    throw ex;
            //}
            //catch (System.Exception ex)
            //{

            //    throw ex;
            //}

        }

        public static void PromocionesCostoEnvio_uUp(int cnscPromo, int IdTipoCatalogo, int? IdOwner, int? IdFormatoTienda, string PostalCodeOrig, string PostalCodeDestino
      , string CiudadOrig, string CiudadDest, string EdoOrig, string EdoDest, int? IdSupplierWH, string SupplierName, int? IdTransportista, string NombreTransportista
      , int? IdTipoEnvio, int? IdTipoServicio, string Cve_CategSAP, string Desc_CategSAP, string Cve_GciaCategSAP, string Desc_GciaCategSAP
      , string Material_MATNR, int? Id_Num_CodBarra, string nombre_SKU, decimal PesoMinimo, decimal PesoMaximo, int? IdTipoLogistica, int MesesSinIntereses
      , decimal ComprasMayor, decimal ComprasMenor, DateTime FechaInicioPromo, TimeSpan HoraInicioPromo, DateTime FechaFinPromo, TimeSpan HoraFinPromo
      , decimal CostoEspecial, decimal TarifaDesc, DateTime FechaUltModif, TimeSpan HoraUltModif, string UsuarioUltModif, bool BitActivo)

        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection_DEV"].ConnectionString))
            {
                using (SqlCommand sqlComm = new SqlCommand("tms.PromocionesCostoEnvio_uUp", con))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;


                    sqlComm.Parameters.AddWithValue("@cnscPromo", cnscPromo);
                    sqlComm.Parameters.AddWithValue("@IdTipoCatalogo", IdTipoCatalogo);
                    sqlComm.Parameters.AddWithValue("@IdOwner", IdOwner);
                    sqlComm.Parameters.AddWithValue("@IdFormatoTienda", IdFormatoTienda);
                    sqlComm.Parameters.AddWithValue("@PostalCodeOrig", PostalCodeOrig);
                    sqlComm.Parameters.AddWithValue("@PostalCodeDestino", PostalCodeDestino);
                    sqlComm.Parameters.AddWithValue("@CiudadOrig", CiudadOrig);
                    sqlComm.Parameters.AddWithValue("@CiudadDest", CiudadDest);
                    sqlComm.Parameters.AddWithValue("@EdoOrig", EdoOrig);
                    sqlComm.Parameters.AddWithValue("@EdoDest", EdoDest);
                    sqlComm.Parameters.AddWithValue("@IdSupplierWH", IdSupplierWH);
                    sqlComm.Parameters.AddWithValue("@SupplierName", SupplierName);
                    sqlComm.Parameters.AddWithValue("@IdTransportista", IdTransportista);
                    sqlComm.Parameters.AddWithValue("@NombreTransportista", NombreTransportista);
                    sqlComm.Parameters.AddWithValue("@IdTipoEnvio", IdTipoEnvio);
                    sqlComm.Parameters.AddWithValue("@Cve_CategSAP", Cve_CategSAP);
                    sqlComm.Parameters.AddWithValue("@Desc_CategSAP", Desc_CategSAP);
                    sqlComm.Parameters.AddWithValue("@Cve_GciaCategSAP", Cve_GciaCategSAP);
                    sqlComm.Parameters.AddWithValue("@Desc_GciaCategSAP", Desc_GciaCategSAP);
                    sqlComm.Parameters.AddWithValue("@Material_MATNR", Material_MATNR);
                    sqlComm.Parameters.AddWithValue("@Id_Num_CodBarra", Id_Num_CodBarra);
                    sqlComm.Parameters.AddWithValue("@nombre_SKU", nombre_SKU);
                    sqlComm.Parameters.AddWithValue("@PesoMinimo", PesoMinimo);
                    sqlComm.Parameters.AddWithValue("@PesoMaximo", PesoMaximo);
                    sqlComm.Parameters.AddWithValue("@IdTipoLogistica", IdTipoLogistica);
                    sqlComm.Parameters.AddWithValue("@MesesSinIntereses", MesesSinIntereses);
                    sqlComm.Parameters.AddWithValue("@ComprasMayor", ComprasMayor);
                    sqlComm.Parameters.AddWithValue("@ComprasMenor", ComprasMenor);
                    sqlComm.Parameters.AddWithValue("@FechaInicioPromo", FechaInicioPromo);
                    sqlComm.Parameters.AddWithValue("@HoraInicioPromo", HoraInicioPromo);
                    sqlComm.Parameters.AddWithValue("@FechaFinPromo", FechaFinPromo);
                    sqlComm.Parameters.AddWithValue("@HoraFinPromo", HoraFinPromo);
                    sqlComm.Parameters.AddWithValue("@CostoEspecial", CostoEspecial);
                    sqlComm.Parameters.AddWithValue("@TarifaDesc", TarifaDesc);
                    sqlComm.Parameters.AddWithValue("@FechaUltModif", FechaUltModif);
                    sqlComm.Parameters.AddWithValue("@HoraUltModif", HoraUltModif);
                    sqlComm.Parameters.AddWithValue("@UsuarioUltModif", UsuarioUltModif);
                    sqlComm.Parameters.AddWithValue("@BitActivo", BitActivo);
                    sqlComm.Parameters.AddWithValue("@IdTipoServicio", @IdTipoServicio);



                    con.Open();
                    sqlComm.ExecuteNonQuery();



                    //SqlDataAdapter adapter = new SqlDataAdapter(sqlComm);
                    //adapter.Fill(ds);



                }
            }

        }

        public static DataSet FormatoTiendaByBit_sUp(bool BitActivo = true)
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
                parametros.Add("@BitActivo", BitActivo);


                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[tms].[FormatoTiendaByBit_sUp]", false, parametros);

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