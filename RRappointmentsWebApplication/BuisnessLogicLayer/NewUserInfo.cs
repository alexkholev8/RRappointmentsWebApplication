using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public class UserInfo
    {
        private string id_num;
        private string first_name;
        private string lastName;
        private string email;
        private string areaCode;
        private string phoneNumber;
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public string AreaCode
        {
            get { return areaCode; }
            set { areaCode = value; }
        }   

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
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
    }
}