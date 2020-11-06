using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankMuscat
{
    public class UserDetails
    {
        public string ID { get; set; }
        public string TerminalID { get; set; }
        public string ErrorCode { get; set; }
        public string Amount { get; set; }

        public string Counterfeit_Amount { get; set; }
        public string Cash_Retracted_Amount { get; set; }

        public string       AccountNo                 { get; set; }
        public string       CardNo                    { get; set; }
        public DateTime?    TransactionTime           { get; set; }
        public DateTime?    BotProcessStartTime       { get; set; }
        public DateTime?    BotProcessEndTime         { get; set; }
        public string       BotStatus                 { get; set; }
        public string       BotRemarks                { get; set; }
        public string       UserAction                { get; set; }
        public string       UserRecommendation { get; set; }
        public string       RepeatedClaimNo { get; set; }
        public string       MakerStatus { get; set; }
        public string       CheckerStatus { get; set; }
        public string       CaseStatus { get; set; }
        public string       SolvedBy { get; set; }
        public string       FeedBackId { get; set; }

        public string       MakerID { get; set; }
        public string       chekerID { get; set; }
        public string       DeviceId { get; set; }

        public string    Reroute_Status { get; set; }




    }
}
