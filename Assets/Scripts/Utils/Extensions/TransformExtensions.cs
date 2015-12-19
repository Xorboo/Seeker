using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Rand = UnityEngine.Random;
using Obj = UnityEngine.Object;


public static class TransformExtensions
{
    public static void Reset(this Transform t)
    {
        t.position = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = new Vector3(1, 1, 1);
    }

    public static void SetLocalPosition(this Transform t, float x = 0, float y = 0, float z = 0)
    {
        t.localPosition = new Vector3(x, y, z);
    }

    public static void SetPosition(this Transform t, float x = 0, float y = 0, float z = 0)
    {
        t.position = new Vector3(x, y, z);
    }

    public static void SetLocalScale(this Transform t, float x = 0, float y = 0, float z = 0)
    {
        t.localScale = new Vector3(x, y, z);
    }

    public static void SetLocalScaleX(this Transform t, float x)
    {
        t.localScale = new Vector3(x, t.localScale.y, t.localScale.z);
    }

    public static void SetLocalScaleY(this Transform t, float y)
    {
        t.localScale = new Vector3(t.localScale.x, y, t.localScale.z);
    }

    public static void SetLocalScaleZ(this Transform t, float z)
    {
        t.localScale = new Vector3(t.localScale.x, t.localScale.y, z);
    }

    public static void SetPositionX(this Transform t, float newX)
    {
        t.position = new Vector3(newX, t.position.y, t.position.z);
    }

    public static void SetPositionY(this Transform t, float newY)
    {
        t.position = new Vector3(t.position.x, newY, t.position.z);
    }

    public static void SetPositionZ(this Transform t, float newZ)
    {
        t.position = new Vector3(t.position.x, t.position.y, newZ);
    }

    public static void SetLocalPositionX(this Transform t, float newX)
    {
        t.localPosition = new Vector3(newX, t.localPosition.y, t.localPosition.z);
    }

    public static void SetLocalPositionY(this Transform t, float newY)
    {
        t.localPosition = new Vector3(t.localPosition.x, newY, t.localPosition.z);
    }

    public static void SetLocalPositionZ(this Transform t, float newZ)
    {
        t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, newZ);
    }

    public static float GetPositionX(this Transform t)
    {
        return t.position.x;
    }

    public static float GetPositionY(this Transform t)
    {
        return t.position.y;
    }

    public static float GetPositionZ(this Transform t)
    {
        return t.position.z;
    }

    public static Transform FindOrCreate(this Transform t, string name)
    {
        var child = t.Find(name);
        if (child == null)
        {
            child = new GameObject(name).transform;
            child.SetParent(t);
            child.SetLocalPosition(0, 0, 0);
            child.localScale = Vector3.one;
        }
        return child;
    }
}
