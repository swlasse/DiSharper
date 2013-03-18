using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication4.Models;

namespace MvcApplication4.Controllers
{
    public class HomeController : Controller
    {

        public ICar MyCar { get; set; }
        private readonly ICar _car;

        public HomeController(ICar car)
        {
            _car = car;
        }

        public ActionResult Index()
        {
            return View(MyCar);
        }
    }
}
