
using System.Web.Mvc;

namespace ServicesManagement.Web.Controllers
{
    /// <summary>
    /// Cpanel controller
    /// </summary>
    [Authorize]
    public class CpanelController : BaseController
    {
        public CpanelController() : base(System.Web.HttpContext.Current)
        {
           
        }
        /// <summary>
        /// Cpanel Index view
        /// </summary>
        public ActionResult Index()
        {
            if (Login == null)
            {
                return RedirectToAction("Login", "Security");
            }

            return View();
        }
      
    }
}