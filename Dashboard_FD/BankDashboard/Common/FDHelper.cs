using BankDashboard.ModelFd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static BankDashboard.Common.FDViewModelClass;

namespace BankDashboard.Common
{
    public class FDHelper
    {
        public static ListOFproperty getAllPropertyList()
        {
            ListOFproperty obj = new ListOFproperty();
            try
            {
                DBFD db = new DBFD();
                var FdList = db.FDRequests.ToList();
                obj.ListofFDCreated = FdList.Select(x => x.FDCreated).Distinct().ToList();
                obj.ListOfMailsent = FdList.Select(x => x.MailSent).Distinct().ToList();
                obj.ListOfSMSSent = FdList.Select(x => x.SMSSent).Distinct().ToList();
                obj.ListMarkedStatusInMB = FdList.Select(x => x.MarkedStatusInMB).Distinct().ToList();
                obj.ListofTermLength = FdList.Select(x => x.TermLength).Distinct().ToList();
            }
            catch { }
            return obj;
        }
        public static List<FDRequest> GetFilteredLIst(ReportView filter)
        {
            List<FDRequest> newfdlist = new List<FDRequest>();
            try
            {
                DBFD db = new DBFD();
                newfdlist = db.FDRequests.SqlQuery(GetQuery(filter)).ToList();
            }
            catch (Exception ex) { }
            return newfdlist;
        }
        public static string GetQuery(FDViewModelClass.ReportView filter)
        {
            string query = "select * from FDRequests where ";
            int qrylength = query.Length;
            if (filter.CustomerNumber != null && !string.IsNullOrEmpty(filter.CustomerNumber.Trim()))
            {
                query = query + "CustomerNumber like '%" + filter.CustomerNumber.Trim() + "%'";
            }
            if (filter.TermLength != null && !string.IsNullOrEmpty(filter.TermLength.Trim()))
            {
                query = query + ((query.Length > qrylength + 3) ? " and TermLength='" + filter.TermLength.Trim() + "'" : " TermLength='" + filter.TermLength.Trim() + "'");
            }
            if (filter.RequestDate != null && !string.IsNullOrEmpty(filter.RequestDate.Trim()))
            {
                string date = Convert.ToDateTime(filter.RequestDate).ToString("yyyy-MM-dd");
                query = query + ((query.Length > qrylength + 3) ? " and RequestDate like '%" + date + "%'" : " RequestDate like '%" + date + "%'");
            }
            if (filter.FDCreated != null && !string.IsNullOrEmpty(filter.FDCreated.Trim()))
            {
                query = query + ((query.Length > qrylength + 3) ? " and FDCreated='" + filter.FDCreated.Trim() + "'" : "  FDCreated='" + filter.FDCreated + "'");
            }
            if (filter.MailSent != null && !string.IsNullOrEmpty(filter.MailSent.Trim()))
            {
                query = query + ((query.Length > qrylength + 3) ? " and MailSent='" + filter.MailSent.Trim() + "'" : " MailSent='" + filter.MailSent + "'");
            }
            if (filter.SMSSent != null && filter.SMSSent != null && !string.IsNullOrEmpty(filter.SMSSent.Trim()))
            {
                query = query + ((query.Length > qrylength + 3) ? " and SMSSent='" + filter.SMSSent.Trim() + "'" : " SMSSent='" + filter.SMSSent + "'");
            }
            if (filter.MarkedStatusInMB != null && !string.IsNullOrEmpty(filter.MarkedStatusInMB.Trim()))
            {
                query = query + ((query.Length > qrylength + 3) ? " and MarkedStatusInMB='" + filter.MarkedStatusInMB.Trim() + "'" : " MarkedStatusInMB='" + filter.MarkedStatusInMB.Trim() + "'");
            }
            if (filter.FromDate != null && !string.IsNullOrEmpty(filter.FromDate) && filter.ToDate != null && !string.IsNullOrEmpty(filter.ToDate))
            {
                DateTime fdate = Convert.ToDateTime(filter.FromDate), todate = Convert.ToDateTime(filter.ToDate).AddHours(23);
                query = query + ((query.Length > qrylength + 3) ? " and BOTEntryDate between '" + fdate.ToString() + "' and '" + todate.ToString() + "'" :
                    "  BOTEntryDate between '" + fdate.ToString() + "' and '" + todate.ToString() + "'");
            }
            return query + ";";
        }
    }
}