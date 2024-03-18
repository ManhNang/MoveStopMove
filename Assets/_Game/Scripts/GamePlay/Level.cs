using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform[] spawnPoints;

    public Transform StartPoint => startPoint;

    public Vector3 GetSpawnPoint()
    {
        int randIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randIndex].position;
    }
}
