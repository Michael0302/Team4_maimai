using MaiMai.Models;
using MaiMai.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaiMai.Controllers
{
    public class MyAccountController : Controller
    {
        // GET: MyAccount
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult myAccountIndex()
        {
            return View();
        }

        maimaiEntities db = new maimaiEntities();

        public ActionResult getAccount_P()
        {
            var userName =Request.Cookies["LoginName"].Value.ToString();
            if (userName == null)
                return Content("尚未登入");
            var getAccount = db.Member.Where(m => m.userAccount == userName).Select(s => new MemberViewModel()
            {
                UserID = s.UserID,
                userAccount = s.userAccount,
                userPassWord = s.userPassWord,
                city = s.city,
                address = s.address,
                phoneNumber = s.phoneNumber,
                firstName = s.firstName,
                lastName = s.lastName,
                birthday = s.birthday,
                identityNumber = s.identityNumber,
                profileImg = s.profileImg,
                userLevel = s.userLevel,
                totalStarRate = s.totalStarRate,
                selfDescription = s.selfDescription,
                email = s.email
            }).ToList();

            return Json(getAccount, JsonRequestBehavior.AllowGet);
        }
        maimaiRepository<Member> mb = new maimaiRepository<Member>();

        [HttpPost]
        public ActionResult editMember_P(Member mem)
        {
            mb.Update(mem);

            return Content("修改成功");
        }


    }
}