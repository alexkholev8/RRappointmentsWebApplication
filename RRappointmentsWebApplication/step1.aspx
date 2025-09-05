<%@ Page Language="C#" AutoEventWireup="true" Inherits="step1" Codebehind="step1.aspx.cs" Async="true" ClientIDMode="Static" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<%--<meta http-equiv="refresh" content="10" />--%>
<%--<meta http-equiv="refresh" content="10;url=step1.aspx" />--%>
<title>Untitled Document</title>

<link rel="stylesheet" type="text/css" href="Styles/GlobalStyle.css" />
<link rel="stylesheet" type="text/css" href="Styles/style1.css" />
<link rel="stylesheet" type="text/css" href="Styles/Step1Style.css" />
<link href="Styles/tips.css" rel="stylesheet" type="text/css" />

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
<script type="text/javascript">
</script>

 <script type="text/javascript"  src="Scripts/LogInValidationScript.js" language="javascript"></script>
 <script type="text/javascript"  src="Scripts/helpPopupScript.js" language="javascript"></script>

    

</head>

<body onload="MM_preloadImages('images/enter_over.gif')">

 <form id="aspnetForm" runat="server">

<div class="steps">
 <div class="chosen_step">1 <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%> כניסה</div>  
  <div class="step">2 <%--&nbsp;&nbsp;--%> קביעת מועד</div>
  <div class="step">3 <%-- &nbsp;&nbsp;--%> פרטי הביקור</div>
  <div class="step">4 <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%> סיום</div>
</div>
<br />
<br />
<br />
<div class="pages_inner_frame_large">
    <br />






<table id="mainTable" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="35" class="style3"><strong>
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager1" />  <%--what is it?  juju--%>
        מספר תעודת זהות</strong></td>                                                                                          
    <td height="35" align="left" class="style27" >
        <asp:TextBox ID="IdTextBox"  runat="server" Width="128px"  TabIndex="1" onkeypress="return DisableEnterKeyPopUp() ;"  
            MaxLength="9" ViewStateMode="Disabled" CssClass="txtBox"></asp:TextBox>
        
        <a class='support-hover' tabindex="-1" style="margin-top: 0px; vertical-align:middle;" onmouseover="return getText('userIdTip');"><em>?</em>
            <span class='tip' id="userIdTip" style="direction:rtl;">
           
            </span>
        </a>        
      </td>                                                                                                     
      <%--//aa(); SetHelpPopUp('userIdQuestionImageButton'); return false;--%>
    <td height="35" align="left">
        <%--&nbsp;--%>
    </td>
  </tr>
  <tr>
    <td class="style4"><strong>סיסמא</strong></td>
    <td align="left" class="style27" >
        <asp:TextBox ID="passwordTextBox" runat="server" Width="128px" onkeypress="return DisableEnterKeyPopUp() ;"
            TextMode="Password" TabIndex="2" MaxLength="6" ViewStateMode="Disabled" 
            EnableViewState="False" CssClass="txtBox"></asp:TextBox>
        <%--<asp:ImageButton ID="passwordQuestionImageButton" runat="server" 
            ImageUrl="~/Images/icon_question.gif" 
             onmouseover="return SetHelpPopUp('passwordQuestionImageButton');" OnClientClick="return false;"
            TabIndex="-1" ToolTip="לחץ להסבר" ViewStateMode="Disabled" />--%>
       <%-- <ajaxToolkit:PopupControlExtender ID="passwordQuestionImageButton_PopupControlExtender" 
            runat="server"  PopupControlID="fieldInfoPopUpPanel" 
            OffsetX="-175" OffsetY="-25"
            TargetControlID="passwordQuestionImageButton">
        </ajaxToolkit:PopupControlExtender>--%>

        <a class='support-hover' tabindex="-1" style="margin-top: 0px; vertical-align:middle;" onmouseover="return getText('userPasswordTip');"><em>?</em>
            <span class='tip' id="userPasswordTip" style="direction:rtl;">
            
            </span>
        </a> 
      </td>                                                                                                  
    <td align="left" class="style2">
        &nbsp;</td>
    </tr>
    <tr>
    <td class="style3">&nbsp;</td>
    <td dir="rtl" align="left" class="style27" rowspan="3">
       
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"><%--UpdateMode="Conditional"--%>
            <ContentTemplate>
            <%--<table style="width: 22px">--%>
            <table id="inUpdatePanelTable">
            <tr>
            <td class="style26">&nbsp;</td>
                <td class="style22" align="right">
                    <asp:Label ID="logInErrorLabel" runat="server" ClientIDMode="Static" 
                        CssClass="errorLabelStyle" Width="200px">errorlabel</asp:Label>
                    
                </td>
            </tr>
                <tr>
                    <td class="style26">
                        </td>
                    <td align="right" class="style22">
                        <a dir="ltr" onmouseout="MM_swapImgRestore()" 
                            onmouseover="MM_swapImage('logInImageButton','','images/enter_over.gif',1)">
                        <asp:ImageButton ID="logInImageButton" runat="server" border="0" height="25" 
                            ImageUrl="~/Images/enter.gif" name="enter" onclick="logInImageButton_Click" 
                            OnClientClick="return Validate();" TabIndex="3" width="100" />
                        </a>
                    </td>
                </tr>
            </table>
                
            </ContentTemplate>
            <%--<Triggers>
                <asp:AsyncPostBackTrigger ControlID="logInImageButton" EventName="Click" />
            </Triggers>--%>
        </asp:UpdatePanel>
       
        <br />
       
        </td>
    <td dir="rtl" align="center">
       
        &nbsp;</td>
    </tr>
  <tr>
    <td class="style19"></td>
    <td align="left" valign="bottom" class="style20">
       
        </td>
    </tr>
  <tr>
    <td class="style28"></td>
    <td class="style29">
       
        </td>
  </tr>
  <tr>
    <td height="35" class="style3">» <a href="forgot_pass.aspx" tabindex="4">שכחתי סיסמה</a></td>
    <td height="35" class="style27">
       
        &nbsp;</td>
    <td height="35">
       
        &nbsp;</td>
    </tr>
  <tr>
    <td height="35" class="style3">» <a href="new_user.aspx" tabindex="5">משתמש חדש</a></td>
    <td height="35" class="style27">
                &nbsp;</td>
    <td height="35">&nbsp;</td>
    </tr>
</table>














<asp:HiddenField ID="sessionInfoHidden" runat="server" />    <%--// added on 04/07/2013--%>

</div>

        <%--<asp:Panel ID="fieldInfoPopUpPanel" runat="server" 
             BackImageUrl="~/Images/popup_panel.png" 
     class="~popup_questionmark_info" Height="77px" Width="222px">  --%>   
             <%--<table style="height: 51px">--%>
             <%--<table id="inPopUpPanelTable">
             <tr>
             <td class="style15"></td>
             <td class="style11"> --%>
                     <%--style="font-size: medium; font-weight: bold;">--%>
                <%-- <asp:Label ID="helpPopupTitleLabel" runat="server" Font-Bold="True" 
                     Font-Size="Medium" ForeColor="#FFFF66"></asp:Label>
             </td>
             <td class="style9" align="left">
             &nbsp;<a onclick="CloseHelpPopup(); return false;">X</a>
             </td>
             </tr>
             <tr>
             <td class="style18"></td>
                 <td class="style17">
                 <asp:Label ID="helpPopupContentLabel" runat="server"></asp:Label>
                 </td>
                 <td class="style18"></td>
             </tr>
      
                
                 <tr>
                     <td class="style14">
                         &nbsp;</td>
                     <td class="style13">
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
      
                
             </table>    
            </asp:Panel>--%>

    </form>
</body>
</html>

