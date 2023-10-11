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
    public class AdminPersonelController : Controller
    {
        // GET: AdminPersonel
        MvcFotografEntities4 db = new MvcFotografEntities4();
        public ActionResult Index()
        {
            var degerler = db.TBLPERSONEL.ToList();

            return View(degerler);
        }
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Ekle(TBLPERSONEL p, HttpPostedFileBase GELENRESIM)
        {
            if (GELENRESIM != null && GELENRESIM.ContentLength > 0)
            {
                if (GELENRESIM.ContentType == "image/jpeg" || GELENRESIM.ContentType == "image/png" || GELENRESIM.ContentType == "image/jpg" || GELENRESIM.ContentType == "image/jfif")
                {
                    var fi = new FileInfo(GELENRESIM.FileName);   
                    var fileName = Path.GetFileName(GELENRESIM.FileName); 
                    fileName = Guid.NewGuid().ToString() + fi.Extension;  
                    var path = Path.Combine(Server.MapPath("~/Resimler/"), fileName);

                   
                    WebImage rr = new WebImage(GELENRESIM.InputStream);

                    if (rr.Width > 1000)

                        rr.Resize(800, 800);
                    rr.Save(path);

                    TBLPERSONEL pers = new TBLPERSONEL();
                    pers.ADSOYAD = p.ADSOYAD;
                    pers.INSTAGRAM = p.INSTAGRAM;
                    pers.MESLEK = p.MESLEK;
                    pers.RESIM = fileName;
                    db.TBLPERSONEL.Add(pers);
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
            var silinecekpers = db.TBLPERSONEL.Find(id);
            var yol = Request.MapPath("~/Resimler/" + silinecekpers.RESIM);
            if (System.IO.File.Exists(yol))
            {
                System.IO.File.Delete(yol);
            }

        
            db.TBLPERSONEL.Remove(silinecekpers);
            db.SaveChanges();
            TempData["sil"] = "asdf";

            return RedirectToAction("Index");
        }
        public ActionResult Guncelle(int id)
        {
            var guncellenecek = db.TBLPERSONEL.Find(id);

            return View("Guncelle", guncellenecek);
        }
        [HttpPost]
        public ActionResult Guncelle(TBLPERSONEL pers, HttpPostedFileBase GELENRESIM)
        {
            var guncellenecekkayit = db.TBLPERSONEL.Find(pers.ID);

            if (GELENRESIM != null && GELENRESIM.ContentLength > 0)
            {
                if (GELENRESIM.ContentType == "image/jpeg" || GELENRESIM.ContentType == "image/png" || GELENRESIM.ContentType == "image/jpg" || GELENRESIM.ContentType == "image/jfif")
                {
                    var yol = Request.MapPath("~/Resimler/" + guncellenecekkayit.RESIM);
                    if (System.IO.File.Exists(yol))
                    {
                        System.IO.File.Delete(yol);
                    }
                   

                    var fi = new FileInfo(GELENRESIM.FileName);   
                    var fileName = Path.GetFileName(GELENRESIM.FileName); 
                    fileName = Guid.NewGuid().ToString() + fi.Extension;  
                    var path = Path.Combine(Server.MapPath("~/Resimler/"), fileName);

                  
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
            guncellenecekkayit.ADSOYAD = pers.ADSOYAD;
            guncellenecekkayit.INSTAGRAM = pers.INSTAGRAM;
            guncellenecekkayit.MESLEK = pers.MESLEK;
            db.SaveChanges();
            TempData["guncelle"] = "başarılı";
            return RedirectToAction("Index");
        }
    }
}