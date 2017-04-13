using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Filters;

namespace ShortLink.Filters
{
    public class WebApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly MediaTypeFormatter _mediaTypeFormatter;

        private readonly Type _exceptionType;

        private readonly HttpStatusCode _statusCode;

        public WebApiExceptionFilter(Type exceptionType, HttpStatusCode statusCode, MediaTypeFormatter mediaTypeFormatter)
        {
            _exceptionType = exceptionType;
            _statusCode = statusCode;
            _mediaTypeFormatter = mediaTypeFormatter;
        }
        public override void OnException(HttpActionExecutedContext context)
        {
            if (_exceptionType.IsInstanceOfType(context.Exception))
            {
                context.Response = new HttpResponseMessage(_statusCode)
                {
                    Content = new ObjectContent<ExceptionResult>(
                        new ExceptionResult { Success = false, Message = context.Exception.Message },
                        _mediaTypeFormatter),
                };

                throw new HttpResponseException(context.Response);
            }
        }
    }
}