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
    
    public partial class tbl_MasterDeviceList
    {
        public long ID { get; set; }
        public string TerminalID { get; set; }
        public string DeviceId { get; set; }
        public string Make { get; set; }
        public string SerialNumber { get; set; }
        public string ModelNumber { get; set; }
        public string TerminalType { get; set; }
        public string GLNumber { get; set; }
        public string DeviceLocation { get; set; }
        public string Custodian { get; set; }
        public string CustodianAlertEmail { get; set; }
        public string CustodianAlertMobile { get; set; }
        public Nullable<System.DateTime> EntryTime { get; set; }
        public string EntryBy { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string UpdateBy { get; set; }
        public bool IsActive { get; set; }
    }
}
