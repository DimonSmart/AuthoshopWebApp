using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Authorization
{
    public static class Operations
    {
        public static OperationAuthorizationRequirement ChangeRole =
            new OperationAuthorizationRequirement { Name = Constants.ChangeRoleOperationName };
    }

    public static class Constants
    {
        public static readonly string ChangeRoleOperationName = "ChangeRole";

        public static readonly string CreateOperationName = "Create";
        public static readonly string UpdateOperationName = "Update";
        public static readonly string ReadOperationName = "Read";
        public static readonly string DeleteOperationName = "Delete";

        public static readonly string AdministratorRole = "Administartors";
        public static readonly string ManagerRole = "Managers";
        public static readonly string Employee = "Employee";
    }
}
