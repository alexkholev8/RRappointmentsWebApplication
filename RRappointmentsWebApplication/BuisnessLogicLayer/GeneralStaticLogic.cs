using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public class GeneralStaticLogic
    {
        private static bool userLogIn = false;
        private static bool pwdUpdate = false;  // added on 29/07/2020 for password update

        public static bool UserLogIn
        {
            get { return userLogIn; }
            set { userLogIn = value; }
        }

        // added on 29/07/2020 for password update : //////
        public static bool PwdUpdate
        {
            get { return pwdUpdate; }
            set { pwdUpdate = value; }
        }
        // end added on 29/07/2020 for password update ////

    }
}