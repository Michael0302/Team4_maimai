using MaiMai.Models;
using MaiMai.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaiMai.Controllers
{
    public class MembersController : Controller
    {
        // GET: Members
        maimaiEntities db = new maimaiEntities();
        [HttpPost]
        public string SignUpMethod(Member mb)
        {

            mb.userLevel = 3;
            mb.totalStarRate = 0;
            if (mb.city == null)
            {
                return "輸入東西";
            }
            new maimaiRepository<Member>().Create(mb);

            return  "註冊成功" ; /* RedirectToAction("Homepage", "HomePage_C");*/
        }










        //public ActionResult test(Member mb)
        //{
        //    //if (ModelState.IsValid == false)
        //    //{


        //    //    return View();
        //    //}
        //    new maimaiRepository<Member>().Create(mb);

        //    return View(); /* RedirectToAction("Homepage", "HomePage_C");*/
        //}
        public ActionResult fuck()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
      
        public string CheckAccount(string account)
        {
            if (account == "")
            {
                return"2";
            }



            SqlConnection conn = new SqlConnection("data source=maimai.database.windows.net;initial catalog=maimai2;persist security info=True;user id=team4;password=MaiMai123;");
            conn.Open();

            SqlCommand bb = new SqlCommand("Select * From Member Where userAccount='" + account + "'", conn);
            SqlDataReader ha = bb.ExecuteReader();
            if (ha.Read())
                 
            {
                return "1";
            }
            return "0";
        }


         
       

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public String LoginMethod(LoginViewModel login)
        {
            //if (ModelState.IsValid)
            //{
            //    return View();
            //}

            Member mb = db.Member.FirstOrDefault(m => m.userAccount == login.userAccount && m.userPassWord == login.userPassWord);
            
            if (mb == null)
            {
                 
               
                return "帳號不存在或密碼錯誤!";
            }

            Response.Cookies["LoginAccount"].Value = mb.UserID.ToString();
            Response.Cookies["LoginName"].Value = mb.userAccount.ToString();
            Response.Cookies["LoginID"].Value = mb.UserID.ToString();
            Response.Cookies["MemberLevel"].Value = mb.userLevel.ToString();


            if (login.RememberMe == "on")
            {
                Response.Cookies["MemberLevel"].Expires = DateTime.Now.AddDays(7);
                Response.Cookies["LoginAccount"].Expires = DateTime.Now.AddDays(7);
                Response.Cookies["LoginName"].Expires = DateTime.Now.AddDays(7);
                Response.Cookies["LoginID"].Value = mb.UserID.ToString();
            }



            //return RedirectToAction("Homepage", "HomePage_C");
            //return RedirectToAction("index", "Home");
            return "登入成功";
        
        }
        public ActionResult Logout()
        {
            Response.Cookies["LoginAccount"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["LoginName"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["MemberLevel"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["LoginID"].Expires = DateTime.Now.AddDays(-1);

            return RedirectToAction("MaimaiIndexNew", "NewMaimaiIndex");
        }

        //public string uploadPhoto(upLoadPhotoViewModel data)
        //{
        //    if (data.upphoto == null)
        //    {
        //        return "../Content/resource_nico/images/無圖示.jpg";
        //    }
        //    //HttpPostedFileBase photo = new HttpPostedFileBase(upphoto);
        //    string filename = data.upphoto.FileName;
        //    data.upphoto.SaveAs(Server.MapPath("../Content/MemberImg/") + filename);
        //    string filePath = $"../Content/MemberImg/{filename}";

        //    return filePath;
        //}

    }
}