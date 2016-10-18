using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkManagement.Model;
using LinkManagement.DAL.UnitOfWork;
using System.Web.Security;

namespace LinkManagement.BL
{
    public class Users : UnitOfWorkInitializer
    {
        
        public User AuthenticateUser(User loginCredential)
        {
            //data.Password = new Helper().GetHasedValue(data.Password);
            var userData = UnitOfWork.user.GetUser(loginCredential);

            
            if (userData != null)
            {
                HttpContext.Current.Session["UserID"] = userData.UserID;
                HttpContext.Current.Session["Name"] = userData.FName;
                
                string roles = "Moderator";
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                  1,
                  userData.UserID.ToString(),  //user id
                  DateTime.Now,
                  DateTime.Now.AddMinutes(20),  // expiry
                  false,  //do not remember
                  roles,
                  "/");
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                                   FormsAuthentication.Encrypt(authTicket));
                HttpContext.Current.Response.Cookies.Add(cookie);
                
                //FormsAuthentication.SetAuthCookie("uday", true);
                userData.Password = "";
            }
            return userData;
            
        }

        //public bool IsAuthenticatedToken(string accessToken)
        //{
        //    var ipAddress = new Helper().GetLocalIPAddress();
        //    var sessionToken = HttpContext.Current.Session["AccessToken"] == null ? String.Empty : HttpContext.Current.Session["AccessToken"].ToString();
            
        //    bool isAuthenticatedToken = (sessionToken == accessToken)|| UnitOfWork.user.IsAuthenticatedToken(new Guid(accessToken));

        //    if (isAuthenticatedToken)
        //    {
        //        HttpContext.Current.Session["AccessToken"] = new Guid(accessToken) ;
        //        FormsAuthentication.SetAuthCookie("uday", true);
        //    }
        //    return isAuthenticatedToken;
        //}

        //public void GetUserCookie()
        //{
        //    var name = HttpContext.Current.User.Identity.Name;
           
        //}
    }
}