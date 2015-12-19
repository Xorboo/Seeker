using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Rand = UnityEngine.Random;
using Obj = UnityEngine.Object;


public static class VectorExtensions
{
	public static Vector3 Clone(this Vector3 v)
    {
        return new Vector3(v.x, v.y, v.z);
    }

    public static Vector3 FromValue(float value)
    {
        return new Vector3(value, value, value);
    }
}
