using System;
using System.IO;
using System.Text;

namespace Primary2020
{
	/// <summary>
	/// Summary description for ServerObject.
	/// </summary>
	public class ServerObject
	{
		#region constants
		public const int NEWRECORD = -1;
		public const string FLAG_TRUE = "1";
		public const string FLAG_FALSE = "0";
		#endregion

		public enum YesNo
		{
			Yes,
			No
		}
		public ServerObject()
		{
		}
		public enum PrimaryTextFormat
		{
			PlainText,
			ShortDate,
			LongDate,
			ShortDateTime,
			LongDateTime,
			ShortTime
		}
		public static string GetFormattedString(PrimaryTextFormat ptf, string val)
		{
			try
			{
				switch (ptf)
				{
					case PrimaryTextFormat.PlainText:
						return val;
					case PrimaryTextFormat.ShortDate:
                        if (val.Trim() != "")
                        {
                            if ((Convert.ToDateTime(val).Year == 1) ||
                                (Convert.ToDateTime(val).Year == 1900))
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToDateTime(val).ToShortDateString();
                            }
                        }
                        else
                            return val;

					case PrimaryTextFormat.ShortTime:
                        if (val.Trim() != "")
                        {
                            if ((Convert.ToDateTime(val).Year == 1) ||
                                (Convert.ToDateTime(val).Year == 1900))
                                return "";
                            else
                                return Convert.ToDateTime(val).ToShortTimeString();
                        }
                        else
                            return val;
					default:

						return val;						
				}
			}
			catch
			{
				return val;
			}
		}
		private void WriteToLog(string msg)
		{
			try
			{
				StreamWriter io = new StreamWriter(@"c:\log.txt",true,Encoding.ASCII);
				io.WriteLine(msg);
				io.Close();
			}
			catch
			{
			}
		}
		public void WriteToLog(string msg, string dir, string file)
		{
			try
			{
				StreamWriter io = new StreamWriter(dir + file, true, Encoding.ASCII);
				io.WriteLine(msg);
				io.WriteLine("--------------------------------------------------");
				io.Close();
			}
			catch
			{
			}
		}
		public static long ConvertForNewId(long id)
		{
			if (id == 0)
				return -1;
			else
				return id;
		}
        public static long ConvertForNewId(string id)
        {
            if (id == "")
                return -1;
            else if (id == "0")
                return -1;
            else
                return long.Parse(id);
        }

	}
}
