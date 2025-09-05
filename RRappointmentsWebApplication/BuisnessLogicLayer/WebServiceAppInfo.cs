using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Configuration;
using System.Web.UI.WebControls;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public static class WebServiceAppInfo
    {
        private static Dictionary<string, string> insuaranceDictionary;
        private static Dictionary<string, string> sitesDictionary;
        private static Dictionary<string, string> treatmentDictionary;
        private static Dictionary<string, string> treatmentGroupDictionary;
        private static Dictionary<string, AssignedAppointment> createdAppointments;

        public static Dictionary<string, string> TreatmentGroupDictionary
        {
            get { return WebServiceAppInfo.treatmentGroupDictionary; }
            set { WebServiceAppInfo.treatmentGroupDictionary = value; }
        }
        

        public static Dictionary<string, AssignedAppointment> CreatedAppointments
        {
            get { return WebServiceAppInfo.createdAppointments; }
            set { WebServiceAppInfo.createdAppointments = value; }
        }

        public static Dictionary<string, string> SitesDictionary
        {
            get { return WebServiceAppInfo.sitesDictionary; }
            set { WebServiceAppInfo.sitesDictionary = value; }
        }


        public static Dictionary<string, string> TreatmentDictionary
        {
            get { return WebServiceAppInfo.treatmentDictionary; }
            set { WebServiceAppInfo.treatmentDictionary = value; }
        }


        public static Dictionary<string, string> InsuaranceDictionary
        {
            get { return WebServiceAppInfo.insuaranceDictionary; }
            set { WebServiceAppInfo.insuaranceDictionary = value; }
        }


        public static bool GetInsuranceList()
        {
            string insur_name = null;
            string insure_code = null;
            InsuaranceDictionary = new Dictionary<string, string>();
            string url = "http://" + ConfigurationManager.AppSettings["WebServiceIP"].ToString() + ConfigurationManager.AppSettings["WebServicePort"].ToString() + "/"+ ConfigurationManager.AppSettings["wsDBname"].ToString() +"/ws_xml_zimun_insur";

            try
            {
                XmlReader operReader = XmlReader.Create(url,BuisnessLogicLayer.WebServiceSecurity.GetXmlReaderSettings());
                while (operReader.Read())
                {
                    if (operReader.Name == "row")
                    {
                        operReader.MoveToAttribute("insur_code");
                        insure_code = operReader.Value;
                        operReader.MoveToAttribute("insur_name");
                        insur_name = operReader.Value;
                        insuaranceDictionary.Add(insure_code, insur_name);
                    }
                }
            }
            catch (Exception)
            {
                return true;
            }
            return false;
        }

        public static bool GetSiteList(string oper_code)
        {
            string site_name = null;
            string site_code = null;
            sitesDictionary = new Dictionary<string, string>();
            string url = "http://" + ConfigurationManager.AppSettings["WebServiceIP"].ToString() + ConfigurationManager.AppSettings["WebServicePort"].ToString() + "/"+ConfigurationManager.AppSettings["wsDBname"].ToString() +"/ws_xml_zimun_sites?al_oper_code="+oper_code;
            try
            {
                XmlReader operReader = XmlReader.Create(url, BuisnessLogicLayer.WebServiceSecurity.GetXmlReaderSettings());
                bool keyExists = false;
                while (operReader.Read())
                {
                    if (operReader.Name == "row")
                    {
                        operReader.MoveToAttribute("branch_code");
                        site_code = operReader.Value;
                        operReader.MoveToAttribute("site_name");
                        site_name = operReader.Value;
                        
                        foreach (string key in sitesDictionary.Keys)
                        {
                            if(key == site_code) keyExists = true;
                        }

                        if (!keyExists) sitesDictionary.Add(site_code, site_name);
                        keyExists = false;            
                    }
                }
            }
            catch (Exception)
            {
                return true;
            }
            return false;
        }

        public static bool GetOperList()
        {
            string treatName = null;
            string treatCode = null;
            string groupName = null;

            treatmentDictionary      = new Dictionary<string, string>();
            treatmentGroupDictionary = new Dictionary<string, string>();
            string url = "http://" + ConfigurationManager.AppSettings["WebServiceIP"].ToString() + ConfigurationManager.AppSettings["WebServicePort"].ToString() + "/"+ConfigurationManager.AppSettings["wsDBname"].ToString() +"/ws_xml_zimun_get_oper";
            try
            {
                XmlReader treatReader = XmlReader.Create(url, BuisnessLogicLayer.WebServiceSecurity.GetXmlReaderSettings());
                while (treatReader.Read())
                {
                    if (treatReader.Name == "row")
                    {
                        treatReader.MoveToAttribute("opername");
                        treatName = treatReader.Value;
                        treatReader.MoveToAttribute("oper_code");
                        treatCode = treatReader.Value;       
                        treatReader.MoveToAttribute("_3");
                        groupName = treatReader.Value;
                        treatmentDictionary.Add(treatCode, treatName);
                        treatmentGroupDictionary.Add(treatCode, treatReader.Value);

                    }
                }
            }
            catch (Exception)
            {
                return true;
            }
            return false;
        }
    }
}