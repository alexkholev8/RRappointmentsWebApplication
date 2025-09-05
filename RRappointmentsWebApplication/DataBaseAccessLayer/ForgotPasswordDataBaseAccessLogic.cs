using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Xml;
using System.Configuration;

namespace RRappointmentsWebApplication.DataBaseAccessLayer
{
    public class ForgotPasswordDataBaseAccessLogic
    {
        internal static bool CheckIfUserExists(string userId)
        {
            string url = "http://" + ConfigurationManager.AppSettings["WebServiceIP"].ToString() + ConfigurationManager.AppSettings["WebServicePort"].ToString() + "/"+ConfigurationManager.AppSettings["wsDBname"].ToString() +"/ws_check_user_id_exists?id_num=" + userId;
            XmlReader logInReader = XmlReader.Create(url,BuisnessLogicLayer.WebServiceSecurity.GetXmlReaderSettings());

            while (logInReader.Read())
            {
                if (logInReader.Name == "row")
                {
                    logInReader.MoveToAttribute("id_num");
                    return true;
                }
            }
            return false;
        }

        internal static bool GetUserPhoneAndEmail(string userId, ref BuisnessLogicLayer.UserInfo userInfo)
        {
            DataTable userInforDataTable = new DataTable();
            userInforDataTable = DataBaseAccessLayer.DataBaseAccessTableAdapters.usersTableAdapter.GetPasswordRecoveryData(userId);
            if (userInforDataTable.Rows.Count == 1)
            {
                userInfo.Email = userInforDataTable.Rows[0]["Email"].ToString();
                userInfo.AreaCode = userInforDataTable.Rows[0]["AreaCode"].ToString();
                userInfo.PhoneNumber = userInforDataTable.Rows[0]["PhoneNumber"].ToString();
                return true;
            }
            return false;
        }
    }
}