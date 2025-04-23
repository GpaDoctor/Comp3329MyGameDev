using UnityEngine.SocialPlatforms.Impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeSystem : MonoBehaviour // Renamed to reflect its purpose
{
    public static int life = 5;
    public GameObject redHearts;
    public GameObject whiteHearts;
    private GameObject[] instantiatedHearts;
    public Vector2 firstHeart = new Vector2(-8f, 4f);

    void Start()
    {
        instantiatedHearts = new GameObject[5];
        InitializeHearts();
    }

    void InitializeHearts()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector2 heartPosition = new Vector2(firstHeart.x + i, firstHeart.y);
            instantiatedHearts[i] = Instantiate(redHearts, heartPosition, Quaternion.identity);
        }
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
        
        UpdateHearts();
    }

    void UpdateHearts()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector2 heartPosition = new Vector2(firstHeart.x + i, firstHeart.y);
            instantiatedHearts[i] = Instantiate(
                i < life ? redHearts : whiteHearts, 
                heartPosition, 
                Quaternion.identity
            );
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