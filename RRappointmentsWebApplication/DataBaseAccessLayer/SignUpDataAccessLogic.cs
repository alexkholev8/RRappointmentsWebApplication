using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Xml;

namespace RRappointmentsWebApplication.DataBaseAccessLayer
{
    public static class SignUpDataBaseAccessLogic
    {
       
        public static bool CheckIfUserExists(string userId)
        {
            string url = "http://" + ConfigurationManager.AppSettings["WebServiceIP"].ToString() + ConfigurationManager.AppSettings["WebServicePort"].ToString() + "/"+ConfigurationManager.AppSettings["wsDBname"].ToString() +"/ws_check_user_id_exists?id_num=" + userId;
            XmlReader logInReader = XmlReader.Create(url, BuisnessLogicLayer.WebServiceSecurity.GetXmlReaderSettings());

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

        internal static bool AddNewUserToDataBase(BuisnessLogicLayer.UserInfo newUserInfo)
        {
            AppointmentsDBDataSet.UsersDataTable usersDataTable = new  AppointmentsDBDataSet.UsersDataTable();
            DataBaseAccessLayer.DataBaseAccessTableAdapters.usersTableAdapter.Fill(usersDataTable);
            AppointmentsDBDataSet.UsersRow usersRow = usersDataTable.NewUsersRow();
            usersRow.UserId = newUserInfo.Id_num;
            usersRow.FirstName = newUserInfo.First_name;
            usersRow.LastName = newUserInfo.LastName;
            usersRow.Email = newUserInfo.Email;
            usersRow.PhoneNumber = newUserInfo.PhoneNumber;
            usersRow.AreaCode = newUserInfo.AreaCode;
            usersRow.IsLogedIn = false;
            usersRow.LogInsCount = 0;
            usersRow.SignUpTime = DateTime.Now;
            usersRow.userPassword = newUserInfo.Password;
            usersRow.is_active = true;

            usersDataTable.Rows.Add(usersRow);
            DataBaseAccessLayer.DataBaseAccessTableAdapters.usersTableAdapter.Update(usersRow);
            return true;
        }
    }
}