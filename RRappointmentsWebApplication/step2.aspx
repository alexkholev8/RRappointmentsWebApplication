<%@ Page Language="C#" AutoEventWireup="true" Inherits="step2" Codebehind="step2.aspx.cs" ClientIDMode="Static"
     %>
     <%--enableEventValidation="false"--%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>

<link rel="stylesheet" type="text/css" href="Styles/GlobalStyle.css" />
<link rel="stylesheet" type="text/css" href="Styles/style1.css" />
<link rel="stylesheet" type="text/css" href="Styles/Step2Style.css" />
<link rel="stylesheet" type="text/css" href="Styles/smartpaginator.css" />
<style type="text/css">    .auto-style1 {
        width: 7px;
        height: 24px;
    }
    .auto-style2 {
        width: 149px;
        height: 24px;
    }
    .auto-style3 {
        height: 24px;
        width: 424px;
    }
    .auto-style4 {
        height: 24px;
    }
    .auto-style5 {
        width: 7px;
        height: 56px;
    }
    .auto-style6 {
        width: 149px;
        height: 56px;
    }
    .auto-style7 {
        height: 56px;
        width: 424px;
    }
    .auto-style8 {
        height: 56px;
    }
    .lineCodeField {
        display:none;
    }
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
<script type="text/javascript" src="Scripts/helpPopupScript.js" language="javascript"></script>
<script type="text/javascript" src="Scripts/GetDaysFromDate.js" language="javascript"></script>
<script type="text/javascript" src="Scripts/AppointmentLogicScript.js" language="javascript"></script>
<script src="Scripts/smartpaginator.js" type="text/javascript"></script>
</head>

<body onload="MM_preloadImages('images/search_over.gif'); GetDaysFromDate();">
<%-- // discarded on 22/08/2013 : //////////////////////////////////////////////////////////// --%>
<%--<body onload="MM_preloadImages('images/search_over.gif'); GetDaysFromDate(); updatePostPrintData();">--%>
<%-- // last function added on 26/06/2013 --%>
<%-- // end discarded //////////////////////////////////////////////////////////////////////// --%>

    <form id="form1" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
        EnableScriptGlobalization="True">
    </asp:ToolkitScriptManager>

<div class="steps">
  <div class="step">1 <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>כניסה</div>  
  <div class="chosen_step">2<%--&nbsp;&nbsp;--%> קביעת מועד</div>
  <div class="step">3 <%--&nbsp;&nbsp;--%>פרטי הביקור</div>
  <div class="step">4 <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%> סיום</div>
</div>
<br />
<br />
<br />
<asp:Panel runat="server" CssClass="pages_inner_frame_larger" 
        ID="pagesInnerFrameLargerPanel" ><%--Width="757px" Height="1116px" ???px --%>
<%--<div class="pages_inner_frame_larger">--%><div class="stephead">
<div class="pages_next">
  <b>שלום&nbsp<asp:Label ID="userNameLabel" runat="server" Text="Label"></asp:Label>
    </b></div></div>
    <asp:Panel runat="server" ID="oldAppointmentsPanel">
 <div style="font-size: 16px; font-weight: bold; margin-right: 47px;">
     <asp:Label ID="oldAppointmentsTilteLabel" runat="server" 
         Text="להלן ביקורים עתידיים שנקבעו עבורך:"></asp:Label>
  
   
     
    </div>
    <table id="oldAppointmentsTable" align="center">
        <tr>
            <td class="style31">
                &nbsp;
            </td>
            <td colspan="2"> 
                &nbsp;
                <asp:GridView ID="oldAppointmentsGridView" runat="server" Width="697px"   
                    AllowPaging="True"  
                    CellPadding="4" ForeColor="#999999" 
                    GridLines="None" PageSize="4" AutoGenerateColumns="False"
                    onpageindexchanging="oldAppointmentsGridView_PageIndexChanging" OnSelectedIndexChanged="oldAppointmentsGridView_SelectedIndexChanged"><%--635px  #333333--%> <%--DataSourceID="ObjectDataSource2"--%>
                    <AlternatingRowStyle BackColor="White" ForeColor="#b5b8b9" /><%--#284775   4E9C9C--%>
                    <%-- // EnableViewState="false"--%>
                    <Columns>

                        <asp:BoundField DataField="AppointmentOperName"  HeaderStyle-HorizontalAlign="Right" HeaderText="שם בדיקה" 
                            SortExpression="AppointmentOperName" />
                        <asp:BoundField DataField="AppointmentLocation" HeaderStyle-HorizontalAlign="Right" HeaderText="מקום הבדיקה" 
                            SortExpression="AppointmentLocation" />
                        <asp:BoundField DataField="AppointmentTime" HeaderStyle-HorizontalAlign="Right" HeaderText="שעה" 
                            SortExpression="AppointmentTime" />
                        <asp:BoundField DataField="AppointmentDate" HeaderStyle-HorizontalAlign="Right" HeaderText="תאריך" 
                            SortExpression="AppointmentDate" />
                        <asp:BoundField DataField="AppointmentDay" HeaderStyle-HorizontalAlign="Right" HeaderText="יום" 
                            SortExpression="AppointmentDay" />
                        <asp:BoundField DataField="LineCode" HeaderStyle-HorizontalAlign="Right" HeaderText="קוד" 
                            SortExpression="LineCode" Visible="true" ControlStyle-CssClass="lineCodeField" 
                            HeaderStyle-CssClass="lineCodeField" ItemStyle-CssClass="lineCodeField"/>
                        <%-- added for test --%>

                        <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton runat="server" AlternateText="ביטול" ImageUrl="Images/icon-cancel.png"
                                ID="cancelAppointment" ToolTip="ביטול" GroupName="appointmentCancelation" 
                                OnClick="cancelAppointment_Click"
                                OnClientClick="if(window.confirm('האם אתה בטוח שברצונך לבטל את התור שנבחר?')){document.getElementById('DeleteAppointment').checked = true;} else{document.getElementById('DeleteAppointment').checked = false; }" />
                        </ItemTemplate>
                            <%--<HeaderStyle HorizontalAlign="Right" BackColor="#e4e6e7" ForeColor="#999999" />--%><%--#82A7FF  B3D9D9--%>
                        </asp:TemplateField>
                        <%-- end added for test --%>
                    </Columns>
                    <EditRowStyle BackColor="#b5b8b9" HorizontalAlign="Right" />
                    <FooterStyle BackColor="#C6C9CA" Font-Bold="True" ForeColor="White" /><%--#5D7B9D    4E9C9C--%>
                    <HeaderStyle BackColor="#e4e6e7" Font-Bold="True" ForeColor="#999999" 
                        HorizontalAlign="Right" /><%--#5D7B9D B3D9D9 White--%>
                    <PagerSettings Mode="NextPreviousFirstLast" 
                        FirstPageText="דף ראשון" 
                        LastPageText="דף אחרון" 
                        NextPageText="&nbsp;הבא  » &nbsp;" 
                        PreviousPageText="&nbsp; «  הקודם&nbsp;" /><%--&gt;&lt;--%>
                    <PagerStyle BackColor="#b5b8b9" ForeColor="White" HorizontalAlign="Center" /><%--#284775   4E9C9C--%>
                    <RowStyle BackColor="#F7F6F3" ForeColor="#999999" /><%--#333333--%>
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#999999" /><%--#333333--%>
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#b5b8b9" /><%--#506C8C     4E9C9C--%>
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="style31">
                &nbsp;
            </td>
            <td class="style33">
                &nbsp;
            </td>
            <td align="left">
            
            <a href="#" onmouseout="MM_swapImgRestore()" 
                    onmouseover="MM_swapImage('print','','images/print_over.gif',1)" dir="rtl">
                <asp:ImageButton ID="print" runat="server" ImageUrl="Images/print.gif"
                    OnClientClick="window.open('PrintOldAppointments.aspx');" />
                    <%-- // discarded on 22/08/2013 --%>
                    <%--OnClientClick="savePagePreprintData(); window.open('PrintOldAppointments.aspx');" />--%>
                    <%-- // end discarded --%>
                </a>&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
<hr align="center" width="600" />
</asp:Panel>

<div runat="server" id="appointmentLimitTitle" visible="false" class="style59">
    <%--<strong></strong>--%>
    <span class="style58">
        <strong> לקוח נכבד, אין באפשרותך כרגע לזמן תורים נוספים.</strong>
    </span>
</div>

<asp:Panel runat="server" ID="mainPanel">
<div style="font-size: 16px; font-weight: bold; margin-right: 47px;"><%--class="pages_next"--%>לחיפוש תור חדש נא למלא את הפרטים הבאים: 
    <br /> <br />
    </div>
    <asp:UpdatePanel  runat="server" ID="updatePanel0" UpdateMode="Conditional">
    <ContentTemplate>
    <table id="dropDownListsTable" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr runat="server" id="treatmentGroupRow"><td class="auto-style1">&nbsp;</td>
            <td class="auto-style2">סוגי בדיקות</td>
            <td class="auto-style3">
                <asp:DropDownList ID="treatmentGroupDropDownList" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="true" ClientIDMode="Static" 
                    Font-Size="14px"  Width="150px" 
                    onselectedindexchanged="treatmentGroupDropDownList_SelectedIndexChanged" TabIndex="1" CssClass="txtBox"><%--Height="25px"--%>
                    <asp:ListItem Selected="True" Text="" Value="0"  ></asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="treatmentGroupErrorLabel" runat="server" CssClass="errorLabelStyle" 
                    ForeColor="#CC0000" Text="יש לבחור את  הבדיקה המבוקשת"></asp:Label>
            </td>
            <td  class="auto-style4">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5">
            </td>
            <td class="auto-style6">הבדיקה המבוקשת</td>
            <td class="auto-style7">
                <%--// AutoPostBack="false" changed to "true" on 18/08/2013 --%>
                <asp:DropDownList ID="operDropDownList" runat="server" 
                    Enabled="true"
                    AppendDataBoundItems="True" 
                    AutoPostBack="true"     
                    ClientIDMode="Static"
                    Font-Size="14px" Width="150px" TabIndex="2" CssClass="txtBox" OnSelectedIndexChanged="operDropDownList_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Text="" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="operErrorLabel" runat="server" CssClass="errorLabelStyle" 
                    ForeColor="#CC0000" Text="יש לבחור את הבדיקה המבוקשת"></asp:Label>
            </td>
            <td class="auto-style8">
                <%--// added for test--%>
            </td>
        </tr>
        <tr>
            <td class="style44">
                &nbsp;</td>
            <td class="style42">מיקום הבדיקה</td>
            <td class="style40">
                <asp:DropDownList ID="siteDropDownList" runat="server" Font-Size="14px" 
                     Width="150px" Enabled="false" TabIndex="3" CssClass="txtBox"><%--Height="25px"--%>
                    <asp:ListItem Selected="True" Text="" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="siteErrorLabel" runat="server" CssClass="errorLabelStyle" 
                    ForeColor="#CC0000" Text="יש לבחור את המרפאה המבוקשת "></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>

<table id="inputDataTable" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
      <td class="style13"></td>
      <td class="style42">גורם מבטח</td><%--"style14"--%>
      <td class="style15">
            <asp:DropDownList ID="insuranceDropDownList" runat="server"
                Enabled="true"
                Font-Size="14px"  Width="150px" TabIndex="4" CssClass="txtBox"><%--Height="25px"--%>
              <asp:ListItem Selected="True" Text="" Value="0"></asp:ListItem>
          </asp:DropDownList>
          <asp:Label ID="insuranceErrorLabel" runat="server" CssClass="errorLabelStyle" 
              ForeColor="Red" Text="יש לבחור את הגורם המבטח"></asp:Label>
        </td>
      <td class="style16">&nbsp;</td>
    </tr>
    <tr>
      <td class="style9"></td>
      <td class="style10">תחום תאריכים מבוקש</td>
             <td class="style11">מתאריך 
                 <asp:TextBox ID="startDateTextBox" runat="server" Width="100px"  
                    Font-Size="14px" ClientIDMode="Static" ReadOnly="false" 
                     onchange="GetDaysFromDate();" onkeypress="return false"  CssClass="txtBox" 
                     TabIndex="5" ><%--Height="20px"--%></asp:TextBox>
                <asp:CalendarExtender ID="startDateTextBox_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="startDateTextBox" 
                     TodaysDateFormat="dd/MM/yyyy" Format="dd/MM/yyyy" ></asp:CalendarExtender>&nbsp;עד תאריך

                <asp:TextBoxWatermarkExtender ID="startDateTextBox_TextBoxWatermarkExtender" 
                    runat="server" Enabled="True" TargetControlID="startDateTextBox" 
                    WatermarkText="לחץ" WatermarkCssClass="waterMarkStyle"></asp:TextBoxWatermarkExtender>
                <asp:TextBox ID="endDateTextBox" runat="server" Width="100px"  onkeypress="return false" 
                    Font-Size="14px" ClientIDMode="Static" ReadOnly="false" 
                     onchange="GetDaysFromDate();"  CssClass="txtBox" TabIndex="6"><%--Height="20px"--%></asp:TextBox>
                <asp:CalendarExtender ID="endDateTextBox_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="endDateTextBox" 
                     TodaysDateFormat="dd/MM/yyyy" Format="dd/MM/yyyy"></asp:CalendarExtender>
                <asp:TextBoxWatermarkExtender ID="endDateTextBox_TextBoxWatermarkExtender" 
                    runat="server" Enabled="True" TargetControlID="endDateTextBox" 
                    WatermarkText="לחץ" WatermarkCssClass="waterMarkStyle"></asp:TextBoxWatermarkExtender>           
            </td>
      <td class="style12">
                <asp:Label ID="dateErrorLabel" runat="server" Text="Label" 
                    ClientIDMode="Static" CssClass="errorLabelStyle"></asp:Label>
            </td>
    </tr>
    <tr>
      <td class="style55"></td>
      <td class="style56"></td>
             <td class="style23">
        </td>
      <td class="style24">
            </td>
    </tr>
    <tr>
      <td>&nbsp;</td>
      <td height="35" class="style54">ימים בשבוע</td>
      <td height="35" class="style4" colspan="2">
          <asp:CheckBox ID="sundayCheckBox" runat="server" Text="ראשון" Checked="True" />&nbsp;&nbsp;
          <asp:CheckBox ID="mondayCheckBox" runat="server" Text="שני" Checked="True" />&nbsp;&nbsp;
          <asp:CheckBox ID="tuesdayCheckBox" runat="server" Text="שלישי" Checked="True" />&nbsp;&nbsp;
          <asp:CheckBox ID="wednesdayCheckBox" runat="server" Text="רביעי" 
              Checked="True" />&nbsp;&nbsp;
          <asp:CheckBox ID="thursdayCheckBox" runat="server" Text="חמישי" 
              Checked="True" />&nbsp;&nbsp;
          <asp:CheckBox ID="fridayCheckBox" runat="server" Text="שישי" Checked="True" />&nbsp;&nbsp;
          <asp:CheckBox ID="saturdayCheckBox" runat="server" Text="שבת" Checked="True" />&nbsp;&nbsp;
          
        </td>
    </tr>
    <tr>
      <td class="style5" colspan="3">
      <table>
      <tr>
      <td class="style57"></td>
      <td>

          <asp:UpdatePanel ID="updatePanel3" runat="server" UpdateMode="Conditional">
              <ContentTemplate>
                  <asp:Label ID="statusLabel" runat="server" BorderStyle="None" Font-Bold="True" 
                      Font-Size="Large" ForeColor="#CC0000" Height="22px" 
                      Text="לא נמצאו תורים פנויים התואמים את נתוני החיפוש" ViewStateMode="Disabled" 
                      Visible="False" Width="365px"></asp:Label>
              </ContentTemplate>
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="search" EventName="Click" />
              </Triggers>
          </asp:UpdatePanel>

          </td>
          </tr>
          </table>
        </td>
      <td class="style8"><a  onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('search','','images/search_over.gif',1)">   
          
          <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
          <asp:ImageButton ID="search" runat="server"
              ImageUrl="~/Images/search.gif" onclick="search_Click" 
                  
                  OnClientClick="if (ValidateDropDownList()==false){ return false;}else{ SetSearchDisplay(); return true;}" 
                  TabIndex="7"   />
          </ContentTemplate>
          </asp:UpdatePanel>

          </a></td>
    </tr>
  </table>
  <hr align="center" width="600" />
        
    <%--<img src="images/search.gif" name="search" width="100" height="25" border="0" id="search" onclick="return search_onclick()" />--%>
 <div class="steps">

<asp:UpdatePanel runat="server" ID="updatePanel5" UpdateMode="Conditional">
    <ContentTemplate>
    <table id="selectionMessageTable">
        <tr>
            <td class="style35">
                <asp:Label ID="selectAppointmentTiltleLabel" runat="server" 
                    Text="בחרו את המועד המתאים:"></asp:Label>
            </td>
            <td class="style36">
                <asp:Image ID="loaderImage" runat="server" 
                    ImageUrl="~/Images/ajax-loader.gif" ClientIDMode="Static" /><%--  Height="27px"  Width="37px"--%>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="search" EventName="Click" />
        </Triggers>
</asp:UpdatePanel>

     <br />
    </div>
    <%--// START AVAILABLE APPOINTMENTS GRID VIEW HERE --%>
    <%--//--%>
    <%--<div align="center">--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">   
        <ContentTemplate>
    <%--//--%>
            <asp:GridView ID="availablesAppointmentsGridView" runat="server"
                AllowPaging="True" AutoGenerateColumns="False"
                DataKeyNames="dline" Width="697px" Height="38px"  Align="right" GridLines="None" 
                EnablePersistedSelection="true"
                CaptionAlign="Right" PageSize="8" ShowHeaderWhenEmpty="true"
                Font-Bold="False" Font-Size="Medium" ClientIDMode="Static" BorderColor="#CCCCCC"

                EnableSortingAndPagingCallbacks="true"
                onrowcreated="availablesAppointmentsGridView_RowCreated"
                onpageindexchanging="availablesAppointmentsGridView_PageIndexChanging"

                EnableViewState="false">
                <%--EnableViewState="true"--%> 

                <Columns>
                    <asp:BoundField DataField="dline" HeaderText="dline" SortExpression="dline" Visible="false">
                        <HeaderStyle BackColor="#e4e6e7" ForeColor="#999999" /><%--#82A7FF     B3D9D9--%>
                    </asp:BoundField>
                                            
                    <asp:TemplateField  HeaderText="בחירה">
                        <ItemTemplate>
                            <asp:RadioButton runat="server" 
                                onclick="SelectOne(this);"
                                GroupName="choice" Value='<%# Eval("dline") %>' /> <%--onclick="SelectOne(this,'availablesAppointmentsGridView')"--%>
                        </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" BackColor="#e4e6e7" ForeColor="#999999" /><%--#82A7FF  B3D9D9--%>
                        </asp:TemplateField>

                    <asp:BoundField  DataField="dDate" HeaderText="תאריך" SortExpression="תאריך">
                        <HeaderStyle HorizontalAlign="Right" BackColor="#e4e6e7" ForeColor="#999999" /><%--#82A7FF  B3D9D9--%>
                    </asp:BoundField>

                    <asp:BoundField  DataField="dtime" HeaderText="שעה" SortExpression="שעה">
                        <HeaderStyle HorizontalAlign="Right" BackColor="#e4e6e7" ForeColor="#999999" /><%--#82A7FF  B3D9D9--%>
                    </asp:BoundField>
                    
                    <asp:BoundField  DataField="Dday" HeaderText="יום" SortExpression="יום">
                        <HeaderStyle HorizontalAlign="Right" BackColor="#e4e6e7" ForeColor="#999999" /><%--#82A7FF  B3D9D9--%>
                    </asp:BoundField>
                </Columns>
                
                <FooterStyle  BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" /><%--#82FAFF Double   2px--%>
                <HeaderStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" /><%--#94332C  Ridge  2px--%>
                <PagerSettings Mode="NextPreviousFirstLast"
                    FirstPageText="לעמוד הראשון" 
                    LastPageText="לעמוד האחרון" 
                    NextPageText="&nbsp;הבא  » &nbsp;" 
                    PreviousPageText="&nbsp; «  הקודם&nbsp;" />
                <%--<PagerStyle Font-Size="Smaller" />--%>
                <PagerStyle CssClass="Pager" ForeColor="White" BackColor="#b5b8b9" HorizontalAlign="Center" /><%--#284775  4E9C9C--%>
                <%--<RowStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />--%><%--#94332C 2px--%>
                <%--<SelectedRowStyle BorderColor="#CC0000" />--%><%--Red--%>
                <%--<SortedAscendingHeaderStyle BorderColor="#CC0000" />--%>
            </asp:GridView>
    <%--//--%>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="search" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel><%--</div>--%>
    <%--//--%>
    <%--// END AVAILABLE APPOINTMENTS GRID VIEW HERE --%>
    <div id="smart_paginator" runat="server" enableviewstate="false" clientidmode="Static"></div>   <%--// added for GridView pagination--%>

    <table id="nextButtonTable">
        <tr>
            <td align="right" class="style32">  
                <div class="next_button"><a onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('next','','images/next_over.gif',1)">
        <asp:ImageButton ID="next" runat="server"  ImageUrl="~/Images/next.gif" 
            OnClientClick="return checkUserChoice();" CausesValidation="False" 
                        onclick="next_Click" CssClass="not_visible" 
            /></a></div>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Panel>
    </asp:Panel>
    <br />

    <asp:HiddenField ID="selectedGridRadioHidden" runat="server" 
        ClientIDMode="Static" Value="0" ViewStateMode="Enabled" />
    <asp:HiddenField ID="preprintPageDataHidden" runat="server" 
        ClientIDMode="Static" Value="0" ViewStateMode="Enabled" />
    <asp:CheckBox runat="server" AutoPostBack="true" ID="DeleteAppointment" Checked="false" Text="Delete Appointment" Visible="true" Style="display:none" />
    </form>
</body>
</html>

