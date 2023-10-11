using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFotograf.Models;

namespace MvcFotograf.Controllers
{
    public class HakkımdaController : Controller
    {
        MvcFotografEntities4 db = new MvcFotografEntities4();
        Class1 cs = new Class1();
        // GET: Hakkımda
        public ActionResult Index()
        {
            cs.hakkımdayazısı = db.TBLHAKKIMIZDA.FirstOrDefault();
            cs.yorumlarlistesi = db.TBLYORUMLAR.ToList();

            return View(cs);
        }
   
    }
}