using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFotograf.Models;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace MvcFotograf.Controllers
{
    public class IletisimController : Controller
    {
        MvcFotografEntities4 db = new MvcFotografEntities4();
      
        public ActionResult Index()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Index(Contact model)
        {
            if (ModelState.IsValid)
            {
                var body = new StringBuilder();
                body.AppendLine("Ad & Soyad: " + model.Name);
                body.AppendLine("E-Mail Adresi: " + model.Email);
                body.AppendLine("Konu: " + model.Subject);
                body.AppendLine("Mesaj: " + model.Message);
                Mail.MailSender(body.ToString());
                ViewBag.Success = true;
            }
            return View();
            
        }
    }
}