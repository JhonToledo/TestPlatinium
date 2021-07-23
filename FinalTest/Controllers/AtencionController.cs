using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalTest.Controllers
{
    public class AtencionController : Controller
    {
        // GET: Atencion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Nuevo()
        {
            return View();
        }
    }
}