﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class maimaiEntities : DbContext
    {
        public maimaiEntities()
            : base("name=maimaiEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Chat> Chat { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<NotificationID> NotificationID { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<ProductPost> ProductPost { get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<ReportDetail> ReportDetail { get; set; }
        public virtual DbSet<RequiredPost> RequiredPost { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
    }
}
