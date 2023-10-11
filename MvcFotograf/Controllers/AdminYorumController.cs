using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFotograf.Models;
using PagedList;
using PagedList.Mvc;

namespace MvcFotograf.Controllers
{
    public class AdminYorumController : Controller
    {
        // GET: AdminYorum
        MvcFotografEntities2 db = new MvcFotografEntities2();
        public ActionResult Index(int sayfa = 1)
        {
            var degerler = db.TBLYORUMLAR.ToList().ToPagedList(sayfa, 2);
            return View(degerler);
        }

        public ActionResult Gizle(int id)
        {
            var yorum = db.TBLYORUMLAR.Find(id);
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
            var silinecekyorum = db.TBLYORUMLAR.Find(id);

            db.TBLYORUMLAR.Remove(silinecekyorum);
            db.SaveChanges();
            TempData["sil"] = "asdf";

            return RedirectToAction("Index");
        }
    }
}