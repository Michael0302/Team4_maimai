//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MaiMai.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Report
    {
        public int ReportID { get; set; }
        public int reportorID { get; set; }
        public int repotedUserID { get; set; }
        public Nullable<int> reportStatus { get; set; }
        public Nullable<System.DateTime> createdTime { get; set; }
        public Nullable<int> ReportDetailID { get; set; }
        public string reportDescription { get; set; }
        public int ProductOrRequire { get; set; }
        public int ProductOrRequireID { get; set; }
    
        public virtual ReportDetail ReportDetail { get; set; }
        public virtual Member Member { get; set; }
        public virtual Member Member1 { get; set; }
    }
}
