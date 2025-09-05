using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Windows.Forms; // added 4 test
using RRappointmentsWebApplication.BuisnessLogicLayer;
using WSLI = RRappointmentsWebApplication.BuisnessLogicLayer.WebServicesLocationInfo;

// ADDED ON 11/08/2013 FOR REMOTE WEB SERVICES DEFINITION : //////////////////////////////////////////////////////////////////////////
using WebServicePasswordRecovery = RRappointmentsWebApplication.PasswordRecoveryWebReference.PasswordRecovery;
using WebServicePasswordRecoveryData = RRappointmentsWebApplication.PasswordRecoveryWebReference.UserPasswordRecoveryData;

// ADDED ON 03/09/2013 FOR LOCAL WEB SERVICES DEFINITION : ///////////////////////////////////////////////////////////////////////////
using WebServicePasswordRecoveryLOCAL = RRappointmentsWebApplication.PasswordRecoveryLocalWebReference.PasswordRecovery;
using WebServicePasswordRecoveryDataLOCAL = RRappointmentsWebApplication.PasswordRecoveryLocalWebReference.UserPasswordRecoveryData;

// ADDED ON 25/09/2013 FOR REMOTE WEB SERVICES DEFINITION : //////////////////////////////////////////////////////////////////////////
using WebServiceEmailSender = RRappointmentsWebApplication.EmailSenderWebReference.EmailSender;
// ADDED ON 24/09/2013 FOR LOCAL WEB SERVICES DEFINITION : ///////////////////////////////////////////////////////////////////////////
using WebServiceEmailSenderLOCAL = RRappointmentsWebApplication.EmailSenderLocalWebReference.EmailSender;
// ADDED ON 04/10/2020 :
using System.Net;
using System.IO;
// END WEB SERVICES RELATED ADDITIONS ////////////////////////////////////////////////////////////////////////////////////////////////

public partial class forgot_pass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {   // added on 21/07/2013 : ////////////////
        if (!IsPostBack)
        {
            RegisterClientScriptWebServicesLocation();
        }
        //send.Attributes.Add("onclick", "javascript:" + send.ClientID + ".disabled = true;" + ClientScript.GetPostBackEventReference(send, "")); // ADDED FOR TEST
        //send.OnClientClick = ClientScript.GetPostBackEventReference(send, "") + "; this.disabled = true;";    // ADDED FOR TEST
        // end additions ////////////////////////
    }

    protected void send_Click(object sender, ImageClickEventArgs e)
    {
        //((ImageButton)sender).Enabled = false;  // TEST
        //send.Enabled = false;   // TEST
        //send.Style.Add("disabled", "disabled"); // TEST

        if (IsPostBack)
        {
            string userId = idTextBox.Text.ToString();
            if (!string.IsNullOrEmpty(userId))
            {
                string status = GetUserPasswordData(userId);
                if (string.IsNullOrEmpty(status))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "if(top!=self) {top.location.href = 'PostPasswordRecovery.aspx';}", true);
                }
                else
                {
                    idErrorLabel.Text = status;
                    idErrorLabel.Style.Add("visibility", "visible");
                    Session.Abandon();
                }
            }
        }
    }

    private string GetUserPasswordData(string userId)
    {
        string error = string.Empty;
        // corrections and additions since 03/09/2013 : ///////////////////////////////////////////
        bool isRemote = (Application[WSLI.WEBSRV_ISREMOTE].ToString() == "false") ? false : true;
        WebServicePasswordRecovery wsPasswordRecovery = null;
        WebServicePasswordRecoveryLOCAL wsPasswordRecoveryLocal = null;
        WebServicePasswordRecoveryData wsPasswordRecoveryData = null;
        WebServicePasswordRecoveryDataLOCAL wsPasswordRecoveryDataLocal = null;

        if (isRemote)
        {
            wsPasswordRecovery = new WebServicePasswordRecovery();
            if (wsPasswordRecovery == null)
            {
                return Global.FAILURE_MSG;
            }

            // condition added here on 29/07/2020 for password update : //
            if (GeneralStaticLogic.PwdUpdate)
            {
                if (!wsPasswordRecovery.OnUpdateUserPassword(userId))
                    return Global.PWD_UPD_ERROR;
                GeneralStaticLogic.PwdUpdate = false;
            }
            // end condition added here on 29/07/2020 for password update 
            wsPasswordRecoveryData = wsPasswordRecovery.OnRecoverUserPassword(userId);
            if (wsPasswordRecoveryData == null)
            {
                return Global.FAILURE_MSG;
            }

            if (string.IsNullOrEmpty(wsPasswordRecoveryData.ClientPassword))
            {
                return Global.UNFOUND_MSG;
            }

            SendUserPasswordByPhone(ref error, wsPasswordRecoveryData);
            if (!string.IsNullOrEmpty(wsPasswordRecoveryData.ClientEmail))
            {
                // SendUserPasswordByEmail(ref error, wsPasswordRecoveryData);
                // ADDED ON 24/09/2013 : //////////////////////////////////////////////////////////
                if (!SendUserPasswordByEmail(wsPasswordRecoveryData.ClientEmail, wsPasswordRecoveryData.SenderEmailTitle, wsPasswordRecoveryData.RecoveryMsgEmail))
                    error = Global.MAIL_ERROR;
            }
        }
        else
        {
            wsPasswordRecoveryLocal = new WebServicePasswordRecoveryLOCAL();
            if (wsPasswordRecoveryLocal == null)
            {
                return Global.FAILURE_MSG;
            }

            // condition added here on 29/07/2020 for password update : //
            if (GeneralStaticLogic.PwdUpdate)
            {
                if (!wsPasswordRecoveryLocal.OnUpdateUserPassword(userId))
                    return Global.PWD_UPD_ERROR;
                GeneralStaticLogic.PwdUpdate = false;
            }
            // end condition added here on 29/07/2020 for password update  
            wsPasswordRecoveryDataLocal = wsPasswordRecoveryLocal.OnRecoverUserPassword(userId);
            if (wsPasswordRecoveryDataLocal == null)
            {
                return Global.FAILURE_MSG;
            }

            if (string.IsNullOrEmpty(wsPasswordRecoveryDataLocal.ClientPassword))
            {
                return Global.UNFOUND_MSG;
            }

            SendUserPasswordByPhone(ref error, wsPasswordRecoveryDataLocal);
            if (!string.IsNullOrEmpty(wsPasswordRecoveryDataLocal.ClientEmail))
            {
                // SendUserPasswordByEmail(ref error, wsPasswordRecoveryDataLocal);
                // ADDED ON 24/09/2013 : //////////////////////////////////////////////////////////
                if (!SendUserPasswordByEmail(wsPasswordRecoveryDataLocal.ClientEmail, wsPasswordRecoveryDataLocal.SenderEmailTitle, wsPasswordRecoveryDataLocal.RecoveryMsgEmail))
                    error = Global.MAIL_ERROR;
            }
        }

        return error;
    }

    private void SendUserPasswordByPhone(ref string error, WebServicePasswordRecoveryData wsData)
    {
        var serviceUrl = wsData.SenderUrl;  // Global.SMS_SENDER_URL;
        serviceUrl += "?" + "CellNumber=" + wsData.ClientCellNumber;
        serviceUrl += "&" + "MessageString=" + wsData.RecoveryMsgSMS;
        serviceUrl += "&" + "SenderCellNumber=" + wsData.SenderCellNumber;
        serviceUrl += "&" + "UserName=" + wsData.SenderName;
        serviceUrl += "&" + "Password=" + wsData.SenderPassword;

        // ADDED ON 04/10/2020 :///////////////////////////
        string RetStr;
        WebRequest request = WebRequest.Create(serviceUrl);

        WebResponse response = request.GetResponse();
        Stream dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        RetStr = reader.ReadToEnd();
        reader.Close();
        response.Close();
        if (RetStr == "0")
        {
            // Ret = "OK";
        }
        else
        {
            // Ret = "Error: unrecognized from web";
        }
        // END ADDED ON 04/10/2020 ////////////////////////

        string script = Global.GetSenderWindowScript(serviceUrl);
        Response.Write(script);
    }

    // OVERLOADED METHODS ADDED ON 03/09/2013 : ////////////////////////////////////////////////////////
    private void SendUserPasswordByPhone(ref string error, WebServicePasswordRecoveryDataLOCAL wsData)
    {
        var serviceUrl = wsData.SenderUrl; // Global.SMS_SENDER_URL; // "http://wap.y-it.co.il:8080/wapdb/ws_send_sms";
        serviceUrl += "?" + "CellNumber=" + wsData.ClientCellNumber;
        serviceUrl += "&" + "MessageString=" + wsData.RecoveryMsgSMS;
        serviceUrl += "&" + "SenderCellNumber=" + wsData.SenderCellNumber;
        serviceUrl += "&" + "UserName=" + wsData.SenderName;
        serviceUrl += "&" + "Password=" + wsData.SenderPassword;

        // ADDED ON 04/10/2020 : //////////////////////////
        WebRequest request = WebRequest.Create(serviceUrl);
        string RetStr;
        WebResponse response = request.GetResponse();
        Stream dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        RetStr = reader.ReadToEnd();
        reader.Close();
        response.Close();
        if (RetStr == "0")
        {
            // Ret = "OK";
        }
        else
        {
            // Ret = "Error: unrecognized from web";
        }
        // END ADDED ON 04/10/2020 ////////////////////////

        string script = Global.GetSenderWindowScript(serviceUrl);
        Response.Write(script);
    }

    // END OVERLOADED METHODS //////////////////////////////////////////////////////////////////////////

    private string GetSenderWindowScript(string serviceUrl)
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

    private void RegisterClientScriptWebServicesLocation()
    {
        foreach (string key in Application.AllKeys)
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (!ClientScript.IsStartupScriptRegistered(key))
                {
                    string script = WSLI.GetClientScriptValueString(key, Application[key].ToString());
                    ClientScript.RegisterStartupScript(GetType(), key, script, true);
                }
            }
        }
    }

    // ADDED ON 24/09/2013 : //////////////////////////////////////////////////////////////////////
    private bool SendUserPasswordByEmail(string recipient, string subject, string content)
    {
        bool isSent = false;
        string localpath = Server.MapPath(Global.GetRootRelativePath(Global.EMAIL_CONFIG));
        Dictionary<string, string> EmailSettings = Global.GetEmailSettings(localpath);
        if (EmailSettings == null || EmailSettings.Count == 0)
            return isSent;

        string mailSender = null, smtpServer = null, userName = null, password = null, nickName = null;
        int smtpPort = Global.DEFAULT_PORT;
        bool useSsl = false, useNick = false;
        // mailSender = Global.GetEmailSenderSettings(ref smtpServer, ref userName, ref password, ref smtpPort, ref useSsl, EmailSettings); // commented on 24/02/2020
        mailSender = Global.GetEmailSenderSettings(ref smtpServer, ref userName, ref password, ref nickName, ref smtpPort, ref useSsl, ref useNick, EmailSettings); // inserted on 24/02/2020
        if (string.IsNullOrEmpty(mailSender))
            return isSent;

        bool isRemote = (Application[WSLI.WEBSRV_ISREMOTE].ToString() == "false") ? false : true;
        WebServiceEmailSenderLOCAL wsEmailSenderLocal = null;
        WebServiceEmailSender wsEmailSender = null;
        if (isRemote)
        {
            wsEmailSender = new WebServiceEmailSender();
            if (wsEmailSender == null)
                return isSent;

            isSent = wsEmailSender.SendEmail(recipient, subject, content, mailSender, smtpServer, smtpPort, useSsl, userName, password);
        }
        else
        {
            wsEmailSenderLocal = new WebServiceEmailSenderLOCAL();
            if (wsEmailSenderLocal == null)
                return isSent;

            //isSent = wsEmailSenderLocal.SendEmail(recipient, subject, content, mailSender, smtpServer, smtpPort, useSsl, userName, password); // commented on 25/02/2020
            isSent = wsEmailSenderLocal.SendEmailExt(recipient, subject, content, mailSender, smtpServer, userName, password, nickName, smtpPort, useSsl, useNick); // inserted on 25/02/2020
        }

        return isSent;
    }
    // END ADDED //////////////////////////////////////////////////////////////////////////////////
}