using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public class AppointmentsLogic
    {
        public static bool GetWsSiteList(string oper_code)
        {
            bool staticDataErrorFound = false;
            staticDataErrorFound = WebServiceAppInfo.GetSiteList(oper_code);
            return staticDataErrorFound;
        }
        public static bool GetWSinformation()
        {
            bool staticDataErrorFound = false;
            staticDataErrorFound = WebServiceAppInfo.GetInsuranceList();   
            staticDataErrorFound = WebServiceAppInfo.GetOperList();
            return staticDataErrorFound;
        }
    }
}