<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostSignUp.aspx.cs" Inherits="RRappointmentsWebApplication.PostSignUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>מערכת זימון תורים</title>

<link href="Styles/style1.css" rel="stylesheet" type="text/css" />
<link href="Styles/GlobalStyle.css" rel="stylesheet" type="text/css" />
<link href="Styles/PostPasswordRecoveryStyle.css" rel="stylesheet" type="text/css" />

<%--<style type="text/css">
</style>--%>

<script type="text/javascript" src="Scripts/DelayRedirectScript.js" language="javascript"></script>
</head>

<body onload="DelayRedirect('index.aspx')";>
    <form id="form1" runat="server">
<div class="index_top">
<%--<div class="index_top_logo"><img src="images/logo.png" alt="" width="200" height="75" /></div>--%>
</div>
<div class="index_menu"></div>
<div class="index_body" style="background-color: #FFFFFF">
    <table bgcolor="White" cellpadding="10px">
        <tr>
            <td class="style1">
            </td>
            <td class="style4"><br />
                <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" ForeColor="#999999" 
                    Text="!שחזור סיסמתך התבצע בהצלחה"></asp:Label><%--"#3399FF"--%>
            </td>
        </tr>
        <tr>
            <td class="style2">
            </td>
            <td class="style5">
                <asp:Label ID="Label2" runat="server"                  
                    Text=".ברגעים אלה נשלחת הודעת טקסט אל הטלפון הנייד שלך ובה סיסמא לכניסתך למערכת" 
                    Font-Bold="True" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style3">
            </td>
            <td class="style6">
                &nbsp;&nbsp; ....המתן, מייד תועבר לדף הכניסה למערכת</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="style7">&nbsp;</td>
        </tr>
    </table>
</div>
<div class="index_botom_line" style="background-color: #FFFFFF" align="left">
<div class="index_RR">
    <a href="http://www.rrsystems.co.il/" target="_blank">
        <img src="images/RR.png" alt="ר.ר. מערכות" width="25" height="25" border="0" align="left" />
    </a>
</div>
Designed by 
R.R. Knowledge System And Technologies LTD.&nbsp;&nbsp;&nbsp;&nbsp; Copyright © All rights reserved. 
</div>
    </form>
</body>
</html>

