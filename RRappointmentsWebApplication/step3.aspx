<%@ Page Language="C#" AutoEventWireup="true" Inherits="step3" Codebehind="step3.aspx.cs" ClientIDMode="Static" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>
<link rel="stylesheet" type="text/css" href="Styles/GlobalStyle.css" />
<link rel="stylesheet" type="text/css" href="Styles/style1.css" />
<link rel="stylesheet" type="text/css" href="Styles/Step3Style.css" />
<style type="text/css">
</style>

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
    <script src="Scripts/NewAppointmentLogicScript.js" type="text/javascript" language="javascript"></script>
<!-- end added -->

<script type="text/javascript">
</script>
</head>

<body onload="MM_preloadImages('images/end_over.gif','images/back_over.gif')">
    <form id="form1" runat="server">
<div class="steps">
   <div class="step">1 <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>כניסה</div>  
  <div class="step">2<%-- &nbsp;&nbsp;--%> קביעת מועד</div>
  <div class="chosen_step">3 <%--&nbsp;&nbsp;--%>פרטי הביקור</div>
  <div class="step">4<%-- &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%> סיום</div>
</div>
<br />
<br />
<br />
<div class="pages_inner_frame_large" style="height: 400px;">
        <div id="ltr"></div>
<div  align="right" style="font-size: 16px; font-weight: bold; width: 200px; margin-right: 60px;">להלן פרטי הביקור שקבעת:
    
    </div><%--class="steps"--%><br /><br />
<div class="arrow"><img src="images/arrow.gif" alt="" height="200" align="right" /></div>

<table border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
      <td class="style2">&nbsp;&nbsp;<%--&nbsp;&nbsp;--%>שם הלקוח:</td>
      <td class="style1">
          <asp:Label ID="nameLabel" runat="server" Text="Label"></asp:Label>
      </td>
      </tr>
  <tr>
      <td class="style2">&nbsp;&nbsp;<%--&nbsp;&nbsp;--%>סוג הבדיקה:</td>
      <td class="style1">
          <asp:Label ID="treatLabel" runat="server" Text="Label"></asp:Label>
      </td>
      </tr>
    <tr>
      <td class="style2">&nbsp;&nbsp;<%--&nbsp;&nbsp;--%>בתאריך:</td>
      <td class="style1">
          <asp:Label ID="dateLabel" runat="server" Text="Label"></asp:Label>
        </td>
      </tr>
    <tr>
      <td class="style2">&nbsp;&nbsp;<%--&nbsp;&nbsp;--%>ביום:</td>
      <td class="style1">
          <asp:Label ID="dayLabel" runat="server" Text="Label"></asp:Label>
        </td>
      </tr>
    <tr>
      <td class="style2">&nbsp;&nbsp;<%--&nbsp;&nbsp;--%>בשעה:</td>
      <td class="style1">
          <asp:Label ID="timeLabel" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
      <td class="style2">&nbsp;&nbsp;<%--&nbsp;&nbsp;--%>במקום:</td>
      <td class="style1">
          <asp:Label ID="siteLabel" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
      <td class="style2">&nbsp;&nbsp;<%--&nbsp;&nbsp;--%>גורם מבטח:</td>
      <td class="style1">
          <asp:Label ID="inuranceLabel" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
      <td class="style2">&nbsp;&nbsp;<%--&nbsp;&nbsp;--%>כתובת:</td>
      <td class="style1">
          <asp:Label ID="addressLabel" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
      <td class="style2">&nbsp;&nbsp;<%--&nbsp;&nbsp;--%>טלפון:</td>
      <td class="style1">
          <asp:Label ID="telephoneLabel" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
</table>
    <div align="center"><asp:Label ID="LockErrorMessage" runat="server" ForeColor="Red" Text="" Visible="false" Font-Bold="true" Font-Size="Medium"></asp:Label></div>
<br /> 
<div class="tip" style="font-size:medium; color:black; width: 490px;"> 
    <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>נא ודא שפרטי התור נכונים ולחץ על &quot;סיום&quot; על מנת לאשר את זימון התור.<br />
    <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;--%>לחץ על &quot;אחורה&quot; כדי לחזור למסך הקודם. 
    </div> 
    <br />
<div class="pages_next">
  <div class="next_button"><a onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('end','','images/end_over.gif',1)">
      <asp:ImageButton runat="server" ImageUrl="images/end.gif" name="end" 
          width="100" height="25" border="0" id="end" OnClick="end_Click" 
          CssClass="end_button" OnClientClick="onEndClick();" /></a></div> 
    <%-- // OnClientClick="addNewAppointment();" OnClientClick="onEndClick();" --%>
  <div class="back_button"><a onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('back','','images/back_over.gif',1)">
      <asp:ImageButton runat="server" src="images/back.gif" name="back" width="100" 
          height="25" border="0" id="back" onclick="back_Click" /></a></div>
</div>
</div>
    </form>
</body>
</html>
