﻿using System.Security.Claims;

namespace OurCleanFuture.App.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetFormattedName(this ClaimsPrincipal claimsPrincipal) =>
        claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value.Replace('.', ' ') ?? "";
}
