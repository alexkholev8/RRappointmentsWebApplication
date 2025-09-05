using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    //[Serializable]
    public class AvailableAppointment
    {
        string dline;
        string diarynum;
        string ddate;
        string dtime;
        string diary_name;
        string dday;    // added on 10/06/2013 

        public string Diary_name
        {
            get { return diary_name; }
            set { diary_name = value; }
        }

        public string Dtime
        {
            get { return dtime; }
            set { dtime = value; }
        }

        public string Ddate
        {
            get { return ddate; }
            set { ddate = value; }
        }

        public string Diarynum
        {
            get { return diarynum; }
            set { diarynum = value; }
        }

        public string Dline
        {
            get { return dline; }
            set { dline = value; }
        }

        // added on 10/06/2013
        public string Dday
        {
            get { return dday; }
            set { dday = value; }
        }
        // end added
    }
}