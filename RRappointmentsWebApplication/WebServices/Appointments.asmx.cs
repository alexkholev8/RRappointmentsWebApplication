using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Data.Odbc;
using System.Web.UI.WebControls;
using RRappointmentsWebApplication.BuisnessLogicLayer;
using System.Xml.Serialization; // ADDED ON 15/08/2013
                         
namespace RRappointmentsWebApplication.WebServices
{
    /// <summary>
    /// Summary description for Appointments
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    [XmlInclude(typeof(ArrayList))] // IMPORTANT ADDITION ON 15/08/2013
    [XmlInclude(typeof(ListItem))]  // IMPORTANT ADDITION ON 15/08/2013
    public class Appointments : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]    // ADDED
        public ArrayList GetInsuranceList()
        {
            Generic.WriteLog(Generic.GetLogString(0, "Appointments", "GetInsuranceList"));  // ADDED ON 03/09/2013
            ArrayList arList = CreateListFromDB("sp_zimun_insur", "insur_code", "insur_name");
            Generic.WriteLog(Generic.GetLogString(1, "Appointments", "GetInsuranceList"));  // ADDED ON 03/09/2013

            return arList;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]    // ADDED
        public ArrayList GetOperList()
        {
            Generic.WriteLog(Generic.GetLogString(0, "Appointments", "GetOperList"));   // ADDED ON 03/09/2013
            ArrayList arList = CreateListFromDB("sp_zimun_get_oper", "oper_code", "opername");
            Generic.WriteLog(Generic.GetLogString(1, "Appointments", "GetOperList"));   // ADDED ON 03/09/2013

            return arList;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]    // ADDED
        public ArrayList GetTreatmentGroupList()
        {
            Generic.WriteLog(Generic.GetLogString(0, "Appointments", "GetTreatmentGroupList")); // ADDED ON 03/09/2013
            //ArrayList arList = CreateListFromDB("sp_zimun_get_oper", "oper_code", /*"groups_name"*/"groups_num"); // commented on 14/07/2019
            ArrayList arList = CreateListFromDB("sp_zimun_get_oper", "groups_num", "groups_name");  // corrected on 14/07/2019
            Generic.WriteLog(Generic.GetLogString(1, "Appointments", "GetTreatmentGroupList")); // ADDED ON 03/09/2013

            return arList;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]    // ADDED
        public ArrayList GetSiteList(int jsonValue)
        {
            Generic.WriteLog(Generic.GetLogString(0, "Appointments", "GetSiteList"));   // ADDED ON 03/09/2013
            ArrayList arList = CreateListFromDB("sp_zimun_sites", "branch_code", "site_name", jsonValue);
            Generic.WriteLog(Generic.GetLogString(1, "Appointments", "GetSiteList"));   // ADDED ON 03/09/2013

            return arList;
        }

        [WebMethod]
        private ArrayList CreateListFromDB(string sProc, string sKey, string sValue, int? paramValue = null)
        {
            string error = string.Empty;
            string itemValue;  // added on 27/11/2019
            OdbcConnection conn = Generic.GetOpenedConnection();
            if (conn == null)
                return null;

            OdbcCommand cmd = new OdbcCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.CommandText = string.Format("call {0} ({1})", sProc, (paramValue == null) ? "" : "?");
            if (paramValue != null)
            {
                OdbcParameter param = GetParameterODBC("@al_oper_code", paramValue);
                cmd.Parameters.Add(param);
            }
            ArrayList arList = new ArrayList();

            try
            {
                OdbcDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // added on 27/11/2019 : ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (sValue == "opername")
                    {
                        itemValue = (reader["inet_name"] != DBNull.Value && !string.IsNullOrEmpty(reader["inet_name"].ToString().Trim())) ?
                            reader["inet_name"].ToString().Trim() : reader[sValue].ToString().Trim();
                    }
                    else
                    {
                        itemValue = reader[sValue].ToString().Trim();
                    }
                    // end added on 27/11/2019 //////////////////////////////////////////////////////////////////////////////////////////////////////
                    ListItem item = new ListItem(/*reader[sValue].ToString().Trim()*/itemValue, reader[sKey].ToString().Trim());    // corrected on 27/11/2019
                    KeyValuePair<string, string> Pair = new KeyValuePair<string, string>(item.Value, item.Text);    // ADDED FOR TEST
                    if (!arList.Contains(item/*.Text*/))    // corrected 
                        arList.Add(item);
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    error = ex.Message.ToString();
                    Generic.WriteLog(error);    // ADDED ON 03/09/2013
                }
            }
            finally
            {
                Generic.CloseConnection(conn);
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }

            return (string.IsNullOrEmpty(error)) ? arList : null;
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
        private OdbcParameter GetParameterODBC(string name, int? value)
        {
            OdbcParameter param = new OdbcParameter();
            param.ParameterName = name;
            param.Direction = ParameterDirection.Input;
            param.OdbcType = OdbcType.Int;
            param.Value = value;

            return param;
        }

        // added on 07/07/2013
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]    // ADDED
        public OldAppointment[] GetOldAppointments(string jsonId_Num, string jsonFileNum)
        {
            Generic.WriteLog(Generic.GetLogString(0, "Appointments", "GetOldAppointments"));    // ADDED ON 03/09/2013
            if (string.IsNullOrEmpty(jsonFileNum))
            {
                jsonFileNum = GetFileNum("sp_login_get_file_num", "file_num", "@id_num", jsonId_Num);
                if (string.IsNullOrEmpty(jsonFileNum))
                    return null;
                // (Session["session"] as SessionInfo).FileNum = jsonFileNum;
            }

            OldAppointment[] oldAppointments = RetrieveOldAppointmentsList("sp_get_old_appointments", "@al_cust_num", jsonFileNum);
            Generic.WriteLog(Generic.GetLogString(1, "Appointments", "GetOldAppointments"));    // ADDED ON 03/09/2013

            return oldAppointments;
        }

        [WebMethod]
        private string GetFileNum(string sProc, string sFieldValue, string sParamName, string sParamValue)
        {
            // OdbcConnection conn = GetOpenedConnection();
            OdbcConnection conn = Generic.GetOpenedConnection();
            if (conn == null)
                return null;

            string fileNum = string.Empty;
            OdbcCommand cmd = new OdbcCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.CommandText = string.Format("call {0} (?)", sProc);
            cmd.Parameters.AddWithValue(sParamName, sParamValue);

            try
            {
                OdbcDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fileNum = reader[sFieldValue].ToString().Trim();
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    Generic.WriteLog(ex.Message.ToString());    // ADDED ON 03/09/2013
                    fileNum = null;
                }
            }
            finally
            {
                Generic.CloseConnection(conn);
            }

            return fileNum;
        }

        [WebMethod]
        private OldAppointment[] RetrieveOldAppointmentsList(string sProc, string sParamName, string sParamValue)
        {
            // OdbcConnection conn = GetOpenedConnection();
            OdbcConnection conn = Generic.GetOpenedConnection();
            if (conn == null)
                return null;

            OdbcCommand cmd = new OdbcCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.CommandText = string.Format("call {0} (?)", sProc);
            cmd.Parameters.AddWithValue(sParamName, int.Parse(sParamValue));
            string sError = string.Empty;

            OldAppointments oldAppointments = new OldAppointments();
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

                if (dtData.Rows.Count > 0)  // && dtData.Columns.Count == 6  - optional 
                {
                    foreach (DataRow dtRow in dtData.Rows)
                    {
                        DateTime date = Convert.ToDateTime(dtRow["ddate"]);
                        DateTime time = Convert.ToDateTime(dtRow["dtime"].ToString());
                        OldAppointment oldAppointment = new OldAppointment
                        {
                            AppointmentOperName = dtRow["oper_name"].ToString(),
                            AppointmentLocation = dtRow["c_branch"].ToString(),
                            AppointmentDate     = date.ToString("dd/MM/yyyy"),
                            AppointmentTime     = time.ToShortTimeString(),
                            AppointmentDay      = date.ToString("dddd"),
                            LineCode            = dtRow["line_code"].ToString() // ADDED ON 30/07/2019
                        };
                        oldAppointments.Add(oldAppointment);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    sError = ex.Message.ToString();
                    Generic.WriteLog(sError);   // ADDED ON 03/09/2013
                }
            }
            finally
            {
                dtData.Dispose();
                cmd.Dispose();
                Generic.CloseConnection(conn);
            }

            return (string.IsNullOrEmpty(sError)) ? oldAppointments.ToArray() : null;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]    // ADDED
        public AvailableAppointment[] GetAvailableAppointments(int jsonOperCode, int jsonSiteCode, string jsonFromDate, string jsonToDate, int jsonDay1, int jsonDay2, int jsonDay3, int jsonDay4, int jsonDay5, int jsonDay6, int jsonDay7)
        {
            Generic.WriteLog(Generic.GetLogString(0, "Appointments", "GetAvailableAppointments"));  // ADDED ON 03/09/2013
            DateTime startDateTime = Convert.ToDateTime(jsonFromDate);
            DateTime endDateTime = Convert.ToDateTime(jsonToDate);
            jsonFromDate = startDateTime.ToString("yyyyMMdd");
            jsonToDate = endDateTime.ToString("yyyyMMdd");
            int[] dowArray = { jsonDay1, jsonDay2, jsonDay3, jsonDay4, jsonDay5, jsonDay6, jsonDay7 };

            List<AvailableAppointment> availableAppointments = RetrieveAvailableAppointmentsList("sp_zimun_free_appointments", jsonFromDate, jsonToDate, jsonOperCode, jsonSiteCode, dowArray);
            Generic.WriteLog(Generic.GetLogString(1, "Appointments", "GetAvailableAppointments"));  // ADDED ON 03/09/2013

            return availableAppointments.ToArray();
        }

        [WebMethod]
        private List<AvailableAppointment> RetrieveAvailableAppointmentsList(string sProc, string sFromDate, string sToDate, int operCode, int branchCode, int[] dowArray)
        {
            // OdbcConnection conn = GetOpenedConnection();
            OdbcConnection conn = Generic.GetOpenedConnection();
            if (conn == null)
                return null;

            OdbcCommand cmd = new OdbcCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.CommandText = string.Format("call {0} (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", sProc);
            cmd.Parameters.AddWithValue("@to_date", sToDate);
            cmd.Parameters.AddWithValue("@from_date", sFromDate);
            for (int index = 0; index < dowArray.Length; index++)
            {
                string sParam = string.Format("@D{0}", index + 1);
                cmd.Parameters.AddWithValue(sParam, dowArray[index]);
            }
            cmd.Parameters.AddWithValue("@oper_code", operCode);
            cmd.Parameters.AddWithValue("@branch_code", branchCode);

            string sError = string.Empty;
            List<AvailableAppointment> availableAppointments = new List<AvailableAppointment>();
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

                if (dtData.Rows.Count > 0)  // && dtData.Columns.Count == 6  - optional 
                {
                    foreach (DataRow dtRow in dtData.Rows)
                    {
                        DateTime date = Convert.ToDateTime(dtRow["ddate"].ToString());
                        DateTime time = Convert.ToDateTime(dtRow["dtime"].ToString());
                        AvailableAppointment availableAppointment = new AvailableAppointment
                        {
                            Dline = dtRow["dline"].ToString(),
                            Diarynum = dtRow["diarynum"].ToString(),
                            Ddate = date.ToString("dd/MM/yyyy"),
                            Dday = date.ToString("dddd"),
                            Dtime = time.ToShortTimeString(),
                            Diary_name = dtRow["diary_name"].ToString()
                        };
                        availableAppointments.Add(availableAppointment);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    sError = ex.Message.ToString();
                    Generic.WriteLog(sError);   // ADDED ON 03/09/2013
                }
            }
            finally
            {
                dtData.Dispose();
                cmd.Dispose();
                Generic.CloseConnection(conn);
            }

            return (string.IsNullOrEmpty(sError)) ? availableAppointments : null;
        }

        // ADDED ON 11/11/2013 : //////////////////////////////////////////////////////////////////
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public OldAppointment[] GetOldAppointmentsWithInstructions(string jsonId_Num, string jsonFileNum)
        {
            Generic.WriteLog(Generic.GetLogString(0, "Appointments", "GetOldAppointmentsWithInstructions"));
            if (string.IsNullOrEmpty(jsonFileNum))
            {
                jsonFileNum = GetFileNum("sp_login_get_file_num", "file_num", "@id_num", jsonId_Num);
                if (string.IsNullOrEmpty(jsonFileNum))
                    return null;
            }

            OldAppointment[] oldAppointments = RetrieveExtendedOldAppointmentsList("sp_get_old_appointments_with_instructions", "@al_cust_num", jsonFileNum);
            Generic.WriteLog(Generic.GetLogString(1, "Appointments", "GetOldAppointmentsWithInstructions"));

            return oldAppointments;
        }

        [WebMethod]
        private OldAppointment[] RetrieveExtendedOldAppointmentsList(string sProc, string sParamName, string sParamValue)
        {
            // OdbcConnection conn = GetOpenedConnection();
            OdbcConnection conn = Generic.GetOpenedConnection();
            if (conn == null)
                return null;

            OdbcCommand cmd = new OdbcCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.CommandText = string.Format("call {0} (?)", sProc);
            cmd.Parameters.AddWithValue(sParamName, int.Parse(sParamValue));
            string sError = string.Empty;

            OldAppointments oldAppointments = new OldAppointments();
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

                if (dtData.Rows.Count > 0)  // && dtData.Columns.Count == 6  - optional 
                {
                    foreach (DataRow dtRow in dtData.Rows)
                    {
                        DateTime date = Convert.ToDateTime(dtRow["ddate"]);
                        DateTime time = Convert.ToDateTime(dtRow["dtime"].ToString());
                        OldAppointment oldAppointment = new OldAppointment
                        {
                            AppointmentOperName = dtRow["oper_name"].ToString(),
                            AppointmentLocation = dtRow["c_branch"].ToString(),
                            AppointmentDate     = date.ToString("dd/MM/yyyy"),
                            AppointmentTime     = time.ToShortTimeString(),
                            AppointmentDay      = date.ToString("dddd"),
                            Instruction         = dtRow["instruction"].ToString(),
                            LineCode            = dtRow["line_code"].ToString() // ADDED ON 30/07/2019
                        };
                        oldAppointments.Add(oldAppointment);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    sError = ex.Message.ToString();
                    Generic.WriteLog(sError);   // ADDED ON 03/09/2013
                }
            }
            finally
            {
                dtData.Dispose();
                cmd.Dispose();
                Generic.CloseConnection(conn);
            }

            return (string.IsNullOrEmpty(sError)) ? oldAppointments.ToArray() : null;
        }
        // END ADDED ON 11/11/2013 ////////////////////////////////////////////////////////////////
        
        // ADDED ON 14/07/2019 : //////////////////////////////////////////////////////////////////
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetGroupOperLinkList()
        {
            Generic.WriteLog(Generic.GetLogString(0, "Appointments", "GetOperList"));
            ArrayList arList = CreateListFromDB("sp_zimun_get_oper", "groups_num", "oper_code");
            Generic.WriteLog(Generic.GetLogString(1, "Appointments", "GetOperList"));

            return arList;
        }
        // END ADDED ON 14/07/2019 ////////////////////////////////////////////////////////////////

        // ADDED ON 29/07/2019: ///////////////////////////////////////////////////////////////////
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public AvailableAppointment[] GetFilteredAvailableAppointments(int jsonOperCode, int jsonSiteCode, string jsonFromDate, string jsonToDate, int jsonDay1, int jsonDay2, int jsonDay3, int jsonDay4, int jsonDay5, int jsonDay6, int jsonDay7)
        {
            Generic.WriteLog(Generic.GetLogString(0, "Appointments", "GetFilteredAvailableAppointments"));
            DateTime startDateTime = Convert.ToDateTime(jsonFromDate);
            DateTime endDateTime = Convert.ToDateTime(jsonToDate);
            jsonFromDate = startDateTime.ToString("yyyyMMdd");
            jsonToDate = endDateTime.ToString("yyyyMMdd");
            int[] dowArray = { jsonDay1, jsonDay2, jsonDay3, jsonDay4, jsonDay5, jsonDay6, jsonDay7 };

            List<AvailableAppointment> availableAppointments = RetrieveAvailableAppointmentsList("sp_zimun_free_appointments_filtered", jsonFromDate, jsonToDate, jsonOperCode, jsonSiteCode, dowArray);
            Generic.WriteLog(Generic.GetLogString(1, "Appointments", "GetFilteredAvailableAppointments"));

            return availableAppointments.ToArray();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SetAppointmentConfirmationStatus(int jsonLineCode, int jsonConfirmationStatus)
        {
            string status = null;
            Generic.WriteLog(Generic.GetLogString(0, "OldAppointments", "DeleteAssignedAppointment"));
            OdbcConnection conn = Generic.GetOpenedConnection();
            if (conn == null)
                return status;
            if (conn.State != ConnectionState.Open)
            {
                conn = null;
                return status;
            }

            string sProc = "sp_set_appointment_confirmation_status";
            status = OnSetAppointmentConfirmationStatus(sProc, jsonLineCode, jsonConfirmationStatus, conn);
            Generic.CloseConnection(conn);
            Generic.WriteLog(Generic.GetLogString(1, "OldAppointments", "DeleteAssignedAppointment"));

            return status;
        }

        [WebMethod]
        private string OnSetAppointmentConfirmationStatus(string proc, int lineCode, int confirmationStatus, OdbcConnection conn)
        {
            string sError = null, status = null;
            OdbcCommand cmd = new OdbcCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.CommandText = string.Format("call {0} (?, ?)", proc);
            cmd.Parameters.AddWithValue("@rowId", lineCode);
            cmd.Parameters.AddWithValue("@confirmationStatus", confirmationStatus);

            try
            {
                status = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message.ToString()))
                {
                    sError = ex.Message.ToString();
                    Generic.WriteLog(sError);
                }
            }
            finally
            {
                cmd.Dispose();
                if (!string.IsNullOrEmpty(sError))
                    status = sError;
            }

            return status;
        }
        // END ADDED ON 28/07/2019 ////////////////////////////////////////////////////////////////

        // ADDED ON 27-28/11/2019 /////////////////////////////////////////////////////////////////
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ArrayList GetGroupMessageList()
        {
            Generic.WriteLog(Generic.GetLogString(0, "Appointments", "GetGroupMessageList"));
            ArrayList arList = CreateListFromDB("sp_zimun_get_oper", "groups_num", "inet_message");
            Generic.WriteLog(Generic.GetLogString(1, "Appointments", "GetGroupMessageList"));

            return arList;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public int AddTime(int jsonDiaryNum, string jsonDateTime)
        {
            int diarynum = 0;
            OdbcConnection conn = Generic.GetOpenedConnection();
            if (conn == null)
                return diarynum;
            if (conn.State != ConnectionState.Open)
            {
                conn = null;
                return diarynum;
            }

            string sProc = "sp_zimun_addtime";
            Generic.WriteLog(Generic.GetLogString(0, "Appointments", "AddTime"));
            diarynum = OnAddTime(sProc, jsonDiaryNum, jsonDateTime, conn);
            Generic.CloseConnection(conn);
            Generic.WriteLog(Generic.GetLogString(1, "Appointments", "AddTime"));

            return diarynum;
        }

        [WebMethod]
        private int OnAddTime(string proc, int diarynum, string date_time, OdbcConnection conn)
        { 
            int dline   = 0;
            bool status = true;
            string sError = null;

            using (OdbcCommand cmd = new OdbcCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandText = string.Format("call {0} (?, ?)", proc);
                cmd.Parameters.AddWithValue("@diarynum", diarynum);
                cmd.Parameters.AddWithValue("@date_time", Convert.ToDateTime(date_time));

                try
                {
                    status = int.TryParse(cmd.ExecuteScalar().ToString(), out dline);
                }
                catch (OdbcException ex)
                {
                    if (!string.IsNullOrEmpty(ex.Message.ToString()))
                    {
                        sError = ex.Message.ToString();
                    }
                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(ex.Message.ToString()))
                    {
                        sError = ex.Message.ToString();
                    }
                }
                finally
                {
                    if (!string.IsNullOrEmpty(sError))
                        Generic.WriteLog(sError);
                }
            }

            return dline;
        }
        // END ADDED ON 27-28/11/2019 /////////////////////////////////////////////////////////////
    }
}
