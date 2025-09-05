using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using RRappointmentsWebApplication.DataBaseAccessLayer;
using System.Xml;
using RRappointmentsWebApplication.BuisnessLogicLayer;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        index_top_logo_img.Src = ConfigurationManager.AppSettings["LogoImage"].ToString();
        Session.Timeout = 20;
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        FormsAuthentication.SignOut();
        Response.Redirect("Index.aspx");
    }
}