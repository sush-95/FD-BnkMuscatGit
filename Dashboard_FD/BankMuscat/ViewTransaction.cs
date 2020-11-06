using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankMuscat
{
    public class ViewTransaction
    {
        public string CustomerName { get; set; }
        public string EJID { get; set; }
        public string FeedBackId { get; set; }
        public string RegMobileNumber { get; set; }
        public string RegEmailId { get; set; }
        public string CardNumber { get; set; }
        public string AccountNumber { get; set; }
        public string TransactionType { get; set; }
        public DateTime? Recordts { get; set; }
        public string Amount { get; set; }

        public string Counterfeit_Amount { get; set; }
        public string Cash_Retracted_Amount { get; set; }

        public string TerminalID { get; set; }
        public string DeviceId { get; set; }
        public string User_Action { get; set; }
        public string User_Recommendation { get; set; }
        public string RepeatedClaimNo { get; set; }
        public string Maker_Input { get; set; }
        public string Checker_Input { get; set; }
        public string BotStatus { get; set; }
        public string MakerComment { get; set; }
        public string MakerComment_Hold { get; set; }
        public string CheckerComment { get; set; }
        public string CheckerComment_Hold { get; set; }
        public string BalancingAmount { get; set; }
        public List<string> T24ImageUrls { get; set; }
        public List<string> WeCareImageUrls { get; set; }
        public string IsLocked { get; set; }
        public string SubmitEnable { get; set; }
        public string Transaction_Logs { get; set; }
        public string CIF_Number { get; set; }
        public string Comment_Eng { get; set; }
        public string Reroute_Status { get; set; }
        
    }
}
