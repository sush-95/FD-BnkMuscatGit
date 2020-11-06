using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankMuscat
{
    public class ReactiveUserDetails
    {        
        public string    FeedbackId          { get; set; }
        public DateTime? Incident_Date       { get; set; }
        public DateTime? EntryTime           { get; set; }
        public string    Comment_Eng         { get; set; }
        public string    BotWC_Status        { get; set; }
        public string    Bot_Remarks         { get; set; }
        public string    User_Action         { get; set; }
        public string    User_Recommendation { get; set; }
        public string    Maker_Input         { get; set; }
        public string    MakerComment        { get; set; }
        public string    Checker_Input       { get; set; }
        public string    CheckerComment      { get; set; }
        public string    Final_Status        { get; set; }

    }
}
