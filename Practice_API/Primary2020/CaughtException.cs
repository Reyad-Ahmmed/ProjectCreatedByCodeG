using System;
using System.Text;
using System.Web;
//using System.Web.SessionState;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;

namespace Primary2020
{
    /// <summary>
    /// Class to handle errors
    /// </summary>
    public class CaughtException : ApplicationException
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

        Exception _codeException;

        
        #endregion

        #region public properties
        public Exception CodeException
        {
            get { return _codeException; }
            set { _codeException = value; }
        }
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
            ExtremelySevere = 4
        }
        public enum ErrorOption
        {
            WriteToEmail = 1,
            WriteToLog = 2,
            WriteToTable = 3
        }
        #endregion

        #region Constructors
        public CaughtException()
        {
        }
        public CaughtException(Exception ex)
        {
            this.ErrorMessage = ex.Message;
        }
        public CaughtException(Exception ex, PrimaryItem pItem, string strMethodName)
        {
            this.ErrorMessage = ex.Message;
            this.MethodName = strMethodName;
            this.Source = pItem.AppName + " -> " + pItem.ClassName + " -> " + MethodName;
            this.AppName = pItem.AppName;
        }
        //public CaughtException(Exception ex, UserControl ctl, string strMethodName, bool blnSendToError)
        //{
        //    this.ErrorMessage = ex.Message;
        //    this.MethodName = strMethodName;
        //    this.Source = ctl.ToString();
        //    this.AppName = ctl.Request.RawUrl;
        //    RedirectToErrorPage(blnSendToError);
        //}
        //public CaughtException(Exception ex, UserControl ctl, string strMethodName, bool blnSendToError, ErrorOption[] eOptions)
        //{
        //    this.ErrorMessage = ex.Message;
        //    this.MethodName = strMethodName;
        //    this.Source = ctl.ToString();
        //    this.AppName = ctl.Request.RawUrl;

        //    try
        //    {
        //        foreach (ErrorOption eOption in eOptions)
        //        {
        //            switch (eOption)
        //            {
        //                case ErrorOption.WriteToEmail:
        //                    WriteToEmail();
        //                    break;
        //                case ErrorOption.WriteToLog:
        //                    WriteToLog();
        //                    break;
        //                case ErrorOption.WriteToTable:
        //                    WriteToTable();
        //                    break;
        //            }
        //        }
        //    }
        //    catch
        //    {

        //        //throw;
        //    }
        //    RedirectToErrorPage(blnSendToError);
        //}
        public CaughtException(string msg, PrimaryItem pItem, string strMethodName)
        {
            this.ErrorMessage = msg;
            this.MethodName = strMethodName;
            this.Source = pItem.AppName + " -> " + pItem.ClassName + " -> " + MethodName;
            this.AppName = pItem.AppName;
        }
        //public CaughtException(Exception ex, string strMethodName, long lngSeverity, ErrorOption[] eOptions)
        //{
        //    bool blnEmailSent = false;
        //    this.ErrorMessage = ex.Message;
        //    this.MethodName = strMethodName;
        //    this.Severity = lngSeverity;
        //    this.ClientPCId = ConfigurationManager.AppSettings["ClientPCId"].ToString();
        //    this.ApplicationId = long.Parse(ConfigurationManager.AppSettings["ApplicationId"].ToString());
        //    try
        //    {
        //        foreach (ErrorOption eOption in eOptions)
        //        {
        //            switch (eOption)
        //            {
        //                case ErrorOption.WriteToEmail:
        //                    WriteToEmail();
        //                    blnEmailSent = true;
        //                    break;
        //                case ErrorOption.WriteToLog:
        //                    WriteToLog();
        //                    break;
        //                case ErrorOption.WriteToTable:
        //                    WriteToTable(this);
        //                    break;
        //            }
        //        }
        //        //check to see if application needs to email programmer on every error
        //        //globalseveritylevel = ExtremelySevere
        //        ApplicationsItem aItem = new ApplicationsItem();
        //        aItem.ApplicationsId = this.ApplicationId;
        //        aItem.Load(ApplicationsItem.LoadOption.LoadById);
        //        if (aItem.Found)
        //        {
        //            if (aItem.GlobalSeverity == (int)GlobalSeverityLevel.ExtremelySevere)
        //            {
        //                //check blnEmailSent to make sure we do not send the email twice
        //                //which may happen if error is marked for email and app is marked
        //                //for email
        //                if (blnEmailSent == false)
        //                {
        //                    WriteToEmail(this);
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}
        public CaughtException(Exception ex, string strMethodName, long lngSeverity)
        {
            this.CodeException = ex;
            this.ErrorMessage = ex.Message;
            this.MethodName = strMethodName;
            this.Severity = lngSeverity;
            this.ClientPCId = ConfigurationManager.AppSettings["ClientPCId"].ToString();
            this.ApplicationId = long.Parse(ConfigurationManager.AppSettings["ApplicationId"].ToString());
            try
            {
                //SendEmailBasedOnSeverity(false);
                this.WriteToTable(this);
            }
            catch
            {
            }
        }
        public CaughtException(Exception ex, string appName, string strMethodName, long lngSeverity)
        {
            this.CodeException = ex;
            this.ErrorMessage = ex.Message;
            this.MethodName = strMethodName;
            this.Severity = lngSeverity;
            this.ClientPCId = ConfigurationManager.AppSettings["ClientPCId"].ToString();
            this.AppName = appName;
            try
            {
                //SendEmailBasedOnSeverity(false);
                this.WriteToTable(this);
            }
            catch
            {
            }
        }

        /// <summary>
        /// sbm - 2/28/08
        /// Function to send email based on application severity
        /// value comes from applications table
        /// </summary>
        /// <param name="blnEmailSent"></param>
        private void SendEmailBasedOnSeverity(bool blnEmailSent)
        {
            ////check to see if application needs to email programmer on every error
            ////globalseveritylevel = ExtremelySevere
            //ApplicationsItem aItem = new ApplicationsItem();
            //aItem.ApplicationsId = this.ApplicationId;
            //aItem.Load(ApplicationsItem.LoadOption.LoadById);
            //if (aItem.Found)
            //{
            //    if (aItem.GlobalSeverity == (int)GlobalSeverityLevel.ExtremelySevere)
            //    {
            //        //check blnEmailSent to make sure we do not send the email twice
            //        //which may happen if error is marked for email and app is marked
            //        //for email
            //        if (blnEmailSent == false)
            //        {
            //            WriteToEmail(this);
            //        }
            //    }
            //}
        }
        #endregion

        private void WriteToEmail()
        {
            StringBuilder EmailBody = new StringBuilder();
            EmailBody.AppendLine("Message: " + this.ErrorMessage);
            EmailBody.AppendLine("Method: " + this.MethodName);
            EmailBody.AppendLine("Source: " + this.Source);
            EmailBody.AppendLine("URL: " + this.AppName);
            string EmailSubject = "Error on page " + this.AppName;
            string EmailTo = ConfigurationManager.AppSettings["ErrorEmailTo"];
            string EmailFrom = ConfigurationManager.AppSettings["ErrorEmailFrom"];
            SendEmail EMail = new SendEmail();
            EMail.SendEmailMsg(EmailBody.ToString(), EmailSubject, EmailTo, EmailFrom);
        }
        private void WriteToEmail(CaughtException ce)
        {
            StringBuilder EmailBody = new StringBuilder();
            EmailBody.AppendLine("Message: " + this.ErrorMessage);
            EmailBody.AppendLine("Method: " + this.MethodName);
            EmailBody.AppendLine("Source: " + this.Source);
            EmailBody.AppendLine("URL: " + this.AppName);
            string EmailSubject = "Error on page " + this.AppName;
            string EmailTo = ConfigurationManager.AppSettings["ErrorEmailTo"];
            string EmailFrom = ConfigurationManager.AppSettings["ErrorEmailFrom"];
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
                string strParam = "&msg=" + this.ErrorMessage;
                strParam += "&meth=" + this.MethodName;
                strParam += "&src=" + this.Source;
                strParam += "&app=" + this.AppName;
                strParam = strParam.Replace("\r", "");
                strParam = strParam.Replace("\n", "");
                //System.Web.HttpContext.Current.Response.Redirect("error.aspx?" + strParam, false);
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
        private void WriteToTable(CaughtException ce)
        {

        }
    }
}
