using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Key : MonoBehaviour
{
    public enum KeyColor { Red, Blue, Green, Yellow }

    [Header("Key Settings")]
    public KeyColor color;

    [HideInInspector] public bool isCollected = false;

    private Transform followTarget;

    public void PickUp(Transform holder)
    {
        if (isCollected) return;

        isCollected = true;

        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<Collider2D>());

        transform.SetParent(null); // detach from scene hierarchy
        DontDestroyOnLoad(gameObject);

        followTarget = holder;
    }

    public void SetFollowTarget(Transform target)
    {
        followTarget = target;
        Debug.Log("Key is now following: " + target.name);
    }

    private void Update()
    {
        if (isCollected && followTarget != null)
        {
            // Follow the player with offset
            transform.position = followTarget.position + new Vector3(0.5f, 0.5f, 0f);
        }
    }
}

