using System;
using System.Configuration;

namespace Primary2020
{
	/// <summary>
	/// Summary description for AppSettings.
	/// </summary>
	public class AppSettings
	{
		#region private variables
		private string _strLogDir;
		private string _strLogFileName;
		private string _strDBCnnString;
		private string _strAppName;
		private string _strAnesthPrs;
		#endregion

		#region public properties
		public string LogDir
		{
			get {return _strLogDir;}
			set {_strLogDir = value;}
		}
		public string LogFileName
		{
			get {return _strLogFileName;}
			set {_strLogFileName = value;}
		}
		public string DBCnnString
		{
			get {return _strDBCnnString;}
			set {_strDBCnnString = value;}
		}
		public string AppName
		{
			get {return _strAppName;}
			set {_strAppName = value;}
		}
		public string AnesthPrs
		{
			get {return _strAnesthPrs;}
			set {_strAnesthPrs = value;}
		}
		#endregion

		public AppSettings()
		{
		}

		public void LoadSettings()
		{
            try
            {
                this.DBCnnString = ConfigurationManager.ConnectionStrings["sqlConnStr"].ToString();
            }
            catch(Exception)
            {
                this.DBCnnString = ConfigurationManager.AppSettings["sqlConnStr"].ToString();
            }
		}
	}

}
