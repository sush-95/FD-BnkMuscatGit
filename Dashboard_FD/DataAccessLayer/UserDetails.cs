using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserDetails
    {
        public string TerminalID { get; set; }
        public string ErrorCode { get; set; }
        public string Amount { get; set; }
        public DateTime? TransactionTime { get; set; }
        public DateTime? BotProcessStartTime { get; set; }
        public DateTime? BotProcessEndTime { get; set; }
        public string BotStatus { get; set; }
        public string BotRemarks { get; set; }
        public string UserAction { get; set; }
        public string UserRecommendation { get; set; }
        public string RepeatedClaimNo { get; set; }
        public string MakerStatus { get; set; }
        public string CheckerStatus { get; set; }
        public string CaseStatus { get; set; }
        public string SolvedBy { get; set; }
    }
}
