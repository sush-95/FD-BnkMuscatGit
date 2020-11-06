using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankMuscat
{
    public class ReactiveReportsType
    {
        
    }

    public class All_Checker_Report
    {

    }

    public class All_Maker_Report
    {

    }
    public class Maximum_Dispute_Raising_Machine
    {
       public string Device_Type    {get;set;}
       public string Device_Number  {get;set;}
       public string MachineCount { get; set; }
    }

    public class Maximum_Dispute_Raising_Customer
    {
        public string CustomerName { get; set; }
        public string CIF_Number { get; set; }
        public string CustomerCount { get; set; }
    }
}
