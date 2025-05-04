using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSpawnController : MonoBehaviour
{
    void Start()
    {
        string entryID = SceneTransitionManager.Instance?.GetLastUsedTube();
        if (string.IsNullOrEmpty(entryID)) return;

        SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();
        foreach (var point in spawnPoints)
        {
            if (point.tubeID == entryID)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    player.transform.position = point.transform.position;
                }
                break;
            }
        }
    }
}
