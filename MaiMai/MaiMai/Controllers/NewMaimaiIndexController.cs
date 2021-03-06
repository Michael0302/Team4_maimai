using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaiMai.Controllers
{
    public class NewMaimaiIndexController : Controller
    {
        // GET: NewMaimaiIndex
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MaimaiIndexNew() 
        {
            return View();
        }
    }
}