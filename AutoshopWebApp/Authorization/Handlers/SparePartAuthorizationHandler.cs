using AutoshopWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Authorization.Handlers
{
    public class SparePartAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, SparePart>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            OperationAuthorizationRequirement requirement, SparePart resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            switch (requirement.Name)
            {
                case Constants.CreateOperationName:
                    if (context.IsAdmin() || context.IsManager() || context.IsEmployee())
                        context.Succeed(requirement);
                    break;
                case Constants.DeleteOperationName:
                    if (context.IsAdmin())
                        context.Succeed(requirement);
                    break;
                case Constants.ReadOperationName:
                    if (context.IsAdmin() || context.IsManager() || context.IsEmployee())
                        context.Succeed(requirement);
                    break;
                case Constants.DetailsOperationName:
                    if (context.IsAdmin() || context.IsManager() || context.IsEmployee())
                        context.Succeed(requirement);
                    break;
                case Constants.UpdateOperationName:
                    if (context.IsAdmin() || context.IsManager() || context.IsEmployee())
                        context.Succeed(requirement);
                    break;
                default:
                    return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
