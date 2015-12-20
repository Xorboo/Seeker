using UnityEngine;
using System.Collections;

public class LookPoint : MonoBehaviour
{
    [SerializeField]
    PlayerMoveController Controller;

    void Update()
    {
        transform.position = Controller.LastHitPosition;
    }
}
