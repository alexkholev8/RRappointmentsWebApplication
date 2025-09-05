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
    /// Summary description for TreatmentInstruction
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class TreatmentInstruction : System.Web.Services.WebService
    {
        private const string sProcedure = "sp_get_treat_instruction";
        private const string sParameter = "@al_oper_code";
        private const string sFieldName = "instruction";

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]    // ADDED 4 TEST
        public string GetTreatmentInstructions(int jsonOperCode)
        {
            Generic.WriteLog(Generic.GetLogString(0, "TreatmentInstruction", "GetTreatmentInstructions")); // ADDED ON 04/09/2013
            string instruction = RetrieveTreatmentInstructions(sProcedure, sParameter, sFieldName, jsonOperCode);
            Generic.WriteLog(Generic.GetLogString(1, "TreatmentInstruction", "GetTreatmentInstructions")); // ADDED ON 04/09/2013

            return instruction;
        }

        [WebMethod] // added on 11/10/2018
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public SiteInfo GetSiteInfo(int jsonBranchCode)
        {
            Generic.WriteLog(Generic.GetLogString(0, "SiteInfo", "GetSiteInfo"));
            SiteInfo siteInfo = RetrieveSiteInfo("sp_get_branch_data", "@branch_code", jsonBranchCode);
            Generic.WriteLog(Generic.GetLogString(1, "SiteInfo", "GetSiteInfo"));

            return siteInfo;
        }

        [WebMethod]
        private string RetrieveTreatmentInstructions(string sProcedure, string sParameter, string sFieldName, int nValue)
        {
            // OdbcConnection conn = GetOpenedConnection();
            OdbcConnection conn = Generic.GetOpenedConnection();
            if (conn == null)
                return null;
            
            string ret = null;
            OdbcCommand cmd = new OdbcCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.CommandText = string.Format("call {0} (?)", sProcedure);
            cmd.Parameters.AddWithValue(sParameter, nValue);

            try
            {
                OdbcDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret = reader[sFieldName].ToString().Trim();
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    Generic.WriteLog(ex.Message.ToString()); // ADDED ON 04/09/2013
                    ret = null;
                }
            }
            finally
            {
                Generic.CloseConnection(conn);
            }

            return ret;
        }

        [WebMethod]
        private SiteInfo RetrieveSiteInfo(string procedure, string parameter,int nValue)
        {
            OdbcConnection conn = Generic.GetOpenedConnection();
            if (conn == null)
                return null;

            SiteInfo siteInfo = new SiteInfo();
            OdbcCommand cmd = new OdbcCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.CommandText = string.Format("call {0} (?)", procedure);
            cmd.Parameters.AddWithValue(parameter, nValue);

            try
            {
                OdbcDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    siteInfo.SiteName   = (DBNull.Value.Equals(reader["branch_name"])) ? string.Empty : reader["branch_name"].ToString().Trim();
                    siteInfo.IsActive   = (DBNull.Value.Equals(reader["active"])) ? string.Empty : reader["active"].ToString().Trim();
                    siteInfo.Address    = (DBNull.Value.Equals(reader["address"])) ? string.Empty : reader["address"].ToString().Trim();
                    siteInfo.Phone1     = (DBNull.Value.Equals(reader["phone1"])) ? string.Empty : reader["phone1"].ToString().Trim();
                    siteInfo.Phone2     = (DBNull.Value.Equals(reader["phone2"])) ? string.Empty : reader["phone2"].ToString().Trim();
                    siteInfo.Mobile     = (DBNull.Value.Equals(reader["mobile"])) ? string.Empty : reader["mobile"].ToString().Trim();
                    siteInfo.FaxNum     = (DBNull.Value.Equals(reader["fax"])) ? string.Empty : reader["fax"].ToString().Trim();
                    siteInfo.Email      = (DBNull.Value.Equals(reader["email"])) ? string.Empty : reader["email"].ToString().Trim();
                    siteInfo.DocName    = (DBNull.Value.Equals(reader["docname"])) ? string.Empty : reader["docname"].ToString().Trim();
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    Generic.WriteLog(ex.Message.ToString()); // ADDED ON 04/09/2013
                    siteInfo = null;
                }
            }
            finally
            {
                Generic.CloseConnection(conn);
            }

            return siteInfo;
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
    }
}
