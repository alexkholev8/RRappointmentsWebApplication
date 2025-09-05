using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Xml;
using System.Net;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    public class SignUpLogic
    {
        //static bool wsSuccess = true;
        //internal static string SignUpNewUser(UserInfo newUserInfo)
        //{
        //    bool userAlreadyExists = false;
        //    bool isWebServiceSignUpSuccsess = false;
        //    //bool isDataBaseSignUpSuccess = false;
            
        //    userAlreadyExists = DataBaseAccessLayer.SignUpDataBaseAccessLogic.CheckIfUserExists(newUserInfo.Id_num);
        //    if (userAlreadyExists) return "User Already Exists";
        //    else
        //    {           
        //        newUserInfo.Password = GeneratePassword();
        //        isWebServiceSignUpSuccsess = WebServiceSignUp(newUserInfo);
        //        if (isWebServiceSignUpSuccsess)
        //        {
        //            //isDataBaseSignUpSuccess =DataBaseAccessLayer.SignUpDataBaseAccessLogic.AddNewUserToDataBase(newUserInfo);
        //            //if (isDataBaseSignUpSuccess) return "Success";
        //            //else return "DataBaseSignUpFailure";
        //            return "Success";
        //        }
        //        else
        //        {
        //            return "WebServiceSignUpFailure";
        //        }
        //    }
        //}

        //private static bool WebServiceSignUp(UserInfo newUserInfo)
        //{

        //    zimunWebReference.ws_dish_zimun zimunWebReferance = new zimunWebReference.ws_dish_zimun();
        //    zimunWebReferance.Url = ConfigurationManager.AppSettings["DishWebServiceURL"].ToString();

        //    try
        //    {
        //        zimunWebReferance.AllowAutoRedirect = true;
                
        //        zimunWebReferance.Timeout = 10000;
        //        CredentialCache cache = new CredentialCache();
        //     //   cache.Add(new Uri(zimunWebReferance.Url),"Negotiate",new NetworkCredential("iisClient","123"));
        //      //  zimunWebReferance.PreAuthenticate = true;
        //      //  zimunWebReferance.Proxy.Credentials = cache;
        //       // zimunWebReferance.Credentials = cache;
        //        zimunWebReferance.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["wsUserName"].ToString(), ConfigurationManager.AppSettings["wsPassword"].ToString());
        //        zimunWebReferance.ws_soap_zimun_SignUpCompleted +=new zimunWebReference.ws_soap_zimun_SignUpCompletedEventHandler(zimunWebReferance_ws_soap_zimun_SignUpCompleted);
        //        zimunWebReferance.ws_soap_zimun_SignUpAsync(newUserInfo.First_name, newUserInfo.LastName, newUserInfo.Id_num, newUserInfo.AreaCode + newUserInfo.PhoneNumber, newUserInfo.Email, newUserInfo.Password);
        //        return wsSuccess;
        //    }

        //    catch
        //    {
        //        return false;
        //    }
        //}

        //static void zimunWebReferance_ws_soap_zimun_SignUpCompleted(object sender, zimunWebReference.ws_soap_zimun_SignUpCompletedEventArgs e)
        //{
        //    if (e.Error.Message == "Unable to connect to the remote server")
        //    {
        //        wsSuccess = false;
        //    }
        //    else if (e.Error.Message == "The request failed with HTTP status 401: Authorization Required.")
        //    {
        //        wsSuccess = false;
        //    }
        //    else
        //    {
        //        wsSuccess = true;
        //    }
           
        //}  

        //private static string GeneratePassword()
        //{
        //    Random randPassword = new Random();
        //    return randPassword.Next(100000,999999).ToString();

        //}

    }
}