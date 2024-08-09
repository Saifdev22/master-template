﻿using System.Security.Claims;

namespace Starter.Application.Identity.Users.Abstractions
{
    public interface ICurrentUserService
    {
        string? Name { get; }

        Guid GetUserId();

        string? GetUserEmail();

        string? GetTenant();

        bool IsAuthenticated();

        bool IsInRole(string role);

        IEnumerable<Claim>? GetUserClaims();
    }
}
