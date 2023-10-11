using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFotograf.Models;

namespace MvcFotograf.Controllers
{

    [Authorize]
    public class AdminBlogYorumController : Controller
    {
        MvcFotografEntities4 db = new MvcFotografEntities4();
        public ActionResult Index()
        {
            var degerler = db.TBLBLOGYORUM.ToList();
            return View(degerler);
        }

        public ActionResult Gizle(int id)
        {
            var yorum = db.TBLBLOGYORUM.Find(id);
            if (yorum.DURUM == false)
            {
                yorum.DURUM = true;
            }
            else
            {
                yorum.DURUM = false;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var silinecekyorum = db.TBLBLOGYORUM.Find(id);

            db.TBLBLOGYORUM.Remove(silinecekyorum);
            db.SaveChanges();
            TempData["sil"] = "asdf";

            return RedirectToAction("Index");
        }
    }
}