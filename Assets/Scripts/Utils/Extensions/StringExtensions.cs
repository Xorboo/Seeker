using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Rand = UnityEngine.Random;
using Obj = UnityEngine.Object;


public static class StringExtensions
{
    // Clear StringBuilder w/o releasing the memory
    public static StringBuilder Clear(this StringBuilder instance)
    {
        instance.Length = 0;
        return instance;
    }
}
