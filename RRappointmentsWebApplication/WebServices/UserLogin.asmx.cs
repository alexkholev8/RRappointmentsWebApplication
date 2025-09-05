using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Data.Odbc;
using RRappointmentsWebApplication.BuisnessLogicLayer;

namespace RRappointmentsWebApplication.WebServices
{
    /// <summary>
    /// Summary description for UserLogin
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class UserLogin : System.Web.Services.WebService
    {
        private enum SessionParams
        {
            custNum, fileNum
        };

        private const string USER_VERIFIED = "User Verified";
        private const string PWD_INCORRECT = "Password Incorrect";
        private const string USER_NOTFOUND = "User Not Found";
        private const string USER_NOTACTIV = "User Not Active";
        private const string SERVICE_ERROR = "WebService Error";

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public SessionInfo CheckUserLogin(string jsonPassword, string jsonUserId)
        {
            SessionInfo sessionInfo = new SessionInfo();
            string logInAttemptStatus = OnLogIn(jsonUserId, jsonPassword, ref sessionInfo);
            Generic.WriteLog(Generic.GetLogString(0, "UserLogin", "CheckUserLogin"));

            switch (logInAttemptStatus)
            {
                case USER_VERIFIED: // "User Verified":
                    {
                        // FormsAuthentication.RedirectFromLoginPage((Session["session"] as SessionInfo).Id_num, false);
                        //GeneralStaticLogic.UserLogIn = true;
                        logInAttemptStatus = "OK";
                    }
                    break;

                case PWD_INCORRECT: // "Password Incorrect":
                    {
                        logInAttemptStatus = "אחד או יותר מהפרטים שהקשת שגויים. נא נסה שנית.";
                    }
                    break;

                case USER_NOTFOUND: // "User Not Found":
                    {
                        logInAttemptStatus = "אחד או יותר מהפרטים שהקשת שגויים. נא נסה שנית.";
                    }
                    break;

                case USER_NOTACTIV: // "User Not Active":
                    {
                        logInAttemptStatus = "כניסתך למערכת נחסמה.";
                    }
                    break;

                case SERVICE_ERROR: // "WebService Error":
                    {
                        logInAttemptStatus = "אין אפשרות לספק את השירות כרגע. נא לנסות שנית במועד מאוחר יותר.";
                    }
                    break;
            } // end switch
            sessionInfo.LoginStatus = logInAttemptStatus;
            Generic.WriteLog(Generic.GetLogString(1, "UserLogin", "CheckUserLogin"));

            return sessionInfo;
        }

        [WebMethod]
        private string OnLogIn(string userId, string password, ref SessionInfo sessionInfo)
        {
            string status = SERVICE_ERROR;
            // OdbcConnection conn = GetOpenedConnection();
            OdbcConnection conn = Generic.GetOpenedConnection();
            if (conn == null)
                return status;

            try
            {
                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                status = OnVerifyUserId(userId, ref sessionInfo, cmd);
                if (status == USER_VERIFIED)
                {
                    status = OnVerifyUser(userId, password, ref sessionInfo, cmd);
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    status = SERVICE_ERROR;
                    Generic.WriteLog(status + ": " + ex.Message);
                }
            }
            finally
            {
                Generic.CloseConnection(conn);       
            }

            return status;
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
        private string OnVerifyUserId(string userId, ref SessionInfo sessionInfo, OdbcCommand cmd)
        {
            bool isFound = false;
            cmd.CommandText = "call sp_zimun_LogIn (?)";
            OdbcParameter param = new OdbcParameter();
            param.ParameterName = "@id_num";
            param.Direction = ParameterDirection.Input;
            // param.OdbcType = OdbcType.Int;   // Disabled on 18/08/2013
            param.OdbcType = OdbcType.VarChar;  // Corrected on 18/08/2013
            param.Value = userId;
            cmd.Parameters.Add(param);
        
            try
            {
                CheckParameter(ref isFound, param, ref sessionInfo, cmd, SessionParams.custNum);
                cmd.CommandText = "call sp_login_get_file_num (?)";
                CheckParameter(ref isFound, param, ref sessionInfo, cmd, SessionParams.fileNum);
            }
            catch (Exception ex)
            {
                Generic.WriteLog(SERVICE_ERROR + ": " + ex.Message);
                return SERVICE_ERROR;
            }

            return (isFound) ? USER_VERIFIED : USER_NOTFOUND;
        }

        [WebMethod]
        private void CheckParameter(ref bool isFound, OdbcParameter oParam, ref SessionInfo sessionInfo, OdbcCommand cmd, SessionParams sessionParam)
        {
        //  cmd.ExecuteNonQuery();  // optional
            DataTable dtData = new DataTable();
            try
            {
                using (OdbcDataAdapter dataAdapter = new OdbcDataAdapter(cmd))
                {
                    dataAdapter.Fill(dtData);
                }

                if (dtData == null)
                {
                    throw new Exception(cmd.CommandText + " returned null");
                }

                if (dtData.Columns.Count == 1 && dtData.Rows.Count == 1)
                {
                    isFound = true;
                    if (sessionParam == SessionParams.custNum)
                        sessionInfo.Cust_num = Convert.ToString(dtData.Rows[0][0]);
                    else
                        sessionInfo.FileNum = Convert.ToString(dtData.Rows[0][0]);
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
            finally
            {
                dtData.Dispose();
            }
        }

        [WebMethod]
        private string OnVerifyUser(string userId, string password, ref SessionInfo sessionInfo, OdbcCommand cmd)
        {
            string status = string.Empty;
            cmd.CommandText = "call sp_check_user_id_exists (?)";
        //  cmd.ExecuteNonQuery();  // optional
            DataTable dtData = new DataTable();
            try
            {
                using (OdbcDataAdapter dataAdapter = new OdbcDataAdapter(cmd))
                {
                    dataAdapter.Fill(dtData);
                }

                if (dtData == null)
                {
                    throw new Exception(cmd.CommandText + " returned null");
                }

                if (dtData.Columns.Count == 5 && dtData.Rows.Count == 1)    // columns number extended on 29/07/2020 for password update
                {
                    sessionInfo.Id_num          = userId;
                    sessionInfo.First_name      = Convert.ToString(dtData.Rows[0][2]);
                    sessionInfo.LastName        = Convert.ToString(dtData.Rows[0][3]);
                    sessionInfo.UserPassword    = Convert.ToString(dtData.Rows[0][1]);
                    // added on 29/07/2020 for password update : //////////////////////////////////
                    sessionInfo.ValidPassword   = (dtData.Rows[0][4] == DBNull.Value) ?
                                                    DateTime.Now.Date :
                                                    Convert.ToDateTime(dtData.Rows[0][4]).Date;
                    // end added on 29/07/2020 for password update //////////////////////////////// 
                }
            }
            catch (Exception ex)
            {
                status = SERVICE_ERROR;
                Generic.WriteLog(status + ": " + ex.Message);
            }
            finally
            {
                dtData.Dispose();
            }

            if (string.IsNullOrEmpty(status))
                status = (sessionInfo.UserPassword == password) ? USER_VERIFIED : PWD_INCORRECT;
            return status;
        }
    }
}
