using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.Odbc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;    // ADDED

namespace RRappointmentsWebApplication.WebServices
{
    /// <summary>
    /// Summary description for Generic
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Generic : System.Web.Services.WebService
    {
        private const string CONNECTION_STRING = @"DSN=Medical;Automatic_Timestamp=ON;SuppressWarnings=YES";
        private const string FILENAME_FORMAT = @"~\App_Data\Log\local_webservice_({0}).txt";   // @"\\dc1\E\diskf\Zimon\Log\local_webservice_({0}).txt";                                             
        private const string LOGINROW_FORMAT = "{0} Web Service \"{1}\". Method: \"{2}\"";
        // ADDED ON 20/10/2013 : //////////////////////////////////////////////////////////////////
        private const string VERSION_CHECK_QRY = "select @@version from dummy";
        private const string AUTHENTICATION_QRY = "SET TEMPORARY OPTION CONNECTION_AUTHENTICATION='Company=RR Systems;Application=Medical;Signature=000fa55157edb8e14d818eb4fe3db41447146f1571g39f4655dbd367a13689ceda04c5a1af8771cdd54'";
        private const string AUTHENTICATION_ERR = "Failed during Authentication";
        private const int LAST_VERSION = 11;
        protected internal const int PREV_VERSION = 9;
        private const char POINT = '.';
        private const char BLANK = ' ';
        private readonly static string [] COMPLETION_STATUS = { "Enter", "Exit" };
        protected internal readonly static string CUSTCODE_PREFIX = "8000";      // ADDED ON 15/01/2014
        // ADDED ON 22/01/2014 : //////////////////////////////////////////////////////////////////
        protected internal readonly static string[] LOCKING_STATUS = { "Lock", "Unlock" };
        protected internal readonly static string STATUS_SUCCESS = "OK";
        protected internal readonly static string STATUS_FAILURE = "ERROR";
        protected internal readonly static string DB_LOCK_ERROR = "{0} Error: {1}";
        // END ADDED //////////////////////////////////////////////////////////////////////////////
        
        [WebMethod]
        protected internal static OdbcConnection GetOpenedConnection()
        {
            OdbcConnection conn = null;
            try
            {
                conn = new OdbcConnection(CONNECTION_STRING);
                if (conn != null)
                {
                    conn.Open();
                    // ADDED ON 20/10/2013 : //////////////
                    if (conn.State == ConnectionState.Open)
                    {
                        Authentication(conn);
                    }
                    // END ADDED //////////////////////////
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    WriteLog(ex.Message);
                }
                CloseConnection(conn);
            }

            return conn;
        }

        [WebMethod]
        protected internal static void CloseConnection(OdbcConnection conn)
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
                conn = null;
            }
        }

        [WebMethod]
        protected internal static bool IsUserAlreadyExists(string userId, OdbcConnection conn = null)
        {
            bool isExist = false;
            if (conn == null)
            {
                if ((conn = GetOpenedConnection()) == null)
                    return isExist;
            }

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
                    WriteLog(ex.Message);
                }
            }
            finally
            {
                cmd.Dispose();
            }

            return isExist;
        }

        protected internal static string ConvertToUTF8(string unicodeString)
        {
            Encoding win = Encoding.GetEncoding(1255);
            Encoding utf8 = Encoding.UTF8;

            byte[] winBytes = win.GetBytes(unicodeString);
            byte[] utfBytes = Encoding.Convert(win, utf8, winBytes);
            //return utf8.GetString(utfBytes);
            string ret = utf8.GetString(utfBytes);

            return HttpUtility.UrlEncode(ret);
        }
        
        protected internal static void WriteLog(string status)
        {
            string error = null;
            StreamWriter writer = null;
            string fileName = string.Format(FILENAME_FORMAT, DateTime.Now.ToShortDateString().Replace('/', '.'));
            fileName = HttpContext.Current.Server.MapPath(fileName);    // ADDED ON 09/10/2013
            // ADDED ON 14/10/2013 : ////////////////////////////////////////////////////
            string dirName = fileName.Remove(fileName.IndexOf(@"\local_webservice_"));
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
            // END ADDED ////////////////////////////////////////////////////////////////
            try
            {
                if (writer == null)
                    writer = new StreamWriter(fileName, true);
                writer.WriteLine(DateTime.Now.ToShortTimeString() + " - " + status + ";");
            }
            catch (DirectoryNotFoundException ex)
            {
                error = ex.Message.ToString();
            }
            catch (FileNotFoundException ex)
            {
                error = ex.Message.ToString();
            }
            catch (IOException ex)
            {
                error = ex.Message.ToString();
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                    writer = null;
                }
                if (string.IsNullOrEmpty(error))
                {
                    Console.WriteLine(error);
                }
            }
        }

        protected internal static string GetLogString(int complFlag, string webServiceName, string webMethodName)
        {
            return string.Format(LOGINROW_FORMAT, COMPLETION_STATUS[complFlag], webServiceName, webMethodName);
        }

        // METHOS ADDED ON 20/10/2013 : ///////////////////////////////////////////////////////////
        private static string CheckVersion(OdbcConnection conn)
        {
            string version = null;
            OdbcCommand command = new OdbcCommand(VERSION_CHECK_QRY, conn);
            if (command == null)
                return version;

            try
            {
                version = command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    WriteLog(ex.Message.ToString());
                }
            }
            finally
            {
                command.Dispose();
            }

            return version;
        }

        private static void Authentication(OdbcConnection conn)
        {
            try
            {
                //string version = CheckVersion(conn);
                //if (string.IsNullOrEmpty(version))
                //    return;
                int? plainVersion = GetCurrentSybaseVersion(conn);
                if (plainVersion == null)
                    return;

                //if (Convert.ToInt32(version.Replace(POINT, BLANK).Substring(0, 2)) >= LAST_VERSION) // COMMENTED ON 05/11/2013
                if (plainVersion >= LAST_VERSION)   // CORRECTED ON 05/11/2013
                {
                    OdbcCommand cmd = new OdbcCommand(AUTHENTICATION_QRY, conn);
                    int ret = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    WriteLog(ex.Message.ToString());
                }
                throw new Exception(AUTHENTICATION_ERR);
            }
        }

        // ADDED ON 05/11/2013 : ////////////////////////////////////////////////////////////////// 
        protected internal static int? GetCurrentSybaseVersion(OdbcConnection conn)
        {
            string version = CheckVersion(conn);
            if (string.IsNullOrEmpty(version))
                return null;

            return Convert.ToInt32(version.Replace(POINT, BLANK).Substring(0, 2));
        }
        // END METHODS ADDED //////////////////////////////////////////////////////////////////////
    }
}
