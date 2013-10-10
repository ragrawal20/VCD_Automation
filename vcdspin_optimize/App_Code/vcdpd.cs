using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Web.UI.WebControls;
using System.Diagnostics;

/// <summary>
/// Summary description for vcdpd
/// </summary>
public class vcdpd
{
    public static string token = "";
    public vcdpd()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string GetAuthentication(string username, string password, string org)
    {
        string url = "https://vcd-dn5.pd-cloud.com/api/sessions";
        HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;

        string authInfo = username + "@" + org + ":" + password;

        authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
        req.Headers["Authorization"] = "Basic " + authInfo;

        req.Accept = "application/*+xml;version=5.1";
        try
        {
            // req.ContentType = "application/xml";
            req.Method = "POST";
            Stream dataStreamw = req.GetRequestStream();
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            token = resp.Headers["x-vcloud-authorization"];
            Encoding enc = System.Text.Encoding.GetEncoding(1252);
            StreamReader loResponseStream = new StreamReader(resp.GetResponseStream(), enc);
            return token;
        }

        catch (Exception e)
        {
            return "error";
        }

    }

    public string GetAdminAuthentication()
    {
        string url = "https://vcd-dn5.pd-cloud.com/api/sessions";
        HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;

        string authInfo = "administrator" + "@" + "System" + ":" + "nkrocKjxa5";

        authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
        req.Headers["Authorization"] = "Basic " + authInfo;

        req.Accept = "application/*+xml;version=5.1";
        try
        {
            // req.ContentType = "application/xml";
            req.Method = "POST";
            Stream dataStreamw = req.GetRequestStream();
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            token = resp.Headers["x-vcloud-authorization"];
            Encoding enc = System.Text.Encoding.GetEncoding(1252);
            StreamReader loResponseStream = new StreamReader(resp.GetResponseStream(), enc);
            return token;
        }

        catch (Exception e)
        {
            return "error";
        }
    }

    public DataTable GetOrgDetails()
    {
        string url = "https://vcd-dn5.pd-cloud.com/api/org";



        HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
        DataTable table = new DataTable();
        req.Headers["x-vcloud-authorization"] = token;

        req.Accept = "application/*+xml;version=5.1";

        // req.ContentType = "application/xml";
        req.Method = "GET";

        HttpWebResponse resp = req.GetResponse() as HttpWebResponse;

        Encoding enc2 = System.Text.Encoding.GetEncoding(1252);
        //     StreamReader loResponseStream2 =
        //new StreamReader(resp2.GetResponseStream(), enc2);

        using (Stream s = req.GetResponse().GetResponseStream())
        {
            using (StreamReader sr = new StreamReader(s))
            {
                // Xml test9 = sr.ReadToEnd();
                string results = sr.ReadToEnd();

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();


                doc.LoadXml(results);

                table.Columns.Add("name", typeof(string));
                table.Columns.Add("href", typeof(string));


                XmlNodeList elemList = doc.GetElementsByTagName("OrgList");
                for (int i = 0; i < elemList.Count; i++)
                {
                    foreach (XmlNode item in elemList[i].ChildNodes)
                    {
                        if (item.Name == "Org")
                        {

                            ListItem litem = new ListItem();
                            litem.Text = item.Attributes["name"].InnerText;
                            litem.Value = item.Attributes["href"].InnerText;
                            DataRow dr = table.NewRow();
                            dr["name"] = item.Attributes["name"].InnerText; ;
                            dr["href"] = item.Attributes["href"].InnerText;
                            table.Rows.Add(dr);

                        }

                    }
                }



            }
        }

        return table;
        //request 2 end
    }

    public DataTable GetVdc(string url,string admintoken)
    {
        //request 3 start

        // string url = "http://localhost:9000/RestServiceImpl.svc/GetAllAppDetails";
        HttpWebRequest req3 = WebRequest.Create(url) as HttpWebRequest;

        req3.Headers["x-vcloud-authorization"] = admintoken;

        req3.Accept = "application/*+xml;version=1.5";

        // req.ContentType = "application/xml";
        req3.Method = "GET";
        DataTable table = new DataTable();
        table.Columns.Add("name", typeof(string));
        table.Columns.Add("href", typeof(string));
        HttpWebResponse resp3 = req3.GetResponse() as HttpWebResponse;

        Encoding enc3 = System.Text.Encoding.GetEncoding(1252);
        //     StreamReader loResponseStream3 =
        //new StreamReader(resp3.GetResponseStream(), enc3);


      

        using (Stream s = req3.GetResponse().GetResponseStream())
        {
            using (StreamReader sr = new StreamReader(s))
            {
                // Xml test9 = sr.ReadToEnd();
                string results = sr.ReadToEnd();

                
                
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();


                doc.LoadXml(results);
                DataSet dsStudent = new DataSet();

                XmlNodeList elemList = doc.GetElementsByTagName("Org");
                for (int i = 0; i < elemList.Count; i++)
                {
                    foreach (XmlNode item in elemList[i].ChildNodes)
                    {
                        if (item.Name == "Link")
                        {

                            ListItem litem = new ListItem();
                            try
                            {
                                litem.Text = item.Attributes["name"].InnerText;
                                if (item.Attributes["href"].InnerText.Contains("vdc"))
                                {
                                    litem.Value = item.Attributes["href"].InnerText;

                                    DataRow dr = table.NewRow();
                                    dr["name"] = item.Attributes["name"].InnerText; ;
                                    dr["href"] = item.Attributes["href"].InnerText;
                                    table.Rows.Add(dr);

                                }

                            }
                            catch (Exception ee)
                            {
                                string message = ee.Message;
                                continue;
                            }
                        }
                    }
                }
            }



        }
        return table;
        //request 3 end
    }


    public DataTable GetNetworkName(string url, string admintoken)
    {
        //request 3 start

        // string url = "http://localhost:9000/RestServiceImpl.svc/GetAllAppDetails";
        HttpWebRequest req3 = WebRequest.Create(url) as HttpWebRequest;

        req3.Headers["x-vcloud-authorization"] = admintoken;

        req3.Accept = "application/*+xml;version=1.5";

        // req.ContentType = "application/xml";
        req3.Method = "GET";
        DataTable table = new DataTable();
        table.Columns.Add("name", typeof(string));
        table.Columns.Add("href", typeof(string));
        HttpWebResponse resp3 = req3.GetResponse() as HttpWebResponse;

        Encoding enc3 = System.Text.Encoding.GetEncoding(1252);
        //     StreamReader loResponseStream3 =
        //new StreamReader(resp3.GetResponseStream(), enc3);

        using (Stream s = req3.GetResponse().GetResponseStream())
        {
            using (StreamReader sr = new StreamReader(s))
            {
                // Xml test9 = sr.ReadToEnd();
                string results = sr.ReadToEnd();

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();


                doc.LoadXml(results);
                DataSet dsStudent = new DataSet();

                XmlNodeList elemList = doc.GetElementsByTagName("Org");
                for (int i = 0; i < elemList.Count; i++)
                {
                    foreach (XmlNode item in elemList[i].ChildNodes)
                    {
                        if (item.Name == "Link")
                        {

                            ListItem litem = new ListItem();
                            try
                            {
                                litem.Text = item.Attributes["name"].InnerText;
                                if (item.Attributes["href"].InnerText.Contains("network"))
                                {
                                    litem.Value = item.Attributes["href"].InnerText;

                                    DataRow dr = table.NewRow();
                                    dr["name"] = item.Attributes["name"].InnerText; ;
                                    dr["href"] = item.Attributes["href"].InnerText;
                                    table.Rows.Add(dr);

                                }

                            }
                            catch (Exception ee)
                            {
                                string message = ee.Message;
                                continue;
                            }
                        }
                    }
                }
            }



        }
        return table;
        //request 3 end
    }

    public DataTable GetCatalog(string url, string admintoken)
    {
        //request 3 start

        // string url = "http://localhost:9000/RestServiceImpl.svc/GetAllAppDetails";
        HttpWebRequest req3 = WebRequest.Create(url) as HttpWebRequest;

        req3.Headers["x-vcloud-authorization"] = admintoken;

        req3.Accept = "application/*+xml;version=1.5";

        // req.ContentType = "application/xml";
        req3.Method = "GET";
        DataTable table = new DataTable();
        table.Columns.Add("name", typeof(string));
        table.Columns.Add("href", typeof(string));
        HttpWebResponse resp3 = req3.GetResponse() as HttpWebResponse;

        Encoding enc3 = System.Text.Encoding.GetEncoding(1252);
        //     StreamReader loResponseStream3 =
        //new StreamReader(resp3.GetResponseStream(), enc3);

        using (Stream s = req3.GetResponse().GetResponseStream())
        {
            using (StreamReader sr = new StreamReader(s))
            {
                // Xml test9 = sr.ReadToEnd();
                string results = sr.ReadToEnd();

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();


                doc.LoadXml(results);
                DataSet dsStudent = new DataSet();

                XmlNodeList elemList = doc.GetElementsByTagName("Org");
                for (int i = 0; i < elemList.Count; i++)
                {
                    foreach (XmlNode item in elemList[i].ChildNodes)
                    {
                        if (item.Name == "Link")
                        {

                            ListItem litem = new ListItem();
                            try
                            {
                                litem.Text = item.Attributes["name"].InnerText;
                                if (!item.Attributes["href"].InnerText.Contains("vdc"))
                                {
                                    litem.Value = item.Attributes["href"].InnerText;

                                    DataRow dr = table.NewRow();
                                    dr["name"] = item.Attributes["name"].InnerText; ;
                                    dr["href"] = item.Attributes["href"].InnerText;
                                    table.Rows.Add(dr);

                                }

                            }
                            catch (Exception ee)
                            {
                                string message = ee.Message;
                                continue;
                            }
                        }
                    }
                }
            }



        }
        return table;
        //request 3 end
    }

    public DataTable GetCatalogItem(string url, string admintoken)
    {
        DataTable table = new DataTable();
        table.Columns.Add("name", typeof(string));
        table.Columns.Add("href", typeof(string));

        HttpWebRequest req3 = WebRequest.Create(url) as HttpWebRequest;

        req3.Headers["x-vcloud-authorization"] = admintoken;

        req3.Accept = "application/*+xml;version=1.5";

        // req.ContentType = "application/xml";
        req3.Method = "GET";

        HttpWebResponse resp3 = req3.GetResponse() as HttpWebResponse;

        Encoding enc3 = System.Text.Encoding.GetEncoding(1252);
        //     StreamReader loResponseStream3 =
        //new StreamReader(resp3.GetResponseStream(), enc3);

        using (Stream s = req3.GetResponse().GetResponseStream())
        {
            using (StreamReader sr = new StreamReader(s))
            {
                // Xml test9 = sr.ReadToEnd();
                string results = sr.ReadToEnd();

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();


                doc.LoadXml(results);
                DataSet dsStudent = new DataSet();

                XmlNodeList elemList = doc.GetElementsByTagName("CatalogItems");
                for (int i = 0; i < elemList.Count; i++)
                {
                    foreach (XmlNode item in elemList[i].ChildNodes)
                    {
                        if (item.Name == "CatalogItem")
                        {

                            ListItem litem = new ListItem();
                            try
                            {
                                litem.Text = item.Attributes["name"].InnerText;

                                litem.Value = item.Attributes["href"].InnerText;

                                DataRow dr = table.NewRow();
                                dr["name"] = item.Attributes["name"].InnerText; ;
                                dr["href"] = item.Attributes["href"].InnerText;
                                table.Rows.Add(dr);

                            }
                            catch (Exception ee)
                            {
                                string message = ee.Message;
                                continue;
                            }
                        }
                    }
                }
            }



        }
        return table;
        //request 3 end
    }

    public DataTable GetVappTemplate(string url, string admintoken)
    {
        //request 3 start
        DataTable table = new DataTable();
        table.Columns.Add("name", typeof(string));
        table.Columns.Add("href", typeof(string));

        HttpWebRequest req3 = WebRequest.Create(url) as HttpWebRequest;

        req3.Headers["x-vcloud-authorization"] = admintoken;

        req3.Accept = "application/*+xml;version=1.5";

        // req.ContentType = "application/xml";
        req3.Method = "GET";

        HttpWebResponse resp3 = req3.GetResponse() as HttpWebResponse;

        Encoding enc3 = System.Text.Encoding.GetEncoding(1252);
        //     StreamReader loResponseStream3 =
        //new StreamReader(resp3.GetResponseStream(), enc3);

        using (Stream s = req3.GetResponse().GetResponseStream())
        {
            using (StreamReader sr = new StreamReader(s))
            {
                // Xml test9 = sr.ReadToEnd();
                string results = sr.ReadToEnd();

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();


                doc.LoadXml(results);
                DataSet dsStudent = new DataSet();

                XmlNodeList elemList = doc.GetElementsByTagName("CatalogItem");
                for (int i = 0; i < elemList.Count; i++)
                {
                    foreach (XmlNode item in elemList[i].ChildNodes)
                    {
                        if (item.Name == "Entity")
                        {

                            ListItem litem = new ListItem();
                            try
                            {
                                litem.Text = item.Attributes["name"].InnerText;

                                litem.Value = item.Attributes["href"].InnerText;

                                DataRow dr = table.NewRow();
                                dr["name"] = item.Attributes["name"].InnerText; ;
                                dr["href"] = item.Attributes["href"].InnerText;
                                table.Rows.Add(dr);

                            }
                            catch (Exception ee)
                            {
                                string message = ee.Message;
                                continue;
                            }
                        }
                    }
                }
            }



        }
        return table;
        //request 3 end
    }


    public DataTable GetVmsFromTemplate(string url, string admintoken)
    {
        DataTable table = new DataTable();
        table.Columns.Add("name", typeof(string));
       

        HttpWebRequest req3 = WebRequest.Create(url) as HttpWebRequest;

        req3.Headers["x-vcloud-authorization"] = admintoken;

        req3.Accept = "application/*+xml;version=1.5";

        // req.ContentType = "application/xml";
        req3.Method = "GET";

        HttpWebResponse resp3 = req3.GetResponse() as HttpWebResponse;

        Encoding enc3 = System.Text.Encoding.GetEncoding(1252);
        //     StreamReader loResponseStream3 =
        //new StreamReader(resp3.GetResponseStream(), enc3);

        using (Stream s = req3.GetResponse().GetResponseStream())
        {
            using (StreamReader sr = new StreamReader(s))
            {
                // Xml test9 = sr.ReadToEnd();
                string results = sr.ReadToEnd();

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();


                doc.LoadXml(results);
                DataSet dsStudent = new DataSet();

                XmlNodeList elemList = doc.GetElementsByTagName("Children");
                for (int i = 0; i < elemList.Count; i++)
                {
                    foreach (XmlNode item in elemList[i].ChildNodes)
                    {
                        if (item.Name == "Vm")
                        {

                            ListItem litem = new ListItem();
                            try
                            {
                                

                                DataRow dr = table.NewRow();
                                dr["name"] = item.Attributes["name"].InnerText; ;
                                
                                table.Rows.Add(dr);

                            }
                            catch (Exception ee)
                            {
                                string message = ee.Message;
                                continue;
                            }
                        }
                    }
                }
            }



        }
        return table;
        //request 3 end
    }

    public string serverSpinUp(string serverDetails, string catalog)
    {
        XmlDocument xm = new XmlDocument();


        xm.LoadXml(serverDetails);
        string url = catalog + "/action/instantiateVAppTemplate";

        byte[] reqBytes = System.Text.UTF8Encoding.UTF8.GetBytes(xm.InnerXml);

        HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
        //  req.ContentLength = reqBytes.Length;
        req.Headers["x-vcloud-authorization"] = token;
        req.ContentType = "application/vnd.vmware.vcloud.instantiateVAppTemplateParams+xml";
        req.Accept = "application/*+xml;version=5.1";

        // req.ContentType = "application/xml";
        req.Method = "POST";


        Stream dataStreamw = req.GetRequestStream();
        dataStreamw.Write(reqBytes, 0, reqBytes.Length);
        string result = "";
        try
        {
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;



            // Get the response stream  
            StreamReader reader = new StreamReader(resp.GetResponseStream());

            // Read the whole contents and return as a string  
            result = reader.ReadToEnd();





            //using (Stream s = resp.GetResponseStream())
            //{
            //    using (StreamReader sr = new StreamReader(s, Encoding.ASCII))
            //    {
            //        // Xml test9 = sr.ReadToEnd();

            //        string results = @sr.ReadToEnd();

            //        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

            //        using (XmlReader reader = XmlReader.Create(url))
            //        {

            //        }



            //        doc.LoadXml(results);







            //    }
            //} //end using

        }
        catch (Exception ee)
        {
            result = ee.Message;
        }


        //return resp.StatusDescription;
        return result;
    }

    public string GetVappGuid(string org, string vapp)
    {

        string url = "http://10.52.121.174:5000/vcd/environments/id/" + org + "/" + vapp + "?format=xml";



        HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
        DataTable table = new DataTable();

        string guid = "";

        HttpWebResponse resp = req.GetResponse() as HttpWebResponse;

        Encoding enc2 = System.Text.Encoding.GetEncoding(1252);
        //     StreamReader loResponseStream2 =
        //new StreamReader(resp2.GetResponseStream(), enc2);
        try
        {
            using (Stream s = req.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s, Encoding.ASCII))
                {
                    // Xml test9 = sr.ReadToEnd();

                    string results = @sr.ReadToEnd();

                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

                    using (XmlReader reader = XmlReader.Create(url))
                    {

                    }


                    doc.LoadXml(results);




                    XmlNodeList elemList = doc.GetElementsByTagName("vApp");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        guid = elemList[i].Attributes["id"].Value;

                    }



                }
            } //end using
        }
        catch (Exception ee)
        {
            string message = ee.Message;
        }

        return guid;
    }


    public string GetVappDetails(string org, string vapp)
    {

        string url = "http://10.52.121.174:5000/vcd/environments/id/" + org + "/" + vapp + "?format=xml";



        HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
        DataTable table = new DataTable();

        string guid = "";

        HttpWebResponse resp = req.GetResponse() as HttpWebResponse;

        Encoding enc2 = System.Text.Encoding.GetEncoding(1252);
        //     StreamReader loResponseStream2 =
        //new StreamReader(resp2.GetResponseStream(), enc2);
        try
        {
            using (Stream s = req.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s, Encoding.ASCII))
                {
                    // Xml test9 = sr.ReadToEnd();

                    string results = @sr.ReadToEnd();

                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

                    using (XmlReader reader = XmlReader.Create(url))
                    {

                    }


                    doc.LoadXml(results);




                    XmlNodeList elemList = doc.GetElementsByTagName("vApp");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        guid = elemList[i].Attributes["id"].Value;

                    }



                }
            } //end using
        }
        catch (Exception ee)
        {
            string message = ee.Message;
        }

        return guid;
    }

    public DataTable GetVappDetailsById(string guid)
    {
        string url = "https://vcd-dn5.pd-cloud.com/api/vApp/" + "vapp-" + guid;



        HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
        DataTable table = new DataTable();
        req.Headers["x-vcloud-authorization"] = token;

        req.Accept = "application/*+xml;version=5.1";

        // req.ContentType = "application/xml";
        req.Method = "GET";

        HttpWebResponse resp = req.GetResponse() as HttpWebResponse;

        Encoding enc2 = System.Text.Encoding.GetEncoding(1252);
        //     StreamReader loResponseStream2 =
        //new StreamReader(resp2.GetResponseStream(), enc2);

        using (Stream s = req.GetResponse().GetResponseStream())
        {
            using (StreamReader sr = new StreamReader(s))
            {
                // Xml test9 = sr.ReadToEnd();
                string results = sr.ReadToEnd();

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();


                doc.LoadXml(results);

                table.Columns.Add("name", typeof(string));
                table.Columns.Add("href", typeof(string));





            }
        }






        return table;
        //request 2 end
    }


    public DataTable GetVmDetailsfromVapp(string org, string vapp)
    {

        DataTable table = new DataTable();
        table.Columns.Add("name", typeof(string));
        table.Columns.Add("href", typeof(string));

        string url = "http://10.52.121.174:5000/vcd/environments/id/" + org + "/" + vapp + "/ALL?format=xml";



        HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;


        HttpWebResponse resp = req.GetResponse() as HttpWebResponse;

        Encoding enc2 = System.Text.Encoding.GetEncoding(1252);
        //     StreamReader loResponseStream2 =
        //new StreamReader(resp2.GetResponseStream(), enc2);
        try
        {
            using (Stream s = req.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s, Encoding.ASCII))
                {
                    // Xml test9 = sr.ReadToEnd();

                    string results = @sr.ReadToEnd();

                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

                    using (XmlReader reader = XmlReader.Create(url))
                    {

                    }


                    doc.LoadXml(results);




                    XmlNodeList elemList = doc.GetElementsByTagName("vApp");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        foreach (XmlNode item in elemList[i].ChildNodes)
                        {
                            DataRow dr = table.NewRow();
                            dr["name"] = item.Attributes["name"].Value;
                            dr["href"] = item.Attributes["id"].Value;
                            table.Rows.Add(dr);
                        }
                    }



                }
            } //end using
        }
        catch (Exception ee)
        {
            string message = ee.Message;
        }


        return table;
        //request 2 end
    }


    public string InsertVappDetails(string taskguid, string vappguid, string vappname, string status,string org,string vms,string network,string netguid)
    {

        DataTable table = new DataTable();
        table.Columns.Add("name", typeof(string));
        table.Columns.Add("href", typeof(string));

        string url = "";
        string taskurl = taskguid;

        if (status == "")
        {
            //url = "http://10.52.121.174:5000/vcd/environments/log/insert/currentStatus/" + taskguid + "," + vappguid + "," + vappname + ",''";
            url = "http://10.52.121.174:5000/vcd/environments/log/insert/vappCreationDetails/" + taskguid + "," + vappguid + "," + vappname + ",''"+","+network+","+netguid;

        }
        else
        {
            //url = "http://10.52.121.174:5000/vcd/environments/log/insert/currentStatus/" + taskguid + "," + vappguid + "," + vappname + "," + status+","+org+","+vms;
            url = "http://10.52.121.174:5000/vcd/environments/log/insert/vappCreationDetails/" + taskguid + "," + vappguid + "," + vappname + "," + status + "," + org + "," + vms+","+network+","+netguid;
        }
        string results = "";

        HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;


        HttpWebResponse resp = req.GetResponse() as HttpWebResponse;

        Encoding enc2 = System.Text.Encoding.GetEncoding(1252);
        //     StreamReader loResponseStream2 =
        //new StreamReader(resp2.GetResponseStream(), enc2);
        try
        {
            using (Stream s = req.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s, Encoding.ASCII))
                {
                    // Xml test9 = sr.ReadToEnd();

                    results = @sr.ReadToEnd();

                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

                    //using (XmlReader reader = XmlReader.Create(url))
                    //{

                    //}


                    doc.LoadXml(results);




                    //XmlNodeList elemList = doc.GetElementsByTagName("vApp");
                    //for (int i = 0; i < elemList.Count; i++)
                    //{
                    //    foreach (XmlNode item in elemList[i].ChildNodes)
                    //    {
                    //        DataRow dr = table.NewRow();
                    //        dr["name"] = item.Attributes["name"].Value;
                    //        dr["href"] = item.Attributes["id"].Value;
                    //        table.Rows.Add(dr);
                    //    }
                    //}



                }
            } //end using
        }
        catch (Exception ee)
        {
            string message = ee.Message;
        }


        return results;
        //request 2 end
    }

    public string GetTaskStatus(string url)
    {
        DataTable table = new DataTable();

        HttpWebRequest req3 = WebRequest.Create(url) as HttpWebRequest;

        req3.Headers["x-vcloud-authorization"] = token;

        req3.Accept = "application/*+xml;version=1.5";

        // req.ContentType = "application/xml";
        req3.Method = "GET";
        string status = "";
        HttpWebResponse resp3 = req3.GetResponse() as HttpWebResponse;

        Encoding enc3 = System.Text.Encoding.GetEncoding(1252);
        //     StreamReader loResponseStream3 =
        //new StreamReader(resp3.GetResponseStream(), enc3);

        using (Stream s = req3.GetResponse().GetResponseStream())
        {
            using (StreamReader sr = new StreamReader(s))
            {
                // Xml test9 = sr.ReadToEnd();
                string results = sr.ReadToEnd();

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();


                doc.LoadXml(results);
                DataSet dsStudent = new DataSet();

                XmlNodeList elemList = doc.GetElementsByTagName("Task");
                
                for (int i = 0; i < elemList.Count; i++)
                {
                    status = elemList[i].Attributes["status"].Value;

                }
            }
        }

        return status;
        //request 3 end



    }


    public string GetVdcDebug(string url)
    {
        //request 3 start

        // string url = "http://localhost:9000/RestServiceImpl.svc/GetAllAppDetails";
        HttpWebRequest req3 = WebRequest.Create(url) as HttpWebRequest;

        req3.Headers["x-vcloud-authorization"] = GetAdminAuthentication();

        req3.Accept = "application/*+xml;version=1.5";

        // req.ContentType = "application/xml";
        req3.Method = "GET";
        DataTable table = new DataTable();
        table.Columns.Add("name", typeof(string));
        table.Columns.Add("href", typeof(string));
        HttpWebResponse resp3 = req3.GetResponse() as HttpWebResponse;

        Encoding enc3 = System.Text.Encoding.GetEncoding(1252);
        //     StreamReader loResponseStream3 =
        //new StreamReader(resp3.GetResponseStream(), enc3);

        string test = "";


        using (Stream s = req3.GetResponse().GetResponseStream())
        {
            using (StreamReader sr = new StreamReader(s))
            {
                // Xml test9 = sr.ReadToEnd();
                string results = sr.ReadToEnd();
                test = results;


                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();


                doc.LoadXml(results);
                DataSet dsStudent = new DataSet();

                XmlNodeList elemList = doc.GetElementsByTagName("Org");
                for (int i = 0; i < elemList.Count; i++)
                {
                    foreach (XmlNode item in elemList[i].ChildNodes)
                    {
                        if (item.Name == "Link")
                        {

                            ListItem litem = new ListItem();
                            try
                            {
                                litem.Text = item.Attributes["name"].InnerText;
                                if (item.Attributes["href"].InnerText.Contains("vdc"))
                                {
                                    litem.Value = item.Attributes["href"].InnerText;

                                    DataRow dr = table.NewRow();
                                    dr["name"] = item.Attributes["name"].InnerText; ;
                                    dr["href"] = item.Attributes["href"].InnerText;
                                    table.Rows.Add(dr);

                                }

                            }
                            catch (Exception ee)
                            {
                                string message = ee.Message;
                                continue;
                            }
                        }
                    }
                }
            }



        }
        return test;
        //request 3 end
    }


}