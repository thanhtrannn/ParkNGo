using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkNGo.Middleware
{
    public class LogMiddleware
    {
        /// <summary>
        /// Run when log is called, allow for customizable logs
        /// </summary>
        private readonly RequestDelegate next;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next">RequestDelegate object</param>
        public LogMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        /// <summary>
        /// Push properties to log
        /// </summary>
        /// <param name="httpContext">HttpContext objext</param>
        /// <returns></returns>
        public Task Invoke(HttpContext httpContext)
        {
            LogContext.PushProperty("User", httpContext.User.Identity.Name);
            LogContext.PushProperty("Ip", httpContext.Connection.RemoteIpAddress);
 /*           if (httpContext.Session.GetString("EmployeeNumber") != null && httpContext.Session.GetString("User") != null)
                LogContext.PushProperty("Login", " - " + httpContext.Session.GetString("User") + "(" + httpContext.Session.GetString("EmployeeNumber") + ")");*/
            return next(httpContext);
        }
    }
}
