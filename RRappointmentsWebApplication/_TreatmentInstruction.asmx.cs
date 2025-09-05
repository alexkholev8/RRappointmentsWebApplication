using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Data.Odbc;
using System.Web.UI.WebControls;
/*
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
*/