using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private BoxCollider spawnBox;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Vector2 spawnInterval;
    [SerializeField] private float minSpawnDistance = 8.0f;

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (gameObject.activeSelf) {
            SpawnEnemy();
            var waitTime = Random.Range(spawnInterval.x, spawnInterval.y);
            yield return new WaitForSeconds(waitTime);
        }
    }
    
    private void SpawnEnemy()
    {
        Vector3 spawnPos;
        do
        {
            float xRand = Random.Range(spawnBox.bounds.min.x, spawnBox.bounds.max.x);
            float zRand = Random.Range(spawnBox.bounds.min.z, spawnBox.bounds.max.z);
            spawnPos = new Vector3(xRand, 1.0f, zRand);
        } while (Vector3.Distance(spawnPos, _player.transform.position) <= minSpawnDistance);

        GameObject.Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}