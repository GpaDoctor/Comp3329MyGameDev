using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    // Track locks that were unlocked by their unique IDs
    private HashSet<string> unlockedLocks = new HashSet<string>();

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

    public void UnlockLock(string lockID)
    {
        unlockedLocks.Add(lockID);
    }

    public bool IsLockUnlocked(string lockID)
    {
        return unlockedLocks.Contains(lockID);
    }
}
