using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace EBMTodo.Filters
{
    public class ValidateViewModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments.Any(kv => kv.Value == null))
            {
                var errorList = actionContext.ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()[0]
                );
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.NotAcceptable, errorList);
            }

            if (actionContext.ModelState.IsValid == false)
            {
                var errorList = actionContext.ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()[0]
                );
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.NotAcceptable, errorList);
            }
        }
    }
}