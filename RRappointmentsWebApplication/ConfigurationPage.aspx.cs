using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RRappointmentsWebApplication.BuisnessLogicLayer;
using System.Text;
using WSLI = RRappointmentsWebApplication.BuisnessLogicLayer.WebServicesLocationInfo;
using System.Net;
using System.Runtime;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Security;
using COLOR = System.Drawing.Color;
using System.IO;
using System.Security;      // ADDED ON 30/09/2013
using System.Reflection;    // ADDED ON 14/01/2014 

namespace RRappointmentsWebApplication
{
    public partial class ConfigurationPage : System.Web.UI.Page
    {
        private readonly string[] webServiceLocation = { "משולב",  "מרוחק" };
        private const char COMMA = ',';
        private const char SEMICOLON = ';';
        private const string NOREPLY = "NoReply@rrsystems.co.il";
        private const string SUCCESS = "OK";
        private const string FAILURE = "failed:\n";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!GeneralStaticLogic.UserLogIn)
                {
                    Response.Redirect("index.aspx");
                }

                LoadConfigurationSettings();

                // ADDED ON 23/09/2013
                if (chkPreset.Checked)
                    LoadPresets();
                // END ADDED /////////
            }
            //else // as condition inserted on 13/02/2020
            //{
            //    // ADDED ON 23/09/2013
            //    if (chkPreset.Checked)
            //        LoadPresets();
            //}
            // ADDED ON 26/04/2015
            txtWcfAddress.Enabled = chkUseWcfDiariesCounterService.Checked;
            txtWcfAddress.ReadOnly = !chkUseWcfDiariesCounterService.Checked;
            txtWcfAddress.ForeColor = (chkUseWcfDiariesCounterService.Checked) ? COLOR.Blue: COLOR.Gray;
            // END ADDED /////////
        }

        private void LoadConfigurationSettings()
        {
            PopulateAppointmentLimitComboBox();
            SiteConfigurationLogic.LoadConfigurations();
            
            if (SiteConfigurationLogic.EnableTreatmentGroups == "True")
            {
                enableTreatmentGroupCheckBox.Checked = true;
            }
            else
            {
                enableTreatmentGroupCheckBox.Checked = false;
            }

            if (SiteConfigurationLogic.AppointmentLimit != "0")
            {
                appointmentLimitCheckBox.Checked = true;
                appointmentLimitDropDownList.Style.Add("visibility", "visible");
                appointmentLimitDropDownList.SelectedIndex = int.Parse(BuisnessLogicLayer.SiteConfigurationLogic.AppointmentLimit) - 1;

            }
            else
            {
                appointmentLimitCheckBox.Checked = false;
                appointmentLimitDropDownList.Style.Add("visibility", "hidden");
            }

            // added on 28/07/2019 to filter available appointments using dline : ///////
            if (SiteConfigurationLogic.UseDlineFiltering == "True")
            {
                dlineFilteringCheckBox.Checked = true;
            }
            else
            {
                dlineFilteringCheckBox.Checked = false;
            }
            // end added on 28/07/2019 //////////////////////////////////////////////////

            // added on 28/07/2020 : ////////////////////////////////////////////////////
            if (SiteConfigurationLogic.PasswordUpdateUse == "True")
            {
                passwordUpdateCheckBox.Checked = true;
                txtDaysCount.Style.Add("visibility", "visible");
                txtDaysCount.Text = SiteConfigurationLogic.PasswordDaysCount;

            }
            else
            {
                passwordUpdateCheckBox.Checked = false;
                txtDaysCount.Style.Add("visibility", "hidden");
                txtDaysCount.Text = string.Empty;
            }
            // end added on 28/07/2020 //////////////////////////////////////////////////
            // corrections and additions on 18/08/2013 : ////////////////////////////////
            wsIPtextBox.Text = SiteConfigurationLogic.WebServiceIp;
            wsPortTextBox.Text = SiteConfigurationLogic.WebServicePort.Replace(":", "");
            
            ddlWebServiceLocation.SelectedValue = SiteConfigurationLogic.WebServiceLocation;
            ddlWebServiceLocation.SelectedItem.Selected = true;
            txtWebServiceDirectory.Text = SiteConfigurationLogic.WebServiceDirectory;

            // added on 08/08/2013 : ////////////////////////////////////////////////////
            ddlWebServiceSyncMode.SelectedValue = SiteConfigurationLogic.WebServiceSyncMode;
            ddlWebServiceSyncMode.SelectedItem.Selected = true;

            SetWebServicesLocation();   // added on 21/07/2013
            // added on 01/10/2013 : ///////////////////////////////////////////////////
            chkPreset.Checked = Convert.ToBoolean(Convert.ToInt32(SiteConfigurationLogic.UseSavedEmailSettings));
            // added on 14/01/2014 : ////////////////////////////////////////////////////
            txtWcfAddress.Text = WcfConfigSettings.GetEndpointAddress();
            // added on 26/04/2015 : ///////////////////////////////////////////////////
            chkUseWcfDiariesCounterService.Checked = Convert.ToBoolean(Convert.ToInt32(SiteConfigurationLogic.UseWcfDiariesCounterService));
            Application["UseWcfDiariesCounterService"] = SiteConfigurationLogic.UseWcfDiariesCounterService;
            // end additions and corrections ///////////////////////////////////////////
        }

        private void PopulateAppointmentLimitComboBox()
        {
            for (int i = 1; i < 100; i++)
            {
                appointmentLimitDropDownList.Items.Add(i.ToString());
            }
        }

        protected void saveExitButton_Click(object sender, EventArgs e)
        {
            SaveConfigurationSettings();
            Response.Redirect("step1.aspx");
        }

        private void SaveConfigurationSettings()
        {
            if (enableTreatmentGroupCheckBox.Checked)
            {
                SiteConfigurationLogic.EnableTreatmentGroups = "True";
            }
            else
            {
                SiteConfigurationLogic.EnableTreatmentGroups = "False";              
            }

            if (appointmentLimitCheckBox.Checked)
            {
                SiteConfigurationLogic.AppointmentLimit = appointmentLimitDropDownList.SelectedItem.ToString();
            }
            else
            {
                SiteConfigurationLogic.AppointmentLimit = "0";
            }

            // added on 28/07/2019 to filter available appointments using dline : ///////
            if (dlineFilteringCheckBox.Checked)
            {
                SiteConfigurationLogic.UseDlineFiltering = "True";
            }
            else
            {
                SiteConfigurationLogic.UseDlineFiltering = "False";
            }

            // end added on 28/07/2019 //////////////////////////////////////////////////

            // added on 28/07/2020 : ////////////////////////////////////////////////////
            if (passwordUpdateCheckBox.Checked)
            {
                SiteConfigurationLogic.PasswordUpdateUse = "True";
                SiteConfigurationLogic.PasswordDaysCount = txtDaysCount.Text;
            }
            else
            {
                SiteConfigurationLogic.PasswordUpdateUse = "False";
                SiteConfigurationLogic.PasswordDaysCount = string.Empty;
            }
            // and added on 28/07/2020 //////////////////////////////////////////////////

            // corrections and additions on 18/08/2013 : ////////////////////////////////
            SiteConfigurationLogic.WebServiceIp = wsIPtextBox.Text;
            SiteConfigurationLogic.WebServicePort = ":" + wsPortTextBox.Text;
            SiteConfigurationLogic.WebServiceDirectory = txtWebServiceDirectory.Text;
            SiteConfigurationLogic.WebServiceLocation = ddlWebServiceLocation.SelectedItem.Value;
            SiteConfigurationLogic.WebServiceSyncMode = ddlWebServiceSyncMode.SelectedItem.Value;   // added on 08/08/2013
            // added on 01/10/2013 : ////////////////////////////////////////////////////
            SiteConfigurationLogic.UseSavedEmailSettings = (Convert.ToInt32(chkPreset.Checked)).ToString();
            // added on 26/04/2015 : ////////////////////////////////////////////////////
            SiteConfigurationLogic.UseWcfDiariesCounterService = (Convert.ToInt32(chkUseWcfDiariesCounterService.Checked)).ToString();
            Application["UseWcfDiariesCounterService"] = SiteConfigurationLogic.UseWcfDiariesCounterService;

            SetWebServicesLocation();   // added on 21/07/2013
            // end corrections and additions ////////////////////////////////////////////
            SiteConfigurationLogic.SaveConfigurations();
            // added on 14/01/2014 : ////////////////////////////////////////////////////
            WcfConfigSettings.SaveEndpointAddress(txtWcfAddress.Text);
            // end added ////////////////////////////////////////////////////////////////
        }

        private void SetWebServicesLocation()
        {
            WebServicesLocationInfo.Directory = SiteConfigurationLogic.WebServiceDirectory;
            WebServicesLocationInfo.IPAddress = SiteConfigurationLogic.WebServiceIp;
            WebServicesLocationInfo.Port = SiteConfigurationLogic.WebServicePort;
            WebServicesLocationInfo.IsRemote = (SiteConfigurationLogic.WebServiceLocation.Equals("1")) ? 
                ("true") : ("false");
            WebServicesLocationInfo.SyncMode = SiteConfigurationLogic.WebServiceSyncMode;   // added on 08/08/2013

            Application[WSLI.WEBSRV_ISREMOTE] = WebServicesLocationInfo.IsRemote;
            Application[WSLI.WEBSRV_DIRECTORY] = WebServicesLocationInfo.Directory;
            Application[WSLI.WEBSRV_IPADDRESS] = WebServicesLocationInfo.IPAddress;
            Application[WSLI.WEBSRV_PORT] = WebServicesLocationInfo.Port;
            Application[WSLI.WBSRV_SYNCMODE] = WebServicesLocationInfo.SyncMode;    // added on 08/08/2013
        }

        protected void SendEmail(object sender, EventArgs e)
        {
            bool isSSL  = chkSSL.Checked;
            // added on 13/02/2020 : /////////////////
            bool isNick = chkNickname.Checked;
            string sNickname = (!isNick) ? (string.Empty) : (txtNickname.Text.Trim() ?? string.Empty);
            // end added on 13/02/2020 ///////////////
            string sFrom = txtFrom.Text.Trim().Trim(SEMICOLON);
            string sAddr = txtTo.Text.Trim().Replace(COMMA, SEMICOLON);
            string sCopy = txtCc.Text.Trim().Replace(COMMA, SEMICOLON); // OPTIONAL
            // SET DEFAULT VALUE FOR CHECKING PURPOSES ONLY : //
            // if (string.IsNullOrEmpty(sCopy))
            //    sCopy = sFrom;
            // END SET DEFAULT VALUE ///////////////////////////
            string mailSubject = txtSubject.Text;
            if (string.IsNullOrEmpty(mailSubject))
                mailSubject = "Test Test Test";
            string mailContent = txtMessage.Text ?? string.Empty;
            string smtpServer = txtSmtpServer.Text.Trim();
            int port = Convert.ToInt32(txtSmtpPort.Text.Trim());
            
            string[] sendTo = sAddr.Split(SEMICOLON);
            string[] copyTo = sCopy.Split(SEMICOLON);
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            
            // ADDED ON 11/09/2013 : ////////////////////////////////////////////////////////////////////////
            SmtpMailData mailData = SmtpMailData.GetSmtpMailData(isSSL, port, smtpServer, sFrom, mailSubject, 
                mailContent, userName, password, sendTo, copyTo, sNickname, isNick);    // 2 last parameters added on 13/02/2020
            OnSendEmail(mailData);
            mailData = null;    //??
            // END ADDED ////////////////////////////////////////////////////////////////////////////////////
            /* OnSendEmail(isSSL, port, smtpServer, sFrom, mailSubject, mailContent,  
                userName, password, sendTo, copyTo);
            */// DISABLED ON 11/09/2013
        }

        private void OnSendEmail(bool isSSL, int port, string server, string from, string subject, string content, string userName, string password, string[] sendTo, string[] copyTo)
        {
            string status = SUCCESS;
            string InrMsg = null;
            if ((server == SmtpMailData.SMTP_HOTMAIL || server == SmtpMailData.SMTP_GMAIL) && !isSSL)
                isSSL = true;
            //Mail Message
            MailMessage message = new MailMessage();
            //Mail Address
            message.From = new MailAddress(from, /*"Mail from " + */NOREPLY, System.Text.Encoding.UTF8);
            message.Sender = new MailAddress(from, NOREPLY, System.Text.Encoding.UTF8); // OPTIONAL
            //receiver email id
            foreach (string address in sendTo)
            {
                message.To.Add(address);
            }
            //message.ReplyTo = new MailAddress("alex@rrsystems.co.il");
            // CC
            foreach (string address in copyTo)
            {
                message.CC.Add(address);
            }

            //subject of the email
            message.Subject = subject;  // "Test Test Test";
            //deciding for the attachment
            //message.Attachments.Add(new Attachment(@"C:\Documents and Settings\dudu\Desktop\jQuery_Ajax.doc"));
            //add the body of the email
            message.Body = content;
            message.Body += "\n[Message sent on " + DateTime.Now.ToShortDateString();
            message.Body += " at " + DateTime.Now.ToShortTimeString();
            message.Body += "\nvia SMTP: " + server;
            message.Body += ";     Port: " + port.ToString() + "]";
            message.IsBodyHtml = true;
            //SMTP client
            SmtpClient client = new SmtpClient(server); // working smtp account
            //SmtpClient client = new SmtpClient();
            //port number for Gmail mail
            client.Host = server;   // OPTIONAL
            client.Port = port;     // usually 25 or 587
            //credentials to login in to Gmail account
            NetworkCredential credentials = new NetworkCredential(userName, password);
            client.Credentials = credentials;   // new NetworkCredential(userName, password);  // works!!!
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //enabled SSL
            client.EnableSsl = isSSL;
            try
            {
                //Send an email
                client.Send(message);
            }
            catch (SmtpFailedRecipientsException ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    status = FAILURE + ex.Message.ToString();
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
                    status = FAILURE + ex.Message.ToString();
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
                    status = FAILURE + ex.Message.ToString();
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
                    status = FAILURE + ex.Message.ToString();
                }
                if (ex.InnerException != null)
                {
                    InrMsg = ex.InnerException.Message;
                }
            }
            finally
            {
                int pos = -1;
                if ((pos = status.IndexOf(" Learn")) != -1)
                {
                    status = status.Remove(pos);
                }
                string sMsg = "Email sending is " + status.Replace(". ", ".\n");
                COLOR fontColor = (sMsg.Contains(SUCCESS)) ? COLOR.Green : COLOR.Red;
                if (!string.IsNullOrEmpty(InrMsg))
                {
                    sMsg += '\n' + InrMsg;
                }
                lblMailingStatus.ForeColor = fontColor;
                lblMailingStatus.Text = sMsg;

                message.Dispose();
                client.Dispose();
                message = null;
                client = null;
                credentials = null;
                GC.Collect();
            }//end of catch
        }

        // OVERLOADED FUNCTION ADDED ON 11/09/2013 : //////////////////////////////////////////////
        private void OnSendEmail(SmtpMailData mailData)
        {
            string status = SUCCESS;
            string InrMsg = null;
            if ((mailData.Server == SmtpMailData.SMTP_HOTMAIL   ||
                mailData.Server == SmtpMailData.SMTP_OUTLOOK    ||
                mailData.Server == SmtpMailData.SMTP_GMAIL) && !mailData.SSL)
                mailData.SSL = true;
           
            string display = mailData.Alias; // mailData.From;
            //Mail Message
            MailMessage message = new MailMessage();
            //Mail Address
            message.From = new MailAddress(display + '<' + mailData.From + '>', display, System.Text.Encoding.UTF8);
            //message.Sender = new MailAddress(display + '<' + mailData.From + '>', display, System.Text.Encoding.UTF8);    // OPTIONAL

            //receiver email id
            foreach (string address in mailData.SendTo)
            {
                if (!string.IsNullOrEmpty(address))
                    message.To.Add(address);
            }
            //message.ReplyTo = new MailAddress("alex@rrsystems.co.il");
            // CC
            foreach (string address in mailData.CopyTo)
            {
                if (!string.IsNullOrEmpty(address))
                    message.CC.Add(address);
            }

            //subject of the email
            message.Subject = mailData.Subject;  // "Test Test Test";
            //deciding for the attachment
            //message.Attachments.Add(new Attachment(@"C:\Documents and Settings\dudu\Desktop\jQuery_Ajax.doc"));
            //add the body of the email
            message.Body = mailData.Content + "<br/>";
            message.Body += "[Message sent on " + DateTime.Now.ToShortDateString();
            message.Body += " at " + DateTime.Now.ToShortTimeString();
            message.Body += "  via SMTP: " + mailData.Server;
            message.Body += ";     Port: " + mailData.Port.ToString() + "]";
            message.IsBodyHtml = true;

            //SMTP client
            SmtpClient client = new SmtpClient(mailData.Server); // working smtp account
            //SmtpClient client = new SmtpClient();
            //port number for Gmail mail
            client.Host = mailData.Server;  // OPTIONAL
            client.Port = mailData.Port;    // usually 25 or 587

            //credentials to login in to Gmail account
            client.UseDefaultCredentials = false;   // IMPORTANT NOTE: this statement has to be declared BEFORE
                                                    // Credentials definition BUT NOT AFTER. Otherwise, Credentials
                                                    // value will be set to null that will cause exception !!!
            //NetworkCredential credentials = new NetworkCredential(mailData.UserName, mailData.Password);
            //client.Credentials = credentials;   // new NetworkCredential(userName, password);  // works!!!
            client.Credentials = new NetworkCredential(mailData.UserName, mailData.Password);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //enabled SSL
            client.EnableSsl = mailData.SSL;

            try
            {
                //Send an email
                client.Send(message);
            }
            catch (SmtpFailedRecipientsException ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    status = FAILURE + ex.Message.ToString();
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
                    status = FAILURE + ex.Message.ToString();
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
                    status = FAILURE + ex.Message.ToString();
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
                    status = FAILURE + ex.Message.ToString();
                }
                if (ex.InnerException != null)
                {
                    InrMsg = ex.InnerException.Message;
                }
            }
            finally
            {
                int pos = -1;
                if ((pos = status.IndexOf(" Learn")) != -1)
                {
                    status = status.Remove(pos);
                }
                string sMsg = "Email sending is " + status.Replace(". ", ".\n");
                COLOR fontColor = (sMsg.Contains(SUCCESS)) ? COLOR.Green : COLOR.Red;
                if (!string.IsNullOrEmpty(InrMsg))
                {
                    sMsg += '\n' + InrMsg;
                }
                lblMailingStatus.ForeColor = fontColor;
                lblMailingStatus.Text = sMsg;

                message.Dispose();
                client.Dispose();
                message = null;
                client = null;
                //credentials = null;
                GC.Collect();
            }//end of catch
        }
        // END OVERLOADED FUNCTION ////////////////////////////////////////////////////////////////

        protected void SaveEmailSettings(object sender, EventArgs e)
        {
            string sFrom = txtFrom.Text.Trim().Trim(SEMICOLON);
            string sSmtp = txtSmtpServer.Text.Trim();
            string sPort = txtSmtpPort.Text.Trim();
            string IsSSL = Convert.ToInt32(chkSSL.Checked).ToString();
            string sUserName = txtUserName.Text.Trim();
            string sPassword = txtPassword.Text.Trim();
            // added on 13/02/2020 : /////////////////
            string sNickname = txtNickname.Text.Trim();
            string sUse_Nick = Convert.ToInt32(chkNickname.Checked).ToString();
            // end added /////////////////////////////

            Dictionary<string, string> EmailSettings = new Dictionary<string, string>();
            EmailSettings.Add(Global.KEY_FROMADDR, sFrom);
            EmailSettings.Add(Global.KEY_SRVRSMTP, sSmtp);
            EmailSettings.Add(Global.KEY_PORTSMTP, sPort);
            EmailSettings.Add(Global.KEY_SSLUSAGE, IsSSL);
            EmailSettings.Add(Global.KEY_USERNAME, sUserName ?? string.Empty);
            EmailSettings.Add(Global.KEY_PASSWORD, sPassword ?? string.Empty);
            // added on 13/02/2020 : /////////////////
            EmailSettings.Add(Global.KEY_USE_NICK, sUse_Nick ?? string.Empty);
            EmailSettings.Add(Global.KEY_NICKNAME, sNickname ?? string.Empty);
            // end added on 13/02/2020 ///////////////

            OnSaveEmailSettings(EmailSettings);
        }

        protected void OnLoadPresets(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                LoadPresets();
            }
        }

        private bool OnSaveEmailSettings(Dictionary<string, string> EmailSettings)
        {
            string fullpath = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile) + 
                Global.GetRelativePath(Global.EMAIL_CONFIG);
            string localpath = Server.MapPath(Global.GetRootRelativePath(Global.EMAIL_CONFIG));
            ShowMessageBox(string.Format("Email.Config File: {0}\n({1})", localpath, fullpath));

            StreamWriter writer = new StreamWriter(localpath, false);
            if (writer == null)
                return false;
            string sError = null;

            try
            {
                if (File.Exists(localpath))
                {
                    // writer.Flush();
                    foreach (KeyValuePair<string, string> item in EmailSettings)
                    {
                        writer.WriteLine("{0}={1}", item.Key, item.Value ?? string.Empty);
                    }
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                sError = ex.Message.ToString();
            }
            catch (IOException ex)
            {
                sError = ex.Message.ToString();
            }
            catch (UnauthorizedAccessException ex)
            {
                sError = ex.Message.ToString();
            }
            catch (SecurityException ex)
            {
                sError = ex.Message.ToString();
            }
            catch (Exception ex)
            {
                sError = ex.Message.ToString();
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                    writer = null;
                }
            }

            return string.IsNullOrEmpty(sError);
        }

        private void LoadPresets()
        { 
            Dictionary<string, string> EmailSettings = GetEmailSettings();
            if (EmailSettings == null || EmailSettings.Count == 0)
                return;

            foreach (KeyValuePair<string, string> item in EmailSettings)
            {
                switch (item.Key)
                {
                    case Global.KEY_FROMADDR:
                        txtFrom.Text = item.Value;
                        break;
                    case Global.KEY_SRVRSMTP:
                        txtSmtpServer.Text = item.Value;
                        break;
                    case Global.KEY_PORTSMTP:
                        txtSmtpPort.Text = item.Value;
                        break;
                    case Global.KEY_SSLUSAGE:
                        chkSSL.Checked = Convert.ToBoolean(Convert.ToInt32(item.Value));
                        break;
                    case Global.KEY_USERNAME:
                        txtUserName.Text = item.Value;
                        break;
                    case Global.KEY_PASSWORD:
                        txtPassword.TextMode = TextBoxMode.SingleLine;
                        txtPassword.Attributes["type"] = "password";
                        txtPassword.Text = item.Value;
                        break;
                    // added on 12/02/2020 : /////////
                    case Global.KEY_USE_NICK:
                        chkNickname.Checked = Convert.ToBoolean(Convert.ToInt32(item.Value));
                        break;
                    case Global.KEY_NICKNAME:
                        txtNickname.Text = item.Value ?? Global.SENDER_NICK;
                        break;
                    // end added on 12/02/2020 ///////
                }
            }
        }

        private Dictionary<string, string> GetEmailSettings()
        {
            string fullpath = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile) +
                Global.GetRelativePath(Global.EMAIL_CONFIG);
            string localpath = Server.MapPath(Global.GetRootRelativePath(Global.EMAIL_CONFIG));
            if (!IsPostBack)    // condition added on 22/02/2016  
            {
                ShowMessageBox(string.Format("Email.Config File: {0}\n({1})", localpath, fullpath));
            }
            Dictionary<string, string> data = Global.GetEmailSettings(localpath);
            
            return data;
        }

        private void ShowMessageBox(string message)
        {
            lblPath.Visible = true;
            lblPath.Text = message;
        }

        protected void wsIPtextBox_TextChanged(object sender, EventArgs e)
        {

        }

        //    protected void chkNickname_CheckedChanged(object sender, EventArgs e)
        //    {
        //        if ((sender as CheckBox).Checked)
        //        {
        //            lblNickname.Visible = true;
        //            txtNickname.Visible = true;
        //        }
        //        else
        //        {
        //            lblNickname.Visible = false;
        //            txtNickname.Visible = false;
        //        }
        //    }
    }
}