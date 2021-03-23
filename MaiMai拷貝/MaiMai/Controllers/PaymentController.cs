using MaiMai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaiMai.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }
        maimaiRepository<Member> mb = new maimaiRepository<Member>();
        maimaiRepository<Order> od = new maimaiRepository<Order>();
        maimaiEntities db = new maimaiEntities();
        //public ActionResult paytoO(int orderID) {





        //    return Json(table, JsonRequestBehavior.AllowGet);
        //}
    }
}