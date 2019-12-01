using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    private GameObject player;
    private GameObject barrier;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        barrier = GameObject.FindGameObjectWithTag("Barrier");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Barrier")
        {
            Destroy(gameObject);

        } if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameManager.instance.addScore(10);
        }
    }

}
