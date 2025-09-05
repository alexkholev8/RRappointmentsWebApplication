using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Configuration;
using System.Net;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public class WebServiceSecurity
    {
        static string password = ConfigurationManager.AppSettings["wsPassword"].ToString();
        static string userId= ConfigurationManager.AppSettings["wsUserName"].ToString();

        public static XmlReaderSettings GetXmlReaderSettings()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            XmlUrlResolver resolver = new XmlUrlResolver();

            resolver.Credentials = new NetworkCredential(userId,password );
            settings.XmlResolver = resolver;
            return settings;
        }
                
              
    }
}