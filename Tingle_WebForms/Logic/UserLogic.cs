using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tingle_WebForms.Models;
using Tingle_WebForms.Logic;

namespace Tingle_WebForms.Logic
{
    public class UserLogic
    {
        FormContext ctx = new FormContext();

        public SystemUsers GetCurrentUser(System.Security.Principal.IPrincipal user)
        {
            try
            {
                SystemUsers sUser = ctx.SystemUsers.Where(u => u.UserName == user.Identity.Name).FirstOrDefault();

                if (sUser != null)
                {
                    return sUser;
                }
                else
                {
                    UserRoles uRole = ctx.UserRoles.Where(ur => ur.RoleName == "User").FirstOrDefault();

                    ctx.SystemUsers.Add(new SystemUsers { UserName = user.Identity.Name, DisplayName = user.Identity.Name.Substring(System.Web.HttpContext.Current.User.Identity.Name.IndexOf(@"\") + 1), UserRole = uRole });
                    
                    ctx.SaveChanges();

                    SystemUsers newUser = ctx.SystemUsers.Where(u => u.UserName == user.Identity.Name).FirstOrDefault();
                    


                    if (newUser != null)
                    {

                        return newUser;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }


        }
    }
}