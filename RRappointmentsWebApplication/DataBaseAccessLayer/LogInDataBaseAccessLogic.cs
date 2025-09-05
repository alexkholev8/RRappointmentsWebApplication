using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Xml;
using System.Configuration;

namespace RRappointmentsWebApplication.DataBaseAccessLayer
{
    public static class LogInDataBaseAccessLogic
    {

        internal static string VerifyUser(string id, string password, ref BuisnessLogicLayer.SessionInfo sessionInfo)
        {
            string url = "http://" + ConfigurationManager.AppSettings["WebServiceIP"].ToString() + ConfigurationManager.AppSettings["WebServicePort"].ToString() + "/"+ConfigurationManager.AppSettings["wsDBname"].ToString() +"/ws_check_user_id_exists?id_num=" + id;
            try
            {
                XmlReader logInReader = XmlReader.Create(url, BuisnessLogicLayer.WebServiceSecurity.GetXmlReaderSettings());

                while (logInReader.Read())
                {
                    if (logInReader.Name == "row")
                    {
                        sessionInfo.Id_num = id;
                        logInReader.MoveToAttribute("first_name");
                        sessionInfo.First_name = logInReader.Value;
                        logInReader.MoveToAttribute("last_name");
                        sessionInfo.LastName = logInReader.Value;
                        logInReader.MoveToAttribute("inet_password");
                        sessionInfo.UserPassword = logInReader.Value;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                return "WS Error";
            }

            if (sessionInfo.UserPassword == password)
            {
                return "User Verified";
            }
            else return "Password Incorrect";
            
        }

        internal static void UpdateUserLogIn(string id, BuisnessLogicLayer.SessionInfo sessionInfo)
        {
            //DataBaseAccessLayer.DataBaseAccessTableAdapters.usersTableAdapter.UpdateUserLogIn(true, DateTime.Now, id);
            
        }
    }
}