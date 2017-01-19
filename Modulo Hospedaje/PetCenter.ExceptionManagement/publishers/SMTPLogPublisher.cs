using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Collections.Specialized;
using System.Text;
using PetCenter.ExceptionManagement;

namespace PetCenter.ExceptionManagement
{
	class SMTPLogPublisher:IExceptionPublisher
	{
		#region IExceptionPublisher Members

		public void Publish(Exception exception, System.Collections.Specialized.NameValueCollection additionalInfo, System.Collections.Specialized.NameValueCollection configSettings)
		{
			try
			{
				string strNewLine = System.Environment.NewLine;
				StringBuilder stbException = new StringBuilder();

				//MailMessage objMessage = new MailMessage();
				//objMessage.From = configSettings["fromemail"].ToString();
				//objMessage.To = configSettings["toemail"].ToString().Replace(",", ";");

                MailAddress from = new MailAddress(configSettings["fromemail"].ToString());

                MailAddress to = new MailAddress(configSettings["toemail"].ToString().Replace(",", ";"));
                MailMessage objMessage = new MailMessage(from, to);
                if (configSettings["ccemail"].ToString()!=""){
                objMessage.CC.Add(configSettings["ccemail"].ToString());}


				objMessage.Subject = configSettings["subject"].ToString();
				for (int index = 0;index < additionalInfo.Count;index++)
				{
					stbException.Append(string.Format("{0}: {1}", additionalInfo.Keys[index], additionalInfo[index]));
					stbException.Append(strNewLine);
				}
				stbException.Append("ExceptionType: ");
				stbException.Append(exception.GetType());
				stbException.Append(strNewLine);
				stbException.Append("Message: ");
				stbException.Append(exception.Message);
				stbException.Append(strNewLine);
				stbException.Append("Target Site: ");
				stbException.Append(exception.TargetSite);
				stbException.Append(strNewLine);
				stbException.Append("Source: ");
				stbException.Append(exception.Source);
				stbException.Append(strNewLine);
				stbException.Append("StackTrace: ");
				stbException.Append(exception.StackTrace);
				stbException.Append(strNewLine);
				stbException.Append("InnerException: ");
				stbException.Append(exception.InnerException);
				stbException.Append(strNewLine);
				objMessage.Body = stbException.ToString();

                SmtpClient client = new SmtpClient(Convert.ToString(configSettings["smtpServer"]));
                //client.Credentials = CredentialCache.DefaultNetworkCredentials;
                client.Credentials = new System.Net.NetworkCredential(Convert.ToString(configSettings["userSMTP"]), Convert.ToString(configSettings["passwordSMTP"]));
                client.Port = Int32.Parse(configSettings["smtpPort"]);
                client.Send(objMessage);

				//SmtpMail.SmtpServer = Convert.ToString(configSettings["smtpServer"]);
				//SmtpMail.Send(objMessage);
			}
			catch (Exception)
			{}
		}
		#endregion
	}
}