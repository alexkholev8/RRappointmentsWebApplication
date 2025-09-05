using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public class SmtpMailData
    {
#region member_fields_section
        private bool ssl;
        private int port;
        private string server;
        private string from;
        private string subject;
        private string content;
        private string username;
        private string alias;
        private string password;
        private string [] sendto;
        private string [] copyto;

        public static readonly string SMTP_HOTMAIL = "smtp.live.com";
        public static readonly string SMTP_GMAIL = "smtp.gmail.com";
        public static readonly string SMTP_YAHOO = "smtp.mail.yahoo.com";
        public static readonly string SMTP_AOL = "smtp.aol.com";
        public static readonly string SMTP_OUTLOOK = "smtp.office365.com";
        public static readonly string[] SMTP_CLIENT_LIST = { SMTP_GMAIL, SMTP_OUTLOOK, SMTP_HOTMAIL, SMTP_YAHOO, SMTP_AOL };
#endregion member_fields_section

#region member_accessors

        public bool SSL
        {
            get { return ssl; }
            set { ssl = value; }
        }

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        public string Server
        {
            get { return server; }
            set { server = value; }
        }

        public string From
        {
            get { return from; }
            set { from = value; }
        }

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        public string UserName
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Alias
        {
            get { return alias; }
            set { alias = value; }
        }

        public string[] SendTo
        {
            get { return sendto; }
            set { sendto = value; }
        }

        public string[] CopyTo
        {
            get { return copyto; }
            set { copyto = value; }
        }
#endregion member_accessors

#region constructors
        public SmtpMailData()
        { 
        
        }

        public SmtpMailData(bool isSSL, int port, string server, string from, 
            string subject, string content, string userName, string password, string[] sendTo, string[] copyTo)
        {
            SSL = isSSL;
            Port = port;
            Server = server;
            From = from;
            Subject = subject;
            Content = content;
            UserName = userName;
            Password = password;
            SendTo = sendTo;
            CopyTo = copyTo;
            Alias = GetAliasName(from);
        }

        ~SmtpMailData()
        {
        }
#endregion constructors

#region member_methods
        public static SmtpMailData GetSmtpMailData(bool isSSL, int port, string server, string from, 
            string subject, string content, string userName, string password, string[] sendTo, string[] copyTo, 
            string nickname, bool isNick)   // 2 last parameters added on 13/02/2020
        {
            return new SmtpMailData
            {
                SSL = isSSL,
                Port = port,
                Server = server,
                From = from,
                Subject = subject,
                Content = content,
                UserName = userName,
                Password = password,
                SendTo = sendTo,
                CopyTo = copyTo,
              //Alias = GetAliasName(from)                  // commented on 13/02/2020
                Alias = (isNick) ? nickname : string.Empty  //GetAliasName(from)    // corrected on 13/02/2020
            };
        }

        private static string GetAliasName(string from)
        {
            string alias = (string.IsNullOrEmpty(from)) ? Environment.UserName : from.Remove(from.IndexOf('@'));

            return alias;
        }

        private string GetAliasName()
        {
            string alias = (string.IsNullOrEmpty(From)) ? Environment.UserName : From.Remove(From.IndexOf('@'));

            return alias;
        }

#endregion member_methods
    } 
}