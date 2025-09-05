using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using RRappointmentsWebApplication.Properties;
using System.Net;
using System.IO;
using System.Xml;
using System.Web.Security;
using System.Web.SessionState;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public class LogInLogic
    {
        internal static string LogIn(string id, string password, ref SessionInfo sessionInfo)
        {
            string userVerifyStatus;
            userVerifyStatus = WsLogIn(id,ref sessionInfo);
            if (userVerifyStatus == "User Verified")
            {
                userVerifyStatus = DataBaseAccessLayer.LogInDataBaseAccessLogic.VerifyUser(id, password, ref sessionInfo);
            }
            switch (userVerifyStatus)
            {
                case "User Verified":
                    {
                       // DataBaseAccessLayer.LogInDataBaseAccessLogic.UpdateUserLogIn(id, sessionInfo);
                    }
                    break;
            }
            return userVerifyStatus;
         }

        private static string WsLogIn(string id, ref SessionInfo sessionInfo)
        {
            bool userInfoFound = false;

            string idNum = id;
            string url = "http://"+ ConfigurationManager.AppSettings["WebServiceIP"].ToString() + ConfigurationManager.AppSettings["WebServicePort"].ToString() + "/"+ConfigurationManager.AppSettings["wsDBname"].ToString() +"/ws_xml_zimun_LogIn?id_num=" + idNum;
            try
            {
                XmlReader logInReader = XmlReader.Create(url,BuisnessLogicLayer.WebServiceSecurity.GetXmlReaderSettings());
               
                while (logInReader.Read())
                {
                    if (logInReader.Name == "row")
                    {
                        logInReader.MoveToAttribute("cust_num");
                        sessionInfo.Cust_num= logInReader.Value;

                        userInfoFound = true;
                        break;
                    }
                }

                url = "http://" + ConfigurationManager.AppSettings["wsUserName"].ToString() + ":" + ConfigurationManager.AppSettings["wsPassword"].ToString() + "@" + ConfigurationManager.AppSettings["WebServiceIP"].ToString() + ConfigurationManager.AppSettings["WebServicePort"].ToString() + "/"+ConfigurationManager.AppSettings["wsDBname"].ToString() +"/ws_login_get_file_num?id_num=" + idNum;
                logInReader = XmlReader.Create(url, BuisnessLogicLayer.WebServiceSecurity.GetXmlReaderSettings());
                while (logInReader.Read())
                {
                    if (logInReader.Name == "row")
                    {
                        logInReader.MoveToAttribute("file_num");
                        sessionInfo.FileNum = logInReader.Value;
                        break;
                    }
                }
            }

            catch (Exception)
            {
                return "WS Error";
            }

            if (userInfoFound) return "User Verified";
            else return "User Id Not Found";

        }

    }
}
