using UnityEngine;
using System.Collections;

public class CopyCameraRotation : MonoBehaviour
{
    [SerializeField]
    PlayerMoveController Controller = null;

    void LateUpdate()
    {
        // Just y axis are important
        Vector3 rotation = Controller.isLocalPlayer ? Controller.GetCameraRotation() : Controller.CameraEuler;
        transform.rotation = Quaternion.Euler(new Vector3(0, rotation.y, 0));
    }
}