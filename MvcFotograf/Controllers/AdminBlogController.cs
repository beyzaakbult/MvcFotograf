using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MvcFotograf.Models;

namespace MvcFotograf.Controllers
{

    [Authorize]
    public class AdminBlogController : Controller
    {
        // GET: AdminBlog
        MvcFotografEntities4 db = new MvcFotografEntities4();   
        public ActionResult Index()
        {
            var degerler = db.TBLBLOG.ToList();

            return View(degerler);
        }
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Ekle(TBLBLOG b, HttpPostedFileBase GELENRESIM)
        {
            if (GELENRESIM != null && GELENRESIM.ContentLength > 0)
            {
                if (GELENRESIM.ContentType == "image/jpeg" || GELENRESIM.ContentType == "image/png" || GELENRESIM.ContentType == "image/jpg" || GELENRESIM.ContentType == "image/jfif")
                {
                    var fi = new FileInfo(GELENRESIM.FileName);
                    var fileName = Path.GetFileName(GELENRESIM.FileName);
                    fileName = Guid.NewGuid().ToString() + fi.Extension;
                    var path = Path.Combine(Server.MapPath("~/BlogResim/"), fileName);


                    WebImage rr = new WebImage(GELENRESIM.InputStream);

                    if (rr.Width > 1000)

                        rr.Resize(800, 800);
                    rr.Save(path);

                    TBLBLOG blog = new TBLBLOG();
                   blog.BASLIK = b.BASLIK;
                   blog.ICERIK = b.ICERIK;
                   blog.RESIM = fileName;
                    db.TBLBLOG.Add(blog);
                    db.SaveChanges();
                    TempData["ekle"] = "başarılı";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["hata"] = "a";
                    return View();
                }
            }

            return View();
        }

        public ActionResult Sil(int id)
        {
            var silinecekblog = db.TBLBLOG.Find(id);
            var yol = Request.MapPath("~/BlogResim/" + silinecekblog.RESIM);
            if (System.IO.File.Exists(yol))
            {
                System.IO.File.Delete(yol);
            }


            db.TBLBLOG.Remove(silinecekblog);
            db.SaveChanges();
            TempData["sil"] = "asdf";

            return RedirectToAction("Index");
        }
        public ActionResult Guncelle(int id)
        {
            var guncellenecek = db.TBLBLOG.Find(id);

            return View("Guncelle", guncellenecek);
        }
        [HttpPost]
        public ActionResult Guncelle(TBLBLOG blg, HttpPostedFileBase GELENRESIM)
        {
            var guncellenecekkayit = db.TBLBLOG.Find(blg.ID);

            if (GELENRESIM != null && GELENRESIM.ContentLength > 0)
            {
                if (GELENRESIM.ContentType == "image/jpeg" || GELENRESIM.ContentType == "image/png" || GELENRESIM.ContentType == "image/jpg" || GELENRESIM.ContentType == "image/jfif")
                {
                    var yol = Request.MapPath("~/BlogResim/" + guncellenecekkayit.RESIM);
                    if (System.IO.File.Exists(yol))
                    {
                        System.IO.File.Delete(yol);
                    }


                    var fi = new FileInfo(GELENRESIM.FileName);
                    var fileName = Path.GetFileName(GELENRESIM.FileName);
                    fileName = Guid.NewGuid().ToString() + fi.Extension;
                    var path = Path.Combine(Server.MapPath("~/BlogResim/"), fileName);


                    WebImage rr = new WebImage(GELENRESIM.InputStream);

                    if (rr.Width > 1000)

                        rr.Resize(800, 800);
                    rr.Save(path);
                    guncellenecekkayit.RESIM = fileName;
                }
                else
                {
                    TempData["hata"] = "Lütfem resim formatında bir dosya seçiniz!!!";
                    return View();
                }
            }
            guncellenecekkayit.BASLIK =blg.BASLIK;
            guncellenecekkayit.ICERIK =blg.ICERIK;
            db.SaveChanges();
            TempData["guncelle"] = "başarılı";
            return RedirectToAction("Index");
        }

    }
}