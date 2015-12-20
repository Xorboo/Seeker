using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Snowball : NetworkBehaviour
{
    [SerializeField]
    Material NormalSnowMaterial = null;

    [SerializeField]
    Material YellowSnowMaterial = null;


    public enum SnowType
    {
        Normal,
        Yellow
    }

    [SyncVar]
    public SnowType Type;
    [SyncVar]
    public int ShooterName;

    public void SetSnow(SnowType type, int playerNumber)
    {
        Type = type;
        ShooterName = playerNumber;
    }

    void Start()
    {
        Material snowMaterial = null;
        switch (Type)
        {
            case SnowType.Normal:
                snowMaterial = NormalSnowMaterial;
                break;
            case SnowType.Yellow:
                snowMaterial = YellowSnowMaterial;
                break;
        }

        GetComponent<Renderer>().material = snowMaterial;
    }

    [ServerCallback]
    void OnCollisionEnter(Collision col)
    {
        if (!isServer)
            return;

        if (col.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
