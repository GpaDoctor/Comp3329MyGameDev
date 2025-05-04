using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// public class EnemySpawner : MonoBehaviour
// {
//     [Header("Spawn Area")]
//     [SerializeField] private float minX = -5f;    // Left boundary
//     [SerializeField] private float maxX = 5f;     // Right boundary
//     [SerializeField] private float spawnY = 10f;  // Spawn height
//     [SerializeField] private float destroyY = -5f; // Destruction height

//     [Header("Spawning")]
//     [SerializeField] private GameObject[] enemyPrefabs;
//     [SerializeField] private GameObject[] keyPrefabs;
//     [SerializeField] [Range(0.1f, 5f)] private float spawnRate = 1f;
//     [SerializeField] [Range(0, 1)] private float keyChance = 0.3f;

//     void Start()
//     {
//         if (enemyPrefabs.Length == 0 && keyPrefabs.Length == 0)
//         {
//             Debug.LogError("No spawn prefabs assigned!", this);
//             enabled = false;
//             return;
//         }

//         StartCoroutine(SpawnRoutine());
//     }

//     IEnumerator SpawnRoutine()
//     {
//         while (true)
//         {
//             yield return new WaitForSeconds(spawnRate);
            
//             Vector3 spawnPos = new Vector3(
//                 Random.Range(minX, maxX),
//                 spawnY,
//                 0
//             );

//             if (ShouldSpawnKey())
//             {
//                 SpawnRandom(keyPrefabs, spawnPos, "Key");
//             }
//             else
//             {
//                 SpawnRandom(enemyPrefabs, spawnPos, "Enemy");
//             }
//         }
//     }

//     bool ShouldSpawnKey()
//     {
//         return keyPrefabs.Length > 0 && Random.value <= keyChance;
//     }

//     void SpawnRandom(GameObject[] prefabs, Vector3 position, string tag)
//     {
//         if (prefabs.Length == 0) return;
        
//         GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
//         if (!prefab) return;

//         GameObject obj = Instantiate(prefab, position, Quaternion.identity);
//         obj.tag = tag;
        
//         // Add auto-destroy component if it doesn't exist
//         if (!obj.GetComponent<DestroyWhenOutOfBounds>())
//         {
//             var destroyer = obj.AddComponent<DestroyWhenOutOfBounds>();
//             destroyer.SetBoundary(destroyY);
//         }
//     }

//     void OnDrawGizmosSelected()
//     {
//         // Draw spawn area box
//         Gizmos.color = new Color(0, 1, 1, 0.3f); // Cyan with transparency
//         Vector3 center = new Vector3((minX + maxX)/2, spawnY, 0);
//         Vector3 size = new Vector3(maxX - minX, 1, 0);
//         Gizmos.DrawCube(center, size);
        
//         // Draw spawn area boundaries
//         Gizmos.color = Color.cyan;
//         Gizmos.DrawWireCube(center, size);
        
//         // Draw destroy line
//         Gizmos.color = Color.red;
//         Gizmos.DrawLine(
//             new Vector3(minX, destroyY, 0),
//             new Vector3(maxX, destroyY, 0)
//         );
//     }
// }

using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Area")]
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;
    [SerializeField] private float spawnY = 10f;

    public float DestroyY => destroyY;
    [SerializeField] public float destroyY = -5f;

    [Header("Spawning")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] keyPrefabs;
    [SerializeField] [Range(0.1f, 5f)] private float spawnRate = 1f;
    [SerializeField] [Range(0, 1)] private float keyChance = 0.3f;

    private Coroutine spawnCoroutine;
    private bool isSpawning = true;

    void Start()
    {
        if (enemyPrefabs.Length == 0 && keyPrefabs.Length == 0)
        {
            Debug.LogError("No spawn prefabs assigned!", this);
            enabled = false;
            return;
        }

        spawnCoroutine = StartCoroutine(SpawnRoutine());
        
        // Listen for key pickup events
        PlayerKeyController.OnKeyPickedUp += StopSpawning;
        PlayerKeyController.OnKeyUsed += ResumeSpawning;
    }

    void OnDestroy()
    {
        // Clean up event subscriptions
        PlayerKeyController.OnKeyPickedUp -= StopSpawning;
        PlayerKeyController.OnKeyUsed -= ResumeSpawning;
    }

    IEnumerator SpawnRoutine()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(spawnRate);
            
            Vector3 spawnPos = new Vector3(
                Random.Range(minX, maxX),
                spawnY,
                0
            );

            if (ShouldSpawnKey())
            {
                SpawnRandom(keyPrefabs, spawnPos, "Key");
            }
            else
            {
                SpawnRandom(enemyPrefabs, spawnPos, "Enemy");
            }
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    public void ResumeSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            spawnCoroutine = StartCoroutine(SpawnRoutine());
        }
    }

    bool ShouldSpawnKey()
    {
        return keyPrefabs.Length > 0 && Random.value <= keyChance;
    }

    void SpawnRandom(GameObject[] prefabs, Vector3 position, string tag)
    {
        if (prefabs.Length == 0) return;
        
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
        if (!prefab) return;

        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        obj.tag = tag;
        
        // if (!obj.GetComponent<DestroyWhenOutOfBounds>())
        // {
        //     var destroyer = obj.AddComponent<DestroyWhenOutOfBounds>();
        //     destroyer.SetBoundary(destroyY);
        // }
            var destroyer = obj.GetComponent<DestroyWhenOutOfBounds>() ?? obj.AddComponent<DestroyWhenOutOfBounds>();
            destroyer.Initialize(destroyY); // Changed from SetBoundary to Initialize
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 1, 0.3f);
        Vector3 center = new Vector3((minX + maxX)/2, spawnY, 0);
        Vector3 size = new Vector3(maxX - minX, 1, 0);
        Gizmos.DrawCube(center, size);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(center, size);
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            new Vector3(minX, destroyY, 0),
            new Vector3(maxX, destroyY, 0)
        );
    }
}