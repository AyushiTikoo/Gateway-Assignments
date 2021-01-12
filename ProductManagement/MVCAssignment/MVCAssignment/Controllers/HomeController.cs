using MVCAssignment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using log4net;

namespace MVCAssignment.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                log.Info("Application started");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
        ExampleDBEntities db = new ExampleDBEntities();
        //Searching
        public ActionResult Product(int? page,string sortOrder, string currentFilter1, string currentFilter2, string currentFilter3, string CurrentSort, string CategoryName, string ProductName, string Price)
        {
            if (Session["UserID"] != null)
            {
                if (CategoryName != null )
                {
                    page = 1;
                }
                else
                {
                    CategoryName = currentFilter1;
                }
                ViewBag.CurrentFilter1 = CategoryName;
                if (ProductName != null)
                {
                    page = 1;
                }
                else
                {                   
                    ProductName = currentFilter2;
                }
                ViewBag.currentFilter2 = ProductName;
                if (Price != null)
                {
                    page = 1;
                }
                else
                {
                    Price = currentFilter3;
                }
                ViewBag.currentFilter3 = Price;
                var products = from p in db.Products
                               select p;
                log.Info("Searching of products");
                //advance searching
                if (!string.IsNullOrEmpty(ProductName))
                {
                    products = products.Where(p => p.ProductName.Contains(ProductName));
                }
                if (!string.IsNullOrEmpty(CategoryName))
                {
                    products = products.Where(p => p.Category.CategoryName.Contains(CategoryName));
                }
                if (!string.IsNullOrEmpty(Price))
                {
                    products = products.Where(p => p.Price.Contains(Price));
                }
                int pageSize = 5
                    ;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                ViewBag.CurrentSort = sortOrder;
                sortOrder = String.IsNullOrEmpty(sortOrder) ? "CategoryName" : sortOrder;
                log.Info("Sorting of products");
                switch (sortOrder)
                {
                    case "CategoryName":
                        if (sortOrder.Equals(CurrentSort))
                        {
                            products = products.OrderByDescending(p => p.Category.CategoryName);
                            ViewBag.CurrentSort = null;
                        }
                        else
                            products = products.OrderBy(p => p.Category.CategoryName);

                        break;
                    case "ProductName":
                        if (sortOrder.Equals(CurrentSort))
                        {
                            products = products.OrderByDescending(p => p.ProductName);
                            ViewBag.CurrentSort = null;
                        }
                        else
                            products = products.OrderBy(p => p.ProductName);
                        break;
                    case "Price":
                        if (sortOrder.Equals(CurrentSort))
                        {
                            products = products.OrderByDescending(p => p.Price);
                            ViewBag.CurrentSort = null;
                        }
                        else
                            products = products.OrderBy(p => p.Price);
                        break;

                }
                return View(products.ToPagedList(pageIndex, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        public ActionResult Product(FormCollection formCollection)
        {
            string[] ids = formCollection["ID"].Split(new char[] { ',' });
            foreach (string id in ids)
            {
                var product = this.db.Products.Find(int.Parse(id));
                this.db.Products.Remove(product);
                this.db.SaveChanges();
            }
            return RedirectToAction("Product");
        }
        public ActionResult Create()
        {
            var categoryList = db.Categories.ToList();
            ViewBag.CategoryList = new SelectList(categoryList, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product p)
        {
            if(ModelState.IsValid == true)
            {
               
                string filename = Path.GetFileNameWithoutExtension(p.ImageFile.FileName);
                string extension = Path.GetExtension(p.ImageFile.FileName);
                HttpPostedFileBase postedFile = p.ImageFile;
                int length = postedFile.ContentLength;
                

                string Lfilename = Path.GetFileNameWithoutExtension(p.LImageFile.FileName);
                string Lextension = Path.GetExtension(p.LImageFile.FileName);
                HttpPostedFileBase LpostedFile = p.LImageFile;
                int Llength = LpostedFile.ContentLength;
                if ((extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg") && (Lextension.ToLower() == ".jpg" || Lextension.ToLower() == ".png" || Lextension.ToLower() == ".jpeg"))
                {
                    if(length <= 2000000 && Llength <= 5000000)
                    {
                        filename = filename + extension;
                        p.ImagePath = "~/images/" + filename;
                        filename = Path.Combine(Server.MapPath("~/images/"),filename);
                        p.ImageFile.SaveAs(filename);
                        //for large image
                        Lfilename = Lfilename + Lextension;
                        p.LImagePath = "~/images/" + Lfilename;
                        Lfilename = Path.Combine(Server.MapPath("~/images/"), Lfilename);
                        p.LImageFile.SaveAs(Lfilename);

                        try
                        {
                            db.Products.Add(p);
                            int a = db.SaveChanges();
                            if (a > 0)
                            {
                                TempData["CreateMessage"] = "<script>alert('Data inserted Successfully')</script>";
                                ModelState.Clear();//to clear all txt box
                                return RedirectToAction("Product", "Home");
                            }
                            else
                            {
                                TempData["CreateMessage"] = "<script>alert('Data not inserted')</script>";
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error occured- ", ex);
                            return View("Error");
                        }
                        
                    }
                    else
                    {
                        TempData["SizeMessage"] = "<script>alert('Small Image size should be less than 2MB And Large Image size should be less than 5MB')</script>";
                    }
                }
                else
                {
                    TempData["ExtensionMessage"] = "<script>alert('Format Not Supported')</script>";
                }

            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var ProductRow = db.Products.Where(model => model.id == id).FirstOrDefault();
            //Set a session for the image and then display the image on the edit view
            var categoryList = db.Categories.ToList();
            ViewBag.CategoryList = new SelectList(categoryList, "CategoryId", "CategoryName");
            Session["LImage"] = ProductRow.LImagePath;
            Session["Image"] = ProductRow.ImagePath;
            return View(ProductRow);
        }
        [HttpPost]
        public ActionResult Edit(Product p)
        {
            if(ModelState.IsValid == true)
            {
                if(p.ImageFile != null && p.LImageFile == null)
                {
                    //if we want to edit the image
                        string filename = Path.GetFileNameWithoutExtension(p.ImageFile.FileName);
                        string extension = Path.GetExtension(p.ImageFile.FileName);
                        HttpPostedFileBase postedFile = p.ImageFile;
                        int length = postedFile.ContentLength;

                        //for large image
                        p.LImagePath = Session["LImage"].ToString();
                        if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                        {
                            if (length <= 2000000)
                            {
                                filename = filename + extension;
                                p.ImagePath = "~/images/" + filename;
                                filename = Path.Combine(Server.MapPath("~/images/"), filename);
                                p.ImageFile.SaveAs(filename);
                            try
                            {
                                db.Entry(p).State = EntityState.Modified;//modify the data which is present in p entity state
                                int a = db.SaveChanges();
                                if (a > 0)
                                {
                                    string ImagePath = Request.MapPath(Session["Image"].ToString());
                                    if (System.IO.File.Exists(ImagePath))
                                    {
                                        //IF path exists then delete it
                                        System.IO.File.Delete(ImagePath);
                                    }
                                    TempData["UpdateMessage"] = "<script>alert('Data Edited Successfully')</script>";
                                    ModelState.Clear();//to clear all txt box
                                    return RedirectToAction("Product", "Home");
                                }
                                else
                                {
                                    TempData["UpdateMessage"] = "<script>alert('Data Not Edited')</script>";
                                }
                            }
                            catch(Exception ex)
                            {
                                log.Error("Error occurred-",ex);
                                return View("Error");
                            }                               
                            }
                            else
                            {
                                TempData["SizeMessage"] = "<script>alert('Small Image size should be less than 2MB And Large Image size should be less than 5MB')</script>";
                            }
                        }
                        else
                        {
                            TempData["ExtensionMessage"] = "<script>alert('Format Not Supported')</script>";
                        }    
                }
                else if (p.ImageFile == null && p.LImageFile != null)
                {
                    //if we want to edit the image
                    p.ImagePath = Session["Image"].ToString();

                    string Lfilename = Path.GetFileNameWithoutExtension(p.LImageFile.FileName);
                    string Lextension = Path.GetExtension(p.LImageFile.FileName);
                    HttpPostedFileBase LpostedFile = p.LImageFile;
                    int Llength = LpostedFile.ContentLength;
                    if (Lextension.ToLower() == ".jpg" || Lextension.ToLower() == ".png" || Lextension.ToLower() == ".jpeg")
                    {
                        if (Llength <= 5000000)
                        {
                            

                            Lfilename = Lfilename + Lextension;
                            p.LImagePath = "~/images/" + Lfilename;
                            Lfilename = Path.Combine(Server.MapPath("~/images/"), Lfilename);
                            p.LImageFile.SaveAs(Lfilename);
                            try
                            {
                                db.Entry(p).State = EntityState.Modified;//modify the data which is present in p entity state
                                int a = db.SaveChanges();
                                if (a > 0)
                                {

                                    string LImagePath = Request.MapPath(Session["LImage"].ToString());

                                    if (System.IO.File.Exists(LImagePath))
                                    {
                                        //IF path exists then delete it
                                        System.IO.File.Delete(LImagePath);
                                    }
                                    TempData["UpdateMessage"] = "<script>alert('Data Edited Successfully')</script>";
                                    ModelState.Clear();//to clear all txt box
                                    return RedirectToAction("Product", "Home");
                                }
                                else
                                {
                                    TempData["UpdateMessage"] = "<script>alert('Data Not Edited')</script>";
                                }
                            }
                            catch(Exception ex)
                            {
                                log.Error("Error Occured-", ex);
                                return View("Error");
                            }
                            
                        }
                        else
                        {
                            TempData["SizeMessage"] = "<script>alert('Small Image size should be less than 2MB And Large Image size should be less than 5MB')</script>";
                        }
                    }
                    else
                    {
                        TempData["ExtensionMessage"] = "<script>alert('Format Not Supported')</script>";
                    }
                }
                else
                {
                    // if we don't want to edit the image we want to edit something else
                    p.ImagePath = Session["Image"].ToString();
                    p.LImagePath = Session["LImage"].ToString();
                    try
                    {
                        db.Entry(p).State = EntityState.Modified;//modify the data which is present in p entity state
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["UpdateMessage"] = "<script>alert('Data Edited Successfully')</script>";
                            ModelState.Clear();//to clear all txt box
                            return RedirectToAction("Product", "Home");
                        }
                        else
                        {
                            TempData["UpdateMessage"] = "<script>alert('Data Not Edited')</script>";
                        }
                    }
                    catch(Exception ex)
                    {
                        log.Error("Exception occured-", ex);
                        return View("Error");
                    }
                    
                }
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            if(id >0)
            {
                var ProductRow = db.Products.Where(model => model.id == id).FirstOrDefault();
                if(ProductRow != null)
                {
                    //delete the row by setting the state of row as deleted
                    try
                    {
                        db.Entry(ProductRow).State = EntityState.Deleted;
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["DeleteMessage"] = "<script>alert('Data Deleted Successfully')</script>";
                            string ImagePath = Request.MapPath(ProductRow.ImagePath.ToString());
                            if (System.IO.File.Exists(ImagePath))
                            {
                                //IF path exists then delete it
                                System.IO.File.Delete(ImagePath);
                            }
                            string LImagePath = Request.MapPath(ProductRow.LImagePath.ToString());
                            if (System.IO.File.Exists(LImagePath))
                            {
                                //IF path exists then delete it
                                System.IO.File.Delete(LImagePath);
                            }
                        }
                        else
                        {
                            TempData["DeleteMessage"] = "<script>alert('Data Not Deleted')</script>";
                        }
                    }
                    catch(Exception ex)
                    {
                        log.Error("Error Occurred-", ex);
                        return View("Error");
                    }
                    
                }
            }
            return RedirectToAction("Product", "Home");
        }
    }
}