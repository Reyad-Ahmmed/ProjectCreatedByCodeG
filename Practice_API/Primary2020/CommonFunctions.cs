using System;
using System.IO;
using System.Text;

namespace Primary2020
{
	/// <summary>
	/// Summary description for ServerObject.
	/// </summary>
	public class CommonFunctions
	{
		string userName;
		string functionName;
		string pageName;
		string appName;
		string logMessage;
		DateTime sysDateTime;
		public CommonFunctions()
		{
			UserName = "Not Defined";
			FunctionName = "Not Defined";
			PageName = "Not Defined";
			AppName = "Not Defined";
			SysDateTime = DateTime.Now;
		}
		public CommonFunctions(string uName, string fName, 
									string pName, string aName)
		{
			UserName = uName;
			FunctionName = fName;
			PageName = pName;
			AppName = aName;
			SysDateTime = DateTime.Now;
		}

		public string UserName
		{
			get { return userName;}
			set { userName = value;}
		}
		public string FunctionName
		{
			get { return functionName;}
			set { functionName = value;}
		}
		public string PageName
		{
			get { return pageName;}
			set { pageName = value;}
		}
		public string AppName
		{
			get { return appName;}
			set { appName = value;}
		}
		public string LogMessage
		{
			get { return logMessage;}
			set { logMessage = value;}
		}
		public DateTime SysDateTime
		{
			get { return sysDateTime;}
			set { sysDateTime = value;}
		}

		
		//Construct the string to be written to log.txt
		//when the user session starts
		public void SessionStartMsg()
		{
			string msg;
			msg = SysDateTime.ToString();
			msg = msg + " User Name: " + UserName;
			msg = msg + " Application: " + AppName;
			msg = msg + " Message: Session Started. ";
			WriteToLog(msg);	
		}
		//Construct the string to be written to log.txt
		//when the user session ends
		public void SessionEndMsg()
		{
			string msg;
			msg = SysDateTime.ToString();
			msg = msg + " User Name: " + UserName;
			msg = msg + " Application: " + AppName;
			msg = msg + " Message: Session Ended. ";
			WriteToLog(msg);	
		}
		private void WriteToLog(string msg)
		{
			try
			{
				StreamWriter io = new StreamWriter(@"c:\log.txt",true,Encoding.ASCII);
				io.WriteLine(msg);
				io.Close();
			}
			catch{}
		}

		public static long ConvertForNewId(long id)
		{
			if (id == 0)
				return -1;
			else
				return id;
		}

        public static string ConvertToBoolean(string bln)
        {
            switch (bln)
            {
                case "1":
                    return "true";
                case "0":
                    return "false";
                default:
                    return bln;
            }
        }
	}
}
