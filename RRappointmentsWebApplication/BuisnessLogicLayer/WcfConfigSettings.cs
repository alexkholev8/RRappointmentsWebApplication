using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Configuration;
using System.Reflection;
using System.IO;
using System.Web.UI;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public class WcfConfigSettings
    {
        private static string NodePath = "//system.serviceModel//client//endpoint";
        public static readonly string CONFIGURATION_FILE = "\\Web.config";
        public static readonly string DEFAULT_ENDPOINT_ADDRESS = @"http://localhost:8732/Design_Time_Addresses/WcfDiariesCounterServiceLibrary/DiariesCounter/";
        private WcfConfigSettings() { }

        public static string GetEndpointAddress()
        {
            string address = WcfConfigSettings.loadConfigDocument().SelectSingleNode(NodePath).Attributes["address"].Value;

            return (string.IsNullOrEmpty(address) ? DEFAULT_ENDPOINT_ADDRESS : address);

            // return WcfConfigSettings.loadConfigDocument().SelectSingleNode(NodePath).Attributes["address"].Value;
        }

        public static void SaveEndpointAddress(string endpointAddress)
        {
            // load config document for current assembly
            XmlDocument doc = loadConfigDocument();

            // retrieve appSettings node
            XmlNode node = doc.SelectSingleNode(NodePath);

            if (node == null)
                throw new InvalidOperationException("Error. Could not find endpoint node in config file.");

            try
            {
                // select the 'add' element that contains the key
                //XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));
                node.Attributes["address"].Value = string.IsNullOrEmpty(endpointAddress) ?
                    DEFAULT_ENDPOINT_ADDRESS : endpointAddress;

                doc.Save(getConfigFilePath());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static XmlDocument loadConfigDocument()
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(getConfigFilePath());
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }
        }

        private static string getConfigFilePath()
        {
            string configPath = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile) + CONFIGURATION_FILE;
            //string localpath = Server.MapPath(OptGlobal.GetRootRelativePath(CONFIGURATION_FILE));

            return configPath;
            //return Assembly.GetExecutingAssembly().Location + ".config";
        }
    }
}