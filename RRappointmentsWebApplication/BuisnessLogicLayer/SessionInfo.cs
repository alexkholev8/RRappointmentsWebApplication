using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    //[Serializable]
    public class SessionInfo
    {
        private string id_num;
        private string first_name;
        private string cust_num;
        private string userPassword;
        private string lastName;
        private string fileNum;
        private string loginStatus;     // added on 04/07/2013
        private DateTime validPassword; // added on 29/07/2020

        // discarded on 04/07/2013 : //////////////////////
        //private string appointmentDate;
        //private string email;
        //private bool isLogIn;
        //private int logInsCount;
        // end discarded //////////////////////////////////
        private List<AvailableAppointment> availableAppointments;   // Re-introduced on 19/08/2013
        private List<OldAppointment> oldAppointments;   // added on 22/08/2013

        public string FileNum
        {
            get { return fileNum; }
            set { fileNum = value; } 
        }

        public string UserPassword
        {
            get { return userPassword; }
            set { userPassword = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Cust_num
        {
            get { return cust_num; }
            set { cust_num = value; }
        }

        public string First_name
        {
            get { return first_name; }
            set { first_name = value; }
        }

        public string Id_num
        {
            get { return id_num; }
            set { id_num = value; }
        }

        // added on 04/07/2013 : ///////////
        public string LoginStatus
        {
            get { return loginStatus; }
            set { loginStatus = value; }
        }

        // added on 29/07/2020 : ///////////
        public DateTime ValidPassword
        {
            get { return validPassword; }
            set { validPassword = value; }
        }

        // Re-introduced on 19/08/2013 : ////////////////////////
        public List<AvailableAppointment> AvailableAppointments
        {
            get { return availableAppointments; }
            set { availableAppointments = value; }
        }
        // end re-introduced ////////////////////////////////////

        // added on 22/08/2013 : /////////////////////
        public List<OldAppointment> OldAppointments
        {
            get { return oldAppointments; }
            set { oldAppointments = value; }
        }

        public List<KeyValuePair<string, ArrayList>> GroupOperLinkList
        {
            get; set;
        }

        public bool OperationDuplicated { get; set; }   // ADDED ON 30/07/2019
        public string Warning { get; set; }             // ADDED ON 30/07/2019

        public ArrayList GroupMessageList { get; set; }   // added on 27/11/2019
        // end added /////////////////////////////////
    }
}