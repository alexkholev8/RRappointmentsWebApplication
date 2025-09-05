<%@ Page Language="C#" AutoEventWireup="true" Inherits="step4" Codebehind="step4.aspx.cs" ClientIDMode="Static" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>
<link rel="stylesheet" type="text/css" href="Styles/GlobalStyle.css" />
<link rel="stylesheet" type="text/css" href="Styles/style1.css" />
<link rel="stylesheet" type="text/css" href="Styles/Step4Style.css" />

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
    <%--<script src="Scripts/TreatmentInstructionsLogicScript.js" type="text/javascript" language="javascript"></script>--%>
<!-- end added -->
<%--<script type="text/javascript">
</script>--%>
</head>

<body onload="MM_preloadImages('images/print_over.gif')">
    <form id="form1" runat="server">
<div class="steps">
    <div class="step">1 <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>כניסה</div>  
    <div class="step">2<%--&nbsp;&nbsp;--%> קביעת מועד</div>
    <div class="step">3 <%--&nbsp;&nbsp;--%>פרטי הביקור</div>
    <div class="chosen_step">4<%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%> סיום</div>
</div>
<br />
<br />
<br />
<asp:Panel runat="server" CssClass="pages_inner_frame_larger" 
        ID="pagesInnerFrameLargerPanel" Width="755px" Height="590px"><%--//458px--%>
<div id ="ltr"></div>
<%--<div class="steps" runat="server" >--%>   <div  align="right" style="font-size: 16px; font-weight: bold; width: 200px; margin-right: 60px;">להלן פרטי הביקור שנקבעו:</div><br /><br />

    <div class="arrow" align="left" style="display: block; clear: both; float: left; right: auto; margin-left: 25px;">
        <img src="images/arrow.gif" alt="" height="200" />
    </div>

<%--<div class="pages_next" align="center">--%>

<table id="tblContent" border="0" align="center" cellpadding="0" cellspacing="0" style="display: inline-table; float: right; ">
  <tr>
      <td class="style2"><%--&nbsp;&nbsp;--%>&nbsp;&nbsp;שם הלקוח:</td>
      <td class="style1">
          <asp:Label ID="nameLabel" runat="server" Text="Label"></asp:Label>
      </td>
      </tr>
  <tr>
      <td class="style2"><%--&nbsp;&nbsp;--%>&nbsp;&nbsp;סוג הבדיקה:</td>
      <td class="style1">
          <asp:Label ID="treatLabel" runat="server" Text="Label"></asp:Label>
      </td>
      </tr>
    <tr>
      <td class="style2"><%--&nbsp;&nbsp;--%>&nbsp;&nbsp;בתאריך:</td>
      <td class="style1">
          <asp:Label ID="dateLabel" runat="server" Text="Label"></asp:Label>
        </td>
      </tr>
    <tr>
      <td class="style2"><%--&nbsp;&nbsp;--%>&nbsp;&nbsp;ביום:</td>
      <td class="style1">
          <asp:Label ID="dayLabel" runat="server" Text="Label"></asp:Label>
        </td>
      </tr>
    <tr>
      <td class="style2"><%--&nbsp;&nbsp;--%>&nbsp;&nbsp;בשעה:</td>
      <td class="style1">
          <asp:Label ID="timeLabel" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
      <td class="style2"><%--&nbsp;&nbsp;--%>&nbsp;&nbsp;במקום:</td>
      <td class="style1">
          <asp:Label ID="siteLabel" runat="server" Text="Label"></asp:Label>
      </td>
    </tr>
    <tr>
      <td class="style2"><%--&nbsp;&nbsp;--%>&nbsp;&nbsp;גורם מבטח:</td>
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

    <table id="tblInstruction">
        <tr>
            <td class="style4">
            </td>
            <td class="style4">
                <asp:Label ID="intructionTitleLabel" runat="server" Text="הנחיות לבדיקה"
                    Font-Bold="True"></asp:Label>
            </td>
            <td class="style4">
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="treatmentInstructionsTextBox" runat="server" BorderStyle="None" 
                    Height="92px" Width="550px" Enabled="False" EnableTheming="False" Font-Bold="True"
                    ReadOnly="True" TextMode="MultiLine" BackColor="White">
                </asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        </table>

&nbsp;
    <div class="pages_next" align="center">
        <table id="tblActions">
            <tr>
                <td class="style3">
                    <div class="back_button"><%-- // div inserted on 11/11/2013 --%>
                        <a onmouseout="MM_swapImgRestore()" 
                            onmouseover="MM_swapImage('back','','images/back_over.gif',1)">
                            <asp:ImageButton runat="server" ID="back"  ImageUrl="Images/back.gif"  width="100" ToolTip="חזרה לשלב של קביעת תור"
                                height="25" onclick="back_Click" />
                        </a>
                    </div><%-- // end div inserted --%>
                </td>
                <td class="style3"><%-- // class added on 11/11/2013 --%>
                <%-- // added on 29/08/2013 --%>
                    <div class="end_button">
                        <a href="#" onmouseout="MM_swapImgRestore()" 
                            onmouseover="MM_swapImage('End','','images/end_over.gif',1)">
                            <asp:ImageButton runat="server" width="100" height="25" ID="End" ToolTip = "יציאה ממערכת לזימון תורים"
                                ImageUrl="Images/end.gif" 
                                OnClientClick="if(top!=self) {top.location.href = 'PostAppointment.aspx';}"
                                onclick="end_Click" />
                                <%--OnClientClick="window.open('PostAppointment.aspx');"--%> 
                       </a>
                   </div>
                <%-- // end added --%>
                </td>
                <td class="style3"><%-- // class added on 11/11/2013 --%>
                    <div class="next_button">
                        <a href="#" onmouseout="MM_swapImgRestore()" 
                            onmouseover="MM_swapImage('print','','images/print_over.gif',1)">
                            <asp:ImageButton runat="server" width="100" height="25"  id="print" ToolTip="הדפסת פרטי הביקור שנקבע"
                                ImageUrl="Images/print.gif" OnClientClick="window.open('PrintAppointment.aspx');" 
                                onclick="print_Click" />
                       </a>
                   </div>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <div class="fin_message" runat="server">
                        <div class="separator"></div>
                        <span id="finalBlackMessage" runat="server" style="color:black; text-align:center">
                            תורך נקבע ואושר.
                        </span><br/>
                        <span id="finalRedMessage" runat="server" style="text-align:right">
                            לסיום ויציאה ממערכת לזימון תורים נא ללחוץ על כפתור 'סיום'.
                        </span><br/>
                        <span id="finalBlueMessage" runat="server" style="color:blue; text-align:right">
                            לחזרה לשלב 2 וקביעת תור חדש נא ללחוץ על כפתור 'אחורה'.
                        </span><br/>
                        <div id="finalMessage" runat="server" style="display:none">
                        ניתן להדפיס הנחיות לבדיקה מתוך מסך קביעת מועד בדיקה – ביקורים עתידיים.
                        </div>
                        <%--<asp:TextBox ID="finalMessage" runat="server" BackColor="White" 
                            BorderStyle="None" Enabled="False" EnableTheming="false" Font-Bold="True" 
                            Height="82px" ReadOnly="True" Text="לחיצה על כפתור 'סיום' מהווה אישור קבלת וקריאת הנחיות לבדיקה.
ניתן להדפיס הנחיות לבדיקה מתוך מסך קביעת מועד בדיקה – ביקורים עתידיים." TextMode="MultiLine" 
                            Width="550px" Wrap="true">
                        </asp:TextBox>--%>
                        <br/>
                        <asp:Label runat="server" ForeColor="Red" Visible="False" Height="16px" ID="WcfWarning" Font-Names="Arial" Font-Size="Small"></asp:Label>
                    </div>
                </td>
            </tr>
        </table>
    </div>

    </asp:Panel>

</form>
</body>
</html>

