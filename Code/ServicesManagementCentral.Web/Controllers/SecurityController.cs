﻿
using Newtonsoft.Json;
using ServicesManagementCentral.Web.Controllers.Api;
using System;
using System.Configuration;
using System.Data;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace ServicesManagement.Web.Controllers
{
    public class UserLoginModel
    {

        public string user { get; set; }
        public string pass { get; set; }
        public string tienda { get; set; }
    }

    public class UserRolModel
    {
        public string nombreRol { get; set; }
        public string idRol { get; set; }
    }

    /// <summary>
    /// Security controller
    /// </summary>
    public class SecurityController : Controller
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="securityManager">The interface SecuritysManager.</param>
        public SecurityController()
        {
        }

        /// <summary>
        /// login page
        /// </summary>
        public ActionResult Login(string MessageLogin = "")
        {
            ViewBag.MessageLogin = MessageLogin;
            return View();
        }

        /// <summary>
        /// Login with user and password
        /// </summary>
        /// <response code="200">Returns the Security ViewModel</response>
        /// <response code="404">If the Security doesn´t exits</response>
        /// <response code="500">If server error exists</response>
        public ActionResult DoLogin(LoginViewModel input)
        {
            ViewBag.MessageLogin = "Accceso Denegado!";
            try
            {
                if (input == null)
                {
                    Session["userFail"] = "Usuario o Password incorrecto";
                    return RedirectToAction("Login", "Security");
                }
                //ResponseViewModel results = _securityManager.LoginActiveDirectory(input);
                //if (results.Code == 200)
                {
                    string loginSerialized = JsonConvert.SerializeObject(input);
                    Session["login"] = loginSerialized;

                    //Agregamos el domain para guardarlo en session
                    input.Domain = ConfigurationManager.AppSettings["Domain"];
                    loginSerialized = JsonConvert.SerializeObject(input);
                    Session.Add("jsonImpersonate", Convert.ToBase64String(Encoding.UTF8.GetBytes(loginSerialized)));

                    ViewBag.MessageLogin = "";


                    if (!validateAccessAD(input))
                    {

                        ViewBag.MessageLogin = "Credenciales no validas";
                        Session.Remove("login");
                        return RedirectToAction("Login", "Security", new { MessageLogin = ViewBag.MessageLogin });
                    }

                    if (!validateAccesDB(input))
                    {

                        ViewBag.MessageLogin = "Credenciales no validas en la Aplicacion";
                        Session.Remove("login");
                        return RedirectToAction("Login", "Security", new { MessageLogin = ViewBag.MessageLogin });
                    }

                    FormsAuthentication.SetAuthCookie(input.Username, false); // set the formauthentication cookie  
                    AuthorizationContext authorization = new AuthorizationContext();


                    string Tienda = validateLogin(input);

                    Session["loginTienda"] = Tienda;


                    return RedirectToAction("MainSystem", "tms");
                }
                //else
                //{
                //    Session.Remove("login");
                //    return RedirectToAction("Login", "Security");
                //}
            }
            catch (System.Exception ex)
            {
                ViewBag.MessageLogin = ex.Message;
                Session.Remove("login");
                return RedirectToAction("Login", "Security", new { MessageLogin = ViewBag.MessageLogin });
            }
        }

        public bool validateAccessAD(LoginViewModel v)
        {

            try
            {
                // DESA - QAS
                // UserLoginModel u = new UserLoginModel { user = v.Username, pass = Soriana.FWK.FmkTools.Seguridad.Encriptar2(v.Password), tienda = "2180" };
                // PROD 1 --
                UserLoginModel u = new UserLoginModel { user = v.Username, pass = Soriana.FWK.FmkTools.Seguridad.Encriptar(v.Password), tienda = "2180" };
                string tienda = null;

                string json2 = string.Empty;
                JavaScriptSerializer js = new JavaScriptSerializer();
                //json2 = js.Serialize(o);

                json2 = JsonConvert.SerializeObject(u);

                js = null;

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Soriana.FWK.FmkTools.RestResponse r = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, System.Configuration.ConfigurationSettings.AppSettings["api_UserAD"], "", json2);

                // DESA - QAS
                // if (r.message.Contains("1"))
                // PROD
                if (r.message.Contains("true") || r.message.Contains("1"))
                {
                    return true;
                }

                return false;
            }
            catch (Exception x)
            {
                return false;
            }



        }

        public string validateLogin(LoginViewModel v)
        {
            try
            {

                UserLoginModel u = new UserLoginModel { user = v.Username, pass = "", tienda = "2180" };
                string tienda = null;

                string json2 = string.Empty;
                JavaScriptSerializer js = new JavaScriptSerializer();
                //json2 = js.Serialize(o);

                json2 = JsonConvert.SerializeObject(u);

                js = null;

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Soriana.FWK.FmkTools.RestResponse r = Soriana.FWK.FmkTools.RestClient.RequestRest(Soriana.FWK.FmkTools.HttpVerb.POST, System.Configuration.ConfigurationSettings.AppSettings["api_Grupos"], "", json2);

                System.Collections.Generic.List<string> grupos = JsonConvert.DeserializeObject<System.Collections.Generic.List<string>>(r.message);




                foreach (string g in grupos)
                {

                    if (g.Trim().ToLower().Contains("tienda"))
                    {

                        foreach (string s in g.Split(' '))
                        {

                            if (Microsoft.VisualBasic.Information.IsNumeric(s))
                            {

                                tienda = Convert.ToInt32(s).ToString();
                                break;
                            }

                        }
                    }

                    if (g.Trim().ToLower().Contains("suc"))
                    {

                        tienda = g.Trim().ToLower().Replace("suc", string.Empty);
                        tienda = Convert.ToInt32(tienda).ToString();
                        break;
                    }
                }

                return tienda;
            }
            catch (Exception x)
            {
                return null;
            }



        }

        public bool validateAccesDB(LoginViewModel v)
        {

            try
            {
                var ds = DALConfig.Autenticar_sUP(v.Username);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    return false;
                }
                else
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        UserRolModel rol = new UserRolModel();
                        rol.idRol = item["rol"].ToString();
                        rol.nombreRol = item["nombreRol"].ToString();
                        Session["UserRol"] = rol;
                    }


                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public ActionResult DoLogout()
        {
            FormsAuthentication.SignOut();
            ViewBag.MessageLogin = "";
            Session.Remove("login");
            Session.Remove("jsonImpersonate");
            return RedirectToAction("Login", "Security");
        }
    }
}