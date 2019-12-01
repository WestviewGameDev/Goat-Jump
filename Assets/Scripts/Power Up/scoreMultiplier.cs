using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreMultiplier : MonoBehaviour
{
    float endTime  = Mathf.Infinity;
    ScoreMultiplierScript scoreMultBar;

    private void Start()
    {
        scoreMultBar = GameObject.Find("InGame UI").GetComponentInChildren<ScoreMultiplierScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            endTime = Time.time + 10;
            GameManager.instance.scoreMultiplier++;
            scoreMultBar.addTime(endTime);
        }
        if(collision.gameObject.tag == "Bottom Barrier")
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(Time.time > endTime)
        {
            GameManager.instance.scoreMultiplier--;
            Destroy(gameObject);
        }
    }

}
