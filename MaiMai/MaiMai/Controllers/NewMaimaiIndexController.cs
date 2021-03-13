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
        maimaiRepository<Cart> CartRepository = new maimaiRepository<Cart>();
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
                ProductPostID = m.ProductPostID,
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
                UserID=m.UserID,
                ProductPostID=m.ProductPostID,
            }).ToList();
            return Json(ProdutPostDetailList, JsonRequestBehavior.AllowGet);
        }
        //商品頁面
        //送去購物車
        public ActionResult goCar() 
        {
            return View();
        }

        public string goCar1(Cart ProductPostIDlll)
        {
            if (Request.Cookies["LoginID"] == null)
            {       
                return ("請先登入");
            }
            var UserID = Convert.ToInt32(Request.Cookies["LoginID"].Value);
            var pdpost = db.ProductPost.FirstOrDefault(m => m.ProductPostID == ProductPostIDlll.ProductPostID);
            var carnumver = db.Cart.FirstOrDefault(m => m.CartNumber != null && m.UserID == UserID && m.Status == false);
            if (carnumver != null)
            {
                
                carnumver.QTY += ProductPostIDlll.QTY;
                carnumver.CartNumber = ProductPostIDlll.CartNumber;
                carnumver.ProductPostID = pdpost.ProductPostID;
                carnumver.Status = false;

                carnumver.UserID = UserID;

                pdpost.inStoreQTY = pdpost.inStoreQTY - carnumver.QTY;
                db.SaveChanges();
                return "新增成功";
            }
            else
            {
                ProductPostIDlll.CartNumber = (new Random().Next(0, int.MaxValue)+(db.Cart.FirstOrDefault().UserID.ToString()));
                ProductPostIDlll.Status = false;
                ProductPostIDlll.UserID = UserID;
                CartRepository.Create(ProductPostIDlll);
                return "新增成功";
            }

            //return "123";
        }
        //送去購物車

    }
}