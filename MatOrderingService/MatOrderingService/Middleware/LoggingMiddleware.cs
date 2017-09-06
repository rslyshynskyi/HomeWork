using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatOrderingService.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            //_logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            //_logger.LogInformation("!!!..." + context.Request.Path + "|||");
            await this._next(context);

            //_logger.LogInformation("|||" + context.Request.Path + "!!!...");
        }
    }
}
