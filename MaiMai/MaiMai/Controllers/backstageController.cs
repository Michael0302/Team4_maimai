using MaiMai.Models;
using MaiMai.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        maimaiRepository<Order> od = new maimaiRepository<Order>();
        maimaiRepository<ProductPost> prod = new maimaiRepository<ProductPost>();
        maimaiEntities db = new maimaiEntities();
        maimaiRepository<Tag> tagdb = new maimaiRepository<Tag>();
        public ActionResult backstageIndex()
        {
            return View();
        }

        public ActionResult getMemberList_P()
        {

            var memList = db.Member.Select(m=> new MemberViewModel() { 
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

            var memList = db.Member.Where(m=>m.userLevel == userLevel).Select(m => new MemberViewModel()
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

        public ActionResult getMember_P(int? id)
        {
            if(id == null)
            {
                return Content("錯誤");
            }
            var mem = db.Member.Where(m=>m.UserID ==id).Select(m=> new MemberViewModel() {
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

        public ActionResult getOrderList_P(int? status)
        {
            if(status != null )
            {               
                if(status == 2 || status==3) { 
                var ordercmplist = db.Order.Where(m=>m.orderStatus >= 2).Join(db.OrderDetail, x => x.OrderId, y => y.OrderID, (x, y) => new
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

                return Json(ordercmplist, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var ordercmplist = db.Order.Where(m => m.orderStatus < 2).Join(db.OrderDetail, x => x.OrderId, y => y.OrderID, (x, y) => new
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
                price =s.Select(i=>i.oneProductTotalPrice).Sum()
            });

            return Json(orderlist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult delOrder_P(int OrderId)
        {
            db.Order.Find(OrderId).orderStatus = 4;
            db.SaveChanges();
            //od.Delete(OrderId);

            return Content("刪除成功");
        }

        public ActionResult getOrderDetail_P(int OrderId)
        {
            var orderDetail = db.OrderDetail.Where(o=>o.OrderID == OrderId).Select(s => new
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
            if(ProductPostID== null || QTY == null)
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
                price = QTY*s.price,
                TagID = s.TagID,
                Tag = s.Tag.tagName,
                createdTime = s.createdTime

            }).ToList();

            return Json(product, JsonRequestBehavior.AllowGet);
        }

        public ActionResult pushInfo_P()
        {
            var memIDList = db.Member.Select(s => new
                                            {
                                                UserID = s.UserID
                                            }).ToList();
            //List<string> names = new List<string>();
            //foreach (var i in memIDList)
            //{

            //    names.Add(i.ToString());
            //}

            //return string.Join(',', names);
            return Json(memIDList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getTag()
        {
            var AllTag = db.Tag.Select(t => new
            {
                tagName = t.tagName
            }).ToList();

            return Json(AllTag, JsonRequestBehavior.AllowGet);
        }


        public ActionResult createTag(string TagName)
        {
            var isActive = db.Tag.FirstOrDefault(x=>x.tagName == TagName);
            if(isActive != null)
            {
                Response.StatusCode = 500;
                return Content("此標籤已存在");
            }
            Tag t = new Tag
            {
                tagName = TagName
        };

            tagdb.Create(t);

            return Content("新增成功");
        }

        //商品列表
        public ActionResult getAllPorducts()
        {
            var prodlist = db.ProductPost.Where(p=>p.status ==true).Select(s=>new {
                ProductPostID = s.ProductPostID,
                productName = s.productName,
                productDescription = s.productDescription,
                productImg = s.productImg,
                UserName = s.Member.firstName,
                UserID = s.UserID,
                price = s.price,
                TagID = s.TagID,
                Tag = s.Tag.tagName,
                createdTime = s.createdTime,
                inStoreQTY = s.inStoreQTY,
                RequiredPostID = s.RequiredPostID
            });

            return Json(prodlist, JsonRequestBehavior.AllowGet);
        }

        //單一商品貼文
        public ActionResult getProductPostFromAll(int ProductPostID)
        {
            var prodlist = db.ProductPost.Where(p=>p.ProductPostID == ProductPostID).Select(s => new {
                ProductPostID = s.ProductPostID,
                productName = s.productName,
                productDescription = s.productDescription,
                productImg = s.productImg,
                UserName = s.Member.firstName,
                UserID = s.UserID,
                price = s.price,
                TagID = s.TagID,
                Tag = s.Tag.tagName,
                createdTime = s.createdTime,
                inStoreQTY = s.inStoreQTY,
                RequiredPostID = s.RequiredPostID
            });

            return Json(prodlist, JsonRequestBehavior.AllowGet);
        }

    //下架商品
        public ActionResult cancelProduct(int ProductPostID)
        {
            var cancel = db.ProductPost.Find(ProductPostID);

            cancel.status = false;
            db.SaveChanges();

            return Content("成功下架");
        }

      //取得刪除列表
        public ActionResult getDelPorducts()
        {
            var prodlist = db.ProductPost.Where(p => p.status == false).Select(s => new {
                ProductPostID = s.ProductPostID,
                productName = s.productName,
                productDescription = s.productDescription,
                productImg = s.productImg,
                UserName = s.Member.firstName,
                UserID = s.UserID,
                price = s.price,
                TagID = s.TagID,
                Tag = s.Tag.tagName,
                createdTime = s.createdTime,
                inStoreQTY = s.inStoreQTY,
                RequiredPostID = s.RequiredPostID
            });

            return Json(prodlist, JsonRequestBehavior.AllowGet);
        }

      //取得檢舉列表
        public ActionResult getReport_P()
        {
            var allreports = db.Report.Select(s => new
            {
                ReportID = s.ReportID,
                reportorID = s.reportorID,
                reportorName = s.Member.firstName,
                repotedUserID = s.repotedUserID,
                repotedUserName = s.Member1.firstName,
                reportStatus = s.reportStatus,
                createdTime = s.createdTime,
                ReportDetailID = s.ReportDetailID,
                ReportDetailTag = s.ReportDetail.reason,
                reportDescription = s.reportDescription,
                ProductOrRequire = s.ProductOrRequire,
                ProductOrRequireID = s.ProductOrRequireID,
            }).OrderByDescending(o=>o.ReportID);

            return Json(allreports, JsonRequestBehavior.AllowGet);
        }

        //檢舉modal
        public ActionResult getReportDetail_P(int ReportID)
        {
            var reportdetail = db.Report.Where(m => m.ReportID == ReportID).Select(s => new
            {
                ReportID = s.ReportID,
                reportorID = s.reportorID,
                reportorName = s.Member.firstName,
                repotedUserID = s.repotedUserID,
                repotedUserName = s.Member1.firstName,
                reportStatus = s.reportStatus,
                createdTime = s.createdTime,
                ReportDetailID = s.ReportDetailID,
                ReportDetailTag = s.ReportDetail.reason,
                reportDescription = s.reportDescription,
                ProductOrRequire = s.ProductOrRequire,
                ProductOrRequireID = s.ProductOrRequireID,
            });

            return Json(reportdetail, JsonRequestBehavior.AllowGet);
        }

        //修改檢舉狀態
        public void editReportStatus_P(int ReportID)
        {
            db.Report.Find(ReportID).reportStatus = 1;
            db.SaveChanges();
        }

        //抓cookie userID
        public ActionResult getUserID_P()
        {
            if(Request.Cookies["LoginID"] == null)
            {
                return Content("null");
            }
            var userID = Request.Cookies["LoginID"].Value.ToString();

            return Content(userID);
        }
    }

    

}