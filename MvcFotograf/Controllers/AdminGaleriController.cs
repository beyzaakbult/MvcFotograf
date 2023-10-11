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
    public class AdminGaleriController : Controller
    {
        // GET: AdminGaleri
        MvcFotografEntities4 db =new MvcFotografEntities4();

        public ActionResult Index()
        {
            var degerler = db.TBLGALERI.ToList();

            return View(degerler);
        }
        public ActionResult GaleriEkle()
        {
            return View();
        }

        [HttpPost]

        public ActionResult GaleriEkle(TBLGALERI g, HttpPostedFileBase GELENRESIM)
        {
            if (GELENRESIM != null && GELENRESIM.ContentLength > 0)
            {
                if (GELENRESIM.ContentType == "image/jpeg" || GELENRESIM.ContentType == "image/png" || GELENRESIM.ContentType == "image/jpg" || GELENRESIM.ContentType == "image/jfif")
                {
                    var fi = new FileInfo(GELENRESIM.FileName);
                    var fileName = Path.GetFileName(GELENRESIM.FileName);
                    fileName = Guid.NewGuid().ToString() + fi.Extension;
                    var path = Path.Combine(Server.MapPath("~/GaleriResim/"), fileName);


                    WebImage rr = new WebImage(GELENRESIM.InputStream);

                    if (rr.Width > 1000)

                        rr.Resize(800, 800);
                    rr.Save(path);

                    TBLGALERI galeri = new TBLGALERI();
                    galeri.RESIM = fileName;
                    db.TBLGALERI.Add(galeri);
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
            var silinecekgaleri = db.TBLGALERI.Find(id);
            var yol = Request.MapPath("~/GaleriResim/" + silinecekgaleri.RESIM);
            if (System.IO.File.Exists(yol))
            {
                System.IO.File.Delete(yol);
            }


            db.TBLGALERI.Remove(silinecekgaleri);
            db.SaveChanges();
            TempData["sil"] = "asdf";

            return RedirectToAction("Index");
        } 

        public ActionResult Guncelle()
        {
            var galeri = db.TBLGALERI.FirstOrDefault();
            return View("Guncelle", galeri);
           
        }
        [HttpPost]
        public ActionResult Guncelle(TBLGALERI gal, HttpPostedFileBase GELENRESIM)
        {
            var guncellenecekgaleri = db.TBLGALERI.Find(gal.ID);
            if (GELENRESIM != null && GELENRESIM.ContentLength > 0)
            {

                if (GELENRESIM.ContentType == "image/jpeg" || GELENRESIM.ContentType == "image/png" || GELENRESIM.ContentType == "image/jpg" || GELENRESIM.ContentType == "image/jfif")
                {
                    var yol = Request.MapPath("~/GaleriResim/" + guncellenecekgaleri.RESIM);
                    if (System.IO.File.Exists(yol))
                    {
                        System.IO.File.Delete(yol);
                    }

                    var fi = new FileInfo(GELENRESIM.FileName);
                    var fileName = Path.GetFileName(GELENRESIM.FileName);
                    fileName = Guid.NewGuid().ToString() + fi.Extension;
                    var path = Path.Combine(Server.MapPath("~/GaleriResim/"), fileName);

                    WebImage rr = new WebImage(GELENRESIM.InputStream);

                    if (rr.Width > 1000)

                        rr.Resize(800, 800);

                    rr.Save(path);
                    guncellenecekgaleri.RESIM = fileName;
                }
                else
                {
                    TempData["hata"] = "Lütfem resim formatında bir dosya seçiniz!!!";
                    return View();
                }
            }
            db.SaveChanges();
            TempData["guncelle"] = "başarılı";
            return RedirectToAction("Index");
        }
    }
}