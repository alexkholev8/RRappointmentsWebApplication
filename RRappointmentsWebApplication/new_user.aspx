<%@ Page Language="C#" AutoEventWireup="true" Inherits="new_user" Codebehind="new_user.aspx.cs"  Async="true"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<%@ Register assembly="MSCaptcha" namespace="MSCaptcha" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>

<link rel="stylesheet" type="text/css" href="Styles/GlobalStyle.css" />
<link rel="stylesheet" type="text/css" href="Styles/style1.css" />
<link rel="stylesheet" type="text/css" href="Styles/NewUserStyle.css" />
<link href="Styles/tips.css" rel="stylesheet" type="text/css" />
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
<!-- end added -->
<script type="text/javascript">
</script>
 <script type="text/javascript" src="Scripts/helpPopupScript.js"   language="javascript"></script>
 <script type="text/javascript" src="Scripts/SignUpValidationScript.js"   language="javascript"></script>
 <script type="text/javascript" src="Scripts/TranslationLogic.js"   language="javascript"></script>
 <script type="text/javascript" src="Scripts/SmsAgreementScript.js"  language="javascript"></script>
 
</head>

<body onload="MM_preloadImages('images/end_over.gif'); SmsAgreementCheckedChanged();">
    <form class="form_align" id="form1" runat="server"><ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <div class="stephead"><%--class="steps"--%>משתמש חדש</div>
    <%--<br />
    <br />--%>

<div class="pages_inner_frame_large">
<div id="ltr"></div>
    <%-- beginning of table 1 --%>
    <table id="table1" border="0" align="center" cellpadding="0" cellspacing="0" >
        <tr>
            <td class="persdata">שם פרטי</td>
            <td  class="style23"><%--height="35"--%><%-- align="left"--%>
                <asp:TextBox ID="firstNameTextBox" runat="server" CssClass="txtBox" TabIndex="1"></asp:TextBox>
                <ajaxToolkit:TextBoxWatermarkExtender ID="firstNameTextBox_TextBoxWatermarkExtender" 
                    runat="server" Enabled="True" TargetControlID="firstNameTextBox" WatermarkText="שדה חובה" 
                    WatermarkCssClass="waterMarkStyle">
                </ajaxToolkit:TextBoxWatermarkExtender>
            </td>
            <td  align="right" class="style5"></td>
            <td class="style7">&nbsp;
                <asp:Label ID="firstNameErrorLabel" runat="server" ClientIDMode="Static" CssClass="errorLabelStyle" Text="שדה זה הוא שדה חובה"></asp:Label>
            </td>
            <%--<td>&nbsp;</td>
            <td>&nbsp;</td>--%>
        </tr>
        <tr>
            <td class="persdata">שם משפחה</td>
            <td  class="style23"><%--height="35" align="left"--%>
                <asp:TextBox ID="lastNameTextBox" runat="server" CssClass="txtBox" TabIndex="2"></asp:TextBox>
                <ajaxToolkit:TextBoxWatermarkExtender ID="lastNameTextBox_TextBoxWatermarkExtender" 
                    runat="server" Enabled="True" TargetControlID="lastNameTextBox"  WatermarkText="שדה חובה" 
                    WatermarkCssClass="waterMarkStyle">
                </ajaxToolkit:TextBoxWatermarkExtender>
            </td>
            <td align="right" class="style5"></td>
            <td class="style7">&nbsp;
                <asp:Label ID="lastNameErrorLabel" runat="server" ClientIDMode="Static" 
                    CssClass="errorLabelStyle" Text="שדה זה הוא שדה חובה" ViewStateMode="Enabled"></asp:Label>
            </td>
            <%--<td>&nbsp;</td>
            <td>&nbsp;</td>--%>
        </tr>
        <tr>
            <td class="persdata">מספר תעודת זהות</td>
            <td class="style23"><%--height="35" align="left" --%>
                <asp:TextBox ID="idTextBox" runat="server" MaxLength="9" CssClass="txtBox"  onkeypress="return DisableEnterKeyPopUp();" TabIndex="3"></asp:TextBox>
                <ajaxToolkit:TextBoxWatermarkExtender ID="idTextBox_TextBoxWatermarkExtender" 
                    runat="server" Enabled="True" TargetControlID="idTextBox"  WatermarkText="שדה חובה" 
                    WatermarkCssClass="waterMarkStyle">
                </ajaxToolkit:TextBoxWatermarkExtender>
            </td>
            <td align="right" class="style5"><%--&nbsp;
                <asp:ImageButton ID="userId2QuestionImageButton" runat="server" 
                    ImageUrl="~/Images/icon_question.gif" 
                    onmouseover="return SetHelpPopUp('userId2QuestionImageButton');"
                    OnClientClick="return false;"
                    TabIndex="-1" ToolTip="לחץ להסבר" />
                <ajaxToolkit:PopupControlExtender ID="userId2QuestionImageButton_PopupControlExtender" 
                    runat="server" Enabled="True" PopupControlID="fieldInfoPopUpPanel"
                    TargetControlID="userId2QuestionImageButton" OffsetX="-175" OffsetY="-25">
                </ajaxToolkit:PopupControlExtender>--%>
                <a class='support-hover' tabindex="-1" style="margin-top: 0px; vertical-align:middle;" onmouseover="return getText('userId2Tip');"><em>?</em>
                    <span class='tip' id="userId2Tip" style="direction:rtl;">
            
                    </span>
               </a> 
            </td>
            <td class="style7">&nbsp;
                <asp:Label ID="idErrorLabel" runat="server" ClientIDMode="Static" CssClass="errorLabelStyle" Text="שדה זה הוא שדה חובה"></asp:Label>
            </td>
           <%-- <td>&nbsp;</td>
            <td>&nbsp;</td>--%>
        </tr>
        <tr>
            <td class="persdata">כתובת דואר אלקטרוני</td>
            <td  class="style23"><%--height="35" align="left"--%>
                <asp:TextBox ID="emailTextBox" runat="server" onKeyUp="ChangeEbrewToEnglish(this.id)" 
                    CssClass="emailTextBoxStyle"
                    onkeypress="return DisableEnterKeyPopUp();" TabIndex="4"></asp:TextBox>
            </td>
            <td align="right" class="style5"><%--&nbsp;
                <asp:ImageButton ID="emailQuestionImageButton" runat="server" 
                    ImageUrl="~/Images/icon_question.gif" 
                    onmouseover="return SetHelpPopUp('emailQuestionImageButton');" 
                    OnClientClick="return false;" 
                    TabIndex="-1" ToolTip="לחץ להסבר" />
                <ajaxToolkit:PopupControlExtender ID="emailQuestionImageButton_PopupControlExtender" 
                    runat="server" Enabled="True" PopupControlID="fieldInfoPopUpPanel"
                    TargetControlID="emailQuestionImageButton" OffsetX="-175" OffsetY="-25">
                </ajaxToolkit:PopupControlExtender>--%>
                <a class='support-hover' tabindex="-1" style="margin-top: 0px; vertical-align:middle;" onmouseover="return getText('userEmailTip');"><em>?</em>
                    <span class='tip' id="userEmailTip" style="direction:rtl;">
            
                    </span>
                </a> 
            </td>
            <td class="style7">&nbsp;
                <asp:Label ID="emailErrorLabel" runat="server" ClientIDMode="Static" CssClass="errorLabelStyle" Text="שדה זה הוא שדה חובה"></asp:Label>
            </td>
            <%--<td>&nbsp;</td>
            <td>&nbsp;</td>--%>
        </tr>
        <tr>
            <td class="persdata">מספר טלפון נייד</td><%--class="style31"--%>
            <td class="style32" align="right"><%-- align="left"--%>
                <%-- beginning of inner table --%>
                <%--<table  align="right" id="innerTable">
                    <tr>
                        <td class="style23">%--class="style16"--%>
                            <asp:TextBox ID="phoneTextBox" runat="server" Width="98px" align="right" MaxLength="7" 
                                CssClass="ltrTextBox" onkeypress="return DisableEnterKeyPopUp();" 
                                TabIndex="6"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="phoneTextBox_TextBoxWatermarkExtender" 
                                runat="server" Enabled="True" TargetControlID="phoneTextBox"
                                WatermarkText="שדה חובה" WatermarkCssClass="waterMarkStyle">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                        <%--</td>
                        <td>--%>
                            <asp:DropDownList ID="areaCodeDropDownList" runat="server" align="left" CssClass="txtBox" 
                                TabIndex="5">
                            <asp:ListItem>050</asp:ListItem>
                            <asp:ListItem>052</asp:ListItem>
                            <asp:ListItem>053</asp:ListItem>
                            <asp:ListItem>054</asp:ListItem>
                            <asp:ListItem>055</asp:ListItem>
                            <asp:ListItem>057</asp:ListItem>
                            <asp:ListItem>058</asp:ListItem>
                            </asp:DropDownList>
                      <%--  </td>
                    </tr>
                </table>--%>
                <%-- end of inner table --%>
            </td>
            <td align="right" class="style5">
               <%-- &nbsp;<asp:ImageButton ID="phoneQuestionImageButton" runat="server" 
                    ImageUrl="~/Images/icon_question.gif" 
                    onmouseover="return SetHelpPopUp('phoneQuestionImageButton');" 
                    OnClientClick="return false;"
                    TabIndex="-1" ToolTip="לחץ להסבר" />
                <ajaxToolkit:PopupControlExtender ID="PopupControlExtender" 
                    runat="server" Enabled="True" PopupControlID="fieldInfoPopUpPanel"
                    TargetControlID="phoneQuestionImageButton" OffsetX="-175" OffsetY="-25">
                </ajaxToolkit:PopupControlExtender>--%>
                <a class='support-hover' tabindex="-1" style="margin-top: 0px; vertical-align:middle;" onmouseover="return getText('userPhoneTip');"><em>?</em>
                    <span class='tip' id="userPhoneTip" style="direction:rtl;">
            
                    </span>
                </a> 


            </td>
            <td class="style7">&nbsp;
                <asp:Label ID="phoneErrorLabel" runat="server" ClientIDMode="Static" CssClass="errorLabelStyle" Text="שדה זה הוא שדה חובה"></asp:Label>
            </td>
            <%--<td class="style31"></td>
            <td class="style31"></td>--%>
        </tr>
        <tr>
            <td  class="style35" colspan="4"><%--width="150"--%>
                <input runat="server" id="smsAgreementCheckBox" type="checkbox" class="chkBox" onchange="SmsAgreementCheckedChanged();" tabindex="7" />
                <asp:Label ID="smsAgreement" runat="server" Text="הנני מאשר קבלת הודעות SMS"></asp:Label><%-- Width="200px"--%>
               
            </td>          
                       
                
           <%-- <td width="150" class="style35"></td>
            <td class="style35"></td>--%>
        </tr>
        <tr>
            <td colspan="4" height="35px">
                <asp:Label ID="popupUnblockLabel" runat="server" Visible="false" CssClass="popupUnblock" Text="לצורך קבלת SMS יש לבטל את חוסם חלונות קופצים לאתר זה"></asp:Label><%--Width="450px"--%><%--added visible=false--%>
            </td>
            <%--<td width="150" class="style35"></td>
            <td class="style35"></td>--%>
        </tr>
    </table>
    <%-- end of table 1 --%>
   <br />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" align="center">
        <ContentTemplate>
        
       
            <%-- beginning of table 2 --%>
            <%--<table id="table2">
                <tr>
                    <td class="style27">&nbsp;</td>
                    <td class="style30"><br />--%> 
                    
                     <div class="ftrError" align="center">
                        <asp:Label ID="signUpErrorLabel" runat="server" CssClass="errorLabelStyle"></asp:Label>
                    </div>
                    <%--</td>
                    <td class="style28" align="left">&nbsp;</td>
                </tr>
                <tr>
                    <td class="style29">&nbsp;</td>
                    <td class="style8" colspan="2" rowspan="2" align="left">--%> <br />
                    <div class="ftr1" align="right">
                        » <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/step1.aspx">חזור לדף הכניסה</asp:LinkButton>
                    
                    
                      <%--  <a onmouseout="MM_swapImgRestore()" 
                            onmouseover="MM_swapImage('submitSignUp','','images/end_over.gif',1)">
                        </a>--%>
                    </div>    
                        <div class="ftr2" align="left">
                        <a dir="ltr" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('submitSignUp','','images/end_over.gif',1)">
                            <asp:ImageButton ID="submitSignUp" runat="server" border="0" height="25" 
                                ImageUrl="~/Images/end.gif" name="enter" onclick="submitSignUp_Click"  
                                OnClientClick="return SignUpValidate();" TabIndex="8" width="100" />
                        </a>
                        </div>
                    <%--</td>
                </tr>
                <tr>
                    <td class="style29"> --%>
                     <%--<div>
                        <asp:LinkButton ID="LinkButton2" runat="server" 
                            PostBackUrl="~/step1.aspx">חזור לדף הכניסה</asp:LinkButton>
                            </div>--%>
                    <%--</td>
                </tr>
            </table>--%>
            <%-- end of table 2 --%>
            
        </ContentTemplate>
        <%--// added on 18/08/2013 --%>
        <Triggers>
            <asp:PostBackTrigger ControlID="submitSignUp" />
        </Triggers>
        <%--// end added --%>
    </asp:UpdatePanel>

   <%--  <asp:Panel ID="fieldInfoPopUpPanel" runat="server" BackImageUrl="~/Images/popup_panel.png" 
        class="popup_questionmark_info" Height="140px" Width="222px">
        beginning of table 3 
        <table id="table3">
            <tr>
                <td class="style22"></td>
                <td class="style22" id="helpPopupTitleCell">
                    <asp:Label ID="helpPopupTitleLabel" runat="server" Font-Bold="True"
                        Font-Size="Medium" ForeColor="#FFFF66" Text="עזרה בתעודת זהות">
                    </asp:Label>
                </td>
                <td class="style22" align="left">&nbsp;
                    <a onclick="CloseHelpPopup(); return false;">X</a>
                </td>
            </tr>
            <tr>
                <td></td>
                <td id="helpPopupContentCell">
                    <asp:Label ID="helpPopupContentLabel" runat="server" 
                        Text="יש להזין מספר תעודת זהות כולל ספרת ביקורת">
                    </asp:Label>
                </td>
                <td></td>
            </tr>
        </table>
        <%-- end of table 3  
    </asp:Panel>--%>

</div>
</form>
</body>
</html>

