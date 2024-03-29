﻿// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace MDS.ColorCode.Common;

public static class Guard
{
    public static void ArgNotNull(object arg, string paramName)
    {
        if (arg == null)
            throw new ArgumentNullException(paramName);
    }

    public static void ArgNotNullAndNotEmpty(string arg, string paramName)
    {
        if (arg == null)
            throw new ArgumentNullException(paramName);

        if (string.IsNullOrEmpty(arg))
            throw new ArgumentException(string.Format("The {0} argument value must not be empty.", paramName), paramName);
    }

#pragma warning disable CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
    public static void EnsureParameterIsNotNullAndNotEmpty<TKey, TValue>(IDictionary<TKey, TValue> parameter, string parameterName)
#pragma warning restore CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
    {
        if (parameter == null || parameter.Count == 0)
            throw new ArgumentNullException(parameterName);
    }

    public static void ArgNotNullAndNotEmpty<T>(IList<T> arg, string paramName)
    {
        if (arg == null)
            throw new ArgumentNullException(paramName);

        if (arg.Count == 0)
            throw new ArgumentException(string.Format("The {0} argument value must not be empty.", paramName), paramName);
    }
}
