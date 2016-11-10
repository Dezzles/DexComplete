using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace DexComplete.Utilities
{
	public static class Email
	{
		static string EmailAddress { get; set; }
		static string EmailPassword { get; set; }
		static string SMTPAddress { get; set; }
		static int SMTPPort { get; set; }

		static Email()
		{
			SLLog Log = Logging.GetLog(null);
			EmailAddress = Services.ServerService.GetEmailAddress(Log);
			EmailPassword = Services.ServerService.GetEmailPassword(Log);
			SMTPAddress = Services.ServerService.GetSMTPSettings(Log);
			SMTPPort = Services.ServerService.GetSMTPPort(Log);
		}

		public static void SendEmail(string Email, string Subject, String Contents, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			MailMessage message = new MailMessage();
			message.From = new MailAddress(EmailAddress);

			message.To.Add(new MailAddress(Email));
			
			message.Subject = Subject;
			message.Body = Contents;
			message.IsBodyHtml = true;
			SmtpClient client = new SmtpClient(SMTPAddress, SMTPPort);
			client.Credentials = new System.Net.NetworkCredential(EmailAddress, EmailPassword);
			client.EnableSsl = true;
			client.Send(message);
		}
	}
}