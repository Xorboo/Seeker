using UnityEngine;
using System.Collections;

public class SpawnPositionChanger : MonoBehaviour
{
    void Start()
    {
        ChangeSpawn();
    }

    public void ChangeSpawn()
    {
        SpawnManager.Instance.ChangeSpawnPosition();
    }
}
