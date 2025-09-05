// This file was modified since 30/05/2013
// added on 30/05/2013: ==================
$().ready(function () {

    $('#submitSignUp').click(function () {

    });

}); // end document.ready section

//var DIR = "WebServices/";
var FILE = "NewUserSignUp.asmx";        // "NewUserSignUp.asmx/";
var FILE1 = "PasswordRecovery.asmx";    // "PasswordRecovery.asmx/";
var isSentSMS = false;
var isSentEmail = false;

// CODE CORRECTIONS SINCE 19/06/2013 : ////////////////////////////////////////////////////////////
// #REGION VALIDATION
function SignUpValidate() {
    var errorFound = false;

    if (validateEmail('#emailTextBox', '#emailErrorLabel') == false) {
        errorFound = true;
    }

    if (validateRequierdField('#lastNameTextBox', '#lastNameErrorLabel') == false) {
        errorFound = true;
    }
    if (validateRequierdField('#firstNameTextBox', '#firstNameErrorLabel') == false) {
        errorFound = true;
    }

    if (validateRequierdField('#idTextBox', '#idErrorLabel') == false) {
        errorFound = true;
    }
    else {
        if (validateId('#idTextBox', '#idErrorLabel', 'מספר תעודת הזהות שהקשת אינו תקין') == false) {
            errorFound = true;
        }
    }

    if (validateRequierdField('#phoneTextBox', '#phoneErrorLabel') == false) {
        errorFound = true;
    }
    else {
        if (validateNumeric('#phoneTextBox', '#phoneErrorLabel', 'מספר הטלפון שהקשת אינו חוקי') == false) {
            errorFound = true;
        }
    }

    if (errorFound) return false;
    DisplayWaitMessage();
    // newUserSignUp(); // REMOVED ON 11/08/2013
    return true;
}

function newUserSignUp() {
    var userId = $('#idTextBox').val();
    var firstName = $('#firstNameTextBox').val();
    var lastName = $('#lastNameTextBox').val();
    var email = $('#emailTextBox').val();
    var phone = $('#phoneTextBox').val();
    var areaCode = $('#areaCodeDropDownList').val();
    var userInfo = new UserInfo(userId, firstName, lastName, email, areaCode, phone, '');
    if (userInfo != null/* && UserInfo != undefined*/)
        onNewUserSignUp(userInfo);
}

function DisplayWaitMessage() {
    var lblError = '#signUpErrorLabel';
    $(lblError).text('המתן...');
    $(lblError).show();
    $(lblError).css('visibility', 'visible');
    $(lblError).css('color', '#999999')   // Green   juju
}

function validateEmail(emailTextBox, errorLabel) {
    var address = $(emailTextBox).val();
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    if (address != "") {
        if (reg.test(address) == false) {
            $(errorLabel).text('כתובת הדואר האלקטורני שהזנת שגויה');
            updateStyle(false, emailTextBox, errorLabel);
            return false;
        }
        else {
            updateStyle(true, emailTextBox, errorLabel);
            return true;
        }
    }
}

function validateRequierdField(textBoxControl, errorLabel) {
    var textValue = $(textBoxControl).val();
    if (textValue == "" || textValue == "שדה חובה") {
        $(errorLabel).text('שדה זה הוא שדה חובה');
        updateStyle(false, textBoxControl, errorLabel);
        return false;
    }
    else {
        updateStyle(true, textBoxControl, errorLabel);
        return true;
    }
}

function validateNumeric(textBoxControl, errorLabel, errorMessage) {
    var textValue = $(textBoxControl).val();
    if (!isNumeric(textValue)) {
        $(errorLabel).text(errorMessage);
        updateStyle(false, textBoxControl, errorLabel);
        return false;
    }
    updateStyle(true, textBoxControl, errorLabel);
    return true;
}

function validateId(textBoxControl, errorLabel, errorMessage) {
    var textValue = $(textBoxControl).val();
    if (!isNumeric(textValue) || textValue.length != ID_LENGTH || !isValidIdNumber(textValue)) {
        $(errorLabel).text(errorMessage);
        updateStyle(false, textBoxControl, errorLabel);
        return false;
    }
    updateStyle(true, textBoxControl, errorLabel);
    
    return true;
}

function updateStyle(isValid, textBoxControl, errorLabel) {
    if (isValid) {
        $(errorLabel).hide();
        $(errorLabel).css('visibility', "hidden");
        $(textBoxControl).css('border', '1px solid #666666');  // 2px black   juju
    }
    else {
        $(errorLabel).show();
        $(errorLabel).css('visibility', 'visible');
        $(textBoxControl).css('border', '1px solid #CC0000');   // 2px red  juju
    }
}
// #ENDREGION VALIDATION

// #REGION SIGN-UP
function onNewUserSignUp(userInfo) {
    var lblError = '#signUpErrorLabel';
    // var service = DIR + FILE + "SignUpNewUser";  // for local (built-in) services only
    var service = GetServicePath(isRemote, directory, ipAddress, port, FILE, "SignUpNewUser"); 
    var data = null;
    if (mode == "Object" && !isRemote) {    // condition isRemote added
        data = { jsonUserInfo: userInfo };
        data = //JSON.stringify(data);  // temporary ewmoved
        service += "PerObject";

    }
    else {
        var data = "{ jsonId:'" + userInfo.id_num +
        "', jsonFirstName:'" + userInfo.first_name +
        "', jsonLastName:'" + userInfo.lastName +
        "', jsonEmail:'" + userInfo.email +
        "', jsonAreaCode:'" + userInfo.areaCode +
        "', jsonPhoneNumber:'" + userInfo.phoneNumber +
        "', jsonPassword:'" + userInfo.password + "' }";
        service += "PerParams";
    }

    if (data != null) {
        callAjax(service, data, userInfo, lblError);
    }
}

function callAjax(service, data, userInfo, lblError) {
    jQuery.support.cors = true; // Mandatory! Added for cross-browther support

    $.ajax({    // start ajax section
        url: service,
        data: data,    //data,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        cache: false,
        async: true,
        crossDomain: true,      // Added for cross-browther support
        processData: false,
        success: function (data) {
            // corrections on 02/07/2013 : //////////////////////////
            // onSuccessFromWebService(data.d, userInfo, lblError);
            if (data.d == null || data.d == "") {
                alert("Failed to retrieve json data");
            }
            else {
                if (data.d == "Exists" || data.d == "Failed") {
                    onUpdateErrorLabel(data.d, lblError);
                }
                else {
                    userInfo.password = data.d;
                    onSuccessFromWebService(userInfo);
                }
            }
            // end corrections //////////////////////////////////////
        },  // end success function (data)

        error: function (xhr, status, error) { // start error fuction
            //alert("Ajax completion gave " + xhr.status + ":\n" + xhr.responseText);
            var errDescr = (xhr.responseText != undefined && !String.isNullOrEmpty(xhr.responseText)) ?
                /*JSON.parse*/(xhr.responseText) : null;
            onAjaxError(xhr, status, error, errDescr);
            // writeLog(errDescr, xhr.statusText, error);   // optional
        }   // end error function
    });         // end ajax section
}

function onUpdateErrorLabel(data, lblError) {
    var message = (data == "Exists") ?
                ("מספר תעודת הזהות קיים כבר במערכת.") :
                ("אין אפשרות לספק את השירות כרגע. נא נסה שנית במועד מאוחר יותר.");
    $(lblError).show();
    $(lblError).text(message);
    $(lblError).css('visibility', 'visible');
    $(lblError).css('border-color', '#CC0000');  //  red juju
}

function onSuccessFromWebService(userInfo) {
    var data = "{ jsonUserID:'" + userInfo.id_num +
                "', jsonEmail:'" + userInfo.email +
                "', jsonPhone:'" + userInfo.phoneNumber +
                "', jsonAreaCode:'" + userInfo.areaCode + 
                "', jsonPassword:'" + userInfo.password + "' }";
    // var service = DIR + FILE1 + "GetNewUserPasswordData";    // for local (built-in) services only
    var service = GetServicePath(isRemote, directory, ipAddress, port, FILE1, "GetNewUserPasswordData");
    callAjaxPwd(service, data);
}

function callAjaxPwd(service, data) {
    jQuery.support.cors = true; // Mandatory! Added for cross-browther support

    $.ajax({    // start ajax section
        url: service,
        data: data,    //data,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        cache: false,
        async: true,
        crossDomain: true,      // Added for cross-browther support
        processData: false,
        success: function (data) {

            sendNewUserPassword(data.d);

        },  // end success function (data)

        error: function (xhr, status, error) { // start error fuction
            //alert("Ajax completion gave " + status + ":\n" + error);
            var errDescr = (xhr.responseText != undefined && !String.isNullOrEmpty(xhr.responseText)) ?
                /*JSON.parse*/(xhr.responseText) : null;
            onAjaxError(xhr, status, error, errDescr);
            // writeLog(errDescr, xhr.statusText, error);   // optional
        }   // end error function
    });        // end ajax section
}


function sendNewUserPassword(data) {
    if (data != null) {
        sendUserPasswordByPhone(data);
        if (!String.isNullOrEmpty(data.ClientEmail)) {
            sendUserPasswordByEmail(data);
        }
        if (isSentSMS/* && isSentEmail*/) { // email is optional
            if (top != self) {
                top.location.href = 'PostSignUp.aspx';
            }
        }
        else {
            alert("שגיאה קריטית של אייאקס");
        }
    }
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

    callAjaxPasswordSending(serviceData, serviceUrl, serviceType, true);
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

    callAjaxPasswordSending(serviceData, serviceUrl, serviceType, false);
}

function callAjaxPasswordSending(serviceData, serviceUrl, serviceType, isSMS) {
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
            onPasswordSendingSuccess(data);

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

function onPasswordSendingSuccess(data) {
//    alert(data); // TEST
}

//function onSuccessFromWebService(data, lblError) {
//    if (data != null) {
//        if (data == "Success") {
//            if (top != self) {
//                 top.location.href = 'PostSignUp.aspx';
//            }
//        }
//        else {
//            var message = (data == "Exists") ? 
//                ("מספר תעודת הזהות קיים כבר במערכת.") : 
//                ("אין אפשרות לספק את השירות כרגע. נא נסה שנית במועד מאוחר יותר.");
//            $(lblError).show();
//            $(lblError).text(message);
//            $(lblError).css('visibility', 'visible');
//            $(lblError).css('border-color', 'Red');
//        }
//    }
//    else {
//        alert("Failed to retrieve json data");
//    }
//}
//// #ENDREGION SIGN-UP

// UserInfo class declaration =====================================================================
function UserInfo(id_num, first_name, lastName, email, areaCode, phoneNumber, password) {
    this.id_num = id_num;
    this.first_name = first_name;
    this.lastName = lastName;
    this.email = email;
    this.areaCode = areaCode;
    this.phoneNumber = phoneNumber;
    this.password = password;

    this.getUserId = getUserId;
    function getUserId() {
        return id_num;
    }

    this.setUserId = setUserId;
    function setUserId(newUserId) {
        this.id_num = newUserId || id_num;
    }

    this.getFirstName = getFirstName;
    function getFirstName() {
        return first_name;
    }

    this.setFirstName = setFirstName;
    function setFirstName(newFirstName) {
        this.first_name = newFirstName || first_name;
    }

    this.getLastName = getLastName;
    function getLastName() {
        return lastName;
    }

    this.setLastName = setLastName;
    function setLastName(newLastName) {
        this.lastName = newLastName || lastName;
    }

    this.getEmail = getEmail;
    function getEmail() {
        return email;
    }

    this.setEmail = setEmail;
    function setEmail(newEmail) {
        this.email = newEmail || email;
    }

    this.getAreaCode = getAreaCode;
    function getAreaCode() {
        return areaCode;
    }

    this.setAreaCode = setAreaCode;
    function setAreaCode(newAreaCode) {
        this.areaCode = newAreaCode || areaCode;
    }

    this.getPhoneNumber = getPhoneNumber;
    function getPhoneNumber() {
        return phoneNumber;
    }

    this.setPhoneNumber = setPhoneNumber;
    function setPhoneNumber(newPhoneNumber) {
        this.phoneNumber = newPhoneNumber || phoneNumber;
    }

    this.getPassword = getPassword;
    function getPassword() {
        return password;
    }

    this.setPassword = setPassword;
    function setPassword(newPassword) {
        this.password = newPassword || null;
    }
}
// end of UserInfo class declaration ==============================================================