using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRappointmentsWebApplication.DataBaseAccessLayer
{
    public class DataTables
    {
        public static AppointmentsDBDataSet.AvailabelAppointmentsTableDataTable availableAppointmentsDataTable = new AppointmentsDBDataSet.AvailabelAppointmentsTableDataTable();
        public static AppointmentsDBDataSet.OldAppointmentsDataTable oldAppointmentsDataTable = new AppointmentsDBDataSet.OldAppointmentsDataTable();
        public static AppointmentsDBDataSet.UsersDataTable usersDataDataTable = new AppointmentsDBDataSet.UsersDataTable();
        
    }
}