using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance { get; private set; }
    public Key CarriedKey { get; private set; }

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

    public void SetKey(Key key)
    {
        CarriedKey = key;
        DontDestroyOnLoad(key.gameObject);
    }

    public void DropKey()
    {
        CarriedKey = null;
    }
}

