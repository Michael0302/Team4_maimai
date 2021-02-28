﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaiMai.Models.MaimaiIndexViewModel
{
    public class MaimaiIndexViewModel
    {
        //public int TagID { get; set; }
        public string tagName { get; set; }

        public int ProductPostID { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public string productImg { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> inStoreQTY { get; set; }
        public Nullable<int> price { get; set; }
        public Nullable<int> TagID { get; set; }
        public Nullable<System.DateTime> createdTime { get; set; }
    }
}