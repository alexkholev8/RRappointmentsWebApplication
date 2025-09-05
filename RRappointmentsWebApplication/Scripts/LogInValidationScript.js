// This file was modified since 30/05/2013
// added on 30/05/2013: ==================
$().ready(function () {

    $('#logInImageButton').click(function () {

        //    onLogIn();

    });
    //$('#sessionInfoHidden').val(false);

});    // end document.ready section

//var DIR = "WebServices/";
var FILE = "UserLogin.asmx";    // "UserLogin.asmx/";
var isLogIn = false;

function onLogIn() {
    var password = $('#passwordTextBox').val();
    var userID = $('#IdTextBox').val();
    if (!String.isNullOrEmpty(password) && !String.isNullOrEmpty(userID)) {
        if (userID == "admin" && password == "134679")
            checkAdminLogIn();
        else
            checkUserLogIn(password, userID);
    }
}

function checkUserLogIn(password, userID) {
    var data = "{ jsonPassword:'" + password + "', jsonUserId:'" + userID + "' }";
    // var service = DIR + FILE + "CheckUserLogin";  // for local (built-in) services only
    var service = GetServicePath(isRemote, directory, ipAddress, port, FILE, "CheckUserLogin");
    callAjax(service, data);
}

function checkAdminLogIn() {
    isLogIn = true;
    url = "ConfigurationPage.aspx";
//    $(location).attr('href', url);    // commented 4 test
}

function callAjax(service, data) {
    jQuery.support.cors = true; // Mandatory! Added for cross-browther support

    $.ajax({    // start ajax section
        url: service,
        data: data,
        dataType: "json",   // "text/html",
        type: "POST",
        contentType: "application/json; charset=utf-8", // "text/html",
        // ADDED 4 TEST : /////////////////////////////////
        //        headers: { "Content-Type": "application/json",
        //            "Accept": "application/json",
        //            "X-Requested-With": "XMLHttpRequest"
        //        },
        // END TEST ///////////////////////////////////////
        cache: false,   // true,
        async: true,    // false, SET TO TRUE TO WORK WITH CROSS DOMAIN
        crossDomain: true,      // Added for cross-browther support
        processData: false,
        // ADDED 4 TEST : /////////////////////////
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Content-type",
                "application/json; charset=utf-8");
        },
        // END ADDED 4 TEST ///////////////////////

        success: function (data) {
            onSuccessLoginFromWebService(data.d);
        },  // end success function (data)

        error: function (xhr, status, error) { // start error fuction
            //alert("Ajax completion gave " + status + ":\n" + error);
            var errDescr = (xhr.responseText != undefined && !String.isNullOrEmpty(xhr.responseText)) ? 
                /*JSON.parse*/(xhr.responseText) : null;
            onAjaxError(xhr, status, error, errDescr);
            // writeLog(errDescr, xhr.statusText, error);   // optional
        }   // end error function

        //,complete: function (xhr, status) { alert(status); }
    });          // end ajax section
}

function onSuccessLoginFromWebService(data) {
    // alert(data);
    //if (data == "OK") {
    if (data.LoginStatus == "OK") {
        isLogIn = true;
        saveSessionInfo(data);
//        url = "step2.aspx";               // TEST
//        $(location).attr('href', url);    // TEST
    }
    else {
        isLogIn = false;
        $('#logInErrorLabel').css('visibility', 'visible');
        $('#logInErrorLabel').text(data.LoginStatus);
    }
}

function saveSessionInfo(sessionInfo) {
    // corrections and additions since 25/07/2013:
    var service = null;
    var data = null;
    if (isRemote == "false") {
        service = "step1.aspx/SetSession";
        data = { jsonSessionInfo: sessionInfo };
        //data = JSON.stringify(data);  // temporary removed
    }
    else {
        service = "step1.aspx/SetSessionPerParams";
        data = "{ jsonIdNum:'" + sessionInfo.Id_num +
            "', jsonFileNum:'" + sessionInfo.FileNum +
            "', jsonPassword:'" + sessionInfo.UserPassword +
            "', jsonFirstName:'" + sessionInfo.First_name +
            "', jsonLastName:'" + sessionInfo.LastName +
            "', jsonCustNum:'" + sessionInfo.Cust_num +
            "', jsonStatus:'" + sessionInfo.LoginStatus + "' }";
    }
    // end corrections and additions ///////////
    if (data != null && service != null)
        callAjaxSession(service, data);
}

function callAjaxSession(service, data) {
    $.ajax({    // start ajax section
        url: service,
        data: data, // single parameter pass
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        cache: false,
        async: true,    // false - may be set to false
        success: function (data) {
            onSuccessSetSession(data.d);
        },  // end success function (data)

        error: function (xhr, status, error) { // start error fuction
            //alert("Ajax completion gave " + status + ":\n" + error);
            var errDescr = (xhr.responseText != undefined && !String.isNullOrEmpty(xhr.responseText)) ?
                /*JSON.parse*/(xhr.responseText) : null;
            onAjaxError(xhr, status, error, errDescr);
            // writeLog(errDescr, xhr.statusText, error);   // optional
        }   // end error function
    }); // end ajax section   
}

function onSuccessSetSession(data) {
    url = "step2.aspx";             // TEST
    $(location).attr('href', url);  // TEST
}
// end additions =========================

// corrections made on 30/05/2013: =======
function Validate() {
    var errorFound = false;

    if (validateRequierdField('#passwordTextBox', '#logInErrorLabel') == false) {
        errorFound = true;
    }

    if (validateRequierdField('#IdTextBox', '#logInErrorLabel') == false) {
        errorFound = true;
    }
    // ADDED ON 01/10/2013 : //////////////////////////////////////////////////
    else {
        if (validateUserIdNumber('#IdTextBox', '#logInErrorLabel') == false) {
            errorFound = true;
        }
    }
    // END ADDED //////////////////////////////////////////////////////////////

    if (errorFound) 
        return false;
    DisplayWaitMessage('#logInErrorLabel');
    // onLogIn();       // COMMENTED ON 08/08/2013
    // return isLogIn;  // COMMENTED ON 08/08/2013 // instead instead true
    return true;        // ADDED ON 08/08/2013
}

function DisplayWaitMessage(errorLabel) {
    $(errorLabel).text('המתן...');
    $(errorLabel).show();
    $(errorLabel).css('visibility', 'visible');
    $(errorLabel).css('color', '#999999'); // green  juju
}

function validateRequierdField(textBoxControl, errorLabel) {
    var textValue = $(textBoxControl).val();
    if (textValue == "" || textValue == "שדה חובה") {
        $(errorLabel).text('חובה להזין שם משתמש וסיסמא');
        $(errorLabel).css('visibility', 'visible');
        $(textBoxControl).css('border', '1px solid #CC0000');  // 2px  red  juju
        return false;
    }
    else {
        if (textBoxControl == '#passwordTextBox') {
            $(errorLabel).css('visibility', 'hidden');
        }
        $(textBoxControl).css('border', '1px solid #666666');  // 2px  black  juju
        return true;
    }
}

// ADDED ON 01/10/2013 FOR UserID VALIDATION : /////////////////
function validateUserIdNumber(textBoxControl, errorLabel) {
    var textValue = $(textBoxControl).val();
    if (textValue === "admin")
        return true;
    if (!isNumeric(textValue) || textValue.length != ID_LENGTH || !isValidIdNumber(textValue)) {
        $(errorLabel).text('מספר תעודת הזהות שהקשת אינו תקין');
        $(errorLabel).css('visibility', 'visible');
        $(textBoxControl).css('border', '1px solid #CC0000');  // 2px red  juju
        return false;
    }
    else {
        if (textBoxControl == '#passwordTextBox') {
            $(errorLabel).css('visibility', 'hidden');
        }
        $(textBoxControl).css('border', '1px solid #666666');  // 2px black juju

        return true;
    }
}
// END ADDED UserID VALIDATION //////////////////

// end corrections =======================
// ADDED ON 05/08/2013 FOR ERROR LOG SCRIPT
function callAjaxWriteLog(service, data) {
    $.ajax({    // start ajax section
        url: service,
        data: data, // single parameter pass
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        cache: false,
        async: true,    // false - may be set to false
        success: function (data) {
            //onSuccessSetSession(data.d);
        },  // end success function (data)

        error: function (xhr, status, error) { // start error fuction
            //alert("Ajax completion gave " + status + ":\n" + error);
            //onAjaxError(xhr, status, error);
        }   // end error function
    }); // end ajax section   
}
// END TEST