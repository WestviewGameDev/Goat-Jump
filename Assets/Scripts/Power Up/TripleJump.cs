using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleJump : MonoBehaviour
{
    public float duration = 0f;

    private PlayerMovement playerMovement;
    private float endTime = Mathf.Infinity;
    private TripleJumpScript tripleJumpBar;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        tripleJumpBar = GameObject.Find("InGame UI").GetComponentInChildren<TripleJumpScript>();
    }


    private void Update()
    {
        if(Time.time > endTime)
        {
            playerMovement.maxJumps--;
            if (playerMovement.currentJumps > playerMovement.maxJumps)
            {
                playerMovement.currentJumps = playerMovement.maxJumps;
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            endTime = Time.time + 10;
            playerMovement.maxJumps++;
            playerMovement.currentJumps++;
            tripleJumpBar.addTime(endTime);
        }
        if (collision.gameObject.tag == "Bottom Barrier")
        {
            Destroy(gameObject);
        }
    }

}
