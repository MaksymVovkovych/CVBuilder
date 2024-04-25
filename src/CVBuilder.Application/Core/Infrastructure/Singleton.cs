using System;
using System.Collections.Generic;

namespace CVBuilder.Application.Core.Infrastructure;

public class Singleton<T>
{
    private static T instance;

    /// <summary>
    ///     The singleton instance for the specified type T. Only one instance (at the time) of this object for each type of T.
    /// </summary>
    public static T Instance
    {
        get => instance;
        set
        {
            instance = value;
            AllSingletons[typeof(T)] = value;
        }
    }

    public static IDictionary<Type, object> AllSingletons { get; }
        = new Dictionary<Type, object>();
}