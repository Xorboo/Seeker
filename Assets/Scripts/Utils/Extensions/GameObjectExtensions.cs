using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Rand = UnityEngine.Random;
using Obj = UnityEngine.Object;


public static class GameObjectExtensions
{
    public static bool HasRigidbody(this GameObject gameObject)
    {
        return (gameObject.GetComponent<Rigidbody>() != null);
    }

    public static bool HasAnimation(this GameObject gameObject)
    {
        return (gameObject.GetComponent<Animation>() != null);
    }

    public static List<T> FindComponents<T>(bool includeActive = true, bool includeInactive = false) where T : Component
    {
#if UNITY_EDITOR
        var allObjects = Resources.FindObjectsOfTypeAll<T>();
        var result = new List<T>(allObjects.Length);
        foreach (var obj in allObjects)
        {
            var objType = UnityEditor.PrefabUtility.GetPrefabType(obj);
            if (objType != UnityEditor.PrefabType.Prefab && objType != UnityEditor.PrefabType.ModelPrefab)
            {
                bool isActive = obj.gameObject.activeSelf;
                if (isActive && includeActive || !isActive && includeInactive)
                {
                    result.Add(obj);
                }
            }
        }
        return result;
#else
        Log.Error("FindComponents<T>() availible only in editor mode!");
        return new List<T>();
#endif
    }
}
