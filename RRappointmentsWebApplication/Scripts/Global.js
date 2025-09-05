var ID_LENGTH = 9;
var LB_ERR = "-1";
var DIR = "WebServices/";

function isNumeric(input) {
    return (isFinite(input) && !isNaN(parseInt(input)));
}

String.isNullOrEmpty = function (value) {
    var ret = true;

    if (value != null) {
        if (typeof (value) == 'string') {
            if (value.length > 0)
                ret = false;
        }
    }

    return ret;
}

function MM_swapImgRestore() { //v3.0
    var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
}

function MM_preloadImages() { //v3.0
    var d = document; if (d.images) {
        if (!d.MM_p) d.MM_p = new Array();
        var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
            if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
    }
}

function MM_findObj(n, d) { //v4.01
    var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
        d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
    }
    if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
    for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
    if (!x && d.getElementById) x = d.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
    var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2); i += 3)
        if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
}

function enter_onclick() {

}

// added on 21/07/2013
function GetServicePath(isRemote, directory, ipAddress, port, file, method) { 
    var path = "";
    if (directory == undefined || directory == null) {
        directory = DIR;
    }
    if (isRemote == undefined || isRemote == null || isRemote == "false") {
        if (directory != DIR) {
            directory = DIR;
        }
        path = directory  + file + '/' + method;
    }
    else {
        path = "http://" + ipAddress + port + '/' + directory + file + '/' /*"?op="*/ + method;
    }

    return path;
}

// added on 29/07/2013
function onAjaxError(xhr, status, error, description) {
    var errMsg = "";
    if (xhr.status == 0) {
        errMsg = "Network error. Not connected.";
    }
    else if (xhr.status == 404) {
        errMsg = "Requested page not found.\n(Error [404])";
    }
    else if (xhr.status == 500) {
        errMsg = "Internal Server Error.\n(Error [500])";
    }
    else if (status == 'parsererror') {
        errMsg = "Requested JSON parse failed.\nXML/JSON format is bad.";
    }
    else if (status == 'timeout') {
        errMsg = "Time out error.";
    }
    else if (status == 'abort') {
        errMsg = "Ajax request aborted.";
    }
    else if (status == 'notmodified') {
        errMsg = "Request was not modified but was not retrieved from the cache.";
    }
    else {
        errMsg = "Uncaught Error.\n" + xhr.statusText;
    }

    errMsg = "AJAX request caused " + status + ":\n" + errMsg;
    if (description != undefined && description != null) {
        if (description.Message != undefined && !String.isNullOrEmpty(description.Message)) {
            errMsg += '\n' + description.Message;
        }
    }

    alert(errMsg);
}

// added on 05/08/2013
function writeLog(description, status, error)
{
    var now = new Date();
    var date = now.format("dd.MM.yyyy");
    var time = now.format("HH:mm");
    var fileName = "//dc1/E/diskf/Zimon/Log/service_(";
    fileName += date;
    fileName += ").txt";
    var line = time;
    line += " - ";
    if (status != undefined && status != null)
    {
        line += status + ". ";
    }
    else
    {
        if (error != undefined && error != null)
        line += error + ". ";
    }
    if (description != undefined && description != null)
    {
        if (description.Message != undefined && description.Message != null)
            line += description.Message;
    }

    //var data = '{jsonLogLine:"' + line + '" }';
    var data = '{ jsonFileName:"' + fileName + '", jsonLogLine:"' + line + '" }';
    var service = "step1.aspx/WriteLog";
    callAjaxWriteLog(service, data);
}

// ADDED ON 01/10/2013 : ///////////////////
function isValidIdNumber(userId) {
    var id = parseInt(userId, 10);
    var mod = id % 10;
    var sw1_2 = 2;
    var result = 0;
    var tmpMod = 0;
    for (var i = 1; i <= 8; i++) {
        id = Math.floor(id / 10);
        tmpMod = Math.floor(id % 10);
        tmpMod *= sw1_2;
        if (sw1_2 === 1) {
            sw1_2 = 2
        }
        else {
            sw1_2 = 1;
        }
        result += tmpMod;
        if (tmpMod >= 10) {
            result -= 9;
        }
    } // end loop

    var resMod = result % 10;
    if (resMod > 0) {
        result = 10 - resMod;
    }
    else {
        result = 0;
    }

    if (result === mod)
        return true;

    return false;
}