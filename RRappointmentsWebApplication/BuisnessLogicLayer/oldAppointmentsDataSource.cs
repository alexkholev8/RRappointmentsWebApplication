using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public class oldAppointmentsDataSource
    {
        public static RRappointmentsWebApplication.AppointmentsDBDataSet.OldAppointmentsDataTable oldAppointemntsDataTable = new AppointmentsDBDataSet.OldAppointmentsDataTable();
        public static RRappointmentsWebApplication.AppointmentsDBDataSet.OldAppointmentsDataTable Select()
        {
            return oldAppointemntsDataTable;
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