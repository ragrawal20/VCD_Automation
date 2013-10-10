<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .DropDownList
        {
           /*margin-left: 0px;
           margin-bottom: 0px;*/
            font-size:10pt;
    padding-bottom:4px;
    border:1px solid #d5d5d5;
    text-transform:uppercase;
    vertical-align:middle;
            margin-left: 89px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
<br />
        Choose Org:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:DropDownList ID="ddlOrg" runat="server" 
         AutoPostBack="True" onselectedindexchanged="ddlOrg_SelectedIndexChanged" 
        Height="25px" Width="200px">
     </asp:DropDownList>
     <br />
     <br />

       
        Choose VDC:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList 
        ID="ddlVDC" runat="server"
         AutoPostBack="True" onselectedindexchanged="ddlVDC_SelectedIndexChanged" 
        Height="25px" Width="200px">
     </asp:DropDownList>
    
     <br />

      <br />
        Choose Network:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:DropDownList ID="ddlnetwork" runat="server" 
         AutoPostBack="True" Height="25px" Width="200px">
     </asp:DropDownList>
     <br />

     <br />
        Choose Catalog:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
     <asp:DropDownList ID="ddlCatalog" runat="server" 
         AutoPostBack="True" 
         onselectedindexchanged="ddlCatalog_SelectedIndexChanged" Height="25px" 
        Width="200px">
     </asp:DropDownList>
     <br />
     <br />
        Choose Catalog Item:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:DropDownList ID="ddlcatalogItem" runat="server"  AutoPostBack="True" 
         onselectedindexchanged="ddlcatalogItem_SelectedIndexChanged" 
        Height="25px"  Width="200px">
     </asp:DropDownList>
     <br />
     <br />
        Choose Template:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
     <asp:DropDownList ID="ddlTemplate" runat="server"  
         AutoPostBack="True" Height="25px"  Width="200px">
     </asp:DropDownList>
     <br />
    <br />
   VappName:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtserverName" runat="server" Height="25px" 
        style="margin-left: 7px" Width="200px"></asp:TextBox>
     <br />

     <br />
        <asp:Label ID="lblresult" Visible="false" runat="server" Text=""></asp:Label>
    <br />

     <br />
     VM Choices:
     <br />
        <asp:Button ID="Button2" runat="server" Text="Select All" 
            onclick="Button2_Click" />&nbsp;&nbsp;
             <asp:Button ID="Button3" runat="server" Text="DeSelect All" 
            onclick="Button3_Click" />
        <br />
      <asp:CheckBoxList id="ddlvms" runat="server" Visible="true"></asp:CheckBoxList>
      <br />

    <br />
        <%--VM List:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox 
            ID="txtvms" runat="server" style="margin-left: 7px" Height="25px" Width="200px" ></asp:TextBox>--%>

           <asp:TextBox 
            ID="txtvms" Visible="false" runat="server" style="margin-left: 7px" Height="25px" Width="200px" ></asp:TextBox>
    <br />
    <%--<br />
        <asp:Label ID="lblresult" Visible="false" runat="server" Text=""></asp:Label>
    <br />--%>

    <br />
        <asp:Label ID="lbldebug" Visible="true" runat="server" Text=""></asp:Label>
    <br />

    
   <br />

  
    
     <asp:ListBox id="lbvms" Visible="false" runat="server" SelectionMode="Multiple" Width="140px" 
        style="margin-left: 0px"></asp:ListBox>
  
   
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Create" />
     <br />
</div>
</asp:Content>

