using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Data.Odbc;
using System.Web.UI.WebControls;
using RRappointmentsWebApplication.BuisnessLogicLayer;

namespace RRappointmentsWebApplication.WebServices
{
    /// <summary>
    /// Summary description for NewUserSignUp
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class NewUserSignUp : System.Web.Services.WebService
    {
        private const string STATUS_EXISTS = "Exists";
        private const string STATUS_FAILED = "Failed";
        private const string STATUS_SUCCESS = "Success";

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]    // ADDED
        public string SignUpNewUserPerObject(UserInfo jsonUserInfo)  // userInfo
        {
            Generic.WriteLog(Generic.GetLogString(0, "NewUserSignUp", "SignUpNewUserPerObject"));   // ADDED ON 03/09/2013
            OdbcConnection conn = Generic.GetOpenedConnection();
            if (conn == null)
                return STATUS_FAILED;

            if (Generic.IsUserAlreadyExists(jsonUserInfo.Id_num, conn)) // userInfo
            {
                return STATUS_EXISTS;
            }

            if (string.IsNullOrEmpty(jsonUserInfo.Password))
            {
                jsonUserInfo.Password = GeneratePassword();
            }

            Generic.WriteLog(Generic.GetLogString(1, "NewUserSignUp", "SignUpNewUserPerObject"));   // ADDED ON 03/09/2013

            return OnSignUpNewUser(jsonUserInfo, conn); // userInfo
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]    // ADDED
        public string SignUpNewUserPerParams(string jsonId, string jsonFirstName, string jsonLastName, string jsonEmail, string jsonAreaCode, string jsonPhoneNumber, string jsonPassword)
        {
            Generic.WriteLog(Generic.GetLogString(0, "NewUserSignUp", "SignUpNewUserPerParameters"));   // ADDED ON 03/09/2013
            OdbcConnection conn = Generic.GetOpenedConnection();
            if (conn == null)
                return STATUS_FAILED;

            if (Generic.IsUserAlreadyExists(jsonId, conn)) // userInfo
            {
                return STATUS_EXISTS;
            }
            
            if (string.IsNullOrEmpty(jsonPassword))
            {
                jsonPassword = GeneratePassword();
            }
            UserInfo userInfo = new UserInfo
            {
                Id_num = jsonId,
                First_name = jsonFirstName,
                LastName = jsonLastName,
                Email = jsonEmail,
                AreaCode = jsonAreaCode,
                PhoneNumber = jsonPhoneNumber,
                Password = jsonPassword
            };
            
            Generic.WriteLog(Generic.GetLogString(1, "NewUserSignUp", "SignUpNewUserPerParameters"));   // ADDED ON 03/09/2013
            
            return OnSignUpNewUser(userInfo, conn); // userInfo
        }
       
        [WebMethod]
        private bool IsUserAlreadyExists(string userId, OdbcConnection conn)    // --> replace by Generic.IsUserAlreadyExists()
        {
            bool isExist = false;
            const string sProcedure = "sp_check_user_id_exists";
            const string sParameter = "id_num";
            string ret = null;
            OdbcCommand cmd = new OdbcCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.CommandText = string.Format("call {0} (?)", sProcedure);
            cmd.Parameters.AddWithValue('@' + sParameter, userId);

            try
            {
                OdbcDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret = reader[sParameter].ToString().Trim();
                    if (!string.IsNullOrEmpty(ret) || ret == userId)
                    {
                        isExist = true;
                        break;
                    }
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    isExist = false;
                    Generic.WriteLog(ex.Message.ToString());    // ADDED ON 22/08/2013
                }
            }
            finally
            {
                cmd.Dispose();
            }

            return isExist;
        }

        [WebMethod]
        private string OnSignUpNewUser(UserInfo userInfo, OdbcConnection conn)
        {
            string signUpStatus = null;
            const string sPocedure = "f_user_SignUp";  // "f_zimun_SignUp"
            OdbcCommand cmd = new OdbcCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.CommandText = string.Format("call {0} (?, ?, ?, ?, ?, ?, ?)", sPocedure);   // extended on 29/07/2020 for password update
            cmd.Parameters.AddWithValue("@first_name", userInfo.First_name);
            cmd.Parameters.AddWithValue("@last_name", userInfo.LastName);
            cmd.Parameters.AddWithValue("@id_num", userInfo.Id_num);
            cmd.Parameters.AddWithValue("@phone", userInfo.AreaCode + userInfo.PhoneNumber);
            cmd.Parameters.AddWithValue("@email", userInfo.Email);
            cmd.Parameters.AddWithValue("@userPassword", userInfo.Password);
            cmd.Parameters.AddWithValue("@validPassword", DateTime.Now.Date);   // added on 29/07/2020 for password update

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                signUpStatus = ex.Message.ToString();
                Generic.WriteLog(signUpStatus); // ADDED ON 03/09/2013
            }
            finally
            {
                cmd.Dispose();
                conn.Dispose();
                signUpStatus = (string.IsNullOrEmpty(signUpStatus)) ?
                    (userInfo.Password) : (STATUS_FAILED);
                Generic.CloseConnection(conn);
            }

            return signUpStatus;
        }

        [WebMethod]
        private OdbcConnection GetOpenedConnection()
        {
            OdbcConnection conn = null;
            try
            {
                conn = new OdbcConnection(@"DSN=Medical;Automatic_Timestamp=ON;SuppressWarnings=YES");
                if (conn != null)
                {
                    conn.Open();
                }
            }
            catch (Exception)
            {
                Generic.CloseConnection(conn);
            }

            return conn;
        }

        [WebMethod]
        protected internal static string GeneratePassword()
        {
            Random randPassword = new Random();
            return randPassword.Next(100000, 999999).ToString();
        }
    }
}
