using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Configuration;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public class ForgotPasswordLogic
    {

        internal static string RecoverUserPassword(string userId)
        {
            bool userExists = false;
            //bool userDataExists = false;
            bool wsPasswordRecoverSuccess = false;
            userExists = DataBaseAccessLayer.ForgotPasswordDataBaseAccessLogic.CheckIfUserExists(userId);
            if (userExists)
            {
                wsPasswordRecoverSuccess =  WSrecoverPassword(userId);
                if (wsPasswordRecoverSuccess)
                {
                    return "success";
                }
                else
                {
                    return "wsError";
                }

            }
            else
            {
                return "id not found";
            }
        }

        private static bool WSrecoverPassword(string userId)
        {
            string url = "http://" + ConfigurationManager.AppSettings["WebServiceIP"].ToString() + ConfigurationManager.AppSettings["WebServicePort"].ToString() + "/"+ConfigurationManager.AppSettings["wsDBname"].ToString() +"/ws_recover_password?user_id=" + userId;
            try
            {
                XmlReader oldAppointmentsReader = XmlReader.Create(url,BuisnessLogicLayer.WebServiceSecurity.GetXmlReaderSettings());

                while (oldAppointmentsReader.Read())
                {
                    if (oldAppointmentsReader.Name == "SQLerror")
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}