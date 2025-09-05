using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RRappointmentsWebApplication.BuisnessLogicLayer;
using System.Xml;
using System.Configuration;
using System.Text;
using RRappointmentsWebApplication;
using System.Web.Security;
using System.IO;
using System.ServiceModel;  // added on 16/09/2018
using System.Windows.Forms; // added on 26/01/2014
using System.Diagnostics;   // added on 07/02/2016 for test purposes
using WSLI = RRappointmentsWebApplication.BuisnessLogicLayer.WebServicesLocationInfo;

// ADDED FOR REMOTE WEB SERVICE DEFINITION : ///////////////////////////////////////////////////////////////////////////////
using WebServiceAddNewAppointment = RRappointmentsWebApplication.NewAppointmentWebReference.NewAppointment;
using WebServiceAppointmentMailData = RRappointmentsWebApplication.NewAppointmentWebReference.AppointmentMailData;

// ADDED ON 04/09/2013 FOR LOCAL WEB SERVICE DEFINITION : //////////////////////////////////////////////////////////////////
using WebServiceAddNewAppointmentLOCAL = RRappointmentsWebApplication.NewAppointmentLocalWebReference.NewAppointment;
using WebServiceAppointmentMailDataLOCAL = RRappointmentsWebApplication.NewAppointmentLocalWebReference.AppointmentMailData;

// ADDED ON 25/09/2013 FOR REMOTE WEB SERVICES DEFINITION : //////////////////////////////////////////////////////////////////////////
using WebServiceEmailSender = RRappointmentsWebApplication.EmailSenderWebReference.EmailSender;
// ADDED ON 24/09/2013 FOR LOCAL WEB SERVICES DEFINITION : ///////////////////////////////////////////////////////////////////////////
using WebServiceEmailSenderLOCAL = RRappointmentsWebApplication.EmailSenderLocalWebReference.EmailSender;
// END WEB SERVICES RELATED ADDITIONS ////////////////////////////////////////////////////////////////////////////////////////////////

// FOR WCF SERVICE DIARIES COUNTER DEFINITION : //////////////////////////////////////////////////////////////////////////////////////
//using WcfDiariesCounter = RRappointmentsWebApplication.WcfDiariesCounterService.DiariesCounterClient; // commented on 26/04/2015
// END WCF SERVICE DIARIES COUNTER DEFINITION ////////////////////////////////////////////////////////////////////////////////////////

public partial class step3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //AssignedAppointment createdAppointment = WebServiceAppInfo.CreatedAppointments[(Session["session"] as SessionInfo).Id_num];
        if (!IsPostBack)
        {
            if (PreviousPage == null)
            {
                Server.Transfer("index.aspx");
            }
      //}   // - commented here

            // corrections and additions since 17/06/2013 : ///////////////////////////////////////////
            AssignedAppointment createdAppointment = WebServiceAppInfo.CreatedAppointments[(Session["session"] as SessionInfo).Id_num];
            nameLabel.Text      = createdAppointment.FirstName + " " + createdAppointment.LastName;  //(Session["session"] as SessionInfo).LastName;
            dateLabel.Text      = createdAppointment.AppointmentDate;
            timeLabel.Text      = createdAppointment.Time;
            inuranceLabel.Text  = createdAppointment.Insurance;
            siteLabel.Text      = createdAppointment.Site;
            treatLabel.Text     = createdAppointment.Treatment;
            dayLabel.Text       = createdAppointment.Day;
            addressLabel.Text   = createdAppointment.Address;   // added on 11/10/2018
            telephoneLabel.Text = createdAppointment.Phone;     // added on 11/10/2018
            RegisterClientScript(createdAppointment);
            RegisterClientScriptWebServicesLocation();  // added on 21/07/2013
        }   // - enabled here
        // end corrections and additions ////////////////////////////////////////////////////////// 
    }

    /// <summary>
    /// Method enables visibility od server's variable from the client side
    /// </summary>
    /// <param name="createdAppointment"></param>
    private void RegisterClientScript(AssignedAppointment createdAppointment)
    {
        string scriptCustCode = string.Format("var custCode = '{0}';", createdAppointment.CustCode);
        string scriptSiteCode = string.Format("var siteCode = '{0}';", createdAppointment.SiteCode);
        string scriptTreatCode = string.Format("var treatCode = '{0}';", createdAppointment.TreatCode);
        string scriptInsurCode = string.Format("var insurCode = '{0}';", createdAppointment.InuranceCode);
        string scriptDline = string.Format("var dline = '{0}';", createdAppointment.Dline);

        if (!ClientScript.IsClientScriptBlockRegistered("CustCode"))
        {
            ClientScript.RegisterClientScriptBlock(typeof(string), "CustCode", scriptCustCode, true);
        }
        if (!ClientScript.IsClientScriptBlockRegistered("SiteCode"))
        {
            ClientScript.RegisterClientScriptBlock(typeof(string), "SiteCode", scriptSiteCode, true);
        }
        if (!ClientScript.IsClientScriptBlockRegistered("TreatCode"))
        {
            ClientScript.RegisterClientScriptBlock(typeof(string), "TreatCode", scriptTreatCode, true);
        }
        if (!ClientScript.IsClientScriptBlockRegistered("InsurCode"))
        {
            ClientScript.RegisterClientScriptBlock(typeof(string), "InsurCode", scriptInsurCode, true);
        }
        if (!ClientScript.IsClientScriptBlockRegistered("Dline"))
        {
            ClientScript.RegisterClientScriptBlock(typeof(string), "Dline", scriptDline, true);
        }
    }

    protected void end_Click(object sender, ImageClickEventArgs e)
    {
        AssignedAppointment createdAppointment = WebServiceAppInfo.CreatedAppointments[(Session["session"] as SessionInfo).Id_num];
        string date = createdAppointment.AppointmentDate;
        int custCode = Convert.ToInt32(createdAppointment.CustCode);
        int siteCode = Convert.ToInt32(createdAppointment.SiteCode);
        int operCode = Convert.ToInt32(createdAppointment.TreatCode);
        int insrCode = Convert.ToInt32(createdAppointment.InuranceCode);
        int dLine = Convert.ToInt32(createdAppointment.Dline);
        // CORRECTIONS AND ADDITIONS SINCE 08/09/2013 : ///////////////////////////////////////////
        bool isRemote = (Application[WSLI.WEBSRV_ISREMOTE].ToString() == "false") ? false : true;
        WebServiceAddNewAppointment wsAddNewAppointment = null;
        WebServiceAddNewAppointmentLOCAL wsAddNewAppointmentLocal = null;
        // WebServiceAppointmentMailData wsAppointmentMailData = null;
        //WebServiceAppointmentMailDataLOCAL wsAppointmentMailDataLocal = null;
        int wcfCustCode = 0;        // added on 14/01/2014
        string lockStatus = null;   // added on 26/01/2014
        string wcfSrvcError = null; // added on 16/09/2018

        if (isRemote)
        {
            wsAddNewAppointment = new WebServiceAddNewAppointment();
            if (wsAddNewAppointment != null)
            {
                // added on 26/01/2014 : //////////////////////////////////////////////////////////
                lockStatus = wsAddNewAppointment.OnLockAppointment(dLine);  
                if (lockStatus != Global.SUCCESS)
                {
                    AlertLockErrorMessage();
                    return;
                }
                // end added //////////////////////////////////////////////////////////////////////
                wcfCustCode = wsAddNewAppointment.GetCustomerCodeForRemoteDiariesReports();      // added on 14/01/2014
                WebServiceAppointmentMailData wsAppointmentMailData = wsAddNewAppointment.AddNewAppointment(
                    date, custCode, siteCode, operCode, insrCode, dLine);
                if (wsAppointmentMailData != null)
                {
                    // ADDED FOR TEST PURPOSES ONLY : //////////////////////////////
                    if (Debugger.IsAttached)
                    {
                        if (wsAppointmentMailData.UserEmail != Global.DEF_RECEPIENT)
                        {
                            wsAppointmentMailData.UserEmail = Global.DEF_RECEPIENT;
                        }
                    }
                    // END ADDED FOR TEST PURPOSES /////////////////////////////////
                    SendAssignedAppointmentEmail(wsAppointmentMailData.UserEmail, wsAppointmentMailData.EmailSubject, wsAppointmentMailData.EmailMessage);  // CORRECTED ON 24/09/2013
                }
                else
                { // condition added on 15/01/2015 
                    AlertLockErrorMessage();
                    return;
                }
            }
        }
        else
        {
            wsAddNewAppointmentLocal = new WebServiceAddNewAppointmentLOCAL(); 
            if (wsAddNewAppointmentLocal != null)
            {
                // added on 26/01/2014 : //////////////////////////////////////////////////////////
                lockStatus = wsAddNewAppointmentLocal.OnLockAppointment(dLine);
                if (lockStatus != Global.SUCCESS)
                {
                    AlertLockErrorMessage();
                    return;
                }
                // end added //////////////////////////////////////////////////////////////////////
                wcfCustCode = wsAddNewAppointmentLocal.GetCustomerCodeForRemoteDiariesReports(); // added on 14/01/2014
                WebServiceAppointmentMailDataLOCAL wsAppointmentMailData = wsAddNewAppointmentLocal.AddNewAppointment(
                    date, custCode, siteCode, operCode, insrCode, dLine);
                if (wsAppointmentMailData != null)
                {
                    // ADDED FOR TEST PURPOSES ONLY : //////////////////////////////
                    if (Debugger.IsAttached)
                    {
                        if (wsAppointmentMailData.UserEmail != Global.DEF_RECEPIENT)
                        {
                            wsAppointmentMailData.UserEmail = Global.DEF_RECEPIENT;
                        }
                    }
                    // END ADDED FOR TEST PURPOSES /////////////////////////////////
                    SendAssignedAppointmentEmail(wsAppointmentMailData.UserEmail, wsAppointmentMailData.EmailSubject, wsAppointmentMailData.EmailMessage);  // CORRECTED ON 24/09/2013
                }
                else
                { // condition added on 15/01/2015
                    AlertLockErrorMessage();
                    return;
                }
            }
        }
        
        // ADDED FOR TEST PURPOSE ONLY !!!
        // HERE ADD PART OF SMS-SENDING
        // END ADDED FOR TEST PURPOSE

        // ADDED ON 14/01/2014 : ///////////////////////////////////////////////////////////////////////////////////////////
        bool useWcfDiariesCounterService = Convert.ToBoolean(Convert.ToInt32(ConfigurationManager.AppSettings["UseWcfDiariesCounterService"])); // added on 26/04/2015
        if (useWcfDiariesCounterService)    // condition added on 26/04/2015
        {
            RRappointmentsWebApplication.WcfDiariesCounterService.DiariesCounterClient wcfDiariesCounter = new RRappointmentsWebApplication.WcfDiariesCounterService.DiariesCounterClient();
            //WcfDiariesCounter wcfDiariesCounter = new WcfDiariesCounter();
            if (wcfDiariesCounter != null)
            {
                // condition extended with try-catch block on 16/09/2018: /////
                try
                {
                    int rows = wcfDiariesCounter.UpdateRemoteDiariesReports(wcfCustCode, Global.ADDN_DATA, Global.PROG_TYPE);
                }
                catch (EndpointNotFoundException ex)
                {
                    wcfSrvcError = ex.Message;
                    if (!string.IsNullOrEmpty(ex.InnerException.Message))
                        wcfSrvcError += string.Format("\nInnerException: {0}", ex.InnerException.Message);
                }
                catch (Exception ex)
                {
                    wcfSrvcError = ex.Message;
                    if (!string.IsNullOrEmpty(ex.InnerException.Message))
                        wcfSrvcError += string.Format("\nInnerException: {0}", ex.InnerException.Message);
                }
                finally
                {
                    step4.WcfMessage = string.Empty;    //
                    if (!string.IsNullOrEmpty(wcfSrvcError))
                    {
                        step4.WcfMessage = string.Format("WARNING. Failed to get WCF Diaries Counter Service:\n{0}", wcfSrvcError);   //
                    }
                }
                //end condition extension /////////////////////////////////////
            }
        }
        // END CORRECTIONS AND ADDITIONS ///////////////////////////////////////////////////////////////////////////////////
 
        try
        {
            end.Visible = false;
        }
        catch (Exception)
        {
            Response.Redirect("step4.aspx");
        }
        Server.Transfer("step4.aspx");
    }

    // METHOD ADDED ON 24/09/2013 : ///////////////////////////////////////////////////////////////
    private void SendAssignedAppointmentEmail(string recipient, string subject, string content)
    {
        string localpath = Server.MapPath(Global.GetRootRelativePath(Global.EMAIL_CONFIG));
        Dictionary<string, string> EmailSettings = Global.GetEmailSettings(localpath);
        if (EmailSettings == null || EmailSettings.Count == 0)
            return;

        string mailSender = null, smtpServer = null, userName = null, password = null, nickName = null;
        int smtpPort = Global.DEFAULT_PORT;
        bool useSsl = false, useNick = false;
        // mailSender = Global.GetEmailSenderSettings(ref smtpServer, ref userName, ref password, ref smtpPort, ref useSsl, EmailSettings); // commetnted on 24/02/2020
        mailSender = Global.GetEmailSenderSettings(ref smtpServer, ref userName, ref password, ref nickName, ref smtpPort, ref useSsl, ref useNick, EmailSettings); // inserted on 24/02/2020
        if (string.IsNullOrEmpty(mailSender))
            return;

        bool isRemote = (Application[WSLI.WEBSRV_ISREMOTE].ToString() == "false") ? false : true;
        bool isSent = false;
        WebServiceEmailSenderLOCAL wsEmailSenderLocal = null;
        WebServiceEmailSender wsEmailSender = null;
        if (isRemote)
        {
            wsEmailSender = new WebServiceEmailSender();
            if (wsEmailSender == null)
                return;

            isSent = wsEmailSender.SendEmail(recipient, subject, content, mailSender, smtpServer, smtpPort, useSsl, userName, password);
        }
        else
        {
            wsEmailSenderLocal = new WebServiceEmailSenderLOCAL();
            if (wsEmailSenderLocal == null)
                return;

            //isSent = wsEmailSenderLocal.SendEmail(recipient, subject, content, mailSender, smtpServer, smtpPort, useSsl, userName, password); // commented on 25/02/2020
            isSent = wsEmailSenderLocal.SendEmailExt(recipient, subject, content, mailSender, smtpServer, userName, password, nickName, smtpPort, useSsl, useNick); // inserted on 25/02/2020
        }
    }
    // END ADDED METHOD ///////////////////////////////////////////////////////////////////////////

    protected void back_Click(object sender, ImageClickEventArgs e)
    {
        // ADDED ON 23/01/2014 : //////////////////////////////////////////////////////////////////
        AssignedAppointment createdAppointment = WebServiceAppInfo.CreatedAppointments[(Session["session"] as SessionInfo).Id_num];
        int dLine = Convert.ToInt32(createdAppointment.Dline);
        bool isRemote = (Application[WSLI.WEBSRV_ISREMOTE].ToString() == "false") ? false : true;
        WebServiceAddNewAppointment wsAddNewAppointment = null;
        WebServiceAddNewAppointmentLOCAL wsAddNewAppointmentLocal = null;
        string unlockStatus = null;

        if (isRemote)
        {
            wsAddNewAppointment = new WebServiceAddNewAppointment();
            if (wsAddNewAppointment != null)
            {
                unlockStatus = wsAddNewAppointment.OnUnlockAppointment(dLine);
            }
        }
        else
        {
            wsAddNewAppointmentLocal = new WebServiceAddNewAppointmentLOCAL();
            if (wsAddNewAppointmentLocal != null)
            {
                unlockStatus = wsAddNewAppointmentLocal.OnUnlockAppointment(dLine);
            }
        }
        // END ADDED //////////////////////////////////////////////////////////////////////////////
        GeneralStaticLogic.UserLogIn = true;
        Response.Redirect("step2.aspx");
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

    // ADDED ON 27/01/2014 : //////////////////////////////////////////////////////////////////////
    //private DialogResult AlertLockErrorMessage()
    //{
    //    DialogResult ret = MessageBox.Show( Global.LOCK_ERROR_MESSAGE, 
    //                                        Global.LOCK_ERROR_CAPTION, 
    //                                        MessageBoxButtons.OK, 
    //                                        MessageBoxIcon.Exclamation, 
    //                                        MessageBoxDefaultButton.Button1, 
    //                                        MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading );
        
    //    return ret;
    //}

    // ADDED ON 20/08/2019 : ////////////////////////////////////////////////////////////////////// 
    private void AlertLockErrorMessage()
    {
        LockErrorMessage.Text = Global.LOCK_ERROR_MESSAGE;
        LockErrorMessage.Visible = true;
        LockErrorMessage.EnableViewState = true;
    }
    // END ADDED //////////////////////////////////////////////////////////////////////////////////
}