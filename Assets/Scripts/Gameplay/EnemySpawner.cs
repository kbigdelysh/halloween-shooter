using Assets.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint = null;

    private Enemy _lastSpawned = null;

    private float minRandomRespawnTime = 1;
    private float maxRandomRespawnTime = 3;
    

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        var enemyPrefab = Resources.Load<Enemy>(Config.instance.GetPumpkinTypeSpawn());
        _lastSpawned = Instantiate(enemyPrefab, _spawnPoint.position, _spawnPoint.rotation);
        _lastSpawned.enemySpawner = this;
    }

    public void OnEnemyDeath()
    {
        if (!_lastSpawned.alive)
            Invoke(nameof(SpawnEnemy), Random.Range(minRandomRespawnTime, maxRandomRespawnTime));
    }
}
