﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Newtonsoft.Json;
using WebApplicationCleint.Models;

namespace WebApplicationCleint
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null) return;

            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null) return;

            var model = JsonConvert.DeserializeObject<UserModel>(authTicket.UserData);

            var roles = model.Roles.Split(',');
            var userPrincipal = new GenericPrincipal(new GenericIdentity(authTicket.Name), roles);
            Context.User = userPrincipal;

        }
        
    }
}
