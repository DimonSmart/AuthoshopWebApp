using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Authorization
{
    public static class Operations
    {
        public static OperationAuthorizationRequirement Create =
            new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };

        public static OperationAuthorizationRequirement Update =
            new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };

        public static OperationAuthorizationRequirement Read =
            new OperationAuthorizationRequirement { Name = Constants.ReadOperationName };

        public static OperationAuthorizationRequirement Delete =
            new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };

        public static OperationAuthorizationRequirement Details =
            new OperationAuthorizationRequirement { Name = Constants.DetailsOperationName };
    }

    public static class Constants
    {
        public static readonly string ChangeRoleOperationName = "ChangeRole";

        public const string CreateOperationName = "Create";
        public const string UpdateOperationName = "Update";
        public const string ReadOperationName = "Read";
        public const string DeleteOperationName = "Delete";
        public const string DetailsOperationName = "Details";

        public static readonly string AdministratorRole = "Administartors";
        public static readonly string ManagerRole = "Managers";
        public static readonly string Employee = "Employee";
    }

    public static class AuthorizationHelpers
    {
        public static bool IsEmployee(this AuthorizationHandlerContext context)
            => context.User.IsInRole(Constants.Employee);

        public static bool IsManager(this AuthorizationHandlerContext context)
            => context.User.IsInRole(Constants.ManagerRole);

        public static bool IsAdmin(this AuthorizationHandlerContext context)
            => context.User.IsInRole(Constants.AdministratorRole);
    }
}
