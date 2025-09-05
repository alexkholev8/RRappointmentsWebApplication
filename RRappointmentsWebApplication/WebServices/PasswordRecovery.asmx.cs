using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Data.Odbc;
using System.Web.UI.WebControls;
using System.Text;
using RRappointmentsWebApplication.BuisnessLogicLayer;

namespace RRappointmentsWebApplication.WebServices
{
    /// <summary>
    /// Summary description for PasswordRecovery
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PasswordRecovery : System.Web.Services.WebService
    {
        const string STATUS_ERROR       = "error";
        const string STATUS_NOT_FOUND   = "id not found";
        const string STATUS_SUCCESS     = "success";
        const string NOT_FOUND_ERROR    = "Table 'remote_settings' not found";
        const string SENDER_TITLE       = "ר.ר מערכות מידע וטכנולוגיות";
        const string PWD_UPDATE_ERROR   = "Failed to update password: ";  // added on 29/07/2020 for password update

#region UserPasswordRecoveryData_class_definition
        public class UserPasswordRecoveryData
        {   // CORRECTIONS SINCE 13/08/2013
            public readonly /*private const*/ string SMS_MESSAGE_PATTERN = "לקוח יקר, סיסמתך למערכת היא: ";
            public readonly /*private const*/ string EMAIL_MESSAGE_PATTERN = "<html><head><meta http-equiv=Content-Type content=\"text/html; charset=windows-1255\"><meta name=Generator content=\"Microsoft Word 11 (filtered medium)\"><title></title><head><body>{0}</body></html>";
            // CORRECTED ON 13/08/2013 : ////////////////////////////////////////////////
            public readonly /*private const*/ string SENDER_CELL_NUMBER = "089451735";
            public readonly /*private const*/ string SENDER_NAME = "rrsystems";
            public readonly /*private const*/ string SENDER_PASSWORD = "rrsystemsxml";
            public readonly /*private const*/ string SENDER_EMAIL = "NoReply@y-it.co.il";
            public readonly /*private const*/ string CONTENT_TYPE = "text/html";
            public readonly string SENDER_URL = "http://wap.y-it.co.il:8080/wapdb/ws_send_sms"; 
            // END CORRECTIONS //////////////////////////////////////////////////////////

            private string clientCellNumber;
            private string clientEmail;
            private string clientPassword;
            private string recoveryMsgSMS;
            private string senderEmailTitle;
            private string recoveryMsgEmail;

            // ADDED ON 13/08/2013 : /////////////////
            private string senderCellNumber;
            private string senderName;
            private string senderPassword;
            private string senderEmail;
            private string contentType;
            private string senderUrl;
            // END ADDED /////////////////////////////

            public string ClientCellNumber
            {
                get { return clientCellNumber; }
                set { clientCellNumber = value; }
            }

            public string ClientEmail
            {
                get { return clientEmail; }
                set { clientEmail = value; }
            }

            public string ClientPassword
            {
                get { return clientPassword; }
                set { clientPassword = value; }
            }

            public string SenderEmailTitle
            {
                get { return senderEmailTitle; }
                set { senderEmailTitle = value; }
            }

            public string RecoveryMsgSMS
            {
                get { return recoveryMsgSMS; }
                set { recoveryMsgSMS = value; }
            }

            public string RecoveryMsgEmail
            {
                get { return recoveryMsgEmail; }
                set { recoveryMsgEmail = value; }
            }

            // CORRECTED ON 13/08/2013 : /////////////
            public string SenderCellNumber
            {
                get { return senderCellNumber; }
                set { senderCellNumber = value; }
            }

            public string SenderName
            {
                get { return senderName; }
                set { senderName = value; }
            }

            public string SenderPassword
            {
                get { return senderPassword; }
                set { senderPassword = value; }
            }

            public string SenderEmail
            {
                get { return senderEmail; }
                set { senderEmail = SENDER_EMAIL; }

            }

            public string ContentType
            {
                get { return contentType; }
                set { contentType = CONTENT_TYPE; }
            }

            // ADDED ON 09/10/2013 : /////////////////
            public string SenderUrl
            {
                get { return senderUrl; }
                set { senderUrl = value; }
            }
            // END CORRECTED /////////////////////////

            public UserPasswordRecoveryData()
            {
            }

            public UserPasswordRecoveryData(string clientCellNumber, string clientPassword, string clientEmail, string senderEmailTitle)
            {
                ClientCellNumber = clientCellNumber;
                ClientEmail = clientEmail;
                ClientPassword = clientPassword;
                RecoveryMsgSMS = // Generic.ConvertToUTF8(SMS_MESSAGE_PATTERN) + clientPassword;  
                                 SMS_MESSAGE_PATTERN + clientPassword;
                // DISCARDED ON 25/09/2013 : /////////////////////////////////////////////////////////////////////
                // SenderEmailTitle = Generic.ConvertToUTF8(senderEmailTitle);
                // RecoveryMsgEmail = string.Format(EMAIL_MESSAGE_PATTERN, this.recoveryMsgSMS);
                // CORRECTED ON 25/09/2013 : /////////////////////////////////////////////////////////////////////
                SenderEmailTitle = senderEmailTitle;
                RecoveryMsgEmail = string.Format(EMAIL_MESSAGE_PATTERN, SMS_MESSAGE_PATTERN + clientPassword);
                // ADDED ON 13/08/2013 : ///////////// 
                SenderCellNumber = SENDER_CELL_NUMBER;
                SenderName = SENDER_NAME;
                SenderPassword = SENDER_PASSWORD;
                SenderEmail = SENDER_EMAIL;
                ContentType = CONTENT_TYPE;
                SenderUrl = SENDER_URL; // ADDED ON 09/10/2013
                // END ADDED /////////////////////////
            }
        }
#endregion

        /// <summary>
        /// New method for password recovery not via database
        /// </summary>
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public UserPasswordRecoveryData OnRecoverUserPassword(string jsonUserID)
        {
            Generic.WriteLog(Generic.GetLogString(0, "PasswordRecovery", "OnRecoverUserPassword"));
            UserPasswordRecoveryData pwdRecoveryData = null;
            // OdbcConnection conn = GetOpenedConnection();
            OdbcConnection conn = Generic.GetOpenedConnection();
            if (conn == null)
            {
                return pwdRecoveryData;
            }

            pwdRecoveryData = new UserPasswordRecoveryData();
            if (Generic.IsUserAlreadyExists(jsonUserID, conn))
            {
                pwdRecoveryData = GetPasswordRecoveryData(jsonUserID, conn);
                UpdatePasswordSenderRecoveryData(pwdRecoveryData, conn);  // ADDED ON 09/10/2013
            }

            Generic.CloseConnection(conn);
            Generic.WriteLog(Generic.GetLogString(1, "PasswordRecovery", "OnRecoverUserPassword"));

            return pwdRecoveryData;
        }

        // method added on 29/07/2020 for password update : ///////////////////
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public bool OnUpdateUserPassword(string jsonUserID)
        {
            bool status = true;
            Generic.WriteLog(Generic.GetLogString(0, "PasswordUpdate", "OnUpdateUserPassword"));
            OdbcConnection conn = Generic.GetOpenedConnection();
            if (conn == null)
            {
                return false;
            }

            if (Generic.IsUserAlreadyExists(jsonUserID, conn))
            {
                status = UpdateUserPassword(jsonUserID, conn);
            }

            Generic.CloseConnection(conn);
            Generic.WriteLog(Generic.GetLogString(1, "PasswordUpdate", "OnUpdateUserPassword"));

            return status;
        }
        // end method added on 29/07/2020 for password update /////////////////

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
                    Generic.WriteLog(ex.Message);
                }
            }
            finally
            {
                cmd.Dispose();
            }

            return isExist;
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
        private UserPasswordRecoveryData GetPasswordRecoveryData(string userId, OdbcConnection conn)
        {
            string password = string.Empty;
            string email = string.Empty;
            string phone = string.Empty;
            string smsTitle = string.Empty;
            UserPasswordRecoveryData data = null;
            if (RetrievePasswordRecoveryData(userId, conn, ref password, ref email, ref phone) &&
                RetrieveSenderEmailTitle(userId, conn, ref smsTitle))
            {
                data = new UserPasswordRecoveryData(phone, password, email, smsTitle);
            }
            
            return data;
        }
        
        [WebMethod] // Overloaded method
        private UserPasswordRecoveryData GetPasswordRecoveryData(string userId, string email, string phone, string password, OdbcConnection conn)
        {
            string smsTitle = string.Empty;
            UserPasswordRecoveryData data = null;

            if (RetrieveSenderEmailTitle(userId, conn, ref smsTitle))
            {
                data = new UserPasswordRecoveryData(phone, password, email, smsTitle);
            }
            
            return data;
        }

        [WebMethod]
        private bool RetrievePasswordRecoveryData(string userId, OdbcConnection conn, ref string password, ref string email, ref string phone)
        {
            bool status = true;
            const string sProcedure = "sp_get_password_recovery_data";
            const string sParameter = "@id_num";
            DataTable dtData = new DataTable();
            try
            {
                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandText = string.Format("call {0} (?)", sProcedure);
                cmd.Parameters.AddWithValue(sParameter, userId);

                using (OdbcDataAdapter dataAdapter = new OdbcDataAdapter(cmd))
                {
                    dataAdapter.Fill(dtData);
                }
                cmd.Dispose();

                if (dtData == null)
                {
                    status = false;
                    throw new Exception(cmd.CommandText + " returned null");
                }

                if (dtData.Columns.Count == 3 && dtData.Rows.Count == 1)
                {
                    password = Convert.ToString(dtData.Rows[0][0]);
                    email = Convert.ToString(dtData.Rows[0][1]);
                    phone = Convert.ToString(dtData.Rows[0][2]);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    status = false;
                    Generic.WriteLog(ex.Message);
                }
            }
            finally
            {
                dtData.Dispose();
            }

            return status;
        }

        [WebMethod]
        private bool RetrieveSenderEmailTitle(string userId, OdbcConnection conn, ref string smsTitle)
        {
            bool status = true;
            const string sProcedure = "sp_get_senderSMS_email_title";
            const string sParameter = "title";
            OdbcCommand cmd = new OdbcCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.CommandText = string.Format("call {0} ()", sProcedure);

            try
            {
                OdbcDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    smsTitle = reader[sParameter].ToString().Trim();
                    if (!string.IsNullOrEmpty(smsTitle))
                    {
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
                    if (ex.Message.Contains(NOT_FOUND_ERROR))
                    {
                        smsTitle = SENDER_TITLE;
                    }
                    else
                    {
                        status = false;
                        Generic.WriteLog(ex.Message);
                    }
                }
            }
            finally
            {
                cmd.Dispose();
            }

            return status;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public UserPasswordRecoveryData GetNewUserPasswordData(string jsonUserID, string jsonEmail, string jsonPhone, string jsonAreaCode, string jsonPassword)
        {
            Generic.WriteLog(Generic.GetLogString(0, "PasswordRecovery", "GetNewUserPasswordData")); // ADDED ON 03/09/2013
            UserPasswordRecoveryData pwdRecoveryData = null;
            // OdbcConnection conn = GetOpenedConnection();
            OdbcConnection conn = Generic.GetOpenedConnection();
            if (conn == null)
            {
                return pwdRecoveryData;
            }

            pwdRecoveryData = GetPasswordRecoveryData(jsonUserID, jsonEmail, jsonAreaCode + jsonPhone, jsonPassword, conn);
            UpdatePasswordSenderRecoveryData(pwdRecoveryData, conn);  // ADDED ON 09/10/2013
            Generic.CloseConnection(conn);
            Generic.WriteLog(Generic.GetLogString(1, "PasswordRecovery", "GetNewUserPasswordData")); // ADDED ON 03/09/2013

            return pwdRecoveryData;
        }

        // ADDED ON 09/10/2013 : //////////////////////////////////////////////////////////////////
        [WebMethod]
        private void UpdatePasswordSenderRecoveryData(UserPasswordRecoveryData data, OdbcConnection conn)
        { 
            Generic.WriteLog(Generic.GetLogString(0, "PasswordRecovery", "GetSMSCredentials")); // ADDED ON 09/10/2013
            string  name = null,
                    pass = null,
                    cell = null,
                    url  = null;
            int? version = Generic.GetCurrentSybaseVersion(conn);   // added on 05/11/2013

            if (RetrieveSenderCredentials(conn, ref name, ref pass, ref cell, ref url))
            {
                data.SenderName         = name ?? data.SENDER_NAME;
                data.SenderPassword     = pass ?? data.SENDER_PASSWORD;
                data.SenderCellNumber   = cell ?? data.SENDER_CELL_NUMBER;
                //data.SenderUrl        = url  ?? data.SENDER_URL;  // COMMENTED ON 12/08/2019
                data.SenderUrl          = data.SENDER_URL;          // CORRECTED ON 12/08/2019
            }
            // added on 05/11/2013 : /////////////////////////////////////
            if (version <= Generic.PREV_VERSION)
            {
                data.SenderUrl = data.SenderUrl.Replace("_utf8", "");
            }
            // end added /////////////////////////////////////////////////
            Generic.WriteLog(Generic.GetLogString(1, "PasswordRecovery", "GetSMSCredentials")); // ADDED ON 09/10/2013
        }

        [WebMethod]
        private bool RetrieveSenderCredentials(OdbcConnection conn, ref string name, ref string pass, ref string cell, ref string url)
        {
            bool status = true;
            const string sProcedure = "sp_get_sms_credentials";
            DataTable dtData = new DataTable();
            try
            {
                OdbcCommand cmd = new OdbcCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandText = string.Format("call {0} ()", sProcedure);

                using (OdbcDataAdapter dataAdapter = new OdbcDataAdapter(cmd))
                {
                    dataAdapter.Fill(dtData);
                }
                cmd.Dispose();

                if (dtData == null)
                {
                    status = false;
                    throw new Exception(cmd.CommandText + " returned null");
                }

                if (dtData.Columns.Count == 4 && dtData.Rows.Count == 1)
                {
                    name    = Convert.ToString(dtData.Rows[0][0]);
                    pass    = Convert.ToString(dtData.Rows[0][1]);
                    url     = Convert.ToString(dtData.Rows[0][2]);
                    cell    = Convert.ToString(dtData.Rows[0][3]);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    status = false;
                    Generic.WriteLog(ex.Message);
                }
            }
            finally
            {
                dtData.Dispose();
            }

            return status;
        }

        // method added on 29/07/2020 for password recovery : ////////////
        private bool UpdateUserPassword(string jsonUserID, OdbcConnection conn)
        {
            bool status = true;
            string error    = null;
            string password = NewUserSignUp.GeneratePassword();
            const string sPocedure = "sp_update_user_password";
            
            using (OdbcCommand cmd = new OdbcCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection  = conn;
                cmd.CommandText = string.Format("call {0} (?, ?, ?)", sPocedure);
                cmd.Parameters.AddWithValue("@id_num", jsonUserID);
                cmd.Parameters.AddWithValue("@inet_password", password);
                cmd.Parameters.AddWithValue("@valid_password", DateTime.Now.Date);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OdbcException ex)
                {
                    error = ex.Message.ToString();
                }
                catch (Exception ex)
                {
                    error = ex.Message.ToString();
                }
                finally
                {
                    if (!string.IsNullOrEmpty(error))
                    {
                        Generic.WriteLog(PWD_UPDATE_ERROR + error);
                    }
                    Generic.CloseConnection(conn);
                }
            }
 
            return status;
        }
        // end method added on 29/07/2020 for password recovery //////////
        // END ADDED ////////////////////////////////////////////////////////////////////////////// 
    }
}

