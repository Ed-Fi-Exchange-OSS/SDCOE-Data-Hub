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
    public class RequirePermissionAttribute : ActionFilterAttribute
    {
        private readonly UserPermission[] _permissions;

        public RequirePermissionAttribute(params UserPermission[] permissions)
        {
            _permissions = permissions;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var orgContext = context.HttpContext.RequestServices.GetService<IOrganizationContextProvider>();
            var logger = context.HttpContext.RequestServices.GetService<ILogger<RequirePermissionAttribute>>();

            var user = await orgContext.GetUser();
            var missingPermissions = _permissions.Except(user.Permissions);
            if (missingPermissions.Any())
            {
                logger.LogDebug("User forbidden due to missing permissions {MissingPermissions}", string.Join(',', missingPermissions));
                context.Result = new ForbidResult();
            }
            else
            {
                await next.Invoke();
            }
        }
    }
}
