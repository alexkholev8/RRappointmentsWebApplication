using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public  class SetupSettings
    {        
        public string EnableTreatmentGroup
        {
            get { return ConfigurationManager.AppSettings["EnableTreatmentGroups"]; }
            set { ConfigurationManager.AppSettings["EnableTreatmentGroups"] = value; }
        }
    }
}