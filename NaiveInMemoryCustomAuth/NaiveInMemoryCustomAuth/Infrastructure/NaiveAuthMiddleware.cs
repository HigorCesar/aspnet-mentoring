using System;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NaiveInMemoryCustomAuth.Infrastructure
{
    public class NaiveAuthMiddleware
    {
        private readonly Authenticator authenticator;
        public NaiveAuthMiddleware(Authenticator auth)
        {
            authenticator = auth;
        }
        public async Task Authenticate(HttpContext httpContext, Func<Task> next)
        {
            Console.WriteLine("Authentication middleware");
            var authKeyHeader = httpContext.Request.Headers.FirstOrDefault(x => x.Key == "auth-key");

            if (httpContext.Request.Path.StartsWithSegments("/api/naiveauth"))
            {
                await next();
                return;
            }

            if (!authenticator.Authenticate(authKeyHeader.Value))
            {
                Console.WriteLine("You need to authenticate");
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }
            //Replace the parameter with the username from the request.
            httpContext.User = new System.Security.Claims.ClaimsPrincipal(new GenericIdentity(authKeyHeader.Value));

            //Will return true.
            Console.WriteLine("Is authenticated: ${context.User.Identity.IsAuthenticated}");

            await next();
        }

    }
}
