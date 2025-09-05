using System;
using System.Collections;   // ADDED
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RRappointmentsWebApplication.BuisnessLogicLayer;
using System.Xml;
using System.Data;
using System.Configuration;
using System.Text;
using System.Web.Security;
using RRappointmentsWebApplication.DataBaseAccessLayer;
using RRappointmentsWebApplication;
using System.Windows.Forms;

using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.Web.Services;
using WSLI = RRappointmentsWebApplication.BuisnessLogicLayer.WebServicesLocationInfo;

// ADDED FOR REMOTE WEB SERVICE DEFINITION : //////////////////////////////////////////////////////////////////////////
using WebServiceAppointments = RRappointmentsWebApplication.AppointmentsWebReference.Appointments;
using WebServiceOldAppointment = RRappointmentsWebApplication.AppointmentsWebReference.OldAppointment;
using WebServiceNewAppointment = RRappointmentsWebApplication.AppointmentsWebReference.AvailableAppointment;
using WebServiceTreatInstruct = RRappointmentsWebApplication.TreatmentInstructionWebReference.TreatmentInstruction;
using WebServiceSiteInfo = RRappointmentsWebApplication.TreatmentInstructionWebReference.SiteInfo; // added on 11/10/2018
using WebServiceListItem = RRappointmentsWebApplication.AppointmentsWebReference.ListItem;  // added on 15/01/2015

// ADDED ON 04/09/2013 FOR LOCAL WEB SERVICE DEFINITION : /////////////////////////////////////////////////////////////
using WebServiceAppointmentsLOCAL = RRappointmentsWebApplication.AppointmentsLocalWebReference.Appointments;
using WebServiceOldAppointmentLOCAL = RRappointmentsWebApplication.AppointmentsLocalWebReference.OldAppointment;
using WebServiceNewAppointmentLOCAL = RRappointmentsWebApplication.AppointmentsLocalWebReference.AvailableAppointment;
using WebServiceTreatInstructLOCAL = RRappointmentsWebApplication.TreatmentInstructionLocalWebReference.TreatmentInstruction;
using WebServiceSiteInfoLOCAL = RRappointmentsWebApplication.TreatmentInstructionLocalWebReference.SiteInfo;   // added on 11/10/2018
using WebServiceListItemLOCAL = RRappointmentsWebApplication.AppointmentsLocalWebReference.ListItem;
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Xml.Serialization;
using System.Xml.Resolvers;

public partial class step2 : System.Web.UI.Page
{
    public List<AvailableAppointment> availableAppointments = new List<AvailableAppointment>();
    public List<OldAppointment> oldAppointments = new List<OldAppointment>();   // added on 22/08/2013
    private bool isRemote = true;
    private bool isEnabledTreatmentGroups = true;
    private bool isUsedDlineAppointFilter = false;  // ADDED ON 28/07/2019

    // ADDED ON 09/09/2015 : ////////////////////
    private static int prevAppointmentsCount;
    private static int prevAppointmentsRowCount;
    // ADDED ON 31/07/2019 : ////////////////////
    private const string DUPLICATION_WARNING    = "תור לבדיקה כזאת כבר נקבע. לא ניתן לקבוע תור לאותה בדיקה שנית";
    private const int APP_CONFIRMATION_STATUS   = 8;
    // END ADDED ////////////////////////////////

    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterClientScript(IsPostBack);
        // ADDED ON 14/08/2013 : ////////////////
        string firstName = (Session["session"] as SessionInfo).First_name;
        string lastName = (Session["session"] as SessionInfo).LastName;
        string Id_Num = (Session["session"] as SessionInfo).Id_num;
        string fileNum = (Session["session"] as SessionInfo).FileNum;
        string selectedSite = null;
        // ADDITIONS AND CORRECTIONS SINCE 04/09/2013 ///////////////////////////////////////////////////////
        isRemote = (Application[WSLI.WEBSRV_ISREMOTE].ToString() == "false") ? false : true;
        isEnabledTreatmentGroups = (ConfigurationManager.AppSettings["EnableTreatmentGroups"].ToString() == "True") ? true : false;
        isUsedDlineAppointFilter = (ConfigurationManager.AppSettings["UseDlineFiltering"].ToString() == "True") ? true : false;
        WebServiceAppointments wsAppointments = null;
        WebServiceAppointmentsLOCAL wsAppointmentsLocal = null;
        if (isRemote)
        {
            wsAppointments = new WebServiceAppointments();
        }
        else
        {
            wsAppointmentsLocal = new WebServiceAppointmentsLOCAL();
        }
        
        if (!IsPostBack)
        {
            // ADDED ON 08/08/2019 : //////////////////////////////////////////////////////////////
            if ((Session["session"] as SessionInfo).OperationDuplicated && !string.IsNullOrEmpty((Session["session"] as SessionInfo).Warning))
            {
                string warning = (Session["session"] as SessionInfo).Warning;                
                Response.Write("<script type=\"text/javascript\">" +
                    "window.alert('" + warning + "');" +
                    "</script>");
                ClientScript.RegisterStartupScript(GetType(), "step2", warning, true);
                (Session["session"] as SessionInfo).OperationDuplicated = false;
                (Session["session"] as SessionInfo).Warning = string.Empty;
            }
            // END ADDED //////////////////////////////////////////////////////////////////////////
            if (!GeneralStaticLogic.UserLogIn)
            {
                Response.Redirect("index.aspx");
            }
            GeneralStaticLogic.UserLogIn = false;
            selectAppointmentTiltleLabel.Visible = false;
            userNameLabel.Text = firstName + ' ' + lastName;
            startDateTextBox.Text = DateTime.Today.ToShortDateString();
            endDateTextBox.Text = DateTime.Today.AddDays(30).ToShortDateString();
            // ADDED ON 09/09/2015 : /////////////////
            prevAppointmentsCount = 0;
            prevAppointmentsRowCount = 0;
            // ADDED ON 14/08/2013 => CORRECTED ON 04/09/2013 ///////////////////////////////////////////////
            if (isRemote)
            {
                // wsAppointments = new WebServiceAppointments();
                if (wsAppointments != null)
                {
                    GetOldAppointments(wsAppointments, Id_Num, fileNum);
                    if (isEnabledTreatmentGroups)
                    {
                        operDropDownList.Enabled = false;
                        if (treatmentGroupDropDownList.Items.Count == 1)
                        {
                            //FillDropDownList(treatmentGroupDropDownList, wsAppointments.GetTreatmentGroupList());   // commented on 17/07/2019
                            // ADDED ON 17/07/2019 : //////////////////////////////////////////////
                            ArrayList treatGroupList = ArrayList.Adapter(wsAppointments.GetTreatmentGroupList());
                            FillDropDownListRemote(treatmentGroupDropDownList, treatGroupList);
                            ArrayList operationsList = ArrayList.Adapter(wsAppointments.GetOperList());
                            ArrayList groupOpersList = ArrayList.Adapter(wsAppointments.GetGroupOperLinkList());
                            ArrayList groupMessageList = ArrayList.Adapter(wsAppointments.GetGroupMessageList()); // ATTENTION !!! COMMENTED TEMPORARY !!! 
                            (Session["session"] as SessionInfo).GroupOperLinkList = GetGroupOperLinkList(treatGroupList, groupOpersList, operationsList);
                            (Session["session"] as SessionInfo).GroupMessageList = groupMessageList;
                            // END ADDED ON 17/07/2019 ////////////////////////////////////////////
                        }
                    }
                    else
                    {
                        treatmentGroupRow.Visible = false;
                        if (operDropDownList.Items.Count == 1)
                        {
                            FillDropDownList(operDropDownList, wsAppointments.GetOperList());
                        }
                    }

                    if (insuranceDropDownList.Items.Count == 1)
                    {
                        FillDropDownList(insuranceDropDownList, wsAppointments.GetInsuranceList());
                    }
                }
            }
            else
            {
                // wsAppointmentsLocal = new WebServiceAppointmentsLOCAL();
                if (wsAppointmentsLocal != null)
                {
                    GetOldAppointments(wsAppointmentsLocal, Id_Num, fileNum);
                    if (isEnabledTreatmentGroups)
                    {
                        operDropDownList.Enabled = false;
                        if (treatmentGroupDropDownList.Items.Count == 1)
                        {
                            //FillDropDownList(treatmentGroupDropDownList, wsAppointmentsLocal.GetTreatmentGroupList());
                            //FillDropDownList(treatmentGroupDropDownList, ArrayList.Adapter(wsAppointmentsLocal.GetTreatmentGroupList())); // commented on 14/07/2019
                            // ADDED ON 14/07/2019 : //////////////////////////////////////////////
                            ArrayList treatGroupList = ArrayList.Adapter(wsAppointmentsLocal.GetTreatmentGroupList());
                            FillDropDownList(treatmentGroupDropDownList, treatGroupList);
                            ArrayList operationsList = ArrayList.Adapter(wsAppointmentsLocal.GetOperList());
                            ArrayList groupOpersList = ArrayList.Adapter(wsAppointmentsLocal.GetGroupOperLinkList());
                            ArrayList groupMessageList = ArrayList.Adapter(wsAppointmentsLocal.GetGroupMessageList());  // added on 27/11/2019
                            (Session["session"] as SessionInfo).GroupOperLinkList = GetGroupOperLinkListLocal(treatGroupList, groupOpersList, operationsList);
                            (Session["session"] as SessionInfo).GroupMessageList = groupMessageList;    // added on 27/11/2019
                            // END ADDED ON 14/07/2019 ////////////////////////////////////////////
                        }
                    }
                    else
                    {
                        treatmentGroupRow.Visible = false;
                        if (operDropDownList.Items.Count == 1)
                        {
                            //  FillDropDownList(operDropDownList, wsAppointmentsLocal.GetOperList());
                            FillDropDownList(operDropDownList, ArrayList.Adapter(wsAppointmentsLocal.GetOperList()));
                        }
                    }

                    if (insuranceDropDownList.Items.Count == 1)
                    {
                        //  FillDropDownList(insuranceDropDownList, wsAppointmentsLocal.GetInsuranceList());
                        FillDropDownList(insuranceDropDownList, ArrayList.Adapter(wsAppointmentsLocal.GetInsuranceList()));
                    }
                }
            }
            // END ADDED //////////////////////////////////////////////////////////////////////////
        } // end if !IsPostBack
        else // if (IsPostBack)
        {
            GeneralStaticLogic.UserLogIn = true;
            if (operDropDownList.SelectedIndex > 0)
            {
                if (siteDropDownList.SelectedIndex != 0)
                {
                    selectedSite = siteDropDownList.SelectedValue;
                }
                siteDropDownList.Items.Clear();

                if (wsAppointments != null || wsAppointmentsLocal != null)
                {
                    string sCurrOperValue = operDropDownList.SelectedValue;
                    int nCurrOperValue = (string.IsNullOrEmpty(sCurrOperValue)) ?
                        Global.LB_ERROR : Convert.ToInt32(sCurrOperValue);
                    if (nCurrOperValue != Global.LB_ERROR)
                    {
                        if (isRemote)
                        {
                            if (wsAppointments != null)
                            {
                                FillDropDownList(siteDropDownList, wsAppointments.GetSiteList(nCurrOperValue));
                            }
                        }
                        else
                        {
                            if (wsAppointmentsLocal != null)
                            {
                            //  FillDropDownList(siteDropDownList, wsAppointmentsLocal.GetSiteList(nCurrOperValue));
                                FillDropDownList(siteDropDownList, ArrayList.Adapter(wsAppointmentsLocal.GetSiteList(nCurrOperValue)));
                            }
                        }
                        
                        siteDropDownList.Enabled = true;
                        if (!string.IsNullOrEmpty(selectedSite) &&
                            siteDropDownList.Items.FindByValue(selectedSite).Value == selectedSite)
                        {
                            siteDropDownList.Items.FindByValue(selectedSite).Selected = true;
                        }
                    }
                }

            }
            else
            {
                siteDropDownList.Items.Clear();
                siteDropDownList.Enabled = false;
            }
            if (wsAppointments != null)
                wsAppointments.Dispose();
            if (wsAppointmentsLocal != null)
                wsAppointmentsLocal.Dispose();  // ADDED ON 04/09/2013
        } // end if IsPostBack
    }

    private void GetOldAppointments(WebServiceAppointments wsAppointments, string id_Num, string fileNum)
    {
        WebServiceOldAppointment[] wsOldAppointments = wsAppointments.GetOldAppointments(id_Num, fileNum);
        if (wsOldAppointments != null && wsOldAppointments.Length > 0)
        {   // corrections and additions since 22/08/2013 : ///////////////////////////////////////
            SetOldAppointments(wsOldAppointments);
            // oldAppointmentsGridView.DataSource = wsOldAppointments;
            (Session["session"] as SessionInfo).OldAppointments = oldAppointments;
            oldAppointmentsGridView.DataSource = (Session["session"] as SessionInfo).OldAppointments;
            // end corrections and additions ////////////////////////////////////////////////////// 
            oldAppointmentsGridView.DataBind();
            oldAppointmentsGridView.PageIndex = 0;
        }
        if (oldAppointmentsGridView.Rows.Count == 0)
        {
            oldAppointmentsPanel.Visible = false;
            pagesInnerFrameLargerPanel.Height = 680;
        }
        else
        {
            oldAppointmentsPanel.Visible = true;
            pagesInnerFrameLargerPanel.Height = 940;
        }
        CheckAppointmentLimit();
    }

    // OVERLOADED METHOD ADDED ON 04/09/2013 : ////////////////////////////////////////////////////
    private void GetOldAppointments(WebServiceAppointmentsLOCAL wsAppointments, string id_Num, string fileNum)
    {
        WebServiceOldAppointmentLOCAL[] wsOldAppointments = wsAppointments.GetOldAppointments(id_Num, fileNum);
        if (wsOldAppointments != null && wsOldAppointments.Length > 0)
        {   // corrections and additions since 22/08/2013 : ///////////////////////////////////////
            SetOldAppointments(wsOldAppointments);
            // oldAppointmentsGridView.DataSource = wsOldAppointments;
            (Session["session"] as SessionInfo).OldAppointments = oldAppointments;
            oldAppointmentsGridView.DataSource = (Session["session"] as SessionInfo).OldAppointments;
            // end corrections and additions ////////////////////////////////////////////////////// 
            oldAppointmentsGridView.DataBind();
            oldAppointmentsGridView.PageIndex = 0;  //
        }
        if (oldAppointmentsGridView.Rows.Count == 0)
        {
            oldAppointmentsPanel.Visible = false;
            pagesInnerFrameLargerPanel.Height = 680;
        }
        else
        {
            oldAppointmentsPanel.Visible = true;
            pagesInnerFrameLargerPanel.Height = 940;
        }
        CheckAppointmentLimit();
    }
    // END OVERLOADED METHOD //////////////////////////////////////////////////////////////////////

    private void SetOldAppointments(WebServiceOldAppointment[] wsOldAppointments)
    {
        if (oldAppointments == null)
            oldAppointments = new List<OldAppointment>();
        oldAppointments.Clear();

        if (wsOldAppointments != null)
        { 
            int length = wsOldAppointments.Length;
            if (length != 0)
            {
                for (int index = 0; index < length; index++)
                {
                    OldAppointment appointment = new OldAppointment
                    {
                        AppointmentDate     = wsOldAppointments[index].AppointmentDate,
                        AppointmentDay      = wsOldAppointments[index].AppointmentDay,
                        AppointmentLocation = wsOldAppointments[index].AppointmentLocation,
                        AppointmentOperName = wsOldAppointments[index].AppointmentOperName,
                        AppointmentTime     = wsOldAppointments[index].AppointmentTime,
                        LineCode            = wsOldAppointments[index].LineCode // ADDED ON 30/07/2019
                    };

                    oldAppointments.Add(appointment);
                }
            }
        }
    }

    // OVERLOADED METHOD ADDED ON 04/09/2013 : ////////////////////////////////////////////////////
    private void SetOldAppointments(WebServiceOldAppointmentLOCAL[] wsOldAppointments)
    {
        if (oldAppointments == null)
            oldAppointments = new List<OldAppointment>();
        oldAppointments.Clear();

        if (wsOldAppointments != null)
        {
            int length = wsOldAppointments.Length;
            if (length != 0)
            {
                for (int index = 0; index < length; index++)
                {
                    OldAppointment appointment = new OldAppointment
                    {
                        AppointmentDate     = wsOldAppointments[index].AppointmentDate,
                        AppointmentDay      = wsOldAppointments[index].AppointmentDay,
                        AppointmentLocation = wsOldAppointments[index].AppointmentLocation,
                        AppointmentOperName = wsOldAppointments[index].AppointmentOperName,
                        AppointmentTime     = wsOldAppointments[index].AppointmentTime,
                        LineCode            = wsOldAppointments[index].LineCode // ADDED ON 30/07/2019
                    };

                    oldAppointments.Add(appointment);
                }
            }
        }
    }
    // END OVERLOADED METHOD //////////////////////////////////////////////////////////////////////

    private void OnInitOldAppointmentsGrid()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("oper_name");
        dt.Columns.Add("c_branch");
        dt.Columns.Add("dtime");
        dt.Columns.Add("ddate");
        dt.Columns.Add("day");
        dt.Rows.Add();
        oldAppointmentsGridView.DataSource = dt;
        oldAppointmentsGridView.DataBind();
        oldAppointmentsGridView.Rows[0].Visible = false;
    }

    private void FillDropDownList(DropDownList ddList, Object[] xmlNodeArray, string sKey = null)
    { // this method was corrected on 15/01/2015
        ArrayList dataList = ArrayList.Adapter(xmlNodeArray);
        if (dataList != null && dataList.Count > 0)
        {
            foreach (WebServiceListItem wsItem in dataList)
            {
                var text = wsItem.Text.ToString();
                var value = wsItem.Value.ToString();
                if (!string.IsNullOrEmpty(sKey) && value != sKey)
                    continue;

                ListItem item = new ListItem(text, value);
                if (!ddList.Items.Contains(item))
                    ddList.Items.Add(item);
            }
        }
    }

    // OVERLOADED METHOD ADDED ON 08/09/2013 : ////////////////////////////////////////////////////
    private void FillDropDownList(DropDownList ddList, ArrayList arrayList, string sKey = null)
    {
        if (arrayList != null && arrayList.Count > 0)
        {
            foreach (WebServiceListItemLOCAL wsItem in arrayList)
            {
                var text = wsItem.Text.ToString();
                var value = wsItem.Value.ToString();
                if (!string.IsNullOrEmpty(sKey) && value != sKey)
                    continue;

                ListItem item = new ListItem(text, value);
                if (!ddList.Items.Contains(item))
                    ddList.Items.Add(item);
            }
        }
    }
    // END ADDED OVERLOADED METOD /////////////////////////////////////////////////////////////////
    // ADDED ON 18/08/2019 : //////////////////////////////////////////////////////////////////////
    private void FillDropDownListRemote(DropDownList ddList, ArrayList arrayList, string sKey = null)
    {
        if (arrayList != null && arrayList.Count > 0)
        {
            foreach (WebServiceListItem wsItem in arrayList)
            {
                var text = wsItem.Text.ToString();
                var value = wsItem.Value.ToString();
                if (!string.IsNullOrEmpty(sKey) && value != sKey)
                    continue;

                ListItem item = new ListItem(text, value);
                if (!ddList.Items.Contains(item))
                    ddList.Items.Add(item);
            }
        }
    }
// END ADDED ON 18/08/2019 ////////////////////////////////////////////////////////////////////////

    private void CheckAppointmentLimit()
    {
        if (ConfigurationManager.AppSettings["AppointmentLimit"].ToString() != "0")
        {
            if (oldAppointmentsGridView.Rows.Count >= int.Parse(ConfigurationManager.AppSettings["AppointmentLimit"].ToString()))
            {
                mainPanel.Visible = false;
                appointmentLimitTitle.Visible = true;
            }
        }
    }

    protected void availablesAppointmentsGridView_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string rowID = "row" + e.Row.RowIndex;

            e.Row.Attributes.Add("onmouseover", "ChangeRowColor('" + rowID + "','mouseOver')");
            e.Row.Attributes.Add("onmouseout", "ChangeRowColor('" + rowID + "','mouseOut')");

            e.Row.Attributes.Add("id", rowID);

            e.Row.Attributes.Add("onclick", "SetRow('" + rowID + "')");
        }
    }

    protected void search_Click(object sender, ImageClickEventArgs e)
    {
        GeneralStaticLogic.UserLogIn = true;
        //// TEST
        //string operation = operDropDownList.SelectedItem.Text;
        //string warning = string.Format("{0}:\r\n{1}", operation, DUPLICATION_WARNING);

        //if (oldAppointmentsGridView.Rows.Count > 0)
        //{
        //    foreach (GridViewRow row in oldAppointmentsGridView.Rows)
        //    {
        //        if (row.Cells[0].Text == operation)
        //        {
        //            operDropDownList.SelectedIndex = -1;
        //            operErrorLabel.Visible = true;
        //            operErrorLabel.Text = warning;

        //            MessageBox.Show(warning, "זימון תורים", MessageBoxButtons.OK);
        //            Response.Redirect("step2.aspx");//
        //            //Server.Transfer("step2.aspx");
        //            return;
        //        }
        //    }
        //}
        // END TEST
        GetAvailableAppointments();
    }

    private void GetAvailableAppointments()
    {
        int D1 = (sundayCheckBox.Checked)   ? 1 : 0;
        int D2 = (mondayCheckBox.Checked)   ? 2 : 0;
        int D3 = (tuesdayCheckBox.Checked)  ? 3 : 0;
        int D4 = (wednesdayCheckBox.Checked)? 4 : 0;
        int D5 = (thursdayCheckBox.Checked) ? 5 : 0;
        int D6 = (fridayCheckBox.Checked)   ? 6 : 0;
        int D7 = (saturdayCheckBox.Checked) ? 7 : 0;


        int operCode = Convert.ToInt32(operDropDownList.SelectedItem.Value);
        int siteCode = Convert.ToInt32(siteDropDownList.SelectedItem.Value);
        string fromDate = startDateTextBox.Text;
        string toDate = endDateTextBox.Text;
        // NO NEED CONVERTION HERE: APPLIED IN THE WEB SERVICE **
        // DateTime startDateTime = Convert.ToDateTime(fromDate);
        // DateTime endDateTime = Convert.ToDateTime(toDate);
        // fromDate = startDateTime.ToString("yyyyMMdd");
        // toDate = endDateTime.ToString("yyyyMMdd");
        // ******************************************************
        // ADDITIONS AND CORRECTIONS SINCE 04/09/2013 : //////////////////////////////////////////////////////////
        WebServiceAppointments wsAppointments = null;
        WebServiceAppointmentsLOCAL wsAppointmentsLocal = null;

        try
        {
            if (isRemote)
            {
                wsAppointments = new WebServiceAppointments();
                if (wsAppointments != null)
                {   // CORRECTED/EXTENDED ON 20/08/2019 : /////////////////////////////////////////
                    WebServiceNewAppointment[] wsAvailableAppointments = (isUsedDlineAppointFilter) ?
                        wsAppointments.GetFilteredAvailableAppointments(
                            operCode, siteCode, fromDate, toDate, D1, D2, D3, D4, D5, D6, D7) :
                        wsAppointments.GetAvailableAppointments(
                            operCode, siteCode, fromDate, toDate, D1, D2, D3, D4, D5, D6, D7);
                    // END CORRECTED/EXTENDED BLOCK ///////////////////////////////////////////////
                    if (wsAvailableAppointments != null && wsAvailableAppointments.Length != 0)
                    {
                        SetAvailableAppointments(wsAvailableAppointments);
                    }
                }
                wsAppointments.Dispose();
            }
            else 
            {
                wsAppointmentsLocal = new WebServiceAppointmentsLOCAL();
                if (wsAppointmentsLocal != null)
                {   // CORRECTED ON 29/07/2019 : //////////////////////////////////////////////////
                    WebServiceNewAppointmentLOCAL[] wsAvailableAppointments = (isUsedDlineAppointFilter) ?
                        wsAppointmentsLocal.GetFilteredAvailableAppointments(
                            operCode, siteCode, fromDate, toDate, D1, D2, D3, D4, D5, D6, D7) :
                        wsAppointmentsLocal.GetAvailableAppointments(
                            operCode, siteCode, fromDate, toDate, D1, D2, D3, D4, D5, D6, D7);
                    // END CORRECTED ON 29/07/2019 ////////////////////////////////////////////////
                    if (wsAvailableAppointments != null && wsAvailableAppointments.Length != 0)
                    {
                        SetAvailableAppointments(wsAvailableAppointments);
                    }
                }
                wsAppointmentsLocal.Dispose();
            }
            if (availableAppointments == null || availableAppointments.Count == 0)
            {
                availablesAppointmentsGridView.Visible = false;
                statusLabel.Visible = true;
                selectAppointmentTiltleLabel.Visible = false;
            }
            else
            { 
                availablesAppointmentsGridView.Visible = true;
                selectAppointmentTiltleLabel.Visible = true;
                statusLabel.Visible = false;
                (Session["session"] as SessionInfo).AvailableAppointments = availableAppointments;
                availablesAppointmentsGridView.DataSource = (Session["session"] as SessionInfo).AvailableAppointments;
                availablesAppointmentsGridView.DataBind();
                // ADDED ON 09/09/2015 : ////////
                prevAppointmentsCount = availableAppointments.Count;
                prevAppointmentsRowCount = availablesAppointmentsGridView.Rows.Count;
                // END ADDED ////////////////////
            }
        }
        catch (Exception)
        {
            Session.Abandon();
        }
    }

    private void SetAvailableAppointments(WebServiceNewAppointment[] wsAvailableAppointments)
    {
        if (availableAppointments == null)
            availableAppointments = new List<AvailableAppointment>();
        availableAppointments.Clear();


        if (wsAvailableAppointments != null)
        {
            int length = wsAvailableAppointments.Length;
            if (length != 0)
            {
                for (int index = 0; index < length; index++)
                {
                    AvailableAppointment appointment = new AvailableAppointment
                    {
                        Ddate       = wsAvailableAppointments[index].Ddate,
                        Dday        = wsAvailableAppointments[index].Dday,
                        Diary_name  = wsAvailableAppointments[index].Diary_name,
                        Diarynum    = wsAvailableAppointments[index].Diarynum,
                        Dline       = wsAvailableAppointments[index].Dline,
                        Dtime       = wsAvailableAppointments[index].Dtime
                    };

                    availableAppointments.Add(appointment);
                }
            }
        }
    }

    // OVERLOADED METHOD ADDED ON 04/09/2013 : ////////////////////////////////////////////////////
    private void SetAvailableAppointments(WebServiceNewAppointmentLOCAL[] wsAvailableAppointments)
    {
        if (availableAppointments == null)
            availableAppointments = new List<AvailableAppointment>();
        availableAppointments.Clear();


        if (wsAvailableAppointments != null)
        {
            int length = wsAvailableAppointments.Length;
            if (length != 0)
            {
                for (int index = 0; index < length; index++)
                {
                    AvailableAppointment appointment = new AvailableAppointment
                    {
                        Ddate = wsAvailableAppointments[index].Ddate,
                        Dday = wsAvailableAppointments[index].Dday,
                        Diary_name = wsAvailableAppointments[index].Diary_name,
                        Diarynum = wsAvailableAppointments[index].Diarynum,
                        Dline = wsAvailableAppointments[index].Dline,
                        Dtime = wsAvailableAppointments[index].Dtime
                    };

                    availableAppointments.Add(appointment);
                }
            }
        }
    }
    // END OVERLOADED METHOD //////////////////////////////////////////////////////////////////////

    protected void next_Click(object sender, ImageClickEventArgs e)
    {
        WebServiceAppInfo.CreatedAppointments = new Dictionary<string, AssignedAppointment>();
        // Corrections and additions since 17/06/2013 : ///////////////////////////////////////////
        AssignedAppointment newAppointment = GetAssignedAppointment();
        newAppointment.TreatmentInstructions = GetTreatmentInstructions();  // added on 20/08/2013
        // added on 11/10/2018 : ////////////////////////////////////
        if (isRemote)
        {
            WebServiceSiteInfo siteInfo = GetSiteInfo(newAppointment.SiteCode);
            newAppointment.Address = siteInfo.Address;
            newAppointment.Phone = siteInfo.Phone1;
        }
        else
        {
            WebServiceSiteInfoLOCAL siteInfo = GetSiteInfoLocal(newAppointment.SiteCode);
            newAppointment.Address = siteInfo.Address;
            newAppointment.Phone = siteInfo.Phone1;
        }
        // end added on 11/10/2018 //////////////////////////////////
        newAppointment.FirstName = (Session["session"] as SessionInfo).First_name;
        newAppointment.LastName = (Session["session"] as SessionInfo).LastName;
        newAppointment.CustCode = (Session["session"] as SessionInfo).Cust_num;
        WebServiceAppInfo.CreatedAppointments.Add((Session["session"] as SessionInfo).Id_num, newAppointment);

        // Response.Redirect("step3.aspx");
        Server.Transfer("step3.aspx");
    }

    // added on 20/08/2013 : ////////////////////
    private string GetTreatmentInstructions()
    {
        string instruction = null;
        int operCode = Convert.ToInt32(operDropDownList.SelectedItem.Value);
        // ADDITIONS AND CORRECTIONS SINCE 04/09/2013 : /////////////////////////////////
        WebServiceTreatInstruct wsTreatInstruction = null;
        WebServiceTreatInstructLOCAL wsTreatInstructionLocal = null;
        if (isRemote)
        {
            wsTreatInstruction = new WebServiceTreatInstruct();
            if (wsTreatInstruction != null)
            {
                instruction = wsTreatInstruction.GetTreatmentInstructions(operCode);
            }
        }
        else
        {
            wsTreatInstructionLocal = new WebServiceTreatInstructLOCAL();
            if (wsTreatInstructionLocal != null)
            {
                instruction = wsTreatInstructionLocal.GetTreatmentInstructions(operCode);
            }
        }

        return (instruction == null) ? string.Empty : instruction;
    }

    // added on 11/10/2018 : ////////////////////
    private WebServiceSiteInfoLOCAL GetSiteInfoLocal(string siteCode)
    {
        WebServiceSiteInfoLOCAL siteInfo = new WebServiceSiteInfoLOCAL();
        int nSiteCode = Convert.ToInt32(siteCode);
        WebServiceTreatInstructLOCAL wsTreatInstructionLocal = null;
        wsTreatInstructionLocal = new WebServiceTreatInstructLOCAL();
        if (wsTreatInstructionLocal != null)
        {
                siteInfo = wsTreatInstructionLocal.GetSiteInfo(nSiteCode);
        }

        return siteInfo;
    }

    private WebServiceSiteInfo GetSiteInfo(string siteCode)
    {
        WebServiceSiteInfo siteInfo = new WebServiceSiteInfo();
        int nSiteCode = Convert.ToInt32(siteCode);
        WebServiceTreatInstruct wsTreatInstruction = null;
        wsTreatInstruction = new WebServiceTreatInstruct();
        if (wsTreatInstruction != null)
        {
            siteInfo = wsTreatInstruction.GetSiteInfo(nSiteCode);
        }

        return siteInfo;
    }
    // end additions ////////////////////////////

    protected void treatmentGroupDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (isEnabledTreatmentGroups)
        {
            string selectedTreatmentGroup = null, 
                //selectedTreatmentGroupDesc = null,
                message = null;    // added on 27/11/2019
            operDropDownList.Enabled = true;
            selectedTreatmentGroup = treatmentGroupDropDownList.SelectedItem.Value;
            //selectedTreatmentGroupDesc = treatmentGroupDropDownList.SelectedItem.Text;
            operDropDownList.Items.Clear();
            ListItem listItem = new ListItem("", "0");
            operDropDownList.Items.Add(listItem);
            // ADDED ON 15/07/2019 : ////////////
            ArrayList operList = (Session["session"] as SessionInfo).GroupOperLinkList.Find(x => x.Key == selectedTreatmentGroup).Value;
            if (operList != null && operList.Count > 0)
            {
                FillDynamicDropDownList(operDropDownList, operList);
            }

            // added on 27/11/2019 : ///////////////////////////
            message = (isRemote) ? GetGroupMessage(selectedTreatmentGroup) : GetGroupMessageLocal(selectedTreatmentGroup);
            if (!string.IsNullOrEmpty(message))
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "MessageBox", "alert('" + message + "');", true);
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox", "<script language='javascript'>alert('" + message + "');</script>");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "')", true);
            }
            // end added on 27/11/2019 /////////////////////////

           
            //siteDropDownList.Items.Clear();
            //siteDropDownList.Enabled = false;
            // CORRECTIONS AND ADDITIONS SINCE 18/08/2013 : ///////////////////////////////////////
            //WebServiceAppointments wsAppointments = null;
            //WebServiceAppointmentsLOCAL wsAppointmentsLocal = null;
            //if (isRemote)
            //{
            //    wsAppointments = new WebServiceAppointments();
            //    if (wsAppointments != null)
            //    {
            //        FillDropDownList(operDropDownList, wsAppointments.GetOperList(), selectedTreatmentGroup);
            //        operDropDownList.SelectedIndex = -1;
            //    }
            //    wsAppointments.Dispose();
            //}
            //else
            //{
            //    wsAppointmentsLocal = new WebServiceAppointmentsLOCAL();
            //    if (wsAppointmentsLocal != null)
            //    {
            //    //  FillDropDownList(operDropDownList, wsAppointmentsLocal.GetOperList(), selectedTreatmentGroup);
            //        FillDropDownList(operDropDownList, ArrayList.Adapter(wsAppointmentsLocal.GetOperList()), selectedTreatmentGroup);
            //        operDropDownList.SelectedIndex = -1;
            //    }
            //    wsAppointmentsLocal.Dispose();
            //}
            // END CORRECTIONS AND ADDITIONS //////////////////////////////////////////////////////
        }
    }

    private string GetGroupMessageLocal(string groupNum)
    {
        string message = null;
        int group_num = int.Parse(groupNum);
        ArrayList groupMessageList = (Session["session"] as SessionInfo).GroupMessageList;
        
        foreach (WebServiceListItemLOCAL item in groupMessageList)
        {
            if (item.Value == groupNum)
            {
                message = item.Text;
                break;
            }
        }
        
        return message;
    }

    private string GetGroupMessage(string groupNum)
    {
        string message = null;
        int group_num = int.Parse(groupNum);
        ArrayList groupMessageList = (Session["session"] as SessionInfo).GroupMessageList;

        foreach (WebServiceListItem item in groupMessageList)
        {
            if (item.Value == groupNum)
            {
                message = item.Text;
                break;
            }
        }

        return message;
    }

    private void OnInitAvailableAppointmentsGrid()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("dline");
        dt.Columns.Add("");
        dt.Columns.Add("dDate");
        dt.Columns.Add("dtime");
        dt.Columns.Add("Dday");
        dt.Rows.Add();
        availablesAppointmentsGridView.DataSource = dt;
        availablesAppointmentsGridView.DataBind();
        availablesAppointmentsGridView.Rows[0].Visible = false;
        availablesAppointmentsGridView.PageIndex = 0;
    }

    protected void availablesAppointmentsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        availablesAppointmentsGridView.PageIndex = e.NewPageIndex;
        availablesAppointmentsGridView.DataSource = (Session["session"] as SessionInfo).AvailableAppointments;
        availablesAppointmentsGridView.DataBind();

        if (!ClientScript.IsClientScriptBlockRegistered("JsScript"))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "JsScript", "UpdateSelection();", true);
        }
    }

    private AssignedAppointment GetAssignedAppointment()
    {
        AssignedAppointment newAppointment =  new AssignedAppointment();
        string data = selectedGridRadioHidden.Value.ToString();
        string [] dataArray = data.Split(';');
        int pos = -1;
        
        foreach (string item in dataArray)
        { 
            pos = item.IndexOf('=');
            string key = item.Substring(0, pos);
            string val = item.Substring(pos + 1);
            switch (key)
            { 
                case "operVal":
                    newAppointment.TreatCode = val;
                    break;
                case "operTxt":
                    newAppointment.Treatment = val;
                    break;
                case "siteVal":
                    newAppointment.SiteCode = val;
                    break;
                case "siteTxt":
                    newAppointment.Site = val;
                    break;
                case "insrVal":
                    newAppointment.InuranceCode = val;
                    break;
                case "insrTxt":
                    newAppointment.Insurance = val;
                    break;
                case "dline":
                    newAppointment.Dline = val;
                    break;
                case "date":
                    newAppointment.AppointmentDate = val;
                    break;
                case "time":
                    newAppointment.Time = val;
                    break;
                case "day":
                    newAppointment.Day = val;
                    break;
                //case "instructions":  // removed on 20/08/2013
                //    newAppointment.TreatmentInstructions = val;
                //    break;
            }
        }

        return newAppointment;
    }

    private void RegisterClientScript(bool isPostBack)
    {
        var appointmentLimit = ConfigurationManager.AppSettings["AppointmentLimit"].ToString();
        var treatmentGroups = ConfigurationManager.AppSettings["EnableTreatmentGroups"].ToString();
        string scriptLimit = string.Format("var appointmentLimit = '{0}';", appointmentLimit);
        string scriptGroups = string.Format("var groups = '{0}';", treatmentGroups);
        // added on 28/07/2018 : //////////////////////////////////////////////////////////////////
        var dlineFiltering = ConfigurationManager.AppSettings["UseDlineFiltering"].ToString();
        string scriptFiltering = string.Format("var dlineFiltering = '{0}'", dlineFiltering);
        // added on 07/07/2013 : //////////////////////////////////////////////////////////////////
        // string scriptUserId = string.Format("var userId = '{0}';", (Session["session"] as SessionInfo).Id_num);
        // string scriptFileNum = string.Format("var fileNum = '{0}';", (Session["session"] as SessionInfo).FileNum);
        // end added //////////////////////////////////////////////////////////////////////////////
        
        if (!ClientScript.IsClientScriptBlockRegistered("AppointmentLimit"))
        {
            ClientScript.RegisterClientScriptBlock(typeof(string), "AppointmentLimit", scriptLimit, true);
        }
        if (!ClientScript.IsClientScriptBlockRegistered("TreatmentGroups"))
        {
            ClientScript.RegisterClientScriptBlock(typeof(string), "TreatmentGroups", scriptGroups, true);
        }
        // added on 28/07/2019 : //////////////////////////////////////////////////////////////////
        if (!ClientScript.IsClientScriptBlockRegistered("DlineFiltering"))
        {
            ClientScript.RegisterClientScriptBlock(typeof(string), "DlineFiltering", scriptFiltering, true);
        }
        // added on 07/07/2013 : //////////////////////////////////////////////////////////////////
        //if (!ClientScript.IsClientScriptBlockRegistered("Id_Num"))
        //{
        //    ClientScript.RegisterClientScriptBlock(GetType(), "Id_Num", scriptUserId, true);
        //}
        //if (!ClientScript.IsClientScriptBlockRegistered("FileNum"))
        //{
        //    ClientScript.RegisterClientScriptBlock(GetType(), "FileNum", scriptFileNum, true);
        //}
        // end added //////////////////////////////////////////////////////////////////////////////

        string script = string.Format("var isPostBack = '{0}';", isPostBack ? ("true") : ("false"));
        ClientScript.RegisterStartupScript(GetType(), "IsPostBack", script, true);

        // added on 21/07/2013 : //////////////////////////////////////////////////////////////////
        RegisterClientScriptWebServicesLocation();
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

    protected void oldAppointmentsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        oldAppointmentsGridView.PageIndex = e.NewPageIndex;
        oldAppointmentsGridView.DataSource = (Session["session"] as SessionInfo).OldAppointments;
        oldAppointmentsGridView.DataBind();
        // ADDED ON 09/09/2015: //////////////////////
        if (IsPostBack)
            CheckAvailableAppointmentsGrid();
        // END ADDED /////////////////////////////////
    }

    // ADDED ON 09/09/2015 : /////////////////////////
    protected void CheckAvailableAppointmentsGrid()
    {
        if ((!availablesAppointmentsGridView.Visible || availablesAppointmentsGridView.Rows.Count == 0) &&
            (prevAppointmentsCount != 0 || prevAppointmentsRowCount != 0))
        {
            if (!GeneralStaticLogic.UserLogIn)
                GeneralStaticLogic.UserLogIn = true;
            GetAvailableAppointments();
        }
    }

    // ADDED ON 14/07/2019 : /////////////////////////
    protected List<KeyValuePair<string, ArrayList>> GetGroupOperLinkListLocal(ArrayList treatGroupList, ArrayList groupOpersList, ArrayList operationsList)
    {
        List<KeyValuePair<string, ArrayList>> groupOperLinkList = new List<KeyValuePair<string, ArrayList>>();
        string keyGroup, valGroup;
        
        foreach (/*ListItem*/ WebServiceListItemLOCAL itemGroup in treatGroupList)
        {
            keyGroup = itemGroup.Value;
            ArrayList groupOperLink = new ArrayList();
            
            foreach (/*ListItem*/ WebServiceListItemLOCAL itemGroupOper in groupOpersList)
            {
                if (itemGroupOper.Value == keyGroup)
                {
                    valGroup = itemGroupOper.Text;
                    foreach (/*ListItem*/ WebServiceListItemLOCAL operation in operationsList)
                    {
                        if (operation.Value == valGroup)
                        {
                            KeyValuePair<string, string> pair = new KeyValuePair<string, string>(operation.Value, operation.Text);
                            groupOperLink.Add(pair);
                        }
                    }
                }
            }
            //
            if (groupOperLink.Count > 0)
            {
                KeyValuePair<string, ArrayList> extPair = new KeyValuePair<string, ArrayList>(keyGroup, groupOperLink);
                groupOperLinkList.Add(extPair);
                
            }
            //groupOperLink.Clear();
            //groupOperLink = null;
        }

        return groupOperLinkList;
    }

    // ADDED ON 13/08/2019 : /////////////////////////
    protected List<KeyValuePair<string, ArrayList>> GetGroupOperLinkList(ArrayList treatGroupList, ArrayList groupOpersList, ArrayList operationsList)
    {
        List<KeyValuePair<string, ArrayList>> groupOperLinkList = new List<KeyValuePair<string, ArrayList>>();
        string keyGroup, valGroup;

        foreach (/*ListItem*/ WebServiceListItem itemGroup in treatGroupList)
        {
            keyGroup = itemGroup.Value;
            ArrayList groupOperLink = new ArrayList();

            foreach (/*ListItem*/ WebServiceListItem itemGroupOper in groupOpersList)
            {
                if (itemGroupOper.Value == keyGroup)
                {
                    valGroup = itemGroupOper.Text;
                    foreach (/*ListItem*/ WebServiceListItem operation in operationsList)
                    {
                        if (operation.Value == valGroup)
                        {
                            KeyValuePair<string, string> pair = new KeyValuePair<string, string>(operation.Value, operation.Text);
                            groupOperLink.Add(pair);
                        }
                    }
                }
            }
            //
            if (groupOperLink.Count > 0)
            {
                KeyValuePair<string, ArrayList> extPair = new KeyValuePair<string, ArrayList>(keyGroup, groupOperLink);
                groupOperLinkList.Add(extPair);

            }
            //groupOperLink.Clear();
            //groupOperLink = null;
        }

        return groupOperLinkList;
    }
    // END ADDED /////////////////////////////////////

    protected void FillDynamicDropDownList(DropDownList dropDownList, ArrayList arrayList)
    {
        //if (dropDownList.Items.Count > 1)
        //    dropDownList.Items.Clear();

        foreach (KeyValuePair<string, string> pair in arrayList) // WebServiceListItemLOCAL wsItem in arrayList
        {
            if (string.IsNullOrEmpty(pair.Key) || string.IsNullOrEmpty(pair.Value))
                continue;

            ListItem item = new ListItem(pair.Value, pair.Key);
            if (!dropDownList.Items.Contains(item))
                dropDownList.Items.Add(item);
        }
    }

    protected void oldAppointmentsGridView_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    // added on 24/07/2019
    protected void operDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string operation = operDropDownList.SelectedItem.Text;
        string warning = string.Format("{0}: {1}", operation, DUPLICATION_WARNING);  // "{0}:\r\n{1}"

        if (oldAppointmentsGridView.Rows.Count > 0)
        {
            foreach (GridViewRow row in oldAppointmentsGridView.Rows)
            {
                if (row.Cells[0].Text == operation)
                {
                    operDropDownList.SelectedIndex = -1;
                    //MessageBox.Show(warning, "זימון תורים");
                    break;
                }
            }
        }
        if (operDropDownList.SelectedIndex > 0)
        {
            siteDropDownList.Enabled = true;
            (Session["session"] as SessionInfo).OperationDuplicated = false;    // ADDED ON 08/08/2019 
            (Session["session"] as SessionInfo).Warning = string.Empty;         // ADDED ON 08/08/2019
        }
        else
        {
            //MessageBox.Show(warning, "זימון תורים");
            siteDropDownList.Enabled = false;
            (Session["session"] as SessionInfo).OperationDuplicated = true; // ADDED ON 08/08/2019
            (Session["session"] as SessionInfo).Warning = warning;          // ADDED ON 08/08/2019
            Response.Redirect("step2.aspx");
            //Server.Transfer("step2.aspx");
        }
    }

    protected void cancelAppointment_Click(object sender, ImageClickEventArgs e)
    {
        bool deleteAppointment = DeleteAppointment.Checked;
        if (deleteAppointment)
        {
            string Code = ((GridViewRow)((ImageButton)sender).Parent.Parent).Cells[5].Text;
            string date_time = string.Format("{0} {1}", 
                ((GridViewRow)((ImageButton)sender).Parent.Parent).Cells[3].Text,
                ((GridViewRow)((ImageButton)sender).Parent.Parent).Cells[2].Text);
            int line_code = 0, diarynum = 0;
            int conf_status = APP_CONFIRMATION_STATUS;
            bool status = true;
            if (string.IsNullOrEmpty(Code) || !int.TryParse(Code, out line_code) || line_code <= 0)
                return;
            
            WebServiceAppointments wsAppointments = null;
            WebServiceAppointmentsLOCAL wsAppointmentsLocal = null;

            try
            {
                if (isRemote)
                {
                    wsAppointments = new WebServiceAppointments();
                    if (wsAppointments != null)
                    {
                        status = int.TryParse(wsAppointments.SetAppointmentConfirmationStatus(line_code, conf_status), out diarynum);
                        // added on 28/11/2019 : //////////////////////////////////////////////////
                        if (diarynum > 0)
                        {
                            line_code = wsAppointments.AddTime(diarynum, date_time); // ATTENTION !!! COMMENTED TEMPORARY !!!
                            if (line_code <= 0)
                                status = false;
                        }
                        // end added on 28/11/2019 ////////////////////////////////////////////////
                    }
                    wsAppointments.Dispose();
                }
                else
                {
                    wsAppointmentsLocal = new WebServiceAppointmentsLOCAL();
                    if (wsAppointmentsLocal != null)
                    {
                        status = int.TryParse(wsAppointmentsLocal.SetAppointmentConfirmationStatus(line_code, conf_status), out diarynum);
                        // added on 28/11/2019 : //////////////////////////////////////////////////
                        if (diarynum > 0)
                        {
                            line_code = wsAppointmentsLocal.AddTime(diarynum, date_time);
                            if (line_code <= 0)
                                status = false;
                        }
                        // end added on 28/11/2019 ////////////////////////////////////////////////
                    }
                    wsAppointmentsLocal.Dispose();
                }
            }
            catch (Exception)
            {
                Session.Abandon();
            }

            if (status)
            {
                Response.Redirect("step2.aspx");
                //Server.Transfer("step2.aspx");
            }

        }
    }
    // END ADDED /////////////////////////////////////
}