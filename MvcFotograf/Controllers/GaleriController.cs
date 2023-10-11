using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFotograf.Models;

namespace MvcFotograf.Controllers
{
    public class GaleriController : Controller
    {
        MvcFotografEntities4 db = new MvcFotografEntities4();
        
        // GET: Galeri
        public ActionResult Index()
        {
            var degerler = db.TBLGALERI.ToList();

            return View(degerler);
        }
    }
}