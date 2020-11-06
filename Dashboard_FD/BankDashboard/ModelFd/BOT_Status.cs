namespace BankDashboard.ModelFd
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BOT_Status
    {
        public int ID { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string RunningStatus { get; set; }
    }
}
