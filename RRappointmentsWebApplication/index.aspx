<%@ Page Language="C#" AutoEventWireup="true" Inherits="index" Codebehind="index.aspx.cs" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>מערכת זימון תורים</title>

<link href="Styles/style1.css" rel="stylesheet" type="text/css" />
<link href="Styles/GlobalStyle.css" rel="stylesheet" type="text/css" />
<link href="Styles/IndexPageStyle.css" rel="stylesheet" type="text/css" />
<style type="text/css">
</style>

</head>

<body>
    <form id="form1" runat="server">
        <div class="index_top">
            <!--div class="index_top_logo">
                <img src="images/logo.png" alt="" width="200" height="75" id="index_top_logo_img" 
                    runat="server" />
            </div                     juju-->
        </div>
        <div class="index_menu">
        </div>
        <div class="index_body">
            <iframe src="step1.aspx" runat="server" scrolling="auto" frameborder="0" id="pageiframe"></iframe>
        </div>

        <div class="index_botom_line">
            <div class="index_RR">
                <a href="http://www.rrsystems.co.il/" target="_blank">
                    <img src="images/RR.png" alt="ר.ר. מערכות" width="25" height="25" border="0" 
                        align="left" />
                </a>
            </div>
Designed by 
R.R. Knowledge System And Technologies LTD.&nbsp;&nbsp;&nbsp;&nbsp; Copyright © All rights reserved. 
        </div>
    </form>
</body>
</html>

