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
    
    public partial class ProductPost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductPost()
        {
            this.Cart = new HashSet<Cart>();
            this.OrderDetail = new HashSet<OrderDetail>();
        }
    
        public int ProductPostID { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public string productImg { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> inStoreQTY { get; set; }
        public Nullable<int> price { get; set; }
        public Nullable<int> TagID { get; set; }
        public Nullable<System.DateTime> createdTime { get; set; }
        public Nullable<int> RequiredPostID { get; set; }
        public string county { get; set; }
        public string district { get; set; }
        public Nullable<bool> status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Cart { get; set; }
        public virtual Member Member { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
