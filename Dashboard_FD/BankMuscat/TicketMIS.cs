using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankMuscat
{
    public class TicketMIS
    {
        public Int64       Total_Count     { get; set; }
        public Int64       Total_Open      { get; set; }
        public Int64       Total_Close     { get; set; }
        public DateTime?   Entry_Time     { get; set; }
    }
}
