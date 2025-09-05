using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Configuration;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public class GetTreatmentInstructionsLogic
    {
        internal static string GetTreatmentInstructions(string treatmentCode)
        {
            string treatInstruction = null;
            string url= "http://" + ConfigurationManager.AppSettings["WebServiceIP"].ToString() + ConfigurationManager.AppSettings["WebServicePort"].ToString() + "/"+ConfigurationManager.AppSettings["wsDBname"].ToString() +"/ws_get_treat_instruction?al_oper_code="+treatmentCode;
            try
            {
                XmlReader operReader = XmlReader.Create(url, BuisnessLogicLayer.WebServiceSecurity.GetXmlReaderSettings());
                while (operReader.Read())
                {
                    if (operReader.Name == "row")
                    {
                        operReader.MoveToAttribute("instruction");
                        treatInstruction = operReader.Value;
                    }
                }
            }
            catch (Exception)
            {
                return "Error";
            }
            return treatInstruction;
        }
    }
}