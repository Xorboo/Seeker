using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class PlayerMoveController : NetworkBehaviour
{
    static int PlayerNumberBase = 0;

    [SerializeField]
    public VRCamera VrCamera;
    [SerializeField]
    CharacterController CC;
    [SerializeField]
    float Speed = 3f;

    [SerializeField]
    GameObject SnowballPrefab;
    [SerializeField]
    float SnowballLifetime = 5f;

    bool isForcemining = false;
    [SerializeField]
    float MaxPickupDistance = 2f;
    [SerializeField]
    float forcemining = 2f;
    [SerializeField]
    float max_forcemine = 5f;
    [SerializeField]
    float d_forcemine = 2.5f;

    [SyncVar]
    public Vector3 CameraEuler;

    [SyncVar]
    public float Health = 100;

    [SyncVar]
    int PlayerNumber = 0;

    int MaxAmmo = 5;
    List<Snowball.SnowType> Snowballs = new List<Snowball.SnowType>();

    [SerializeField]
    float AmmoTakeTime = 2f;

    bool IsTakingAmmo = false;

    Snowdrift WatchedDrift = null;

    bool IsResting = false;
    [SerializeField]
    float RestTime = 0.5f;

    public Vector3 LastHitPosition;

    void Reset()
    {
        StopAllCoroutines();
        Snowballs.Clear();
        IsTakingAmmo = false;
        IsResting = false;
        isForcemining = false;
        Health = 100f;
        StopWatchingDrift();
    }

    void StopWatchingDrift()
    {
        if (WatchedDrift != null)
        {
            WatchedDrift.StopHighlight();
            WatchedDrift = null;
        }
    }

    [ServerCallback]
    void Awake()
    {
        PlayerNumber = PlayerNumberBase++;
        name = "Player #" + PlayerNumber;
    }
    [ClientCallback]
    void Update()
    {
        if (!isLocalPlayer)
            return;

        // Updatiing our look direction to the server
        CmdUpdateCameraAngle(GetCameraRotation());

        // Moving
        if (!IsTakingAmmo)
        {
            Vector3 move =
                Vector3.forward * FibrumInput.GetJoystickAxis(FibrumInput.Axis.Vertical1) +
                Vector3.right * FibrumInput.GetJoystickAxis(FibrumInput.Axis.Horizontal1);
            CmdMovePLayer(VrCamera.vrCameraHeading.TransformDirection(move));
        }

        // Trying to highlight snowdrifts
        Vector3 pos = VrCamera.vrCameraHeading.position;
        Ray ray = new Ray(pos, VrCamera.vrCameraHeading.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Drift")
            {
                var newDrift = hit.collider.gameObject.GetComponent<Snowdrift>();
                if (WatchedDrift != null && WatchedDrift != newDrift)
                {
                    WatchedDrift.StopHighlight();
                }

                WatchedDrift = newDrift;
                float distance = Vector3.Distance(pos, hit.point);
                WatchedDrift.StartHighlight(distance < MaxPickupDistance);
            }
            else
            {
                StopWatchingDrift();
            }

            LastHitPosition = hit.point;
        }
        else
        {
            StopWatchingDrift();
            LastHitPosition = new Vector3(0, -1000, 0);
        }


        // Taking Ammo
        if (FibrumInput.GetJoystickButtonDown(FibrumInput.Button.B) || Input.GetMouseButtonDown(1))
        {
            Log.Write("Taking ammo: pressed");


            if (Snowballs.Count < MaxAmmo && !isForcemining && !IsTakingAmmo) // Looking at snow
            {
                Ray checkRay = new Ray(VrCamera.vrCameraHeading.position, VrCamera.vrCameraHeading.forward);
                RaycastHit resultHit;
                if (Physics.Raycast(checkRay, out resultHit, MaxPickupDistance))
                {
                    if (hit.collider.tag == "Drift")
                    {
                        //Log.Write(Random.Range(0, 10) + " Hit: " + resultHit.collider.gameObject.name + " ; " + Vector3.Distance(VrCamera.vrCameraHeading.position, resultHit.point));
                        var snowdrift = hit.collider.GetComponent<Snowdrift>();
                        snowdrift.CollectSnow();
                        StartCoroutine(TakeAmmo(snowdrift.Type));
                    }
                }
            }
        }


        // Shooting
        if (FibrumInput.GetJoystickButtonDown(FibrumInput.Button.A) && !IsTakingAmmo && Snowballs.Count > 0 && !IsResting)
        {
            isForcemining = true;
            forcemining = 0;
        }
        if (isForcemining)
            forcemining = Mathf.Min(max_forcemine, forcemining + d_forcemine * Time.deltaTime);
        if (FibrumInput.GetJoystickButtonUp(FibrumInput.Button.A) && isForcemining)
        {
            Vector3 bulletDir =
               VrCamera.vrCameraHeading.position +
               VrCamera.vrCameraHeading.TransformDirection(Vector3.forward * 1.2f);
            Vector3 bulletRotation = VrCamera.vrCameraHeading.rotation.eulerAngles + new Vector3(-45, 0, 0);
            CmdSpawnBullet(bulletDir, bulletRotation, forcemining, Snowballs[0], PlayerNumber);
            Snowballs.RemoveAt(0);
            isForcemining = false;

            StartCoroutine(Rest());
        }
    }

    IEnumerator TakeAmmo(Snowball.SnowType type)
    {
        Log.Write("Taking ammo: " + type);
        IsTakingAmmo = true;
        yield return new WaitForSeconds(AmmoTakeTime);
        if (Snowballs.Count < MaxAmmo)
            Snowballs.Add(type);
        IsTakingAmmo = false;
    }

    IEnumerator Rest()
    {
        IsResting = true;
        yield return new WaitForSeconds(RestTime);
        IsResting = false;
    }

    public Vector3 GetCameraRotation()
    {
        return VrCamera.vrCameraHeading.rotation.eulerAngles;
    }

    [Command]
    public void CmdMovePLayer(Vector3 move)
    {
        CC.SimpleMove(Speed * move);
    }

    [Command]
    public void CmdSpawnBullet(Vector3 direction, Vector3 rotation, float force, Snowball.SnowType type, int playerNumber)
    {
        //int type = lstAmmo[0];
        //lstAmmo.Remove(lstAmmo[0]);
        GameObject snowball = Instantiate(SnowballPrefab, direction, Quaternion.Euler(rotation)) as GameObject;
        snowball.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * force, ForceMode.Impulse);
        snowball.GetComponent<Snowball>().SetSnow(type, playerNumber);
        Destroy(snowball, SnowballLifetime);
        NetworkServer.Spawn(snowball);

        Log.Write("Snowball spawned");
    }

    [Command(channel = 1)]
    public void CmdUpdateCameraAngle(Vector3 euler)
    {
        CameraEuler = euler;
    }

    [ServerCallback]
    void OnCollisionEnter(Collision col)
    {
        //Log.Write("Base hit!");
        if (!isServer)
            return;

        if (col.gameObject.tag == "Snowball")
        {
            var snowball = col.gameObject.GetComponent<Snowball>();

            if (snowball.ShooterName == PlayerNumber)
            {
                Log.Warning("Hit self");
                return;
            }

            DoDamage(15);
            if (snowball.Type == Snowball.SnowType.Yellow)
                Poison();

            Log.Write("Hit " + name + ", Hp: " + Health);
            Destroy(col.gameObject);
        }
    }

    void DoDamage(float value)
    {
        Health -= value;
        if (Health <= 0)
        {
            Respawn();
        }
    }

    void Poison()
    {
        // TODO Poison
    }

    void Respawn()
    {
        Log.Write("Respawning!");
        Reset();

        GetComponent<SpawnPositionChanger>().ChangeSpawn();
        transform.position = SpawnManager.Instance.NetworkPosition.transform.position;
    }
    /*[ServerCallback]
    void OnTriggerEnter(Collider col)
    {
        Log.Write("Base trigger!");
        if (!isServer)
            return;

        if (col.gameObject.tag == "Snowball")
        {
            Log.Write("Hit!");
            Destroy(col.gameObject);
        }
    }*/
}
