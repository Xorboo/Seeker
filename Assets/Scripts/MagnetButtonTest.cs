using System.Collections;
using UnityEngine;

public class MagnetButtonTest : MonoBehaviour
{
    public bool magnetDetectionEnabled = true;
    public Renderer render;

    private int index = 0;

    private void Start()
    {
        CardboardMagnetSensor.SetEnabled(magnetDetectionEnabled);
        // Disable screen dimming: 
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Update()
    {
        if (!magnetDetectionEnabled)
        {
            return;
        }
        if (CardboardMagnetSensor.CheckIfWasClicked() ||
            Input.anyKey)
        {
            Debug.Log("DO SOMETHING HERE");  // PERFORM ACTION.
            ++index;
            switch (index)
            {
                case 1:
                    render.material.color = Color.white;
                    break;

                case 2:
                    render.material.color = Color.red;
                    break;

                case 3:
                    render.material.color = Color.yellow;
                    break;

                default:
                    render.material.color = Color.blue;
                    index = 0;
                    break;
            }
            CardboardMagnetSensor.ResetClick();
        }
    }
}