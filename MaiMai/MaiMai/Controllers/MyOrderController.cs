
using MaiMai.Models;
using MaiMai.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaiMai.Controllers
{

    public class MyOrderController : Controller
    {
        // GET: backstage
        public ActionResult Index()
        {
            return View();
        }

        maimaiRepository<Member> mb = new maimaiRepository<Member>();
        maimaiRepository<Order> od = new maimaiRepository<Order>();
        maimaiEntities db = new maimaiEntities();
        public ActionResult myOrder()
        {
            return View();
        }

        public ActionResult getMemberList_P()
        {

            var memList = db.Member.Select(m => new MemberViewModel()
            {
                UserID = m.UserID,
                userAccount = m.userAccount,
                userPassWord = m.userPassWord,
                city = m.city,
                address = m.address,
                phoneNumber = m.phoneNumber,
                firstName = m.firstName,
                lastName = m.lastName,
                Name = m.lastName + m.firstName,
                birthday = m.birthday,
                identityNumber = m.identityNumber,
                profileImg = m.profileImg,
                userLevel = m.userLevel,
                totalStarRate = m.totalStarRate,
                selfDescription = m.selfDescription,
                email = m.email,
                userLevelString = m.userLevel.ToString()
            });

            return Json(memList, JsonRequestBehavior.AllowGet);

        }

        public ActionResult getAdminList_P(int userLevel)
        {

            var memList = db.Member.Where(m => m.userLevel == userLevel).Select(m => new MemberViewModel()
            {
                UserID = m.UserID,
                userAccount = m.userAccount,
                userPassWord = m.userPassWord,
                city = m.city,
                address = m.address,
                phoneNumber = m.phoneNumber,
                firstName = m.firstName,
                lastName = m.lastName,
                Name = m.lastName + m.firstName,
                birthday = m.birthday,
                identityNumber = m.identityNumber,
                profileImg = m.profileImg,
                userLevel = m.userLevel,
                totalStarRate = m.totalStarRate,
                selfDescription = m.selfDescription,
                email = m.email,
                userLevelString = m.userLevel.ToString()
            }).ToList();

            return Json(memList, JsonRequestBehavior.AllowGet);

        }

        public ActionResult getMember_P(int? id)
        {
            if (id == null)
            {
                return Content("錯誤");
            }
            var mem = db.Member.Where(m => m.UserID == id).Select(m => new MemberViewModel()
            {
                UserID = m.UserID,
                userAccount = m.userAccount,
                userPassWord = m.userPassWord,
                city = m.city,
                address = m.address,
                phoneNumber = m.phoneNumber,
                Name = m.lastName + m.firstName,
                birthday = m.birthday,
                identityNumber = m.identityNumber,
                profileImg = m.profileImg,
                userLevel = m.userLevel,
                totalStarRate = m.totalStarRate,
                selfDescription = m.selfDescription,
                email = m.email,
                firstName = m.firstName,
                lastName = m.lastName,
                userLevelString = m.userLevel.ToString()
            }).ToList();

            return Json(mem, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult editUserLevel_P(Member m)
        {
            mb.Update(m);
            return Json(m, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getOrderList_P(int? status)  //key group by
        {

            var id = Convert.ToInt32(Request.Cookies["LoginID"].Value); 
            //Response.Cookies["LoginID"]設定者給的初始值  rememberme=="on" checkbox attr
            //<a href="SignUp"> controller action
            if (status != null)
            {
                if (status >= 2)
                {
                    var ordercmplist = db.Order.Where(m => (m.orderStatus >= 2) && (m.buyerUserID == id)).Join(db.OrderDetail, x => x.OrderId, y => y.OrderID, (x, y) => new
                    {
                        x.OrderId,
                        x.orderStatus,
                        x.createdTime,
                        x.buyerUserID,
                        x.Member.firstName,
                        //y.SellerID,
                        y.oneProductTotalPrice,

                    }).GroupBy(g => new { g.OrderId, g.orderStatus, g.createdTime, g.buyerUserID, g.firstName }).Select(s => new
                    {
                        OrderId = s.Key.OrderId,
                        orderStatus = s.Key.orderStatus,
                        createdTime = s.Key.createdTime,
                        buyerUserID = s.Key.buyerUserID,
                        buyerName = s.Key.firstName,
                        //SellerID =s.Select(i => i.SellerID),
                        price = s.Select(i => i.oneProductTotalPrice).Sum(),
                    }) ;
                    return Json(ordercmplist, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var ordercmplist = db.Order.Where(m => (m.orderStatus < 2)&&(m.buyerUserID==id)).Join(db.OrderDetail, x => x.OrderId, y => y.OrderID, 
                        /*db.Order, x => x.OrderId -----db.OrderDetail,y=>y.OrderID兩張表單串聯*/
                        (x, y) => new{
                        x.OrderId,
                        x.orderStatus,
                        x.createdTime,
                        x.buyerUserID,
                        x.Member.firstName,
                        //y.SellerID,
                        y.oneProductTotalPrice,

                    }).GroupBy(g => new { g.OrderId, g.orderStatus, g.createdTime, g.buyerUserID, g.firstName }).Select(s => new
                    {
                        OrderId = s.Key.OrderId,
                        orderStatus = s.Key.orderStatus,
                        createdTime = s.Key.createdTime,
                        buyerUserID = s.Key.buyerUserID,
                        buyerName = s.Key.firstName,
                        //SellerID =s.Select(i => i.SellerID),
                        price = s.Select(i => i.oneProductTotalPrice).Sum()
                    });

                    return Json(ordercmplist, JsonRequestBehavior.AllowGet);
                }
            }

            var orderlist = db.Order.Join(db.OrderDetail, x => x.OrderId, y => y.OrderID, (x, y) => new
            {
                x.OrderId,
                x.orderStatus,
                x.createdTime,
                x.buyerUserID,
                x.Member.firstName,
                //y.SellerID,
                y.oneProductTotalPrice,

            }).GroupBy(g => new { g.OrderId, g.orderStatus, g.createdTime, g.buyerUserID, g.firstName }).Select(s => new
            {
                OrderId = s.Key.OrderId,
                orderStatus = s.Key.orderStatus,
                createdTime = s.Key.createdTime,
                buyerUserID = s.Key.buyerUserID,
                buyerName = s.Key.firstName,
                //SellerID =s.Select(i => i.SellerID),
                price = s.Select(i => i.oneProductTotalPrice).Sum()
            });

            return Json(orderlist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult delOrder_P(int OrderId)
        {
            od.Delete(OrderId);
            return Content("刪除成功");
        }

        public ActionResult getOrderDetail_P(int OrderId)
        {
            var orderDetail = db.OrderDetail.Where(o => o.OrderID == OrderId).Select(s => new
            {
                ProductPostID = s.ProductPostID,
                productName = s.ProductPost.productName,
                oneProductTotalPrice = s.QTY * s.ProductPost.price,
                QTY = s.QTY,
                createdTime = s.Order.createdTime,
                SellerName = s.ProductPost.Member.firstName
            }).ToList();

            return Json(orderDetail, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getProduct_P(int? ProductPostID, int? QTY)
        {
            if (ProductPostID == null || QTY == null)
            {
                return Content("格式錯誤");
            }
            var product = db.ProductPost.Where(p => p.ProductPostID == ProductPostID).Select(s => new
            {
                ProductPostID = s.ProductPostID,
                productName = s.productName,
                productDescription = s.productDescription,
                productImg = s.productImg,
                UserName = s.Member.firstName,
                QTY = QTY,
                price = QTY * s.price,
                TagID = s.TagID,
                Tag = s.Tag.tagName,
                createdTime = s.createdTime

            }).ToList();

            return Json(product, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult shoppingCart()
        //{

        //    var id = Convert.ToInt32(Request.Cookies["LoginID"].Value);
        //    var list = db.OrderDetail.Where(x => x.OrderID == id && (x.ProductPost.Cart.cartID == id)).Select({ 
        //            cartID=x.ProductPost.Cart.cartID,
        //            cartNumber= x.ProductPost.Cart.cartNumber,
        //            cartQTY=x.ProductPost.Cart.QTY,
        //    }).ToList();
        //    return Json(list, JsonRequestBehavior.AllowGet));
        //}

        public ActionResult report()
        {
            return View();
        }

        public ActionResult getReport(int ?postID)
        {
            var reportDetail = db.ReportDetail.Select(t => new
            {
                //reportorID = t.reportorID,
                //reportedUserID = t.repotedUserID,
                //reportStatus = t.reportStatus,
                //reportDetailID = t.ReportDetailID,
                //createdTime = t.createdTime,
                reason = t.reason
            }).ToList();

            return Json(reportDetail, JsonRequestBehavior.AllowGet);
        }


        maimaiRepository<Report> rtdb = new maimaiRepository<Report>();

        public ActionResult saveReport(string reportTXT, Array reportTAG)
        {
            Report r = new Report
            {
                //reportorID = formadata.reportorID,
                //reportedUserID = formdata.reportedUserID,
                //reportStatus = formdata.reportStatus,
                //ReportDetailID=form.reportDetailID,
                //createdTime=form.createdTime,
                //ReportDetail.reason = reportTXT
            };

            rtdb.Create(r);

            return Content("新增成功");
        }




    }
}