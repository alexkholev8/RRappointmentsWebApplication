// This file was modified since 30/06/2013
// added on 30/05/2013: ==================
$().ready(function () {

    $('#smsAgreementCheckBox').change(function() {
        if ($('#smsAgreementCheckBox').attr("checked") == 'checked' ||
            $('#smsAgreementCheckBox').is(':checked')) {
            $('#submitSignUp').show();
            $('#submitSignUp').css('visibility', 'visible');
            // added on 15/10/2013; commented on 11/02/2019 : ///////
            //$('#popupUnblockLabel').show();
            //$('#popupUnblockLabel').css('visibility', 'visible');
            // end added ////////////////////////////////////////////
        }
        else {
            $('#submitSignUp').hide();
            $('#submitSignUp').css('visibility', 'hidden');
            // added on 15/10/2013 : ///////////////////////////////
            $('#popupUnblockLabel').hide();
            $('#popupUnblockLabel').css('visibility', 'hidden');
            // end added ///////////////////////////////////////////
        }
    }); // end checkbox.change function


}); // end document.ready section

// CODE CORRECTIONS SINCE 19/06/2013 : ////////////////////////////////////////////////////////////
function SmsAgreementCheckedChanged() {
//    var checkBox = this;
//    if (checkBox == null || checkBox == undefined)
        checkBox = '#smsAgreementCheckBox';
    if ($(checkBox).attr("checked") == 'checked' || $(checkBox).is(':checked')) {
        $('#submitSignUp').show();
        $('#submitSignUp').css('visibility', 'visible');
        // added on 15/10/2013; commented on 11/02/2020 : //////
        //$('#popupUnblockLabel').show();
        //$('#popupUnblockLabel').css('visibility', 'visible');
        // end added ///////////////////////////////////////////
    }
    else {
        $('#submitSignUp').hide();
        $('#submitSignUp').css('visibility', 'hidden');
        // added on 15/10/2013 : ///////////////////////////////
        $('#popupUnblockLabel').hide();
        $('#popupUnblockLabel').css('visibility', 'hidden');
        // end added ///////////////////////////////////////////
    }
}

// THIS PART OF CODE COMMENTED SINCE 19/06/2013 ///////////////////////////////////////////////////
/*
function SmsAgreementCheckedChanged() {
    if (document.getElementById('smsAgreementCheckBox').checked) {
        document.getElementById('submitSignUp').style.visibility = "visible";
    }
    else {
        document.getElementById('submitSignUp').style.visibility = "hidden";
    }
}
*/