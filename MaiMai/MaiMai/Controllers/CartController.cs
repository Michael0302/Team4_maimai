using MaiMai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaiMai.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart

        maimaiEntities db = new maimaiEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult cartIndex()
        {
            return View();
        }

        public ActionResult getCart_P()
        {
            if (Request.Cookies["LoginID"] == null)
            {
                
                return Content("fail");
            }

            var UserID = Convert.ToInt32(Request.Cookies["LoginID"].Value);
            var products = db.Cart.Where(c => c.UserID == UserID).Join(db.ProductPost, x => x.ProductPostID, y => y.ProductPostID, (x, y) => new
            {
                x.CartID,
                x.CartNumber,
                x.UserID,
                x.QTY,
                y.ProductPostID,
                y.productName,
                y.productImg,
                y.price
            }).GroupBy(g => new { g.CartID, g.QTY, g.CartNumber, g.UserID }).Select(s => new
            {
                CartID = s.Key.CartID,
                CartNumber = s.Key.CartNumber,
                UserID = s.Key.UserID,
                QTY = s.Key.QTY,
                ProductPostID = s.Select(i => i.ProductPostID),
                productName = s.Select(i => i.productName),
                productImg = s.Select(i => i.productImg),
                price = s.Select(i => i.price),
            }).ToList();          

            return Json(products, JsonRequestBehavior.AllowGet);
        }

        maimaiRepository<Cart> dbCart = new maimaiRepository<Cart>();
       
        public ActionResult delProduct(int? productID)
        {
            if (productID ==null)
            {
                return Content("false");
            }
            dbCart.Delete((int)productID);

            return Content("true");
        }
    }
}