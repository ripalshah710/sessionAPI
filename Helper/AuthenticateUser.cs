using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SessionInfoApi.Helper
{
    public class AuthenticateUser
    {
       
        public static bool ValidateUser(IHeaderDictionary request)
        {
            if (request.ContainsKey("Authorization"))
            {
                string authToken = request.FirstOrDefault(x => x.Key == "Authorization").Value.FirstOrDefault();
                if (authToken.Contains("Basic"))
                {
                    // decoding authToken we get decode value in 'Username:Password' format  
                    var decodeauthToken = System.Text.Encoding.UTF8.GetString(
                        Convert.FromBase64String(authToken.Substring("Basic ".Length).Trim()));

                    // spliting decodeauthToken using ':'   
                    var arrUserNameandPassword = decodeauthToken.Split(':');

                    // at 0th postion of array we get username and at 1st we get password  
                    if (arrUserNameandPassword.Length > 1 && IsAuthorizedUser(arrUserNameandPassword[0].Trim(), arrUserNameandPassword[1].Trim()))
                    {
                        return true;
                    }//if
                    else
                    {
                        return false;
                    }//else
                }//if

              
                else
                {
                    return false;
                }//else
            }//if
            else
            {
                return false;
            }//else
        }//OnAuthorization

        public static bool IsAuthorizedUser(string Username, string Password)
        {
            // In this method we can handle our database logic here... 
            string userName = Util.GetAppSetting("username");
            string password = Util.GetAppSetting("password");
            return Username == userName && Password == password;

        }//IsAuthorizedUser
      
    }
}
