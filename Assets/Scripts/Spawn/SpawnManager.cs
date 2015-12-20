using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField]
    int TestPoints = 3;
    [SerializeField]
    public NetworkStartPosition NetworkPosition;

    List<Vector3> SpawnPoints = new List<Vector3>();
    int LastIndex = -1;

    void Awake()
    {
        foreach(Transform child in transform)
        {
            SpawnPoints.Add(child.position);
        }

        ChangeSpawnPosition();
    }

    public void ChangeSpawnPosition()
    {
        if (NetworkPosition != null)
        {
            Vector3 newPosition = GetPosition();
            NetworkPosition.transform.position = newPosition;
        }
    }

    Vector3 GetPosition()
    {
        if (LastIndex == -1)
        {
            LastIndex = Random.Range(0, SpawnPoints.Count);
            return SpawnPoints[LastIndex];
        }

        var points = new List<Vector3>();
        for (int i = 0; i < TestPoints; i++)
        {
            points.Add(SpawnPoints.Random());
        }

        float maxDist = 0;
        Vector3 result = points.First();
        Vector3 lastPosition = SpawnPoints[LastIndex];
        foreach(var point in points)
        {
            float dist = Vector3.Distance(point, lastPosition);
            if (dist > maxDist)
            {
                maxDist = dist;
                result = point;
            }
        }

        return result;
    }
}
