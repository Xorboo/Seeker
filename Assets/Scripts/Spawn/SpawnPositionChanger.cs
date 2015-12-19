using UnityEngine;
using System.Collections;

public class SpawnPositionChanger : MonoBehaviour
{
    void Start()
    {
        SpawnManager.Instance.ChangeSpawnPosition();
    }
}
