using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Rand = UnityEngine.Random;
using Obj = UnityEngine.Object;


public static class ListExtensions
{
    public static bool Empty<T>(this List<T> list)
    {
        return list.Count == 0;
    }

    public static T Random<T>(this List<T> list)
    {
        if (list.Empty())
            return default(T);
        return list[Rand.Range(0, list.Count)];
    }

    public static T First<T>(this List<T> list)
    {
        if (list.Empty())
            return default(T);
        return list[0];
    }

    public static T Last<T>(this List<T> list)
    {
        if (list.Empty())
            return default(T);
        return list[list.Count - 1];
    }

    public static bool RemoveFirst<T>(this List<T> list)
    {
        if (list.Empty())
            return false;
        list.RemoveAt(0);
        return true;
    }

    public static bool RemoveLast<T>(this List<T> list)
    {
        if (list.Empty())
            return false;
        list.RemoveAt(list.Count - 1);
        return true;
    }
}
