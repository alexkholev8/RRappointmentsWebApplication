
// This file was modified since 30/05/2013
// added on 30/05/2013: ==================
$().ready(function () {

    // DISABLED ON 22/08/2013 : //////
    //    if (isPostBack == "true") {
    //        updatePreprintData();
    //    }
    // END DISABLED //////////////////

    // ENVELOPING CONDITION ADDED ON 09./09/2015 : ///////////////////////
    if (isPostBack == "false") {
        $('#availablesAppointmentsGridView').css("visibility", "hidden");
        $('#availablesAppointmentsGridView').hide();
    }
    // END ENVELOPING CONDITION ADDED ////////////////////////////////////

    // CLIENT-SIDE CODE DISABLED ON 18/08/2013: /////
    //    getInsuranceList();
    //    getOperationList();
    //    getOperGroupList();
    //    getOldAppointments();

    //    $("#operDropDownList").change(function () {
    //        updateSiteList();
    //    }); // end dropdown change function

    //    $("#treatmentGroupDropDownList").change(function () {
    //        var GroupValue = null;
    //        GroupValue = $('#treatmentGroupDropDownList option:selected').val();
    //        $('#operDropDownList').removeAttr('disabled');
    //        getOperationListPerGroup(GroupValue);
    //        $('#siteDropDownList').empty();
    //        $('#siteDropDownList').prop('disabled', true);
    //    }); // end dropdown change function

    //    $("#siteDropDownList").change(function () {

    //    }); // end dropdown change function

    //    $("#insuranceDropDownList").change(function () {

    //    });
    // END CLIENT-SIDE CODE DISABLED ////////////////
});        // end document.ready section

//var DIR = "WebServices/";
var FILE  = "Appointments.asmx";            // "Appointments.asmx/";
var FILE1 = "TreatmentInstruction.asmx";    // "TreatmentInstruction.asmx/";
var operInstructions = "";
// added on 25/06/2013 : ////
var operCode = "0";
var groupCode = "0";
var insurCode = "0";
var siteCode = "0";
var checkSun = true;
var checkMon = true;
var checkTue = true;
var checkWed = true;
var checkThu = true;
var checkFri = true;
var checkSat = true;
var line = 0;   // ADDED ON 21/08/2013
// end additions ////////////

function getInsuranceList() {
    // var service = DIR + FILE + "GetInsuranceList";   // for local (built-in) services only
    var service = GetServicePath(isRemote, directory, ipAddress, port, FILE, "GetInsuranceList");
    var data = "{}";
    callAjaxDDL(service, data, '#insuranceDropDownList');
    curInsurance = $('#insuranceDropDownList option:selected').val();
}

function getOperationList() {
    // var service = DIR + FILE + "GetOperList";    // for local (built-in) services only
    var service = GetServicePath(isRemote, directory, ipAddress, port, FILE, "GetOperList");
    var data = "{}";
    callAjaxDDL(service, data, '#operDropDownList');
}

function getOperGroupList()
{
    // var service = DIR + FILE + "GetTreatmentGroupList";  // for local (built-in) services only
    var service = GetServicePath(isRemote, directory, ipAddress, port, FILE, "GetTreatmentGroupList");
    var data = "{}";
    callAjaxDDL(service, data, '#treatmentGroupDropDownList');
    updateOperationListVisibility();
}

function getSiteList(operValue) {
    // var service = DIR + FILE + "GetSiteList";    // for local (built-in) services only
    var service = GetServicePath(isRemote, directory, ipAddress, port, FILE, "GetSiteList");
    var data = "{ 'jsonValue': '" + operValue + "' }";
    callAjaxDDL(service, data, '#siteDropDownList');
    $('#siteDropDownList').removeAttr('disabled');
}

function getOperationListPerGroup(group) {
    // var service = DIR + FILE + "GetOperList";    // for local (built-in) services only
    var service = GetServicePath(isRemote, directory, ipAddress, port, FILE, "GetOperList");
    var data = "{}";
    callAjaxDDL(service, data, '#operDropDownList', group);
}

function callAjaxDDL(service, data, ddlist, cond) {
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
            if (cond == undefined || cond == null) {
                fillDropDownList(ddlist, data.d);
            }
            else {
                fillDropDownListCond(ddlist, data.d, cond);
            }

        },  // end success function (data)

        error: function (xhr, status, error) { // start error fuction
            //alert("Ajax completion gave " + status + ":\n" + error);
            var errDescr = (xhr.responseText != undefined && !String.isNullOrEmpty(xhr.responseText)) ?
                /*JSON.parse*/(xhr.responseText) : null;
            onAjaxError(xhr, status, error, errDescr);
            // writeLog(errDescr, xhr.statusText, error);   // optional
        }   // end error function
    });  // end ajax part
}

function fillDropDownList(ddlist, data) {
    var isSiteList = (ddlist == '#siteDropDownList') ? true : false;
    var defSite = (isSiteList) ? (getPrevSelectedValue(ddlist)) : null;
    $(ddlist).empty();
    if (!isSiteList)
    //    $(ddlist).empty().append($("<OPTION></OPTION>").val("0").html(""));
        $(ddlist).append($("<OPTION></OPTION>").val("0").html("")); 
    if (data != null && data != undefined) {
        $.each(data, function () {
            $(ddlist).append($("<OPTION></OPTION>").val(this['Value']).html(this['Text']));
        });

        if (isSiteList) {
            if (defSite != null)
                $(ddlist).val(defSite);
            else
                $(ddlist).index(1); //?
        }

        if (ddlist == "#operDropDownList" && operCode != "0") {
            $("#operDropDownList").val(operCode);
            updateSiteList();
        }
        if (ddlist == "#insuranceDropDownList" && insurCode != "0") {
            $("#insuranceDropDownList").val(insurCode);
        }
        if (ddlist == "#treatmentGroupDropDownList" && $("#treatmentGroupDropDownList").is(":visible") && groupCode != "0") {
            $("#treatmentGroupDropDownList").val(groupCode);
        }

    }
    else alert("Failed to fill " + ddlist);
}

function fillDropDownListCond(ddlist, data, cond) {
    $(ddlist).empty();
    $(ddlist).append($("<OPTION></OPTION>").val("0").html(""));
    if (data != null && data != undefined) {
        $.each(data, function () {
            if (this['Value'] == cond) {
                $(ddlist).append($("<OPTION></OPTION>").val(this['Value']).html(this['Text']));
            }
        });
    }
    else alert("Failed to fill " + ddlist);
}

function getPrevSelectedValue(ddlist) {
    var index = $(ddlist).index();
    var value = (index != 0) ? $(ddlist + ' option:selected').val() : null;
    // added on 23/07/2013 // 
    if (siteCode != "0") {
        value = siteCode;
    }
    // end added ////////////
    
    return value;
}

function getOldAppointments() {
    // var service = DIR + FILE + "GetOldAppointments"; // for local (built-in) services only
    var service = GetServicePath(isRemote, directory, ipAddress, port, FILE, "GetOldAppointments");
    // var data = "{}";
    var data = "{ jsonId_Num:'" + userId + "', jsonFileNum:'" + fileNum + "' }";
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

function fillGridView(grid, data)
{
    if (data != null && data != undefined) {
        var len = data.length;
        if (len == 0) {
            $('#oldAppointmentsPanel').hide();
            $('#pagesInnerFrameLargerPanel').height(680);
        }
        else {
            $('#oldAppointmentsPanel').show();
            $('#pagesInnerFrameLargerPanel').height(940);
            $(grid + " tr:has(td)").remove();
            for (var i = 0; i < len; i++) {
                $(grid).append("<tr><td>" + data[i].AppointmentOperName +
                "</td><td>" + data[i].AppointmentLocation +
                "</td><td>" + data[i].AppointmentTime +
                "</td><td>" + data[i].AppointmentDate +
                "</td><td>" + data[i].AppointmentDay +
                "</td></tr>");
            }
            checkAppointmentLimit(len);
        }
    }
    else alert("Failed to fill " + grid);
}

function checkAppointmentLimit(len) {
    if (appointmentLimit != "0") {
        if (len >= parseInt(appointmentLimit, 10)) {
            $('#mainPanel').hide();
            $('#appointmentLimitTitle').show();
        }
    }
}

function updateOperationListVisibility() {
    if (groups == "True") {
        $('#operDropDownList').prop('disabled', true);
    }
    else {
        $('#treatmentGroupRow').hide();
    }
}

function getAvailableAppointments() {
    var operCode = $('#operDropDownList option:selected').val();
    var siteCode = $('#siteDropDownList option:selected').val();
    var fromDate = $('#startDateTextBox').val();
    var toDate = $('#endDateTextBox').val();
    var day1 = $('#sundayCheckBox').is(':checked') ? 1 : 0;
    var day2 = $('#mondayCheckBox').is(':checked') ? 2 : 0;
    var day3 = $('#tuesdayCheckBox').is(':checked') ? 3 : 0;
    var day4 = $('#wednesdayCheckBox').is(':checked') ? 4 : 0;
    var day5 = $('#thursdayCheckBox').is(':checked') ? 5 : 0;
    var day6 = $('#fridayCheckBox').is(':checked') ? 6 : 0;
    var day7 = $('#saturdayCheckBox').is(':checked') ? 7 : 0;

    // var service = DIR + FILE + "GetAvailableAppointments";   // for local (built-in) services only
    var service = GetServicePath(isRemote, directory, ipAddress, port, FILE, "GetAvailableAppointments");
    var data = "{ jsonOperCode:" + parseInt(operCode, 10) +
              ", jsonSiteCode:" + parseInt(siteCode, 10) +
              ", jsonFromDate:'" + fromDate +
              "', jsonToDate:'" + toDate + 
              "', jsonDay1:" + parseInt(day1, 10) + 
              ", jsonDay2:" + parseInt(day2, 10) +
              ", jsonDay3:" + parseInt(day3, 10) +
              ", jsonDay4:" + parseInt(day4, 10) +
              ", jsonDay5:" + parseInt(day5, 10) +
              ", jsonDay6:" + parseInt(day6, 10) +
              ", jsonDay7:" + parseInt(day7, 10) + " }";
   
    callAjaxAvailableGridView(service, data, '#availablesAppointmentsGridView');
}

function callAjaxAvailableGridView(service, data, grid) {
    jQuery.support.cors = true; // Mandatory! Added for cross-browther support

    $.ajax({    // start ajax section
        url: service,
        data: data,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        cache: false,
        async: true,
        crossDomain: true,      // Added for cross-browther support
        processData: false,
        success: function (data) {
            fillAvailableGridView(grid, data.d);
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

function fillAvailableGridView(grid, data) {
    if (data != null && data != undefined) {
        var len = data.length;
        if (len == 0) {
            $(grid).hide();
            $('#selectAppointmentTiltleLabel').hide();
            $('#statusLabel').show();
        }
        else {
            $('#selectAppointmentTiltleLabel').show();
            $('#statusLabel').hide();
            $(grid + " tr:has(td)").remove();
            var row = "";
            for (var i = 0; i < len; i++) {
                row += getGridViewRowString(i) + // "<tr>" 
                "<td class=\"HiddenCell\" style=\"display:none\">" + data[i].Dline +
                "</td><td>" + getRadioButtonString(i, data[i].Dline, "availablesAppointmentsGridView") + 
                "</td><td>" + data[i].Ddate +
                "</td><td>" + data[i].Dtime +
                "</td><td>" + data[i].Dday +
                "</td></tr>";
            }
            $(grid).css('visibility', 'visible');
            $(grid).append(row);
            $(grid).show();

            setGridViewPaginator(grid, len);
        }
    }
    else alert("Failed to fill " + grid);
}

function setGridViewPaginator(grid, len) {
    grid = grid.substring(1);
    $('#smart_paginator').smartpaginator({ totalrecords: len, recordsperpage: 8, initval: 0,
        controlsalways: true, datacontainer: grid, dataelement: 'tr', display: 'single',
        next: 'הבא', prev: 'הקודם', first: 'לדף הראשון', last: 'לדף האחרון', theme: 'green', go: 'לעמוד', onchange: onChange
    });
}

function onChange(newPageValue) {
// alert(newPageValue);
} 

function getGridViewRowString(i) {
    var rowId = "row" + i; 
    var tr = "";
    tr += "<tr id=\"" + rowId + "\" ";
    tr += "onclick=\"SetRow('" + rowId + "');\" ";
    tr += "onmouseover=\"ChangeRowColor('" + rowId + "', 'mouseOver');\" ";
    tr += "onmouseout=\"ChangeRowColor('" + rowId + "', 'mouseOut');\">";

    return tr;
}

function getRadioButtonString(i, dLine, grid) {
    var cell = "";
    cell += "<input name=\"" + grid + "Row";
    cell += "\" id=\"" + "radio_row" + i + "\" ";
    cell += "type=\"radio\" ";
    cell += "runat = \"server\" ";
    cell += "value=\"" + dLine + "\" />";
    
    return cell;
}
// end additions 

function SelectOne(rdo) {
    $('#next').show();
    $('#next').css('visibility', 'visible');
    //var tr = rdo.parent().parent();
    $("input:radio").attr("checked", false);
    var tr = rdo.parentElement.parentElement;
    rdo.checked = true;
    SetRow(tr.id);
}

function SetRow(rowId) {
    var tr = $('#' + rowId);
    var rdo;
    if (tr != null && tr != undefined) {
        rdo = tr.find('input:radio');
    }
    else {
        rdo = $('#radio_' + rowId);
        tr = rdo.parent().parent();
    }

    $('#availablesAppointmentsGridView tr').css('background-color', '');
    $('#availablesAppointmentsGridView tr').css('color', '#999999');
    //$('#availablesAppointmentsGridView tr').css('border-color', '#94332C');
    tr.css('background-color', '#999999');  // '#82B9FA'
    //tr.css("border-color", "Red");
    tr.css('color', 'White');
    $("input:radio").attr("checked", false);
    rdo.prop('checked', true);
    $('#next').show();
    $('#next').css('visibility', 'visible');
    line = rdo.val();   // ADDED ON 21/08/2013
}

//  ADDED ON 21/08/2013 : //////////////////////////////////////
function UpdateSelection() {
    $('#availablesAppointmentsGridView tr').each(function () {
        var row = $(this);
        var rdo = row.find('input:radio');
        var val = rdo.val();
        if (line != 0 && val == line) {
            var tr = rdo.parent().parent();
            SetRow(tr[0].id);
            return;
        }
    });
}
// END ADDED /////////////////////////////////////////////////////

// Function corrected on 03/07/2013 : ////////////////////////////
function ChangeRowColor(rowId, mouseLocation) {
    var tr = $('#' + rowId);
    var rdo;
    if (tr != null && tr != undefined) {
        rdo = tr.find('input:radio');
    }
    else {
        rdo = $('#radio_' + rowId);
        tr = rdo.parent().parent();
    }

    resetRowsColor(mouseLocation);   // added on 18/07/2013 to forse color reset for unselected rows, optional
    if (!rdo.is(':checked')) {
        if (mouseLocation == 'mouseOver') {
            if (tr.css("background-color") != "rgb(255,255,0)") {
                tr.css("background-color", "#D2E4E4");   // "yellow"  juju
            } // end if on color check 
        } // end if on mouse location 
        else { // if (mouseLocation == 'mouseOut')
            if (tr.css("background-color") == "rgb(255,255,0)") {
                tr.css("background-color", "");
            } // end if on color check
        } // end else on mouse location
    } // end if-checked block
} // end function

// added on 18/07/2013 to forse color reset for unselected rows /////
function resetRowsColor(mouseLocation) {
    if (mouseLocation == 'mouseOver') {
        $('#availablesAppointmentsGridView tr').each(function () {
            var row = $(this);
            var rdo = row.find('input:radio');
            if (!rdo.is(':checked')) {
                row.css("background-color", "");
            }
        });
    }
}
// end additions and corrections ///////////////////////////////////////////////////

function Validate() {
    var errorFound = false;
    // corrected on 11/06/2013
    if (isValidDate('#startDateTextBox', '#dateErrorLabel') == false) {
        errorFound = true;
    }
    //    else {
    //        if (isValidDate('aspnetForm', 'endDateTextBox', 'dateErrorLabel') == false) {
    //            errorFound = true;
    //        }
    //    }

    if (errorFound) return false;
}

// corrected on 11/06/2013
function isValidDate(textBoxControl, errorLabel) {
    // Checks for the following valid date formats:
    // MM/DD/YY   MM/DD/YYYY   MM-DD-YY   MM-DD-YYYY
    // Also separates date into month, day, and year variables

    var datePattern = '/^(\d{1,2})(\/|-)(\d{1,2})\2(\d{2}|\d{4})$/';

    // To require a 4 digit year entry, use this line instead:
    var textValue = $(textBoxControl).val();    // document.forms[form_id].elements[textBoxControl].value;
    var matchArray = textValue.match(datePattern); // is the format ok?
    if (matchArray == null) {
        $(errorLabel).text('תאריך שגוי');
        $(errorLabel).css('visibility', 'visible');
        $(textBoxControl).css('border', '1px solid #CC0000');  //  2px red   juju
        return false;
    }
    else {
        $(errorLabel).css('visibility', 'hidden');
        $(textBoxControl).css('border', '1px solid #666666');   //  2px black   juju
        return true;  // date is valid
    }
}

// function corrected on 11/06/2013
function ValidateDropDownList() {
    var returnValue = true;

    if ($('#treatmentGroupDropDownList') != null && $('#treatmentGroupRow').is(':visible')) {
        if ($('#treatmentGroupDropDownList').val() == '0') {
            $('#treatmentGroupErrorLabel').css('visibility', 'visible');
            $('#treatmentGroupErrorLabel').focus(); // ?
            returnValue = false;
        }
        else {
            $('#treatmentGroupErrorLabel').css('visibility', 'hidden');
        }
    }       

    if ($('#operDropDownList').val() == '0') {
        $('#operErrorLabel').css('visibility', 'visible');
        $('#operErrorLabel').focus();
        returnValue = false;
    }
    else {
        $('#operErrorLabel').css('visibility', 'hidden');
    }

    if ($('#siteDropDownList').val() == '0') {
        $('#siteErrorLabel').css('visibility', 'visible');
        $('#siteErrorLabel').focus();
        returnValue = false;
    }
    else {
        $('#siteErrorLabel').css('visibility', 'hidden');
    }

    if ($('#insuranceDropDownList').val() == '0') {
        $('#insuranceErrorLabel').css('visibility', 'visible');
        $('#insuranceErrorLabel').focus();
        returnValue = false;
    }
    else {
        $('#insuranceErrorLabel').css('visibility', 'hidden');
    }
    
    return returnValue;
}

// function corrected on 11/06/2013
function SetSearchDisplay() {
    $('#next').css('visibility', 'hidden');
    $('#loaderImage').css('visibility', 'visible');
    $('#selectAppointmentTiltleLabel').css('visibility', 'hidden');
    $('#availablesAppointmentsGridView').css('visibility', 'hidden');
    // getAvailableAppointments();  // commented on 18/08/2013
    // getTreatmentInstructions();  // commented on 18/08/2013
    // setGridViewPaginator('#availablesAppointmentsGridView', 100);  // test
}

function checkUserChoice() {
    var checked = false;
    $.each($('#availablesAppointmentsGridView tr td input:radio'), function () {
        if ($(this).attr("checked") == 'checked' || $(this).is(':checked')) {
            getChosenRowData(this);
            checked = true;
        }
    });

    return checked;
}

function getChosenRowData(rdoObj) { // corrected on 20/08/2013
    var date = $(rdoObj).parent().nextAll().eq(0).text();
    var time = $(rdoObj).parent().nextAll().eq(1).text();
    var day  = $(rdoObj).parent().nextAll().eq(2).text();
    var dline = $(rdoObj).parent().prevAll().eq(0).text();
    if (dline == undefined || dline == null || dline == "")
        dline = $(rdoObj).val();
    var operVal = $('#operDropDownList option:selected').val();
    var operTxt = $('#operDropDownList option:selected').text();
    var siteVal = $('#siteDropDownList option:selected').val();
    var siteTxt = $('#siteDropDownList option:selected').text();
    var insrVal = $('#insuranceDropDownList option:selected').val();
    var insrTxt = $('#insuranceDropDownList option:selected').text();
    // if (operInstructions == null) {
    //  operInstructions = "";
    //}
    
    var param = "operVal=" + operVal + ';' +
                "operTxt=" + operTxt + ';' +
                "siteVal=" + siteVal + ';' +
                "siteTxt=" + siteTxt + ';' +
                "insrVal=" + insrVal + ';' +
                "insrTxt=" + insrTxt + ';' +
                "dline=" + dline + ';' +
                "date=" + date + ';' +
                "time=" + time + ';' +
                "day=" + day;   // + ';' + 
                // "instructions=" + operInstructions;

    $('#selectedGridRadioHidden').val(param);
}

function getTreatmentInstructions() {
        var operVal = $('#operDropDownList option:selected').val();
        // var service = DIR + "TreatmentInstruction.asmx/" + "GetTreatmentInstructions";   // for local (built-in) services only
        var service = GetServicePath(isRemote, directory, ipAddress, port, FILE1, "GetTreatmentInstructions");
        var data = "{ jsonOperCode:" + parseInt(operVal, 10) + " }";
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
        async: true,
        crossDomain: true,      // Added for cross-browther support
        processData: false,
        success: function (data) {
            //setTreatmentInstructions(data.d);
            operInstructions = data.d;
        },  // end success function (data)

        error: function (xhr, status, error) { // start error fuction
            //alert("Ajax completion gave " + status + ":\n" + error);
            var errDescr = (xhr.responseText != undefined && !String.isNullOrEmpty(xhr.responseText)) ?
                /*JSON.parse*/(xhr.responseText) : null;
            onAjaxError(xhr, status, error, errDescr);
            // writeLog(errDescr, xhr.statusText, error);   // optional
        }   // end error function
    });    // end ajax part
}

function savePagePreprintData() {
    var curTreatment = $("#operDropDownList").val();
    var curTreatSite = $("#siteDropDownList").val();
    var curInsurance = $("#insuranceDropDownList").val();
    var curOperGroup = $("#treatmentGroupDropDownList").is(":visible") ? 
                        $("#treatmentGroupDropDownList").val() : "0";
    var curDay1 = $('#sundayCheckBox').is(':checked') ? 1 : 0;
    var curDay2 = $('#mondayCheckBox').is(':checked') ? 1 : 0;
    var curDay3 = $('#tuesdayCheckBox').is(':checked') ? 1 : 0;
    var curDay4 = $('#wednesdayCheckBox').is(':checked') ? 1 : 0;
    var curDay5 = $('#thursdayCheckBox').is(':checked') ? 1 : 0;
    var curDay6 = $('#fridayCheckBox').is(':checked') ? 1 : 0;
    var curDay7 = $('#saturdayCheckBox').is(':checked') ? 1 : 0;

    var param = "oper=" + curTreatment + ';' +
                "site=" + curTreatSite + ';' +
                "insur=" + curInsurance + ';' +
                "group=" + curOperGroup + ';' +
                "sun=" + curDay1 + ';' +
                "mon=" + curDay2 + ';' +
                "tue=" + curDay3 + ';' +
                "wed=" + curDay4 + ';' +
                "thu=" + curDay5 + ';' +
                "fri=" + curDay6 + ';' +
                "sat=" + curDay7;
    $('#preprintPageDataHidden').val(param);
}

function updateSiteList() {

    var operValue = $('#operDropDownList option:selected').val();   // .attr('value');
    var operIndex = $('#operDropDownList option:selected').index();
    if (operIndex > 0) {
        getSiteList(operValue);
    }
    else {
        $('#siteDropDownList').empty();
        $('#siteDropDownList').prop('disabled', true);
    }
}

function updatePreprintData() { 
    var data = $('#preprintPageDataHidden').val();
    var dataArray = data.split(';');
    var pos = -1;
    var len = dataArray.length;

    for(var i = 0; i < len; i++) {
        var item = dataArray[i];
        pos = item.indexOf('=');
        var key = item.substring(0, pos);
        var val = item.substring(pos + 1);
        switch (key)
        {
            case "oper":
                operCode = val;
                break;
            case "site":
                siteCode = val;
                break;
            case "insur":
                insurCode = val;
                break;
            case "group":
                if ($("#treatmentGroupDropDownList").is(":visible")) {
                    $("#treatmentGroupDropDownList option:selected").val(val);
                    groupCode = val;
                }
                break;
            case "sun":
                checkSun = (val == "1") ? true : false;
                break;
            case "mon":
                checkMon = (val == "1") ? true : false;
                break;
            case "tue":
                checkTue = (val == "1") ? true : false;
                break;
            case "wed":
                checkWed = (val == "1") ? true : false;
                break;
            case "thu":
                checkThu = (val == "1") ? true : false;
                break;
            case "fri":
                checkFri = (val == "1") ? true : false;
                break;
            case "sat":
                checkSat = (val == "1") ? true : false;
                break;
        }
    }
}

function updatePostPrintData() {
    if (isPostBack == "true") {
        updateDowCheckboxes();
        updateSelectedAppointmentRow();
    }
    else {
        $("input:checkbox").attr("checked", true);
    }
}

function updateDowCheckboxes() {
    $("#sundayCheckBox").prop("checked", checkSun);
        $("#mondayCheckBox").prop("checked", checkMon);
        $("#tuesdayCheckBox").prop("checked", checkTue);
        $("#wednesdayCheckBox").prop("checked", checkWed);
        $("#thursdayCheckBox").prop("checked", checkThu);
        $("#fridayCheckBox").prop("checked", checkFri);
        $("#saturdayCheckBox").prop("checked", checkSat);
}

function updateSelectedAppointmentRow() {
    $.each($('#availablesAppointmentsGridView tr td input:radio'), function () {
        if ($(this).attr("checked") == 'checked' || $(this).is(':checked')) {
            $(this).parent().parent().css('background-color', '#82B9FA');
            $('#next').show();
            $('#next').css('visibility', 'visible');
        }
    });
}