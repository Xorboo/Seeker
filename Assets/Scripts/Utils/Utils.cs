using UnityEngine;
using System.Collections.Generic;
using Rand = UnityEngine.Random;


public static class Utils
{
    public static readonly int SecondsToNanoseconds = 10000000;
    public static readonly float SecondsToNanosecondsF = 10000000f;

    public static T Random<T>(T[] array)
    {
        return array[Rand.Range(0, array.Length)];
    }
    public static T Random<T>(List<T> list)
    {
        return list[Rand.Range(0, list.Count)];
    }

    public static int ToArrayIndexSimple(int value, int length)
    {
        return Mathf.Abs(value) % length;
    }

    public static int ToArrayIndex(int value, int length)
    {
        if (length <= 0)
            return 0;

        value %= length;
        if (value < 0)  // We can do that, because [-13 % 10 = -3]
            return value + length;
        return value;
    }

    public static void Swap<T>(ref T a, ref T b)
    {
        T temp;
        temp = a;
        a = b;
        b = temp;
    }
}
