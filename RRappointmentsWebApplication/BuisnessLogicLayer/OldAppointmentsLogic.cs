using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Configuration;
using System.Data;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public class OldAppointmentsLogic
    {
        public static AppointmentsDBDataSet.OldAppointmentsDataTable oldAppointmentsDataTabel = new AppointmentsDBDataSet.OldAppointmentsDataTable();
                           
        List<OldAppointment> oldAppointments = new List<OldAppointment>();

        public void GetOldAppointments(string cust_num, ref System.Web.UI.WebControls.GridView oldAppointmentsGridView)
        {
            BuisnessLogicLayer.oldAppointmentsDataSource.oldAppointemntsDataTable.Rows.Clear();
            string url = "http://" + ConfigurationManager.AppSettings["WebServiceIP"].ToString() + ConfigurationManager.AppSettings["WebServicePort"].ToString() + "/"+ConfigurationManager.AppSettings["wsDBname"].ToString() +"/ws_get_old_appointments?al_cust_num=" + cust_num;
            try
            {
                XmlReader oldAppointmentsReader = XmlReader.Create(url, BuisnessLogicLayer.WebServiceSecurity.GetXmlReaderSettings());

                while (oldAppointmentsReader.Read())
                {
                    if (oldAppointmentsReader.Name == "row")
                    {
                        AppointmentsDBDataSet.OldAppointmentsRow row = BuisnessLogicLayer.oldAppointmentsDataSource.oldAppointemntsDataTable.NewOldAppointmentsRow();

                        OldAppointment oldAppointment = new OldAppointment();

                        oldAppointmentsReader.MoveToAttribute("dtime");
                        oldAppointment.AppointmentTime = Convert.ToDateTime(oldAppointmentsReader.Value).ToShortTimeString();
                        row.dtime= Convert.ToDateTime(oldAppointmentsReader.Value).ToShortTimeString();

                        oldAppointmentsReader.MoveToAttribute("ddate");
                        oldAppointment.AppointmentDate = Convert.ToDateTime(oldAppointmentsReader.Value).ToString("dd/MM/yyyy");
                        row.ddate = Convert.ToDateTime(oldAppointmentsReader.Value).ToString("dd/MM/yyyy");

                        row.day = Convert.ToDateTime(oldAppointmentsReader.Value).ToString("dddd");

                        oldAppointmentsReader.MoveToAttribute("c_branch");
                        oldAppointment.AppointmentLocation = oldAppointmentsReader.Value;
                        row.c_branch = oldAppointmentsReader.Value;
                        

                        oldAppointmentsReader.MoveToAttribute("oper_name");
                        oldAppointment.AppointmentLocation = oldAppointmentsReader.Value;
                        row.oper_name = oldAppointmentsReader.Value;

                        oldAppointments.Add(oldAppointment);

                        BuisnessLogicLayer.oldAppointmentsDataSource.oldAppointemntsDataTable.Rows.Add(row);
                    }
                }
                
            }
            catch(Exception)
            {
                
            }

        }


        internal string GetFileNum(string idNum)
        {
            string url = "http://" + ConfigurationManager.AppSettings["WebServiceIP"].ToString() + ConfigurationManager.AppSettings["WebServicePort"].ToString() + "/"+ConfigurationManager.AppSettings["wsDBname"].ToString() +"/ws_login_get_file_num?id_num=" + idNum;
            try
            {
                XmlReader logInReader = XmlReader.Create(url, BuisnessLogicLayer.WebServiceSecurity.GetXmlReaderSettings());
                while (logInReader.Read())
                {
                    if (logInReader.Name == "row")
                    {
                        logInReader.MoveToAttribute("file_num");
                        return logInReader.Value;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

    }
}