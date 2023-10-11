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
    public class AdminSliderController : Controller
    {
        // GET: AdminSlider
        MvcFotografEntities4 db = new MvcFotografEntities4();
        public ActionResult Index()
        {
            var degerler = db.TBLSLIDER.ToList();

            return View(degerler);
        }
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Ekle(TBLSLIDER s, HttpPostedFileBase GELENRESIM)
        {
            if (GELENRESIM != null && GELENRESIM.ContentLength > 0)
            {
                if (GELENRESIM.ContentType == "image/jpeg" || GELENRESIM.ContentType == "image/png" || GELENRESIM.ContentType == "image/jpg" || GELENRESIM.ContentType == "image/jfif")
                {
                    var fi = new FileInfo(GELENRESIM.FileName);
                    var fileName = Path.GetFileName(GELENRESIM.FileName);
                    fileName = Guid.NewGuid().ToString() + fi.Extension;
                    var path = Path.Combine(Server.MapPath("~/SlıderResimler/"), fileName);


                    WebImage rr = new WebImage(GELENRESIM.InputStream);

                    if (rr.Width > 1000)

                        rr.Resize(800, 800);
                    rr.Save(path);

                    TBLSLIDER slıder = new TBLSLIDER();
                   slıder.BASLIK = s.BASLIK;
                   slıder.ACIKLAMA = s.ACIKLAMA;
                   slıder.RESIM = fileName;
                    db.TBLSLIDER.Add(slıder);
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
            var silinecekslıder = db.TBLSLIDER.Find(id);
            var yol = Request.MapPath("~/SlıderResimler/" + silinecekslıder.RESIM);
            if (System.IO.File.Exists(yol))
            {
                System.IO.File.Delete(yol);
            }


            db.TBLSLIDER.Remove(silinecekslıder);
            db.SaveChanges();
            TempData["sil"] = "asdf";

            return RedirectToAction("Index");
        }
        public ActionResult Guncelle(int id)
        {
            var guncellenecek = db.TBLSLIDER.Find(id);

            return View("Guncelle", guncellenecek);
        }
        [HttpPost]
        public ActionResult Guncelle(TBLSLIDER slıder, HttpPostedFileBase GELENRESIM)
        {
            var guncellenecekkayit = db.TBLSLIDER.Find(slıder.ID);

            if (GELENRESIM != null && GELENRESIM.ContentLength > 0)
            {
                if (GELENRESIM.ContentType == "image/jpeg" || GELENRESIM.ContentType == "image/png" || GELENRESIM.ContentType == "image/jpg" || GELENRESIM.ContentType == "image/jfif")
                {
                    var yol = Request.MapPath("~/SlıderResimler/" + guncellenecekkayit.RESIM);
                    if (System.IO.File.Exists(yol))
                    {
                        System.IO.File.Delete(yol);
                    }


                    var fi = new FileInfo(GELENRESIM.FileName);
                    var fileName = Path.GetFileName(GELENRESIM.FileName);
                    fileName = Guid.NewGuid().ToString() + fi.Extension;
                    var path = Path.Combine(Server.MapPath("~/SlıderResimler/"), fileName);


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
            guncellenecekkayit.BASLIK = slıder.BASLIK;
            guncellenecekkayit.ACIKLAMA = slıder.ACIKLAMA;
            db.SaveChanges();
            TempData["guncelle"] = "başarılı";
            return RedirectToAction("Index");
        }
    }
}