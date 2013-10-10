using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {

        Session["username"] = UserName.Text;
        Session["pwd"] = Password.Text;
        Session["org"] = OrgName.Text;
        vcdpd pdcloud = new vcdpd();
        string token = pdcloud.GetAuthentication(UserName.Text, Password.Text, OrgName.Text);
        if (token != "error")
        {
            if (token != "" || token != string.Empty || token != null)
            {
                Response.Redirect("dashboard.aspx", false);

            }
            else
            {

                lblwrong.Visible = true;
                lblwrong.Text = "Cannot Login";
            }
        }
        else
        {
            lblwrong.Visible = true;
            lblwrong.Text = "Cannot Login";
        }
    }
}