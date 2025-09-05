function ChangeEbrewToEnglish(x)
{
var y=document.getElementById(x).value;
y=y.replace("א", "t");
y=y.replace("ב", "c");
y = y.replace("ג", "d");
y = y.replace("ד", "s");
y = y.replace("ה", "v");
y = y.replace("ו", "u");
y = y.replace("ז", "z");
y = y.replace("ח", "j");
y = y.replace("ט", "y");
y = y.replace("י", "h");
y = y.replace("כ", "f");
y = y.replace("ל", "k");
y = y.replace("מ", "n");
y = y.replace("נ", "b");
y = y.replace("ס", "x");
y = y.replace("ע", "g");
y = y.replace("פ", "p");
y = y.replace("צ", "m");
y = y.replace("ק", "e");
y = y.replace("ר", "r");
y = y.replace("ש", "a");
y = y.replace("ת", ",");
y = y.replace("ם", "o");
y = y.replace("ן", "i");
y = y.replace("ף", ";");
y = y.replace("ץ", ".");
y = y.replace("כ", "f");
y = y.replace("ך", "l");
y = y.replace("'", "w");
y = y.replace("/", "q");
document.getElementById(x).value=y
}

//ChangeEbrewToEnglish(this.id)
