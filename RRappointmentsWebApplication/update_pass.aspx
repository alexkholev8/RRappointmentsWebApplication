<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="update_pass.aspx.cs" Inherits="RRappointmentsWebApplication.update_pass" ClientIDMode="Static" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Untitled Document</title>
    <link rel="stylesheet" type="text/css" href="Styles/GlobalStyle.css" />
    <link rel="stylesheet" type="text/css" href="Styles/style1.css" />

    <style type="text/css">
        .auto-style1 {
            height: 18px;
        }
    </style>

    <script type="text/javascript">
    </script>

    <link href="Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/juery-1.8.2.js" type="text/javascript"></script>
    <script src="Scripts/juery-ui.js" type="text/javascript"></script>
    <script src="Scripts/json2.js" type="text/javascript"></script>
    <script src="Scripts/Global.js" type="text/javascript"></script>
</head>

<body style="background-color:lightgray">
    
    <form id="form1" runat="server" >
    <div style="display:block; background-color:white; border:1px; clear:both;">
        <br />
        <br />
        <table border="0" align="center" cellpadding="0" cellspacing="0" dir="rtl" bgcolor="White" frame="border" runat="server">
        <tr>
            <td></td>
            <td colspan="3" valign="middle" align="center">
                <br />
                <asp:Label ID="lblWarning" runat="server" Text="Label" Font-Bold="True" Font-Size="Larger" ClientIDMode="Static"></asp:Label>
            </td>
            <td>
                <br />
                <asp:Image ID="Image1" runat="server" ImageAlign="AbsMiddle" Visible="true" AlternateText="Warning" ImageUrl="~/Images/warning.png" />
            </td>
        </tr>
        <tr>
            <td colspan="5" valign="middle">
                <br />
                <div style="visibility:hidden; display:block;"></div>
            </td>
        </tr>
        <tr>
            <td></td>
            <td valign="middle" align="center">
            <br />
                <asp:Button ID="btnOK" runat="server" Font-Bold="true" Font-Size="Larger" Text="אישור" OnClick="btnOK_Click" />
            </td>
            <td valign="middle">
                <br />
                <div style="width:200px; visibility:hidden; display:block;"></div>
            </td>
            <td valign="middle" align="center">                
            <br />
                <asp:Button ID="btnCancel" runat="server" Font-Bold="true" Font-Size="Larger" Text="ביטול" OnClick="btnCancel_Click" />
           </td>
            <td></td>
        </tr>
            <tr>
            <td colspan="5" valign="middle">
                <br />
                <div style="visibility:hidden; display:block;"></div>
            </td>
        </tr>
    </table>

    <br />
    <br />
        
    </div>
    </form>
</body>
</html>
