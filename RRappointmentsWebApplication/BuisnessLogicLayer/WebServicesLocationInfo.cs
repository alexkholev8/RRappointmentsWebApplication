using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;    // added
using System.Text;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public class WebServicesLocationInfo
    {
        private const string DEFAULT_DIR = "WebServices/";
        private const string FALSE = "false";
        private const string COLON = ":";
        
        // added on 22/07/2013
        public static readonly string WEBSRV_ISREMOTE = "IsRemote";     // "WebServices_IsRemote";
        public static readonly string WEBSRV_DIRECTORY = "Directory";   // "WebServices_Directory";
        public static readonly string WEBSRV_IPADDRESS = "IpAddress";   // "WebServices_IpAddress";
        public static readonly string WEBSRV_PORT = "Port";             // "WebServices_Port";
        public static readonly string WBSRV_SYNCMODE = "SyncMode";      // "WebServices_SyncMode"
        // end added

        private static string port;
        private static string ipAddress;
        private static string directory;
        private static string isRemote;
        private static string syncMode;

        //private WebServicesLocationInfo()
        //{
        //    port = null;
        //    ipAddress = null;
        //    directory = DEFAULT_DIR;
        //    isRemote = false;
        //}

        static WebServicesLocationInfo()
        {
            port = string.Empty;
            ipAddress = string.Empty;
            directory = DEFAULT_DIR;
            isRemote = FALSE;
            syncMode = "0";
        }

        public static string Port
        {
            get { return GetPort(); }
            set { port = value.Replace(COLON, ""); }
        }

        public static string IPAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        public static string Directory
        {
            get
            {
                return GetDirectory();
            }

            set
            {
                directory = value;    
            }
        }

        public static string IsRemote
        {
            get { return isRemote; }
            set { isRemote = value; }
        }

        public static string SyncMode
        {
            get { return syncMode; }
            set { syncMode = value; }
        }

        private static string GetDirectory()
        {
            if (string.IsNullOrEmpty(directory))
            {
                directory = DEFAULT_DIR;
            }
            else
            {
                if (directory.LastIndexOf('/') != directory.Length - 1)
                    directory += '/';
            }

            return directory;
        }

        private static string GetPort()
        {
            if (!string.IsNullOrEmpty(port))
            {
                if (port.IndexOf(COLON) != 0 && port.LastIndexOf(COLON) != 0)
                    port = COLON + port;
            }

            return port;
        }

        public static string GetClientScriptValueString(string key, string val)
        {
            string script = string.Format("var {0} = '{1}';", 
                key.Replace(key[0], Char.ToLower(key[0])), val);

            return script;
        }

    }

}