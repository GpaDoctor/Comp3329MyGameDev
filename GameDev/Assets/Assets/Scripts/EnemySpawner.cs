using UnityEngine;
using System.Collections;

using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Area Settings")]
    [SerializeField] private float spawnHeight = 10f; // Y-position where objects spawn
    [SerializeField] private float spawnWidth = 8f;   // X-axis spawn range
    [SerializeField] private float destroyHeight = -5f; // Y-position where objects get destroyed

    [Header("Spawn Behavior")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] keyPrefabs;
    [SerializeField] [Range(0.1f, 5f)] private float spawnRate = 1f;
    [SerializeField] [Range(0, 1)] private float keySpawnChance = 0.3f;

    private Camera mainCamera;
    private float horizontalBoundary;

    void Start()
    {
        mainCamera = Camera.main;
        horizontalBoundary = mainCamera.orthographicSize * mainCamera.aspect;
        
        if (!ValidatePrefabs())
        {
            Debug.LogError("Spawner disabled due to invalid setup!", this);
            enabled = false;
            return;
        }
        
        StartCoroutine(SpawnObjects());
    }
    IEnumerator SpawnObjects()
    {
        while (true)
        {
            SpawnRandomObject();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void SpawnRandomObject()
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(-spawnWidth/2, spawnWidth/2),
            spawnHeight,
            0
        );

        if (Random.value <= keySpawnChance)
        {
            SpawnKey(spawnPos);
        }
        else if (enemyPrefabs.Length > 0)
        {
            SpawnEnemy(spawnPos);
        }
    }
    private void SpawnKey(Vector3 position)
    {
        // Safely get random key prefab
        if (keyPrefabs.Length == 0) return;
        GameObject keyPrefab = keyPrefabs[Random.Range(0, keyPrefabs.Length)];
        if (keyPrefab == null) return;

        // Instantiate key
        GameObject key = Instantiate(keyPrefab, position, Quaternion.identity);
        key.tag = "Key";
        
        // Ensure Key component exists
        Key keyScript = key.GetComponent<Key>();
        if (keyScript == null)
        {
            keyScript = key.AddComponent<Key>();
        }

        // Add auto-destruction component
        DestroyWhenOutOfBounds destroyer = key.GetComponent<DestroyWhenOutOfBounds>();
        if (destroyer == null)
        {
            destroyer = key.AddComponent<DestroyWhenOutOfBounds>();
        }
        destroyer.SetBoundary(destroyHeight);
    }

    private void SpawnEnemy(Vector3 position)
    {
        GameObject enemy = Instantiate(
            enemyPrefabs[Random.Range(0, enemyPrefabs.Length)],
            position,
            Quaternion.identity
        );
        
        enemy.tag = "Enemy";
        enemy.AddComponent<DestroyWhenOutOfBounds>().SetBoundary(destroyHeight);
    }

    private bool ValidatePrefabs()
    {
        // Ensure enemyPrefabs and keyPrefabs arrays are not empty
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogError("Enemy prefabs are not assigned or empty.", this);
            return false;
        }

        if (keyPrefabs == null || keyPrefabs.Length == 0)
        {
            Debug.LogError("Key prefabs are not assigned or empty.", this);
            return false;
        }

        return true;
    }
    
    #if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        // Draw spawn area boundaries
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(
            new Vector3(0, spawnHeight, 0),
            new Vector3(spawnWidth, 0.5f, 0)
        );
        
        // Draw destroy line
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            new Vector3(-horizontalBoundary, destroyHeight, 0),
            new Vector3(horizontalBoundary, destroyHeight, 0)
        );
    }
    #endif
}