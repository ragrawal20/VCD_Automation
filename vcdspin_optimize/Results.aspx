﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Results.aspx.cs" Inherits="Results" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
   <tr>
   <td>
       <asp:Label ID="lblresult" runat="server" Text=""></asp:Label>
   </td>
   </tr>
   <tr>
   <td>
       <asp:Button ID="Button1" runat="server" Text="Back<<" onclick="Button1_Click" 
           style="height: 26px" />
   </td>
   </tr>
    </table>
    </div>
    </form>
</body>
</html>
