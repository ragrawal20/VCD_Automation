using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Results : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["results"] != "" || Request.QueryString["results"] != null || Request.QueryString["results"] != string.Empty)
            {
                lblresult.Text = Request.QueryString["results"].ToString();

            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");

    }
}