using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace DexComplete.Services
{
	public class EmailService
	{
		string EmailAddress { get; set; }
		string EmailPassword { get; set; }
		string SMTPAddress { get; set; }
		int SMTPPort { get; set; }

		public EmailService(Services.ServerService ServerService)
		{
			SLLog Log = Logging.GetLog(null);
			EmailAddress = ServerService.GetEmailAddress(Log);
			EmailPassword = ServerService.GetEmailPassword(Log);
			SMTPAddress = ServerService.GetSMTPSettings(Log);
			SMTPPort = ServerService.GetSMTPPort(Log);
		}

		public void SendEmail(string Email, string Subject, String Contents, SLLog Log)
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