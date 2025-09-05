function DelayRedirect(url) {
    var timeOut = setTimeout("window.location='" + url+"'", 5000);
}