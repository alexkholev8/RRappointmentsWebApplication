using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RRappointmentsWebApplication.BuisnessLogicLayer;
using System.Web.Security;
using RRappointmentsWebApplication;
using System.Net;
using System.IO;

public partial class step4 : System.Web.UI.Page
{
    public static string WcfMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
        //AssignedAppointment createdAppointment = WebServiceAppInfo.CreatedAppointments[(Session["session"] as SessionInfo).Id_num];
        if (!IsPostBack)
        {
            if (PreviousPage == null)
            {
                Server.Transfer("index.aspx");
            }

            AssignedAppointment createdAppointment = WebServiceAppInfo.CreatedAppointments[(Session["session"] as SessionInfo).Id_num];
            nameLabel.Text      = (Session["session"] as SessionInfo).First_name + " " + (Session["session"] as SessionInfo).LastName;
            dateLabel.Text      = createdAppointment.AppointmentDate;
            timeLabel.Text      = createdAppointment.Time;
            dayLabel.Text       = createdAppointment.Day;
            inuranceLabel.Text  = createdAppointment.Insurance;
            siteLabel.Text      = createdAppointment.Site;
            treatLabel.Text     = createdAppointment.Treatment;
            addressLabel.Text   = createdAppointment.Address; // added on 11/10/2018
            telephoneLabel.Text = createdAppointment.Phone; // added on 11/10/2018

            if (!string.IsNullOrEmpty(WcfMessage))
            {
                WcfWarning.Text = WcfMessage;
                WcfWarning.Visible = true;
            }

            if (String.IsNullOrEmpty(createdAppointment.TreatmentInstructions))
            {
                intructionTitleLabel.Visible = false;
                treatmentInstructionsTextBox.Visible = false;
            }
            else
            {
                intructionTitleLabel.Visible = true;
                treatmentInstructionsTextBox.Text = createdAppointment.TreatmentInstructions;
            }
        }
    }

    protected void print_Click(object sender, ImageClickEventArgs e)
    {

    }

    private void RegisterClientScript(AssignedAppointment createdAppointment)
    {
        string scriptTreatCode = string.Format("var treatCode = '{0}';", createdAppointment.TreatCode);
        if (!ClientScript.IsClientScriptBlockRegistered("TreatCode"))
        {
            ClientScript.RegisterClientScriptBlock(typeof(string), "TreatCode", scriptTreatCode, true);
        }
    }

    protected void back_Click(object sender, ImageClickEventArgs e)
    {
        GeneralStaticLogic.UserLogIn = true;
        Response.Redirect("step2.aspx");
    }

    protected void end_Click(object sender, ImageClickEventArgs e)
    {

    }
}