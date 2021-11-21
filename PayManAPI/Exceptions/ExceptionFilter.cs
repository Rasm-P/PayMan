using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Exceptions
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException exception)
            {
                var result = new ObjectResult(new
                {
                    code = exception.Status,
                    message = exception.Message,
                });
                result.StatusCode = exception.Status;
                context.Result = result;
                context.ExceptionHandled = true;
            }
            else
            {
                var result = new ObjectResult(new
                {
                    code = 500,
                    message = "Internal server error occurred.",
                });

                result.StatusCode = 500;
                context.Result = result;
            }
            
        }
    }
}
