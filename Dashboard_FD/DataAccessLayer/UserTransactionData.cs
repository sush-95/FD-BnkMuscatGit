using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.App_Data;

namespace DataAccessLayer
{
    public class UserTransactionData
    {
        public List<UserDetails> GetTransactionData(DateTime? startDate, DateTime? endDate, string terminaldId, string status, string reportType, string userName, string userType)
        {
            List<UserDetails> userDetails = new List<UserDetails>();
            using (var context = new Bank_Entities())
            {
                var data = context.GetDetailUserWise("Default");
                if (data.Count() > 0)
                {
                    foreach (var datarow in data)
                    {
                        UserDetails transactionDetails = new UserDetails();
                        transactionDetails.TerminalID = datarow.TerminalID;
                        transactionDetails.ErrorCode = datarow.Error_Code;
                        transactionDetails.Amount = datarow.Amount;
                        transactionDetails.TransactionTime = datarow.RecordTS;
                        transactionDetails.BotProcessStartTime = datarow.BotProcess_StartTime;
                        transactionDetails.BotProcessEndTime = datarow.BotProcess_EndTime;
                        transactionDetails.BotStatus = datarow.Bot_Status;
                        transactionDetails.BotRemarks = datarow.Bot_Remarks;
                        transactionDetails.UserAction = datarow.User_Action;
                        transactionDetails.UserRecommendation = datarow.User_Recommendation;
                        //  transactionDetails.RepeatedClaimNo = datarow.RepeatedClaimNo;
                        //   transactionDetails.MakerStatus = datarow.MakerStatus;
                        transactionDetails.CheckerStatus = datarow.Checker_Input;
                        transactionDetails.CaseStatus = datarow.Final_Status;
                        transactionDetails.SolvedBy = datarow.SolvedBy;
                        userDetails.Add(transactionDetails);
                    }
                }
            }

            return userDetails;
        }

    }
}
