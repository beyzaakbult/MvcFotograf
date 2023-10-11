using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFotograf.Models;

namespace MvcFotograf.Controllers
{
    public class AnaSayfaController : Controller
    {
        MvcFotografEntities4 db = new MvcFotografEntities4();
        Class1 cs = new Class1();
        public ActionResult Index()
        {
            cs.sliderlistesi = db.TBLSLIDER.ToList();
            cs.kategorılistesi = db.TBLKATEGORI.ToList();
            cs.ssslistesi = db.TBLSSS.ToList();
            cs.yorumlarlistesi=db.TBLYORUMLAR.ToList();
          
            return View(cs);
        }
    }
}