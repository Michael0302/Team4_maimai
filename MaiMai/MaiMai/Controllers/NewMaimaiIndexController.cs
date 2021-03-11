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
        //首頁
        public ActionResult MaimaiIndexNew() 
        {
            return View();
        }
        //首頁
        //五星廣告大版
        public ActionResult StarFive()
        {
            var StarFiveList = db.Member.Where(m => m.totalStarRate == 5.0 ).Select(m => new {
                img = m.profileImg,
                userAccount = m.userAccount
            }).ToList();
            return Json(StarFiveList, JsonRequestBehavior.AllowGet);
        }
        //五星廣告大版
        //增加輪播圖片
        public ActionResult addCarousel()
        {
            var addCarouselList = db.ProductPost.Select(m => new MaimaiIndexViewModel()
            {
                productImg = m.productImg,
                price = m.price,
                productDescription = m.productDescription
            }).ToList();
            return Json(addCarouselList, JsonRequestBehavior.AllowGet);
        }
        //增加輪播圖片
        //增加輪播新商品
        public ActionResult NewaddCarousel()
        {
            var addCarouselList = db.ProductPost.Select(m => new MaimaiIndexViewModel()
            {
                ProductPostID = m.ProductPostID,
                productImg = m.productImg,
                price = m.price,
                productDescription = m.productDescription
            }).ToList();
            return Json(addCarouselList, JsonRequestBehavior.AllowGet);
        }
        //增加輪播新商品
        //商品頁面
        public ActionResult ProdutPost() {

            return View();
        }
        public ActionResult ProdutPostDetail(int PostID)
        {
            var ProdutPostDetailList = db.ProductPost.Where(m => m.ProductPostID == PostID).Select(m=>new{
                img=m.productImg,
                ProductName=m.productName,
                price=m.price,
                Description=m.productDescription,
                QTY=m.inStoreQTY,
            }).ToList();
            return Json(ProdutPostDetailList, JsonRequestBehavior.AllowGet);
        }
        //商品頁面



    }
}