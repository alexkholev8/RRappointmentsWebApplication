using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using RRappointmentsWebApplication.BuisnessLogicLayer;
using WSLI = RRappointmentsWebApplication.BuisnessLogicLayer.WebServicesLocationInfo;

// ADDED FOR REMOTE WEB SERVICE DEFINITION : /////////////////////////////////////////////////////////////////////
using WebServiceAppointments = RRappointmentsWebApplication.AppointmentsWebReference.Appointments;
using WebServiceOldAppointment = RRappointmentsWebApplication.AppointmentsWebReference.OldAppointment;

// ADDED ON 08/09/2013 FOR LOCAL WEB SERVICE DEFINITION : ////////////////////////////////////////////////////////
using WebServiceAppointmentsLOCAL = RRappointmentsWebApplication.AppointmentsLocalWebReference.Appointments;
using WebServiceOldAppointmentLOCAL = RRappointmentsWebApplication.AppointmentsLocalWebReference.OldAppointment;
// END WEB SERVICES RELATED ADDITIONS ////////////////////////////////////////////////////////////////////////////

namespace RRappointmentsWebApplication
{
    public partial class PrintOldAppointments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterClientScript(IsPostBack);   // added on 18/07/2013
            // OnInitOldAppointmentsGrid();     // added on 25/06/2013 // discarded on 20/08/2013
            // ADDED ON 20/08/2013
            if (!IsPostBack)
            {
                GetOldAppointments();
            }
        }

        // ADDED ON 20/08/2013 : //////////////////////////////////////////////////////////////////
        private void GetOldAppointments()
        {
            // CORRECTIONS AND ADDITIONS SINCE 08/09/2013 : ///////////////////////////////////////
            bool isRemote = (Application[WSLI.WEBSRV_ISREMOTE].ToString() == "false") ? false : true;
            WebServiceAppointments wsAppointments = null;
            WebServiceAppointmentsLOCAL wsAppointmentsLocal = null;
            string Id_Num = (Session["session"] as SessionInfo).Id_num;
            string fileNum = (Session["session"] as SessionInfo).FileNum;

            if (isRemote)
            {
                wsAppointments = new WebServiceAppointments();
                if (wsAppointments != null)
                {
                    GetOldAppointments(wsAppointments, Id_Num, fileNum);
                }
            }
            else
            {
                wsAppointmentsLocal = new WebServiceAppointmentsLOCAL();
                if (wsAppointmentsLocal != null)
                {
                    GetOldAppointments(wsAppointmentsLocal, Id_Num, fileNum);
                }
            }
        }

        private void GetOldAppointments(WebServiceAppointments wsAppointments, string id_Num, string fileNum)
        {
            WebServiceOldAppointment[] wsOldAppointments = wsAppointments.GetOldAppointmentsWithInstructions(id_Num, fileNum);
            if (wsOldAppointments != null && wsOldAppointments.Length > 0)
            {
                oldAppointmentsGridView.DataSource = wsOldAppointments;
                oldAppointmentsGridView.DataBind();
            }
        }

        // OVERLOADED METHOD ADDED ON 09/09/2013 : ///////////////////////////////////////////////////////////////
        private void GetOldAppointments(WebServiceAppointmentsLOCAL wsAppointments, string id_Num, string fileNum)
        {
            WebServiceOldAppointmentLOCAL[] wsOldAppointments = wsAppointments.GetOldAppointmentsWithInstructions(id_Num, fileNum);
            if (wsOldAppointments != null && wsOldAppointments.Length > 0)
            {
                oldAppointmentsGridView.DataSource = wsOldAppointments;
                oldAppointmentsGridView.DataBind();
            }
        }
        // END ADDED OVERLOADED METHOD ///////////////////////////////////////////////////////////////////////////

        // CORRECTIONS AND ADDITIONS SINCE 25/06/2013 : ///////////////////////////////////////////
        //private void OnInitOldAppointmentsGrid()
        //{
        //    DataTable dt = new DataTable();

        //    dt.Columns.Add("oper_name");
        //    dt.Columns.Add("c_branch");
        //    dt.Columns.Add("dtime");
        //    dt.Columns.Add("ddate");
        //    dt.Columns.Add("day");
        //    dt.Rows.Add();
        //    oldAppointmentsGridView.DataSource = dt;
        //    oldAppointmentsGridView.DataBind();
        //    oldAppointmentsGridView.Rows[0].Visible = false;
        //}

        // added on 18/07/2013 : //////////////////////////////////////////////////////////////////
        private void RegisterClientScript(bool isPostBack)
        {
            string scriptUserId = string.Format("var userId = '{0}';", (Session["session"] as SessionInfo).Id_num);
            string scriptFileNum = string.Format("var fileNum = '{0}';", (Session["session"] as SessionInfo).FileNum);

            if (!ClientScript.IsClientScriptBlockRegistered("Id_Num"))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "Id_Num", scriptUserId, true);
            }
            if (!ClientScript.IsClientScriptBlockRegistered("FileNum"))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "FileNum", scriptFileNum, true);
            }

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
        // end added //////////////////////////////////////////////////////////////////////////////
    }
}