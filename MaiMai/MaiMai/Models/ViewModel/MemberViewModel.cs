using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaiMai.Models.ViewModel
{
    public class MemberViewModel
    {
        public int UserID { get; set; }
        public string userAccount { get; set; }
        public string userPassWord { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Nullable<System.DateTime> birthday { get; set; }
        public string identityNumber { get; set; }
        public string profileImg { get; set; }
        public Nullable<int> userLevel { get; set; }
        public Nullable<double> totalStarRate { get; set; }
        public string selfDescription { get; set; }
        public string email { get; set; }
    }
}