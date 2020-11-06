using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankMuscat.App_Data;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;

namespace BankMuscat
{
    public class UserTransactionData
    {
        string folderPath = ConfigurationManager.AppSettings["folder"];

        string connectionString = GetSqlConnection();
        public List<UserDetails> GetTransactionData(DateTime? startDate, DateTime? endDate, string terminaldId, string status, string reportType, string userName, string userType)
        {

            List<UserDetails> userDetails = new List<UserDetails>();
            using (var context = new EntitiesList())
            {
                var data = context.GetDetailUserWise(reportType).ToList();
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
                        transactionDetails.CheckerStatus = datarow.Checker_Input;
                        transactionDetails.CaseStatus = datarow.Final_Status;
                        transactionDetails.SolvedBy = datarow.SolvedBy;
                        userDetails.Add(transactionDetails);
                    }
                }
            }

            return userDetails;
        }

        public static string GetSqlConnection()
        {
            var x = System.Configuration.ConfigurationManager.ConnectionStrings["EntitiesList1"].ConnectionString;
            byte[] inputArray = Convert.FromBase64String(x);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes("sblw-3hn8-sqoy19");
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }


        public void CreateFile(string directory, string data)
        {
            string filename = "Log_" + DateTime.Today.ToString("ddMMyyyy");
            string path = Path.Combine(directory, filename);
            bool DirExists = Directory.Exists(directory);

            if (DirExists)
            {
                bool IsExists = File.Exists(path);
                if (IsExists)
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(data);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(data);
                    }
                }
            }

        }


        public List<UserDetails> GetTransactionDataList(DateTime? startDate, DateTime? endDate, string terminaldId, string status, string reportType, string userId, string userType)
        {

            List<UserDetails> userDetails = new List<UserDetails>();
            CreateFile(folderPath, "The Method is GetTransactionDataList and SP is UDSP_Search_GetDetailUserWise parameter of Date is : " + System.DateTime.Now + "\n input_type = " + reportType + "\n Param_StartDate = " + startDate + "\n Param_EndDate =" + endDate + "\n Param_TerminalId =" + terminaldId + "\n Param_Status =" + status + "\n Param_Userid =" + userId + "\n Param_Usertype =" + userType);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("UDSP_Search_GetDetailUserWise", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@input_type", string.IsNullOrWhiteSpace(reportType) ? null : reportType);
                if((startDate != null) && (endDate != null))
                {
                    cmd.Parameters.AddWithValue("@Param_StartDate", startDate);
                    cmd.Parameters.AddWithValue("@Param_EndDate", endDate);
                }                
                cmd.Parameters.AddWithValue("@Param_TerminalId", string.IsNullOrWhiteSpace(terminaldId) ? null : terminaldId);
                cmd.Parameters.AddWithValue("@Param_Status", string.IsNullOrWhiteSpace(status) ? null : status);
                cmd.Parameters.AddWithValue("@Param_Userid", userId);
                cmd.Parameters.AddWithValue("@Param_Usertype", userType);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            UserDetails transactionDetails = new UserDetails();
                            //transactionDetails.EJID = dr["ejid"].ToString();
                            transactionDetails.ID = dr["id"].ToString();
                            transactionDetails.TerminalID = dr["terminalid"].ToString();
                            transactionDetails.ErrorCode = dr["Error_Code"].ToString();
                            transactionDetails.Amount = dr["Amount"].ToString();
                            transactionDetails.Counterfeit_Amount = dr["Counterfeit_Amount"].ToString();
                            transactionDetails.Cash_Retracted_Amount = dr["Cash_Retracted_Amount"].ToString();
                            //transactionDetails.AccountNo = dr["AccountNumber"].ToString();
                            //transactionDetails.CardNo = dr["CardNumber"].ToString();
                            // transactionDetails.AccountNo=datarow.AccountNumber;AccountNumber CardNumber
                            DateTime dt;
                            bool check;
                            check = DateTime.TryParse(dr["RecordTS"].ToString(), out dt);
                            if (check == true)
                                transactionDetails.TransactionTime = dt;
                            // transactionDetails.TransactionTime =Convert.ToDateTime(dr["RecordTS"].ToString());
                            check = DateTime.TryParse(dr["BotProcess_StartTime"].ToString(), out dt);
                            if (check == true)
                                transactionDetails.BotProcessStartTime = dt;
                            //transactionDetails.BotProcessStartTime = Convert.ToDateTime(dr["BotProcess_StartTime"].ToString());
                            check = DateTime.TryParse(dr["BotProcess_EndTime"].ToString(), out dt);
                            if (check == true)
                                transactionDetails.BotProcessEndTime = dt;
                            //transactionDetails.BotProcessEndTime = Convert.ToDateTime(dr["BotProcess_EndTime"].ToString());
                            transactionDetails.BotStatus = dr["Bot_Status"].ToString();
                            transactionDetails.BotRemarks = dr["Bot_Remarks"].ToString();
                            transactionDetails.UserAction = dr["User_Action"].ToString();
                            transactionDetails.UserRecommendation = dr["User_Recommendation"].ToString();
                            transactionDetails.RepeatedClaimNo = dr["repeatedClaimNo"].ToString();
                            transactionDetails.MakerStatus = dr["Maker_Input"].ToString();
                            transactionDetails.CheckerStatus = dr["Checker_Input"].ToString();
                            transactionDetails.CaseStatus = dr["Final_Status"].ToString();
                            transactionDetails.SolvedBy = dr["SolvedBy"].ToString();
                            transactionDetails.MakerID = dr["MakerUserId"].ToString();
                            transactionDetails.chekerID = dr["CheckerUserId"].ToString();
                            transactionDetails.DeviceId = dr["DeviceId"].ToString();
                            transactionDetails.Reroute_Status = dr["Reroute_Status"].ToString();

                            userDetails.Add(transactionDetails);
                        }
                    }
                }

            }

            return userDetails;
        }

        public List<MaxDisputedMachine> GetMaxDisputedMachine(DateTime? startDate, DateTime? endDate, string reportType, string userName, string userType)
        {
            CreateFile(folderPath, "The Method is GetMaxDisputedMachine and SP is UDSP_Search_GetDetailUserWise parameter of Date is : " + System.DateTime.Now + "\n input_type = " + reportType + "\n startDate = " + startDate + "\n endDate = " + endDate + "\n userName = " + userName + "\n userType = " + userType);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("UDSP_Search_GetDetailUserWise", con);
                cmd.CommandType = CommandType.StoredProcedure;
                List<MaxDisputedMachine> ListmaxDisputedMachineDetail = new List<MaxDisputedMachine>();
                cmd.Parameters.AddWithValue("@input_type", reportType);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            MaxDisputedMachine maxDisputedMachineDetail = new MaxDisputedMachine();
                            maxDisputedMachineDetail.TerminalID = dr["terminalid"].ToString();
                            maxDisputedMachineDetail.DeviceId = dr["deviceid"].ToString();
                            maxDisputedMachineDetail.Make = dr["make"].ToString();
                            maxDisputedMachineDetail.TerminalType = dr["terminaltype"].ToString();
                            maxDisputedMachineDetail.Count = dr["countdispute"].ToString();
                            ListmaxDisputedMachineDetail.Add(maxDisputedMachineDetail);
                        }
                    }
                }
                return ListmaxDisputedMachineDetail;
            }
        }

        public MaxComplaintCustomer GetMaxDisputedCustomer(DateTime? startDate, DateTime? endDate, string reportType, string userName, string userType)
        {
            MaxComplaintCustomer maxComplaintCustomer = new MaxComplaintCustomer();
            CreateFile(folderPath, "The Method is GetMaxDisputedCustomerand SP is UDSP_Search_GetDetailUserWise parameter of Date is : " + System.DateTime.Now + "\n input_type = " + reportType + "\n startDate = " + startDate + "\n endDate = " + endDate + "\n userName = " + userName + "\n userType = " + userType);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("UDSP_Search_GetDetailUserWise", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@input_type", reportType);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                // string decryptKey = ConfigurationManager.ConnectionStrings["DecryptKey"].ConnectionString;
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            maxComplaintCustomer.CustomerName = dr["CustomerName"].ToString();
                            maxComplaintCustomer.Mobile = dr["RegMobileNumber"].ToString();
                            //maxComplaintCustomer.Mobile =Decrypt(dr["RegMobileNumber"].ToString(),decryptKey);
                            maxComplaintCustomer.Email = dr["RegEmailId"].ToString();
                            // maxComplaintCustomer.Email = Decrypt(dr["RegEmailId"].ToString(), decryptKey);
                            maxComplaintCustomer.AccountNo = dr["AccountNumber"].ToString();
                            // maxComplaintCustomer.AccountNo = Decrypt(dr["AccountNumber"].ToString(), decryptKey);
                            maxComplaintCustomer.CardNo = dr["CardNumber"].ToString();
                            //maxComplaintCustomer.CardNo = Decrypt(dr["CardNumber"].ToString(), decryptKey);
                            maxComplaintCustomer.Count = dr["countDisputeMatters"].ToString();
                        }
                    }
                }
                return maxComplaintCustomer;
            }
        }

        #region
        public Object GetReactiveHomeReportData(DateTime? startDate, DateTime? endDate, string userReportType)
        {
            List<Maximum_Dispute_Raising_Customer> maximum_Dispute_Raising_Customers = new List<Maximum_Dispute_Raising_Customer>();
            List<Maximum_Dispute_Raising_Machine> maximum_Dispute_Raising_Machines = new List<Maximum_Dispute_Raising_Machine>();
            List<tbl_DisputeData> lst_tblDispute = new List<tbl_DisputeData>();
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                List<TicketMIS> lstTicketMIS = new List<TicketMIS>();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("UDSP_Get_ReactiveReportType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param_ReportType", userReportType);
                cmd.Parameters.AddWithValue("@Param_FromDate", startDate);
                cmd.Parameters.AddWithValue("@Param_ToDate", endDate);
           
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {                        
                        if (userReportType.Equals("All Checker Report") || userReportType.Equals("All Maker Report"))
                        {
                            List<tbl_DisputeData> tbl_DisputeDatas = new List<tbl_DisputeData>();
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                tbl_DisputeData DisputeDataItem = new tbl_DisputeData();
                                DateTime dt;
                                bool check;
                                check = DateTime.TryParse(dr["EntryTime"].ToString(), out dt);
                                if (check == true)
                                {
                                    DisputeDataItem.EntryTime = dt;                                    
                                }
                                DisputeDataItem.Comment_Eng = dr["Comment_Eng"].ToString();
                                DisputeDataItem.Bot_Status = dr["Bot_Status"].ToString();
                                DisputeDataItem.Bot_Remarks = dr["Bot_Remarks"].ToString();
                                DisputeDataItem.User_Action = dr["User_Action"].ToString();
                                DisputeDataItem.User_Recommendation = dr["User_Recommendation"].ToString();
                                DisputeDataItem.MakerComment = dr["MakerComment"].ToString();
                                DisputeDataItem.CheckerComment = dr["CheckerComment"].ToString();
                                DisputeDataItem.MakerComment_Hold = dr["MakerComment_Hold"].ToString();
                                DisputeDataItem.CheckerComment_Hold = dr["CheckerComment_Hold"].ToString();
                                DisputeDataItem.Checker_Input = dr["Checker_Input"].ToString();
                                DisputeDataItem.Maker_Input = dr["Maker_Input"].ToString();
                                DisputeDataItem.MakerUserId = dr["MakerUserId"].ToString();
                                DisputeDataItem.CheckerUserId = dr["CheckerUserId"].ToString();
                                DisputeDataItem.Final_Status = dr["Final_Status"].ToString();
                                DisputeDataItem.FeedbackId = (!string.IsNullOrEmpty(dr["FeedbackId"].ToString())) ? Convert.ToInt64(dr["FeedbackId"]):0;
                                DisputeDataItem.Reroute_Status = dr["Reroute_Status"].ToString();

                                tbl_DisputeDatas.Add(DisputeDataItem);
                            }
                            return tbl_DisputeDatas;
                            
                        }
                        else if (userReportType.Equals("Maximum Dispute Raising Machine"))
                        {
                            
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                Maximum_Dispute_Raising_Machine maximum_Dispute = new Maximum_Dispute_Raising_Machine();
                                maximum_Dispute.Device_Type = dr["Device_Type"].ToString();
                                maximum_Dispute.Device_Number = dr["Device_Number"].ToString();
                                maximum_Dispute.MachineCount = dr["MachineCount"].ToString();
                                maximum_Dispute_Raising_Machines.Add(maximum_Dispute);
                            }
                            return maximum_Dispute_Raising_Machines;


                        }
                        else if (userReportType.Equals("Maximum Dispute Raising Customer"))
                        {

                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                Maximum_Dispute_Raising_Customer MDRC = new Maximum_Dispute_Raising_Customer();
                                MDRC.CIF_Number = dr["CIF_Number"].ToString();
                                MDRC.CustomerName = dr["CustomerName"].ToString();
                                MDRC.CustomerCount = dr["CustomerCount"].ToString();

                                maximum_Dispute_Raising_Customers.Add(MDRC);
                            }
                            return maximum_Dispute_Raising_Customers;
                        }
                        
                    }
                }
                return null;
                //return lstTicketMIS;
            }
        }
        #endregion

        #region
        public List<TicketMIS> GetTicketInformation(DateTime? startDate, DateTime? endDate)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                List<TicketMIS> lstTicketMIS = new List<TicketMIS>();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("UDSP_Get_Total_Open_Close_TicketCount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param_Days", DBNull.Value);
                cmd.Parameters.AddWithValue("@Param_FromDate", startDate);
                cmd.Parameters.AddWithValue("@Param_ToDate", endDate);
                
                //cmd.Parameters.AddWithValue("@Param_TransactionTime", argRecordTs);
                //  cmd.Parameters.AddWithValue("@Param_UserId", argUserId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DateTime dt;
                            bool check;
                            TicketMIS ticketMIS = new TicketMIS();
                            ticketMIS.Total_Close = Convert.ToInt64(dr["Total_Close"]);
                            ticketMIS.Total_Count = Convert.ToInt64(dr["Total_Count"]);
                            ticketMIS.Total_Open = Convert.ToInt64(dr["Total_Open"]);
                                                      
                            check = DateTime.TryParse(dr["Entry_Time"].ToString(), out dt);
                            if (check == true)
                            { 
                                ticketMIS.Entry_Time = dt;
                                lstTicketMIS.Add(ticketMIS);
                            }
                        }
                    }
                }
                return lstTicketMIS;
            }
        }
        #endregion
        #region
        public List<TicketMIS> GetTicketInformation(long NoOfDays)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                List<TicketMIS> lstTicketMIS = new List<TicketMIS>();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("UDSP_Get_Total_Open_Close_TicketCount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param_Days", NoOfDays);
                cmd.Parameters.AddWithValue("@Param_FromDate", DBNull.Value);
                cmd.Parameters.AddWithValue("@Param_ToDate", DBNull.Value);

                //cmd.Parameters.AddWithValue("@Param_TransactionTime", argRecordTs);
                //  cmd.Parameters.AddWithValue("@Param_UserId", argUserId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DateTime dt;
                            bool check;
                            TicketMIS ticketMIS = new TicketMIS();
                            ticketMIS.Total_Close = Convert.ToInt64(dr["Total_Close"]);
                            ticketMIS.Total_Count = Convert.ToInt64(dr["Total_Count"].ToString());
                            ticketMIS.Total_Open = Convert.ToInt64(dr["Total_Open"].ToString());

                            check = DateTime.TryParse(dr["Entry_Time"].ToString(), out dt);
                            if (check == true)
                            {
                                ticketMIS.Entry_Time = dt;
                                lstTicketMIS.Add(ticketMIS);
                            }
                        }
                    }
                }
                return lstTicketMIS;
            }
        }
        #endregion
        #region--------------------------------ViewTransaction Checker Or Maker-------------------------------------------------
        public ViewTransaction GetViewTransactionDetail(string argID, string argSolvedBy, string userid, string usertype)
        {
            ViewTransaction viewTransaction = new ViewTransaction();
            CreateFile(folderPath, "The Method is GetViewTransactionDetail and SP is UDSP_View_TransactionDashboard parameter of Date is : " + System.DateTime.Now + "\n argID = " + argID + "\n argSolvedBy = " + argSolvedBy + "\n userid = " + userid + "\n usertype = " + usertype);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //string decryptKey = ConfigurationManager.ConnectionStrings["DecryptKey"].ConnectionString;
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("UDSP_View_TransactionDashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param_ID",argID);
                cmd.Parameters.AddWithValue("@Param_SolvedBy", argSolvedBy);
                cmd.Parameters.AddWithValue("@Param_User", userid);
                cmd.Parameters.AddWithValue("@Param_Usertype", usertype);
                //cmd.Parameters.AddWithValue("@Param_TransactionTime", argRecordTs);
                //  cmd.Parameters.AddWithValue("@Param_UserId", argUserId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DateTime dt;
                            bool check;
                            viewTransaction.EJID = dr["ejid"].ToString();
                            viewTransaction.FeedBackId = dr["feedbackid"].ToString();
                            viewTransaction.CustomerName = dr["CustomerName"].ToString();
                            viewTransaction.RegMobileNumber = dr["RegMobileNumber"].ToString();
                            //viewTransaction.RegMobileNumber = Decrypt(dr["RegMobileNumber"].ToString(), decryptKey);
                            viewTransaction.RegEmailId = dr["RegEmailId"].ToString();
                            //viewTransaction.RegEmailId = Decrypt(dr["RegEmailId"].ToString(), decryptKey);
                            viewTransaction.CardNumber = dr["CardNumber"].ToString();
                            //viewTransaction.CardNumber = Decrypt(dr["CardNumber"].ToString(), decryptKey);
                            viewTransaction.AccountNumber = dr["AccountNumber"].ToString();
                            //viewTransaction.AccountNumber = Decrypt(dr["AccountNumber"].ToString(), decryptKey);
                            check = DateTime.TryParse(dr["recordts"].ToString(), out dt);
                            if (check == true)
                                viewTransaction.Recordts = dt;
                            viewTransaction.Amount = dr["amount"].ToString();
                            viewTransaction.Counterfeit_Amount = dr["Counterfeit_Amount"].ToString();
                            viewTransaction.Cash_Retracted_Amount = dr["Cash_Retracted_Amount"].ToString();
                            viewTransaction.BotStatus = dr["Bot_Status"].ToString();
                            viewTransaction.TerminalID = dr["TerminalID"].ToString();
                            viewTransaction.DeviceId = dr["DeviceId"].ToString();
                            viewTransaction.User_Action = dr["User_Action"].ToString();
                            viewTransaction.User_Recommendation = dr["User_Recommendation"].ToString();
                            //viewTransaction.RepeatedClaimNo = dr["repeatedClaimNo"].ToString();
                            viewTransaction.Maker_Input = dr["Maker_Input"].ToString();
                            viewTransaction.Checker_Input = dr["Checker_Input"].ToString();
                            viewTransaction.MakerComment = dr["MakerComment"].ToString();
                            viewTransaction.MakerComment_Hold = dr["MakerComment_Hold"].ToString();
                            viewTransaction.CheckerComment = dr["CheckerComment"].ToString();
                            viewTransaction.CheckerComment_Hold = dr["CheckerComment_Hold"].ToString();
                            //viewTransaction.BalancingAmount = dr["BalancingAmount"].ToString();
                            viewTransaction.TransactionType = dr["TransactionType"].ToString();
                            viewTransaction.RepeatedClaimNo = dr["repeatedClaimNo"].ToString();
                            viewTransaction.IsLocked = dr["IsLocked"].ToString();
                            viewTransaction.SubmitEnable = dr["SubmitEnable"].ToString();
                            viewTransaction.Transaction_Logs = dr["Transaction_Logs"].ToString();
                            viewTransaction.CIF_Number = dr["CIF_Number"].ToString();
                            viewTransaction.Comment_Eng = dr["Comment_Eng"].ToString();
                            viewTransaction.Reroute_Status = dr["Reroute_Status"].ToString();

                        }
                    }
                }
                return viewTransaction;
            }
        }
        #endregion-----------------------------ViewTransaction Checker or Maker End Here-----------------------------------------

        #region--------------------------------UserList Starts Here--------------------------------------------------------------
        public List<UserList> UserData()
        {
            List<UserList> userLists = new List<UserList>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("UDSP_Get_UserList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                UserList userSelect = new UserList();
                userSelect.UserId = "0";
                userSelect.UserName = "Select";
                userLists.Add(userSelect);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            UserList user = new UserList();
                            user.UserId = dr["ID"].ToString();
                            user.UserName = dr["User_name"].ToString();
                            userLists.Add(user);
                        }
                    }
                }
                return userLists;
            }
        }
        #endregion-----------------------------UserList Ends Here----------------------------------------------------------------

        #region--------------------------------GetTransactionWiseT24URL Reactive----------------------------------------------------------

        
        public List<string> GetT24ImageUrlsReactive(string ejId)
        {
           
            List<string> urlsT24 = new List<string>();

            string imagePath = System.Configuration.ConfigurationManager.AppSettings["DownloadImageLocation"];
            string ejIdCheck = string.IsNullOrEmpty(ejId) ? "No_Image" : System.Configuration.ConfigurationManager.AppSettings["Reactive_T24"];

            //string ssName = System.Configuration.ConfigurationManager.AppSettings["EJIdFormat"].Trim().ToString();
            if (Directory.Exists(imagePath))
            {
                DirectoryInfo directory = new DirectoryInfo(imagePath);
                FileInfo[] files = directory.GetFiles(ejIdCheck + ejId + "*.jpg", SearchOption.AllDirectories);

                var allfiles = files.Select(s => s.FullName);
                //var allT24Files = allfiles.Where(x => x.Contains(ejIdCheck)).Select(x => x);
                foreach (var T24File in allfiles)
                {
                    urlsT24.Add(T24File.ToString().Replace(imagePath,""));
                }
            }



            return urlsT24;
        }
        #endregion

        #region--------------------------------GetTransactionWiseT24URLProactive----------------------------------------------------------
        public List<string> GetT24ImageUrls(string ejId)
        {
            //List<string> urlsT24 = new List<string>();
            //string imagePath = System.Configuration.ConfigurationManager.AppSettings["DownloadImageLocation"];
            //string ejIdCheck = string.IsNullOrEmpty(ejId) ? "No_Image" : System.Configuration.ConfigurationManager.AppSettings["EJIdFormat"] + ejId;
            //if (Directory.Exists(imagePath))
            //{
            //    DirectoryInfo directory = new DirectoryInfo(imagePath);
            //    FileInfo[] files = directory.GetFiles("*.jpg", SearchOption.AllDirectories);
            //    var allfiles = files.Select(s => s.Name);
            //    var allT24Files = allfiles.Where(x => x.Contains(ejIdCheck)).Select(x => x);
            //    foreach (var T24File in allT24Files)
            //    {
            //        urlsT24.Add(T24File.ToString());
            //    }
            //}
            //return urlsT24;
            List<string> urlsT24 = new List<string>();

            string imagePath = System.Configuration.ConfigurationManager.AppSettings["DownloadImageLocation"];
            string ejIdCheck = string.IsNullOrEmpty(ejId) ? "No_Image" : System.Configuration.ConfigurationManager.AppSettings["EJIdFormat"];

            //string ssName = System.Configuration.ConfigurationManager.AppSettings["EJIdFormat"].Trim().ToString();
            if (Directory.Exists(imagePath))
            {
                DirectoryInfo directory = new DirectoryInfo(imagePath);
                FileInfo[] files = directory.GetFiles(ejIdCheck + "??????????" + ejId + "*.jpg", SearchOption.AllDirectories);

                var allfiles = files.Select(s => s.FullName);
                //var allT24Files = allfiles.Where(x => x.Contains(ejIdCheck)).Select(x => x);
                foreach (var T24File in allfiles)
                {
                    urlsT24.Add(T24File.ToString().Replace(imagePath, ""));
                }
            }



            return urlsT24;
        }
        #endregion-----------------------------GetTransactionWiseT24URL Ends Here------------------------------------------------

        #region--------------------------------GetTransactionWiseWCURL----------------------------------------------------------
        public List<string> GetWCImageUrls(string wcId)
        {
            //List<string> urlsWc = new List<string>();
            //string imagePath = System.Configuration.ConfigurationManager.AppSettings["DownloadImageLocation"];
            //string wcIdCheck = string.IsNullOrEmpty(wcId) ? "No_Image" : System.Configuration.ConfigurationManager.AppSettings["WCIdFormat"] + wcId;
            //if (Directory.Exists(imagePath))
            //{
            //    DirectoryInfo directory = new DirectoryInfo(imagePath);
            //    FileInfo[] files = directory.GetFiles("*.jpg", SearchOption.AllDirectories);
            //    var allfiles = files.Select(s => s.Name);
            //    var allwcFiles = allfiles.Where(x => x.Contains(wcIdCheck)).Select(x => x);
            //    foreach (var wcFile in allwcFiles)
            //    {
            //        urlsWc.Add(wcFile.ToString());
            //    }
            //}
            //return urlsWc;
            List<string> urlsWc = new List<string>();
                string imagePath = System.Configuration.ConfigurationManager.AppSettings["DownloadImageLocation"];
                string wcIdCheck = string.IsNullOrEmpty(wcId) ? "No_Image" : System.Configuration.ConfigurationManager.AppSettings["WCIdFormat"];
                if (Directory.Exists(imagePath))
                {
                    DirectoryInfo directory = new DirectoryInfo(imagePath);
                    FileInfo[] files = directory.GetFiles(wcIdCheck + wcId + "*.jpg", SearchOption.AllDirectories);
                    var allfiles = files.Select(s => s.FullName);
                    //var allwcFiles = allfiles.Where(x => x.Contains(wcIdCheck)).Select(x => x);
                    foreach (var wcFile in allfiles)
                    {
                        urlsWc.Add(wcFile.ToString().Replace(imagePath, ""));
                    }
                }            
            return urlsWc;
        }
        #endregion-----------------------------GetTransactionWiseWCURL Ends Here------------------------------------------------

        #region--------------------------------Decrypt Function Starts Here-----------------------------------------------------
        public static string Decrypt(string input, string key)

        {

            byte[] inputArray = Convert.FromBase64String(input);

            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();

            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);

            tripleDES.Mode = CipherMode.ECB;

            tripleDES.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tripleDES.CreateDecryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);

            tripleDES.Clear();

            return UTF8Encoding.UTF8.GetString(resultArray);

        }
        #endregion-----------------------------Decrypt Function Ends Here-------------------------------------------------------
        #region-------------------------------Function to make maker or checker id null ----------------------------------------
        public void MakerOrCheckerBack(string ID, string solvedby, string usertype)
        {
            CreateFile(folderPath, "The Method is MakerOrCheckerBack and SP is UDSP_Maker_CheckerBack parameter of Date is : " + System.DateTime.Now + "\n ID = " + ID + "\n solvedby = " + solvedby + "\n usertype = " + usertype);

            if (!string.IsNullOrEmpty(ID) && !string.IsNullOrEmpty(solvedby) && ID != "0")
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("UDSP_Maker_CheckerBack", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@SolvedBy", solvedby);
                    cmd.Parameters.AddWithValue("@UserType", solvedby);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }

        }
        #endregion----------------------------Function to make maker or checker id null ends here-------------------------------

        #region ---- Lock Transaction ----
        public bool LockTransaction(string ID, string userName, string userType)
        {
            bool id=false;
            if (!string.IsNullOrEmpty(ID) && !string.IsNullOrEmpty(userName) && ID != "0" && !string.IsNullOrEmpty(userType))
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("UDSP_LockReactiveTransaction", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Param_ID", ID);
                    cmd.Parameters.AddWithValue("@Param_User", userName);
                    cmd.Parameters.AddWithValue("@Param_Usertype", userType);
                    //cmd.Parameters.Add("",SqlDbType.Bit).Direction.
                    SqlParameter parm = new SqlParameter("@Locked", SqlDbType.Bit);
                    parm.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(parm);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    da.Fill(ds);
                    id = Convert.ToBoolean(parm.Value);
                }

            }
            return id;
        }

        #endregion

        #region ------Lock Proactive Transaction ------

        public bool LockProactiveTransaction(string ID, string userName, string userType)
        {
            bool id = false;
            if (!string.IsNullOrEmpty(ID) && !string.IsNullOrEmpty(userName) && ID != "0" && !string.IsNullOrEmpty(userType))
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("UDSP_LockProactiveTransaction", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Param_ID", ID);
                    cmd.Parameters.AddWithValue("@Param_User", userName);
                    cmd.Parameters.AddWithValue("@Param_Usertype", userType);
                    //cmd.Parameters.Add("",SqlDbType.Bit).Direction.
                    SqlParameter parm = new SqlParameter("@Locked", SqlDbType.Bit);
                    parm.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(parm);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    da.Fill(ds);
                    id = Convert.ToBoolean(parm.Value);
                }

            }
            return id;
        }

        #endregion


        //#region----------------------------------Machine Logger--------------------------------------------------------

        //public int CheckMachineAvailable(string Machinename)
        //{
        //    int check = 0;
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        DataSet ds = new DataSet();

        //        SqlCommand cmd = new SqlCommand("UDSP_Check_Machine_Availability", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@MachineName", Machinename);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(ds);
        //        if (ds != null)
        //        {
        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {

        //                foreach (DataRow dr in ds.Tables[0].Rows)
        //                {
        //                    check = Convert.ToInt32(dr["IsAvailable"]);
        //                }
        //            }
        //        }
        //    }
        //    return check;
        //}
        //public void MachineLogout(string Machinename)
        //{
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        DataSet ds = new DataSet();
        //        SqlCommand cmd = new SqlCommand("UDSP_Machine_Logout", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@MachineName", Machinename);
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }

        //}
        //#endregion-----------------------------------------------------------------------------------------------------
    }
}