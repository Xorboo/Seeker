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

    public void SetSnow(SnowType type)
    {
        Type = type;
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
