using FormValidation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormValidation.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult UserDetails(UserModel sm)
        {
            if (ModelState.IsValid)
            {
                ViewBag.name = sm.Name;
                ViewBag.email = sm.Email;
                ViewBag.age = sm.Age;
                ViewBag.mobile = sm.Mobile;
                ViewBag.password = sm.Password;
                ViewBag.address = sm.Address;
                return View("Index");
            }
            else
            {
                ViewBag.name = "No Data";
                ViewBag.email = "No Data";
                ViewBag.age = "No Data";
                ViewBag.mobile = "No Data";
                ViewBag.password = "No Data";
                ViewBag.address = "No Data";
                return View("Index");
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}