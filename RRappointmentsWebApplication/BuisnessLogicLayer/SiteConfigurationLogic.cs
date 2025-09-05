using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Microsoft.Web.Administration;   // commented on 30/05/2013
// added on 14/01/2015 : /////////
using System.Web.Configuration; 
using System.Configuration;
using System.Windows.Forms;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public static class SiteConfigurationLogic
    {
        private const string CONFIG_ERROR_MESSAGE = "Failed to update file Web.config:\r\n:{0}";
        private const string DEFAULT_STATE = "False";   // ADDED ON 28/07/2019

        #region Private Members 
        private static string enableTreatmentGroups;
        private static string appointmentLimit;
        private static string webServiceIP;
        private static string webServicePort;
        // corrections and additions since 18/07/2013:
        private static string webServiceLocation;
        private static string webServiceDirectory;
        // added on 08/08/2013: //////////////////////
        private static string webServiceSyncMode;
        // added on 01/10/2013: //////////////////////
        private static string useSavedEmailSettings;
        // added on 14/01/2014 : /////////////////////
        private static string wcfEndpointAddress;
        // added on 26/04/2015 : /////////////////////
        private static string useWcfDiariesCounterService;
        // added on 28/07/2019 : /////////////////////
        private static string useDlineFiltering;
        // added on 28/07/2020 : /////////////////////
        private static string passwordDaysCount;
        private static string passwordUpdateUse;
        // end corrections and additions /////////////
        #endregion

        //open configuration file and load settings
        internal static void LoadConfigurations()
        {
            enableTreatmentGroups = ConfigurationManager.AppSettings["EnableTreatmentGroups"];
            appointmentLimit = ConfigurationManager.AppSettings["AppointmentLimit"];
            useDlineFiltering = ConfigurationManager.AppSettings["UseDlineFiltering"] ?? DEFAULT_STATE;  // ADDED ON 28/07/2019
            webServiceIP = ConfigurationManager.AppSettings["WebServiceIP"];
            webServicePort = ConfigurationManager.AppSettings["WebServicePort"];
            // corrections and additions on 18/08/2013 : /////////////////////////////////////
            webServiceDirectory = ConfigurationManager.AppSettings["WebServiceDirectory"];
            webServiceLocation  = ConfigurationManager.AppSettings["WebServiceLocation"];
            // added on 08/08/2013 : /////////////////////////////////////////////////////////
            webServiceSyncMode = ConfigurationManager.AppSettings["WebServiceSyncMode"];
            // added on 01/10/2013 : /////////////////////////////////////////////////////////
            useSavedEmailSettings = ConfigurationManager.AppSettings["UseSavedEmailSettings"];
            // added on 26/04/2015
            useWcfDiariesCounterService = ConfigurationManager.AppSettings["UseWcfDiariesCounterService"];
            // added on 28/07/2020 : /////////////////////////////////////////////////////////
            passwordUpdateUse = ConfigurationManager.AppSettings["PasswordUpdateUse"] ?? DEFAULT_STATE;
            passwordDaysCount = ConfigurationManager.AppSettings["PasswordDaysCount"] ?? string.Empty;
            // end additions and corrections /////////////////////////////////////////////////
        }

        #region Public Properties

        public static string EnableTreatmentGroups
        {
            get { return enableTreatmentGroups; }
            set { enableTreatmentGroups = value; }
        }

        public static string AppointmentLimit
        {
            get { return appointmentLimit; }
            set { appointmentLimit = value; }
        }

        public static string WebServiceIp
        {
            get { return webServiceIP; }
            set { webServiceIP = value; }
        }

        public static string WebServicePort
        {
            get { return webServicePort; }
            set { webServicePort = value; }
        }

        public static string WebServiceLocation
        {
            get { return webServiceLocation; }
            set { webServiceLocation = value; }
        }

        public static string WebServiceDirectory
        {
            get { return webServiceDirectory; }
            set { webServiceDirectory = value; }
        }

        // added on 08/08/2013 : /////////////////////
        public static string WebServiceSyncMode
        {
            get { return webServiceSyncMode; }
            set { webServiceSyncMode = value; }
        }

        // added on 01/10/2013 : /////////////////////
        public static string UseSavedEmailSettings
        {
            get { return useSavedEmailSettings; }
            set { useSavedEmailSettings = value; }
        }

        // added on 12/01/2014 : /////////////////////
        public static string WcfEndpointAddress
        {
            get { return wcfEndpointAddress; }
            set { wcfEndpointAddress = value; }
        }

        // added on 26/04/2015 : /////////////////////
        public static string UseWcfDiariesCounterService
        {
            get { return useWcfDiariesCounterService; }
            set { useWcfDiariesCounterService = value; }
        }

        // added on 28/07/2019 : /////////////////////
        public static string UseDlineFiltering
        {
            get { return useDlineFiltering; }
            set { useDlineFiltering = value; }
        }

        // added on 28/07/2020 : /////////////////////
        public static string PasswordDaysCount
        {
            get { return passwordDaysCount; }
            set { passwordDaysCount = value; }
        }

        public static string PasswordUpdateUse
        {
            get { return passwordUpdateUse; }
            set { passwordUpdateUse = value; }
        }
        // end additions /////////////////////////////
        #endregion

        internal static void SaveConfigurations()
        {
            ConfigurationManager.AppSettings["EnableTreatmentGroups"] = enableTreatmentGroups;
            ConfigurationManager.AppSettings["AppointmentLimit"] = appointmentLimit;
            ConfigurationManager.AppSettings["UseDlineFiltering"] = useDlineFiltering;  // ADDED ON 28/07/2019
            ConfigurationManager.AppSettings["WebServiceIP"] = webServiceIP;
            ConfigurationManager.AppSettings["WebServicePort"] = webServicePort;
            // corrections and additions on 18/08/2013 : /////////////////////////////////////
            ConfigurationManager.AppSettings["WebServiceDirectory"] = webServiceDirectory;
            ConfigurationManager.AppSettings["WebServiceLocation"] = webServiceLocation;
            // added on 08/08/2013 : /////////////////////////////////////////////////////////
            ConfigurationManager.AppSettings["WebServiceSyncMode"] = webServiceSyncMode;
            // added on 01/10/2013 : /////////////////////////////////////////////////////////
            ConfigurationManager.AppSettings["UseSavedEmailSettings"] = useSavedEmailSettings;
            // added on 26/04/2015 : /////////////////////////////////////////////////////////
            ConfigurationManager.AppSettings["UseWcfDiariesCounterService"] = useWcfDiariesCounterService;
            // added on 28/07/2020 : /////////////////////////////////////////////////////////
            ConfigurationManager.AppSettings["PasswordUpdateUse"] = passwordUpdateUse ?? DEFAULT_STATE;
            ConfigurationManager.AppSettings["PasswordDaysCount"] = passwordDaysCount ?? string.Empty;
            // added on 14/01/2015 : /////////////////////////////////////////////////////////
            // string configPath = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            string error = null;
            try
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
                config.AppSettings.Settings["EnableTreatmentGroups"].Value = enableTreatmentGroups;
                config.AppSettings.Settings["AppointmentLimit"].Value = appointmentLimit;
                config.AppSettings.Settings["UseDlineFiltering"].Value = useDlineFiltering;
                config.AppSettings.Settings["WebServiceIP"].Value = webServiceIP;
                config.AppSettings.Settings["WebServicePort"].Value = webServicePort;
                config.AppSettings.Settings["WebServiceLocation"].Value = webServiceLocation;
                config.AppSettings.Settings["WebServiceDirectory"].Value = webServiceDirectory;
                config.AppSettings.Settings["WebServiceSyncMode"].Value = webServiceSyncMode;
                config.AppSettings.Settings["UseSavedEmailSettings"].Value = useSavedEmailSettings;
                config.AppSettings.Settings["UseWcfDiariesCounterService"].Value = useWcfDiariesCounterService; // added on 26/04/2015
                config.AppSettings.Settings["PasswordUpdateUse"].Value = passwordUpdateUse ?? DEFAULT_STATE;    // added on 28/07/2020
                config.AppSettings.Settings["PasswordDaysCount"].Value = passwordDaysCount ?? string.Empty;     // added on 28/07/2020
                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (ConfigurationErrorsException ex)
            {
                error = ex.Message;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                if (!string.IsNullOrEmpty(error))
                {
                    // MessageBox.Show(string.Format(CONFIG_ERROR_MESSAGE, error));
                    Console.WriteLine(string.Format(CONFIG_ERROR_MESSAGE, error));
                }
            }
            // end additions and corrections /////////////////////////////////////////////////
        }

    }
}