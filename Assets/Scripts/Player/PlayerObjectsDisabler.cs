using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class PlayerObjectsDisabler : NetworkBehaviour
{
    [SerializeField]
    bool DeleteInsteadOfDisable = false;
    [SerializeField]
    List<GameObject> DisableSelf = null;
    [SerializeField]
    List<GameObject> DisableOthers = null;


    void Start()
    {
        Destroy(GameObject.Find("InitialCamera"));
        if (isLocalPlayer)
        {
            foreach (var obj in DisableSelf)
            {
                if (DeleteInsteadOfDisable)
                    Destroy(obj);
                else
                    obj.SetActive(false);
            }
        }
        else
        {
            foreach (var obj in DisableOthers)
            {
                if (DeleteInsteadOfDisable)
                    Destroy(obj);
                else
                    obj.SetActive(false);
            }
        }
    }
}