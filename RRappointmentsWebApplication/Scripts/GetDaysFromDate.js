function GetDaysFromDate() {
    var day;
    var startDate, endDate;

    document.getElementById('sundayCheckBox').checked = false;
    document.getElementById('mondayCheckBox').checked = false;
    document.getElementById('tuesdayCheckBox').checked = false;
    document.getElementById('wednesdayCheckBox').checked = false;
    document.getElementById('thursdayCheckBox').checked = false;
    document.getElementById('fridayCheckBox').checked = false;
    document.getElementById('saturdayCheckBox').checked = false;

    startDate = document.getElementById('startDateTextBox').value;
    endDate   =   document.getElementById('endDateTextBox').value;

    startDate = startDate.substr(3, 2) + '/' + startDate.substr(0, 2) + '/' + startDate.substr(6, 4);
    endDate   = endDate.substr(3, 2) + '/' + endDate.substr(0, 2) + '/' + endDate.substr(6, 4);

    startDate = new Date(startDate.toString());
    endDate = new Date(endDate.toString());

    while (startDate <= endDate) {
        day = startDate.getDay();

        if (day == 0) document.getElementById('sundayCheckBox').checked = true;
        if (day == 1) document.getElementById('mondayCheckBox').checked = true;
        if (day == 2) document.getElementById('tuesdayCheckBox').checked = true;
        if (day == 3) document.getElementById('wednesdayCheckBox').checked = true;
        if (day == 4) document.getElementById('thursdayCheckBox').checked = true;
        if (day == 5) document.getElementById('fridayCheckBox').checked = true;
        if (day == 6) document.getElementById('saturdayCheckBox').checked = true;

        startDate = new Date(startDate.getTime() + 86400000);
    }
}