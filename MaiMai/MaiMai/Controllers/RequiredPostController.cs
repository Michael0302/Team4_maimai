﻿using DocumentFormat.OpenXml.Bibliography;
using MaiMai.Models;
using MaiMai.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaiMai.Controllers
{
    public class RequiredPostController : Controller
    {
        // GET: RequiredPost

        maimaiRepository<RequiredPost> requiredPostTable = new maimaiRepository<RequiredPost>();
        maimaiEntities db = new maimaiEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult requiredPostIndex()
        {

            return View();
        }

       
        public JsonResult allrequiredPost()
        {

            var table = db.RequiredPost.Select(m => new RequiredPostViewModel_C()
            {
                RequiredPostID = m.RequiredPostID,
                postTime = m.postTime,
                postDescription = m.postDescription,
                postName = m.postName,
                postImg = m.postImg,
                UserID = m.UserID,
                requiredQTY = m.requiredQTY,
                estimatePrice = m.estimatePrice,
                TagID = m.TagID,
                OrderID = m.OrderID,
                userAccount = m.Member.userAccount
            }).ToList();
         

            return Json(table, JsonRequestBehavior.AllowGet);
        }// allpostend

        public ActionResult allpoccessPost() {

            var table = db.RequiredPost.Where( m => m.OrderID != null).Select(m => new RequiredPostViewModel_C()
            {
                RequiredPostID = m.RequiredPostID,
                postTime = m.postTime,
                postDescription = m.postDescription,
                postName = m.postName,
                postImg = m.postImg,
                UserID = m.UserID,
                requiredQTY = m.requiredQTY,
                estimatePrice = m.estimatePrice,
                TagID = m.TagID,
                OrderID = m.OrderID,
                userAccount = m.Member.userAccount
            }).ToList();


            return Json(table, JsonRequestBehavior.AllowGet);

        }

        public string uploadPhoto(HttpPostedFileBase upphoto) {
            if (upphoto == null)
            {
                return "/Content/resource_nico/images/無圖示.jpg";
            }
            string filename = upphoto.FileName;
            upphoto.SaveAs(Server.MapPath("~/Content/resource_nico/images/") + filename);
            string filePath = $"/Content/resource_nico/images/{filename}";

            return filePath;
        }

        public ActionResult getAllTag() {

            var table = db.Tag.Select(m => new TagViewModel() {

                TagID = m.TagID,
                tagName = m.tagName

            }).ToList();


            return Json(table, JsonRequestBehavior.AllowGet);
        }

        maimaiRepository<ProductPost> productPostRepository = new maimaiRepository<ProductPost>();
        public string commemtProductPost(ProductPost ps)
        {

            productPostRepository.Create(ps);

            return "建立成功";
        }




    }//class end
}//namespace end