using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Configuration;
using System.Windows.Forms;
using System.IO;
//using Microsoft.Web.Administration;   // commented on 30/05/2013

namespace SetupSettingsClassLibray
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver); 
            ConfigureWebConfig();               
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
            
        }

        void ConfigureWebConfig()
        {
            
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }
    }
}
