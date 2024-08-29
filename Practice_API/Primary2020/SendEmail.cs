using System;

namespace Primary2020
{
	/// <summary>
	/// Summary description for SendEmail.
	/// </summary>
	public class SendEmail
	{
		public SendEmail()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public void SendEmailMsg(string strBody, string strSubject, string strTo, string strFrom, ref bool blnSend)
		{
			try
			{
				CDO.Message msg = new CDO.Message();
			
				msg.From = strFrom;
				msg.Sender = strFrom; 
				msg.To = strTo;					
				msg.HTMLBody = strBody;				
				msg.Subject = strSubject;
				msg.Send();
				blnSend = true;
			}
			catch (Exception ex)
			{
				blnSend	= false;
				string str = ex.ToString();
			}
		}

		public void SendEmailMsg(string strBody, string strSubject, string strTo, string strFrom, string strCC, string strBCC)
		{
			CDO.Message msg = new CDO.Message();
			
			msg.From = strFrom;
			msg.Sender = strFrom; 
			msg.To = strTo;
			msg.CC = strCC;
			msg.BCC = strBCC;
			msg.HTMLBody = strBody;
			msg.Subject = strSubject;
			msg.Send();
		}
        public void SendEmailMsg(string strBody, string strSubject, string strTo, string strFrom, string strCC, string strBCC, out bool blnSend)
        {
            try
            {
                CDO.Message msg = new CDO.Message();

                msg.From = strFrom;
                msg.Sender = strFrom;
                msg.To = strTo;
                msg.CC = strCC;
                msg.BCC = strBCC;
                msg.HTMLBody = strBody;
                msg.Subject = strSubject;
                msg.Send();
                blnSend = true;
            }
            catch (Exception ex)
            {
                blnSend = false;
                string str = ex.ToString();
            }
        }
		
		public void SendEmailMsg(string strBody, string strSubject, string strTo, string strFrom)
		{
			CDO.Message msg = new CDO.Message();			
			msg.From = strFrom;
			msg.Sender = strFrom; 
			msg.To = strTo;					
			msg.HTMLBody = strBody;				
			msg.Subject = strSubject;
			msg.Send();
		}

		public void SendEmailMsg(string strBody, string strSubject, string strTo, string strFrom, string strBCC)
		{
			CDO.Message msg = new CDO.Message();			
			msg.From = strFrom;
			msg.Sender = strFrom; 
			msg.To = strTo;		
			msg.BCC = strBCC;
			msg.HTMLBody = strBody;				
			msg.Subject = strSubject;
			msg.Send();
		}
	}
}
