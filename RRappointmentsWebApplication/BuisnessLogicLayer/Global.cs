using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    
    public class Global
    {
        public static readonly string SMS_SENDER_URL = "http://wap.y-it.co.il:8080/wapdb/ws_send_sms";
        public static readonly string EMAIL_SENDER_URL = "http://reports.y-it.co.il:8080/reports/ws_send_email";
        public static readonly int LB_ERROR = -1;

        public static readonly string UNFOUND_MSG   = "מספר תעודת הזהות שהקשת לא קיים במערכת";
        public static readonly string EXISTS_MSG    = "מספר תעודת הזהות קיים כבר במערכת.";
        public static readonly string FAILURE_MSG   = "אין אפשרות לספק את השירות כרגע. נא נסה שנית במועד מאוחר יותר.";
        public static readonly string MAIL_ERROR    = "לא הצלחתי לשלוח דואר אלקטרוני";
        public static readonly string ADDN_DATA     = "appointment";
        public static readonly string PROG_TYPE     = "medical";
        public static readonly string DEF_RECEPIENT = "alex@rrsystems.co.il";   // added on 07/02/2016 for test purposes
        public static readonly string PWD_UPD_ERROR = "לא הצלחתי להפיק סיסמא חדשה";  // added on 29/07/2020 for password recovery
        public static readonly string PWD_UPDATE    = "פג תוקף סיסמתך. הינך מועבר להפקת סיסמא מחדש";    // added on 29/07/2020 for password update

        public const string EMAIL_CONFIG = "App_Data\\Email.Config.txt"; //"Email.Config.txt"; - corrected on 02/10/2013
        public const string APP_ROOT_REL = "~";
        public const string BACK_SLASH = "\\";
        public const string KEY_FROMADDR = "From:";
        public const string KEY_SRVRSMTP = "Smtp:";
        public const string KEY_PORTSMTP = "Port:";
        public const string KEY_SSLUSAGE = "SSL:";
        public const string KEY_USERNAME = "UserName:";
        public const string KEY_PASSWORD = "Password:";
        // added on 12/02/2020 : //////////////////////////
        public const string KEY_USE_NICK = "Use Nickname:"; 
        public const string KEY_NICKNAME = "Nickname:";
        public const string SENDER_NICK  = "Medsoft";
        // end added on 12/02/2020 ////////////////////////

        public const char CONFG_SEPAR = '=';
        public const int DEFAULT_PORT = 25;

        // added on 26/01/2014 : //////////////////////////////////////////////
        public static readonly string SUCCESS = "OK";
        public static readonly string LOCK_ERROR_CAPTION = "זימון תור נכשל";
        public static readonly string LOCK_ERROR_MESSAGE = "לקוח יקר! התור שבחרת לזימון נתפס על ידי לקוח אחר.\nאנא לחץ \"אחורה\" על מנת לקבוע מועד חדש.";
        // end added //////////////////////////////////////////////////////////

        public static string GetSenderWindowScript(string serviceUrl)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\">");
            sb.Append("var winRef = window.open('");
            sb.Append(serviceUrl);
            sb.Append("'); sleep(3000); winRef.close(); ");
            sb.Append("function sleep(interval) { var start = new Date().getTime(); ");
            sb.Append("while ((new Date().getTime() - start) < interval){} };");
            sb.Append("</script>");

            return sb.ToString();
        }

        public static Dictionary<string, string> GetEmailSettings(string localpath)
        {
            StreamReader reader = new StreamReader(localpath);
            if (reader == null)
                return null;
            string sError = null;
            Dictionary<string, string> data = new Dictionary<string, string>();
            if (data == null)
                return null;

            try
            {
                if (File.Exists(localpath))
                {
                    reader.DiscardBufferedData();
                    string line;

                    // Read and display lines from the file until the end of  
                    // the file is reached. 
                    while ((line = reader.ReadLine()) != null)
                    {
                        string key = line.Split(CONFG_SEPAR)[0];
                        string val = line.Split(CONFG_SEPAR)[1];
                        data.Add(key, val ?? string.Empty);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                sError = ex.Message.ToString();
            }
            catch (DirectoryNotFoundException ex)
            {
                sError = ex.Message.ToString();
            }
            catch (IOException ex)
            {
                sError = ex.Message.ToString();
            }
            catch (Exception ex)
            {
                sError = ex.Message.ToString();
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                    reader = null;
                }
            }

            return string.IsNullOrEmpty(sError) ? data : null;
        }

        public static string GetEmailSenderSettings(ref string smtpServer, ref string userName, ref string password,
            ref int smtpPort, ref bool useSsl, Dictionary<string, string> EmailSettings)
        {
            string mailSender = null;
            foreach (KeyValuePair<string, string> item in EmailSettings)
            {
                switch (item.Key)
                {
                    case Global.KEY_FROMADDR:
                        mailSender = item.Value;
                        break;
                    case Global.KEY_SRVRSMTP:
                        smtpServer = item.Value;
                        break;
                    case Global.KEY_PORTSMTP:
                        smtpPort = Convert.ToInt32(item.Value);
                        break;
                    case Global.KEY_SSLUSAGE:
                        useSsl = Convert.ToBoolean(Convert.ToInt32(item.Value));
                        break;
                    case Global.KEY_USERNAME:
                        userName = item.Value;
                        break;
                    case Global.KEY_PASSWORD:
                        password = item.Value;
                        break;
                }
            }

            return mailSender;
        }

        // overloaded function added on 23/02/2020 : //
        public static string GetEmailSenderSettings(ref string smtpServer, ref string userName, ref string password,
            ref string nickname, ref int smtpPort, ref bool useSsl, ref bool useNick, Dictionary<string, string> EmailSettings)
        {
            string mailSender = null;
            foreach (KeyValuePair<string, string> item in EmailSettings)
            {
                switch (item.Key)
                {
                    case Global.KEY_FROMADDR:
                        mailSender = item.Value;
                        break;
                    case Global.KEY_SRVRSMTP:
                        smtpServer = item.Value;
                        break;
                    case Global.KEY_PORTSMTP:
                        smtpPort = Convert.ToInt32(item.Value);
                        break;
                    case Global.KEY_SSLUSAGE:
                        useSsl = Convert.ToBoolean(Convert.ToInt32(item.Value));
                        break;
                    case Global.KEY_USERNAME:
                        userName = item.Value;
                        break;
                    case Global.KEY_PASSWORD:
                        password = item.Value;
                        break;
                    case Global.KEY_NICKNAME:
                        nickname = item.Value;
                        break;
                    case Global.KEY_USE_NICK:
                        useNick = Convert.ToBoolean(Convert.ToInt32(item.Value));
                        break;
                }
            }

            return mailSender;
        }

        public static string GetRelativePath(string fileName)
        {
            return BACK_SLASH + fileName;
        }

        public static string GetRootRelativePath(string fileName)
        {
            return APP_ROOT_REL + BACK_SLASH + fileName;
        }

    }
}