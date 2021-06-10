using System.Web.Mvc;

namespace ServicesManagement.Web.Controllers
{
    /// <summary>
    /// home controller
    /// </summary>
    public class HomeController : BaseController
    {
        public HomeController():base(System.Web.HttpContext.Current)
        {
           
        }

        /// <summary>
        /// home index view
        /// </summary>
        public ActionResult Index()
        {
            //if (Login == null)
            //{
            //    return RedirectToAction("Login", "Security");
            //}

            if (Login != null)
            {
                return RedirectToAction("Index", "Ordenes");
            }

            return RedirectToAction("Index", "CPanel");
            //return RedirectToAction("Index", "Ordenes");
            //  return View();
        }
       
    }
}