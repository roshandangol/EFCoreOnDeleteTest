using EFCoreOnDeleteTest.MOdel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreOnDeleteTest
{
    public static class AuthenticationExtensionMethods
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            if (users == null) return null;

            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            if (user == null) return null;

            user.Password = null;
            return user;
        }

        /// <summary>
        /// Gets the current API resource name from HTTP context
        /// </summary>
        /// <param name="httpContext">The HTTP context</param>
        /// <returns>The current resource name if available, otherwise an empty string</returns>
        public static string GetMetricsCurrentResourceName(this HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));
            
            Endpoint endpoint = httpContext.GetEndpoint();
            
            var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            return $"At Controller: {controllerActionDescriptor.ControllerName}, Action: {controllerActionDescriptor.ActionName} ";
        }

        //public static string GetMetricsCurrentResourceName(this HttpContext httpContext)
        //{
        //    if (httpContext == null)
        //        throw new ArgumentNullException(nameof(httpContext));

        //    Endpoint endpoint = httpContext.GetEndpoint();
        //    return endpoint?.Metadata.GetMetadata<EndpointNameMetadata>()?.EndpointName;
        //}
    }
}
