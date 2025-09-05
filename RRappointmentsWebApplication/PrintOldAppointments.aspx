<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintOldAppointments.aspx.cs" Inherits="RRappointmentsWebApplication.PrintOldAppointments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/style1.css" rel="stylesheet" type="text/css" />
    <link href="Styles/GlobalStyle.css" rel="stylesheet" type="text/css" />
    <link href="Styles/PrintOldAppointmentsStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css"></style>

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
    <!-- end added -->
   
    <script type="text/javascript" language="javascript">   // added on 25/06/2013
    
    $().ready(function () {

        // getOldAppointments();    // discarded on 20/08/2013

    });  // end document.ready section

    //var DIR = "WebServices/";
    var FILE = "Appointments.asmx";    // "Appointments.asmx/";

    function getOldAppointments() {
        // var service = DIR + FILE + "GetOldAppointments"; // for local (built-in) services only
        // var data = "{}";  // commented on 18/07/2013
        var service = GetServicePath(isRemote, directory, ipAddress, port, FILE, "GetOldAppointments");
        var data = "{ jsonId_Num:'" + userId + "', jsonFileNum:'" + fileNum + "' }";    // corrected on 18/07/2013
        callAjaxGridView(service, data, '#oldAppointmentsGridView');
    }

    function callAjaxGridView(service, data, grid) {
        jQuery.support.cors = true; // Mandatory! Added for cross-browther support

        $.ajax({    // start ajax section
            url: service,
            data: data, // single parameter pass
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            cache: false,
            async: true,
            crossDomain: true,      // Added for cross-browther support
            processData: false,
            success: function (data) {
                fillGridView(grid, data.d);
            },  // end success function (data)

            error: function (xhr, status, error) { // start error fuction
                //alert("Ajax completion gave " + status + ":\n" + error);
                var errDescr = (xhr.responseText != undefined && !String.isNullOrEmpty(xhr.responseText)) ?
                    /*JSON.parse*/(xhr.responseText) : null;
                onAjaxError(xhr, status, error, errDescr);
                // writeLog(errDescr, xhr.statusText, error);   // optional
            }   // end error function
        }); // end ajax part
    }

    function fillGridView(grid, data) {
        if (data != null && data != undefined) {
            var len = data.length;
            if (len != 0) {
                $(grid + " tr:has(td)").remove();
                for (var i = 0; i < len; i++) {
                    $(grid).append("<tr><td>" + data[i].AppointmentTime +
                "</td><td>" + data[i].AppointmentDate +
                "</td><td>" + data[i].AppointmentLocation +
                "</td><td>" + data[i].AppointmentDay +
                "</td></tr>");
                }
            }
        }
        else alert("Failed to fill " + grid);
    }

    </script>
     

</head>

<body onload="window.print();" ><br /><div align="center">
    <form id="form1" runat="server" dir="rtl">
        <div align="center" style="font-weight: bold; font-size: large;">להלן תורים עתידיים שנקבעו עבורך:</div><br />
        
        <asp:GridView ID="oldAppointmentsGridView" runat="server" Width="635px"  
            AutoGenerateColumns="False" Height="141px"><%--DataSourceID="ObjectDataSource2"--%> 
            <Columns>
                <%--
                <asp:BoundField DataField="dtime" HeaderText="שעה" SortExpression="dtime">
                </asp:BoundField>
                <asp:BoundField DataField="ddate" HeaderText="תאריך" SortExpression="ddate">
                </asp:BoundField>
                <asp:BoundField DataField="c_branch" HeaderText="מיקום" SortExpression="c_branch">
                </asp:BoundField>
                <asp:BoundField DataField="day" HeaderText="יום" SortExpression="day">
                </asp:BoundField>
                --%>

                <asp:BoundField DataField="AppointmentOperName" HeaderText="שם בדיקה" 
                    SortExpression="AppointmentOperName" />
                <asp:BoundField DataField="AppointmentLocation" HeaderText="מקום הבדיקה" 
                    SortExpression="AppointmentLocation" />
                <asp:BoundField DataField="AppointmentTime" HeaderText="שעה" 
                    SortExpression="AppointmentTime" />
                <asp:BoundField DataField="AppointmentDate" HeaderText="תאריך" 
                    SortExpression="AppointmentDate" />
                <asp:BoundField DataField="AppointmentDay" HeaderText="יום" 
                    SortExpression="AppointmentDay" />
                <%-- // added in 11/11/2013 --%>
                <asp:BoundField DataField="Instruction" HeaderText="הנחיות לבדיקה"
                    SortExpression="Instruction" />
                <%-- // end added ///////// --%>
            </Columns>
            <PagerSettings FirstPageText="דף ראשון" LastPageText="דף אחרון" NextPageText="הבא" 
                PreviousPageText="קודם" />
        </asp:GridView>
    
        <br />

        <%--<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="Select" 
            
            TypeName="RRappointmentsWebApplication.BuisnessLogicLayer.oldAppointmentsDataSource">
        </asp:ObjectDataSource>--%>
    
    </form></div>
</body>
</html>
