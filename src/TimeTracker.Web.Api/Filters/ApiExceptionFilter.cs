using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using TimeTracker.Core.Exceptions.Domain;
using TimeTracker.Core.Exceptions.Technical;
using TimeTracker.Utils;

namespace TimeTracker.Web.Api.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;
        private readonly ICaseConverter _caseConverter;

        public ApiExceptionFilter(ILogger logger, ICaseConverter caseConverter)
        {
            _logger = logger;
            _caseConverter = caseConverter;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.Error(context.Exception, "ApiExceptionFilter");

            if (context.Exception is NotFoundItemException)
            {
                context.Result = new NotFoundResult();
            }
            else if (context.Exception is NotAuthenticatedException)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
            else if (context.Exception is NotAuthorizedException)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
            else if (context.Exception is ConcurrencyException)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status409Conflict);
            }
            else if (context.Exception is DomainException domainException)
            {
                var code = GetCodeFromException(domainException);

                context.Result = new ObjectResult(new {code})
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity
                };
            }
            else if (context.Exception is BadRequestException brException)
            {
                var code = GetCodeFromException(brException);

                context.Result = new ObjectResult(new {code})
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            if (context.Result == null) context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);

            base.OnException(context);
        }

        private string GetCodeFromException(Exception exception)
        {
            var name = exception.GetType().Name;

            var nameAsKebab = _caseConverter.ToKebabCase(name)
                .ToUpper()
                .Replace("-EXCEPTION", "");

            var code = $"COMMON.SERVER-ERROR.DOMAIN.{nameAsKebab}";
            return code;
        }
    }
}