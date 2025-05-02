using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isHidden = false;
    private Rigidbody2D rb; // Declare the rb variable

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Example usage of SetHidden, passing a value (e.g., false)
        SetHidden(false);
    }

    public void SetHidden(bool hidden)
    {
        isHidden = hidden;
        GetComponent<Collider2D>().enabled = !hidden;
        GetComponent<SpriteRenderer>().enabled = !hidden;
    }
}
