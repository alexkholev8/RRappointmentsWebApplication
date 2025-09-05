<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigurationPage.aspx.cs" Inherits="RRappointmentsWebApplication.ConfigurationPage" ClientIDMode="Static" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>
<link href="Styles/style1.css" rel="stylesheet" type="text/css" />
<link href="Styles/GlobalStyle.css" rel="stylesheet" type="text/css" />
<link href="Styles/ConfigurationPageStyle.css" rel="stylesheet" type="text/css" />

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

    <script src="Scripts/Global.js" type="text/javascript" language="javascript"></script>
    <script src="Scripts/ConfigurationPageScript.js" type="text/javascript" language="javascript"></script>


    <style type="text/css">
        .auto-style1 {
            width: 58px;
            text-align: right;
            height: 25px;
        }
    </style>


</head>

<body onload="MM_preloadImages('images/enter_over.gif')">

    <form id="aspnetForm" runat="server" align="center">

    <div class="steps">
        <div class="chosen_step">ניהול הגדרות</div>  
    </div>
    <br />
    <br />
    <br />
    <div class="pages_inner_frame_large">
        <asp:Panel ID="mainPanel" runat="server" align="center"><%-- Height="577px"--%>
            <asp:Panel ID="sitePropertiesPanel" runat="server" Height="107px"><%--Width="98%"--%>
                
                            <table id="sitePropertiesInnerTable" align="center">
                                <tr>
                                    <td class="style6">&nbsp;&nbsp;&nbsp; 
                                        <asp:Label ID="sitePropertiesTitleLabel" runat="server" 
                                            Font-Bold="True" Text="מאפייני אתר"></asp:Label>
                                    </td><td class="style8"></td><td class="style8"></td>
                                </tr>
                                <tr>
                                    <td class="style6">
                                        <asp:CheckBox ID="enableTreatmentGroupCheckBox" runat="server" 
                                            Text="הפעל מיון ע&quot;פ סוגי בדיקות" />
                                    </td>
                                    <td class="style8"></td>
                                    <td class="style8" style="display:inline">
                                        <asp:CheckBox ID="passwordUpdateCheckBox" runat="server" 
                                           Text="משך תוקף סיסמא בימים" />
                                        <div style="display:inline-block; visibility:hidden;" class="style14"></div>
                                        <asp:TextBox ID="txtDaysCount" runat="server" MaxLength="3" Width="30px" Visible="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style6">
                                        <asp:CheckBox ID="appointmentLimitCheckBox" runat="server" 
                                            Text="הגבל מספר זימוני תור" onClick="DisplayAppointmentLimit();" />
                                    </td>
                                    <td class="auto-style1">
                                        <asp:DropDownList ID="appointmentLimitDropDownList" runat="server">
                                        </asp:DropDownList>
                                    </td><td class="style8"></td>
                                </tr>
                                <%--// added on 28/07/2019 for additional filtering of available appointments--%>
                                <tr>
                                    <td class="style6">
                                        <asp:CheckBox ID="dlineFilteringCheckBox" runat="server" 
                                            Text="הפעל סינון לפי dline"/>
                                    </td><td></td><td class="style8"></td>
                                </tr>
                                <%--// end added on 28/07/2019 for additional filtering of available appointments--%>
                            </table>
            </asp:Panel>

            <div style="display:block; height:16px"></div><%--// inserted on 28/07/2019--%>

            <asp:Panel runat="server" ID="wsSettingsPanel">
                
                            <table id="settingsInnerTable" align="center">
                                <tr>
                                    <td class="style6"><%--style16  juju--%>&nbsp;&nbsp;
                                        <asp:Label ID="wsSettingsLabel" runat="server" Font-Bold="True" 
                                            Text="הגדרות WS"></asp:Label>
                                    </td><td></td><td  width="70%"></td>
                                </tr>
                                <tr>
                                    <td class="style6">&nbsp;מיקום WS:</td>
                                    <td align="right">
                                        <asp:DropDownList ID="ddlWebServiceLocation" runat="server" Width="194px" ToolTip="" Enabled="true" BackColor="White">
                                        <asp:ListItem Text="משולב" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="מרוחק" Value="1" Selected="False"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td><td></td>
                                </tr>
                                <tr>
                                    <td class="style6">&nbsp;שם הסיפרייה:</td>
                                    <td align="right">
                                        <asp:TextBox ID="txtWebServiceDirectory" runat="server" Text="WebServices/" 
                                            ReadOnly="true" Width="190px"></asp:TextBox>
                                    </td><td></td>
                                </tr>
                                <tr class="hiddenRow">
                                    <td class="style6">&nbsp;כתובת IP:</td>
                                    <td width="190px" align="right" dir="ltr">
                                        <asp:TextBox ID="wsIPtextBox" runat="server" Width="190px" OnTextChanged="wsIPtextBox_TextChanged"></asp:TextBox>
                                    </td>
                                    <td class="style32">כתובת שירות DISH:</td>
                                </tr>
                                <tr class="hiddenRow">
                                    <td class="style6">&nbsp;פורט:</td>
                                    <td  align="right" dir="ltr">
                                        <asp:TextBox ID="wsPortTextBox" runat="server" Width="46px"></asp:TextBox>
                                    </td>
                                    <td align="right" dir="ltr">
                                        <asp:TextBox ID="wsDISHtextBox" runat="server"></asp:TextBox><%-- Width="386px"--%>
                                    </td>
                                </tr>
                                <%-- // added on 08/08/2013 : // --%>
                                <tr class="newRow">
                                    <td class="style6">&nbsp;אופן ביצוע:</td>
                                    <td  align="right" dir="ltr">
                                        <asp:DropDownList ID="ddlWebServiceSyncMode" runat="server" Width="190px" ToolTip="">
                                            <asp:ListItem Text="סינכרוני" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="אסינכרוני" Value="1" Selected="False"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td  align="right" dir="ltr"></td>
                                </tr>
                                <%-- // end added on 08/08/2013  --%>
                            </table>
            </asp:Panel><%--<br />--%>
            <div style="display:block; height:6px"></div><%--// inserted on 28/07/2019--%>

            <%-- // ADDED ON 14/01/2014 FOR WCF ENDPOINT ADDRESS DEFINITION: // --%>
            <asp:Panel runat="server" ID="WcfConfigPanel">
                <div class="WcfConfig" align="center">
                    <asp:Label ID="lblWCF" runat="server" Text="&nbsp;&nbsp;הגדרות WCF" Height="16px"></asp:Label>
                    <div class="separator"></div>
                    <asp:Label ID="lblWcfAddress" CssClass="WcfConfigLabel" runat="server" Text="מיקום WCF:">
                    </asp:Label>
                    <asp:TextBox ID="txtWcfAddress" CssClass="WcfConfigInput" runat="server" Wrap="false">
                    </asp:TextBox>
                    <br /><br />
                    <asp:CheckBox ID="chkUseWcfDiariesCounterService" runat="server" 
                        CssClass="useWcfDiariesCounter" Text="לאפשר WCF:" TextAlign="Left" 
                        Width="115px" />
                </div>
            </asp:Panel><br />
            <%-- // END OF WCF ENDPOINT ADDRESS DEFINITION SECTION //////////// --%> 

            <%-- // ADDED ON 01/09/2013 FOR E-MAIL SENDING DEFINITIONS: --%>
            <asp:Panel runat="server" ID="emailConfigPanel" align="center"><%-- style="margin-top: 14px"--%>
            <div class="emailConfig" align="center">
                <asp:Label ID="lblConfig" runat="server" Text="הגדרות של דואר האלקטרוני:">
                </asp:Label>
                <div class="separator"></div>
                
                <asp:Label ID="lblFrom" CssClass="emailConfigLabel" runat="server" Text="From:">
                </asp:Label>
                <asp:TextBox ID="txtFrom" CssClass="emailConfigInput" runat="server" Wrap="false">
                </asp:TextBox>
                <%-- BLOCK INSERTED ON 11/02/2020 : //--%>
                <asp:Label ID="lblNickname" CssClass="emailNicknameLabel" runat="server" Text="Nickname:"></asp:Label>
                <asp:TextBox ID="txtNickname" CssClass="emailNicknameInput" runat="server" Wrap="false" Text="Medsoft" Enabled="true" ReadOnly="false" ToolTip="במידה ויש צורך להחליף כתובת של שולח לכינוי נא להקליד פה את שם הכינוי"></asp:TextBox> 
                <div class="Nickname">
                <asp:CheckBox ID="chkNickname" runat="server" Checked="false" Text="Use Nickname" TextAlign="Right"/>
                </div>
                <%-- END BLOCK INSERTED ON 11/02/2020 --%>
                <div class="separator"></div>

                <asp:Label ID="lblTo" CssClass="emailConfigLabel" runat="server" Text="To:">
                </asp:Label>
                <asp:TextBox ID="txtTo" CssClass="emailConfigInput" runat="server" Wrap="false">
                </asp:TextBox>
                <div class="separator"></div>

                <asp:Label ID="lblCc" CssClass="emailConfigLabel" runat="server" Text="Cc:">
                </asp:Label>
                <asp:TextBox ID="txtCc" CssClass="emailConfigInput" runat="server" Wrap="false">
                </asp:TextBox>
                <div class="separator"></div>

                <%-- // ADDED ON 11/09/2013 : ////////////////////////////////////////////////////////// --%>
                <asp:Label ID="lblSubject" CssClass="subjTag" runat="server" Text="Subject:">
                </asp:Label>
                <asp:TextBox ID="txtSubject" CssClass="subjText" runat="server" Wrap="False" 
                    Text="Test Test Test">
                </asp:TextBox>
                <div class="separator"></div>
                <asp:Label ID="lblMessage" CssClass="messageTag" runat="server" Text="Message:">
                </asp:Label>
                <asp:TextBox ID="txtMessage" CssClass="messageText" runat="server" Wrap="true" TextMode="MultiLine"
                    ToolTip="Write your message here">
                </asp:TextBox>
                <div class="separator"></div>
                <%-- // END ADDED ////////////////////////////////////////////////////////////////////// --%>

                <asp:Label ID="lblSmtpServer" CssClass="emailConfigLabel" runat="server" Text="Smtp Server:"></asp:Label>
               
                <asp:TextBox ID="txtSmtpServer" CssClass="emailConfigInput" runat="server" Wrap="false"></asp:TextBox>
               
                <asp:Label ID="lblSmtpPort" runat="server" Text="Port:"></asp:Label>
                
                <asp:TextBox ID="txtSmtpPort" runat="server" Wrap="false" MaxLength="3"></asp:TextBox>
                
                <div class="SSL">
                <asp:CheckBox ID="chkSSL" runat="server" Checked="false" Text="Enable SSL" TextAlign="Right" />
                </div>
                <div class="separator"></div>

                <div id="encrypt" class="Encryption">
                    <asp:CheckBox ID="chkEncrypt" runat="server" Checked="false" Text="Enable Encryption Method" 
                        TextAlign="Right" />
                </div>
                <div id="radio" class="Radio">
                    <asp:RadioButton GroupName="Encrypt" CssClass="rdoButton" runat="server" ID="rdoSSL" Text="SSL" 
                        TextAlign="Right"/>
                    <asp:RadioButton GroupName="Encrypt" CssClass="rdoButton" runat="server" ID="rdoTLS" Text="TLS" 
                        TextAlign="Right" Enabled="false"/>
                </div>
                <div class="separator"></div>
                <div class="login" id="loginBlock">
                    <asp:Label ID="lblUserName" CssClass="lblLogin" runat="server" Text="User Name:" 
                        ToolTip="User account name or e-mail address">
                    </asp:Label>
                    <asp:TextBox ID="txtUserName" CssClass="txtLogin" runat="server" Wrap="false">
                    </asp:TextBox>
                    <div class="separator"></div>
                    <asp:Label ID="lblPassword" CssClass="lblLogin" runat="server" Text="Password:" ToolTip="Password">
                    </asp:Label>
                    <asp:TextBox ID="txtPassword" CssClass="txtLogin" runat="server" Wrap="false" TextMode="Password">
                    </asp:TextBox>
                </div>
                <div class="separator"></div>
                <asp:Button runat="server" OnClick="SendEmail" OnClientClick="return onValidateData();" ID="send" Text="Send Email"/>
                <asp:Label runat="server" ID="lblMailingStatus" CssClass="lblEmailStatus"  Text="" ToolTip="Email Sending Status">
                </asp:Label>
                <div class="separator"></div>

                <asp:Button runat="server" ID="reset" OnClientClick="return resetData();" CausesValidation="false" Text="Reset" />
                <asp:Button runat="server" ID="save" OnClientClick="return checkEmailSettings();" OnClick="SaveEmailSettings" Text="Save Settings"  />
                <div id="preset">
                    <asp:CheckBox runat="server" ID="chkPreset" Checked="false" Text="Use Saved Settings" 
                        ForeColor="#999999" TextAlign="right" OnCheckedChanged="OnLoadPresets" AutoPostBack="true" />    
                </div>
                <asp:Label runat="server" ID="lblPath" CssClass="Path" Text="" Visible="false"></asp:Label>
                <div class="separator"></div>
            </div>
            </asp:Panel>
            <%-- // END ADDED --%>

            <asp:Panel runat="server" ID="bottomPanel" Height="25px">
               
                            <asp:Button ID="saveExitButton" runat="server" 
                                 onclick="saveExitButton_Click" 
                                Text="אישור" Width="104px" CssClass="exitbtn" /><%-- Font-Bold="True" Font-Size="Medium"--%>
                        
            </asp:Panel>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
