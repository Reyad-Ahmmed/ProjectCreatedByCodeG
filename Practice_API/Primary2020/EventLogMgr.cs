using System;
using System.Diagnostics;

namespace Primary2020
{
	/// <summary>
	/// Calls used to write to the event log
	/// </summary>
	public class EventLogMgr
	{
		public const string LogName = "JAYDB";
		public EventLogMgr()
		{
		}

		public void write(EventLogEntryType logType, string strAppName, string strClass,
			string strMessage)
		{
			EventLog el;
			string strEventText;
			if (EventLog.LogNameFromSourceName(strAppName, ".") != EventLogMgr.LogName)
			{
				if (EventLog.SourceExists(strAppName))
				{
					EventLog.DeleteEventSource(strAppName);
				}
			}
			//associate with correct folder.
			if (!EventLog.SourceExists(strAppName))
			{
				EventLog.CreateEventSource(strAppName, EventLogMgr.LogName);
			}

			strEventText = "Class: " + strClass + "\nMessage: " + strMessage;

			el = new EventLog(EventLogMgr.LogName, ".", strAppName);
			el.WriteEntry(strEventText, logType);
			el.Close();
			el = null;
		}
		
	}
}
