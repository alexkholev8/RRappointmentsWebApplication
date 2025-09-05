// This file was modified since 30/05/2013
// added on 30/05/2013: ==================
$().ready(function () {

    updateErrorLabelView('#idErrorLabel', "");

    //    $('#send').click(function () {
    //        var userId = $('#idTextBox').val();
    //        if (userId != null && userId != undefined)
    //            recoverUserPassword(userId);
    //    });

    // ADDED FOR TEST
    //$('#send').click(function () {
    //    ForgotPasswordValidate();
    //    $('#<%=send.ClientID %>').prop("disabled", true);
    //    $('#<%=send.ClientID %>').attr('disabled', true);
    //    $('#send').prop('disabled', true);
    //    $('#send').attr('disabled', true);
    //});
    // END ADDED FOR TEST

});   // end document.ready section

//var DIR = "WebServices/";
var FILE = "PasswordRecovery.asmx"; // "PasswordRecovery.asmx/";
var isSentSMS = false;
var isSentEmail = false; 

// CODE CORRECTIONS SINCE 19/06/2013 : ////////////////////////////////////////////////////////////
// #REGION PASSWORD RECOVERY
function recoverUserPassword(userId) {
    var data = "{ jsonUserID:'" + userId + "' }";
    // var service = DIR + FILE + "OnRecoverUserPassword";  // for local (built-in) services only
    var service = GetServicePath(isRemote, directory, ipAddress, port, FILE, "OnRecoverUserPassword");
    var lblError = '#idErrorLabel';
    callAjax(service, data, lblError);
}

function callAjax(service, data, lblError) {
    jQuery.support.cors = true;     // Mandatory! Added for cross-browther support

    $.ajax({    // start ajax section
        url: service,
        data: data,
        dataType: "json",  // TEST
        //jsonp : '$callback',    // TEST
        type: "POST",
        contentType: "application/json; charset=utf-8",
        // ADDED 4 TEST : /////////////////////////////////
//        headers: { "Content-Type": "application/json",
//                   "Accept": "application/json",
//                   "X-Requested-With": "XMLHttpRequest" },
        // END TEST ///////////////////////////////////////
        crossDomain: true,  // Added for cross-browther support
        cache: false,
        async: true,
        // ADDED 4 TEST : /////////////////////////
        beforeSend: function(xhr) {
            xhr.setRequestHeader("Content-type", 
                "application/json; charset=utf-8");
        },
        // END ADDED 4 TEST ///////////////////////

        success: function (data) {

            onRecoverUserPassword(data.d, lblError);        // added on 30/06/2013

        },  // end success function (data)

        error: function (xhr, status, error) { // start error fuction
            //alert("Ajax completion gave " + status + ":\n" + error);
            var errDescr = (xhr.responseText != undefined && !String.isNullOrEmpty(xhr.responseText)) ?
                /*JSON.parse*/(xhr.responseText) : null;
            onAjaxError(xhr, status, error, errDescr);
            // writeLog(errDescr, xhr.statusText, error);   // optional
        }   // end error function
    });         // end ajax section
}

function onRecoverUserPassword(data, lblError) {
    var message = "";
    if (data != null) {
        if (String.isNullOrEmpty(data.ClientPassword)) {
            message = "מספר תעודת הזהות שהקשת לא קיים במערכת";
        }
        else {
            sendUserPasswordByPhone(data);
            if (!String.isNullOrEmpty(data.ClientEmail))
                sendUserPasswordByEmail(data);
            if (isSentSMS/* && isSentEmail*/) { // email is optional
                if (top != self) {
                    top.location.href = 'PostPasswordRecovery.aspx'
                }
            }
            else {
                alert("שגיאה קריטית של אייאקס");
            }
        }
    }
    else {
        message = "אין אפשרות לספק את השירות כרגע. נא נסה שנית במועד מאוחר יותר.";
    }
    
    updateErrorLabelView(lblError, message);
}

function sendUserPasswordByPhone(data) {
    var serviceUrl = "http://wap.y-it.co.il:8080/wapdb/ws_send_sms";
    serviceUrl += "?" + "CellNumber=" + data.ClientCellNumber;
    serviceUrl += "&" + "MessageString=" + data.RecoveryMsgSMS;
    serviceUrl += "&" + "SenderCellNumber=" + data.SenderCellNumber;
    serviceUrl += "&" + "UserName=" + data.SenderName;
    serviceUrl += "&" + "Password=" + data.SenderPassword;
    var serviceType = "GET";
    var serviceData = "{}";

    callAjaxPasswordRecovery(serviceData, serviceUrl, serviceType, true);
}

function sendUserPasswordByEmail(data) {
    var serviceUrl = "http://reports.y-it.co.il:8080/reports/ws_send_email";
    serviceUrl += "?" + "@as_smtp_sender=" + data.SenderEmail;
    serviceUrl += "&" + "@as_smtp_sender_name=" + data.SenderEmailTitle;
    serviceUrl += "&" + "@as_recipient=" + data.ClientEmail;
    serviceUrl += "&" + "@as_subject=" + data.SenderEmailTitle;
    serviceUrl += "&" + "@as_details=" + data.RecoveryMsgEmail;
    serviceUrl += "&" + "@as_content_type=" + data.ContentType;
    var serviceType = "POST";
    var serviceData = "{}";

    callAjaxPasswordRecovery(serviceData, serviceUrl, serviceType, false);
}

function callAjaxPasswordRecovery(serviceData, serviceUrl, serviceType, isSMS) {
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

            if (isSMS) {
                isSentSMS = true;
            }
            else {
                isSentEmail = true;
            }
            onPasswordRecoverySuccess(data);

        },  // end success function (data)

        error: function (xhr, status, error) { // start error fuction
            //alert("Ajax completion gave " + status + ":\n" + error);
            var errDescr = (xhr.responseText != undefined && !String.isNullOrEmpty(xhr.responseText)) ?
                /*JSON.parse*/(xhr.responseText) : null;
            onAjaxError(xhr, status, error, errDescr);
            // writeLog(errDescr, xhr.statusText, error);   // optional
        }   // end error function
    });     // end ajax section
}

function onPasswordRecoverySuccess(data) {
    // alert(data); // TEST
}

// #ENDREGION PASSWORD RECOVERY

// #REGION VALIDATION
function ForgotPasswordValidate() {
    var errorFound = false;
    updateErrorLabelView('#idErrorLabel', '');  // added on 01/07/2013

    if (validateRequierdField('#idTextBox', '#idErrorLabel') == false) {
        errorFound = true;
    }
    else {
        if (validateId('#idTextBox', '#idErrorLabel', 'מספר תעודת הזהות שהקשת אינו תקין') == false) {
            errorFound = true;
        }
    }
    
    if (errorFound) return false;
    // DisplayWaitMessage();
    // ADDED FOR TEST : /////
    //document.getElementById("<%=send.ClientID %>").disabled = true;
    //document.getElementById("send").disabled = true;
    //$('#<%=send.ClientID %>').prop('disabled', true);
    //$('#<%=send.ClientID %>').attr('disabled', true);
    //$('#send').prop('disabled', true);
    //$('#send').attr('disabled', true);
    //$('#send').css('display', '').addClass('disabled'); // ADDED FOR TEST
    // END ADDED FOR TEST

    return true;    // restored on 11/08/2013 // COMMENTED FOR TEST
}

function validateRequierdField(textBoxControl, errorLabel) {
    var textValue = $(textBoxControl).val();
    if (textValue == "" || textValue == "שדה חובה") {
        $(errorLabel).text('שדה זה הוא שדה חובה');
        $(errorLabel).show();
        $(errorLabel).css('visibility', 'visible');
        $(textBoxControl).css('border', '1px solid #CC0000');   // 1px  red   juju
        return false;
    }
    else {
        $(errorLabel).hide();
        $(errorLabel).css('visibility', 'hidden');
        $(textBoxControl).css('border', '1px solid #666666');   // 2px  black   juju
        return true;
    }
}

function validateId(textBoxControl, errorLabel, errorMessage) {
    var textValue = $(textBoxControl).val();
    if (!isNumeric(textValue) || textValue.length != ID_LENGTH || !isValidIdNumber(textValue)) {

        $(errorLabel).text(errorMessage);
        $(errorLabel).show();
        $(errorLabel).css('visibility', 'visible');
        $(textBoxControl).css('border', '1px solid #CC0000');    // 2px red  juju
        return false;
    }

    $(errorLabel).hide();
    $(errorLabel).css('visibility', 'hidden');
    $(textBoxControl).css('border', '1px solid #666666');    // 2px black   juju
    return true;
}
// #ENDREGION VALIDATION

function updateErrorLabelView(errorLabel, errorText) {
    if (String.isNullOrEmpty(errorLabel)) {
        return;
    }

    if (String.isNullOrEmpty(errorText)) {
        $(errorLabel).css('visibility', 'hidden');
        $(errorLabel).text("");
        $(errorLabel).hide();
    }
    else {
        $(errorLabel).css('visibility', 'visible');
        $(errorLabel).text(errorText);
        $(errorLabel).show();
    }
}