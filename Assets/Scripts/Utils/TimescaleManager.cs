using UnityEngine;


public class TimescaleManager : Singleton<TimescaleManager>
{
    int PauseCount
    {
        get { return _PauseCount; }
        set
        {
            _PauseCount = Mathf.Max(0, value);
            Time.timeScale = _PauseCount == 0 ? TimescaleSave : 0f;
        }
    }
    int _PauseCount = 0;

    float TimescaleSave = 1f;

    public void Pause()
    {
        PauseCount++;
    }

    public void Unpause()
    {
        PauseCount--;
    }

    public void ForceUnpause()
    {
        PauseCount = 0;
    }

    public void SetTimescale(float percent)
    {
        float clampedPercent = Mathf.Clamp(percent, 0f, 100f);

        TimescaleSave = clampedPercent;

        if (PauseCount == 0)
            Time.timeScale = TimescaleSave;
    }
}