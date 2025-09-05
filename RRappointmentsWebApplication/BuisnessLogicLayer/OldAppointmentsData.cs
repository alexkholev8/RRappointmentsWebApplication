using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public static class OldAppointmentsData
    {
        private static AppointmentsDBDataSet.OldAppointmentsDataTable oldAppointmentsDataTable = new AppointmentsDBDataSet.OldAppointmentsDataTable();

        public static AppointmentsDBDataSet.OldAppointmentsDataTable OldAppointmentsDataTable
        {
            get { return oldAppointmentsDataTable; }
            set { oldAppointmentsDataTable = value; }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static AppointmentsDBDataSet.OldAppointmentsDataTable GetOldAppointments()
        {
            AppointmentsDBDataSet appointmentsDataSet = new AppointmentsDBDataSet();

            foreach (AppointmentsDBDataSet.OldAppointmentsRow row in oldAppointmentsDataTable.Rows)
            {
                appointmentsDataSet.OldAppointments.Rows.Add(row);
            }

            return oldAppointmentsDataTable;
        }
    }
}