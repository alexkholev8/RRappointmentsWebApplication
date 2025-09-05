using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Xml;
using System.Net.Mail;
using System.Net;
using System.Web.Security;
using System.Configuration;
using RRappointmentsWebApplication.BuisnessLogicLayer;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using WSLI = RRappointmentsWebApplication.BuisnessLogicLayer.WebServicesLocationInfo;

// ADDED ON 08/08/2013 FOR REMOTE WEB SERVICE DEFINITION : /////////////////////////////////////////////
using WebServiceUserLogin = RRappointmentsWebApplication.UserLoginWebReference.UserLogin;
using WebServiceSessionInfo = RRappointmentsWebApplication.UserLoginWebReference.SessionInfo;

// ADDED ON 04/09/2013 FOR LOCAL WEB SERVICE DEFINITION : //////////////////////////////////////////////
using WebServiceUserLoginLOCAL = RRappointmentsWebApplication.UserLoginLocalWebReference.UserLogin;
using WebServiceSessionInfoLOCAL = RRappointmentsWebApplication.UserLoginLocalWebReference.SessionInfo;
// END WEB SERVICES RELATED ADDITIONS //////////////////////////////////////////////////////////////////
 
public partial class step1 : System.Web.UI.Page
{
    private const string ADMIN_ID   = "admin";
    private const string PASSWORD   = "134679";
    private const string SUCCESS    = "OK";
    private const string FAILURE    = "שגיאת WebService. חיבור עם WebService מרחוק נכשל.";

    protected void logInImageButton_Click(object sender, ImageClickEventArgs e)
    {
        if (IsPostBack)
        {
            string userId = IdTextBox.Text.ToString();
            string password = passwordTextBox.Text.ToString();
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(password))
            {
                bool isAdminLogin = (IdTextBox.Text == ADMIN_ID && passwordTextBox.Text == PASSWORD);
                if (isAdminLogin)
                {
                    AdministratorLogIn();
                }

                string status = CheckUserLogIn(userId, password);
                if (status != SUCCESS)
                {
                    //Response.Write(status);
                    logInErrorLabel.Text = status;
                    logInErrorLabel.Style.Add("visibility", "visible");
                    SetInputBoxWarningBorder(); // added on 01/09/2015
                    GeneralStaticLogic.UserLogIn = false;
                    Session.Abandon();
                }
            }  
        }
    }

    // ADDED ON 01/09/2015 : ////////////////////
    void SetInputBoxWarningBorder()
    {
        IdTextBox.Style.Add("border-color", "Red");
        passwordTextBox.Style.Add("border-color", "Red");
    }
    // END ADDED

    // ADDITIONS AND CORRECTIONS SINCE 04/09/2013 : ///////////////////////////////////////////////
    private string CheckUserLogIn(string userId, string password)
    {
        string status = string.Empty;
        bool isRemote = (Application[WSLI.WEBSRV_ISREMOTE].ToString() == "false") ? false : true;
        WebServiceUserLogin userLogIn = null;
        WebServiceUserLoginLOCAL userLogInLocal = null;
        WebServiceSessionInfo wsSessionInfo = null;
        WebServiceSessionInfoLOCAL wsSessionInfoLocal = null;
        SessionInfo sessionInfo = null;
     
        if (isRemote)
        {
            userLogIn = new WebServiceUserLogin();
            if (userLogIn == null)
            {
                return FAILURE;
            }

            wsSessionInfo = userLogIn.CheckUserLogin(password, userId);
            if (wsSessionInfo != null)
            {
                status = wsSessionInfo.LoginStatus;
            }
        }
        else
        {            
            userLogInLocal = new WebServiceUserLoginLOCAL();
            if (userLogInLocal == null)
            {
                return FAILURE;
            }

            wsSessionInfoLocal = userLogInLocal.CheckUserLogin(password, userId);
            if (wsSessionInfoLocal != null)
            {
                status = wsSessionInfoLocal.LoginStatus;
            }
                           
        }

        if (status == SUCCESS)
        {
            sessionInfo = (isRemote) ?  GetSessionInfo(wsSessionInfo) : 
                                        GetSessionInfo(wsSessionInfoLocal);
            Session["session"] = sessionInfo;
            FormsAuthentication.RedirectFromLoginPage((Session["session"] as SessionInfo).Id_num, false);
            GeneralStaticLogic.UserLogIn = true;
            // added on 29/07/2020 for password update : ////////
            SiteConfigurationLogic.LoadConfigurations();
            if (bool.Parse(string.IsNullOrEmpty(SiteConfigurationLogic.PasswordUpdateUse) ? "False" : SiteConfigurationLogic.PasswordUpdateUse))
            {
                if (DateTime.Now.Date > sessionInfo.ValidPassword.AddDays(double.Parse(SiteConfigurationLogic.PasswordDaysCount)))  // 1==1 /*TEST*/ || 
                {
                    GeneralStaticLogic.PwdUpdate = true;
                    Response.Redirect("update_pass.aspx");

                }
            }            
            // end added on 29/07/2020 for password update //////
            Response.Redirect("step2.aspx");
        }

        return status;
    }

    // OVERLOADED METHODS ADDED ON 04/09/2013 : ///////////////////////////////////////////////////
    SessionInfo GetSessionInfo(WebServiceSessionInfo wsSessionInfo)
    {
        return new SessionInfo
        {
            Cust_num        = wsSessionInfo.Cust_num,
            FileNum         = wsSessionInfo.FileNum,
            First_name      = wsSessionInfo.First_name,
            Id_num          = wsSessionInfo.Id_num,
            LastName        = wsSessionInfo.LastName,
            LoginStatus     = wsSessionInfo.LoginStatus,
            UserPassword    = wsSessionInfo.UserPassword,
            ValidPassword   = wsSessionInfo.ValidPassword // added on 29/07/2020 for password update
        };
    }

    SessionInfo GetSessionInfo(WebServiceSessionInfoLOCAL wsSessionInfo)
    {
        return new SessionInfo
        {
            Cust_num        = wsSessionInfo.Cust_num,
            FileNum         = wsSessionInfo.FileNum,
            First_name      = wsSessionInfo.First_name,
            Id_num          = wsSessionInfo.Id_num,
            LastName        = wsSessionInfo.LastName,
            LoginStatus     = wsSessionInfo.LoginStatus,
            UserPassword    = wsSessionInfo.UserPassword,
            ValidPassword   = wsSessionInfo.ValidPassword   // added on 29/07/2020 for password update
        };
    }
    // END ADDITIONS AND CORRECTIONS //////////////////////////////////////////////////////////////

    private void AdministratorLogIn()
    {
        GeneralStaticLogic.UserLogIn = true;
        Response.Redirect("ConfigurationPage.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "כניסה - מערכת זימון התורים של מאר";
        // Added on 21/07/2013 ///////////////////////////////////////////
        if (!IsPostBack)
        {
            CheckApplicationConfiguration();
            RegisterClientScriptWebServicesLocation();
        }
        // End added /////////////////////////////////////////////////////

        // ADDED FOR TEST ONLY !!!
        
        // END TEST
    }

    [WebMethod(EnableSession=true)]
    public static void SetSession(SessionInfo jsonSessionInfo)
    {
        HttpContext.Current.Session["session"] = jsonSessionInfo;
        //GeneralStaticLogic.UserLogIn = true;
        //HttpContext.Current.Session.Add("session"], jsonSessionInfo);
    }

    [WebMethod(EnableSession = true)]
    public static void SetSessionPerParams(string jsonIdNum, string jsonFileNum, string jsonPassword, string jsonFirstName, string jsonLastName, string jsonCustNum, string jsonStatus)
    {
        SessionInfo sessionInfo = new SessionInfo();
        sessionInfo.Id_num  = jsonIdNum;
        sessionInfo.FileNum = jsonFileNum;
        sessionInfo.UserPassword = jsonPassword;
        sessionInfo.First_name = jsonFirstName;
        sessionInfo.LastName = jsonLastName;
        sessionInfo.Cust_num = jsonCustNum;
        sessionInfo.LoginStatus = jsonStatus;

        HttpContext.Current.Session["session"] = sessionInfo;
        GeneralStaticLogic.UserLogIn = true;    // ADDED
    }

    private void CheckApplicationConfiguration()
    {
        WebServicesLocationInfo.Directory = ConfigurationManager.AppSettings["WebServiceDirectory"];
        WebServicesLocationInfo.IPAddress = ConfigurationManager.AppSettings["WebServiceIP"];
        WebServicesLocationInfo.Port = ConfigurationManager.AppSettings["WebServicePort"];
        WebServicesLocationInfo.IsRemote = (ConfigurationManager.AppSettings["WebServiceLocation"].Equals("1")) ?
            ("true") : ("false");
        WebServicesLocationInfo.SyncMode = ConfigurationManager.AppSettings["WebServiceSyncMode"];  // added on 08/08/2013

        if (Application[WSLI.WEBSRV_ISREMOTE] == null)
            Application[WSLI.WEBSRV_ISREMOTE] = WSLI.IsRemote;
        if (Application[WSLI.WEBSRV_DIRECTORY] == null)
            Application[WSLI.WEBSRV_DIRECTORY] = WSLI.Directory;
        if (Application[WSLI.WEBSRV_IPADDRESS] == null)
            Application[WSLI.WEBSRV_IPADDRESS] = WSLI.IPAddress;
        if (Application[WSLI.WEBSRV_PORT] == null)
            Application[WSLI.WEBSRV_PORT] = WSLI.Port;
        // added on 08/08/2013 : ///////////////////////////////
        if (Application[WSLI.WBSRV_SYNCMODE] == null)
            Application[WSLI.WBSRV_SYNCMODE] = WSLI.SyncMode;
        // end added ///////////////////////////////////////////
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

    // ADDED 4 TEST
    [WebMethod(EnableSession = true)]
    public static void WriteLog(string jsonFileName, string jsonLogLine)
    {
        string fileName = string.Format(@"\\dc1\server2008\E\diskf\Zimon\Log\service_({0}).txt", DateTime.Now.ToShortDateString().Replace('/', '.'));
        StreamWriter writer = null;
        try
        {
            if (writer == null)
                writer = new StreamWriter(jsonFileName, true);
            writer.WriteLine(jsonLogLine + ";");
        }
        catch (IOException ex)
        {
            Console.WriteLine(ex.Message.ToString());
            //if (writer != null)
            //    writer.Close();
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
        HttpContext.Current.Session.Abandon();
        GeneralStaticLogic.UserLogIn = false;
    }
    // END ADDED
}