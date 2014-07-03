using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.UI.HtmlControls;

namespace USAB.Util
{
    public class SMTPHelper
    {
        public static bool SendMail(string from, string tos, string ccs, string bccs, string subject, string body)
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            string host = settings.Smtp.Network.Host;
            int port = settings.Smtp.Network.Port;

            SmtpClient sc = new SmtpClient(host, port); // pass the host address and port
            sc.UseDefaultCredentials = settings.Smtp.Network.DefaultCredentials;

            MailMessage msg = new MailMessage();
            msg.IsBodyHtml = true;

            msg.Body = "<HTML><BODY>" + body + "</BODY></HTML>";
            ContentType mimeType = new ContentType("text/html");
            AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
            msg.AlternateViews.Add(alternate);

            msg.Subject = subject;
            msg.From = new MailAddress(settings.Smtp.From);
            msg.To.Add(tos.Replace(";", ","));
            if (!ccs.Equals(""))
                msg.CC.Add(ccs.Replace(";", ","));
            if (!bccs.Equals(""))
                msg.Bcc.Add(bccs.Replace(";", ","));

            try
            {
                sc.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool SendMail(string from, string tos, string ccs, string bccs, string subject, string body, string attachFileName)
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            string host = settings.Smtp.Network.Host;
            int port = settings.Smtp.Network.Port;

            SmtpClient sc = new SmtpClient(host, port); // pass the host address and port
            sc.UseDefaultCredentials = settings.Smtp.Network.DefaultCredentials;

            MailMessage msg = new MailMessage();
            msg.Body = body;
            msg.Attachments.Add(new Attachment(attachFileName));
            msg.Subject = subject;
            msg.From = new MailAddress(from);
            msg.To.Add(tos.Replace(";", ","));
            if (!ccs.Equals(""))
                msg.CC.Add(ccs.Replace(";", ","));
            if (!bccs.Equals(""))
                msg.Bcc.Add(bccs.Replace(";", ","));

            try
            {
                sc.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
