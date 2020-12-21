using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RegistrationAndLogin.Models;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using System.Web.Security;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RegistrationAndLogin.Controllers
{
    public class UserController : Controller
    {

        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        //Registration Action

        [HttpGet]
        public ActionResult Registration()
        {
            log.Info("Registration Page Started");
            return View();
        }

        //Registration POST action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")]User user)
        {
            bool Status = false;
            string message = "";
            // Model Validation 
            if (ModelState.IsValid)
            {

                //Email is already Exist 
                var isExist = IsEmailExist(user.EmailID);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }

                //Generate Activation Code 
                user.ActivationCode = Guid.NewGuid();
                
                //Password Hashing 
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword); 
                user.IsEmailVerified = false;

                //Save to Database
                try
                {
                    using (MyDatabaseEntities dc = new MyDatabaseEntities())
                    {
                        dc.Users.Add(user);
                        dc.SaveChanges();

                        //Send Email to User
                        SendVerificationLinkEmail(user.EmailID, user.ActivationCode.ToString());
                        message = " Registration successfully done. Account activation link " +
                            " has been sent to your email id : " + user.EmailID;
                        Status = true;
                    }
                }
                catch(Exception ex)
                {
                    log.Error("Error occured- ", ex);
                }
                
            }
            else
            {
                message = "Invalid Request";
            }
            log.Info("Data Saved to Database successfully");
            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }
        //Verify Account
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            try
            {
                using (MyDatabaseEntities dc = new MyDatabaseEntities())
                {
                    dc.Configuration.ValidateOnSaveEnabled = false; // This line I have added here to avoid 
                                                                    // Confirm password does not match issue on save changes
                    var v = dc.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                    
                    if (v != null)
                    {
                        v.IsEmailVerified = true;
                        dc.SaveChanges();
                        Status = true;
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Request";
                    }
                }
            }
            catch(Exception ex)
            {
                log.Error("Error occured- ",ex);
            }
            log.Info("Account Verified");
            ViewBag.Status = Status;
            return View();
        }

        //Login
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
                using (MyDatabaseEntities dc = new MyDatabaseEntities())
                {
                    var v = dc.Users.Where(a => a.EmailID == login.EmailID).FirstOrDefault();

                    //Using Newtonsoft.json to first serilize the record and then fetch fields from it
                    User u = dc.Users.Where(a => a.EmailID == login.EmailID).FirstOrDefault();
                    string jsonString = JsonConvert.SerializeObject(u);
                    JObject jObject = JObject.Parse(jsonString);
                    string FirstName = (string)jObject.SelectToken("FirstName");
                    string LastName = (string)jObject.SelectToken("LastName");
                    string DateOfBirth = (string)jObject.SelectToken("DateOfBirth");
                    string Mobile = (string)jObject.SelectToken("Mobile");
                    string email = (string)jObject.SelectToken("EmailID");
                    if (v != null)
                    {
                        if (!v.IsEmailVerified)
                        {
                            ViewBag.Message = "Please verify your email first";
                            return View();
                        }
                        if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                        {
                            
                            Session["UserID"] = v.UserID.ToString();
                            Session["FirstName"] = FirstName;
                            Session["LastName"] = LastName;
                            Session["EmailID"] = email;
                            Session["DateOfBirth"] = DateOfBirth;
                            Session["Mobile"] = Mobile;


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
                        message = "Invalid credential provided";
                    }
                }
            }
            catch(Exception ex)
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
            Session["FirstName"] = null;
            Session["LastName"] = null;
            Session["EmailID"] = null;
            Session["DateOfBirth"] = null;
            Session["Mobile"] = null;
            log.Info("Logout done");
            return RedirectToAction("Login", "User");
        }

        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Users.Where(a => a.EmailID == emailID).FirstOrDefault();
                log.Info("Email Exists");
                return v != null;
            }
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            var verifyUrl = "/User/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("tikoochaiya@gmail.com", "Ayushi Tikoo");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "ayushipiyushi13"; // Replace with actual password
            string subject = "Your account is successfully created!";

            string body = "<br/><br/> We are excited to tell you that your account is" +
                " successfully created. Please click on the below link to verify your account" +
                " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                try
                {
                    smtp.Send(message);
                }
                catch(Exception ex)
                {
                    log.Error("Error occured- ",ex);
                }
            log.Info("Email verification sent");
        }

    }
}