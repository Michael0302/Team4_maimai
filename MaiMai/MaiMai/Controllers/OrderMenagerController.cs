using MaiMai.Models;
using MaiMai.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaiMai.Controllers
{
    public class OrderMenagerController : Controller
    {
        // GET: OrderMenager
        maimaiEntities db = new maimaiEntities();
        public ActionResult OrderMenagerIndex()
        {
            return View();
        }

        public ActionResult getNonPayOrderList(string userid) {

            var id = Convert.ToInt32(userid);

            //var table = db.Order.Where(m => m.buyerUserID == id && m.orderStatus == 0).Select(m => new OrderViewModel() {

            //    OrderId = m.OrderId,
            //    orderStatus = m.orderStatus,
            //    createdTime = m.createdTime,

            //}).ToList();

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
                createdTime = s.Key.createdTime,
                price = (int)s.Select(i => i.oneProductTotalPrice).Sum()
            }).ToList(); ;


            return Json(table, JsonRequestBehavior.AllowGet);

        }
    }
}