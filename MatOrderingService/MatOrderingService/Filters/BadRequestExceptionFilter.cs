using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace MatOrderingService.Filters
{
    public class BadRequestExceptionFilter: ExceptionFilterAttribute
    {
        private readonly ILogger<BadRequestExceptionFilter> _logger;

        public BadRequestExceptionFilter(ILogger<BadRequestExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is Exception)
            {
                _logger.LogInformation("-------------------------" + context.Exception.Message);
                context.Result = new BadRequestResult();
            }
        }
    }
}