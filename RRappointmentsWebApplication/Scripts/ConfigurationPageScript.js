// added on 24/06/2013 : ////
$().ready(function () {

    updateControls();

    $('#ddlWebServiceLocation').change(function () {
        updateControls();
    }); // end dropdown change function

    // ADDED ON 12/09/2013 : /////////////////////////
    $('#chkSSL').click(function () {
        if (this.checked) {
            $(".login").show();
            $(".login").css('visibility', 'visible');
        }
        else {
            $(".login").hide();
            $(".login").css('visibility', 'hidden');
        }
    });
    // ADDED ON 12/02/2020 : /////////////////////////
    $('#chkNickname').click(function () {
        if (this.checked) {
            $("#lblNickname").show();
            $("#lblNickname").css('visibility', 'visible');
            $("#txtNickname").show();
            $("#txtNickname").css('visibility', 'visible');

        }
        else {
            $("#lblNickname").hide();
            $("#lblNickname").css('visibility', 'hidden');
            $("#txtNickname").hide();
            $("#txtNickname").css('visibility', 'hidden');
        }
    });
    // END ADDED /////////////////////////////////////

    // added on 09/09/2013 : 
    $("#txtSmtpServer").change(function () {
        var smtp = $(this).val();
        if (smtp.search("gmail") != -1 ||
            smtp.search("office365") != -1 ||
            smtp.search("yahoo") != -1 ||
            smtp.search("aol") != -1) {
            $("#txtSmtpPort").val(587);
        }
        else {
            $("#txtSmtpPort").val(25);
        }
    });

    $("#txtSmtpServer").focusout(function () {
        var smtp = $(this).val();
        if (smtp.search("gmail") != -1 ||
            smtp.search("yahoo") != -1 ||
            smtp.search("office365") != -1 ||
            smtp.search("aol") != -1) {
            $("#txtSmtpPort").val(587);
        }
        else {
            $("#txtSmtpPort").val(25);
        }
    });

    $("input").focusout(function () {
        var text = $(this).val();
        if (!String.isNullOrEmpty(text))
            $(this).css("border-color", "White");
    });

    $('#chkUseWcfDiariesCounterService').click(function () {
        if (this.checked) {
            $(".WcfConfigInput").prop('disabled', false);
            $(".WcfConfigInput").prop('readonly', false);
            $(".WcfConfigInput").css('color', 'blue');
        }
        else {
            $(".WcfConfigInput").prop('disabled', true);
            $(".WcfConfigInput").prop('readonly', true);
            $(".WcfConfigInput").css('color', 'gray');
        }
    });

    $('#passwordUpdateCheckBox').click(function () {
        if (this.checked) {
            $("#txtDaysCount").show();
            $("#txtDaysCount").css('visibility', 'visible');
        }
        else {
            $("#txtDaysCount").hide();
            $("#txtDaysCount").css('visibility', 'hidden');
        }
    });

    $("#txtDaysCount").change(function () {
        var txt = $(this).val();
        var numbers = /^[0-9]+$/;
        if(!txt.match(numbers) || !/^\d+$/.test(txt) || !$.isNumeric(txt))
        {
            alert("נא להקליד אך ורק מספרים!");
            $(this).val("");
        }
    });
    // end added

});                     // end document.ready section

// corrected on 24/06/2013 //
function DisplayAppointmentLimit() {
    var ddl = "#appointmentLimitDropDownList";
    if ($('#appointmentLimitCheckBox').is(':checked')) {
        $(ddl).css('visibility', 'visible');
        $(ddl).show();
    }
    else {
        $(ddl).css('visibility', 'hidden');
        $(ddl).show();
    }
}

// added on 18/07/2013 //
function updateControls() {
    var val = $('#ddlWebServiceLocation option:selected').val();
    if (val == undefined || val == null) {
        val = 0;
    }
    else {
        val = parseInt(val, 10);
    }
    if (val === 0) {
        $('.hiddenRow').hide();
    }
    else {
        $('.hiddenRow').show();
    }

    // ADDED ON 12/09/2013: /////////////////////
    var checkBox = $('#chkSSL');
    if (checkBox[0].checked) {
        $(".login").show();
        $(".login").css('visibility', 'visible');
    }
    else {
        $(".login").hide();
        $(".login").css('visibility', 'hidden');
    }

    // ADDED ON 12/02/2020 : /////////////////////////
    var nickName = $('#chkNickname');
    if (nickName[0].checked) {
        $("#lblNickname").show();
        $("#lblNickname").css('visibility', 'visible');
        $("#txtNickname").show();
        $("#txtNickname").css('visibility', 'visible');

    }
    else {
        $("#lblNickname").hide();
        $("#lblNickname").css('visibility', 'hidden');
        $("#txtNickname").hide();
        $("#txtNickname").css('visibility', 'hidden');
    }
    // ADDED ON 12/02/2020: /////////////////////

    // END ADDED ////////////////////////////////
}

function onValidateData() {
    $('#lblMailingStatus').text("");
    // var isChecked = $('#chkEncrypt')[0].checked;
    var isChecked = $('#chkSSL')[0].checked;
    var isNickUse = $('#chkNickname')[0].checked;// added on 12/02/2020

    if (!isValidString("txtFrom")       ||
        !isValidString("txtTo")         ||
        !isValidString("txtSmtpServer") ||
        !isValidString("txtSmtpPort")   ||
        !isValidString("txtSubject")    ||
        (isChecked && (
        !isValidString("txtUserName")   ||
        !isValidString("txtPassword"))) ||
        (isNickUse &&
        !isValidString("txtNickname")))
    {
        alert("!אנא מלא את כל השדות הנדרשים");
        return false;
    }
    $("input").css("border-color", "White");

    return true;
}

function isValidString(id) {
    id = '#' + id;
    var text = $(id).val();
    //prompt(id, text);   // added for test
    
    if (String.isNullOrEmpty(text)) {
        $(id).css("border-color", "Red");
        return false;
    }
    $(id).css("border-color", "White");

    return true;
}

function sendEmail() {
    var sendTo = $("#txtTo").val();
    var copyTo = $("#txtCc").val();
    if (String.isNullOrEmpty(copyTo)) {
        copyTo = $("#txtFrom").val();
    }
    var subj = $("#txtSubject").val();
    if (String.isNullOrEmpty(subj))
        subj = "Test Test Test";

    var fullDate = "";
    var now = new Date();

    // Get the month, day, and year.
    var date = now.getDate();
    if (parseInt(date, 10) < 10)
        date = "0" + date;
    var month = (now.getMonth() + 1);
    if (parseInt(month, 10) < 10)
        month = "0" + month;
    var hours = now.getHours();
    if (parseInt(hours, 10) < 10)
        hours = "0" + hours;
    var minutes = now.getMinutes();
    if (parseInt(minutes, 10) < 10)
        minutes = "0" + minutes;

    fullDate += date + "/";
    fullDate += month + "/";
    fullDate += now.getFullYear();
    fullDate += " at ";
    fullDate += hours + ":";
    fullDate += minutes;

    var body = $("#txtMessage").val(); 
    body += "\n[Message sent on " + fullDate + "]";
    var link = "mailto:" + sendTo;
    link += "?cc=" + copyTo;
    link += "&subject=" + subj;
    link += "&body=" + body;

    window.location.href = link;

}

function resetData() {
    $('input[type=text]').val("");
    $('#lblMailingStatus').text("");
    $('#txtMessage').text("");
    $('#txtPassword').val("");
    $('#lblPath').text("").hide();
    $('#chkPreset').attr('checked', false);
  
    return false;
}

function checkEmailSettings() {
    var isChecked = $('#chkSSL')[0].checked;

    if (!isValidString("txtFrom")       ||
        !isValidString("txtSmtpServer") ||
        !isValidString("txtSmtpPort")   ||
        (isChecked && (
        !isValidString("txtUserName")   ||
        !isValidString("txtPassword"))))
    {
        alert("!אנא מלא את כל השדות הנדרשים");
        return false;
    }
    $("input").css("border-color", "White");

    return true;
}