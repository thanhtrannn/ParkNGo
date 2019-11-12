using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ParkNGo.Tools
{
    /// <summary>
    /// Ensure sessions are set with appropriate information as the flow of the web application relies on it. This includes credentials and roles of the user.
    /// </summary>
    public class SetSessionVariables : ActionFilterAttribute
    {
        private string role;
        private string credential;
        private string user;
        /// <summary>
        /// Set session variables with values associated with user
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // set variables
            role = context.HttpContext.Session.GetString("Role") != null ? context.HttpContext.Session.GetString("Role") : string.Empty;
            credential = context.HttpContext.Session.GetString("Credential") != null ? context.HttpContext.Session.GetString("Credential") : string.Empty;
            user = context.HttpContext.Session.GetString("Username") != null ? context.HttpContext.Session.GetString("Username") : string.Empty;
            context.HttpContext.Session.SetString("Role", role);
            context.HttpContext.Session.SetString("Credential", credential);
            context.HttpContext.Session.SetString("Username", user);
            base.OnActionExecuting(context);
            var controller = context.Controller as Controller;
            controller.ViewData["Role"] = role;
            controller.ViewData["Username"] = user;
        }
    }
}
