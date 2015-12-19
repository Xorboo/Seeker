using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class PlayerMoveController : NetworkBehaviour
{
    [SerializeField]
    VRCamera VrCamera;
    [SerializeField]
    CharacterController CC;
    [SerializeField]
    float Speed = 3f;

    [SerializeField]
    GameObject SnowballPrefab;
    [SerializeField]
    float SnowballSpeed = 5f;
    [SerializeField]
    float SnowballLifetime = 5f;

    [SyncVar]
    public Vector3 CameraEuler;


    [ClientCallback]
    void Update()
    {
        if (!isLocalPlayer)
            return;

        // Updatiing our look direction to the server
        CmdUpdateCameraAngle(VrCamera.vrCameraHeading.rotation.eulerAngles);

        // Moving
        Vector3 move = 
            Vector3.forward * FibrumInput.GetJoystickAxis(FibrumInput.Axis.Vertical1) + 
            Vector3.right * FibrumInput.GetJoystickAxis(FibrumInput.Axis.Horizontal1);
        CmdMovePLayer(VrCamera.vrCameraHeading.TransformDirection(move));


        // Shooting
        if (FibrumInput.GetJoystickButtonDown(FibrumInput.Button.A))
        {
            Vector3 bulletDir =
               VrCamera.vrCameraHeading.position +
               VrCamera.vrCameraHeading.TransformDirection(Vector3.forward * 0.5f - Vector3.up * 0.5f);
            Vector3 bulletRotation = VrCamera.vrCameraHeading.transform.rotation.eulerAngles;
            CmdSpawnBullet(bulletDir, bulletRotation);
        }

        
        // Todo reloading - if we are looking down at ferst (later - raycasting to the snow)
    }

    [Command]
    public void CmdMovePLayer(Vector3 move)
    {
        CC.SimpleMove(Speed * move);
    }

    [Command]
    public void CmdSpawnBullet(Vector3 direction, Vector3 rotation)
    {
        GameObject snowball = Instantiate(SnowballPrefab, direction, Quaternion.Euler(rotation)) as GameObject;
        snowball.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * SnowballSpeed, ForceMode.Impulse);
        Destroy(snowball, SnowballLifetime);
        NetworkServer.Spawn(snowball);
    }

    [Command(channel = 1)]
    public void CmdUpdateCameraAngle(Vector3 euler)
    {
        CameraEuler = euler;
    }
}
