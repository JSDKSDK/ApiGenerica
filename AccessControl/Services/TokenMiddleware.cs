using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Services
{
    public class TokenMiddleware
    {
        public readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next )
        {
            _next = next;
           
        }
        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public async Task Invoke(HttpContext context, ITokenManager tokenManager)
        {
            var path = context.Request.Path.Value;

            if (path.Length > 1)
            {
                if (tokenManager.GetAllowedPaths().Contains(path))
                {
                    await _next(context);
                    return;
                    
                }
                if (tokenManager.IsActive())
                {
                    await _next(context);
                    return;
                }
                else
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

        }
    }
}
