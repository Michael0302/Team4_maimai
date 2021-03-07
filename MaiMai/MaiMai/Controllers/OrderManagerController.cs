﻿using MaiMai.Models;
using MaiMai.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaiMai.Controllers
{
    public class OrderManagerController : Controller
    {
        // GET: OrderMenager
        maimaiEntities db = new maimaiEntities();
     
        public ActionResult OrderIndex() {
            return View();
        }

        public ActionResult getNonPayOrderList(string userid)
        {

            var id = Convert.ToInt32(userid);

            var table = db.Order.Where(m => m.buyerUserID == id && m.orderStatus == 0).Join(db.OrderDetail, o => o.OrderId, od => od.OrderID
            , (o, od) => new
            {
                o.OrderId,
                o.orderStatus,
                o.createdTime,
                o.buyerUserID,
                od.oneProductTotalPrice,

            }).GroupBy(g => new { g.OrderId, g.orderStatus, g.createdTime, g.buyerUserID }).Select(s => new OrderViewModel()
            {
                OrderId = s.Key.OrderId,
                orderStatus = s.Key.orderStatus,
                orderStatusString = s.Key.orderStatus.ToString(),
                createdTime = s.Key.createdTime,
                price = (int)s.Select(i => i.oneProductTotalPrice).Sum()
            }).ToList(); ;


            return Json(table, JsonRequestBehavior.AllowGet);

        }


        public ActionResult showOrderRecipt(string orderid, string userid)
        {
            var oid = Convert.ToInt32(orderid);
            var mid = Convert.ToInt32(userid);

            var table = db.Order.Where(m => m.buyerUserID == mid && m.OrderId == oid).Join(db.OrderDetail, o => o.OrderId, od => od.OrderID
               , (o, od) => new
               {
                   createdTime = o.createdTime,
                   productName =od.ProductPost.productName,
                   productImg= od.ProductPost.productImg,
                   QTY= od.QTY,
                   OrderDetailID= od.OrderDetailID,
                   oneProductTotalPrice= od.oneProductTotalPrice,
                   userAccount=od.Member.userAccount
               }).ToList();

            return Json(table, JsonRequestBehavior.AllowGet);
        }

        maimaiRepository<Order> order = new maimaiRepository<Order>();



        public ActionResult checkout( string orderid) {

            var oid = Convert.ToInt32(orderid);
            var  orderReciept= order.GetbyID(oid);

            orderReciept.orderStatus = 1;

            

        }
    } //end of class
}//end of namespace