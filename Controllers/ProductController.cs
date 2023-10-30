using SwiftSnack.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwiftSnack.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DBSportStoreEntities database = new DBSportStoreEntities();
        public ActionResult Index()
        {
            return View(database.Products.ToList());
        }
        public ActionResult Create()
        {
            Product pro = new Product();
            return View(pro);
        }
        public ActionResult SelectCate()
        {
            Category se_cate = new Category();
            se_cate.ListCate = database.Categories.ToList<Category>();
            return PartialView(se_cate);
        }
        public ActionResult Details(int id)
        {
            Product pro = database.Products.Find(id);
            return View(pro);
        }

        [NotMapped]
        public HttpPostedFileBase UploadImage { get; set; }

        [HttpPost]
        public ActionResult Create(Product pro)
        {
            try
            {
                if (pro.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(pro.UploadImage.FileName);
                    string extent = Path.GetExtension(pro.UploadImage.FileName);
                    filename = filename + extent;
                    pro.ImagePro = "~/Content/Img/" + filename;
                    pro.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/Img/"), filename));
                }
                database.Products.Add(pro);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}