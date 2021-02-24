using DocumentFormat.OpenXml.Bibliography;
using MaiMai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaiMai.Controllers
{
    public class RequiredPostController : Controller
    {
        // GET: RequiredPost

        maimaiRepository<RequiredPost> requiredPostTable = new maimaiRepository<RequiredPost>();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult requiredPostIndex() {

            return View();
        }
        [HttpPost]
        public JsonResult allrequiredPost() {
            maimaiEntities db = new maimaiEntities();

            var table = db.RequiredPost;

            //var table = requiredPostTable.GetAll();
          
            return Json(table, JsonRequestBehavior.AllowGet);
        }
    }
}