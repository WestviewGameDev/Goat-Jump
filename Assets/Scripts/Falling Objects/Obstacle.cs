using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private PlayerMovement playerMovement;

    GameManager gameManager;

    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }


    private void Update()
    {
        if (isDead)
        {
            gameManager.playerDeath();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player") {
            isDead = true;
        }

        if (collision.gameObject.tag == "Bottom Barrier")
        {
            Destroy(gameObject);
        }

    }
}

