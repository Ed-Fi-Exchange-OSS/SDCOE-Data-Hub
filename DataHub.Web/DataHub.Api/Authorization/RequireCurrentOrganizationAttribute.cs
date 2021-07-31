using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataHub.Api.Enums;
using DataHub.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DataHub.Api.Authorization
{
    public class RequireCurrentOrganizationAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var orgContext = context.HttpContext.RequestServices.GetService<IOrganizationContextProvider>();
            
            try
            {
                await orgContext.GetCurrentOrganizationId();
                await next.Invoke();
            }
            catch
            {
                context.Result = new BadRequestObjectResult("This request requires a current organization context. For users with access to all organizations, the 'x-local-organization-id' header should be included in requests.");
            }
        }
    }
}