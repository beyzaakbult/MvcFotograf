using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFotograf.Models;

namespace MvcFotograf.Controllers
{
    public class BlogController : Controller
    {
        MvcFotografEntities4 db = new MvcFotografEntities4();
        Class1 by = new Class1();
        // GET: Blog
        public ActionResult Index()
        {
            var degerler = db.TBLBLOG.ToList();
            return View(degerler);
        }
        public ActionResult BlogDetay(int id)
        {
            var muzeler = db.TBLBLOG.Where(x => x.ID == id).ToList();
            by.bloglistesi = db.TBLBLOG.Where(x => x.ID == id).ToList();
           
            return View(by);
        }
        public PartialViewResult PartialBlogYorum(int id)
        {
            var degerler = db.TBLBLOGYORUM.Where(x => x.BLOGID == id && x.DURUM==true).ToList();
            ViewBag.blgid = id;

            return PartialView(degerler);
        }
        public PartialViewResult PartialBlogYorumYaz(int id)
        {
            ViewBag.pid = id;

            return PartialView();
        }

        [HttpPost]
        public PartialViewResult PartialBlogYorumYaz(TBLBLOGYORUM b)
        {
            b.DURUM = false;
            b.TARIH = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            db.TBLBLOGYORUM.Add(b);
            Response.Redirect("/Blog/BlogDetay/" + b.BLOGID);
            db.SaveChanges();
            return PartialView();
        }
        
        public ActionResult PartialYorumKaydet(TBLBLOGYORUM b)
        {
            db.TBLBLOGYORUM.Add(b);
            b.TARIH=Convert.ToDateTime(DateTime.Now.ToShortDateString());
            b.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("BlogDetay/" + b.BLOGID);
        }
    }
}