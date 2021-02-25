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
    
    public class backstageController : Controller
    {
        // GET: backstage
        public ActionResult Index()
        {
            return View();
        }

        maimaiRepository<Member> mb = new maimaiRepository<Member>();
        maimaiEntities db = new maimaiEntities();
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

        public ActionResult getOrderList_P()
        {
            var orderlist = db.Order.Select(o=>new OrderViewModel() { 
                OrderId = o.OrderId,
                orderStatus = o.orderStatus,
                createdTime = o.createdTime,
                //UserID = o.OrderDetail
            }).ToList();

            return Json(orderlist, JsonRequestBehavior.AllowGet);
        }
    }

}