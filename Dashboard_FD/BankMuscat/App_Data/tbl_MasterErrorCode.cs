//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BankMuscat.App_Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_MasterErrorCode
    {
        public long ID { get; set; }
        public string Make { get; set; }
        public string Machine_Type { get; set; }
        public string Error_Code { get; set; }
        public string Error_Description { get; set; }
        public Nullable<System.DateTime> EntryTime { get; set; }
        public string EntryBy { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string UpdateBy { get; set; }
        public bool IsActive { get; set; }
    }
}