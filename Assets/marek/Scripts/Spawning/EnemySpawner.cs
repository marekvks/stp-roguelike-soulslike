using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    private List<Transform> _unusedSpawnPoints;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _enemyCount;

    private void Start()
    {
        _unusedSpawnPoints = new List<Transform>(_spawnPoints);
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        if (_spawnPoints.Count < _enemyCount)
            _enemyCount = _spawnPoints.Count;

        for (int i = 0; i < _enemyCount; i++)
        {
            Vector3 randomSpawnPointPosition = GetRandomPosition();
            GameObject enemy = Instantiate(_enemyPrefab, randomSpawnPointPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPosition()
    {
        int randomIndex = Random.Range(0, _unusedSpawnPoints.Count);
        Transform randomTransform = _unusedSpawnPoints[randomIndex];
        _unusedSpawnPoints.Remove(randomTransform);
        return randomTransform.position;
    }
}