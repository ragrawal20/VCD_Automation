using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Net;
using System.IO;
using System.Data;

public partial class dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDowns();
        }
    }
    protected void LoadDropDowns()
    {
        vcdpd pdcloud = new vcdpd();
        ddlOrg.DataSource = pdcloud.GetOrgDetails();
        ddlOrg.DataTextField = "name";
        ddlOrg.DataValueField = "href";
        ddlOrg.DataBind();
        lblresult.Visible = false;
        lblresult.Text = "";
        Session["admin"] = pdcloud.GetAdminAuthentication();

        if (Session["admin"] == null)
        {
            Response.Redirect("login.aspx", false);
        }


        ddlVDC.DataSource = pdcloud.GetVdc(ddlOrg.SelectedItem.Value, Session["admin"].ToString());
        ddlVDC.DataTextField = "name";
        ddlVDC.DataValueField = "href";
        ddlVDC.DataBind();


        ddlnetwork.DataSource = pdcloud.GetNetworkName(ddlOrg.SelectedItem.Value, Session["admin"].ToString());
        ddlnetwork.DataTextField = "name";
        ddlnetwork.DataValueField = "href";
        ddlnetwork.DataBind();


        ddlCatalog.DataSource = pdcloud.GetCatalog(ddlOrg.SelectedItem.Value, Session["admin"].ToString());
        ddlCatalog.DataTextField = "name";
        ddlCatalog.DataValueField = "href";
        ddlCatalog.DataBind();
        ddlCatalog.SelectedIndex = 0;

        //today start
        ddlcatalogItem.DataSource = pdcloud.GetCatalogItem(ddlCatalog.SelectedItem.Value, Session["admin"].ToString());

        ddlcatalogItem.DataTextField = "name";
        ddlcatalogItem.DataValueField = "href";
        ddlcatalogItem.DataBind();

        ddlTemplate.DataSource = pdcloud.GetVappTemplate(ddlcatalogItem.SelectedItem.Value, Session["admin"].ToString());
        ddlTemplate.DataTextField = "name";
        ddlTemplate.DataValueField = "href";
        ddlTemplate.DataBind();

        DataTable dtTemplateVms = pdcloud.GetVmsFromTemplate(ddlTemplate.SelectedItem.Value, Session["admin"].ToString());
        ddlvms.DataSource = dtTemplateVms;
        ddlvms.DataTextField = "name";
        ddlvms.DataValueField = "name";
        ddlvms.DataBind();



        

    }

    protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlVDC.Items.Clear();
        ddlnetwork.Items.Clear();
        ddlCatalog.Items.Clear();
        ddlcatalogItem.Items.Clear();
        ddlTemplate.Items.Clear();
        ddlvms.Items.Clear();
        lblresult.Visible = false;
        lblresult.Text = "";

        lbldebug.Text = "";

        try
        {

            if (Session["admin"] == null)
            {
                Response.Redirect("login.aspx", false);
            }

            vcdpd pdcloud = new vcdpd();
            ddlVDC.DataSource = pdcloud.GetVdc(ddlOrg.SelectedItem.Value, Session["admin"].ToString());
            ddlVDC.DataTextField = "name";
            ddlVDC.DataValueField = "href";
            ddlVDC.DataBind();

            //lbldebug.Text = pdcloud.GetVdcDebug(ddlOrg.SelectedItem.Value);


            ddlnetwork.DataSource = pdcloud.GetNetworkName(ddlOrg.SelectedItem.Value, Session["admin"].ToString());
            ddlnetwork.DataTextField = "name";
            ddlnetwork.DataValueField = "href";
            ddlnetwork.DataBind();

            ddlCatalog.DataSource = pdcloud.GetCatalog(ddlOrg.SelectedItem.Value, Session["admin"].ToString());
            ddlCatalog.DataTextField = "name";
            ddlCatalog.DataValueField = "href";
            ddlCatalog.DataBind();

            ddlcatalogItem.DataSource = pdcloud.GetCatalogItem(ddlCatalog.SelectedItem.Value, Session["admin"].ToString());

            ddlcatalogItem.DataTextField = "name";
            ddlcatalogItem.DataValueField = "href";
            ddlcatalogItem.DataBind();

            ddlTemplate.DataSource = pdcloud.GetVappTemplate(ddlcatalogItem.SelectedItem.Value, Session["admin"].ToString());
            ddlTemplate.DataTextField = "name";
            ddlTemplate.DataValueField = "href";
            ddlTemplate.DataBind();

            DataTable dtTemplateVms = pdcloud.GetVmsFromTemplate(ddlTemplate.SelectedItem.Value, Session["admin"].ToString());
            ddlvms.DataSource = dtTemplateVms;
            ddlvms.DataTextField = "name";
            ddlvms.DataValueField = "name";
            ddlvms.DataBind();
           


        }
        catch (Exception ee)
        {
            string message = ee.Message;
            //  continue;
        }
    }

    protected void ddlCatalog_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlcatalogItem.Items.Clear();
        ddlTemplate.Items.Clear();
        ddlvms.Items.Clear();
        lblresult.Visible = false;
        lblresult.Text = "";
        if (Session["admin"] == null)
        {
            Response.Redirect("login.aspx", false);
        }
        try
        {
            vcdpd pdcloud = new vcdpd();
            ddlcatalogItem.DataSource = pdcloud.GetCatalogItem(ddlCatalog.SelectedItem.Value, Session["admin"].ToString());

            ddlcatalogItem.DataTextField = "name";
            ddlcatalogItem.DataValueField = "href";
            ddlcatalogItem.DataBind();

            ddlTemplate.DataSource = pdcloud.GetVappTemplate(ddlcatalogItem.SelectedItem.Value, Session["admin"].ToString());
            ddlTemplate.DataTextField = "name";
            ddlTemplate.DataValueField = "href";
            ddlTemplate.DataBind();

            DataTable dtTemplateVms = pdcloud.GetVmsFromTemplate(ddlTemplate.SelectedItem.Value, Session["admin"].ToString());
            ddlvms.DataSource = dtTemplateVms;
            ddlvms.DataTextField = "name";
            ddlvms.DataValueField = "name";
            ddlvms.DataBind();


    


        }
        catch (Exception ee)
        {
            string message = ee.Message;
            //  continue;
        }

    }

    protected void ddlcatalogItem_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlTemplate.Items.Clear();
        ddlvms.Items.Clear();
        lblresult.Visible = false;
        lblresult.Text = "";
        try
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("login.aspx", false);
            }
            vcdpd pdcloud = new vcdpd();

            ddlTemplate.DataSource = pdcloud.GetVappTemplate(ddlcatalogItem.SelectedItem.Value, Session["admin"].ToString());
            ddlTemplate.DataTextField = "name";
            ddlTemplate.DataValueField = "href";
            ddlTemplate.DataBind();

            DataTable dtTemplateVms = pdcloud.GetVmsFromTemplate(ddlTemplate.SelectedItem.Value, Session["admin"].ToString());
            ddlvms.DataSource = dtTemplateVms;
            ddlvms.DataTextField = "name";
            ddlvms.DataValueField = "name";
            ddlvms.DataBind();




        }
        catch (Exception ee)
        {
            string message = ee.Message;

        }



    }
   
        protected void ddlVDC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try{
                vcdpd pdcloud = new vcdpd();
                if (Session["admin"] == null)
                {
                    Response.Redirect("login.aspx", false);
                }

                lblresult.Visible = false;
                lblresult.Text = "";
                ddlnetwork.DataSource = pdcloud.GetNetworkName(ddlOrg.SelectedItem.Value, Session["admin"].ToString());
                ddlnetwork.DataTextField = "name";
                ddlnetwork.DataValueField = "href";
                ddlnetwork.DataBind();
                ddlCatalog.SelectedIndex = 0;

                ddlCatalog.DataSource = pdcloud.GetCatalog(ddlOrg.SelectedItem.Value, Session["admin"].ToString());
                    ddlCatalog.DataTextField = "name";
                    ddlCatalog.DataValueField = "href";
                    ddlCatalog.DataBind();
                    ddlCatalog.SelectedIndex = 0;

                    //today start
                    ddlcatalogItem.DataSource = pdcloud.GetCatalogItem(ddlCatalog.SelectedItem.Value, Session["admin"].ToString());

                    ddlcatalogItem.DataTextField = "name";
                    ddlcatalogItem.DataValueField = "href";
                    ddlcatalogItem.DataBind();

                    ddlTemplate.DataSource = pdcloud.GetVappTemplate(ddlcatalogItem.SelectedItem.Value, Session["admin"].ToString());
                    ddlTemplate.DataTextField = "name";
                    ddlTemplate.DataValueField = "href";
                    ddlTemplate.DataBind();
                    //today end
                }
                catch (Exception ee)
                {
                    string message = ee.Message;
                  //  continue;
                }
        }

        //protected void Button1_OnPreRender(object sender, EventArgs e)
        //{
        //    string URL = "~/MyPage.aspx";
        //    URL = Page.ResolveClientUrl(URL);
        //    Button1.OnClientClick = "window.open('" + URL + "'); return false;";
        //}

        protected void Button1_Click(object sender, EventArgs e)
        {
            string networkvdc = ddlnetwork.SelectedItem.Value;

            string vdc = ddlVDC.SelectedItem.Value;
            lblresult.Text = "";
            lblresult.Visible = false;
            if (Session["admin"] == null)
            {
                Response.Redirect("login.aspx", false);
            }

            string networkname = ddlnetwork.SelectedItem.Text;
            vcdpd pdcloud = new vcdpd();
            string test2 = "<InstantiateVAppTemplateParams" + "\n" +
"xmlns='http://www.vmware.com/vcloud/v1.5'" + "\n" +
"xmlns:ovf='http://schemas.dmtf.org/ovf/envelope/1'" + "\n" +
"name=" + "'" + txtserverName.Text + "'" + "\n" +
"deploy='true'" + "\n" +
"powerOn='true'>" + "\n" +
"<Description>Testing AppServer</Description>" + "\n" +
"<InstantiationParams>" + "\n" +
"<NetworkConfigSection type='application/vnd.vmware.vcloud.networkConfigSection+xml' href=" + "'" + ddlTemplate.SelectedItem.Value + "/networkConfigSection/" + "'" + " ovf:required='false'>" + "\n" +
   "<ovf:Info>The configuration parameters for logical networks</ovf:Info>" + "\n" +
   "<NetworkConfig networkName=" + "'" + networkname + "'" + ">" + "\n" +
       "<Description>This is a special place-holder used for disconnected network interfaces.</Description>" + "\n" +
       
       "<IsDeployed>false</IsDeployed>" + "\n" +
   "</NetworkConfig>" + "\n" +
"</NetworkConfigSection>" + "\n" +
"</InstantiationParams>" + "\n" +
"<Source href=" + "'" + ddlTemplate.SelectedItem.Value + "'" + "/>" + "\n" +
"</InstantiateVAppTemplateParams>";

          

            string vcdspin = "<InstantiateVAppTemplateParams xmlns='http://www.vmware.com/vcloud/v1.5' xmlns:ovf='http://schemas.dmtf.org/ovf/envelope/1'"+"\n"+
              "name=" + "'" + txtserverName.Text + "'" + "\n" +
"deploy='false'" + "\n" +
"powerOn='false'>" + "\n" +
  "<Description>Created By Service Account</Description>" + "\n" +
 "<InstantiationParams>" + "\n" +

  "</InstantiationParams>" + "\n" +
   "<Source href=" + "'" + ddlTemplate.SelectedItem.Value + "'" + "/>" + "\n" +
  "</InstantiateVAppTemplateParams>";
           // string test = pdcloud.serverSpinUp(test2, ddlVDC.SelectedItem.Value);



            string vcdnetworkspin = "<InstantiateVAppTemplateParams xmlns='http://www.vmware.com/vcloud/v1.5' xmlns:ovf='http://schemas.dmtf.org/ovf/envelope/1'" + "\n" +
              "name=" + "'" + txtserverName.Text + "'" + "\n" +
"deploy='false'" + "\n" +
"powerOn='false'>" + "\n" +
  "<Description>Created By Service Account</Description>" + "\n" +
 "<InstantiationParams>" + "\n" +
 "<NetworkConfigSection type='application/vnd.vmware.vcloud.networkConfigSection+xml' href=" + "'" + ddlTemplate.SelectedItem.Value + "/networkConfigSection/'" +" ovf:required='false'>"+"\n"+
	"<ovf:Info>The configuration parameters for logical networks</ovf:Info>"+"\n"+
	  "<NetworkConfig networkName=" + "'sds-network"  + "'" + ">" + "\n" +
	"<Description>This is a special place-holder used for disconnected network interfaces.</Description>"+"\n"+
	
	"<IsDeployed>true</IsDeployed>"+"\n"+
	"</NetworkConfig>"+"\n"+
	"</NetworkConfigSection>"+"\n"+
  "</InstantiationParams>" + "\n" +
   "<Source href=" + "'" + ddlTemplate.SelectedItem.Value + "'" + "/>" + "\n" +
  "</InstantiateVAppTemplateParams>";

            //prepare the vms list start
            string vmslist = "";
            for (int i = 0; i < ddlvms.Items.Count; i++)
            {
                if (ddlvms.Items[i].Selected == true)
                {
                    vmslist = vmslist.ToLower() + "&&"+ddlvms.Items[i].Text.ToLower();
                }

            }
            //prepare the vms list end
      


                //string result = pdcloud.serverSpinUp(vcdspin, ddlVDC.SelectedItem.Value);
            string result = pdcloud.serverSpinUp(vcdspin, ddlVDC.SelectedItem.Value);
                lblresult.Visible = true;
                lblresult.Text = result+"\n\n"+ "Please login to https://vcd-dn5.pd-cloud.com/cloud/org/"+ddlOrg.SelectedItem.Text+" to see status of your vapp";
                try
                {
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.LoadXml(result);

                    XmlNodeList elemList = doc.GetElementsByTagName("Task");
                    string taskhref = "";
                    string taskguid = "";
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        taskhref = elemList[i].Attributes["href"].Value;
                        taskguid = taskhref.Replace("https://vcd-dn5.pd-cloud.com/api/task/", "");

                    }


                    string netguid = "";
                    netguid = ddlnetwork.SelectedItem.Value.Replace("https://vcd-dn5.pd-cloud.com/api/network/", "");
                    string status = new vcdpd().GetTaskStatus(taskhref);


                    string guid = new vcdpd().GetVappGuid(ddlOrg.SelectedItem.Text, txtserverName.Text);

                      string results = new vcdpd().InsertVappDetails(taskguid, guid, txtserverName.Text, "juststarted", ddlOrg.SelectedItem.Text, vmslist,ddlnetwork.SelectedItem.Text,netguid);

                  //    Response.Redirect("Results.aspx?results=" + lblresult.Text, false);

                    //lbvms.DataSource = new vcdpd().GetVmDetailsfromVapp(ddlOrg.SelectedItem.Text, txtserverName.Text);
                    //lbvms.DataValueField = "href";
                    //lbvms.DataTextField = "name";
                    //lbvms.DataBind();


                    //DataTable dt = new vcdpd().GetVappDetailsById(guid);

                }
                catch (Exception ee)
                {
                    lblresult.Visible = true;
                    lblresult.Text = "error.try again " + ee.Message;
                }
            


        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ddlvms.Items.Count; i++)
            {
                ddlvms.Items[i].Selected = true;

            }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ddlvms.Items.Count; i++)
            {
                ddlvms.Items[i].Selected = false;

            }
        }
}