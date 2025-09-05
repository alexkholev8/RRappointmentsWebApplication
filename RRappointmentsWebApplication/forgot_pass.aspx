<%@ Page Language="C#" AutoEventWireup="true" Inherits="forgot_pass" Codebehind="forgot_pass.aspx.cs" ClientIDMode="Static" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>

<link rel="stylesheet" type="text/css" href="Styles/GlobalStyle.css" />
<link rel="stylesheet" type="text/css" href="Styles/style1.css" />
<link rel="stylesheet" type="text/css" href="Styles/ForgotPasswordStyle.css" />

<style type="text/css">
</style>

<script type="text/javascript">
</script>
<!-- added on 30/05/2013 for Web Services implementation -->
    <%--// old links:        
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.1/themes/base/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/ui/1.9.1/jquery-ui.js" type="text/javascript"></script>
    
    // new links:
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js" type="text/javascript"></script>
    <script src="https://github.com/douglascrockford/JSON-js/blob/master/json2.js" type="text/javascript"></script>
--%>
    <%--// links from the project itself--%>
    <link href="Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/juery-1.8.2.js" type="text/javascript"></script>
    <script src="Scripts/juery-ui.js" type="text/javascript"></script>
    <script src="Scripts/json2.js" type="text/javascript"></script>
    <%--// end links from the project itself--%>

    <script src="Scripts/Global.js" type="text/javascript"></script>
<!-- end added -->
    <script type="text/javascript" src="Scripts/ForgotPasswordValidationScript.js" language="javascript"></script>
</head>

<body onload="MM_preloadImages('images/send_over.gif')">
    <form id="form1" runat="server">
<br />
<br />
<br />
    <div class="pages_inner_frame_forgot">
    <div class="forgot_text">נא להקיש את מספר תעודת הזהות שלך כפי שרשום במערכת.<br />
        סיסמתך למערכת תישלח אליך בהודעת טקסט לטלפון הנייד הרשום במערכת ולכתובת הדואר 
        האלקטרוני
        במידה וקיימת.</div>
    <table border="0" align="center" 
        cellpadding="0" cellspacing="0" >
        <tr>
            <td height="50" valign="bottom" class="style3"><strong>מספר תעודת זהות</strong></td>
            <td height="50"  valign="bottom" class="style5">
                <asp:TextBox ID="idTextBox" runat="server" ClientIDMode="Static" MaxLength="9" 
                    Width="128px" CssClass="txtBox" TabIndex="1" align="right"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1" colspan="2">
                <asp:Label ID="idErrorLabel" runat="server" CssClass="errorLabelStyle" 
                    ClientIDMode="Static">error</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style4">
            <br />
                » <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/step1.aspx">חזור לדף הכניסה</asp:LinkButton>
            </td>
            <td valign="bottom" class="style6">
                <a onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('send','','images/send_over.gif',1)">
                    <asp:ImageButton ID="send" runat="server" ImageUrl="~/Images/send.gif" 
                        width="100" height="25" align="center" onclick="send_Click"
                        onclientclick="return ForgotPasswordValidate();" TabIndex="2" />
                </a>&nbsp;
           </td>
        </tr>
    </table>
&nbsp;
</div>
</form>
</body>
</html>
