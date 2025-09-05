using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using RRappointmentsWebApplication.BuisnessLogicLayer;

namespace RRappointmentsWebApplication
{
    public partial class update_pass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblWarning.Text = Global.PWD_UPDATE;
            lblWarning.ForeColor = System.Drawing.Color.DarkRed;
            lblWarning.Style.Add("visibility", "visible");
            lblWarning.Style.Add(HtmlTextWriterStyle.FontSize, "18px");

            btnOK.Style.Add(HtmlTextWriterStyle.FontSize, "18px");
            btnCancel.Style.Add(HtmlTextWriterStyle.FontSize, "18px");
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("forgot_pass.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("step1.aspx");
        }
    }
}