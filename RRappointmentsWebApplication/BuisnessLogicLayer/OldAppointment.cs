using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public class OldAppointment
    {
        private string appointmentTime;
        private string appointmentDate;
        private string appointmentLocation;
        private string appointmentOperName; // added on 09/06/2013
        private string appointmentDay;
        private string instruction;         // added on 11/11/2013
        private string lineCode;            // added on 30/07/2019

        public string AppointmentTime
        {
            get { return appointmentTime; }
            set { appointmentTime = value; }
        }
        
        public string AppointmentDate
        {
            get { return appointmentDate; }
            set { appointmentDate = value; }
        }    

        public string AppointmentLocation
        {
            get { return appointmentLocation; }
            set { appointmentLocation = value; }
        }

        public string AppointmentDay
        {
            get { return appointmentDay; }
            set { appointmentDay = value; }
        }

        // added on 09/06/2013
        public string AppointmentOperName
        {
            get { return appointmentOperName; }
            set { appointmentOperName = value; }
        }

        // added on 11/11/2013 : ///////////////
        public string Instruction
        {
            get { return instruction; }
            set { instruction = value; }
        }

        // added on 30/07/2019 : ////////////////
        public string LineCode
        {
            get { return lineCode; }
            set { lineCode = value; }
        }
        // end added /////////
    }
}