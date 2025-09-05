using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RRappointmentsWebApplication.BuisnessLogicLayer;

namespace RRappointmentsWebApplication
{
    public partial class PrintAppointment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AssignedAppointment createdAppointment = WebServiceAppInfo.CreatedAppointments[(Session["session"] as SessionInfo).Id_num];
            
            // CORRECTIONS SINCE 23/06/2013 : /////////////////////////////////////////////////////
            nameLabel.Text      = createdAppointment.FirstName + " " + createdAppointment.LastName;  // + (Session["session"] as SessionInfo).LastName;
            dateLabel.Text      = createdAppointment.AppointmentDate;
            timeLabel.Text      = createdAppointment.Time;
            dayLabel.Text       = createdAppointment.Day;
            inuranceLabel.Text  = createdAppointment.Insurance;
            siteLabel.Text      = createdAppointment.Site;
            treatLabel.Text     = createdAppointment.Treatment;
            addressLabel.Text   = createdAppointment.Address;   // added on 14/10/2018
            telephoneLabel.Text = createdAppointment.Phone;     // added on 14/10/2018 
            instructionLabel.Text = createdAppointment.TreatmentInstructions;   // commented on 23/06/2013
            //RegisterClientScript(createdAppointment.TreatCode);
            // COMMENTED ON  23/06/2013 : /////////////////////////////////////////////////////////
            //inuranceLabel.Text = WebServiceAppInfo.InsuaranceDictionary[createdAppointment.InuranceCode];
            //siteLabel.Text = WebServiceAppInfo.SitesDictionary[createdAppointment.SiteCode];
            //treatLabel.Text = WebServiceAppInfo.TreatmentDictionary[createdAppointment.TreatCode];
            ///////////////////////////////////////////////////////////////////////////////////////

        // will be filled from client part 
        }

        private void RegisterClientScript(string operCode)
        {
            string scriptTreatCode = string.Format("var treatCode = '{0}';", operCode);
            if (!ClientScript.IsClientScriptBlockRegistered("TreatCode"))
            {
                ClientScript.RegisterClientScriptBlock(typeof(string), "TreatCode", scriptTreatCode, true);
            }
        }
    }
}