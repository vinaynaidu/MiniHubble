using MiniHubble.Models.Home;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniHubble.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(GetCurrentStatus());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Status()
        {
            return Json(GetCurrentStatus(), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Put)]
        public ActionResult Status(string id)
        {
            Utils.UpdateServiceHeartbeat(id);
            return Json(new { status = "OK" });
        }

        private ServiceStatusModel GetCurrentStatus()
        {
            var currentStatus = Utils.FetchCurrentStatus();
            return new ServiceStatusModel(currentStatus);
        }
    }
}
