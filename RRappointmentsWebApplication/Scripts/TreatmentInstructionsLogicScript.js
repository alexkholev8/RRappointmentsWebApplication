$().ready(function () {

    getTreatmentInstructions();

});   // end document.ready section

var DIR = "WebServices/";
var FILE = "TreatmentInstruction.asmx/";
var operCode = null;

function getTreatmentInstructions() {
    if (operCode == -1) return;
    var service = DIR + FILE + "GetTreatmentInstructions";
    var data = "{ jsonOperCode:" + parseInt(treatCode, 10) + " }";
    callAjax(service, data);
}

function callAjax(service, data) {
    $.ajax({    // start ajax section
        url: service,
        data: data, // single parameter pass
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        cache: false,
        async: true,
        success: function (data) {
            setTreatmentInstructions(data.d);
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

function setTreatmentInstructions(data) {
    if (data == null || data == undefined) {
        $('#intructionTitleLabel').hide();
        $('#treatmentInstructionsTextBox').hide();
    }
    else {
        $('#intructionTitleLabel').show();
        $('#treatmentInstructionsTextBox').val(data);
    }
    operCode = -1;
}