﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaiMai.Models.ViewModel
{
    public class RequiredPostViewModel_C
    {
        public int RequiredPostID { get; set; }
        public Nullable<System.DateTime> postTime { get; set; }
        public string postDescription { get; set; }
        public string postName { get; set; }
        public string postImg { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> requiredQTY { get; set; }
        public Nullable<int> estimatePrice { get; set; }
        public Nullable<int> TagID { get; set; }
        public Nullable<int> OrderID { get; set; }

        public string userAccount { get; set; }

    }
}