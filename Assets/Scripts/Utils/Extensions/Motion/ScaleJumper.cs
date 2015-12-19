using UnityEngine;
using System;
using System.Collections;


public class ScaleJumper : MonoBehaviour
{
    [SerializeField] float Duration = 0.2f;
    float HalfDuration;

    [SerializeField] float MaxScale = 1.2f;
    Vector3 MaxScaleVector;
    Vector3 DefaultScale = Vector3.one;

    [SerializeField] EaseType UpEase = EaseType.Linear;
    [SerializeField] EaseType DownEase = EaseType.Linear;
    Easer UpEaser;
    Easer DownEaser;

    IEnumerator Routine = null;


    public void DoScale()
    {
        Reset();

        if (gameObject.activeInHierarchy)
        {
            Routine = ScaleRoutine();
            StartCoroutine(Routine);
        }
    }


    void Awake() 
    {
        HalfDuration = Duration / 2f;

        MaxScaleVector = VectorExtensions.FromValue(MaxScale);
        DefaultScale = transform.localScale;

        UpEaser = Ease.FromType(UpEase);
        DownEaser = Ease.FromType(DownEase);
    }

    void OnEnable()
    {
        Reset();
    }

    void OnDisable()
    {
        Reset();
    }

    void Reset()
    {
        if (Routine != null)
        {
            StopCoroutine(Routine);
            Routine = null;
            transform.localScale = DefaultScale;
        }
    }

    IEnumerator ScaleRoutine()
    {
        float elapsed = 0;

        var start = transform.localScale;
        var range = MaxScaleVector - start;
        while (elapsed < HalfDuration)
        {
            elapsed = Mathf.MoveTowards(elapsed, HalfDuration, Time.deltaTime);
            transform.localScale = start + range * UpEaser(elapsed / HalfDuration);
            yield return 0;
        }

        elapsed = 0;

        start = transform.localScale;
        range = DefaultScale - start;
        while (elapsed < HalfDuration)
        {
            elapsed = Mathf.MoveTowards(elapsed, HalfDuration, Time.deltaTime);
            transform.localScale = start + range * DownEaser(elapsed / HalfDuration);
            yield return 0;
        }
        
        transform.localScale = DefaultScale;
    }
}
