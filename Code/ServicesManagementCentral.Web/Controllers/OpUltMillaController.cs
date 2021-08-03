using RestSharp;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ServicesManagement.Web.Controllers
{
    [Authorize]
    public class OpUltMillaController : Controller
    {
        // GET: OpUltMilla
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cobertura()
        {
            return View();
        }


        public ActionResult Costos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object 
            if (Request.Files.Count > 0)
            {
                try
                {

                    string servername = Request.Form["servername"].ToString();
                    string nombre = Request.Form["nombre"].ToString();
                    string extension = Request.Form["extension"].ToString();
                    string tipoFolder = Request.Form["tipo"].ToString();

                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        DateTime dt = DateTime.Now;
                        string dateTime = dt.ToString("yyyyMMddHHmmssfff");

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        //fname = string.Format("{0}_{1}_{2}", nombre, "contenido", dateTime + "." + extension);
                        fname = string.Format("{0}_{1}", nombre, "." + extension);


                        // Get the complete folder path and store the file inside it.  
                        var path = Path.Combine(Server.MapPath("~/Files/"), fname);
                        var pathServerName = servername + "/Files/" + fname;

                        file.SaveAs(path);

                        RestClient restClient = new RestClient(System.Configuration.ConfigurationManager.AppSettings["api_Upload_Files"]);
                        RestRequest restRequest = new RestRequest("/Soriana_Upload_Files?Folder=" + tipoFolder);
                        restRequest.RequestFormat = DataFormat.Json;
                        restRequest.Method = Method.POST;
                        restRequest.AddHeader("Authorization", "Authorization");
                        restRequest.AddHeader("Content-Type", "multipart/form-data");
                        restRequest.AddFile("content", path);
                        //restRequest.AddParameter("folder", tipoFolder);
                        var response = restClient.Execute(restRequest);


                        if (response.StatusDescription.Equals("Bad Request"))
                        {

                            return Json("File Uploaded ERROR!");

                        }

                        // DALManualesOperativos.spManualTitles_iUP(int.Parse(idManual), int.Parse(idOwner), ManualDesc, string.Empty, string.Empty, true, fname, DateTime.Now, User.Identity.Name);

                        //DALManualesOperativos.spManualTitles_iUP(int.Parse(idManual), int.Parse(idOwner), ManualDesc, string.Empty, string.Empty, true, pathServerName, DateTime.Now, User.Identity.Name);
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
    }
}