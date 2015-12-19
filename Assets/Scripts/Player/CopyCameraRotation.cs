using UnityEngine;
using System.Collections;

public class CopyCameraRotation : MonoBehaviour
{
    [SerializeField]
    PlayerMoveController Controller;

    void Update()
    {
        // Just y axis are important
        //transform.rotation = Quaternion.Euler(Controller.CameraEuler);
        transform.rotation = Quaternion.Euler(new Vector3(0, Controller.CameraEuler.y, 0));
    }
}
