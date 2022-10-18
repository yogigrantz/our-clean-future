﻿using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace OurCleanFuture.App.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue) =>
        enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            ?.GetName() ?? $"Error: enum {enumValue} is missing a displayname attribute";
}
