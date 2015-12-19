using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Rand = UnityEngine.Random;
using Obj = UnityEngine.Object;


public static class AnimationExtensions
{
    public static void SetSpeed(this Animation anim, float newSpeed)
    {
        anim[anim.clip.name].speed = newSpeed;
    }
}
