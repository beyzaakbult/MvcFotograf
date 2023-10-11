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
    public class AdminHakkımdaController : Controller
    {
        // GET: AdminHakkımda
        MvcFotografEntities4 db = new MvcFotografEntities4();
        public ActionResult Index()
        {
            var bl = db.TBLHAKKIMIZDA.FirstOrDefault();
            return View("Index", bl);
        }
        [HttpPost]
        public ActionResult Index(TBLHAKKIMIZDA hak, HttpPostedFileBase GELENRESIM)
        {
            var guncellenecekhak = db.TBLHAKKIMIZDA.Find(hak.ID);
            if (GELENRESIM != null && GELENRESIM.ContentLength > 0)
            {

                if (GELENRESIM.ContentType == "image/jpeg" || GELENRESIM.ContentType == "image/png" || GELENRESIM.ContentType == "image/jpg" || GELENRESIM.ContentType == "image/jfif")
                {
                    var yol = Request.MapPath("~/Resimler/" + guncellenecekhak.RESIM);
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
                    guncellenecekhak.RESIM = fileName;
                }
                else
                {
                    TempData["hata"] = "Lütfem resim formatında bir dosya seçiniz!!!";
                    return View();
                }
            }
            guncellenecekhak.BASLIK = hak.BASLIK;
            guncellenecekhak.ICERIK = hak.ICERIK;
            db.SaveChanges();
            TempData["guncelle"] = "başarılı";
            return RedirectToAction("Index");
        }
    }
}