﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assignment2.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HardwareDBEntities : DbContext
    {
        public HardwareDBEntities()
            : base("name=HardwareDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<lgcustomer> lgcustomers { get; set; }
        public virtual DbSet<lgdepartment> lgdepartments { get; set; }
        public virtual DbSet<lgemployee> lgemployees { get; set; }
        public virtual DbSet<lginvoice> lginvoices { get; set; }
        public virtual DbSet<lgline> lglines { get; set; }
        public virtual DbSet<lgproduct> lgproducts { get; set; }
        public virtual DbSet<lgsupply> lgsupplies { get; set; }
        public virtual DbSet<lgvendor> lgvendors { get; set; }
    }
}
