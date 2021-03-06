using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaiMai.Models.ViewModel
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public Nullable<int> buyerUserID { get; set; }
        public Nullable<int> orderStatus { get; set; }
        public Nullable<System.DateTime> createdTime { get; set; }
        
        public Nullable<int> SellerID { get; set; }
        public int price { get; set; }

  
    }
}