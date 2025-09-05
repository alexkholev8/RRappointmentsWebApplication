using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MSCaptcha;
using System.Drawing;
using System.Data;
using RRappointmentsWebApplication.BuisnessLogicLayer;
using System.Web.UI.HtmlControls;
using System.Text;
using WSLI = RRappointmentsWebApplication.BuisnessLogicLayer.WebServicesLocationInfo;

// ADDED ON 11/08/2013 FOR REMOTE WEB SERVICES DEFINITION : //////////////////////////////////////////////////////////////////////////
using WebServicePasswordRecovery = RRappointmentsWebApplication.PasswordRecoveryWebReference.PasswordRecovery;
using WebServicePasswordRecoveryData = RRappointmentsWebApplication.PasswordRecoveryWebReference.UserPasswordRecoveryData;
using WebServiceNewUserSignUp = RRappointmentsWebApplication.NewUserSignUpWebReference.NewUserSignUp;

// ADDED ON 03/09/2013 FOR LOCAL WEB SERVICES DEFINITION : ///////////////////////////////////////////////////////////////////////////
using WebServicePasswordRecoveryLOCAL = RRappointmentsWebApplication.PasswordRecoveryLocalWebReference.PasswordRecovery;
using WebServicePasswordRecoveryDataLOCAL = RRappointmentsWebApplication.PasswordRecoveryLocalWebReference.UserPasswordRecoveryData;
using WebServiceNewUserSignUpLOCAL = RRappointmentsWebApplication.NewUserSignUpLocalWebReference.NewUserSignUp;

// ADDED ON 25/09/2013 FOR REMOTE WEB SERVICES DEFINITION : //////////////////////////////////////////////////////////////////////////
using WebServiceEmailSender = RRappointmentsWebApplication.EmailSenderWebReference.EmailSender;
// ADDED ON 24/09/2013 FOR LOCAL WEB SERVICES DEFINITION : ///////////////////////////////////////////////////////////////////////////
using WebServiceEmailSenderLOCAL = RRappointmentsWebApplication.EmailSenderLocalWebReference.EmailSender;
// END WEB SERVICES RELATED ADDITIONS //////////////////////////////////////////////////////////////////////////////////////////////// 

public partial class new_user : System.Web.UI.Page
{
    private const string EXISTS = "Exists";
    private const string FAILED = "Failed";

    protected void Page_Load(object sender, EventArgs e)
    {
        // ADDED ON 23/06/2013 : ////////////////
        if (!IsPostBack)
        {
            var mode = ConfigurationManager.AppSettings["ajaxTransferingMode"].ToString();
            RegisterClientScript(mode);
            RegisterClientScriptWebServicesLocation();  // added on 21/07/2013
        }
        // END ADDITIONS 
 
    }

    protected void submitSignUp_Click(object sender, ImageClickEventArgs e)
    {
        if (IsPostBack)
        {
            bool isRemote = (Application[WSLI.WEBSRV_ISREMOTE].ToString() == "false") ? false : true; // added on 03/09/2013
            UserInfo newUserInfo = new UserInfo()
            {
                First_name = firstNameTextBox.Text,
                LastName = lastNameTextBox.Text,
                Id_num = idTextBox.Text,
                Email = emailTextBox.Text,
                AreaCode = areaCodeDropDownList.SelectedItem.ToString(),
                PhoneNumber = phoneTextBox.Text
            };
            string status = NewUserSignUp(newUserInfo, isRemote);   // corrected on 03/09/2013
            if (string.IsNullOrEmpty(status))
            {
                status = FAILED;
            }
            if (status == EXISTS || status == FAILED)
            {
                signUpErrorLabel.Text = (status == EXISTS) ?
                    (Global.EXISTS_MSG) : (Global.FAILURE_MSG);
                signUpErrorLabel.Style.Add("visibility", "visible");
                signUpErrorLabel.BorderColor = Color.Red;
                // Session.Abandon();
            }
            else
            {
                newUserInfo.Password = status;
                status = GetNewUserPasswordData(newUserInfo, isRemote); // corrected on 03/09/2013
                if (string.IsNullOrEmpty(status))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "if(top!=self) {top.location.href = 'PostSignUp.aspx';}", true);
                }
                else
                {
                    signUpErrorLabel.Text = status;
                    signUpErrorLabel.Style.Add("visibility", "visible");
                    signUpErrorLabel.BorderColor = Color.Red;
                    // Session.Abandon();
                }         
            }
        }
    }

    // CORRECTIONS AND ADDITIONS SINCE 03/09/2013 : ////////////////////////////////////////////////////
    private string NewUserSignUp(UserInfo newUserInfo, bool isRemote)
    {
        string status = null;
        WebServiceNewUserSignUp wsNewUserSignUp = null;
        WebServiceNewUserSignUpLOCAL wsNewUserSignUpLocal = null;
        if (isRemote)
        {
            wsNewUserSignUp = new WebServiceNewUserSignUp();
            if (wsNewUserSignUp == null)
            {
                return FAILED;
            }
            status = wsNewUserSignUp.SignUpNewUserPerParams(newUserInfo.Id_num,
            newUserInfo.First_name,
            newUserInfo.LastName,
            newUserInfo.Email,
            newUserInfo.AreaCode,
            newUserInfo.PhoneNumber,
            newUserInfo.Password);
        }
        else 
        {
            wsNewUserSignUpLocal = new WebServiceNewUserSignUpLOCAL();
            if (wsNewUserSignUpLocal == null)
            {
                return FAILED;
            }
            status = wsNewUserSignUpLocal.SignUpNewUserPerParams(newUserInfo.Id_num,
            newUserInfo.First_name,
            newUserInfo.LastName,
            newUserInfo.Email,
            newUserInfo.AreaCode,
            newUserInfo.PhoneNumber,
            newUserInfo.Password);
        }
        
        return status;
    }

    private string GetNewUserPasswordData(UserInfo newUserInfo, bool isRemote)
    {
        string error = string.Empty;
        WebServicePasswordRecovery wsPasswordRecovery = null;
        WebServicePasswordRecoveryData wsPasswordRecoveryData = null;
        WebServicePasswordRecoveryLOCAL wsPasswordRecoveryLocal = null;
        WebServicePasswordRecoveryDataLOCAL wsPasswordRecoveryDataLocal = null;

        if (isRemote)
        {
            wsPasswordRecovery = new WebServicePasswordRecovery();
            if (wsPasswordRecovery == null)
            {
                return FAILED;
            }

            wsPasswordRecoveryData = wsPasswordRecovery.GetNewUserPasswordData(newUserInfo.Id_num,
                newUserInfo.Email,
                newUserInfo.PhoneNumber,
                newUserInfo.AreaCode,
                newUserInfo.Password);

            if (wsPasswordRecoveryData == null)
            {
                return FAILED;
            }

            if (string.IsNullOrEmpty(wsPasswordRecoveryData.ClientPassword))
            {
                return FAILED;
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
                return FAILED;
            }

            wsPasswordRecoveryDataLocal = wsPasswordRecoveryLocal.GetNewUserPasswordData(newUserInfo.Id_num,
                newUserInfo.Email,
                newUserInfo.PhoneNumber,
                newUserInfo.AreaCode,
                newUserInfo.Password);

            if (wsPasswordRecoveryDataLocal == null)
            {
                return FAILED;
            }

            if (string.IsNullOrEmpty(wsPasswordRecoveryDataLocal.ClientPassword))
            {
                return FAILED;
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

        string script = Global.GetSenderWindowScript(serviceUrl);
        Response.Write(script);
    }

    // OVERLOADED METHODS ADDED ON 03/09/2013 : ////////////////////////////////////////////////////////
    private void SendUserPasswordByPhone(ref string error, WebServicePasswordRecoveryDataLOCAL wsData)
    {
        var serviceUrl = wsData.SenderUrl;  // Global.SMS_SENDER_URL; //
        serviceUrl += "?" + "CellNumber=" + wsData.ClientCellNumber;
        serviceUrl += "&" + "MessageString=" + wsData.RecoveryMsgSMS;
        serviceUrl += "&" + "SenderCellNumber=" + wsData.SenderCellNumber;
        serviceUrl += "&" + "UserName=" +  wsData.SenderName;
        serviceUrl += "&" + "Password=" + wsData.SenderPassword;

        string script = Global.GetSenderWindowScript(serviceUrl);
        Response.Write(script);
    }
    // END OVERLOADED METHODS //////////////////////////////////////////////////////////////////////////
    // END ADDITIONS AND CORRECTIONS ///////////////////////////////////////////////////////////////////

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

    private void RegisterClientScript(string sMode)
    {
        string scriptMode = string.Format("var mode = '{0}';", sMode);
        if (!ClientScript.IsClientScriptBlockRegistered("Mode"))
        {
            ClientScript.RegisterClientScriptBlock(typeof(string), "Mode", scriptMode, true);
        }
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
