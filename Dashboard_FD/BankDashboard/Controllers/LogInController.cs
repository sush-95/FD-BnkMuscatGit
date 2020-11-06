using BankDashboard.Common;

using BankMuscat;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankDashboard.LogFile;
using System.Text.RegularExpressions;

namespace BankDashboard.Controllers
{
    public class LogInController : Controller
    {
        // GET: LogIn
        public ActionResult LogIn()
        {

            //string ErrorMsg = string.Empty; string Action = string.Empty; string cntrlr = string.Empty, groupname = string.Empty;
            //if (Session["User"] != null)
            //{
            //    var userSession = ((tbl_UserDetail)Session["User"]);
            //    if (userSession.UserGroup.ToLower().Equals(Constants.UserGroups.Admin.ToLower()))
            //    {
            //        Action = "index"; cntrlr = "Admin";
            //    }
            //    else if (userSession.UserGroup.ToLower().Equals(Constants.UserGroups.ParameterManager.ToLower()))
            //    {
            //        Action = "index"; cntrlr = "Admin";
            //    }
            //    else if (userSession.UserGroup.ToLower().Equals(Constants.UserGroups.Checker.ToLower()) || userSession.UserGroup.ToLower().Equals(Constants.UserGroups.Maker.ToLower()))
            //    {
            //        Action = "index"; cntrlr = "User";
            //    }
            //    return RedirectToAction(Action, cntrlr);
            //}
            //else
            //{

            //    Session["LoginKey"] = MvcHelper.generateRandomKey(31).ToUpper() + "=";
            //    //WriteToLogFile.writeMessage("Key Used for Encryption and Decryption " + Session["LoginKey"].ToString());

            //    TempData["keyData"] = Session["LoginKey"];

            //}


            return View();
        }
        [HttpPost]
        //public ActionResult LogIn(string uname, string pwd)        
        //{
        //    //if (string.IsNullOrEmpty(pwd))
        //    return RedirectToAction("LogIn", "LogIn");
        //if (Session["LoginKey"] == null)
        //{

        //}

        //pwd = pwd.Trim();

        ////pwd = pwd.Replace("\"", string.Empty);
        //bool logincheck = false;
        //string key = Session["LoginKey"].ToString();

        //pwd = MvcHelper.DecryptToBytesUsingCBC(Convert.FromBase64String(pwd), key);


        //pwd = pwd.Trim();


        //string ErrorMsg = string.Empty; string Action = string.Empty; string cntrlr = string.Empty, groupname = string.Empty, pages = string.Empty;

        //tbl_UserDetail user = new tbl_UserDetail();
        //UserTransactionData utobj = new UserTransactionData();


        //user = MvcHelper.GetUser(uname, pwd, ref pages);

        //if (user != null)
        //{
        //    logincheck = true;
        //    groupname = user.UserGroup;
        //}

        //if (logincheck)
        //{

        //    if (!string.IsNullOrEmpty(groupname))
        //    {
        //        user = new tbl_UserDetail()
        //        {
        //            AccountName = uname,
        //            UserGroup = groupname,
        //        };
        //        user.GroupPages = MvcHelper.GetGroupPages(groupname);


        //        bool check = MvcHelper.CheckMachine(user.AccountName.Trim().ToString());


        //        if (check)
        //        {
        //            Session["USerName"] = user.AccountName.Trim().ToString();                        
        //            MvcHelper.SaveUser(user);
        //            Action = "Index"; cntrlr = "Home";
        //            //WriteToLogFile.writeMessage("Machine is available User will be Redirected to -- Action = index cntrlr = Dashboard");
        //        }
        //        else
        //        {
        //            //WriteToLogFile.writeMessage("Sorry...!!! Multiple user login is not allowed.");
        //            ErrorMsg = "Sorry...!!! Multiple user login is not allowed.";
        //        }
        //    }
        //    else
        //    {
        //        //WriteToLogFile.writeMessage("User type is not authorized for the dashboard...!");

        //        ErrorMsg = "User type is not authorized for the dashboard...!";
        //    }
        //}
        //else
        //{
        //    //WriteToLogFile.writeMessage("Wrong credential...!");
        //    ErrorMsg = "Wrong credential...!";
        //}

        //if (!string.IsNullOrEmpty(ErrorMsg))
        //{
        //    //WriteToLogFile.writeMessage("Error msg" +ErrorMsg +"Redirecting to [GET] login page");
        //    TempData["invalidmsg"] = ErrorMsg;

        //    return RedirectToAction("LogIn");
        //}
        //else
        //{
        //    Session["User"] = user;
        //    //WriteToLogFile.writeMessage("[HttpPost]Login  Ended");
        //    //WriteToLogFile.writeMessage("Redirecting to -- Action = +"+Action.ToString() +" cntrlr = " + cntrlr.ToString());


        //    return RedirectToAction(Action, cntrlr);
        //}

        //   }

        //public JsonResult EncryptPassword(string pwd)
        //{

        //    //WriteToLogFile.writeMessage("EncryptedPassword [Started]");

        //    string EncryptKey = Convert.ToString(ConfigurationManager.AppSettings["DecryptKey"]);

        //    //WriteToLogFile.writeMessage("EncryptKey = "+ EncryptKey.ToString() );

        //    string encrypteddata = MvcHelper.CodeEncrypt(pwd.Trim(), EncryptKey.Trim());

        //    //WriteToLogFile.writeMessage("encrypteddata = " + encrypteddata.ToString());

        //    //WriteToLogFile.writeMessage("EncryptedPassword [Ended]");

        //    return Json(encrypteddata, JsonRequestBehavior.AllowGet);

        //}
        public ActionResult LogOout()
        {
            return RedirectToAction("LogIn");
        }
        public ActionResult Logout()
        {
            ////UserTransactionData utobj = new UserTransactionData();
            ////utobj.MachineLogout(Session["Machine"].ToString());
            //WriteToLogFile.writeMessage("Logout [Started] " +"Machine id ="+ ((Session["USerName"] == null)?"null": Session["USerName"].ToString()));
            ////Session["USerName"]
            ////if (Session["Machine"] != null)
            if (Session["USerName"] != null)
            {
                // MvcHelper.MachineLogout(Session["USerName"].ToString());
            }
            Session.Abandon();
            try
            {
                //WriteToLogFile.writeMessage("set ASP.NET_SessionID to null");
                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                //WriteToLogFile.writeMessage("Successfully Generated New ID");
            }
            catch (Exception e)
            {
                //WriteToLogFile.writeMessage("Error Occured While Generating New ID ERROR = " + e.Message.ToString());
            }
            //WriteToLogFile.writeMessage("Logout [Ended]");
            return RedirectToAction("LogIn");
        }
    }
}