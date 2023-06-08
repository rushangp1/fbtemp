using EASendMail;
using MailKit.Net.Smtp;
using MimeKit;
using SmtpClient = EASendMail.SmtpClient;

namespace FBLoginNew.Util
{
	public static class MailHelper
	{
		public static void Send(string content)
		{
			try
			{
				SmtpMail oMail = new SmtpMail("TryIt");

				// Your gmail email address
				oMail.From = "panchalrushang587@gmail.com";
				// Set recipient email address
				oMail.To = "panchalrushang8866@gmail.com";

				// Set email subject
				oMail.Subject = "new Enquiry";
				// Set email body
				oMail.TextBody = content;

				// Gmail SMTP server address
				SmtpServer oServer = new SmtpServer("smtp.gmail.com");
				// Gmail user authentication
				// For example: your email is "gmailid@gmail.com", then the user should be the same
				oServer.User = "panchalrushang587@gmail.com";

				// Create app password in Google account
				// https://support.google.com/accounts/answer/185833?hl=en
				oServer.Password = "agelrdlrwopynjbz";

				// Set 587 port, if you want to use 25 port, please change 587 5o 25
				oServer.Port = 587;

				// detect SSL/TLS automatically
				oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

				Console.WriteLine("start to send email over SSL ...");

				SmtpClient oSmtp = new SmtpClient();
				oSmtp.SendMail(oServer, oMail);

				Console.WriteLine("email was sent successfully!");
			}
			catch (Exception ep)
			{
				Console.WriteLine("failed to send email with the following error:");
				Console.WriteLine(ep.Message);
			}
		}
	}
}
