//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer.App_Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_MasterBINList
    {
        public long ID { get; set; }
        public string ProductName { get; set; }
        public string BIN { get; set; }
        public string SUBBIN { get; set; }
        public Nullable<System.DateTime> EntryTime { get; set; }
        public string EntryBy { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string UpdateBy { get; set; }
        public bool IsActive { get; set; }
    }
}
