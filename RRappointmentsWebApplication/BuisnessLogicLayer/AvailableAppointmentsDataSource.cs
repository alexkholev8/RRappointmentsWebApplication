using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public class AvailableAppointmentsDataSource
    {
        public static RRappointmentsWebApplication.AppointmentsDBDataSet.AvailabelAppointmentsTableDataTable appointmentsDataTable = new RRappointmentsWebApplication.AppointmentsDBDataSet.AvailabelAppointmentsTableDataTable();
        public static RRappointmentsWebApplication.AppointmentsDBDataSet.AvailabelAppointmentsTableDataTable Select()
        {
            return appointmentsDataTable;
        }
        public static void Insert(string FirstName, string LastName /* ... */)
        {
            // connect to db, and do perform the insert
        }
        public static void Update(string FirstName, string LastName, /* ... */ long id)
        {
            // connect to db, and do perform the update
        }
        public static void Delete(long id)
        {
            // connect to db, and do perform the delete
        }
    }
}