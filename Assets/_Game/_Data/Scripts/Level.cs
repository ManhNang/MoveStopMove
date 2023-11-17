using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject spawnPoints;

    [SerializeField] private List<Transform> listSpawnPoint;

    [SerializeField] private int numberEnemy;

    [SerializeField] private List<EnemyController> listEnemyAlive;
    public List<EnemyController> ListEnemyAlive { get => listEnemyAlive; }
    public void SpawnEnemy()
    {
        for (int i = 0; i < numberEnemy; i++)
        {
            listSpawnPoint.Add(spawnPoints.transform.GetChild(i).GetComponent<Transform>());
            EnemyController enemy = Instantiate(enemyPrefab, listSpawnPoint[i].position, Quaternion.identity).GetComponent<EnemyController>();
            listEnemyAlive.Add(enemy);
        }
    }
    public void OnPlay()
    {
        for (int i = 0; i < listEnemyAlive.Count; i++)
        {
            listEnemyAlive[i].ChangeState(new PatrolState());
        }
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        listEnemyAlive.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}
