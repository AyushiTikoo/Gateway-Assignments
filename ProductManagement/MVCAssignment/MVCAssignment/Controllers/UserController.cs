using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCAssignment.Models;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Helpers;

namespace MVCAssignment.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        [HttpGet]
        public ActionResult Login()
        {
            log.Info("Login Page Started");
            return View();
        }
        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl = "")
        {
            string message = "";
            try
            {
                using (ExampleDBEntities dc = new ExampleDBEntities())
                {
                    var v = dc.Users.Where(a => a.EmailID == login.EmailID).FirstOrDefault();
          
                    if (v != null)
                    {
                        if (string.Compare(login.Password, v.Password) == 0)
                        {

                            Session["UserID"] = v.UserId.ToString();
                            Session["EmailID"] = v.EmailID.ToString();

                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {
                            message = "Invalid credential provided";
                        }
                    }
                    else
                    {
                        message = "Invalid email provided";
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error occured- ", ex);
            }
            log.Info("Login Sucessfully Completed");
            ViewBag.Message = message;
            return View();
        }

        //Logout
        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            Session["UserID"] = null;
            log.Info("Logout done");
            return RedirectToAction("Login", "User");
        }

        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (ExampleDBEntities dc = new ExampleDBEntities())
            {
                var v = dc.Users.Where(a => a.EmailID == emailID).FirstOrDefault();
                log.Info("Email Exists");
                return v != null;
            }
        }
    }
}