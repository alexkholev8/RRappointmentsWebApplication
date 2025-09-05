function DisableEnterKeyPopUp() {
    var keycode;
    if (window.event) keycode = window.event.keyCode;
    if(keycode == 13){
    return false;
    }
    return true;
}



function SetHelpPopUp(controlId) {

    if (controlId == 'userIdQuestionImageButton') {
        document.getElementById('helpPopupTitleLabel').innerText = 'עזרה - תעודת זהות';
        document.getElementById('helpPopupContentLabel').innerText = 'יש להזין מספר תעודת זהות כולל ספרת ביקורת';
        document.getElementById('helpPopupContentLabel').style.fontSize = 'small';
    //    document.getElementById('fieldInfoPopUpPanel').visibility = 'visible';
    }

    if (controlId == 'passwordQuestionImageButton') {
        document.getElementById('helpPopupTitleLabel').innerText = 'עזרה - סיסמא';
        document.getElementById('helpPopupContentLabel').innerText = 'יש להזין סיסמא כפי שנשלחה לך במסרון';
        document.getElementById('helpPopupContentLabel').style.fontSize = 'small';
    }
    
    if (controlId == 'firstNameQuestionImageButton') {
        document.getElementById('helpPopupTitleLabel').innerText = 'עזרה - שם פרטי';
        document.getElementById('helpPopupContentLabel').innerText = 'יש להזין את שמך הפרטי';
        document.getElementById('helpPopupContentLabel').style.fontSize = 'small';
    }

    if (controlId == 'lastNameQuestionImageButton') {
        document.getElementById('helpPopupTitleLabel').innerText = 'עזרה - שם משפחה';
        document.getElementById('helpPopupContentLabel').innerText = 'יש להזין את שם משפחתך';
        document.getElementById('helpPopupContentLabel').style.fontSize = 'small';
    }

    if (controlId == 'userId2QuestionImageButton') {
        document.getElementById('helpPopupTitleLabel').innerText = 'עזרה - תעודת זהות';
        document.getElementById('helpPopupContentLabel').innerText = 'יש להזין מספר תעודת זהות כולל ספרת ביקורת. תעודת הזהות תשמש כאמצעי הזיהוי הראשון עם כניסתך למערכת';
        document.getElementById('helpPopupContentLabel').style.fontSize = '12px';
    }

    if (controlId == 'emailQuestionImageButton') {
        document.getElementById('helpPopupTitleLabel').innerText = 'עזרה - כתובת דוא"ל';
        document.getElementById('helpPopupContentLabel').innerText = 'יש להזין את כתובת הדואר האלקטרוני שברשותך. לדוא"ל ישלחו פרטי התור, שיקבע ומידע אישי';
        document.getElementById('helpPopupContentLabel').style.fontSize = '12px';
    }
    if (controlId == 'phoneQuestionImageButton') {
        document.getElementById('helpPopupTitleLabel').innerText = 'עזרה - מספר טלפון נייד';
        document.getElementById('helpPopupContentLabel').innerText = 'יש להזין את מספר הטלפון הנייד. לטלפון הנייד ישלח מסרון ובו הסיסמא לכניסתך למערכת';
        document.getElementById('helpPopupContentLabel').style.fontSize = '12px';
    }
    return false;

  //  document.getElementById('fieldInfoPopUpPanel').style.visibility = 'visible';
}

function CloseHelpPopup() {
    document.getElementById('fieldInfoPopUpPanel').style.visibility = 'hidden';
}

function getText(id)
{
    var help;
    if (id == 'userIdTip') 
    {
        help = '<strong>' + 'עזרה - תעודת זהות' + '</strong><br/>' + 'יש להזין מספר תעודת זהות כולל ספרת ביקורת';
        document.getElementById(id).innerHTML = help;
    }
    else if (id == 'userPasswordTip') 
    {
        help = '<strong>' + 'עזרה - סיסמא' + '</strong><br/>' + 'יש להזין סיסמא כפי שנשלחה לך במסרון';
        document.getElementById(id).innerHTML = help;
    }
    else if (id == 'userId2Tip')
    {
        help = '<strong>' + 'עזרה - תעודת זהות' + '</strong><br/>' + 'יש להזין מספר תעודת זהות כולל ספרת ביקורת. תעודת הזהות תשמש כאמצעי הזיהוי הראשון עם כניסתך למערכת';
        document.getElementById(id).innerHTML = help;
    }
    else if (id == 'userEmailTip') {
        help = '<strong>' + 'עזרה - כתובת דוא"ל' + '</strong><br/>' + 'יש להזין את כתובת הדואר האלקטרוני שברשותך. לדוא"ל ישלחו את פרטי התור, שיקבע ומידע אישי';
        document.getElementById(id).innerHTML = help;
    }
    else if (id == 'userPhoneTip') 
    {
        help = '<strong>' + 'עזרה - מספר טלפון נייד' + '</strong><br/>' + 'יש להזין מספר טלפון נייד. לטלפון הנייד ישלח מסרון ובו הסיסמא לכניסתך למערכת';
        document.getElementById(id).innerHTML = help;
    }
        return false;
}



