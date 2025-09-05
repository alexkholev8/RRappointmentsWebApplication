using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Data.Odbc;
using System.Web.UI.WebControls;
using System.Text;

namespace RRappointmentsWebApplication.WebServices
{
    /// <summary>
    /// Summary description for NewAppointment
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class NewAppointment : System.Web.Services.WebService
    {
        // added on 10/07/2013: ////////////////////////////////
#region AppointmentMailData_class_definition
        [Serializable]
        public class AppointmentMailData
        {
            private const string SENDER_EMAIL = "NoReply@y-it.co.il";
            private const string CONTENT_TYPE = "text/html";
            private const string MAIL_SUBJECT = "זימון תור";
            private const string FILELIST_PATH = "דודו_files/filelist.xml";             // {0}
            private const string THEMEDATA_PATH = "דודו_files/themedata.thmx";          // {1}
            private const string CLRSCHEME_PATH = "דודו_files/colorschememapping.xml";  // {2}
            private const string GREETING_HEB = "שלום";                                 // {3}
            private const string CONTENT_SUBJECT_HEB = "להלן פרטי התור";               // {6}
            private const string APPOINT_SUBJECT_HEB = "שנקבע לך:";                     // {7}
            private const string NAME_HEB = "שם";                                        // {8}, {11}
            private const string OPER_HEB = "הבדיקה:";                                  // {9}
            private const string SITE_HEB = "המרפאה:";                                  // {12}
            private const string DATE_HEB = "תאריך:";                                   // {14}
            private const string DAYW_HEB = "יום:";                                     // {16}
            private const string TIME_HEB = "שעה:";                                     // {18}
            private const string INSTRUCTION1_HEB = "הנחיות";                          // {20}
            private const string INSTRUCTION2_HEB = "לבדיקה:";                         // {21}
            private const string MAIL_PATTERN = "<html>\x0D\x0A<head>\x0D\x0A<meta http-equiv=Content-Type content=\"text/html; charset=windows-1255\">\x0D\x0A<meta name=ProgId content=Word.Document>\x0D\x0A<meta name=Generator content=\"Microsoft Word 12\">\x0D\x0A<meta name=Originator content=\"Microsoft Word 12\">\x0D\x0A<link rel=File-List href=\"{0}\">\x0D\x0A<link rel=themeData href=\"{1}\">\x0D\x0A<link rel=colorSchemeMapping href=\"{2}\">\x0D\x0A\x0D\x0A<style>\x0D\x0A<!--\x0D\x0A\x0D\x0A @font-face\x0D\x0A\x09{font-family:\"Cambria Math\";\x0D\x0A\x09panose-1:2 4 5 3 5 4 6 3 2 4;\x0D\x0A\x09mso-font-charset:1;\x0D\x0A\x09mso-generic-font-family:roman;\x0D\x0A\x09mso-font-format:other;\x0D\x0A\x09mso-font-pitch:variable;\x0D\x0A\x09mso-font-signature:0 0 0 0 0 0;}\x0D\x0A@font-face\x0D\x0A\x09{font-family:Calibri;\x0D\x0A\x09panose-1:2 15 5 2 2 2 4 3 2 4;\x0D\x0A\x09mso-font-charset:0;\x0D\x0A\x09mso-generic-font-family:swiss;\x0D\x0A\x09mso-font-pitch:variable;\x0D\x0A\x09mso-font-signature:-1610611985 1073750139 0 0 159 0;}\x0D\x0A\x0D\x0A p.MsoNormal, li.MsoNormal, div.MsoNormal\x0D\x0A\x09{mso-style-unhide:no;\x0D\x0A\x09mso-style-qformat:yes;\x0D\x0A\x09mso-style-parent:\"\";\x0D\x0A\x09margin:0cm;\x0D\x0A\x09margin-bottom:.0001pt;\x0D\x0A\x09mso-pagination:widow-orphan;\x0D\x0A\x09font-size:11.0pt;\x0D\x0A\x09font-family:\"Calibri\",\"sans-serif\";\x0D\x0A\x09mso-fareast-font-family:Calibri;\x0D\x0A\x09mso-fareast-theme-font:minor-latin;\x0D\x0A\x09mso-bidi-font-family:\"Times New Roman\";}\x0D\x0Aa:link, span.MsoHyperlink\x0D\x0A\x09{mso-style-noshow:yes;\x0D\x0A\x09mso-style-priority:99;\x0D\x0A\x09color:blue;\x0D\x0A\x09text-decoration:underline;\x0D\x0A\x09text-underline:single;}\x0D\x0Aa:visited, span.MsoHyperlinkFollowed\x0D\x0A\x09{mso-style-noshow:yes;\x0D\x0A\x09mso-style-priority:99;\x0D\x0A\x09color:purple;\x0D\x0A\x09text-decoration:underline;\x0D\x0A\x09text-underline:single;}\x0D\x0Ap.MsoListParagraph, li.MsoListParagraph, div.MsoListParagraph\x0D\x0A\x09{mso-style-priority:34;\x0D\x0A\x09mso-style-unhide:no;\x0D\x0A\x09mso-style-qformat:yes;\x0D\x0A\x09margin-top:0cm;\x0D\x0A\x09margin-right:0cm;\x0D\x0A\x09margin-bottom:0cm;\x0D\x0A\x09margin-left:36.0pt;\x0D\x0A\x09margin-bottom:.0001pt;\x0D\x0A\x09mso-pagination:widow-orphan;\x0D\x0A\x09font-size:11.0pt;\x0D\x0A\x09font-family:\"Calibri\",\"sans-serif\";\x0D\x0A\x09mso-fareast-font-family:Calibri;\x0D\x0A\x09mso-fareast-theme-font:minor-latin;\x0D\x0A\x09mso-bidi-font-family:\"Times New Roman\";}\x0D\x0Aspan.EmailStyle18\x0D\x0A\x09{mso-style-type:personal;\x0D\x0A\x09mso-style-noshow:yes;\x0D\x0A\x09mso-style-unhide:no;\x0D\x0A\x09font-family:\"Calibri\",\"sans-serif\";\x0D\x0A\x09mso-ascii-font-family:Calibri;\x0D\x0A\x09mso-hansi-font-family:Calibri;\x0D\x0A\x09color:windowtext;}\x0D\x0Aspan.EmailStyle19\x0D\x0A\x09{mso-style-type:personal;\x0D\x0A\x09mso-style-noshow:yes;\x0D\x0A\x09mso-style-unhide:no;\x0D\x0A\x09font-family:\"Calibri\",\"sans-serif\";\x0D\x0A\x09mso-ascii-font-family:Calibri;\x0D\x0A\x09mso-hansi-font-family:Calibri;\x0D\x0A\x09color:#1F497D;}\x0D\x0A.MsoChpDefault\x0D\x0A\x09{mso-style-type:export-only;\x0D\x0A\x09mso-default-props:yes;\x0D\x0A\x09font-size:10.0pt;\x0D\x0A\x09mso-ansi-font-size:10.0pt;\x0D\x0A\x09mso-bidi-font-size:10.0pt;}\x0D\x0A@page Section1\x0D\x0A\x09{size:612.0pt 792.0pt;\x0D\x0A\x09margin:72.0pt 90.0pt 72.0pt 90.0pt;\x0D\x0A\x09mso-header-margin:36.0pt;\x0D\x0A\x09mso-footer-margin:36.0pt;\x0D\x0A\x09mso-paper-source:0;}\x0D\x0Adiv.Section1\x0D\x0A\x09{page:Section1;}\x0D\x0A-->\x0D\x0A</style>\x0D\x0A\x0D\x0A</head>\x0D\x0A\x0D\x0A<body lang=EN-US link=blue vlink=purple style=\"tab-interval:36.0pt\">\x0D\x0A\x0D\x0A<div class=Section1>\x0D\x0A\x0D\x0A<p class=MsoNormal style=\"text-autospace:none\"><o:p>&nbsp;</o:p></p>\x0D\x0A\x0D\x0A<p class=MsoNormal dir=RTL style=\"text-align:right;direction:rtl;unicode-bidi:\x0D\x0Aembed\"><b><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\"> {3}  {4} {5}</span></b></p>\x0D\x0A\x0D\x0A<p class=MsoNormal dir=RTL style=\"text-align:right;direction:rtl;unicode-bidi:\x0D\x0Aembed\"><span dir=RTL></span><b><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\"><span\x0D\x0Adir=RTL></span>&nbsp;</span></b><span lang=AR-SA style=\"font-family:\"Arial\",\"sans-serif\";\x0D\x0Amso-bidi-language:AR-SA\"><o:p></o:p></span></p>\x0D\x0A\x0D\x0A<p class=MsoNormal dir=RTL style=\"text-align:right;direction:rtl;unicode-bidi:\x0D\x0Aembed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\">{6}\x0D\x0A{7}</span><span dir=LTR style=\"font-family:\"Arial\",\"sans-serif\";\x0D\x0Amso-bidi-language:AR-SA\"><o:p></o:p></span></p>\x0D\x0A\x0D\x0A<p class=MsoNormal dir=RTL style=\"text-align:right;direction:rtl;unicode-bidi:\x0D\x0Aembed\"><span dir=RTL></span><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\"><span\x0D\x0Adir=RTL></span>&nbsp;</span><span lang=AR-SA style=\"font-family:\"Arial\",\"sans-serif\";\x0D\x0Amso-bidi-language:AR-SA\"><o:p></o:p></span></p>\x0D\x0A\x0D\x0A<div align=right>\x0D\x0A\x0D\x0A<table class=MsoNormalTable dir=rtl border=0 cellspacing=0 cellpadding=0\x0D\x0A style=\"margin-left:36.0pt;border-collapse:collapse;mso-yfti-tbllook:1184;\x0D\x0A mso-padding-alt:0cm 0cm 0cm 0cm;mso-table-dir:bidi\">\x0D\x0A <tr style=\"mso-yfti-irow:0;mso-yfti-firstrow:yes\">\x0D\x0A  <td width=118 valign=top style=\"width:88.85pt;border:solid black 1.0pt;\x0D\x0A  padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:right;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><b><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\">{8}\x0D\x0A  {9}</span></b></p>\x0D\x0A  </td>\x0D\x0A  <td width=222 valign=top style=\"width:166.5pt;border:solid black 1.0pt;\x0D\x0A  border-right:none;padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:justify;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\";\x0D\x0A  color:#1F497D\">{10}</span></p>\x0D\x0A  </td>\x0D\x0A </tr>\x0D\x0A <tr style=\"mso-yfti-irow:1\">\x0D\x0A  <td width=118 valign=top style=\"width:88.85pt;border:solid black 1.0pt;\x0D\x0A  border-top:none;padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:right;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\">{11}\x0D\x0A  {12}</span></p>\x0D\x0A  </td>\x0D\x0A  <td width=222 valign=top style=\"width:166.5pt;border-top:none;border-left:\x0D\x0A  solid black 1.0pt;border-bottom:solid black 1.0pt;border-right:none;\x0D\x0A  padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:right;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\";\x0D\x0A  color:#1F497D\">{13}</span></p>\x0D\x0A  </td>\x0D\x0A </tr>\x0D\x0A <tr style=\"mso-yfti-irow:2\">\x0D\x0A  <td width=118 valign=top style=\"width:88.85pt;border:solid black 1.0pt;\x0D\x0A  border-top:none;padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:right;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\">{14}</span></p>\x0D\x0A  </td>\x0D\x0A  <td width=222 valign=top style=\"width:166.5pt;border-top:none;border-left:\x0D\x0A  solid black 1.0pt;border-bottom:solid black 1.0pt;border-right:none;\x0D\x0A  padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:right;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><span dir=LTR>{15}</span></p>\x0D\x0A  </td>\x0D\x0A </tr>\x0D\x0A <tr style=\"mso-yfti-irow:3\">\x0D\x0A  <td width=118 valign=top style=\"width:88.85pt;border:solid black 1.0pt;\x0D\x0A  border-top:none;padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:right;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\">{16}</span></p>\x0D\x0A  </td>\x0D\x0A  <td width=222 valign=top style=\"width:166.5pt;border-top:none;border-left:\x0D\x0A  solid black 1.0pt;border-bottom:solid black 1.0pt;border-right:none;\x0D\x0A  padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:right;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><span dir=LTR style=\"font-family:\"Arial\",\"sans-serif\"\">{17}</span></p>\x0D\x0A  </td>\x0D\x0A </tr>\x0D\x0A <tr style=\"mso-yfti-irow:4;mso-yfti-lastrow:yes\">\x0D\x0A  <td width=118 valign=top style=\"width:88.85pt;border:solid black 1.0pt;\x0D\x0A  border-top:none;padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:right;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\">{18}</span></p>\x0D\x0A  </td>\x0D\x0A  <td width=222 valign=top style=\"width:166.5pt;border-top:none;border-left:\x0D\x0A  solid black 1.0pt;border-bottom:solid black 1.0pt;border-right:none;\x0D\x0A  padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:right;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><span dir=LTR>{19}</span></p>\x0D\x0A  </td>\x0D\x0A </tr>\x0D\x0A</table>\x0D\x0A\x0D\x0A</div>\x0D\x0A\x0D\x0A<p class=MsoListParagraph dir=RTL style=\"margin-top:0cm;margin-right:36.0pt;\x0D\x0Amargin-bottom:0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:right;\x0D\x0Adirection:rtl;unicode-bidi:embed\"><span dir=LTR>&nbsp;</span><span lang=AR-SA\x0D\x0Astyle=\"font-family:\"Arial\",\"sans-serif\";mso-bidi-language:AR-SA\"><o:p></o:p></span></p>\x0D\x0A\x0D\x0A<p class=MsoListParagraph dir=RTL style=\"margin-top:0cm;margin-right:36.0pt;\x0D\x0Amargin-bottom:0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:right;\x0D\x0Adirection:rtl;unicode-bidi:embed\"><b><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\">{20}\x0D\x0A{21}</span></b><span lang=AR-SA style=\"font-family:\"Arial\",\"sans-serif\";\x0D\x0Amso-bidi-language:AR-SA\"><o:p></o:p></span></p>\x0D\x0A\x0D\x0A<p class=MsoListParagraph dir=RTL style=\"margin-top:0cm;margin-right:36.0pt;\x0D\x0Amargin-bottom:0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:right;\x0D\x0Adirection:rtl;unicode-bidi:embed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\";\x0D\x0Acolor:#1F497D\">{22}</span><span lang=AR-SA style=\"font-family:\x0D\x0A\"Arial\",\"sans-serif\";mso-bidi-language:AR-SA\"><o:p></o:p></span></p>\x0D\x0A\x0D\x0A<p class=MsoListParagraph dir=RTL style=\"margin-top:0cm;margin-right:36.0pt;\x0D\x0Amargin-bottom:0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:right;\x0D\x0Adirection:rtl;unicode-bidi:embed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\">&nbsp;</span><span\x0D\x0Alang=AR-SA style=\"font-family:\"Arial\",\"sans-serif\";mso-bidi-language:AR-SA\"><o:p></o:p></span></p>\x0D\x0A\x0D\x0A<p class=MsoListParagraph dir=RTL style=\"margin-top:0cm;margin-right:36.0pt;\x0D\x0Amargin-bottom:0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:right;\x0D\x0Adirection:rtl;unicode-bidi:embed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\">&nbsp;</span><span\x0D\x0Alang=AR-SA style=\"font-family:\"Arial\",\"sans-serif\";mso-bidi-language:AR-SA\"><o:p></o:p></span></p>\x0D\x0A\x0D\x0A</div>\x0D\x0A\x0D\x0A</body>\x0D\x0A\x0D\x0A</html>\x0D\x0A";
            
            private string userEmail;
            private string emailMessage;
            private string emailSubject;
            // ADDED ON 20/08/2013 : //
            private string senderEmail;
            private string contentType;
            // END ADDED //////////////

            public string EmailSubject
            {
                get { return emailSubject; }
                set { emailSubject = value; }
            }

            public string UserEmail
            {
                get { return userEmail; }
                set { userEmail = value; }
            }

            public string EmailMessage
            {
                get { return emailMessage; }
                set { emailMessage = value; }
            }

            // ADDED ON 20/08/2013 : ////////////
            public string SenderEmail
            {
                get { return senderEmail; }
                set { senderEmail = SENDER_EMAIL; }
            }

            public string ContentType
            {
                get { return contentType; }
                set { contentType = CONTENT_TYPE; }
            }
            // END ADDED ////////////////////////

            public AppointmentMailData()
            { 
            
            }

            public AppointmentMailData(string firstName, string lastName, string operName, string branchName, string dDate, string dDay, string dTime, string instruction, string userEmail)
            {
                UserEmail = userEmail;
                // EmailSubject = Generic.ConvertToUTF8(MAIL_SUBJECT);  // DISCARDED ON 25/09/2013
                EmailSubject = MAIL_SUBJECT;                            // CORRECTED ON 25/09/2013

                StringBuilder sb = new StringBuilder("");
                sb.Append("<html>\x0D\x0A<head>\x0D\x0A<meta http-equiv=Content-Type content=\"text/html; charset=windows-1255\">\x0D\x0A<meta name=ProgId content=Word.Document>\x0D\x0A<meta name=Generator content=\"Microsoft Word 12\">\x0D\x0A<meta name=Originator content=\"Microsoft Word 12\">\x0D\x0A<link rel=File-List href=\"");
                sb.Append(FILELIST_PATH);
                sb.Append("\">\x0D\x0A<link rel=themeData href=\"");
                sb.Append(THEMEDATA_PATH);
                sb.Append("\">\x0D\x0A<link rel=colorSchemeMapping href=\"");
                sb.Append(CLRSCHEME_PATH);
                sb.Append("\">\x0D\x0A\x0D\x0A<style>\x0D\x0A<!--\x0D\x0A\x0D\x0A @font-face\x0D\x0A\x09{font-family:\"Cambria Math\";\x0D\x0A\x09panose-1:2 4 5 3 5 4 6 3 2 4;\x0D\x0A\x09mso-font-charset:1;\x0D\x0A\x09mso-generic-font-family:roman;\x0D\x0A\x09mso-font-format:other;\x0D\x0A\x09mso-font-pitch:variable;\x0D\x0A\x09mso-font-signature:0 0 0 0 0 0;}\x0D\x0A@font-face\x0D\x0A\x09{font-family:Calibri;\x0D\x0A\x09panose-1:2 15 5 2 2 2 4 3 2 4;\x0D\x0A\x09mso-font-charset:0;\x0D\x0A\x09mso-generic-font-family:swiss;\x0D\x0A\x09mso-font-pitch:variable;\x0D\x0A\x09mso-font-signature:-1610611985 1073750139 0 0 159 0;}\x0D\x0A\x0D\x0A p.MsoNormal, li.MsoNormal, div.MsoNormal\x0D\x0A\x09{mso-style-unhide:no;\x0D\x0A\x09mso-style-qformat:yes;\x0D\x0A\x09mso-style-parent:\"\";\x0D\x0A\x09margin:0cm;\x0D\x0A\x09margin-bottom:.0001pt;\x0D\x0A\x09mso-pagination:widow-orphan;\x0D\x0A\x09font-size:11.0pt;\x0D\x0A\x09font-family:\"Calibri\",\"sans-serif\";\x0D\x0A\x09mso-fareast-font-family:Calibri;\x0D\x0A\x09mso-fareast-theme-font:minor-latin;\x0D\x0A\x09mso-bidi-font-family:\"Times New Roman\";}\x0D\x0Aa:link, span.MsoHyperlink\x0D\x0A\x09{mso-style-noshow:yes;\x0D\x0A\x09mso-style-priority:99;\x0D\x0A\x09color:blue;\x0D\x0A\x09text-decoration:underline;\x0D\x0A\x09text-underline:single;}\x0D\x0Aa:visited, span.MsoHyperlinkFollowed\x0D\x0A\x09{mso-style-noshow:yes;\x0D\x0A\x09mso-style-priority:99;\x0D\x0A\x09color:purple;\x0D\x0A\x09text-decoration:underline;\x0D\x0A\x09text-underline:single;}\x0D\x0Ap.MsoListParagraph, li.MsoListParagraph, div.MsoListParagraph\x0D\x0A\x09{mso-style-priority:34;\x0D\x0A\x09mso-style-unhide:no;\x0D\x0A\x09mso-style-qformat:yes;\x0D\x0A\x09margin-top:0cm;\x0D\x0A\x09margin-right:0cm;\x0D\x0A\x09margin-bottom:0cm;\x0D\x0A\x09margin-left:36.0pt;\x0D\x0A\x09margin-bottom:.0001pt;\x0D\x0A\x09mso-pagination:widow-orphan;\x0D\x0A\x09font-size:11.0pt;\x0D\x0A\x09font-family:\"Calibri\",\"sans-serif\";\x0D\x0A\x09mso-fareast-font-family:Calibri;\x0D\x0A\x09mso-fareast-theme-font:minor-latin;\x0D\x0A\x09mso-bidi-font-family:\"Times New Roman\";}\x0D\x0Aspan.EmailStyle18\x0D\x0A\x09{mso-style-type:personal;\x0D\x0A\x09mso-style-noshow:yes;\x0D\x0A\x09mso-style-unhide:no;\x0D\x0A\x09font-family:\"Calibri\",\"sans-serif\";\x0D\x0A\x09mso-ascii-font-family:Calibri;\x0D\x0A\x09mso-hansi-font-family:Calibri;\x0D\x0A\x09color:windowtext;}\x0D\x0Aspan.EmailStyle19\x0D\x0A\x09{mso-style-type:personal;\x0D\x0A\x09mso-style-noshow:yes;\x0D\x0A\x09mso-style-unhide:no;\x0D\x0A\x09font-family:\"Calibri\",\"sans-serif\";\x0D\x0A\x09mso-ascii-font-family:Calibri;\x0D\x0A\x09mso-hansi-font-family:Calibri;\x0D\x0A\x09color:#1F497D;}\x0D\x0A.MsoChpDefault\x0D\x0A\x09{mso-style-type:export-only;\x0D\x0A\x09mso-default-props:yes;\x0D\x0A\x09font-size:10.0pt;\x0D\x0A\x09mso-ansi-font-size:10.0pt;\x0D\x0A\x09mso-bidi-font-size:10.0pt;}\x0D\x0A@page Section1\x0D\x0A\x09{size:612.0pt 792.0pt;\x0D\x0A\x09margin:72.0pt 90.0pt 72.0pt 90.0pt;\x0D\x0A\x09mso-header-margin:36.0pt;\x0D\x0A\x09mso-footer-margin:36.0pt;\x0D\x0A\x09mso-paper-source:0;}\x0D\x0Adiv.Section1\x0D\x0A\x09{page:Section1;}\x0D\x0A-->\x0D\x0A</style>\x0D\x0A\x0D\x0A</head>\x0D\x0A\x0D\x0A<body lang=EN-US link=blue vlink=purple style=\"tab-interval:36.0pt\">\x0D\x0A\x0D\x0A<div class=Section1>\x0D\x0A\x0D\x0A<p class=MsoNormal style=\"text-autospace:none\"><o:p>&nbsp;</o:p></p>\x0D\x0A\x0D\x0A<p class=MsoNormal dir=RTL style=\"text-align:right;direction:rtl;unicode-bidi:\x0D\x0Aembed\"><b><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\">");
                sb.Append(GREETING_HEB);
                sb.Append("  ");
                sb.AppendFormat("{0} {1}", firstName, lastName);
                sb.Append("</span></b></p>\x0D\x0A\x0D\x0A<p class=MsoNormal dir=RTL style=\"text-align:right;direction:rtl;unicode-bidi:\x0D\x0Aembed\"><span dir=RTL></span><b><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\"><span\x0D\x0Adir=RTL></span>&nbsp;</span></b><span lang=AR-SA style=\"font-family:\"Arial\",\"sans-serif\";\x0D\x0Amso-bidi-language:AR-SA\"><o:p></o:p></span></p>\x0D\x0A\x0D\x0A<p class=MsoNormal dir=RTL style=\"text-align:right;direction:rtl;unicode-bidi:\x0D\x0Aembed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\">");          
                sb.Append(CONTENT_SUBJECT_HEB);
                sb.Append("\x0D\x0A");
                sb.Append(APPOINT_SUBJECT_HEB);              
                sb.Append("</span><span dir=LTR style=\"font-family:\"Arial\",\"sans-serif\";\x0D\x0Amso-bidi-language:AR-SA\"><o:p></o:p></span></p>\x0D\x0A\x0D\x0A<p class=MsoNormal dir=RTL style=\"text-align:right;direction:rtl;unicode-bidi:\x0D\x0Aembed\"><span dir=RTL></span><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\"><span\x0D\x0Adir=RTL></span>&nbsp;</span><span lang=AR-SA style=\"font-family:\"Arial\",\"sans-serif\";\x0D\x0Amso-bidi-language:AR-SA\"><o:p></o:p></span></p>\x0D\x0A\x0D\x0A<div align=right>\x0D\x0A\x0D\x0A<table class=MsoNormalTable dir=rtl border=0 cellspacing=0 cellpadding=0\x0D\x0A style=\"margin-left:36.0pt;border-collapse:collapse;mso-yfti-tbllook:1184;\x0D\x0A mso-padding-alt:0cm 0cm 0cm 0cm;mso-table-dir:bidi\">\x0D\x0A <tr style=\"mso-yfti-irow:0;mso-yfti-firstrow:yes\">\x0D\x0A  <td width=118 valign=top style=\"width:88.85pt;border:solid black 1.0pt;\x0D\x0A  padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:right;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><b><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\">");
                sb.Append(NAME_HEB);
                sb.Append("\x0D\x0A  ");
                sb.Append(OPER_HEB);
                sb.Append("</span></b></p>\x0D\x0A  </td>\x0D\x0A  <td width=222 valign=top style=\"width:166.5pt;border:solid black 1.0pt;\x0D\x0A  border-right:none;padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:justify;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\";\x0D\x0A  color:#1F497D\">");
                sb.AppendFormat("{0}", operName);
                sb.Append("</span></p>\x0D\x0A  </td>\x0D\x0A </tr>\x0D\x0A <tr style=\"mso-yfti-irow:1\">\x0D\x0A  <td width=118 valign=top style=\"width:88.85pt;border:solid black 1.0pt;\x0D\x0A  border-top:none;padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:right;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\">");
                sb.Append(NAME_HEB);
                sb.Append("\x0D\x0A  ");
                sb.Append(SITE_HEB);
                sb.Append("</span></p>\x0D\x0A  </td>\x0D\x0A  <td width=222 valign=top style=\"width:166.5pt;border-top:none;border-left:\x0D\x0A  solid black 1.0pt;border-bottom:solid black 1.0pt;border-right:none;\x0D\x0A  padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:right;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\";\x0D\x0A  color:#1F497D\">");
                sb.AppendFormat("{0}", branchName);
                sb.Append("</span></p>\x0D\x0A  </td>\x0D\x0A </tr>\x0D\x0A <tr style=\"mso-yfti-irow:2\">\x0D\x0A  <td width=118 valign=top style=\"width:88.85pt;border:solid black 1.0pt;\x0D\x0A  border-top:none;padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:right;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\">");
                sb.Append(DATE_HEB);
                sb.Append("</span></p>\x0D\x0A  </td>\x0D\x0A  <td width=222 valign=top style=\"width:166.5pt;border-top:none;border-left:\x0D\x0A  solid black 1.0pt;border-bottom:solid black 1.0pt;border-right:none;\x0D\x0A  padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:right;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><span dir=LTR>");
                sb.AppendFormat("{0}", dDate);
                sb.Append("</span></p>\x0D\x0A  </td>\x0D\x0A </tr>\x0D\x0A <tr style=\"mso-yfti-irow:3\">\x0D\x0A  <td width=118 valign=top style=\"width:88.85pt;border:solid black 1.0pt;\x0D\x0A  border-top:none;padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:right;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\">");
                sb.Append(DAYW_HEB);
                sb.Append("</span></p>\x0D\x0A  </td>\x0D\x0A  <td width=222 valign=top style=\"width:166.5pt;border-top:none;border-left:\x0D\x0A  solid black 1.0pt;border-bottom:solid black 1.0pt;border-right:none;\x0D\x0A  padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:right;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><span dir=LTR style=\"font-family:\"Arial\",\"sans-serif\"\">");
                sb.AppendFormat("{0}", dDay);
                sb.Append("</span></p>\x0D\x0A  </td>\x0D\x0A </tr>\x0D\x0A <tr style=\"mso-yfti-irow:4;mso-yfti-lastrow:yes\">\x0D\x0A  <td width=118 valign=top style=\"width:88.85pt;border:solid black 1.0pt;\x0D\x0A  border-top:none;padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:right;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\">");
                sb.Append(TIME_HEB);
                sb.Append("</span></p>\x0D\x0A  </td>\x0D\x0A  <td width=222 valign=top style=\"width:166.5pt;border-top:none;border-left:\x0D\x0A  solid black 1.0pt;border-bottom:solid black 1.0pt;border-right:none;\x0D\x0A  padding:0cm 5.4pt 0cm 5.4pt\">\x0D\x0A  <p class=MsoListParagraph dir=RTL style=\"margin-left:0cm;text-align:right;\x0D\x0A  direction:rtl;unicode-bidi:embed\"><span dir=LTR>");
                sb.AppendFormat("{0}", dTime);
                sb.Append("</span></p>\x0D\x0A  </td>\x0D\x0A </tr>\x0D\x0A</table>\x0D\x0A\x0D\x0A</div>\x0D\x0A\x0D\x0A<p class=MsoListParagraph dir=RTL style=\"margin-top:0cm;margin-right:36.0pt;\x0D\x0Amargin-bottom:0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:right;\x0D\x0Adirection:rtl;unicode-bidi:embed\"><span dir=LTR>&nbsp;</span><span lang=AR-SA\x0D\x0Astyle=\"font-family:\"Arial\",\"sans-serif\";mso-bidi-language:AR-SA\"><o:p></o:p></span></p>\x0D\x0A\x0D\x0A<p class=MsoListParagraph dir=RTL style=\"margin-top:0cm;margin-right:36.0pt;\x0D\x0Amargin-bottom:0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:right;\x0D\x0Adirection:rtl;unicode-bidi:embed\"><b><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\">");
                sb.Append(INSTRUCTION1_HEB);
                sb.Append("\x0D\x0A");
                sb.Append(INSTRUCTION2_HEB);
                sb.Append("</span></b><span lang=AR-SA style=\"font-family:\"Arial\",\"sans-serif\";\x0D\x0Amso-bidi-language:AR-SA\"><o:p></o:p></span></p>\x0D\x0A\x0D\x0A<p class=MsoListParagraph dir=RTL style=\"margin-top:0cm;margin-right:36.0pt;\x0D\x0Amargin-bottom:0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:right;\x0D\x0Adirection:rtl;unicode-bidi:embed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\";\x0D\x0Acolor:#1F497D\">");
                sb.AppendFormat("{0}", instruction);
                sb.Append("</span><span lang=AR-SA style=\"font-family:\x0D\x0A\"Arial\",\"sans-serif\";mso-bidi-language:AR-SA\"><o:p></o:p></span></p>\x0D\x0A\x0D\x0A<p class=MsoListParagraph dir=RTL style=\"margin-top:0cm;margin-right:36.0pt;\x0D\x0Amargin-bottom:0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:right;\x0D\x0Adirection:rtl;unicode-bidi:embed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\">&nbsp;</span><span\x0D\x0Alang=AR-SA style=\"font-family:\"Arial\",\"sans-serif\";mso-bidi-language:AR-SA\"><o:p></o:p></span></p>\x0D\x0A\x0D\x0A<p class=MsoListParagraph dir=RTL style=\"margin-top:0cm;margin-right:36.0pt;\x0D\x0Amargin-bottom:0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:right;\x0D\x0Adirection:rtl;unicode-bidi:embed\"><span lang=HE style=\"font-family:\"Arial\",\"sans-serif\"\">&nbsp;</span><span\x0D\x0Alang=AR-SA style=\"font-family:\"Arial\",\"sans-serif\";mso-bidi-language:AR-SA\"><o:p></o:p></span></p>\x0D\x0A\x0D\x0A</div>\x0D\x0A\x0D\x0A</body>\x0D\x0A\x0D\x0A</html>\x0D\x0A");
 
                // EmailMessage = Generic.ConvertToUTF8(sb.ToString()); // DISCARDED ON 25/09/2013
                EmailMessage = sb.ToString();                           // CORRECTED ON 25/09/2013
                // ADDED ON 20/08/2013 : ////////
                SenderEmail = SENDER_EMAIL;
                ContentType = CONTENT_TYPE;
                // END ADDED ////////////////////
            }
        }
#endregion AppointmentMailData_class_definition

        // end additions

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public AppointmentMailData AddNewAppointment(string jsonDate, int jsonCustCode, int jsonSiteCode, int jsonOperCode, int jsonInsrCode, int jsonDline)
        {
            Generic.WriteLog(Generic.GetLogString(0, "NewAppointment", "AddNewAppointment"));   // ADDED ON 04/09/2013
            OdbcConnection conn = Generic.GetOpenedConnection();
            if (conn == null) 
                return null;
            // ADDED ON 23/01/2013 : //////////////////////////////////////////
            if (conn.State != ConnectionState.Open)
            {
                conn = null;
                return null;
            }
           
            //string status = GetAppointmentStatusOnLock(jsonDline, conn);
            //if (status != Generic.STATUS_SUCCESS)
            //{
            //    Generic.WriteLog(string.Format(Generic.DB_LOCK_ERROR, Generic.LOCKING_STATUS[0], status));
            //    status = GetAppointmentStatusOnUnlock(jsonDline, conn);
            //    if (status != Generic.STATUS_SUCCESS)
            //    {
            //        Generic.WriteLog(string.Format(Generic.DB_LOCK_ERROR, Generic.LOCKING_STATUS[1], status));
            //    }
            //    return null;
            //}    
            // END ADDED //////////////////////////////////////////////////////

            string sProc = "sp_add_new_appointment";
            DateTime date = Convert.ToDateTime(jsonDate);
            string sDate = date.ToString("yyyyMMdd");
            AppointmentMailData appointmentMailData = null;
            int visitCode = OnAddNewAppointment(sProc, sDate, jsonCustCode, jsonSiteCode, jsonOperCode, jsonInsrCode, jsonDline, conn);
            if (visitCode > 0)
            {
                sProc = "sp_get_new_appointment_mail_data"; //"sp_get_appointment_mail_data";   // corrected on 02/10/2013
                appointmentMailData = GetAppointmentMailData(visitCode, sProc, conn);
            }
            Generic.CloseConnection(conn);
            Generic.WriteLog(Generic.GetLogString(1, "NewAppointment", "AddNewAppointment"));   // ADDED ON 04/09/2013
            
            return appointmentMailData;
        }

        [WebMethod]
        private OdbcConnection GetOpenedConnection()
        {
            OdbcConnection conn = null;
            try
            {
                conn = new OdbcConnection(@"DSN=Medical;Automatic_Timestamp=ON;SuppressWarnings=YES");
                if (conn != null)
                {
                    conn.Open();
                }
            }
            catch (Exception)
            {
                Generic.CloseConnection(conn);
            }

            return conn;
        }

        [WebMethod]
        private int OnAddNewAppointment(string proc, string date, int custCode, int siteCode, int operCode, int insureCode, int dline, OdbcConnection conn)
        {
            int visitCode = -1;
            string sError = null;
            OdbcCommand cmd = new OdbcCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.CommandText = string.Format("call {0} (?, ?, ?, ?, ?, ?)", proc);
            cmd.Parameters.AddWithValue("@inet_line", custCode);
            cmd.Parameters.AddWithValue("@ad_date", date);
            cmd.Parameters.AddWithValue("@al_branch", siteCode);
            cmd.Parameters.AddWithValue("@al_Oper_Code", operCode);
            cmd.Parameters.AddWithValue("@al_insure_Code", insureCode);
            cmd.Parameters.AddWithValue("@dline", dline);

            try
            {
                visitCode = (Int32) cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message.ToString()))
                {
                    sError = ex.Message.ToString();
                    Generic.WriteLog(sError);   // ADDED ON 04/09/2013;
                }
            }
            finally
            {
                cmd.Dispose();
                if (!string.IsNullOrEmpty(sError))
                    visitCode = -1;
            }

            //return (string.IsNullOrEmpty(sError));
            return visitCode;
        }

        [WebMethod]
        private AppointmentMailData GetAppointmentMailData(int visitCode, string proc, OdbcConnection conn)
        {
            OdbcCommand cmd = new OdbcCommand();
            DataTable dtData = new DataTable();
            if (cmd == null || dtData == null)
                return null;

            AppointmentMailData appointmentMailData = null;
            string firstName, lastName, operName, branchName, dDate, dDay, dTime, instruction, userEmail;
            firstName = lastName = operName = branchName = dDate = dDay = dTime = instruction = userEmail = 
                string.Empty;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.CommandText = string.Format("call {0} (?)", proc);
            cmd.Parameters.AddWithValue("@ai_Visit_Code", visitCode);
            try
            {
                using (OdbcDataAdapter dataAdapter = new OdbcDataAdapter(cmd))
                {
                    dataAdapter.Fill(dtData);
                }

                if (dtData.Columns.Count == 10 && dtData.Rows.Count == 1)
                {
                    firstName    = Convert.ToString(dtData.Rows[0][0]);
                    lastName     = Convert.ToString(dtData.Rows[0][1]);
                    userEmail    = Convert.ToString(dtData.Rows[0][2]);
                    // dDate     = Convert.ToString(dtData.Rows[0][3]);                         // discarded on 25/09/2013
                    dDate        = Convert.ToDateTime(dtData.Rows[0][3]).ToShortDateString();   // corrected on 25/09/2013 
                    dTime        = Convert.ToString(dtData.Rows[0][4]);
                    operName     = Convert.ToString(dtData.Rows[0][5]);
                    branchName   = Convert.ToString(dtData.Rows[0][6]);  
                    instruction  = Convert.ToString(dtData.Rows[0][7]);
                    dDay         = Convert.ToString(dtData.Rows[0][8]);
                    if (instruction == null) instruction = string.Empty;
                    // added on 02/10/2013 : /////////////////////////////
                    if (string.IsNullOrEmpty(userEmail))
                        userEmail = Convert.ToString(dtData.Rows[0][9]);
                    // end added /////////////////////////////////////////

                    appointmentMailData = new AppointmentMailData(firstName, lastName, operName, branchName,
                        dDate, dDay, dTime, instruction, userEmail);
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message.ToString()))
                {
                    appointmentMailData = null;
                    Generic.WriteLog(ex.Message.ToString());    // ADDED ON 04/09/2013
                }
            }
            finally
            {
                dtData.Dispose();
                cmd.Dispose();
            }

            return appointmentMailData;
        }

        [WebMethod]
        private void CloseConnection(OdbcConnection conn)
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
                conn = null;
            }
        }

        // ADDED ON 14/01/2014 FOR USAGE IN WCF REMOTE SERVICE, HOSTED IN WINDOWS SERVICE : ///////
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public int GetCustomerCodeForRemoteDiariesReports()
        {
            Generic.WriteLog(Generic.GetLogString(0, "NewAppointment", "GetCustomerCode"));
            int custCode = 0;
            string proc = "sp_get_customer_code";
            OdbcConnection conn = Generic.GetOpenedConnection();

            try
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    using (OdbcCommand cmd = new OdbcCommand())
                    {
                        if (cmd != null)
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = conn;
                            cmd.CommandText = string.Format("call {0} ()", proc);
                            custCode = Convert.ToInt32(Generic.CUSTCODE_PREFIX + // added on 15/01/2014
                                cmd.ExecuteScalar().ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message.ToString()))
                {
                    custCode = 0;
                    Generic.WriteLog(ex.Message.ToString());
                }
            }
            finally
            {
                Generic.CloseConnection(conn);
            }
            Generic.WriteLog(Generic.GetLogString(1, "NewAppointment", "GetCustomerCode"));

            return custCode;
        }

        // ADDED ON 22/01/2014 : //////////////////////////////////////////////////////////////////
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string OnLockAppointment(int jsonDline)
        {
            Generic.WriteLog(Generic.GetLogString(0, "NewAppointment", "OnLockAppointment"));
            OdbcConnection conn = Generic.GetOpenedConnection();
            if (conn == null) 
                return null;
            if (conn.State != ConnectionState.Open)
            {
                conn = null;
                return null;
            }

            string status = GetAppointmentStatusOnLock(jsonDline, conn);
            
            Generic.CloseConnection(conn);
            Generic.WriteLog(Generic.GetLogString(1, "NewAppointment", "OnLockAppointment"));

            return status;
        }

        
        [WebMethod]
        private string GetAppointmentStatusOnLock(int dline, OdbcConnection conn)
        {
            string status = null;
            string error = null;
            string proc = "sp_lock_free_line";

            try
            {
                using (OdbcCommand cmd = new OdbcCommand())
                {
                    if (cmd != null)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.CommandText = string.Format("call {0} (?)", proc);
                        cmd.Parameters.AddWithValue("@dline", dline);
                        status = cmd.ExecuteScalar().ToString();
                    }
                }
            }
            catch (OdbcException ex)
            {
                error = ex.Message;
            }
            catch (Exception ex)
            {

                error = ex.Message;
            }
            finally
            {
                if (!string.IsNullOrEmpty(error))
                {
                    Generic.WriteLog(error);
                }
                status = status ?? Generic.STATUS_FAILURE;
            }

            return status;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string OnUnlockAppointment(int jsonDline)
        {
            Generic.WriteLog(Generic.GetLogString(0, "NewAppointment", "OnUnlockAppointment"));
            OdbcConnection conn = Generic.GetOpenedConnection();
            if (conn == null)
                return null;
            if (conn.State != ConnectionState.Open)
            {
                conn = null;
                return null;
            }

            string status = GetAppointmentStatusOnUnlock(jsonDline, conn);

            Generic.CloseConnection(conn);
            Generic.WriteLog(Generic.GetLogString(1, "NewAppointment", "OnUnlockAppointment"));

            return status;
        }

        [WebMethod]
        private string GetAppointmentStatusOnUnlock(int dline, OdbcConnection conn)
        {
            string status = null;
            string error = null;
            string proc = "sp_unlock_line";

            try
            {
                using (OdbcCommand cmd = new OdbcCommand())
                {
                    if (cmd != null)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        cmd.CommandText = string.Format("call {0} (?)", proc);
                        cmd.Parameters.AddWithValue("@dline", dline);
                        status = cmd.ExecuteScalar().ToString();
                    }
                }
            }
            catch (OdbcException ex)
            {
                error = ex.Message;
            }
            catch (Exception ex)
            {

                error = ex.Message;
            }
            finally
            {
                if (!string.IsNullOrEmpty(error))
                {
                    Generic.WriteLog(error);
                }
                status = status ?? Generic.STATUS_FAILURE;
            }

            return status;
        }
        // END ADDED //////////////////////////////////////////////////////////////////////////////
    }
}
