using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRappointmentsWebApplication.DataBaseAccessLayer
{
    public class DataBaseAccessTableAdapters
    {
        public static AppointmentsDBDataSetTableAdapters.UsersTableAdapter usersTableAdapter = new AppointmentsDBDataSetTableAdapters.UsersTableAdapter();
        public static RRappointmentsWebApplication.AppointmentsDBDataSetTableAdapters.AvailabelAppointmentsTableTableAdapter availableAppointmentsTableAdapter = new RRappointmentsWebApplication.AppointmentsDBDataSetTableAdapters.AvailabelAppointmentsTableTableAdapter();
        public static RRappointmentsWebApplication.AppointmentsDBDataSetTableAdapters.OldAppointmentsTableAdapter oldAppointmentsDataAdapter = new RRappointmentsWebApplication.AppointmentsDBDataSetTableAdapters.OldAppointmentsTableAdapter();
        private DataBaseAccessTableAdapters()
        {
            
        }
    }
}