using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankDashboard.Common
{
    public class FDViewModelClass
    {
        public class ReportView
        {
            public int ID { get; set; }

            public string CustomerNumber { get; set; }

            public string TermLength { get; set; }

            public string ProfitPaymentFrequency { get; set; }

            public string DepositAmount { get; set; }

            public string RequestDate { get; set; }

            public string FDCreated { get; set; }

            public string MailSent { get; set; }

            public string MarkedStatusInMB { get; set; }

            public string SMSSent { get; set; }

            public DateTime? BOTEntryDate { get; set; }

            public string MaturityDate { get; set; }

            public string FDBookingDate { get; set; }

            public string FDAccNo { get; set; }

            public string PaymentMethod { get; set; }

            public string Status { get; set; }

            public string FromDate { get; set; }

            public string ToDate { get; set; }


        }
        public class ListOFproperty
        {
            public List<string> ListofTermLength { get; set; }

            public List<string> ListOfMailsent { get; set; }

            public List<string> ListOfSMSSent { get; set; }

            public List<string> ListofFDCreated { get; set; }

            public List<string> ListMarkedStatusInMB { get; set; }
        }

    }
}