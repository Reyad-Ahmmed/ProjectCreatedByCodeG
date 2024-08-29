using System;
using System.Text;
using System.Web;
//using System.Web.SessionState;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;
using Primary2020;
//using System.Windows.Forms;

namespace GSACaughtException
{
    /// <summary>
    /// Class to handle errors
    /// </summary>
    public class GSACaughtException : ApplicationException
    {
        #region private variables
        private string _strMethodName;
        private string _strClientPCId;
        private string _strUserId;
        private string _strErrorMessage;
        //private System.Web.UI.Page _callingWebPage;
        private string _strAppName;
        private long _lngSeverity;
        private long _lngApplicationId;
        private string _strCEStackTrace;
        #endregion

        #region public properties
        public long Severity
        {
            get { return _lngSeverity; }
            set { _lngSeverity = value; }
        }
        public long ApplicationId
        {
            get { return _lngApplicationId; }
            set { _lngApplicationId = value; }
        }
        public string MethodName
        {
            get { return _strMethodName; }
            set { _strMethodName = value; }
        }
        public string ClientPCId
        {
            get { return _strClientPCId; }
            set { _strClientPCId = value; }
        }
        public string UserId
        {
            get { return _strUserId; }
            set { _strUserId = value; }
        }
        public string ErrorMessage
        {
            get { return _strErrorMessage; }
            set { _strErrorMessage = value; }
        }
        //public System.Web.UI.Page CallingWebPage
        //{
        //    get { return _callingWebPage; }
        //    set { _callingWebPage = value; }
        //}
        public string AppName
        {
            get { return _strAppName; }
            set { _strAppName = value; }
        }
        public override string Message
        {
            get { return _strErrorMessage; }
        }
        public string CEStackTrace
        {
            get { return _strCEStackTrace; }
            set { _strCEStackTrace = value; }
        }
        #endregion

        #region public enum
        public enum WriteLogOption
        {
            LogToEventViewer = 1,
            LogToFile = 2
        }
        public enum GlobalSeverityLevel
        {
            LowSeverity = 1,
            ModerateSeverity = 2,
            HighSeverity = 3,
            ExtremelySevere = 4,
            ShowErrorPage = 5
        }
        public enum ErrorOption
        {
            WriteToEmail = 1,
            WriteToLog = 2,
            WriteToTable = 3
        }
        #endregion

        #region Constructors
        public GSACaughtException()
        {
        }
        public GSACaughtException(string msg, PrimaryItem pItem, string strMethodName)
        {
            this.ErrorMessage = msg;
            this.MethodName = strMethodName;
            this.Source = pItem.AppName + " -> " + pItem.ClassName + " -> " + MethodName;
            this.AppName = pItem.AppName;
        }
        public GSACaughtException(Exception ex, PrimaryItem pItem, string strMethodName)
        {
            this.ErrorMessage = ex.Message;
            this.MethodName = strMethodName;
            this.Source = pItem.AppName + " -> " + pItem.ClassName + " -> " + MethodName;
            this.AppName = pItem.AppName;
        }
        public GSACaughtException(Exception ex, string strMethodName, long lngSeverity)
        {
            //string errorPage = ConfigurationManager.AppSettings["ErrorPage"].ToString(); 
            //this.ErrorMessage = ex.Message + " - " + HttpContext.Current.Request.RawUrl;
            //this.MethodName = strMethodName;
            //this.Severity = lngSeverity;
            //this.CEStackTrace = ex.StackTrace;
            //try { this.ClientPCId = ConfigurationManager.AppSettings["ClientPCId"].ToString(); }
            //catch { this.ClientPCId = "undefined"; }
            //try { this.ApplicationId = long.Parse(ConfigurationManager.AppSettings["ApplicationId"].ToString()); }
            //catch { this.ApplicationId = -1; }
            //try { this.AppName = ConfigurationManager.AppSettings["ApplicationName"].ToString(); }
            //catch { this.AppName = "undefined"; }
            //this.Source = ex.Source;
            //try
            //{
            //    SendEmailBasedOnSeverity(false, lngSeverity);
            //    this.WriteToTable(this);
            //    if (lngSeverity == (long)GlobalSeverityLevel.ShowErrorPage)
            //    {
            //        HttpContext.Current.Session.Add("exception", this);
            //        HttpContext.Current.Response.Redirect(errorPage, true);
            //    }
            //}
            //catch (Exception ex1)
            //{
            //    string s = ex1.Message;
            //}
        }

        //public GSACaughtException(Exception ex, System.Web.UI.Page page, string strMethodName, long lngSeverity)
        //{
        //    this.ErrorMessage = ex.Message + " - " + page.Request.RawUrl;
        //    this.MethodName = strMethodName;
        //    this.Severity = lngSeverity;
        //    this.CEStackTrace = ex.StackTrace;
        //    string errorPage = ConfigurationManager.AppSettings["ErrorPage"].ToString(); 
        //    try { this.ApplicationId = long.Parse(ConfigurationManager.AppSettings["ApplicationId"].ToString()); }
        //    catch { this.ApplicationId = -1; }

        //    try { this.ClientPCId = ConfigurationManager.AppSettings["ClientPCId"].ToString(); }
        //    catch { this.ClientPCId = page.Server.MachineName; }
            
        //    try { this.AppName = ConfigurationManager.AppSettings["ApplicationName"].ToString(); }
        //    catch { this.AppName = page.Request.ApplicationPath; }

        //    //this.AppName = Application.ProductName;
        //    this.Source = page.Request.RawUrl;
        //    try
        //    {
        //        SendEmailBasedOnSeverity(false, lngSeverity);
        //        this.WriteToTable(this);
        //        if (lngSeverity == (long)GlobalSeverityLevel.ShowErrorPage)
        //        {
        //            page.Session.Add("exception", this);
        //            page.Response.Redirect(errorPage, true);
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}


        /// <summary>
        /// sbm - 2/28/08
        /// Function to send email based on application severity
        /// value comes from applications table
        /// </summary>
        /// <param name="blnEmailSent"></param>
        private void SendEmailBasedOnSeverity(bool blnEmailSent, long lngSeverity)
        {
            try
            {
                if (lngSeverity >= (int)GlobalSeverityLevel.ExtremelySevere)
                {
                    //check blnEmailSent to make sure we do not send the email twice
                    //which may happen if error is marked for email and app is marked
                    //for email
                    if (blnEmailSent == false)
                    {
                        WriteToEmail(this);
                    }
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }
        #endregion

        private void WriteToEmail()
        {
            StringBuilder EmailBody = new StringBuilder();
            EmailBody.AppendLine("Message: " + this.ErrorMessage + "<br>");
            EmailBody.AppendLine("Method: " + this.MethodName + "<br>");
            EmailBody.AppendLine("Source: " + this.Source + "<br>");
            EmailBody.AppendLine("URL: " + this.AppName + "<br>");
            string EmailSubject = "Error on page " + this.AppName;
            string EmailTo = ConfigurationManager.AppSettings["ErrorEmailTo"];
            string EmailFrom = ConfigurationManager.AppSettings["ErrorEmailFrom"];
            SendEmail EMail = new SendEmail();
            EMail.SendEmailMsg(EmailBody.ToString(), EmailSubject, EmailTo, EmailFrom);
        }
        private void WriteToEmail(GSACaughtException ce)
        {
            StringBuilder EmailBody = new StringBuilder();
            EmailBody.AppendLine("Message: " + this.ErrorMessage);
            EmailBody.AppendLine("Method: " + this.MethodName);
            EmailBody.AppendLine("Source: " + this.Source);
            //EmailBody.AppendLine("URL: " + this.AppName);
            string EmailSubject = "Error on page " + this.AppName;
            string EmailTo = ConfigurationManager.AppSettings["ErrorEmailTo"];
            string EmailFrom = ConfigurationManager.AppSettings["ErrorEmailFrom"];
            SendEmail EMail = new SendEmail();

            EMail.SendEmailMsg(EmailBody.ToString(), EmailSubject, EmailTo, EmailFrom);
        }
        private void WriteToEmail(string msg)
        {
            StringBuilder EmailBody = new StringBuilder();
            EmailBody.AppendLine("Message: " + msg);
            string EmailTo = ConfigurationManager.AppSettings["ErrorEmailTo"];
            string EmailFrom = ConfigurationManager.AppSettings["ErrorEmailFrom"];
            string EmailSubject = "Error on page " + this.AppName;
            SendEmail EMail = new SendEmail();

            EMail.SendEmailMsg(EmailBody.ToString(), EmailSubject, EmailTo, EmailFrom);
        }
        private void WriteToLog()
        {
            FileStream fs = new FileStream(ConfigurationManager.AppSettings["ErrorLogFile"], FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("Date: " + DateTime.Now);
            sw.WriteLine("Message: " + this.ErrorMessage);
            sw.WriteLine("Method: " + this.MethodName);
            sw.WriteLine("Source: " + this.Source);
            sw.WriteLine("URL: " + this.AppName);
            sw.WriteLine();
            sw.Close();
            fs.Close();
        }
        private void WriteToTable()
        {
        }
        private void RedirectToErrorPage(bool blnSendToError)
        {
            if (blnSendToError)
            {
                string errorPage = ConfigurationManager.AppSettings["ErrorPage"].ToString(); 
                string strParam = "&msg=" + this.ErrorMessage;
                strParam += "&meth=" + this.MethodName;
                strParam += "&src=" + this.Source;
                strParam += "&app=" + this.AppName;
                strParam = strParam.Replace("\r", "");
                strParam = strParam.Replace("\n", "");
                //System.Web.HttpContext.Current.Response.Redirect(errorPage + strParam, false);
                //System.Web.HttpContext.Current.Response.Flush();
            }
        }


        public void SetAttributes(string strMethodName, string strAppName, string strSource)
        {
            this.MethodName = strMethodName;
            this.Source = strSource;
            this.AppName = strAppName;
        }

        public void log(WriteLogOption lOption)
        {
            string strErrorMsg;
            AppSettings app = new AppSettings();
            app.LoadSettings();
            switch (lOption)
            {
                case WriteLogOption.LogToEventViewer:
                    strErrorMsg = this.getErrorMessage();
                    WriteToEventViewer(strErrorMsg, app);
                    break;
                case WriteLogOption.LogToFile:
                    strErrorMsg = this.getErrorMessage();
                    WriteToLog(strErrorMsg, app);
                    break;
                default:
                    break;
            }
        }

        private string getErrorMessage()
        {
            try
            {
                StringBuilder sbd = new StringBuilder();
                sbd.Append("UserId: " + this.UserId + "\n");
                sbd.Append("DateTime: " + System.DateTime.Now.ToString() + "\n");
                sbd.Append("Source: " + this.Source + "\n");
                sbd.Append("Error Message: " + this.ErrorMessage + "\n");
                return sbd.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void WriteToLog(string strMessage, AppSettings app)
        {
            try
            {
                ServerObject so = new ServerObject();
                so.WriteToLog(strMessage, app.LogDir, app.LogFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void WriteToEventViewer(string strMessage, AppSettings app)
        {
            try
            {
                EventLogMgr el = new EventLogMgr();
                el.write(System.Diagnostics.EventLogEntryType.Error, this.AppName, this.Source, strMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// sbm - 2/28/08
        /// Writes the error to the errorlog table
        /// </summary>
        /// <param name="ce"></param>
        private void WriteToTable(GSACaughtException ce)
        {
            try
            {
                //ErrorLogItem eItem = new ErrorLogItem();
                //eItem.ErrorLogId = -1;//-1 is used to identify new insert
                //eItem.ErrorMsg = ce.Message;
                //eItem.MethodName = ce.MethodName;
                //eItem.Severity = ce.Severity;
                //eItem.StackTrace = ce.CEStackTrace;
                //try { eItem.ClientPCId = ConfigurationManager.AppSettings["ClientPCId"].ToString(); }
                //catch { eItem.ClientPCId = "undefined"; }
                //try { eItem.ApplicationId = long.Parse(ConfigurationManager.AppSettings["ApplicationId"].ToString()); }
                //catch { eItem.ApplicationId = ce.ApplicationId; }
                //eItem.ApplicationName = ce.AppName;
                //eItem.Save(ErrorLogItem.SaveOption.SaveRow);
            }
            catch
            {
                WriteToEmail("WriteToTable failed");
            }
        }
    }
}
