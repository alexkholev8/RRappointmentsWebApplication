<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintAppointment.aspx.cs" Inherits="RRappointmentsWebApplication.PrintAppointment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="Styles/PrintAppointmentStyle.css" rel="stylesheet" type="text/css" />
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
    <script type="text/javascript" language="javascript">
    </script>

</head>

<body onload="window.print()">
    <form id="form1" runat="server">
        <div class="pages_next" dir="rtl"><br />
        <div class="style2">
להלן פרטי התור שנקבע לך:
        </div><br />
        <table id="mainTable" border="0" align="center" cellpadding="1" cellspacing="10">
            <tr>
                <td class="style4"><strong>שם הלקוח:</strong></td>
                <td class="style1">
                    <asp:Label ID="nameLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4"><strong>סוג הבדיקה:</strong></td>
                <td class="style1">
                    <asp:Label ID="treatLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4"><strong>בתאריך:</strong></td>
                <td class="style1">
                    <asp:Label ID="dateLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4"><strong>ביום:</strong></td>
                <td class="style1">
                    <asp:Label ID="dayLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4"><strong>בשעה:</strong></td>
                <td class="style1">
                    <asp:Label ID="timeLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4"><strong>במקום:</strong></td>
                <td class="style1">
                    <asp:Label ID="siteLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4"><strong>גורם מבטח:</strong></td>
                <td class="style1">
                    <asp:Label ID="inuranceLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4"><strong>כתובת:</strong></td>
                <td class="style1">
                    <asp:Label ID="addressLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4"><strong>טלפון:</strong></td>
                <td class="style1">
                    <asp:Label ID="telephoneLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table><br />

&nbsp;<asp:Label ID="Label1" runat="server" Text="הנחיות לבדיקה"></asp:Label>
        <br />
        <br />
        <table id="instructionTable" align="center">
            <tr>
                <td>&nbsp;</td>
                <td class="style3">
                    <asp:Label ID="instructionLabel" runat="server" Text="Label"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="style3">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="style3">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
