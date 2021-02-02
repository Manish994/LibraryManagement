﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Helpers;
using Microsoft.AspNetCore.Authorization;
using static LibraryManagement.Requirements.UserStatusRequirement;

namespace LibraryManagement.Handlers
{
    public class UserBlockedStatusHandler : AuthorizationHandler<UserBlockedStatusRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserBlockedStatusRequirement requirement)
        {
            var claim = context.User.FindFirst(c => c.Type == "IsBlocked" && c.Issuer == TokenHelper.Issuer);
            if (!context.User.HasClaim(c => c.Type == "IsBlocked" && c.Issuer == TokenHelper.Issuer))
            {
                return Task.CompletedTask;
            }

            string value = context.User.FindFirst(c => c.Type == "IsBlocked" && c.Issuer == TokenHelper.Issuer).Value;
            var customerBlockedStatus = Convert.ToBoolean(value);

            if (customerBlockedStatus == requirement.IsBlocked)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
