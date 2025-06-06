using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerControls : MonoBehaviour
{
    float h_speed = 10;
    float v_speed = 250;
    Rigidbody2D rb;
    public Vector2 v;
    // int jumpPower = 15;

    // public Transform groundCheck;
    // public LayerMask groundLayer;
    // bool isGrounded;

    public static int life = 5;
    public GameObject redHearts;
    public GameObject whiteHearts;
    private GameObject[] instantiatedHearts;
    public Vector2 firstHeart = new Vector2(-8f, 4f);


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        v = rb.velocity;
        instantiatedHearts = new GameObject[5];
        for (int i = 0; i < 5; i++)
        {
            Vector2 heartPosition = new Vector2(firstHeart.x + i, firstHeart.y);
            instantiatedHearts[i] = Instantiate(redHearts, heartPosition, Quaternion.identity);
        }
    }

    void Update()
    {
        // isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.8f, 0.3f), CapsuleDirection2D.Horizontal, 0, groundLayer);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            v.x = -h_speed;
            rb.velocity = v;
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            v.x = h_speed;
            rb.velocity = v;
        }
        // else if (Input.GetButtonDown("Jump") && isGrounded)
        // {
        //     // v.y = v_speed;
        //     // rb.velocity = v;
        //     rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        // }

        else
        {
            v.x = 0;
            v.y = 0;
            rb.velocity = v;

        }
        rb.velocity = v;

    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        DestroyAllHearts();
        string name = obj.gameObject.name;

        if (name == "enemy")
        {
            life -= 1;
        }
        else if (name == "food")
        {
            if (life != 5)
            {
                life += 1;
            }
        }
        

        for (int i = 0; i < 5; i++)
        {
            if (i < life)
            {
                Vector2 heartPosition = new Vector2(firstHeart.x + i, firstHeart.y);
                instantiatedHearts[i] = Instantiate(redHearts, heartPosition, Quaternion.identity);
            }
            else
            {
                Vector2 heartPosition = new Vector2(firstHeart.x + i, firstHeart.y);
                instantiatedHearts[i] = Instantiate(whiteHearts, heartPosition, Quaternion.identity);
            }
          
        }
    }
    void OnGUI()
    {
        if (life == 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2, 500, 100), "Game Over!");
            Time.timeScale = 0;
        }
    }

    void DestroyAllHearts()
    {
        foreach (GameObject obj in instantiatedHearts)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
    }
}
