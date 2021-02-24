using MaiMai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaiMai.Controllers
{
    public class backstageController : Controller
    {
        // GET: backstage
        public ActionResult Index()
        {
            return View();
        }

        maimaiRepository<Member> mb = new maimaiRepository<Member>();
        public ActionResult backstageIndex()
        {
            return View();
        }

        public ActionResult getMemberList_P()
        {
            var memList = mb.GetAll();
            return Json(memList, JsonRequestBehavior.AllowGet);
        }
    }
}