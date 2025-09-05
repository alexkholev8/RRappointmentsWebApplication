$().ready(function () {

    // added on 17/02/2020 : /////
    $('#end').click(function () {

        $(this).hide();
        $(this).css('visibility', 'hidden');

        //$('#end').hide();
        //$('#end').css('visibility', 'hidden');
    });
    // end added on 17/02/2020 ///
}); // end document.ready section

//var DIR = "WebServices/";
var FILE = "NewAppointment.asmx";  // "NewAppointment.asmx/";
var isSentEmail = false;

function addNewAppointment() {
    var date = $('#dateLabel').text();
    // var service = DIR + FILE + "AddNewAppointment";  // for local (built-in) services only
    var service = GetServicePath(isRemote, directory, ipAddress, port, FILE, "AddNewAppointment");
    var data = "{ jsonDate:'" + date +
              "', jsonCustCode:" + parseInt(custCode, 10) +
              ", jsonSiteCode:" + parseInt(siteCode, 10) +
              ", jsonOperCode:" + parseInt(treatCode, 10) +
              ", jsonInsrCode:" + parseInt(insurCode, 10) +
              ", jsonDline:" + parseInt(dline, 10) + " }";
    callAjax(service, data);
}

function callAjax(service, data) {
    jQuery.support.cors = true; // Mandatory! Added for cross-browther support

    $.ajax({    // start ajax section
        url: service,
        data: data, // single parameter pass
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        cache: false,
        async: true, // false, //('true' was changed to 'false')
        crossDomain: true,      // Added for cross-browther support
        processData: false,
        success: function (data) {
            onSuccess(data.d);
        },  // end success function (data)

        error: function (xhr, status, error) { // start error fuction
            //alert("Ajax completion gave " + status + ":\n" + error);
            var errDescr = (xhr.responseText != undefined && !String.isNullOrEmpty(xhr.responseText)) ?
                /*JSON.parse*/(xhr.responseText) : null;
            onAjaxError(xhr, status, error, errDescr);
            // writeLog(errDescr, xhr.statusText, error);   // optional
        }   // end error function
    });   // end ajax part
}

function onSuccess(data) {
    if (data == null) {
        alert("Failed to add new appointment.");
    }
    else {
        $('#end').hide();
        // Redirect from client part:
        // var url = "step4.aspx";
        // $(location).attr('href', url);
        // end redirect from client part
        sendAppointmentEmail(data);
    }
}

function sendAppointmentEmail(data) {
    var serviceUrl = "http://reports.y-it.co.il:8080/reports/ws_send_email";
    serviceUrl += "?" + "@as_smtp_sender=" + data.SenderEmail;
    serviceUrl += "&" + "@as_smtp_sender_name=" + data.EmailSubject;
    serviceUrl += "&" + "@as_recipient=" + data.UserEmail;
    serviceUrl += "&" + "@as_subject=" + data.EmailSubject;
    serviceUrl += "&" + "@as_details=" + data.EmailMessage;
    serviceUrl += "&" + "@as_content_type=" + data.ContentType;
    var serviceType = "POST";
    var serviceData = "{}";

    callAjaxSendEmail(serviceData, serviceUrl, serviceType);
}

function callAjaxSendEmail(serviceData, serviceUrl, serviceType) {
    jQuery.support.cors = true;     // Mandatory! Added for cross-browther support

    $.ajax({    // start ajax section
        url: serviceUrl,
        data: serviceData,
        dataType: "html",           //"json", - NOT JSON!
        type: serviceType,
        contentType: "text/html",   //"application/json; charset=utf-8", - NOT JSON!
        crossDomain: true,          // Optional. Added for cross-browther support
        cache: false,
        async: false, /*true,*/
        success: function (data) {
            onSendEmailSuccess(data);
        },  // end success function (data)

        error: function (xhr, status, error) { // start error fuction
            alert("Ajax completion gave " + status + ":\n" + error);
        }   // end error function
    });     // end ajax section
}

function onSendEmailSuccess(data) {
    isSentEmail = true;
    // alert(data); // TEST
}

// TEST
function onEndClick() {
    
    $('#end').hide();
    $('#end').css('visibility', 'hidden');
}
// END TEST