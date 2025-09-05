using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public class AssignedAppointment
    {
        private string firstName;
        private string inuranceCode;
        private string siteCode;
        private string treatCode;
        private string custCode;
        private string dline;
        private string appointmentDate;
        private string time;
        private string day;
        private string treatmentInstructions;

        // added on 17/06/2013 : ///////////
        private string lastName;
        private string insurance;
        private string site;
        private string treatment;
        // added on 09/10/2018 : ///////////
        private string phone;
        private string address;
        // end additions ///////////////////

        public string TreatmentInstructions
        {
            get { return treatmentInstructions; }
            set { treatmentInstructions = value; }
        }

        public string Day
        {
            get { return day; }
            set { day = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        public string AppointmentDate
        {
            get { return appointmentDate; }
            set { appointmentDate = value; }
        }

        public string Dline
        {
            get { return dline; }
            set { dline = value; }
        }

        public string CustCode
        {
            get { return custCode; }
            set { custCode = value; }
        }


        public string SiteCode
        {
            get { return siteCode; }
            set { siteCode = value; }
        }

        public string InuranceCode
        {
            get { return inuranceCode; }
            set { inuranceCode = value; }
        }

        public string TreatCode
        {
            get { return treatCode; }
            set { treatCode = value; }
        }

        // added on 17/06/2013 : ///////////
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Insurance
        {
            get { return insurance; }
            set { insurance = value; }
        }

        public string Site
        {
            get { return site; }
            set { site = value; }
        }

        public string Treatment
        {
            get { return treatment; }
            set { treatment = value; }
        }

        // added on 09/10/2018 : ///////////
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        // end additions ///////////////////
    }
}