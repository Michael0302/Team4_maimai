using MaiMai.Models;
using MaiMai.Models.MaimaiIndexViewModel;
using MaiMai.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaiMai.Controllers
{
    public class NewMaimaiIndexController : Controller
    {

        maimaiEntities db = new maimaiEntities();
        // GET: NewMaimaiIndex
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MaimaiIndexNew() 
        {
            return View();
        }
        //五星廣告
        public ActionResult StarFive()
        {
            var StarFiveList = db.Member.Where(m => m.totalStarRate == 5.0).ToList();
            return Json(StarFiveList, JsonRequestBehavior.AllowGet);
        }
        //五星廣告

        public ActionResult addCarousel()
        {
            var PostList = db.ProductPost.Select(m => new MaimaiIndexViewModel()
            {
                productImg = m.productImg,
                price = m.price,
                productDescription = m.productDescription
            }).ToList();
            return Json(PostList, JsonRequestBehavior.AllowGet);
        }
    }
}