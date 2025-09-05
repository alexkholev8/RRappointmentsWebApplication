using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Microsoft.Web.Administration;   // commented on 30/05/2013
using System.Web.UI.WebControls;
using System.Configuration;

namespace SetupSettingsClassLibray
{
    public partial class SettingsWindow : Form
    {
        // ServerManager manager;  // commented on 30/05/2013
        System.Configuration.Configuration config;

        public SettingsWindow()
        {
        // manager = new ServerManager();    // commented on 30/05/2013
            InitializeComponent();
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            for (int i = 1; i < 100; i++)
            {
                appointmentLimitComboBox.Items.Add(i);
            }

            appointmentLimitComboBox.Visible = false;
            appointmentLimitComboBox.SelectedIndex = 0;
        }


        private void LoadConfiguration()
        {
            string value;

            value = config.AppSettings.Settings["EnableTreatmentGroups"].Value;

            if (value == "True")
            {
                enableTreatmentGroupsCheckBox.Checked = true;
            }
            else
            {
                enableTreatmentGroupsCheckBox.Checked = false;
            }

            value = config.AppSettings.Settings["AppointmentLimit"].Value;

            if (value == "0")
            {
                appointmentLimitCheckBox.Checked = false;
                appointmentLimitComboBox.Visible = false;
            }
            else
            {
                appointmentLimitCheckBox.Checked = true;
                appointmentLimitComboBox.Visible = true;
                appointmentLimitComboBox.SelectedIndex = int.Parse(config.AppSettings.Settings["AppointmentLimit"].Value) -1;
            }

            value = config.AppSettings.Settings["WebServiceIP"].Value;
            wsIPtextBox.Text = value;

            value = config.AppSettings.Settings["WebServicePort"].Value;
            wsPortTextBox.Text = value;

            value = config.AppSettings.Settings["DishWebServiceURL"].Value;
            wsDISHtextBox.Text = value;

            value = config.AppSettings.Settings["wsUserName"].Value;
            wsUserIdTextBox.Text = value;

            value = config.AppSettings.Settings["wsPassword"].Value;
            wsPasswordTextBox.Text = value;



             
            //      //config.AppSettings.Settings["webServicePort"].Value = "ASDFASD";
            //        label3.Text = config.AppSettings.Settings["webServicePort"].Value;
            //      //config.Save();
        }

        private void saveSettingsButton_Click(object sender, EventArgs e)
        {

        }

        private void loadConfiurationButton_Click(object sender, EventArgs e)
        {
            try
            {
                config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(virtualDirectoryTextBox.Text);
                LoadConfiguration();
                FinishButton.Enabled = true;
            }
            catch
            {
                MessageBox.Show("קובץ קונפיגיורציה לא נמצא. ודא להכניס את השם התיקייה הוירטואלית כפי שהוגדרה בהתקנת האתר בין שני לוכסנים. דוגמא : /RRsite/");
            }
        }

        private void FinishButton_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
            this.Close();
        }

        private void SaveConfiguration()
        {

            if (enableTreatmentGroupsCheckBox.Checked)
            {
                config.AppSettings.Settings["EnableTreatmentGroups"].Value = "True";
            }
            else
            {
                config.AppSettings.Settings["EnableTreatmentGroups"].Value = "False";
            }

            if (appointmentLimitCheckBox.Checked)
            {
                config.AppSettings.Settings["AppointmentLimit"].Value = appointmentLimitComboBox.SelectedItem.ToString();
            }
            else
            {
                config.AppSettings.Settings["AppointmentLimit"].Value = "0";
            }

            config.AppSettings.Settings["WebServiceIP"].Value = wsIPtextBox.Text;
            

            config.AppSettings.Settings["WebServicePort"].Value =  wsPortTextBox.Text;

            config.AppSettings.Settings["DishWebServiceURL"].Value = wsDISHtextBox.Text ;

            config.AppSettings.Settings["wsUserName"].Value = wsUserIdTextBox.Text;

            config.AppSettings.Settings["wsPassword"].Value = wsPasswordTextBox.Text;
            config.Save();
            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void appointmentLimitCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (appointmentLimitCheckBox.Checked)
            {
                appointmentLimitComboBox.Visible = true;
            }
            else
            {
                appointmentLimitComboBox.Visible = false;
            }
        }
    
    }
}

// string s = manager.Sites[sitesComboBox.SelectedItem.ToString()].Applications["RRsite"].VirtualDirectories["RRsite"].Path;
//  string virtualDirectoryPhysicalPath = manager.Sites[sitesComboBox.SelectedItem.ToString()].Applications["/"].VirtualDirectories["/"].PhysicalPath + @"\" + virtualDirectoriesComboBox.SelectedItem.ToString();

//  WebConfigurationMap wcm = new WebConfigurationMap(null,virtualDirectoryPhysicalPath
// Microsoft.Web.Administration.Configuration config = manager.GetWebConfiguration(sitesComboBox.SelectedItem.ToString(),virtualDirectoriesComboBox.SelectedItem.ToString());
// label3.Text = virtualDirectoryPhysicalPath + @"\app.config";
// System.Configuration.Configuration config = ConfigurationManager.(virtualDirectoryPhysicalPath + @"\app.config");
// label3.Text = config.AppSettings.Settings["EnableTreatmentGroups"].Value;

//  ConfigurationManager.AppSettings["EnableTreatmentGroups"].ToString();

//private void InitialSettings()
//{
//    string sitePhysicalPath = null;


//    SiteCollection siteCollection = manager.Sites;

//    foreach (Site site in siteCollection)
//    {
//        sitesComboBox.Items.Add(site.Name);
//    }

//    if (sitesComboBox.Items.Count > 0)
//    {
//        sitesComboBox.SelectedIndex = 0;
//    }

//}

////private void sitesComboBox_SelectedIndexChanged(object sender, EventArgs e)
////{
////    virtualDirectoriesComboBox.Items.Clear();
////    string virtualDirectoryName = null;
////    string defaultVituralDirectoryName = "/RRsite/";
////    int defaultVituralDirectoryNameIndex = 0;

////    ApplicationCollection applications = manager.Sites[sitesComboBox.SelectedItem.ToString()].Applications;

//    foreach (Microsoft.Web.Administration.Application application in applications)
//    {
//        VirtualDirectoryCollection virtualDirectories = application.VirtualDirectories;

//        foreach (VirtualDirectory virtualDirectory in virtualDirectories)
//        {
//            virtualDirectoryName = virtualDirectory.ToString();

//            virtualDirectoryName = virtualDirectoryName.Substring(virtualDirectoryName.IndexOf("/") + 1);
//            virtualDirectoryName = virtualDirectoryName.Replace("/", "");
//            virtualDirectoryName = "/" + virtualDirectoryName + "/";
//            virtualDirectoriesComboBox.Items.Add(virtualDirectoryName);

//            if (virtualDirectoryName == defaultVituralDirectoryName)
//            {
//                defaultVituralDirectoryNameIndex = virtualDirectoriesComboBox.Items.Count - 1;
//            }
//        }
//        virtualDirectoriesComboBox.SelectedIndex = defaultVituralDirectoryNameIndex;

//    }
//}



//    private void virtualDirectoriesComboBox_SelectedIndexChanged(object sender, EventArgs e)
//    {
//        System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/RRsite/");
//      //config.AppSettings.Settings["webServicePort"].Value = "ASDFASD";
//        label3.Text = config.AppSettings.Settings["webServicePort"].Value;
//      //config.Save();


//    }
//}
