using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Rand = UnityEngine.Random;
using Obj = UnityEngine.Object;


public static class GraphicExtensions
{
    public static IEnumerator OpacityTo(this Graphic graphic, float value, float duration, Easer ease, Action finishDelegate = null)
    {
        float elapsed = 0;
        var start = graphic.color.a;
        var range = value - start;
        while (elapsed < duration)
        {
            elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, start + range * ease(elapsed / duration));
            yield return 0;
        }
        graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, value);

        if (finishDelegate != null)
        {
            finishDelegate();
        }
    }
    public static IEnumerator OpacityTo(this Graphic graphic, float value, float duration, EaseType ease = EaseType.Linear, Action finishDelegate = null)
    {
        return OpacityTo(graphic, value, duration, Ease.FromType(ease), finishDelegate);
    }


    public static IEnumerator ColorTo(this Graphic graphic, Color color, float duration, Easer ease, Action finishDelegate = null)
    {
        float elapsed = 0;
        var start = graphic.color;
        var range = color - start;
        while (elapsed < duration)
        {
            elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
            graphic.color = start + range * ease(elapsed / duration);
            yield return 0;
        }
        graphic.color = color;

        if (finishDelegate != null)
        {
            finishDelegate();
        }
    }
    public static IEnumerator ColorTo(this Graphic graphic, Color color, float duration, EaseType ease = EaseType.Linear, Action finishDelegate = null)
    {
        return ColorTo(graphic, color, duration, Ease.FromType(ease), finishDelegate);
    }
}
