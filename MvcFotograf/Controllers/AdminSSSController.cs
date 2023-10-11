using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFotograf.Models;

namespace MvcFotograf.Controllers
{

    [Authorize]
    public class AdminSSSController : Controller
    {
        // GET: AdminSSS
        MvcFotografEntities4 db = new MvcFotografEntities4();
        public ActionResult Index()
        {
            var degerler = db.TBLSSS.ToList();
            return View(degerler);
        }
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Ekle(TBLSSS p, HttpPostedFileBase GELENRESIM)
        {
            TBLSSS pers = new TBLSSS();
            pers.SORU = p.SORU;
            pers.CEVAP = p.CEVAP;
            db.TBLSSS.Add(pers);
            db.SaveChanges();
            TempData["ekle"] = "başarılı";
            return RedirectToAction("Index");

        }
        public ActionResult Sil(int id)
        {
            var silinecekyorum = db.TBLSSS.Find(id);

            db.TBLSSS.Remove(silinecekyorum);
            db.SaveChanges();
            TempData["sil"] = "asdf";

            return RedirectToAction("Index");
        }
        public ActionResult Guncelle(int id)
        {
            var guncellenecek = db.TBLSSS.Find(id);

            return View("Guncelle", guncellenecek);
        }
        [HttpPost]
        public ActionResult Guncelle(TBLSSS slıder, HttpPostedFileBase GELENRESIM)
        {
            var guncellenecekkayit = db.TBLSSS.Find(slıder.ID);
            guncellenecekkayit.SORU = slıder.SORU;
            guncellenecekkayit.CEVAP = slıder.CEVAP;

            db.SaveChanges();
            TempData["guncelle"] = "başarılı";
            return RedirectToAction("Index");
        }
    }
}