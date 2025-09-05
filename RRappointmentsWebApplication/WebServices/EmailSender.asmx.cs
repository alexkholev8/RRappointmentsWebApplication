using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Security;

namespace RRappointmentsWebApplication.WebServices
{
    /// <summary>
    /// Summary description for EmailSender
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EmailSender : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public bool SendEmail(string Recipient, string Subject, string Content, string Sender, string SmtpServer, int Port, bool SSL, string UserName, string Password)
        {
            Generic.WriteLog(Generic.GetLogString(0, "EmailSender", "SendEmail"));
            bool ret = true;
            string sError = string.Empty;
            string InrMsg = null;

            MailMessage message = new MailMessage();
            if (message == null)
            {
                Generic.WriteLog("Failed to initialize class MailMessage");
                return false;
            }
            string Display = Sender;    // may differ
            //Mail Address
            message.From = new MailAddress(Sender, Display, System.Text.Encoding.UTF8);
            message.Sender = new MailAddress(Sender, Display, System.Text.Encoding.UTF8);   // OPTIONAL
            //receiver email id
            message.To.Add(Recipient);
            // CC
            // Not implemented here

            //subject of the email
            message.Subject = Subject;
            //add the body of the email
            message.Body = Content;
            message.IsBodyHtml = true;

            //SMTP client
            SmtpClient client = new SmtpClient(SmtpServer); // working smtp account
            if (client == null)
            {
                Generic.WriteLog("Failed to initialize class SmtpClient");
                return false;
            }
            client.Host = SmtpServer;   // OPTIONAL
            client.Port = Port;         // usually 25 or 587

            //credentials to login in to Gmail account
            client.UseDefaultCredentials = false;   // IMPORTANT NOTE: this statement has to be declared BEFORE
            // Credentials definition BUT NOT AFTER. Otherwise, Credentials
            // value will be set to null that will cause exception !!!
            client.Credentials = new NetworkCredential(UserName, Password);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //enabled SSL
            client.EnableSsl = SSL;

            try
            {
                //Send an email
                client.Send(message);
            }
            catch (SmtpFailedRecipientsException ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    sError = ex.Message.ToString();
                }
                if (ex.InnerException != null)
                {
                    InrMsg = ex.InnerException.Message;
                }
            }
            catch (SmtpFailedRecipientException ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    sError = ex.Message.ToString();
                }
                if (ex.InnerException != null)
                {
                    InrMsg = ex.InnerException.Message;
                }
            }
            catch (SmtpException ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    sError = ex.Message.ToString();
                }
                if (ex.InnerException != null)
                {
                    InrMsg = ex.InnerException.Message;
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    sError = ex.Message.ToString();
                }
                if (ex.InnerException != null)
                {
                    InrMsg = ex.InnerException.Message;
                }
            }
            finally
            {
                if (!string.IsNullOrEmpty(sError))
                {
                    int pos = -1;
                    if ((pos = sError.IndexOf(" Learn")) != -1)
                    {
                        sError = sError.Remove(pos);
                    }
                    sError = "Email sending error: " + sError;
                    if (!string.IsNullOrEmpty(InrMsg))
                    {
                        sError += '\n' + InrMsg; 
                    }
                    Generic.WriteLog(sError);
                    ret = false;
                }

                message.Dispose();
                client.Dispose();
                message = null;
                client = null;
                // GC.Collect();
            }

            Generic.WriteLog(Generic.GetLogString(1, "EmailSender", "SendEmail"));
            
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public bool SendEmailExt(string Recipient, string Subject, string Content, string Sender, string SmtpServer, string UserName, string Password, string NickName, int Port, bool SSL, bool useNick)
        {
            Generic.WriteLog(Generic.GetLogString(0, "EmailSender", "SendEmail"));
            bool ret = true;
            string sError = string.Empty;
            string InrMsg = null;

            MailMessage message = new MailMessage();
            if (message == null)
            {
                Generic.WriteLog("Failed to initialize class MailMessage");
                return false;
            }

            string Display = useNick ? NickName : string.Empty;
            if (useNick)
                Sender = string.Format("{0}<{1}>", Display, Sender);
            //Mail Address
            message.From = new MailAddress(Sender, Display, System.Text.Encoding.UTF8);
            message.Sender = new MailAddress(Sender, Display, System.Text.Encoding.UTF8);   // OPTIONAL
            //receiver email id
            message.To.Add(Recipient);
            // CC
            // Not implemented here

            //subject of the email
            message.Subject = Subject;
            //add the body of the email
            message.Body = Content;
            message.IsBodyHtml = true;

            //SMTP client
            SmtpClient client = new SmtpClient(SmtpServer); // working smtp account
            if (client == null)
            {
                Generic.WriteLog("Failed to initialize class SmtpClient");
                return false;
            }
            client.Host = SmtpServer;   // OPTIONAL
            client.Port = Port;         // usually 25 or 587

            //credentials to login in to Gmail account
            client.UseDefaultCredentials = false;   // IMPORTANT NOTE: this statement has to be declared BEFORE
            // Credentials definition BUT NOT AFTER. Otherwise, Credentials
            // value will be set to null that will cause exception !!!
            client.Credentials = new NetworkCredential(UserName, Password);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //enabled SSL
            client.EnableSsl = SSL;

            try
            {
                //Send an email
                client.Send(message);
            }
            catch (SmtpFailedRecipientsException ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    sError = ex.Message.ToString();
                }
                if (ex.InnerException != null)
                {
                    InrMsg = ex.InnerException.Message;
                }
            }
            catch (SmtpFailedRecipientException ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    sError = ex.Message.ToString();
                }
                if (ex.InnerException != null)
                {
                    InrMsg = ex.InnerException.Message;
                }
            }
            catch (SmtpException ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    sError = ex.Message.ToString();
                }
                if (ex.InnerException != null)
                {
                    InrMsg = ex.InnerException.Message;
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    sError = ex.Message.ToString();
                }
                if (ex.InnerException != null)
                {
                    InrMsg = ex.InnerException.Message;
                }
            }
            finally
            {
                if (!string.IsNullOrEmpty(sError))
                {
                    int pos = -1;
                    if ((pos = sError.IndexOf(" Learn")) != -1)
                    {
                        sError = sError.Remove(pos);
                    }
                    sError = "Email sending error: " + sError;
                    if (!string.IsNullOrEmpty(InrMsg))
                    {
                        sError += '\n' + InrMsg;
                    }
                    Generic.WriteLog(sError);
                    ret = false;
                }

                message.Dispose();
                client.Dispose();
                message = null;
                client = null;
                // GC.Collect();
            }

            Generic.WriteLog(Generic.GetLogString(1, "EmailSender", "SendEmail"));

            return ret;
        }
    }
}
