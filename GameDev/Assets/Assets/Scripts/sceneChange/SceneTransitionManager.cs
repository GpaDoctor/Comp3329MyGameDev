using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    private string lastUsedTubeID;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetLastUsedTube(string tubeID)
    {
        lastUsedTubeID = tubeID;
    }

    public string GetLastUsedTube()
    {
        return lastUsedTubeID;
    }
}
