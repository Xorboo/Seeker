using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Snowdrift : NetworkBehaviour
{
    public Snowball.SnowType Type;

    [SerializeField]
    Color NormalSnowColor = Color.blue;

    [SerializeField]
    Color YellowSnowColor = Color.yellow;

    [SerializeField]
    float MinScale = 0.2f;
    [SerializeField]
    int MaxCharges = 15;
    [SyncVar]
    public int Charges;
    int LastKnownCharges = -1;

    Renderer Renderer;

    void Awake()
    {
        Renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        Charges = 15;

        Color snowColor = Color.black;
        switch (Type)
        {
            case Snowball.SnowType.Normal:
                snowColor = NormalSnowColor;
                break;
            case Snowball.SnowType.Yellow:
                snowColor = YellowSnowColor;
                break;
        }

        GetComponent<Renderer>().material.SetColor("_Color", snowColor);
    }


    void Update()
    {
        if (LastKnownCharges != Charges)
        {
            LastKnownCharges = Charges;
            UpdateScale();
        }
    }

    public void SetType(Snowball.SnowType type)
    {
        Type = type;
    }

    // Called on client
    public void CollectSnow()
    {
        CmdCollectSnow();
    }

    public void StopHighlight()
    {
        Renderer.material.SetFloat("_Outline", -0.01f);
    }

    public void StartHighlight(bool isAvailible)
    {
        Renderer.material.SetFloat("_Outline", 0.02f);
        Renderer.material.SetColor("_OutlineColor", isAvailible ? Color.green : Color.red);
    }

    [Command]
    public void CmdCollectSnow()
    {
        Charges--;

        if (Charges <= 0)
            Destroy(gameObject, 0.5f);
    }

    void UpdateScale()
    {
        float s = MinScale + (1f - MinScale) * Charges / (float)MaxCharges;
        transform.localScale = new Vector3(s, s, s);
    }
}
