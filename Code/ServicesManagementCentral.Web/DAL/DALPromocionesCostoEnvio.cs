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

        public static DataSet PromocionesCostoEnvio_dUp(int cnscPromo,DateTime FechaUltModif,TimeSpan HoraUltModif,string UsuarioUltModif)
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
                parametros.Add("@FechaUltModif", FechaUltModif);
                parametros.Add("@HoraUltModif", HoraUltModif);
                parametros.Add("@UsuarioUltModif", UsuarioUltModif);


                ds = Soriana.FWK.FmkTools.SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, "[tms].[PromocionesCostoEnvio_dUp]", false, parametros);

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

        public static DataSet PromocionesCostoEnvio_iUp(int IdTipoCatalogo,int IdOwner,int IdFormatoTienda,string PostalCodeOrig,string PostalCodeDestino
            ,string CiudadOrig,string CiudadDest,string EdoOrig ,string EdoDest,int IdSupplierWH,string SupplierName,int IdTransportista,string NombreTransportista
            ,int IdTipoEnvio, int IdTipoServicio,string Cve_CategSAP,string Desc_CategSAP,string Cve_GciaCategSAP,string Desc_GciaCategSAP
            ,string Material_MATNR,int Id_Num_CodBarra,int Id_Num_SKU,decimal PesoMinimo, decimal PesoMaximo,int IdTipoLogistica,int MesesSinIntereses
            ,decimal ComprasMayor,decimal ComprasMenor,DateTime FechaInicioPromo,TimeSpan HoraInicioPromo,DateTime FechaFinPromo,TimeSpan HoraFinPromo
            ,decimal CostoEspecial,decimal TarifaDesc,DateTime FechaCreacion,TimeSpan HoraCreacion,string UsuarioCreacion,bool BitActivo)
            
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
                parametros.Add("@IdTipoCatalogo", IdTipoCatalogo);
                parametros.Add("@IdOwner", IdOwner);
                parametros.Add("@IdFormatoTienda", IdFormatoTienda);
                parametros.Add("@PostalCodeOrig", PostalCodeOrig);
                parametros.Add("@PostalCodeDestino", PostalCodeDestino);
                parametros.Add("@CiudadOrig", CiudadOrig);
                parametros.Add("@CiudadDest", CiudadDest);
                parametros.Add("@EdoOrig", EdoOrig);
                parametros.Add("@EdoDest", EdoDest);
                parametros.Add("@IdSupplierWH", IdSupplierWH);
                parametros.Add("@SupplierName", SupplierName);
                parametros.Add("@IdTransportista", IdTransportista);
                parametros.Add("@NombreTransportista", NombreTransportista);
                parametros.Add("@IdTipoEnvio", IdTipoEnvio);
                parametros.Add("@IdTipoServicio", IdTipoServicio);
                parametros.Add("@Cve_CategSAP", Cve_CategSAP);
                parametros.Add("@Desc_CategSAP", Desc_CategSAP);
                parametros.Add("@Cve_GciaCategSAP", Cve_GciaCategSAP);
                parametros.Add("@Desc_GciaCategSAP", Desc_GciaCategSAP);
                parametros.Add("@Material_MATNR", Material_MATNR);

                parametros.Add("@Id_Num_CodBarra", Id_Num_CodBarra);
                parametros.Add("@Id_Num_SKU", Id_Num_SKU);
                parametros.Add("@PesoMinimo", PesoMinimo);
                parametros.Add("@PesoMaximo", PesoMaximo);
                parametros.Add("@IdTipoLogistica", IdTipoLogistica);
                parametros.Add("@MesesSinIntereses", MesesSinIntereses);
                parametros.Add("@ComprasMayor", ComprasMayor);
                parametros.Add("@ComprasMenor", ComprasMenor);
                parametros.Add("@FechaInicioPromo", FechaInicioPromo);
                parametros.Add("@HoraInicioPromo", HoraInicioPromo);
                parametros.Add("@FechaFinPromo", FechaFinPromo);
                parametros.Add("@HoraFinPromo", HoraFinPromo);
                parametros.Add("@CostoEspecial", CostoEspecial);
                parametros.Add("@TarifaDesc", TarifaDesc);
                parametros.Add("@FechaCreacion", FechaCreacion);
                parametros.Add("@HoraCreacion", HoraCreacion);
                parametros.Add("@UsuarioCreacion", UsuarioCreacion);
                parametros.Add("@BitActivo", BitActivo);
             

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

        public static DataSet PromocionesCostoEnvio_uUp(int cnscPromo,int IdTipoCatalogo, int IdOwner, int IdFormatoTienda, string PostalCodeOrig, string PostalCodeDestino
      , string CiudadOrig, string CiudadDest, string EdoOrig, string EdoDest, int IdSupplierWH, string SupplierName, int IdTransportista, string NombreTransportista
      , int IdTipoEnvio, int IdTipoServicio, string Cve_CategSAP, string Desc_CategSAP, string Cve_GciaCategSAP, string Desc_GciaCategSAP
      , string Material_MATNR, int Id_Num_CodBarra, int Id_Num_SKU, decimal PesoMinimo, decimal PesoMaximo, int IdTipoLogistica, int MesesSinIntereses
      , decimal ComprasMayor, decimal ComprasMenor, DateTime FechaInicioPromo, TimeSpan HoraInicioPromo, DateTime FechaFinPromo, TimeSpan HoraFinPromo
      , decimal CostoEspecial, decimal TarifaDesc, DateTime FechaUltModif, TimeSpan HoraUltModif, string UsuarioUltModif, bool BitActivo)

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
                parametros.Add("@IdTipoCatalogo", IdTipoCatalogo);
                parametros.Add("@IdOwner", IdOwner);
                parametros.Add("@IdFormatoTienda", IdFormatoTienda);
                parametros.Add("@PostalCodeOrig", PostalCodeOrig);
                parametros.Add("@PostalCodeDestino", PostalCodeDestino);
                parametros.Add("@CiudadOrig", CiudadOrig);
                parametros.Add("@CiudadDest", CiudadDest);
                parametros.Add("@EdoOrig", EdoOrig);
                parametros.Add("@EdoDest", EdoDest);
                parametros.Add("@IdSupplierWH", IdSupplierWH);
                parametros.Add("@SupplierName", SupplierName);
                parametros.Add("@IdTransportista", IdTransportista);
                parametros.Add("@NombreTransportista", NombreTransportista);
                parametros.Add("@IdTipoEnvio", IdTipoEnvio);
                parametros.Add("@IdTipoServicio", IdTipoServicio);
                parametros.Add("@Cve_CategSAP", Cve_CategSAP);
                parametros.Add("@Desc_CategSAP", Desc_CategSAP);
                parametros.Add("@Cve_GciaCategSAP", Cve_GciaCategSAP);
                parametros.Add("@Desc_GciaCategSAP", Desc_GciaCategSAP);
                parametros.Add("@Material_MATNR", Material_MATNR);

                parametros.Add("@Id_Num_CodBarra", Id_Num_CodBarra);
                parametros.Add("@Id_Num_SKU", Id_Num_SKU);
                parametros.Add("@PesoMinimo", PesoMinimo);
                parametros.Add("@PesoMaximo", PesoMaximo);
                parametros.Add("@IdTipoLogistica", IdTipoLogistica);
                parametros.Add("@MesesSinIntereses", MesesSinIntereses);
                parametros.Add("@ComprasMayor", ComprasMayor);
                parametros.Add("@ComprasMenor", ComprasMenor);
                parametros.Add("@FechaInicioPromo", FechaInicioPromo);
                parametros.Add("@HoraInicioPromo", HoraInicioPromo);
                parametros.Add("@FechaFinPromo", FechaFinPromo);
                parametros.Add("@HoraFinPromo", HoraFinPromo);
                parametros.Add("@CostoEspecial", CostoEspecial);
                parametros.Add("@TarifaDesc", TarifaDesc);
                parametros.Add("@FechaUltModif", FechaUltModif);
                parametros.Add("@HoraUltModif", HoraUltModif);
                parametros.Add("@UsuarioUltModif", UsuarioUltModif);
                parametros.Add("@BitActivo", BitActivo);


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
    }
}