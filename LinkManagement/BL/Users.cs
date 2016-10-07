using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkManagement.Model;
using LinkManagement.DAL.UnitOfWork;

namespace LinkManagement.BL
{
    public class Users : UnitOfWorkInitializer
    {
        
        public User AuthenticateUser(LoginData loginCredential)
        {
            //data.Password = new Helper().GetHasedValue(data.Password);
            var userData = UnitOfWork.user.GetUser(loginCredential);

            
            if (userData != null)
            {
                userData = UnitOfWork.user.AddAccessToken(userData);
                UnitOfWork.Commit();
                
                HttpContext.Current.Session["UserID"] = userData.UserID;
                HttpContext.Current.Session["Name"] = userData.FName;
                HttpContext.Current.Session["AccessToken"] = userData.AccessToken;
                
                userData.Password = "";
            }
            return userData;
            
        }

        public bool IsAuthenticatedToken(string accessToken)
        {
            var sessionToken = HttpContext.Current.Session["AccessToken"] == null ? String.Empty : HttpContext.Current.Session["AccessToken"].ToString();
            
            bool isAuthenticatedToken = (sessionToken == accessToken)
                ? true : UnitOfWork.user.IsAuthenticatedToken(new Guid(accessToken));

            if (isAuthenticatedToken)
            {
                HttpContext.Current.Session["AccessToken"] = new Guid(accessToken) ;
            }
            return isAuthenticatedToken;
        }
    }
}