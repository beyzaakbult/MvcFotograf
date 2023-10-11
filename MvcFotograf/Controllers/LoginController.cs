using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcFotograf.Models;

namespace MvcFotograf.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login,
        MvcFotografEntities4 db = new MvcFotografEntities4();
        public ActionResult Index()
        {   
            return View();
        }
        [HttpPost]
        public ActionResult Index(TBLADMIN ad)
        {
            var bilgiler = db.TBLADMIN.Where(x => x.KULLANICIADI == ad.KULLANICIADI && x.SIFRE == ad.SIFRE).FirstOrDefault();
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KULLANICIADI, false);
                Session["KULLANICI"] = bilgiler.KULLANICIADI.ToString();
                Session["yetki"] = bilgiler.YETKİ;
                Session["ADSOYAD"] = bilgiler.ADSOYAD;
                return RedirectToAction("Index", "AdminAnaSayfa");
            }
            else
            {
                return View();
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "AnaSayfa");
        }
    }
}